using ProjetEpîcerie.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjetEpîcerie.Controllers
{
    public class DefaultController : Controller
    {
        private LoloEpicerieDb Db = new LoloEpicerieDb();   
        // GET: Default

        public ActionResult Index()
        {
            Db.Logins.Count();

            return View();
        }

        private bool EstValide(string partie, string pattern)
        {
            return Regex.IsMatch(partie, pattern);
        }

        private bool ValiderMail(Inscription InscriptionInfo)
        {
            if (InscriptionInfo.Email.Contains("@") && InscriptionInfo.Email.Contains("."))
            {
                string[] parties = InscriptionInfo.Email.Split('@', '.');

                if (parties.Length == 3)
                {
                    string partie1 = parties[0];
                    string partie2 = parties[1];
                    string partie3 = parties[2];

                    if (EstValide(partie1, @"^[a-zA-Z0-9._-]+$") &&
                        EstValide(partie2, @"^[a-zA-Z_-]+$") &&
                        EstValide(partie3, "^[a-zA-Z]+$"))
                    {
                        return true;
                    }
                }

            }

            return false;
        }

        private bool SendMail(Inscription InscriptionInfo)
        {

            string suject = "Bienvenue à l'Épicerie TEC-EPIC";
            string Message = $"Voici vos informations de connexion:\nNom : {InscriptionInfo.username}\nMot de passe : {InscriptionInfo.password}\nCordialement.";

            string sender = System.Configuration.ConfigurationManager.AppSettings["mailSender"];
            string pw = System.Configuration.ConfigurationManager.AppSettings["mailPw"];

            try
            {
                SmtpClient smtpclient = new SmtpClient("smtp.office365.com", 587);
                smtpclient.Timeout = 3000;
                smtpclient.EnableSsl = true;
                smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpclient.UseDefaultCredentials = false;
                smtpclient.Credentials = new NetworkCredential(sender, pw);
                MailMessage mailMessage = new System.Net.Mail.MailMessage(sender, InscriptionInfo.Email, suject, Message);
                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                smtpclient.Send(mailMessage);

                return true;


            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }




        }


        //Inscription 
        public ActionResult Inscription(int? IdLangue)
        {
            var listLangue = Db.Langues.ToList();
            ViewBag.Langue = listLangue;

            //Gestion de la traduction de la page
            if (IdLangue != null)
            {
                var PageInscription = Db.PageInscriptions.Where(x => x.IdLangue == IdLangue).ToList();
                ViewBag.PageInscriptions = PageInscription;
            }
            else
            {
                int IdLangueDefault = 1;
                var PageInscription = Db.PageInscriptions.Where(x => x.IdLangue == IdLangueDefault).ToList();
                ViewBag.PageInscriptions = PageInscription;
            }


            return View(new Inscription());
        }
        [HttpPost]
        public ActionResult Inscription(int ? IdLangue, Inscription InfoInscription)
        {
            var listLangue = Db.Langues.ToList();
            ViewBag.Langue = listLangue;


            //Gestion de la traduction de la page
            if (IdLangue != null)
            {
                var PageInscription = Db.PageInscriptions.Where(x => x.IdLangue == IdLangue).ToList();
                ViewBag.PageInscriptions = PageInscription;
            }
            else
            {
                int IdLangueDefault = 1;
                var PageInscription = Db.PageInscriptions.Where(x => x.IdLangue == IdLangueDefault).ToList();
                ViewBag.PageInscriptions = PageInscription;
            }



            if (ModelState.IsValid)
            {
                if (ValiderMail(InfoInscription )){

                    string password = InfoInscription.password;
                    // Crypter le mot de passe
                    string hashedPassword = Crypto.HashPassword(password);

                    Login login = new Login { IdLogin = InfoInscription.IdInscription, username = InfoInscription.username, password = hashedPassword };
                    Utilisateur Utilisateur = new Utilisateur
                    {
                        IdUtilisateur = InfoInscription.IdInscription,
                        Name = InfoInscription.Name,
                        Telephone = InfoInscription.Telephone,
                        Adresse = InfoInscription.Adresse,
                        Email = InfoInscription.Email
                    };
                    int nb = (Db.Inscription.Where(x =>  x.Email == InfoInscription.Email
                    )).Count();

                    int nb1 = (Db.Inscription.Where(x => x.username == InfoInscription.username 
                    )).Count();

                    if (nb == 0 || nb1==0 )
                    {
                        Db.Inscription.Add(InfoInscription);
                        Db.Logins.Add(login);
                        Db.Utilisateur.Add(Utilisateur);
                        Db.SaveChanges();
                        if (SendMail(InfoInscription))
                        {
                            ViewBag.connexioOk = $"Votre compte est crée avec succès, veuillez consulter \n " +
                                $"votre courriel, puis connectez-vous. Merci !";
                            return View("Inscription");
                        }
                        else
                        {
                            ViewBag.Message = $"{InfoInscription.Email} invalide. Utiliser un autre Email!";
                            return View();
                        }



                    }
                    else if(nb > 0)
                    {
                        ViewBag.Message = $"{InfoInscription.Email} existe déja.Utiliser un autre autre!";
                        return View();
                    }
                    else
                    {
                        ViewBag.Message = $"{InfoInscription.username} existe déja. Utiliser un autre!";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = $"{InfoInscription.Email} invalide. Utiliser un autre Email!";
                    return View();
                }



            }
            else
            {
                return View(InfoInscription);
            }



        }



        public ActionResult connexion(int ? IdLangue)
        {

            if (IdLangue != null)
            {
                var Pageconnexion = Db.PageConnexions.Where(x => x.IdLangue == IdLangue).ToList();
                ViewBag.Pageconnexion = Pageconnexion;
            }
            else
            {
               int  IdLangueDefault = 1;
               var Pageconnexion = Db.PageConnexions.Where(x => x.IdLangue == IdLangueDefault).ToList();
               ViewBag.Pageconnexion = Pageconnexion;
            }


            var listLangue = Db.Langues.ToList();
            ViewBag.Langue = listLangue;
            return View(new Login());
        }
        [HttpPost]
        public ActionResult connexion( int ? IdLangue,  Login Infologin)
        {
            var listLangue = Db.Langues.ToList();
            ViewBag.Langue = listLangue;


            if (IdLangue != null)
            {
                var Pageconnexion = Db.PageConnexions.Where(x => x.IdLangue == IdLangue).ToList();
                ViewBag.Pageconnexion = Pageconnexion;
            }
            else
            {
                int IdLangueDefault = 1;
                var Pageconnexion = Db.PageConnexions.Where(x => x.IdLangue == IdLangueDefault).ToList();
                ViewBag.Pageconnexion = Pageconnexion;
            }
            if (Infologin.username == "Admin" & Infologin.password == "Admin")
            {
                return RedirectToAction("AcceuilAdministrateur", "Administrateur");

            }


            if (ModelState.IsValid)
            {
                Login login = Db.Logins.SingleOrDefault(m => m.username == Infologin.username.ToString() );
                string hashedPassword = login.password;
                string passwordToCheck = Infologin.password;

                bool passwordMatch = Crypto.VerifyHashedPassword(hashedPassword, passwordToCheck);

                if (passwordMatch)
                {
                    Session["username"] = Infologin.username;
                    Session["password"] = Infologin.password;
                    return RedirectToAction("Acceuil", "Default");
                } 
                else
                {
                    ViewBag.Message = "Email invalide !  Veuillez résayer !";
                    return View("connexion");
                }

            }else
            {
                return View("connexion");
            }

        }


        public ActionResult Deconnexion()
        {
            Session.Clear();
            return RedirectToAction("connexion", "Default");
        }



        public ActionResult Acceuil(int? IdLangue)
        {
            string username = Session["username"].ToString();
            string password = Session["password"].ToString();
            var listLangue = Db.Langues.ToList();
            ViewBag.Langue = listLangue;

            //gestion de la déconnexion 
            if (Session["password"] == null)
            {
                FormsAuthentication.SignOut(); 
                Session.Abandon(); 
                return RedirectToAction("connexion", "Default"); 
            }



            //Count panier 
            Utilisateur utilisateur = Db.Utilisateur.SingleOrDefault(m =>  m.Login.username == username);
            int nbPanier = Db.Paniers.Where(m => m.IdUtilisateur == utilisateur.IdUtilisateur).Count();
            ViewBag.nbPanier = nbPanier;


            if (IdLangue != null)
            {
                var TraduitAcceuils = Db.TraduitAcceuils.Where(x => x.IdLangue == IdLangue).ToList();
                ViewBag.TraduitAcceuils = TraduitAcceuils;
            }
            else
            {
                int IdLangueDefault = 1;
                var TraduitAcceuils = Db.TraduitAcceuils.Where(x => x.IdLangue == IdLangueDefault).ToList();
                ViewBag.TraduitAcceuils = TraduitAcceuils;
            }


            //Gestion des catégories 

            int idDefault = 1;

            if (IdLangue == null)
            {
                Session["IdLangue"] = idDefault;    
                return View(Db.CategoriesTraductions.Where(m => m.IdLangue == idDefault).ToList());
            }
            else if (IdLangue == 1)
            {
                Session["IdLangue"] = IdLangue;
                return View(Db.CategoriesTraductions.Where(m => m.IdLangue == idDefault).ToList());

            }
            else
            {
                Session["IdLangue"] = IdLangue;
                return View(Db.CategoriesTraductions.Where(m => m.IdLangue == IdLangue).ToList());
            }
        }



        public ActionResult AfficherProduits(int? id, int? IdLangue)
        {
            string username = Session["username"].ToString();
            string password = Session["password"].ToString();

            //gestion de la déconnexion 
            if (Session["password"] == null)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                return RedirectToAction("connexion", "Default");
            }

            //Count panier 
            Utilisateur utilisateur = Db.Utilisateur.SingleOrDefault(m => m.Login.username == username);
            int nbPanier = Db.Paniers.Where(m => m.IdUtilisateur == utilisateur.IdUtilisateur).Count();
            ViewBag.nbPanier = nbPanier;


            //gestion de la traduction
            if (IdLangue != null)
            {
                var pageAfficherProduits = Db.pageAfficherProduits.Where(x => x.IdLangue == IdLangue).ToList();
                ViewBag.pageAfficherProduits = pageAfficherProduits;
            }
            else
            {
                int IdLangueDefault = 1;
                var pageAfficherProduits = Db.pageAfficherProduits.Where(x => x.IdLangue == IdLangueDefault).ToList();
                ViewBag.pageAfficherProduits = pageAfficherProduits;
            }


            //Gestion liste des langues
            var listLangue = Db.Langues.ToList();
            ViewBag.Langue = listLangue;
            Session["IdCategorie"] = id;

            //gestion des catégorie
            var categories = Db.CategoriesTraductions.Where(m => m.IdLangue == IdLangue).ToList();
            ViewBag.Categories = categories;

            return View((Db.ProduitsTraductions.Where(m => m.IdCategorie == id && m.IdLangue == IdLangue).ToList()));


        }


        public ActionResult VoirProduits(int? IdProduits, int? IdLangue)
        {
            string username = Session["username"].ToString();
            string password = Session["password"].ToString();

            //Count panier 
            Utilisateur utilisateur = Db.Utilisateur.SingleOrDefault(m =>  m.Login.username == username);
            int nbPanier = Db.Paniers.Where(m => m.IdUtilisateur == utilisateur.IdUtilisateur).Count();
            ViewBag.nbPanier = nbPanier;

            //gestion de la déconnexion 
            if (Session["password"] == null)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                return RedirectToAction("connexion", "Default");
            }



            //gestion de la traduction
            if (IdLangue != null)
            {
                var pageAfficherProduits = Db.pageAfficherProduits.Where(x => x.IdLangue == IdLangue).ToList();
                ViewBag.pageAfficherProduits = pageAfficherProduits;
            }
            else
            {
                int IdLangueDefault = 1;
                var pageAfficherProduits = Db.pageAfficherProduits.Where(x => x.IdLangue == IdLangueDefault).ToList();
                ViewBag.pageAfficherProduits = pageAfficherProduits;
            }


            //Gestion liste des langues
            var listLangue = Db.Langues.ToList();
            ViewBag.Langue = listLangue;

            //gestion des catégorie
            var categories = Db.CategoriesTraductions.Where(m => m.IdLangue == IdLangue).ToList();
            ViewBag.Categories = categories;


            //gestion du produits
            if (IdProduits == null)
            {
                int idP = Convert.ToInt32(Session["IdProduits"]);
                return View(Db.ProduitsTraductions.Where(m => m.Produits.IdProduits == idP && m.IdLangue == IdLangue).ToList());
            }
            else
            {

                return View(Db.ProduitsTraductions.Where(m => m.Produits.IdProduits == IdProduits && m.IdLangue == IdLangue).ToList());
            }



        }




        public ActionResult AjouterPanier(int id, int? IdLangue)
        {
            Produits produit = Db.Produits.SingleOrDefault(m => m.IdProduits == id);
            string username = Session["username"].ToString();
            string password = Session["password"].ToString();


            //gestion de la déconnexion 
            if (Session["password"] == null)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                return RedirectToAction("connexion", "Default");
            }


            Utilisateur utilisateur = Db.Utilisateur.SingleOrDefault(m => m.Login.username == username);

            Panier panier = new Panier { Quantité=1, Produits = produit, Utilisateur = utilisateur };
            Db.Paniers.Add(panier);
            Db.SaveChanges();
            Session["IdLangue"] = IdLangue;

            return RedirectToAction("Panier");
        }

        public ActionResult Panier(int ? IdLangue)
        {
            string username = Session["username"].ToString();
            string password = Session["password"].ToString();

            //recuperation de  Session["IdLangue"] 

            int IdLangu = Convert.ToInt32(Session["IdLangue"]);

            //gestion de la traduction
            if (IdLangue != null)
            {
                var PagePanier = Db.PagePaniers.Where(x => x.IdLangue == IdLangue).ToList();
                ViewBag.PagePanier = PagePanier;

            }else if(Session["IdLangue"] != null)
            {
                var PagePanier = Db.PagePaniers.Where(x => x.IdLangue == IdLangu).ToList();
                ViewBag.PagePanier = PagePanier;
            }
            else
            {
                int IdLangueDefault = 1;
                var PagePanier = Db.PagePaniers.Where(x => x.IdLangue == IdLangueDefault).ToList();
                ViewBag.PagePanier = PagePanier;
            }



            //gestion de la déconnexion 
            if (Session["password"] == null)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                return RedirectToAction("connexion", "Default");
            }



            //Count panier 
            Utilisateur utilisateur = Db.Utilisateur.SingleOrDefault(m =>m.Login.username == username);
            int nbPanier = Db.Paniers.Where(m => m.IdUtilisateur == utilisateur.IdUtilisateur).Count();
            ViewBag.nbPanier = nbPanier;



            //Gestion liste des langues
            var listLangue = Db.Langues.ToList();
            ViewBag.Langue = listLangue;

            //gestion des catégorie
            if(IdLangue != null)
            {
                var categories = Db.CategoriesTraductions.Where(m => m.IdLangue == IdLangue).ToList();
                ViewBag.Categories = categories;
            }
            else
            {
                var categories = Db.CategoriesTraductions.Where(m => m.IdLangue == IdLangu).ToList();
                ViewBag.Categories = categories;
            }

            var idUtilisateur = Convert.ToInt32(utilisateur.IdUtilisateur); 

            var paniers = Db.Paniers.Where(p => p.Utilisateur.IdUtilisateur == idUtilisateur);

            // Calcule du prix total
            if(paniers.Any())
            {
                var prixTotal = paniers.Select(panier => panier.Produits.Prix * panier.Quantité).Sum();
                ViewBag.prixTotal = prixTotal;
                Session["prixTotal"] = prixTotal;
            }
            else
            {
                Session["prixTotal"] = 0;
            }



            return View(paniers.ToList());

        }


        // GET: Paniers/Edit/5
        public ActionResult Edit(int? id, int? IdLangue)
        {
            string username = Session["username"].ToString();
            string password = Session["password"].ToString();


            //recuperation de  Session["IdLangue"] 
            int IdLangu = Convert.ToInt32(Session["IdLangue"]);
            //gestion des catégorie
            if (IdLangue != null)
            {
                var categories = Db.CategoriesTraductions.Where(m => m.IdLangue == IdLangue).ToList();
                ViewBag.Categories = categories;
            }
            else
            {
                var categories = Db.CategoriesTraductions.Where(m => m.IdLangue == IdLangu).ToList();
                ViewBag.Categories = categories;
            }


            //Gestion liste des langues
            var listLangue = Db.Langues.ToList();
            ViewBag.Langue = listLangue;


            //gestion de la déconnexion 
            if (Session["password"] == null)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                return RedirectToAction("connexion", "Default");
            }

            //Count panier 
            Utilisateur utilisateur = Db.Utilisateur.SingleOrDefault(m =>  m.Login.username == username);
            int nbPanier = Db.Paniers.Where(m => m.IdUtilisateur == utilisateur.IdUtilisateur).Count();
            ViewBag.nbPanier = nbPanier;


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Panier panier = Db.Paniers.Find(id);
            if (panier == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProduits = new SelectList(Db.Produits, "IdProduits", "Nom", panier.IdProduits);
            ViewBag.IdUtilisateur = new SelectList(Db.Utilisateur, "IdUtilisateur", "Name", panier.IdUtilisateur);
            return View(panier);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Panier panier)
        {
            string username = Session["username"].ToString();
            string password = Session["password"].ToString();


            //Gestion liste des langues
            var listLangue = Db.Langues.ToList();
            ViewBag.Langue = listLangue;

            //Count panier 
            Utilisateur utilisateur = Db.Utilisateur.SingleOrDefault(m => m.Login.username == username);
            int nbPanier = Db.Paniers.Where(m => m.IdUtilisateur == utilisateur.IdUtilisateur).Count();
            ViewBag.nbPanier = nbPanier;

            Panier panierExist = Db.Paniers.Find(panier.IdPanier);

            if (panierExist != null)
            {
                panierExist.Quantité = panier.Quantité;
                Db.SaveChanges();

                return RedirectToAction("Panier");
            }
            else
            {
                return HttpNotFound();
            }

        }


        public ActionResult Delete(int id)
        {
            Panier panier = Db.Paniers.Find(id);
            Db.Paniers.Remove(panier);
            Db.SaveChanges();
            return RedirectToAction("Panier");
        }





    }
}