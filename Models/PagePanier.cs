using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetEpîcerie.Models
{
    public class PagePanier
    {
        [Key]
        public int IdPagePanier { get; set; }
        public string Totapayer { get; set; }

        public string NomProduit { get; set; }
        public string Quantité { get; set; }
        public string PrixUnitaire { get; set; }
        public string Action { get; set; }

        public string btnDeconncter { get; set; }
        public string btnModifier{ get; set; }
        public string btnSupprimer { get; set; }

        //Relation avec langue
        public int IdLangue { get; set; }
        public virtual Langue Langue { get; set; }


    }
}