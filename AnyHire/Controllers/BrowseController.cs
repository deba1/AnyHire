using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnyHire.Repository;
using AnyHire.Interface;
using AnyHire.Models;

namespace AnyHire.Controllers
{
    public class BrowseController : Controller
    {
        IRepository<Service> srepo = new ServiceRepository(new AnyHireDbContext());
        IRepository<Package> prepo = new PackageRepository(new AnyHireDbContext());
        //
        // GET: /Browse/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Service(int? id)
        {
            if (id.HasValue)
            {
                var sid = id ?? 0;
                return View(srepo.GetById(sid).Packages);
            }
            
            return View("Services",srepo.GetAll());
        }


        public ActionResult Package(int? id)
        {
            if (id.HasValue)
            {
                var pid = id ?? 0;
                return View(prepo.GetById(pid));
            }
            
            return View("Service", prepo.GetAll());
        }

        public ActionResult Provider()
        {
            return View();
        }
	}
}