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

        public bool IsAdmin(Account user)
        {
            return user.UserType == 1;
        }

        public bool IsCustomer(Account user)
        {
            return user.UserType == 2;
        }

        public bool IsServiceProvider(Account user)
        {
            return user.UserType == 3;
        }

        public Customer GetCustomer(Account user)
        {
            return user.Customer;
        }

        public ServiceProvider GetServiceProvider(Account user)
        {
            return user.ServiceProvider;
        }
    }
}