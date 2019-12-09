using AnyHireAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AnyHireAPI.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomerController : BaseController
    {
        AnyHireDbContext context = new AnyHireDbContext();


        [Route("")]
        public IHttpActionResult Get()
        {
            BaseUrl = Request.RequestUri.Scheme + "://"+Request.RequestUri.Host+":"+Request.RequestUri.Port;
            var customers = new List<Consumer>();
            foreach (var item in context.Accounts.Where(c=>c.UserTypeId==2))
            {
                var consumer = new Consumer(item);
                consumer.Links = consumer.CreateLinks(BaseUrl, "GetAll");
                customers.Add(consumer);
            }
            return Ok(customers);
        }
        [Route("{id}", Name="GetCustomerById")]
        public IHttpActionResult Get(int id)
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            var customer = context.Accounts.Where(c => c.Id == id && c.UserTypeId == 2).FirstOrDefault();
            if (customer == null)
                return StatusCode(HttpStatusCode.NoContent);
            var consumer = new Consumer(customer);
            consumer.Links = consumer.CreateLinks(BaseUrl, "GetOne");
            return Ok(consumer);
        }
        [Route("")]
        public IHttpActionResult Post(Consumer model)
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            if (context.Accounts.Where(c=>c.Username == model.Username).FirstOrDefault() != null)
                return StatusCode(HttpStatusCode.Conflict);
            var customer = new Customer()
            {
                NID = model.NID
            };
            context.Customers.Add(customer);
            context.SaveChanges();
            var account = new Account()
            {
                Username = model.Username,
                Password = Encrypt.CreateMD5(model.Password),
                Email = model.Email,
                Name = model.Name,
                Mobile = int.Parse(model.Mobile.Replace("+880","")),
                UserTypeId = 2,
                Gender = model.Gender,
                Address = model.Address,
                DateOfBirth = DateTime.Parse(model.DateOfBirth),
                Active = true,
                CustomerId = customer.Id
            };
            context.Accounts.Add(account);
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                context.Customers.Remove(customer);
                return Content(HttpStatusCode.NotAcceptable, e);
            }
            var consumer = new Consumer(account);
            consumer.Links = consumer.CreateLinks(BaseUrl,"Add");
            return Created(Url.Link("GetCustomerById", new { id = account.Id }),consumer);
        }

        [Route("{id}")]
        public IHttpActionResult Put([FromUri]int id, [FromBody]Consumer model)
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            var account = context.Accounts.Where(c => c.Id == id && c.UserTypeId == 2).FirstOrDefault();
            if (account == null)
                return StatusCode(HttpStatusCode.NoContent);
            account.Customer.NID = model.NID;
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
            var consumer = new Consumer(account);
            consumer.Links = consumer.CreateLinks(BaseUrl, "Edit");
            return Ok(consumer);
        }

        [Route("{id}")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            var account = context.Accounts.Where(c => c.Id == id && c.UserTypeId == 2).FirstOrDefault();
            if (account == null)
                return StatusCode(HttpStatusCode.NoContent);
            context.Customers.Remove(account.Customer);
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
    }
}
