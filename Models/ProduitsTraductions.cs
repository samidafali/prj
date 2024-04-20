using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetEpîcerie.Models
{
    public class ProduitsTraductions
    {


        //Relation avec Produits
        public int IdProduits { get; set; }
        public virtual Produits Produits { get; set; }


        //Relation avec categorie
        public int IdCategorie { get; set; }
        public virtual Categorie Categorie { get; set; }

        //Relation avec langue
        public int IdLangue { get; set; }
        public virtual Langue Langue { get; set; }

        [Key]
        public int IdProduitsTraductions { get; set; }
        [Required]
        public string NomTraductions  { get; set; }
        [Required]
        public string DescriptionTraductions { get; set; }
        [Required]
        public float PrixTraductions { get; set; }

        public string urlImageTraductions { get; set; }


    }
}