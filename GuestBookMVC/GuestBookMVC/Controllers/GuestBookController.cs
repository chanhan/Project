using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuestBookMVC.Models;
using System.IO;

namespace GuestBookMVC.Controllers
{
    public class GuestBookController : Controller
    {
        private GuestBookMVCContext db = new GuestBookMVCContext();

        //
        // GET: /GuestBook/

        public ActionResult Index()
        {
            return View(db.GuestBooks.ToList());
        }

        //
        // GET: /GuestBook/Details/5

        public ActionResult Details(int id = 0)
        {
            GuestBook guestbook = db.GuestBooks.Find(id);
            if (guestbook == null)
            {
                return HttpNotFound();
            }
            return View(guestbook);
        }

        //
        // GET: /GuestBook/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /GuestBook/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GuestBook guestbook)
        {
            if (ModelState.IsValid)
            {
                db.GuestBooks.Add(guestbook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(guestbook);
        }

        //
        // GET: /GuestBook/Edit/5

        public ActionResult Edit(int id = 0)
        {
            GuestBook guestbook = db.GuestBooks.Find(id);
            if (guestbook == null)
            {
                return HttpNotFound();
            }
            return View(guestbook);
        }

        //
        // POST: /GuestBook/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GuestBook guestbook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guestbook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(guestbook);
        }

        //
        // GET: /GuestBook/Delete/5

        public ActionResult Delete(int id = 0)
        {
            GuestBook guestbook = db.GuestBooks.Find(id);
            if (guestbook == null)
            {
                return HttpNotFound();
            }
            return View(guestbook);
        }

        //
        // POST: /GuestBook/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GuestBook guestbook = db.GuestBooks.Find(id);
            db.GuestBooks.Remove(guestbook);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        protected override void HandleUnknownAction(string actionName)
        {
            Response.Write("<script>alert('"+actionName+"不存在')</script>");
            Response.Redirect("Http://www.baidu.com");
        }

        public ActionResult GetFile() {
            return File(Server.MapPath("~/App_Data/image1.png"),"image/png");
        }
        public ActionResult DownFile() {
            FileStream file = new FileStream(Server.MapPath("~/App_Data/ASP.NET MVC4开发指南.pdf"), FileMode.Open);
          //  StreamReader reader = new StreamReader(file);
            string fileDownName;
            if (Request.Browser.Browser=="IE"&&Convert.ToInt32(Request.Browser.MajorVersion)<9)
            {
                fileDownName = "ASP.NET MVC4开发指南.pdf";
            }
            else
            {
                fileDownName = Server.UrlPathEncode("ASP.NET MVC4开发指南.pdf");
            }
            return File(file, "application/pdf", fileDownName); 
            
        }
        public ActionResult JavaScript2() {
          return JavaScript("alert('ok')");
          //  return Content("alert(1)","text/javascript");
        }

        public ActionResult JSON()
        {
            return Json(new { id = 1, name = "Will", CreateOn = DateTime.Now }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Redirect() {
            return RedirectToRoute(new {controller="User",action="Edit",id=1});
        }

        public ActionResult CheckPermission2() {

            if (1 == 2)
            {
                return View();
            }
            else
            {
                return new HttpUnauthorizedResult();
            }
        }
    }
}