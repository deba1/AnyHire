using AnyHire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnyHire.Repository
{
    public class TransactionRepository : Repository<Transaction>
    {
        public TransactionRepository(AnyHireDbContext context)
            : base(context)
        {

        }
    }
}