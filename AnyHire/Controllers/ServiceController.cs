using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AnyHire.Models;
using AnyHire.Interface;
using AnyHire.Repository;
using System.IO;

namespace AnyHire.Controllers
{
    public class ServiceController : Controller
    {
        IRepository<Service> srepo = new ServiceRepository(new AnyHireDbContext());
        private AnyHireDbContext db = new AnyHireDbContext();
        

        // GET: /Service/
        public ActionResult Index()
        {
            return View(srepo.GetAll());
        }

        // GET: /Service/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: /Service/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Service/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,ImagePath")] Service service)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    var postedFile = Request.Files[0];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        string imagesPath = HttpContext.Server.MapPath("~/Content/Images");
                        string extension = Path.GetExtension(postedFile.FileName);
                        string newFileName = "service_" + System.DateTime.Now.ToString("yyMMddHHmmss") + extension;
                        string saveToPath = Path.Combine(imagesPath, newFileName);
                        service.ImagePath = newFileName;
                        postedFile.SaveAs(saveToPath);
                    }
                }
                srepo.Insert(service);
                srepo.Submit();
                return RedirectToAction("Index");
            }

            return View(service);
        }

        // GET: /Service/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: /Service/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Service service)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    var postedFile = Request.Files[0];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        string imagesPath = HttpContext.Server.MapPath("~/Content/Images");
                        string extension = Path.GetExtension(postedFile.FileName);
                        string newFileName = "service_" + System.DateTime.Now.ToString("yyMMddHHmmss") + extension;
                        string saveToPath = Path.Combine(imagesPath, newFileName);
                        service.ImagePath = newFileName;
                        postedFile.SaveAs(saveToPath);
                    }
                }
                srepo.Update(service);
                srepo.Submit();
                return RedirectToAction("Index");
            }
            return View(service);
        }

        // GET: /Service/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: /Service/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
