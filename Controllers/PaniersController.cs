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
    public class PaniersController : Controller
    {
        private SamiDb db = new SamiDb();

        // GET: Paniers
        public ActionResult Index()
        {
            var paniers = db.Paniers.Include(p => p.Produits).Include(p => p.Utilisateur);
            return View(paniers.ToList());
        }

        // GET: Paniers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Panier panier = db.Paniers.Find(id);
            if (panier == null)
            {
                return HttpNotFound();
            }
            return View(panier);
        }

        // GET: Paniers/Create
        public ActionResult Create()
        {
            ViewBag.IdProduits = new SelectList(db.Produits, "IdProduits", "Nom");
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateur, "IdUtilisateur", "Name");
            return View();
        }

        // POST: Paniers/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPanier,Quantité,IdUtilisateur,IdProduits")] Panier panier)
        {
            if (ModelState.IsValid)
            {
                db.Paniers.Add(panier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProduits = new SelectList(db.Produits, "IdProduits", "Nom", panier.IdProduits);
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateur, "IdUtilisateur", "Name", panier.IdUtilisateur);
            return View(panier);
        }

        // GET: Paniers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Panier panier = db.Paniers.Find(id);
            if (panier == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProduits = new SelectList(db.Produits, "IdProduits", "Nom", panier.IdProduits);
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateur, "IdUtilisateur", "Name", panier.IdUtilisateur);
            return View(panier);
        }

        // POST: Paniers/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPanier,Quantité,IdUtilisateur,IdProduits")] Panier panier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(panier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProduits = new SelectList(db.Produits, "IdProduits", "Nom", panier.IdProduits);
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateur, "IdUtilisateur", "Name", panier.IdUtilisateur);
            return View(panier);
        }

        // GET: Paniers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Panier panier = db.Paniers.Find(id);
            if (panier == null)
            {
                return HttpNotFound();
            }
            return View(panier);
        }

        // POST: Paniers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Panier panier = db.Paniers.Find(id);
            db.Paniers.Remove(panier);
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
