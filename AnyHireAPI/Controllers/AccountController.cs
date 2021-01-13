using AnyHireAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Cryptography;

namespace AnyHireAPI.Controllers
{
    [RoutePrefix("api/users")]
    public class AccountController : ApiController
    {
        AnyHireDbContext context = new AnyHireDbContext();
        [Route("")]
        public IHttpActionResult Get()
        {
            var accounts = new List<Account>();

            foreach (var item in context.Accounts)
            {
                accounts.Add(new Account()
                {
                    Id = item.Id,
                    Username = item.Username,
                    Password = Encrypt.CreateMD5(item.Password),
                    Name = item.Name,
                    Email = item.Email,
                    Mobile = item.Mobile,
                    Gender = item.Gender,
                    DateOfBirth = item.DateOfBirth,
                    Address = item.Address,
                    Active = item.Active,
                    UserTypeId = item.UserTypeId,
                    ProfilePicture = item.ProfilePicture,
                    /*UserType = new UserType()
                    {
                        Id = item.UserType.Id,
                        Title = item.UserType.Title
                    },*/
                    ServiceProviderId = item.ServiceProviderId,
                    /*ServiceProvider = (item.ServiceProvider == null) ? null : new ServiceProvider()
                    {
                        Id = item.ServiceProvider.Id,
                        NID = item.ServiceProvider.NID,
                        Coverage = item.ServiceProvider.Coverage,
                        JoinDate = item.ServiceProvider.JoinDate,
                        Revenue = item.ServiceProvider.Revenue,
                        Skills = item.ServiceProvider.Skills,
                        Commission = item.ServiceProvider.Commission
                    },*/
                    CustomerId = item.CustomerId,
                    /*Customer = (item.Customer == null) ? null : new Customer()
                    {
                        Id = item.Customer.Id,
                        NID = item.Customer.NID
                    }*/
                });
            }
            return Ok(accounts);
        }
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var user = context.Accounts.Find(id);
            if (user == null)
                return StatusCode(HttpStatusCode.NoContent);
            var usr = new Account()
                {
                    Id = user.Id,
                    Username = user.Username,
                    Password = Encrypt.CreateMD5(user.Password),
                    Name = user.Name,
                    Email = user.Email,
                    Mobile = user.Mobile,
                    Gender = user.Gender,
                    DateOfBirth = user.DateOfBirth,
                    Address = user.Address,
                    Active = user.Active,
                    UserTypeId = user.UserTypeId,
                    ProfilePicture = user.ProfilePicture,
                    /*UserType = new UserType()
                    {
                        Id = user.UserType.Id,
                        Title = user.UserType.Title
                    },*/
                    ServiceProviderId = user.ServiceProviderId,
                    /*ServiceProvider = (user.ServiceProvider == null) ? null : new ServiceProvider()
                    {
                        Id = user.ServiceProvider.Id,
                        NID = user.ServiceProvider.NID,
                        Coverage = user.ServiceProvider.Coverage,
                        JoinDate = user.ServiceProvider.JoinDate,
                        Revenue = user.ServiceProvider.Revenue,
                        Skills = user.ServiceProvider.Skills,
                        Commission = user.ServiceProvider.Commission
                    },*/
                    CustomerId = user.CustomerId,
                    /*Customer = (user.Customer == null) ? null : new Customer()
                    {
                        Id = user.Customer.Id,
                        NID = user.Customer.NID
                    }*/
                };
            return Ok(usr);
        }
        public IHttpActionResult Post(Account model)
        {
            var user = new Account();
            if (model.UserTypeId == 2)
            {

            }
            return Created("",model);
        }
    }
}
