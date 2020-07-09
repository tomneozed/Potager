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
    public class SujetsController : Controller
    {
        private PotagerDBEntities db = new PotagerDBEntities();

        // GET: Sujets
        public ActionResult Index()
        {
            var sujet = db.Sujet.Include(s => s.Graine).Include(s => s.Plante).Include(s => s.Recolte).Include(s => s.Sujet_Entretien).Include(s => s.Zone);
            return View(sujet.ToList());
        }

        // GET: Sujets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sujet sujet = db.Sujet.Find(id);
            if (sujet == null)
            {
                return HttpNotFound();
            }
            return View(sujet);
        }

        // GET: Sujets/Create
        public ActionResult Create()
        {
            ViewBag.sujet_id = new SelectList(db.Graine, "graine_id", "graine_id");
            ViewBag.sujet_id = new SelectList(db.Plante, "plante_id", "nom_commun");
            ViewBag.sujet_id = new SelectList(db.Recolte, "recolte_id", "recolte_id");
            ViewBag.sujet_id = new SelectList(db.Sujet_Entretien, "entretien_id", "action");
            ViewBag.sujet_id = new SelectList(db.Zone, "zone_id", "nom");
            return View();
        }

        // POST: Sujets/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sujet_id,plante_id,zone_id,date_semis,date_plantation,date_debut_floraison,date_mort,poids_recolte_total,maladie,observations")] Sujet sujet)
        {
            if (ModelState.IsValid)
            {
                db.Sujet.Add(sujet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.sujet_id = new SelectList(db.Graine, "graine_id", "graine_id", sujet.sujet_id);
            ViewBag.sujet_id = new SelectList(db.Plante, "plante_id", "nom_commun", sujet.sujet_id);
            ViewBag.sujet_id = new SelectList(db.Recolte, "recolte_id", "recolte_id", sujet.sujet_id);
            ViewBag.sujet_id = new SelectList(db.Sujet_Entretien, "entretien_id", "action", sujet.sujet_id);
            ViewBag.sujet_id = new SelectList(db.Zone, "zone_id", "nom", sujet.sujet_id);
            return View(sujet);
        }

        // GET: Sujets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sujet sujet = db.Sujet.Find(id);
            if (sujet == null)
            {
                return HttpNotFound();
            }
            ViewBag.sujet_id = new SelectList(db.Graine, "graine_id", "graine_id", sujet.sujet_id);
            ViewBag.sujet_id = new SelectList(db.Plante, "plante_id", "nom_commun", sujet.sujet_id);
            ViewBag.sujet_id = new SelectList(db.Recolte, "recolte_id", "recolte_id", sujet.sujet_id);
            ViewBag.sujet_id = new SelectList(db.Sujet_Entretien, "entretien_id", "action", sujet.sujet_id);
            ViewBag.sujet_id = new SelectList(db.Zone, "zone_id", "nom", sujet.sujet_id);
            return View(sujet);
        }

        // POST: Sujets/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sujet_id,plante_id,zone_id,date_semis,date_plantation,date_debut_floraison,date_mort,poids_recolte_total,maladie,observations")] Sujet sujet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sujet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sujet_id = new SelectList(db.Graine, "graine_id", "graine_id", sujet.sujet_id);
            ViewBag.sujet_id = new SelectList(db.Plante, "plante_id", "nom_commun", sujet.sujet_id);
            ViewBag.sujet_id = new SelectList(db.Recolte, "recolte_id", "recolte_id", sujet.sujet_id);
            ViewBag.sujet_id = new SelectList(db.Sujet_Entretien, "entretien_id", "action", sujet.sujet_id);
            ViewBag.sujet_id = new SelectList(db.Zone, "zone_id", "nom", sujet.sujet_id);
            return View(sujet);
        }

        // GET: Sujets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sujet sujet = db.Sujet.Find(id);
            if (sujet == null)
            {
                return HttpNotFound();
            }
            return View(sujet);
        }

        // POST: Sujets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sujet sujet = db.Sujet.Find(id);
            db.Sujet.Remove(sujet);
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
