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
    public class HouseController : BaseController
    {
        //
        // GET: /House/
        const int pageSize = 5;
        public ActionResult Index(int pageIndex = 1)
        {
            var pageList = new PagedList<House>(db.House.ToList(), pageIndex, pageSize);
            return View(pageList);
        }

        //
        // GET: /House/Details/5

        public ActionResult Details(int id = 0)
        {
            House house = db.House.Find(id);
            if (house == null)
            {
                return HttpNotFound();
            }
            return View(house);
        }

        //
        // GET: /House/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /House/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(House house)
        {
            if (ModelState.IsValid)
            {
                db.House.Add(house);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(house);
        }

        //
        // GET: /House/Edit/5

        public ActionResult Edit(int id = 0)
        {
            House house = db.House.Find(id);
            if (house == null)
            {
                return HttpNotFound();
            }
            return View(house);
        }

        //
        // POST: /House/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(House house)
        {
            if (ModelState.IsValid)
            {
                db.Entry(house).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(house);
        }

        //
        // GET: /House/Delete/5

        public ActionResult Delete(int id = 0)
        {
            House house = db.House.Find(id);
            if (house == null)
            {
                return HttpNotFound();
            }
            return View(house);
        }

        //
        // POST: /House/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            House house = db.House.Find(id);
            db.House.Remove(house);
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