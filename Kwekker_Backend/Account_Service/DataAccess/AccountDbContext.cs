using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Account_Service.DataAccess
{
    public class AccountDbContext : DbContext
    {
        public DbSet<Account> Account { get; set; }

        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        {
        }       
    }
}
