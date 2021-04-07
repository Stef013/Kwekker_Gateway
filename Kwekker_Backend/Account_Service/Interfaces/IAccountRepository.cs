using Account_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account_Service.Interfaces
{
    interface IAccountRepository
    {
        public int create(Account account);
        public Account get(Account account);
        public bool checkEmail(string email);
        public bool update(Account account);
        public bool delete(Account account);
    }
}
