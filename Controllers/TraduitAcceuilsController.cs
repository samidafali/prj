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
    public class TraduitAcceuilsController : Controller
    {
        private SamiDb db = new SamiDb();

        // GET: TraduitAcceuils
        public ActionResult Index()
        {
            var traduitAcceuils = db.TraduitAcceuils.Include(t => t.Langue);
            return View(traduitAcceuils.ToList());
        }

        // GET: TraduitAcceuils/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraduitAcceuil traduitAcceuil = db.TraduitAcceuils.Find(id);
            if (traduitAcceuil == null)
            {
                return HttpNotFound();
            }
            return View(traduitAcceuil);
        }

        // GET: TraduitAcceuils/Create
        public ActionResult Create()
        {
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description");
            return View();
        }

        // POST: TraduitAcceuils/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTraduitAcceuil,btnDeconnecter,txt1,txt2,IdLangue")] TraduitAcceuil traduitAcceuil)
        {
            if (ModelState.IsValid)
            {
                db.TraduitAcceuils.Add(traduitAcceuil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", traduitAcceuil.IdLangue);
            return View(traduitAcceuil);
        }

        // GET: TraduitAcceuils/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraduitAcceuil traduitAcceuil = db.TraduitAcceuils.Find(id);
            if (traduitAcceuil == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", traduitAcceuil.IdLangue);
            return View(traduitAcceuil);
        }

        // POST: TraduitAcceuils/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTraduitAcceuil,btnDeconnecter,txt1,txt2,IdLangue")] TraduitAcceuil traduitAcceuil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(traduitAcceuil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdLangue = new SelectList(db.Langues, "IdLangue", "Description", traduitAcceuil.IdLangue);
            return View(traduitAcceuil);
        }

        // GET: TraduitAcceuils/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraduitAcceuil traduitAcceuil = db.TraduitAcceuils.Find(id);
            if (traduitAcceuil == null)
            {
                return HttpNotFound();
            }
            return View(traduitAcceuil);
        }

        // POST: TraduitAcceuils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TraduitAcceuil traduitAcceuil = db.TraduitAcceuils.Find(id);
            db.TraduitAcceuils.Remove(traduitAcceuil);
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
