using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Account_Service.DataAccess;
using Account_Service.Models;
using Account_Service.Repository;

namespace Account_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private AccountRepository accRepository;

        public AccountController(AccountDbContext context, ILogger<AccountController> logger)
        {
            accRepository = new AccountRepository(context);
            _logger = logger;
        }

        [HttpPost]
        public ResponseModel create([FromBody]Account account)
        {
            ResponseModel response = new ResponseModel();

            if(String.IsNullOrEmpty(account.email) || String.IsNullOrEmpty(account.password))
            {
                response.success = false;
                response.message = "Email and Password cannot be empty";
            }
            else
            {
                int accountID = accRepository.create(account);

                if ( accountID > 0)
                {
                    response.success = true;
                    response.message = "Success!";
                    response.accountID = accountID;
                }
                else
                {
                    response.success = false;
                    response.message = "Database Error!";
                }
            }

            return response;
        }

        [HttpGet]
        public string get()
        {
            return "accountservice";
        }

        [HttpGet("email")]
        public bool checkEmail(string email)
        {
            return accRepository.checkEmail(email);
        }

        [HttpPut]
        public bool update([FromBody] Account account)
        {
            return accRepository.update(account);
        }

        [HttpDelete]
        public bool delete([FromBody] Account account)
        {
            return accRepository.delete(account);
        }

    }
}
