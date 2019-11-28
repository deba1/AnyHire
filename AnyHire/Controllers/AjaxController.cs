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

        [Route("api/packages")]
        public IHttpActionResult GetPackages()
        {
            var repo = new PackageRepository(new AnyHireDbContext());
            var ser = repo.GetAll();
            var all = new List<Package>();
            foreach (var item in ser)
            {
                all.Add(new Package() { Id = item.Id, Title = item.Title, Service = new Service() { Id = item.ServiceId, Name = item.Service.Name } });
            }
            return Ok(all);
        }

        [Route("api/locations")]
        public IHttpActionResult GetLocations()
        {
            var sp = new ServiceProviderRepository(new AnyHireDbContext()).GetAll();
            List<string> locs = new List<string>();
            foreach (var item in sp)
            {
                foreach (var loc in item.Coverage.Split(','))
                {
                    if (!locs.Contains(loc))
                        locs.Add(loc);
                }
            }
            return Ok(locs);
        }

        [Route("api/locations/{pid}")]
        public IHttpActionResult GetLocations(int pid)
        {
            var pr = new PackageRepository(new AnyHireDbContext()).GetById(pid);
            var sp = pr.Account.ServiceProvider.Coverage;
            List<string> locs = new List<string>();
            foreach (var loc in sp.Split(','))
            {
                if (!locs.Contains(loc))
                    locs.Add(loc);
            }
            return Ok(locs);
        }

        [Route("api/apply/{id}")]
        public IHttpActionResult PostApply([FromUri]int id, [FromBody]AppointmentViewModel avm)
        {
            var arepo = new AppointmentRepository(new AnyHireDbContext());
            try
            {
                var app = new Appointment();
                app.Completed = false;
                app.Location = avm.Location;
                app.PackageId = id;
                app.CustomerId = avm.uid;
                app.Time = Convert.ToDateTime(avm.Date + " " + avm.Time.Replace('-',':'));
                arepo.Insert(app);
                arepo.Submit();
            }
            catch (Exception e)
            {
                return Content<string>(HttpStatusCode.NotAcceptable, e.Message);
            }
            return Created("/Browse/Package/"+id,"Appointment Scheduled Successfully.");
        }
    }
}
