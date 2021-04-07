using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Profile_Service.Models;
using Profile_Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Profile_Service.DataAccess;

namespace Profile_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<ProfileController> _logger;
        private ProfileRepository profRepository;

        public ProfileController(ProfileDbContext context, ILogger<ProfileController> logger)
        {
            profRepository = new ProfileRepository(context);
            _logger = logger;
        }

        [HttpPost]
        public string create([FromBody] Profile profile)
        {
            if (String.IsNullOrEmpty(profile.profileName) || String.IsNullOrEmpty(profile.userTag) || profile.accountID == 0)
            {
                return "Certain field are empty.";
            }
            else
            {
                if (profRepository.create(profile))
                {
                    return "Success!";
                }
                else
                {
                    return "Database error!";
                }
            }
        }

        [HttpGet("account")]
        public Profile getByAccountID(int accountID)
        {
            return profRepository.getByAccountID(accountID);
        }

        [HttpGet("name")]
        public Profile getByProfileName(string profileName)
        {
            return profRepository.getByProfileName(profileName);
        }

        [HttpGet("userTag")]
        public bool checkUserTag( string usertag)
        {
            return profRepository.checkUserTag(usertag);
        }

        [HttpPut]
        public bool update([FromBody] Profile profile)
        {
            return profRepository.update(profile);
        }

        [HttpDelete]
        public bool delete([FromBody] Profile profile)
        {
            return profRepository.create(profile);
        }
    }
}
