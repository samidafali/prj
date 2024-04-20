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
    public class CategoriesController : Controller
    {
        private LoloEpicerieDb db = new LoloEpicerieDb();

        // GET: Categories
        public ActionResult Index()
        {
            var categories = db.Categories.Include(c => c.Langue);
            return View(categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorie categorie = db.Categories.Find(id);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description");
            return View();
        }

        // POST: Categories/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCategorie,IdLangue,Name,Description")] Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(categorie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", categorie.IdLangue);
            return View(categorie);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorie categorie = db.Categories.Find(id);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", categorie.IdLangue);
            return View(categorie);
        }

        // POST: Categories/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCategorie,IdLangue,Name,Description")] Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categorie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", categorie.IdLangue);
            return View(categorie);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorie categorie = db.Categories.Find(id);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categorie categorie = db.Categories.Find(id);
            db.Categories.Remove(categorie);
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

        public ActionResult Acceuil(int? id)
        {
            string username = Session["username"].ToString();
            string password = Session["password"].ToString();
            var listLangue = db.Langues.ToList();
            ViewBag.Langue = listLangue;

            int idDefault = 1;

            if (id == null)
            {
                Session["Idlangue"] = idDefault;
                return View(db.Categories.Where(m => m.IdLangue == idDefault).ToList());
            }
            else if (id == 1)
            {
                return View(db.Categories.Where(m => m.IdLangue == idDefault).ToList());

            }
            else
            {
                Session["Idlangue"] = id;
                var listTraduction = db.CategoriesTraductions.Where(m => m.IdLangue == id).ToList();
                List<Categorie> categories = new List<Categorie>();
    

                foreach (var traduction in listTraduction)
                {
                    Categorie Cat = new Categorie(); 
                    Cat.IdLangue = traduction.IdLangue;
                    Cat.Name = traduction.NameTraduction;
                    Cat.Description = traduction.DescriptionTraduction;
                    categories.Add(Cat);
                }

                return View(categories);
            }
        }

    }
}
