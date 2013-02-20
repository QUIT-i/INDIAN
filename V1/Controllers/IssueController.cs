using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Mapping;
using V1.STORAGE;
using V1.ViewModel;

namespace V1.Controllers
{
    public class IssueController : Controller
    {
        private STORAGE_Con db = new STORAGE_Con();
        private IssueViewModel is=new IssueViewModel();
        //
        // GET: /Issue/

        public ActionResult Index()
        { 
            var entity_issues = db.entity_Issues.Include(e => e.entity_Place_Pin);
            return View(entity_issues.ToList());
        }

        //
        // GET: /Issue/Details/5

        public ActionResult Details(string id = null)
        {
            entity_Issues entity_issues = db.entity_Issues.Find(id);
            if (entity_issues == null)
            {
                return HttpNotFound();
            }
            return View(entity_issues);
        }

        //
        // GET: /Issue/Create

        public ActionResult Create()
        {
            ViewBag.PIN = new SelectList(db.entity_Place_Pin, "PIN", "Place_Name");
            return View();
        }

        //
        // POST: /Issue/Create

        [HttpPost]
        public ActionResult Create(entity_Issues entity_issues)
        {
            if (ModelState.IsValid)
            {
                db.entity_Issues.Add(entity_issues);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PIN = new SelectList(db.entity_Place_Pin, "PIN", "Place_Name", entity_issues.PIN);
            return View(entity_issues);
        }

        //
        // GET: /Issue/Edit/5

        public ActionResult Edit(string id = null)
        {
            entity_Issues entity_issues = db.entity_Issues.Find(id);
            if (entity_issues == null)
            {
                return HttpNotFound();
            }
            ViewBag.PIN = new SelectList(db.entity_Place_Pin, "PIN", "Place_Name", entity_issues.PIN);
            return View(entity_issues);
        }

        //
        // POST: /Issue/Edit/5

        [HttpPost]
        public ActionResult Edit(entity_Issues entity_issues)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entity_issues).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PIN = new SelectList(db.entity_Place_Pin, "PIN", "Place_Name", entity_issues.PIN);
            return View(entity_issues);
        }

        //
        // GET: /Issue/Delete/5

        public ActionResult Delete(string id = null)
        {
            entity_Issues entity_issues = db.entity_Issues.Find(id);
            if (entity_issues == null)
            {
                return HttpNotFound();
            }
            return View(entity_issues);
        }

        //
        // POST: /Issue/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            entity_Issues entity_issues = db.entity_Issues.Find(id);
            db.entity_Issues.Remove(entity_issues);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}