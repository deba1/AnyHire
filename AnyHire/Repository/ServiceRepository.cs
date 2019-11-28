using AnyHire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnyHire.Repository
{
    public class ServiceRepository : Repository<Service>
    {
        public ServiceRepository(AnyHireDbContext context)
            : base(context)
        {

        }
    }
}