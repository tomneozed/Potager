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
    public class TerrainsController : Controller
    {
        private PotagerDBEntities db = new PotagerDBEntities();

        // GET: Terrains
        public ActionResult Index()
        {
            var terrain = db.Terrain.Include(t => t.Meteo);
            return View(terrain.ToList());
        }

        // GET: Terrains/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terrain terrain = db.Terrain.Find(id);
            if (terrain == null)
            {
                return HttpNotFound();
            }
            return View(terrain);
        }

        // GET: Terrains/Create
        public ActionResult Create()
        {
            ViewBag.terrain_id = new SelectList(db.Meteo, "meteo_id", "meteo_id");
            return View();
        }

        // POST: Terrains/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "terrain_id,ville,type_sol,structures")] Terrain terrain)
        {
            if (ModelState.IsValid)
            {
                db.Terrain.Add(terrain);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.terrain_id = new SelectList(db.Meteo, "meteo_id", "meteo_id", terrain.terrain_id);
            return View(terrain);
        }

        // GET: Terrains/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terrain terrain = db.Terrain.Find(id);
            if (terrain == null)
            {
                return HttpNotFound();
            }
            ViewBag.terrain_id = new SelectList(db.Meteo, "meteo_id", "meteo_id", terrain.terrain_id);
            return View(terrain);
        }

        // POST: Terrains/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "terrain_id,ville,type_sol,structures")] Terrain terrain)
        {
            if (ModelState.IsValid)
            {
                db.Entry(terrain).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.terrain_id = new SelectList(db.Meteo, "meteo_id", "meteo_id", terrain.terrain_id);
            return View(terrain);
        }

        // GET: Terrains/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terrain terrain = db.Terrain.Find(id);
            if (terrain == null)
            {
                return HttpNotFound();
            }
            return View(terrain);
        }

        // POST: Terrains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Terrain terrain = db.Terrain.Find(id);
            db.Terrain.Remove(terrain);
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
