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
    public class PageInscriptionsController : Controller
    {
        private SamiDb db = new SamiDb();

        // GET: PageInscriptions
        public ActionResult Index()
        {
            var pageInscriptions = db.PageInscriptions.Include(p => p.Langue);
            return View(pageInscriptions.ToList());
        }

        // GET: PageInscriptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PageInscription pageInscription = db.PageInscriptions.Find(id);
            if (pageInscription == null)
            {
                return HttpNotFound();
            }
            return View(pageInscription);
        }

        // GET: PageInscriptions/Create
        public ActionResult Create()
        {
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description");
            return View();
        }

        // POST: PageInscriptions/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPageInscription,TitreSite,BtnDescon,BtnInscrire,BtnConnecter,lblNomUtilisateur,LabMotPasse,lblNom,lblTélephone,lblAdresse,lblEmail,IdLangue")] PageInscription pageInscription)
        {
            if (ModelState.IsValid)
            {
                db.PageInscriptions.Add(pageInscription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", pageInscription.IdLangue);
            return View(pageInscription);
        }

        // GET: PageInscriptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PageInscription pageInscription = db.PageInscriptions.Find(id);
            if (pageInscription == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", pageInscription.IdLangue);
            return View(pageInscription);
        }

        // POST: PageInscriptions/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPageInscription,TitreSite,BtnDescon,BtnInscrire,BtnConnecter,lblNomUtilisateur,LabMotPasse,lblNom,lblTélephone,lblAdresse,lblEmail,IdLangue")] PageInscription pageInscription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pageInscription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", pageInscription.IdLangue);
            return View(pageInscription);
        }

        // GET: PageInscriptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PageInscription pageInscription = db.PageInscriptions.Find(id);
            if (pageInscription == null)
            {
                return HttpNotFound();
            }
            return View(pageInscription);
        }

        // POST: PageInscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PageInscription pageInscription = db.PageInscriptions.Find(id);
            db.PageInscriptions.Remove(pageInscription);
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
