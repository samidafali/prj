using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetEpîcerie.Models
{
    public class Langue
    {
        [Key]
        public int IdLangue { get; set; }
        [Required]  
        public string Description { get; set; }
        [Required]  
        public string Symbole { get; set;}

        //Relation avec Categorie
        public virtual List<Categorie> Categorie { get; set; }

        //Relation avec Categorie
        public virtual List<CategoriesTraductions> CategoriesTraductions { get; set; }

        //Relation avec Produits
        public virtual List<Produits> Produit { get; set; }

        //Relation avec ProduitsTraductions
        public virtual List<ProduitsTraductions> ProduitsTraductions { get; set; }

        //Relation avec PageConnexion
        public virtual List<PageConnexion> PageConnexions { get; set; }

        // Relation avec PageInscription
        public virtual List<PageInscription> PageInscription { get; set; }

        // Relation avec TraduitAcceuil
        public virtual List<TraduitAcceuil> TraduitAcceuil { get; set; }

        // Relation avec PagePanier
        public virtual List<PagePanier> PagePanier { get; set; }

        // Relation avec PageUpdateQuantite
        public virtual List<PageUpdateQuantite> PageUpdateQuantite { get; set; }




    }
}