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
    [RoutePrefix("api/transactions")]
    public class TransactionController : BaseController
    {
        AnyHireDbContext context = new AnyHireDbContext();
        [Route("")]
        public IHttpActionResult Get()
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            var list = new List<Transaction>();
            foreach (var item in context.Transactions)
            {
                list.Add(new Transaction()
                {
                    Id = item.Id,
                    AppointmentId = item.AppointmentId,
                    CompanyRevenue = item.CompanyRevenue,
                    ServiceProviderId = item.ServiceProviderId,
                    ServiceProviderRevenue = item.ServiceProviderRevenue,
                    TotalAmount = item.TotalAmount,
                    Links = item.CreateLinks(BaseUrl, "GetAll")
                });
            }
            return Ok(list);
        }

        [Route("{id}", Name = "GetTransactionById")]
        public IHttpActionResult Get(int id)
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            var transaction = context.Transactions.Find(id);
            if (transaction == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(new Transaction() {
                Id = transaction.Id,
                AppointmentId = transaction.AppointmentId,
                CompanyRevenue = transaction.CompanyRevenue,
                ServiceProviderId = transaction.ServiceProviderId,
                ServiceProviderRevenue = transaction.ServiceProviderRevenue,
                TotalAmount = transaction.TotalAmount,
                Links = transaction.CreateLinks(BaseUrl, "GetOne")
            });
        }

        [Route("{id}")]
        [AdminAuthenticationAttribute]
        public IHttpActionResult Put([FromUri]int id, [FromBody]Transaction transaction)
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tran = context.Transactions.Find(id);
            if (tran == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            tran.Id = id;
            tran.AppointmentId = transaction.AppointmentId;
            tran.CompanyRevenue = transaction.CompanyRevenue;
            tran.ServiceProviderId = transaction.ServiceProviderId;
            tran.ServiceProviderRevenue = transaction.ServiceProviderRevenue;
            tran.TotalAmount = transaction.TotalAmount;

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e);
            }

            tran.Links = transaction.CreateLinks(BaseUrl, "Edit");
            return Ok(tran.Clone());
        }

        [Route("")]
        [AdminAuthenticationAttribute]
        public IHttpActionResult Post(Transaction transaction)
        {
            BaseUrl = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Transactions.Add(transaction);
            context.SaveChanges();

            transaction.Links = transaction.CreateLinks(BaseUrl, "Add");
            return CreatedAtRoute("GetTransactionById", new { id = transaction.Id }, transaction.Clone());
        }

        [Route("{id}")]
        [AdminAuthenticationAttribute]
        public IHttpActionResult Delete(int id)
        {
            Transaction transaction = context.Transactions.Find(id);
            if (transaction == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            context.Transactions.Remove(transaction);

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
