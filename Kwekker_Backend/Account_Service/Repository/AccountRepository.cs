using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account_Service.DataAccess;
using Account_Service.Interfaces;
using Account_Service.Models;

namespace Account_Service.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDbContext _context;

        public AccountRepository(AccountDbContext context)
        {
            _context = context;
        }

        public int create(Account account)
        {
            int accountID;
            try
            {
                using (var context = _context)
                {
                    context.Account.Add(account);
                    context.SaveChanges();

                    accountID = account.ID;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                accountID = 0;
            }
            
            return accountID;
        }

        public Account get(Account account)
        {
            Account acc;
            try
            {
                using (var context = _context)
                {
                    acc = _context.Account
                        .Single(a => a.email == account.email && a.password == account.password);

                }
                return acc;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public bool checkEmail(string email)
        {
            bool result;
            try
            {
                using (var context = _context)
                {
                    result = _context.Account
                        .Any(a => a.email == email);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result = false;
            }

            return result;
        }

        public bool update(Account account)
        {
            bool result;
            try
            {
                using (var context = _context)
                {
                    var acc = _context.Account
                        .Single(a => a.ID == account.ID);
                    acc.email = account.email;
                    acc.password = account.password;
                    context.SaveChanges();
                }
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result = false;
            }

            return result;
        }

        public bool delete(Account account)
        {
            bool result;

            try
            {
                using (var context = _context)
                {
                    var acc = _context.Account
                        .Single(a => a.ID == account.ID && a.email == account.email && a.password == account.password);

                    context.Account.Remove(acc);
                    context.SaveChanges();
                }
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result = false;
            }

            return result;
        }
    }
}
