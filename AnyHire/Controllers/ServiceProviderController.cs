using AnyHire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnyHire.Interface;
using AnyHire.Repository;

namespace AnyHire.Controllers
{
    public class ServiceProviderController : Controller
    {
        IRepository<Account> sprepo = new AccountRepository(new AnyHireDbContext());
        //
        // GET: /ServiceProvider/
        public ActionResult Index()
        {
            var model = new ServiceProviderViewModel();
            model.Packages = sprepo.GetById(4).Packages;
            return View(model);
        }
	}
}