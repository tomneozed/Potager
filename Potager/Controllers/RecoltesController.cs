using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Potager.Models;

namespace Potager.Controllers
{
    public class RecoltesController : Controller
    {
        private PotagerDBEntities db = new PotagerDBEntities();

        // GET: Recoltes
        public ActionResult Index()
        {
            var recolte = db.Recolte.Include(r => r.Sujet);
            return View(recolte.ToList());
        }

        // GET: Recoltes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recolte recolte = db.Recolte.Find(id);
            if (recolte == null)
            {
                return HttpNotFound();
            }
            return View(recolte);
        }

        // GET: Recoltes/Create
        public ActionResult Create()
        {
            ViewBag.recolte_id = new SelectList(db.Sujet, "sujet_id", "maladie");
            return View();
        }

        // POST: Recoltes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "recolte_id,sujet_id,date,poids")] Recolte recolte)
        {
            if (ModelState.IsValid)
            {
                db.Recolte.Add(recolte);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.recolte_id = new SelectList(db.Sujet, "sujet_id", "maladie", recolte.recolte_id);
            return View(recolte);
        }

        // GET: Recoltes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recolte recolte = db.Recolte.Find(id);
            if (recolte == null)
            {
                return HttpNotFound();
            }
            ViewBag.recolte_id = new SelectList(db.Sujet, "sujet_id", "maladie", recolte.recolte_id);
            return View(recolte);
        }

        // POST: Recoltes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "recolte_id,sujet_id,date,poids")] Recolte recolte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recolte).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.recolte_id = new SelectList(db.Sujet, "sujet_id", "maladie", recolte.recolte_id);
            return View(recolte);
        }

        // GET: Recoltes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recolte recolte = db.Recolte.Find(id);
            if (recolte == null)
            {
                return HttpNotFound();
            }
            return View(recolte);
        }

        // POST: Recoltes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recolte recolte = db.Recolte.Find(id);
            db.Recolte.Remove(recolte);
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
