using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetEpîcerie.Models
{
    public class Produits
    {
        //Relation avec langue
        public int IdLangue { get; set; }
        public virtual Langue Langue { get; set; }


        //Relation avec categorie
        public int IdCategorie { get; set; }
        public virtual Categorie Categorie { get; set; }

        [Key]
        public int IdProduits{ get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public float Prix { get; set; }

        public string urlImage { get; set; }

        //Relation avec ProduitsTraductions
        public virtual List<ProduitsTraductions> ProduitsTraductions { get; set; }



    }
}