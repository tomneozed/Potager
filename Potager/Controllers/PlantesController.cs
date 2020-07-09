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
    public class PlantesController : Controller
    {
        private PotagerDBEntities db = new PotagerDBEntities();

        // GET: Plantes
        public ActionResult Index()
        {
            var plante = db.Plante.Include(p => p.Graine).Include(p => p.Sujet);
            return View(plante.ToList());
        }

        // GET: Plantes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plante plante = db.Plante.Find(id);
            if (plante == null)
            {
                return HttpNotFound();
            }
            return View(plante);
        }

        // GET: Plantes/Create
        public ActionResult Create()
        {
            ViewBag.plante_id = new SelectList(db.Graine, "graine_id", "graine_id");
            ViewBag.plante_id = new SelectList(db.Sujet, "sujet_id", "maladie");
            return View();
        }

        // POST: Plantes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "plante_id,nom_commun,regne,sous_regne,division,classe,sous_classe,ordre,famille,genre,espece,variete")] Plante plante)
        {
            if (ModelState.IsValid)
            {
                db.Plante.Add(plante);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.plante_id = new SelectList(db.Graine, "graine_id", "graine_id", plante.plante_id);
            ViewBag.plante_id = new SelectList(db.Sujet, "sujet_id", "maladie", plante.plante_id);
            return View(plante);
        }

        // GET: Plantes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plante plante = db.Plante.Find(id);
            if (plante == null)
            {
                return HttpNotFound();
            }
            ViewBag.plante_id = new SelectList(db.Graine, "graine_id", "graine_id", plante.plante_id);
            ViewBag.plante_id = new SelectList(db.Sujet, "sujet_id", "maladie", plante.plante_id);
            return View(plante);
        }

        // POST: Plantes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "plante_id,nom_commun,regne,sous_regne,division,classe,sous_classe,ordre,famille,genre,espece,variete")] Plante plante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.plante_id = new SelectList(db.Graine, "graine_id", "graine_id", plante.plante_id);
            ViewBag.plante_id = new SelectList(db.Sujet, "sujet_id", "maladie", plante.plante_id);
            return View(plante);
        }

        // GET: Plantes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plante plante = db.Plante.Find(id);
            if (plante == null)
            {
                return HttpNotFound();
            }
            return View(plante);
        }

        // POST: Plantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Plante plante = db.Plante.Find(id);
            db.Plante.Remove(plante);
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
