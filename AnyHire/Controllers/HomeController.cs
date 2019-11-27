using AnyHire.Interface;
using AnyHire.Models;
using AnyHire.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnyHire.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Service> repoSer = new ServiceRepository(new AnyHireDbContext());
        IRepository<Package> prepo = new PackageRepository(new AnyHireDbContext());
        public ActionResult Index()
        {
            var model = new HomeViewModel();
            model.Services = repoSer.GetAll();
            model.Packages = prepo.GetAll().Take(8);
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Web based Management System for hiring and providing Services.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Us.";

            return View();
        }
    }
}