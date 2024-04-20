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
    public class ProduitsController : Controller
    {
        private LoloEpicerieDb db = new LoloEpicerieDb();

        // GET: Produits
        public ActionResult Index()
        {
            var produits = db.Produits.Include(p => p.Categorie).Include(p => p.Langue);
            return View(produits.ToList());
        }

        // GET: Produits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produits produits = db.Produits.Find(id);
            if (produits == null)
            {
                return HttpNotFound();
            }
            return View(produits);
        }

        // GET: Produits/Create
        public ActionResult Create()
        {
            ViewBag.IdCategorie = new SelectList(db.Categories, "IdCategorie", "Name");
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description");
            return View();
        }

        // POST: Produits/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProduits,IdLangue,IdCategorie,Nom,Description,Prix,urlImage")] Produits produits)
        {
            if (ModelState.IsValid)
            {
                db.Produits.Add(produits);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCategorie = new SelectList(db.Categories, "IdCategorie", "Name", produits.IdCategorie);
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", produits.IdLangue);
            return View(produits);
        }

        // GET: Produits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produits produits = db.Produits.Find(id);
            if (produits == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCategorie = new SelectList(db.Categories, "IdCategorie", "Name", produits.IdCategorie);
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", produits.IdLangue);
            return View(produits);
        }

        // POST: Produits/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProduits,IdLangue,IdCategorie,Nom,Description,Prix,urlImage")] Produits produits)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produits).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCategorie = new SelectList(db.Categories, "IdCategorie", "Name", produits.IdCategorie);
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", produits.IdLangue);
            return View(produits);
        }

        // GET: Produits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produits produits = db.Produits.Find(id);
            if (produits == null)
            {
                return HttpNotFound();
            }
            return View(produits);
        }

        // POST: Produits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produits produits = db.Produits.Find(id);
            db.Produits.Remove(produits);
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

        public ActionResult AfficherProduits(int? id, int? IdLangue)
        {
            string username = Session["username"].ToString();
            string password = Session["password"].ToString();

            var listLangue = db.Langues.ToList();
            ViewBag.Langue = listLangue;
            Session["IdCategorie"] = id;     
            if(IdLangue == 1)
            {
                return View((db.Produits.Where(m => m.IdCategorie == id && m.IdLangue == IdLangue).ToList()));
            }
            else
            {
                var listTraduction = db.ProduitsTraductions.Where(m => m.Categorie.IdCategorie == id && m.IdLangue == IdLangue).ToList();
                List<Produits> Produits = new List<Produits>();


                foreach (var traduction in listTraduction)
                {
                    Produits prod = new Produits();
                    prod.IdLangue = traduction.IdLangue;
                    prod.Nom = traduction.NomTraductions;
                    prod.Description = traduction.DescriptionTraductions;
                    prod.Prix = traduction.PrixTraductions;
                    prod.urlImage = traduction.urlImageTraductions;
                    prod.IdCategorie = traduction.IdCategorie;
                    Produits.Add(prod);

                }

                return View(Produits);
            }


        }


        public ActionResult VoirProduits(int ?id, int? IdLangue)
        {
            string username = Session["username"].ToString();
            string password = Session["password"].ToString();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(db.Produits.Where(p => p.IdProduits == id).ToList());


        }

        public ActionResult AjouterPanier(int id)
        {
            Produits produit = db.Produits.SingleOrDefault(m => m.IdProduits == id);
            string username = Session["username"].ToString();
            string password = Session["password"].ToString();

            Utilisateur utilisateur = db.Utilisateur.SingleOrDefault(m => m.Login.password == password & m.Login.username == username);

            Panier panier = new Panier { Produits = produit, Utilisateur = utilisateur };
            db.Paniers.Add(panier);
            db.SaveChanges();
            return RedirectToAction("Index", "Paniers");
        }






    }






    
}
