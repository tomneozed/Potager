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
    public class GrainesController : Controller
    {
        private PotagerDBEntities db = new PotagerDBEntities();

        // GET: Graines
        public ActionResult Index()
        {
            var graine = db.Graine.Include(g => g.Plante).Include(g => g.Sujet);
            return View(graine.ToList());
        }

        // GET: Graines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Graine graine = db.Graine.Find(id);
            if (graine == null)
            {
                return HttpNotFound();
            }
            return View(graine);
        }

        // GET: Graines/Create
        public ActionResult Create()
        {
            ViewBag.graine_id = new SelectList(db.Plante, "plante_id", "nom_commun");
            ViewBag.graine_id = new SelectList(db.Sujet, "sujet_id", "maladie");
            return View();
        }

        // POST: Graines/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "graine_id,plante_id,parent_id,nombre,poids")] Graine graine)
        {
            if (ModelState.IsValid)
            {
                db.Graine.Add(graine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.graine_id = new SelectList(db.Plante, "plante_id", "nom_commun", graine.graine_id);
            ViewBag.graine_id = new SelectList(db.Sujet, "sujet_id", "maladie", graine.graine_id);
            return View(graine);
        }

        // GET: Graines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Graine graine = db.Graine.Find(id);
            if (graine == null)
            {
                return HttpNotFound();
            }
            ViewBag.graine_id = new SelectList(db.Plante, "plante_id", "nom_commun", graine.graine_id);
            ViewBag.graine_id = new SelectList(db.Sujet, "sujet_id", "maladie", graine.graine_id);
            return View(graine);
        }

        // POST: Graines/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "graine_id,plante_id,parent_id,nombre,poids")] Graine graine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(graine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.graine_id = new SelectList(db.Plante, "plante_id", "nom_commun", graine.graine_id);
            ViewBag.graine_id = new SelectList(db.Sujet, "sujet_id", "maladie", graine.graine_id);
            return View(graine);
        }

        // GET: Graines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Graine graine = db.Graine.Find(id);
            if (graine == null)
            {
                return HttpNotFound();
            }
            return View(graine);
        }

        // POST: Graines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Graine graine = db.Graine.Find(id);
            db.Graine.Remove(graine);
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
