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
    public class CategoriesTraductionsController : Controller
    {
        private SamiDb db = new SamiDb();

        // GET: CategoriesTraductions
        public ActionResult Index()
        {
            var categoriesTraductions = db.CategoriesTraductions.Include(c => c.Categorie).Include(c => c.Langue);
            return View(categoriesTraductions.ToList());
        }

        // GET: CategoriesTraductions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriesTraductions categoriesTraductions = db.CategoriesTraductions.Find(id);
            if (categoriesTraductions == null)
            {
                return HttpNotFound();
            }
            return View(categoriesTraductions);
        }

        // GET: CategoriesTraductions/Create
        public ActionResult Create()
        {
            ViewBag.IdCategorie = new SelectList(db.Categories, "IdCategorie", "Name");
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description");
            return View();
        }

        // POST: CategoriesTraductions/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCategoriesTraductions,IdCategorie,NameTraduction,DescriptionTraduction,IdLangue")] CategoriesTraductions categoriesTraductions)
        {
            if (ModelState.IsValid)
            {
                db.CategoriesTraductions.Add(categoriesTraductions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCategorie = new SelectList(db.Categories, "IdCategorie", "Name", categoriesTraductions.IdCategorie);
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", categoriesTraductions.IdLangue);
            return View(categoriesTraductions);
        }

        // GET: CategoriesTraductions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriesTraductions categoriesTraductions = db.CategoriesTraductions.Find(id);
            if (categoriesTraductions == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCategorie = new SelectList(db.Categories, "IdCategorie", "Name", categoriesTraductions.IdCategorie);
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", categoriesTraductions.IdLangue);
            return View(categoriesTraductions);
        }

        // POST: CategoriesTraductions/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCategoriesTraductions,IdCategorie,NameTraduction,DescriptionTraduction,IdLangue")] CategoriesTraductions categoriesTraductions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoriesTraductions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCategorie = new SelectList(db.Categories, "IdCategorie", "Name", categoriesTraductions.IdCategorie);
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", categoriesTraductions.IdLangue);
            return View(categoriesTraductions);
        }

        // GET: CategoriesTraductions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriesTraductions categoriesTraductions = db.CategoriesTraductions.Find(id);
            if (categoriesTraductions == null)
            {
                return HttpNotFound();
            }
            return View(categoriesTraductions);
        }

        // POST: CategoriesTraductions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoriesTraductions categoriesTraductions = db.CategoriesTraductions.Find(id);
            db.CategoriesTraductions.Remove(categoriesTraductions);
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
