using Profile_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Profile_Service.Interfaces
{
    interface IProfileRepository
    {
        public bool create(Profile profile);
        public Profile getByAccountID(int accountID);
        public Profile getByProfileName(string profileName);
        public bool checkUserTag(string userTag);
        public bool update(Profile profile);
        public bool delete(Profile profile);
    }
}
