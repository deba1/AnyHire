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
    public class AdminController : Controller
    {
        IRepository<Appointment> arepo = new AppointmentRepository(new AnyHireDbContext());


        //
        // GET: /Admin/
        public ActionResult Index()
        {
            var model = new AdminViewModel();
            model.Customers = arepo.GetAll().Select(c => c.Account).Distinct();
            model.ServiceProvides = arepo.GetAll().Select(s => s.Package.Account).Distinct();
            return View(model);
        }
	}
}