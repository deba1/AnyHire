using AnyHire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnyHire.Repository
{
    public class AppointmentRepository : Repository<Appointment>
    {
        public AppointmentRepository(AnyHireDbContext context)
            : base(context)
        {

        }
    }
}