using AnyHireAPI.Attributes;
using AnyHireAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AnyHireAPI.Controllers
{
    [RoutePrefix("api/providers")]
    public class ProviderController : BaseController
    {
        AnyHireDbContext context = new AnyHireDbContext();


        [Route("")]
        public IHttpActionResult Get()
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            var provider = new List<Provider>();
            foreach (var item in context.Accounts.Where(c => c.UserTypeId == 3))
            {
                var sp = new Provider(item);
                sp.Links = sp.CreateLinks(BaseUrl, "GetAll");
                provider.Add(sp);
            }
            return Ok(provider);
        }
        [Route("{id}", Name = "GetProviderById")]
        public IHttpActionResult Get(int id)
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            var provider = context.Accounts.Where(c => c.Id == id && c.UserTypeId == 3).FirstOrDefault();
            if (provider == null)
                return StatusCode(HttpStatusCode.NoContent);
            var sp = new Provider(provider);
            sp.Links = sp.CreateLinks(BaseUrl, "GetOne");
            return Ok(sp);
        }
        [Route("")]
        public IHttpActionResult Post(Provider model)
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            if (context.Accounts.Where(c => c.Username == model.Username).FirstOrDefault() != null)
                return StatusCode(HttpStatusCode.Conflict);
            var provider = new ServiceProvider()
            {
                NID = model.NID,
                Coverage = model.Coverage,
                Commission = 0,
                JoinDate = DateTime.Now,
                Revenue = 0,
                Skills = model.Skill
            };
            context.ServiceProviders.Add(provider);
            context.SaveChanges();
            var account = new Account()
            {
                Username = model.Username,
                Password = Encrypt.CreateMD5(model.Password),
                Email = model.Email,
                Name = model.Name,
                Mobile = int.Parse(model.Mobile.Replace("+880", "")),
                UserTypeId = 3,
                Gender = model.Gender,
                Address = model.Address,
                DateOfBirth = DateTime.Parse(model.DateOfBirth),
                Active = true,
                ServiceProviderId = provider.Id
            };
            context.Accounts.Add(account);
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                context.ServiceProviders.Remove(provider);
                return Content(HttpStatusCode.NotAcceptable, e);
            }
            var sp = new Provider(account);
            sp.Links = sp.CreateLinks(BaseUrl, "Add");
            return Created(Url.Link("GetProviderById", new { id = account.Id }), sp);
        }

        [Route("{id}")]
        public IHttpActionResult Put([FromUri]int id, [FromBody]Provider model)
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;

            var account = context.Accounts.Where(c => c.Id == id && c.UserTypeId == 3).FirstOrDefault();
            if (account == null)
                return StatusCode(HttpStatusCode.NoContent);

            account.ServiceProvider.NID = model.NID;
            account.ServiceProvider.Commission = model.Commission;
            account.ServiceProvider.Coverage = model.Coverage;
            account.ServiceProvider.Revenue = model.Revenue;
            account.ServiceProvider.Skills = model.Skill;
 
            account.Password = Encrypt.CreateMD5(model.Password);
            account.Email = model.Email;
            account.Name = model.Name;
            account.Mobile = int.Parse(model.Mobile.Replace("+880", ""));
            account.Gender = model.Gender;
            account.Address = model.Address;
            account.DateOfBirth = DateTime.Parse(model.DateOfBirth);
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e);
            }
            var sp = new Provider(account);
            sp.Links = sp.CreateLinks(BaseUrl, "Edit");
            return Ok(sp);
        }

        [Route("{id}")]
        [AdminAuthenticationAttribute]
        public IHttpActionResult Delete([FromUri]int id)
        {
            var account = context.Accounts.Where(c => c.Id == id && c.UserTypeId == 3).FirstOrDefault();
            if (account == null)
                return StatusCode(HttpStatusCode.NoContent);
            context.ServiceProviders.Remove(account.ServiceProvider);
            context.Accounts.Remove(account);
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("{spid}/packages")]
        public IHttpActionResult GetPackagesByPID(int spid)
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            var packages = context.Packages.Where(p => p.ServiceProviderId == spid);
            if (packages.Count() < 1)
                return StatusCode(HttpStatusCode.NoContent);

            var list = new List<Package>();
            foreach (var item in packages)
            {
                list.Add(new Package() {
                    Id = item.Id,
                    Title = item.Title,
                    ServiceId = item.ServiceId,
                    Description = item.Description,
                    ImagePath = item.ImagePath,
                    Price = item.Price,
                    ServiceProviderId = item.ServiceProviderId,
                    Links = item.CreateLinks(BaseUrl,"GetAll")
                });
            }

            return Ok(list);
        }

        [Route("{spid}/packages/{pid}", Name="GetPackageBySpId")]
        public IHttpActionResult GetPackageByIdByPID(int spid, int pid)
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            var package = context.Packages.Where(p => p.ServiceProviderId == spid && p.Id == pid).FirstOrDefault();
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
                ServiceProviderId = package.ServiceProviderId,
                Links = package.CreateLinks(BaseUrl,"GetOne")
            };
            

            return Ok(list);
        }

        [Route("{spid}/packages")]
        public IHttpActionResult PostPackagesByPID([FromUri]int spid, [FromBody]Package model)
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            var provider = context.ServiceProviders.Where(p => p.Accounts.FirstOrDefault().Id == spid).FirstOrDefault();
            if (provider == null)
                return StatusCode(HttpStatusCode.NoContent);

            var list = new Package()
            {
                Title = model.Title,
                ServiceId = model.ServiceId,
                Description = model.Description,
                ImagePath = model.ImagePath,
                Price = model.Price,
                ServiceProviderId = spid,
                Links = model.CreateLinks(BaseUrl, "Add")
            };

            context.Packages.Add(list);
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e);
            }
            return Created(Url.Link("GetPackageBySpId", new { spid = list.ServiceProviderId, pid = list.Id }), list);
        }

        [Route("{spid}/packages/{pid}")]
        public IHttpActionResult PutPackagesByPID([FromUri]int spid, [FromUri]int pid, [FromBody]Package model)
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            var package = context.Packages.Where(p => p.ServiceProviderId == spid && p.Id == pid).FirstOrDefault();
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
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e);
            }

            pack.Links = package.CreateLinks(BaseUrl, "Edit");
            return Ok(pack);
        }

        [Route("{spid}/packages/{pid}")]
        public IHttpActionResult DeletePackagesByPID([FromUri]int spid, [FromUri]int pid)
        {
            var package = context.Packages.Where(p => p.ServiceProviderId == spid && p.Id == pid).FirstOrDefault();
            if (package == null)
                return StatusCode(HttpStatusCode.NoContent);

            context.Packages.Remove(package);

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
