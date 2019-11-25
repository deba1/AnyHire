using AnyHire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnyHire.Repository
{
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository(AnyHireDbContext context)
            : base(context)
        {

        }
    }
}