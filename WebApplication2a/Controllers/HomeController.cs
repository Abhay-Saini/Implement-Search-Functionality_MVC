using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2a.Models;

namespace WebApplication2a.Controllers
{
    public class HomeController : Controller
    {
        private ProductDbEntities db = new ProductDbEntities();

        // GET: Home
        public ActionResult Index()
        {
            return View(db.tbl_Product.ToList());
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Product tbl_Product = db.tbl_Product.Find(id);
            if (tbl_Product == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Product);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Product_Id,Product_Name,Size,Price,MfgDate,Category")] tbl_Product tbl_Product)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Product.Add(tbl_Product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Product);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Product tbl_Product = db.tbl_Product.Find(id);
            if (tbl_Product == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Product);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Product_Id,Product_Name,Size,Price,MfgDate,Category")] tbl_Product tbl_Product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Product);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Product tbl_Product = db.tbl_Product.Find(id);
            if (tbl_Product == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Product);
        }
        //To Create Search Fields

        public ActionResult Search(string searchBy , string search)
        {

            if (searchBy == "productName")
            {
                var data = db.tbl_Product.Where(model => model.Product_Name.StartsWith(search)).ToList();
                return View(data);
            }
            else if (searchBy == "size")
            {
                var data = db.tbl_Product.Where(model => model.Size == search).ToList();
                return View(data);
            }
            else if(searchBy == "category")
            {
                var data = db.tbl_Product.Where(model => model.Category == search).ToList();
                return View(data);
            }
           

            else
            {
                var data = db.tbl_Product.ToList();
                return View(data);
            }
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Product tbl_Product = db.tbl_Product.Find(id);
            db.tbl_Product.Remove(tbl_Product);
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
