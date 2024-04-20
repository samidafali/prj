using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetEpîcerie.Models
{
    public class CategoriesTraductions
    {

        //Relation avec Categorie
        public int IdCategorie { get; set; }
        public virtual Categorie Categorie { get; set; }

        [Key]
        public int IdCategoriesTraductions { get; set; }
        [Required]
        public string NameTraduction { get; set; }
        [Required]
        public string DescriptionTraduction { get; set; }

        //Relation avec langue
        public int IdLangue { get; set; }
        public virtual Langue Langue { get; set; }



    }
}