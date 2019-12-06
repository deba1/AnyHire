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
    public class AppointmentController : Controller
    {
        IRepository<Account> acrepo = new AccountRepository(new AnyHireDbContext());
        IRepository<Appointment> arepo = new AppointmentRepository(new AnyHireDbContext());
        IRepository<Transaction> trepo = new TransactionRepository(new AnyHireDbContext());
        //
        // GET: /Appointment/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Complete(int id, int NID)
        {
            var uid = (int)Session["user-id"];
            var app = arepo.GetById(id);
            var cnid = app.Account.Customer.NID;
            if (cnid == NID)
            {
                app.Completed = true;
                var trans = new Transaction() {
                    AppointmentId = app.Id,
                    CompanyRevenue = 50,
                    ServiceProviderRevenue = app.Package.Price,
                    ServiceProviderId = uid,
                    TotalAmount = app.Package.Price+50
                };
                trepo.Insert(trans);
                arepo.Update(app);
                trepo.Submit();
                arepo.Submit();
            }
            else
            {
                TempData["Error"] = "NID didt Match!";
            }
            return Redirect("/ServiceProvider/#appointments");
        }
	}
}