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
        IRepository<Customer> crepo = new CustomerRepository(new AnyHireDbContext);

        //
        // GET: /Admin/
        public ActionResult Index()
        {   
            var model = new AdminViewModel();
            var Customers = crepo.GetAll();
            model.Customers=Customers;
            return View(model);
        }
	}
}