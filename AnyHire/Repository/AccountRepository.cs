using AnyHire.Models;
using AnyHire.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnyHire.Repository
{
    public class AccountRepository : Repository<Account>
    {
        public AccountRepository(AnyHireDbContext context)
            : base(context)
        {

        }

        public Account LoginUser(string username, string password)
        {
            return this.GetAll().Where(a=>a.Username == username && a.Password == password).FirstOrDefault();
        }

    }
}