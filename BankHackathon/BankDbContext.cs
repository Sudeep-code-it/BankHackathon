using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankHackathon
{
    internal class BankDbContext: DbContext
    {

        public BankDbContext() : base("defaultConnection")
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions {  get; set; }   

       
    }

}
