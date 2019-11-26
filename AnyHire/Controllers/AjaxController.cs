using AnyHire.Interface;
using AnyHire.Models;
using AnyHire.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AnyHire.Controllers
{
    public class AjaxController : ApiController
    {
        [Route("api/services")]
        public IHttpActionResult GetServices()
        {
            IRepository<Service> repo = new ServiceRepository(new AnyHireDbContext());
            var ser = repo.GetAll();
            var all = new List<Service>();
            foreach (var item in ser)
            {
                all.Add(new Service() { Id = item.Id, Name = item.Name });
            }
            return Ok(all);
        }
    }
}
