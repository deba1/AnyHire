using AnyHire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnyHire.Repository
{
    public class ServiceProviderRepository : Repository<ServiceProvider>
    {
        public ServiceProviderRepository(AnyHireDbContext context)
            : base(context)
        {

        }
    }
}