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
    public class pageAfficherProduitsController : Controller
    {
        private SamiDb db = new SamiDb();

        // GET: pageAfficherProduits
        public ActionResult Index()
        {
            var pageAfficherProduits = db.pageAfficherProduits.Include(p => p.Langue);
            return View(pageAfficherProduits.ToList());
        }

        // GET: pageAfficherProduits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pageAfficherProduits pageAfficherProduits = db.pageAfficherProduits.Find(id);
            if (pageAfficherProduits == null)
            {
                return HttpNotFound();
            }
            return View(pageAfficherProduits);
        }

        // GET: pageAfficherProduits/Create
        public ActionResult Create()
        {
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description");
            return View();
        }

        // POST: pageAfficherProduits/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdpageAfficherProduits,BtnDescon,BtnVoir,BtnAjoutPanier,IdLangue")] pageAfficherProduits pageAfficherProduits)
        {
            if (ModelState.IsValid)
            {
                db.pageAfficherProduits.Add(pageAfficherProduits);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", pageAfficherProduits.IdLangue);
            return View(pageAfficherProduits);
        }

        // GET: pageAfficherProduits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pageAfficherProduits pageAfficherProduits = db.pageAfficherProduits.Find(id);
            if (pageAfficherProduits == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", pageAfficherProduits.IdLangue);
            return View(pageAfficherProduits);
        }

        // POST: pageAfficherProduits/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdpageAfficherProduits,BtnDescon,BtnVoir,BtnAjoutPanier,IdLangue")] pageAfficherProduits pageAfficherProduits)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pageAfficherProduits).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", pageAfficherProduits.IdLangue);
            return View(pageAfficherProduits);
        }

        // GET: pageAfficherProduits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pageAfficherProduits pageAfficherProduits = db.pageAfficherProduits.Find(id);
            if (pageAfficherProduits == null)
            {
                return HttpNotFound();
            }
            return View(pageAfficherProduits);
        }

        // POST: pageAfficherProduits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pageAfficherProduits pageAfficherProduits = db.pageAfficherProduits.Find(id);
            db.pageAfficherProduits.Remove(pageAfficherProduits);
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
