using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetEpîcerie.Models
{
    public class pageAfficherProduits
    {
        [Key]
        public int IdpageAfficherProduits { get; set; }
        public string BtnDescon { get; set; }
        public string BtnVoir { get; set; }
        public string BtnAjoutPanier { get; set; }

        //Relation avec langue
        public int IdLangue { get; set; }
        public virtual Langue Langue { get; set; }

    }
}