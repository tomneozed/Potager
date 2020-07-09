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
    public class FaunesController : Controller
    {
        private PotagerDBEntities db = new PotagerDBEntities();

        // GET: Faunes
        public ActionResult Index()
        {
            return View(db.Faune.ToList());
        }

        // GET: Faunes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faune faune = db.Faune.Find(id);
            if (faune == null)
            {
                return HttpNotFound();
            }
            return View(faune);
        }

        // GET: Faunes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Faunes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "faune_id,embranchement,classe,ordre,sous_ordre,famille,sous_famille,tribu,genre,nom_commun")] Faune faune)
        {
            if (ModelState.IsValid)
            {
                db.Faune.Add(faune);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(faune);
        }

        // GET: Faunes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faune faune = db.Faune.Find(id);
            if (faune == null)
            {
                return HttpNotFound();
            }
            return View(faune);
        }

        // POST: Faunes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "faune_id,embranchement,classe,ordre,sous_ordre,famille,sous_famille,tribu,genre,nom_commun")] Faune faune)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faune).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(faune);
        }

        // GET: Faunes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faune faune = db.Faune.Find(id);
            if (faune == null)
            {
                return HttpNotFound();
            }
            return View(faune);
        }

        // POST: Faunes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Faune faune = db.Faune.Find(id);
            db.Faune.Remove(faune);
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
