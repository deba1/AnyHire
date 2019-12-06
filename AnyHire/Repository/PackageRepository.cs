using AnyHire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnyHire.Repository
{
    public class PackageRepository : Repository<Package>
    {
        public PackageRepository(AnyHireDbContext context)
            : base(context)
        {

        }
    }
}