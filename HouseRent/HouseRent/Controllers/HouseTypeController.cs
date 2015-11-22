using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseRent.Models;

namespace HouseRent.Controllers
{
    public class HouseTypeController : Controller
    {
        private HouseRentContext db = new HouseRentContext();

        //
        // GET: /HouseType/

        public ActionResult Index()
        {
            return View(db.HouseTypes.ToList());
        }

        //
        // GET: /HouseType/Details/5

        public ActionResult Details(int id = 0)
        {
            HouseType housetype = db.HouseTypes.Find(id);
            if (housetype == null)
            {
                return HttpNotFound();
            }
            return View(housetype);
        }

        //
        // GET: /HouseType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /HouseType/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HouseType housetype)
        {
            if (ModelState.IsValid)
            {
                db.HouseTypes.Add(housetype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(housetype);
        }

        //
        // GET: /HouseType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            HouseType housetype = db.HouseTypes.Find(id);
            if (housetype == null)
            {
                return HttpNotFound();
            }
            return View(housetype);
        }

        //
        // POST: /HouseType/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HouseType housetype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(housetype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(housetype);
        }

        //
        // GET: /HouseType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            HouseType housetype = db.HouseTypes.Find(id);
            if (housetype == null)
            {
                return HttpNotFound();
            }
            return View(housetype);
        }

        //
        // POST: /HouseType/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HouseType housetype = db.HouseTypes.Find(id);
            db.HouseTypes.Remove(housetype);
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