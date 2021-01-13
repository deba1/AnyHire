using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AnyHireAPI.Models;
using AnyHireAPI.Attributes;


namespace AnyHireAPI.Controllers
{
    [RoutePrefix("api/services")]
    public class ServiceController : ApiController
    {
        string BaseUrl;
        private AnyHireDbContext db = new AnyHireDbContext();

        [Route("")]
        public IHttpActionResult GetServices()
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            var list = new List<Service>();
            foreach (var item in db.Services)
            {
                list.Add(new Service()
                {
                    Id = item.Id,
                    Name = item.Name,
                    ImagePath = item.ImagePath,
                    Links = item.CreateLinks(BaseUrl, "GetAll")
                });
            }
            return Ok(list);
        }

        [Route("{id}", Name="GetServiceById")]
        public IHttpActionResult GetService(int id)
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            service.Links = service.CreateLinks(BaseUrl, "GetOne");
            return Ok(service.Clone());
        }

        [Route("{id}")]
        [AdminAuthenticationAttribute]
        public IHttpActionResult PutService([FromUri]int id, [FromBody]Service service)
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var serv = db.Services.Find(id);
            if (serv == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            serv.Id = id;
            serv.Name = service.Name;
            serv.ImagePath = service.ImagePath;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e);
            }
            serv.Links = service.CreateLinks(BaseUrl, "Edit");
            return Ok(serv.Clone());
        }

        [Route("")]
        [AdminAuthenticationAttribute]
        public IHttpActionResult PostService(Service service)
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Services.Add(service);
            db.SaveChanges();
            service.Packages = new List<Package>();
            service.Links = service.CreateLinks(BaseUrl, "Add");
            return CreatedAtRoute("GetServiceById", new { id = service.Id }, service);
        }

        [Route("{id}")]
        [AdminAuthenticationAttribute]
        public IHttpActionResult DeleteService(int id)
        {
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            db.Services.Remove(service);

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("{sid}/packages")]
        public IHttpActionResult GetPackages(int sid)
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            var packages = db.Packages.Where(p => p.ServiceId == sid);
            if (packages.Count() < 1)
                return StatusCode(HttpStatusCode.NoContent);

            var list = new List<Package>();
            foreach (var item in packages)
            {
                list.Add(new Package()
                {
                    Id = item.Id,
                    Title = item.Title,
                    ServiceId = item.ServiceId,
                    Description = item.Description,
                    ImagePath = item.ImagePath,
                    Price = item.Price,
                    ServiceProviderId = item.ServiceId,
                    Links = item.CreateLinks(BaseUrl, "GetAll")
                });
            }

            return Ok(list);
        }

        [Route("{sid}/packages/{pid}", Name = "GetPackageById")]
        public IHttpActionResult GetPackageById(int sid, int pid)
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            var package = db.Packages.Where(p => p.ServiceId == sid && p.Id == pid).FirstOrDefault();
            if (package == null)
                return StatusCode(HttpStatusCode.NoContent);

            var list = new Package()
            {
                Id = package.Id,
                Title = package.Title,
                ServiceId = package.ServiceId,
                Description = package.Description,
                ImagePath = package.ImagePath,
                Price = package.Price,
                ServiceProviderId = package.ServiceId,
                Links = package.CreateLinks(BaseUrl, "GetOne")
            };


            return Ok(list);
        }

        [Route("{sid}/packages")]
        [ProviderAuthentication]
        public IHttpActionResult PostPackages([FromUri]int sid, [FromBody]Package model)
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            var provider = db.ServiceProviders.Where(p => p.Accounts.FirstOrDefault().Id == sid).FirstOrDefault();
            if (provider == null)
                return StatusCode(HttpStatusCode.NoContent);

            var list = new Package()
            {
                Title = model.Title,
                ServiceId = model.ServiceId,
                Description = model.Description,
                ImagePath = model.ImagePath,
                Price = model.Price,
                ServiceProviderId = sid,
                Links = model.CreateLinks(BaseUrl, "Add")
            };

            db.Packages.Add(list);
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e);
            }
            return Created(Url.Link("GetPackageById", new { sid = list.ServiceId, pid = list.Id }), list);
        }

        [Route("{sid}/packages/{pid}")]
        [AdminAuthentication]
        public IHttpActionResult PutPackages([FromUri]int sid, [FromUri]int pid, [FromBody]Package model)
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            var package = db.Packages.Where(p => p.ServiceId == sid && p.Id == pid).FirstOrDefault();
            if (package == null)
                return StatusCode(HttpStatusCode.NoContent);

            package.Title = model.Title;
            package.ServiceId = model.ServiceId;
            package.Description = model.Description;
            package.ImagePath = model.ImagePath;
            package.Price = model.Price;
            package.Links = model.CreateLinks(BaseUrl, "Edit");

            var pack = package.Clone();

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e);
            }

            pack.Links = package.CreateLinks(BaseUrl, "Edit");
            return Ok(pack.Clone());
        }

        [Route("{sid}/packages/{pid}")]
        
        public IHttpActionResult DeletePackages([FromUri]int sid, [FromUri]int pid)
        {
            var package = db.Packages.Where(p => p.ServiceId == sid && p.Id == pid).FirstOrDefault();
            if (package == null)
                return StatusCode(HttpStatusCode.NoContent);

            db.Packages.Remove(package);

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("{sid}/packages/{pid}/appointments")]
        public IHttpActionResult GetAppointments(int sid, int pid)
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            var list = new List<Appointment>();
            var appoints = db.Appointments.Where(a=>a.PackageId == pid && a.Package.ServiceId == sid);
            if (appoints.Count()<1)
                return StatusCode(HttpStatusCode.NoContent);
            foreach (var item in appoints)
            {
                list.Add(new Appointment() { 
                    Id = item.Id,
                    Completed = item.Completed,
                    CustomerId = item.CustomerId,
                    Location = item.Location,
                    PackageId = item.PackageId,
                    Time = item.Time,
                    Links = item.CreateLinks(BaseUrl,"GetAll")
                });
            }
            return Ok(list);
        }

        [Route("{sid}/packages/{pid}/appointments/{aid}", Name="GetAppointmentById")]
        public IHttpActionResult GetAppointmentById(int sid, int pid, int aid)
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            var item = db.Appointments.Where(a => a.PackageId == pid && a.Package.ServiceId == sid && a.Id == aid).FirstOrDefault();
            if (item == null)
                return StatusCode(HttpStatusCode.NoContent);

            item.Links = item.CreateLinks(BaseUrl, "GetOne");
            var appoints = item.Clone();
            return Ok(appoints);
        }

        [Route("{sid}/packages/{pid}/appointments")]
        public IHttpActionResult PostAppointment(int sid, int pid, Appointment model)
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            

            var appoint = new Appointment()
            {
                Completed = model.Completed,
                CustomerId = model.CustomerId,
                Location = model.Location,
                PackageId = model.PackageId,
                Time = model.Time
            };
            try
            {
                db.Appointments.Add(appoint);
                db.SaveChanges();
            }
            catch (Exception e){
                return Content(HttpStatusCode.BadRequest,e);
            }


            appoint.Links = appoint.CreateLinks(BaseUrl, "Add");

            return CreatedAtRoute("GetAppointmentById", new { sid = appoint.Package.ServiceId, pid = appoint.PackageId, aid = appoint.Id }, appoint.Clone());
        }

        [Route("{sid}/packages/{pid}/appointments/{aid}")]
        public IHttpActionResult PutAppointment(int sid, int pid, int aid, Appointment model)
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            var item = db.Appointments.Where(a => a.PackageId == pid && a.Package.ServiceId == sid && a.Id == aid).FirstOrDefault();
            if (item == null)
                return StatusCode(HttpStatusCode.NoContent);
            item.Completed = model.Completed;
            item.CustomerId = model.CustomerId;
            item.Location = model.Location;
            item.PackageId = model.PackageId;
            item.Time = model.Time;
            
            item.Links = item.CreateLinks(BaseUrl, "Edit");
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, e);
            }
            var appoints = item.Clone();
            return Ok(appoints);
        }


        [Route("{sid}/packages/{pid}/appointments/{aid}")]
        public IHttpActionResult DeleteAppointment(int sid, int pid, int aid)
        {
            var item = db.Appointments.Where(a => a.PackageId == pid && a.Package.ServiceId == sid && a.Id == aid).FirstOrDefault();
            if (item == null)
                return StatusCode(HttpStatusCode.NoContent);
            
            try
            {
                db.Appointments.Remove(item);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, e);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}