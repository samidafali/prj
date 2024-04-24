using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjetEpîcerie.Models;

namespace ProjetEpîcerie.Controllers
{
    public class ProduitsTraductionsController : Controller
    {
        private SamiDb db = new SamiDb();

        // GET: ProduitsTraductions
        public ActionResult Index()
        {
            var produitsTraductions = db.ProduitsTraductions.Include(p => p.Categorie).Include(p => p.Langue).Include(p => p.Produits);
            return View(produitsTraductions.ToList());
        }

        // GET: ProduitsTraductions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProduitsTraductions produitsTraductions = db.ProduitsTraductions.Find(id);
            if (produitsTraductions == null)
            {
                return HttpNotFound();
            }
            return View(produitsTraductions);
        }

        // GET: ProduitsTraductions/Create
        public ActionResult Create()
        {
            ViewBag.IdCategorie = new SelectList(db.Categories, "IdCategorie", "Name");
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description");
            ViewBag.IdProduits = new SelectList(db.Produits, "IdProduits", "Nom");
            return View();
        }

        // POST: ProduitsTraductions/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProduitsTraductions,IdProduits,IdCategorie,IdLangue,NomTraductions,DescriptionTraductions,PrixTraductions,urlImageTraductions")] ProduitsTraductions produitsTraductions)
        {
            if (ModelState.IsValid)
            {
                db.ProduitsTraductions.Add(produitsTraductions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCategorie = new SelectList(db.Categories, "IdCategorie", "Name", produitsTraductions.IdCategorie);
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", produitsTraductions.IdLangue);
            ViewBag.IdProduits = new SelectList(db.Produits, "IdProduits", "Nom", produitsTraductions.IdProduits);
            return View(produitsTraductions);
        }

        // GET: ProduitsTraductions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProduitsTraductions produitsTraductions = db.ProduitsTraductions.Find(id);
            if (produitsTraductions == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCategorie = new SelectList(db.Categories, "IdCategorie", "Name", produitsTraductions.IdCategorie);
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", produitsTraductions.IdLangue);
            ViewBag.IdProduits = new SelectList(db.Produits, "IdProduits", "Nom", produitsTraductions.IdProduits);
            return View(produitsTraductions);
        }

        // POST: ProduitsTraductions/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProduitsTraductions,IdProduits,IdCategorie,IdLangue,NomTraductions,DescriptionTraductions,PrixTraductions,urlImageTraductions")] ProduitsTraductions produitsTraductions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produitsTraductions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCategorie = new SelectList(db.Categories, "IdCategorie", "Name", produitsTraductions.IdCategorie);
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", produitsTraductions.IdLangue);
            ViewBag.IdProduits = new SelectList(db.Produits, "IdProduits", "Nom", produitsTraductions.IdProduits);
            return View(produitsTraductions);
        }

        // GET: ProduitsTraductions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProduitsTraductions produitsTraductions = db.ProduitsTraductions.Find(id);
            if (produitsTraductions == null)
            {
                return HttpNotFound();
            }
            return View(produitsTraductions);
        }

        // POST: ProduitsTraductions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProduitsTraductions produitsTraductions = db.ProduitsTraductions.Find(id);
            db.ProduitsTraductions.Remove(produitsTraductions);
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
