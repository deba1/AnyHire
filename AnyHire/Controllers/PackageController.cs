﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AnyHire.Models;
using AnyHire.Repository;
using AnyHire.Interface;
using System.IO;

namespace AnyHire.Controllers
{
    public class PackageController : Controller
    {
        IRepository<Package> prepo = new PackageRepository(new AnyHireDbContext());
        IRepository<Service> srepo = new ServiceRepository(new AnyHireDbContext());
        private AnyHireDbContext db = new AnyHireDbContext();

        // GET: /Package/
        public ActionResult Index()
        {
            return Redirect("/ServiceProvider#packages");
        }

        // GET: /Package/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = prepo.GetById(id??0);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // GET: /Package/Create
        public ActionResult Create()
        {
            ViewBag.ServiceId = new SelectList(srepo.GetAll(), "Id", "Name");
            return View();
        }

        // POST: /Package/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Title,ServiceId,ServiceProviderId,Description,ImagePath,Price")] Package package)
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
                        string newFileName = Session["username"] + "_" + System.DateTime.Now.ToString("yyMMddHHmmss") + extension;
                        string saveToPath = Path.Combine(imagesPath, newFileName);
                        package.ImagePath = newFileName;
                        postedFile.SaveAs(saveToPath);
                    }
                }
                prepo.Insert(package);
                prepo.Submit();
                return RedirectToAction("Index");
            }

            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Name", package.ServiceId);
            return View(package);
        }

        // GET: /Package/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = prepo.GetById(id??0);
            if (package == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Name", package.ServiceId);
            return View(package);
        }

        // POST: /Package/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Title,ServiceId,ServiceProviderId,Description,ImagePath,Price")] Package package)
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
                        string newFileName = Session["username"] + "_" + System.DateTime.Now.ToString("yyMMddHHmmss") + extension;
                        string saveToPath = Path.Combine(imagesPath, newFileName);
                        package.ImagePath = newFileName;
                        postedFile.SaveAs(saveToPath);
                    }
                }
                package.ServiceProviderId = (int)Session["user-id"];
                prepo.Update(package);
                prepo.Submit();
                return RedirectToAction("Index");
            }
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Name", package.ServiceId);
            return View(package);
        }

        // GET: /Package/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = prepo.GetById(id??0);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // POST: /Package/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Package package = db.Packages.Find(id);
            db.Packages.Remove(package);
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
