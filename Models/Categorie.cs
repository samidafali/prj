using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetEpîcerie.Models
{
    public class Categorie
    {
        //Relation avec langue
        public int IdLangue { get; set; }
        public virtual Langue Langue { get; set; }

        [Key]
        public int IdCategorie { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }


        //Relation avec Produits
        public virtual List<Produits> Produit { get; set; }

        //Relation avec CategoriesTraductions
        public virtual List<CategoriesTraductions> CategoriesTraductions { get; set; }

        //Relation avec ProduitsTraductions
        public virtual List<ProduitsTraductions> ProduitsTraductions { get; set; }
        


    }
}