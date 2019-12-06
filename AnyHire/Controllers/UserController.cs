using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AnyHire.Models;

namespace AnyHire.Controllers
{
    public class UserController : Controller
    {
        private AnyHireDbContext db = new AnyHireDbContext();

        // GET: /User/
        public ActionResult Index()
        {
            if (Session["user-type"] == null || Session["user-type"].ToString() != "1")
                return Redirect("/");
            var accounts = db.Accounts.Include(a => a.Customer).Include(a => a.ServiceProvider).Include(a => a.UserType1);
            return View(accounts.ToList());
        }

        // GET: /User/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["user-type"] == null || Session["user-type"].ToString() != "1")
                return Redirect("/");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: /User/Create
        public ActionResult Create()
        {
            if (Session["user-type"] == null || Session["user-type"].ToString() != "1")
                return Redirect("/");
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Id");
            ViewBag.ServiceProviderId = new SelectList(db.ServiceProviders, "Id", "Coverage");
            ViewBag.UserType = new SelectList(db.UserTypes, "Id", "Title");
            return View();
        }

        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Username,Password,Name,Email,Mobile,Gender,DateOfBirth,Address,ProfilePicture,UserType,CustomerId,ServiceProviderId")] Account account)
        {
            if (Session["user-type"] == null || Session["user-type"].ToString() != "1")
                return Redirect("/");
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Id", account.CustomerId);
            ViewBag.ServiceProviderId = new SelectList(db.ServiceProviders, "Id", "Coverage", account.ServiceProviderId);
            ViewBag.UserType = new SelectList(db.UserTypes, "Id", "Title", account.UserType);
            return View(account);
        }

        // GET: /User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["user-type"] == null || Session["user-type"].ToString() != "1")
                return Redirect("/");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Id", account.CustomerId);
            ViewBag.ServiceProviderId = new SelectList(db.ServiceProviders, "Id", "Coverage", account.ServiceProviderId);
            ViewBag.UserType = new SelectList(db.UserTypes, "Id", "Title", account.UserType);
            return View(account);
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Username,Password,Name,Email,Mobile,Gender,DateOfBirth,Address,ProfilePicture,UserType,CustomerId,ServiceProviderId")] Account account)
        {
            if (Session["user-type"] == null || Session["user-type"].ToString() != "1")
                return Redirect("/");
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Id", account.CustomerId);
            ViewBag.ServiceProviderId = new SelectList(db.ServiceProviders, "Id", "Coverage", account.ServiceProviderId);
            ViewBag.UserType = new SelectList(db.UserTypes, "Id", "Title", account.UserType);
            return View(account);
        }

        // GET: /User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["user-type"] == null || Session["user-type"].ToString() != "1")
                return Redirect("/");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["user-type"] == null || Session["user-type"].ToString() != "1")
                return Redirect("/");
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
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
