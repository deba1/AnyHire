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
        IRepository<Appointment> arepo = new AppointmentRepository(new AnyHireDbContext());
        IRepository<Transaction> trepo = new TransactionRepository(new AnyHireDbContext());
        //
        // GET: /ServiceProvider/
        public ActionResult Index()
        {
            var uid = (int)Session["user-id"];
            var model = new ServiceProviderViewModel();
            model.Packages = sprepo.GetById(uid).Packages;
            model.Appointments = arepo.GetAll().Where(a => a.Package.Account.Id == uid).OrderBy(o=>o.Time);
            var trans = trepo.GetAll().Where(t => t.ServiceProviderId == uid);
            if(Request["fromDate"]!=null && Request["toDate"]!=null){
                var fromDate = Convert.ToDateTime(Request["fromDate"]);
                var toDate = Convert.ToDateTime(Request["toDate"]);
                trans = trans.Where(t=>t.Appointment.Time>=fromDate && t.Appointment.Time<=toDate);
            }
            model.Transactions = trans;
            return View(model);
        }
	}
}