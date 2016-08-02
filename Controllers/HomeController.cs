using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CheckListManager.Models;

namespace CheckListManager.Controllers
{
    public class HomeController : Controller
    {
        private CheckListManagerContext db = new CheckListManagerContext();

        // GET: Home
        public ActionResult Index()
        {
            return View(db.CheckLists.ToList());
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckList checkList = db.CheckLists.Find(id);
            if (checkList == null)
            {
                return HttpNotFound();
            }
            return View(checkList);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CheckListId,Name,UserId")] CheckList checkList)
        {
            if (ModelState.IsValid)
            {
                db.CheckLists.Add(checkList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(checkList);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckList checkList = db.CheckLists.Find(id);
            if (checkList == null)
            {
                return HttpNotFound();
            }
            return View(checkList);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CheckListId,Name,UserId")] CheckList checkList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checkList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(checkList);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckList checkList = db.CheckLists.Find(id);
            if (checkList == null)
            {
                return HttpNotFound();
            }
            return View(checkList);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CheckList checkList = db.CheckLists.Find(id);
            db.CheckLists.Remove(checkList);
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
