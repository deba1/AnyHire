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
        IRepository<Transaction> trepo = new TransactionRepository(new AnyHireDbContext());

        //
        // GET: /Admin/
        public ActionResult Index()
        {
            if (Session["user-type"] == null || Session["user-type"].ToString() != "1")
                return Redirect("/");
            var model = new AdminViewModel();
            model.Customers = arepo.GetAll().Select(c => c.Account).Distinct();
            model.IncomeDaily = trepo.GetAll().Where(i => i.Appointment.Time == System.DateTime.Now).Sum(i=>i.TotalAmount);
            model.ServiceProvides = arepo.GetAll().Select(s => s.Package.Account).Distinct();
            model.IncomeMonth = trepo.GetAll().Where(i => i.Appointment.Time.Month == System.DateTime.Now.Month).Sum(i=>i.TotalAmount);
            return View(model);
        }
	}
}