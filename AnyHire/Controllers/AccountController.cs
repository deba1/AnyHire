using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using AnyHire.Models;
using AnyHire.Interface;
using AnyHire.Repository;

namespace AnyHire.Controllers
{
    public class AccountController : Controller
    {
        AccountRepository repo = new AccountRepository(new AnyHireDbContext());
        CustomerRepository repoCustomer = new CustomerRepository(new AnyHireDbContext());
        ServiceProviderRepository repoSerPro = new ServiceProviderRepository(new AnyHireDbContext());
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Account model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = repo.LoginUser(model.Username,model.Password);
                if (user != null)
                {
                    Session["username"] = user.Username;
                    Session["user-id"] = user.Id;
                    Session["user-type"] = user.UserType;
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return RedirectToAction("RegisterCustomer");
        }

        [AllowAnonymous]
        public ActionResult RegisterCustomer()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult RegisterServiceProvider()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterCustomer(CustomerRegisterViewModel model)
        {
            if (repo.GetAll().Where(x => x.Username == model.Username).FirstOrDefault() != null)
                ModelState.AddModelError("", "Username already exists!");
            else if (ModelState.IsValid)
            {
                Customer customer = new Customer() { NID = model.NID };
                repoCustomer.Insert(customer);
                repoCustomer.Submit();
                Account account = new Account() { Name = model.Name,
                                                  Password = model.Password,
                                                  Username = model.Username,
                                                  Email = model.Email,
                                                  Mobile = model.Mobile,
                                                  Gender = model.Gender,
                                                  DateOfBirth = model.DateOfBirth,
                                                  Address = model.Address,
                                                  CustomerId = customer.Id,
                                                  Customer = customer,
                                                  ProfilePicture = "",
                                                  UserType = 2
                                                };
                repo.Insert(account);
                repo.Submit();
                return Redirect("/");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterServiceProvider(ServiceProviderRegisterViewModel model)
        {
            if (repo.GetAll().Where(x => x.Username == model.Username).FirstOrDefault() != null)
                ModelState.AddModelError("", "Username already exists!");
            else if (ModelState.IsValid)
            {
                ServiceProvider serpro = new ServiceProvider() { NID = model.NID,
                                                                 Coverage = model.Coverage,
                                                                 JoinDate = System.DateTime.Now,
                                                                 Skills = model.Skills,
                                                                 Revenue = 0,
                                                                 Commission = 0,
                                                                 Activated = false
                                                               };
                repoSerPro.Insert(serpro);
                repoSerPro.Submit();

                Account account = new Account()
                {
                    Name = model.Name,
                    Password = model.Password,
                    Username = model.Username,
                    Email = model.Email,
                    Mobile = model.Mobile,
                    Gender = model.Gender,
                    DateOfBirth = model.DateOfBirth,
                    Address = model.Address,
                    ServiceProviderId = serpro.Id,
                    ServiceProvider = serpro,
                    ProfilePicture = "",
                    UserType = 3
                };
                repo.Insert(account);
                repo.Submit();
                return Redirect("/");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Manage
        [Authorize]
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Manage(ManageUserViewModel model)
        {
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (ModelState.IsValid)
            {
                    
            }
            else
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        
        public ActionResult LogOut()
        {
            Session.Abandon();
            return Redirect("/");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }


        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}