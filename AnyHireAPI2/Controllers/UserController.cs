using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AnyHireAPI.Models;

namespace AnyHireAPI.Controllers
{
    public class UserController : ApiController
    {
        AnyHireDbContext context = new AnyHireDbContext();
        public IHttpActionResult Get()
        {
            var accounts = context.Accounts;
            foreach (var item in accounts)
            {
                item.Appointments = new List<Appointment>();
                item.Feedbacks = new List<Feedback>();
                item.Notices = new List<Notice>();
                item.Notices1 = new List<Notice>();
                item.Packages = new List<Package>();
                item.Transactions = new List<Transaction>();
            }
            return Ok(accounts);
        }
    }
}
