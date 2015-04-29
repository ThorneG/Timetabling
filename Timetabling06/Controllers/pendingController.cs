using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Timetabling06.Models;

namespace Timetabling06.Controllers
{
    public class pendingController : Controller
    {
        private team06Entities db = new team06Entities();

        // GET: pending
        public ActionResult Index(string user)
        {
            if (user == null)
            {
                return RedirectToAction("Index", "Home", new { error = "Please Log In." });
            }
            else
            {
                var requests = db.requests.Include(r => r.department).Include(r => r.module).Include(r => r.round).Where(r => r.deptCode == user).Where(r => r.sent == 1).Where(r => r.status == 0);
                ViewBag.user = user;
                return View(requests.ToList());
            }
        }

        // GET: pending/Details/5
        public ActionResult Details(int? id, string user)
        {
            if (user == null)
            {
                return RedirectToAction("Index", "Home", new { error = "Please Log In." });
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                request request = db.requests.Find(id);
                if (request == null)
                {
                    return HttpNotFound();
                }
                ViewBag.user = user;
                return View(request);
            }
        }

        // GET: pending/Create
        public ActionResult Create(string user)
        {
            if (user == null)
            {
                return RedirectToAction("Index", "Home", new { error = "Please Log In." });
            }
            else
            {
                ViewBag.deptCode = new SelectList(db.departments, "code", "name");
                ViewBag.moduleCode = new SelectList(db.modules, "moduleCode", "moduleTitle");
                ViewBag.roundID = new SelectList(db.rounds, "roundID", "roundID");
                ViewBag.user = user;
                return View();
            }
        }

        // POST: pending/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "requestID,moduleCode,deptCode,priority,day,start,length,weeks,type,otherReqs,date,roundID,requestLink,sent,status,viewed,booked")] request request, string user)
        {
            if (user == null)
            {
                return RedirectToAction("Index", "Home", new { error = "Please Log In." });
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.requests.Add(request);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { user = user });
                }

                ViewBag.deptCode = new SelectList(db.departments, "code", "name", request.deptCode);
                ViewBag.moduleCode = new SelectList(db.modules, "moduleCode", "moduleTitle", request.moduleCode);
                ViewBag.roundID = new SelectList(db.rounds, "roundID", "roundID", request.roundID);
                ViewBag.user = user;
                return View(request);
            }
        }

        // GET: pending/Edit/5
        public ActionResult Edit(int? id, string user)
        {
            if (user == null)
            {
                return RedirectToAction("Index", "Home", new { error = "Please Log In." });
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                request request = db.requests.Find(id);
                if (request == null)
                {
                    return HttpNotFound();
                }
                ViewBag.deptCode = new SelectList(db.departments, "code", "name", request.deptCode);
                ViewBag.moduleCode = new SelectList(db.modules, "moduleCode", "moduleTitle", request.moduleCode);
                ViewBag.roundID = new SelectList(db.rounds, "roundID", "roundID", request.roundID);
                ViewBag.user = user;
                return View(request);
            }
        }

        // POST: pending/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "requestID,moduleCode,deptCode,priority,day,start,length,weeks,type,otherReqs,date,roundID,requestLink,sent,status,viewed,booked")] request request, string user)
        {
            if (user == null)
            {
                return RedirectToAction("Index", "Home", new { error = "Please Log In." });
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(request).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { user = user });
                }
                ViewBag.deptCode = new SelectList(db.departments, "code", "name", request.deptCode);
                ViewBag.moduleCode = new SelectList(db.modules, "moduleCode", "moduleTitle", request.moduleCode);
                ViewBag.roundID = new SelectList(db.rounds, "roundID", "roundID", request.roundID);
                ViewBag.user = user;
                return View(request);
            }
        }

        // GET: pending/Delete/5
        public ActionResult Delete(int? id, string user)
        {
            if (user == null)
            {
                return RedirectToAction("Index", "Home", new { error = "Please Log In." });
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                request request = db.requests.Find(id);
                if (request == null)
                {
                    return HttpNotFound();
                }
                ViewBag.user = user;
                return View(request);
            }
        }

        // POST: pending/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string user)
        {
            if (user == null)
            {
                return RedirectToAction("Index", "Home", new { error = "Please Log In." });
            }
            else
            {
                request request = db.requests.Find(id);
                db.requests.Remove(request);
                db.SaveChanges();
                return RedirectToAction("Index", new { user = user });
            }
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
