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
    public class PagePaniersController : Controller
    {
        private SamiDb db = new SamiDb();

        // GET: PagePaniers
        public ActionResult Index()
        {
            var pagePaniers = db.PagePaniers.Include(p => p.Langue);
            return View(pagePaniers.ToList());
        }

        // GET: PagePaniers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagePanier pagePanier = db.PagePaniers.Find(id);
            if (pagePanier == null)
            {
                return HttpNotFound();
            }
            return View(pagePanier);
        }

        // GET: PagePaniers/Create
        public ActionResult Create()
        {
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description");
            return View();
        }

        // POST: PagePaniers/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPagePanier,Totapayer,NomProduit,Quantité,PrixUnitaire,Action,btnDeconncter,btnModifier,btnSupprimer,IdLangue")] PagePanier pagePanier)
        {
            if (ModelState.IsValid)
            {
                db.PagePaniers.Add(pagePanier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", pagePanier.IdLangue);
            return View(pagePanier);
        }

        // GET: PagePaniers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagePanier pagePanier = db.PagePaniers.Find(id);
            if (pagePanier == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", pagePanier.IdLangue);
            return View(pagePanier);
        }

        // POST: PagePaniers/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPagePanier,Totapayer,NomProduit,Quantité,PrixUnitaire,Action,btnDeconncter,btnModifier,btnSupprimer,IdLangue")] PagePanier pagePanier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pagePanier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", pagePanier.IdLangue);
            return View(pagePanier);
        }

        // GET: PagePaniers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagePanier pagePanier = db.PagePaniers.Find(id);
            if (pagePanier == null)
            {
                return HttpNotFound();
            }
            return View(pagePanier);
        }

        // POST: PagePaniers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PagePanier pagePanier = db.PagePaniers.Find(id);
            db.PagePaniers.Remove(pagePanier);
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
