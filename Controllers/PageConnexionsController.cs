﻿using System;
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
    public class PageConnexionsController : Controller
    {
        private LoloEpicerieDb db = new LoloEpicerieDb();

        // GET: PageConnexions
        public ActionResult Index()
        {
            var pageConnexions = db.PageConnexions.Include(p => p.Langue);
            return View(pageConnexions.ToList());
        }

        // GET: PageConnexions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PageConnexion pageConnexion = db.PageConnexions.Find(id);
            if (pageConnexion == null)
            {
                return HttpNotFound();
            }
            return View(pageConnexion);
        }

        // GET: PageConnexions/Create
        public ActionResult Create()
        {
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description");
            return View();
        }

        // POST: PageConnexions/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPageConnexion,TitreSite,BtnDescon,BtnInscrire,BtnConnecter,LableNom,LabMotPasse,IdLangue")] PageConnexion pageConnexion)
        {
            if (ModelState.IsValid)
            {
                db.PageConnexions.Add(pageConnexion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", pageConnexion.IdLangue);
            return View(pageConnexion);
        }

        // GET: PageConnexions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PageConnexion pageConnexion = db.PageConnexions.Find(id);
            if (pageConnexion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", pageConnexion.IdLangue);
            return View(pageConnexion);
        }

        // POST: PageConnexions/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPageConnexion,TitreSite,BtnDescon,BtnInscrire,BtnConnecter,LableNom,LabMotPasse,IdLangue")] PageConnexion pageConnexion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pageConnexion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", pageConnexion.IdLangue);
            return View(pageConnexion);
        }

        // GET: PageConnexions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PageConnexion pageConnexion = db.PageConnexions.Find(id);
            if (pageConnexion == null)
            {
                return HttpNotFound();
            }
            return View(pageConnexion);
        }

        // POST: PageConnexions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PageConnexion pageConnexion = db.PageConnexions.Find(id);
            db.PageConnexions.Remove(pageConnexion);
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
