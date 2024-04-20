using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetEpîcerie.Models
{
    public class PageConnexion
    {
        [Key]
        public int IdPageConnexion { get; set; }
        public string TitreSite { get; set; }
        public string BtnDescon { get; set; }
        public string BtnInscrire { get; set; }
        public string BtnConnecter { get; set; }
        public string LableNom { get; set; }
        public string LabMotPasse { get; set; }


        //Relation avec langue
        public int IdLangue { get; set; }
        public virtual Langue Langue { get; set; }

    }
}