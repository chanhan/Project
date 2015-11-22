using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuestBookMVC.Models;

namespace GuestBookMVC.Controllers
{
    public class UserController : Controller
    {
        private GuestBookMVCContext db = new GuestBookMVCContext();

        //
        // GET: /User/

        public ActionResult Index()
        {
            User user = new User();
            user.CreateTime = DateTime.Now;
            user.Email = "123456@example.com";
            user.ID = 1;
            user.Password = "123456";
            user.Telephone = "1234565789";
            user.UserName = "Will";
            List<User> list = new List<User>();
            list.Add(user);
            ViewData.Model = list;
            return View();
            //    return View(db.User.ToList());
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(int id = 0)
        {
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id = 0)
        {
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(int id = 0)
        {
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.User.Find(id);
            db.User.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        public ActionResult TestForm(string UserName)
        {
            ViewData.Model = UserName;
            return View();
        }
        public ActionResult TestForm2(FormCollection form)
        {
            ViewData.Model = form["UserName"];
            return View();
        }
        public ActionResult TestForm3(UserForm user)
        {
          //  ViewData.Model = user.UserName;
            return View();
        }
    }
}