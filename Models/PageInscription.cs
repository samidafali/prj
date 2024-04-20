using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetEpîcerie.Models
{
    public class PageInscription
    {
        [Key] 
        public int IdPageInscription { get; set; }
        public string TitreSite { get; set; }
        public string BtnDescon { get; set; }
        public string BtnInscrire { get; set; }
        public string BtnConnecter { get; set; }
        public string lblNomUtilisateur { get; set; }
        public string LabMotPasse { get; set; }
        public string lblNom { get; set; }
        public string lblTélephone { get; set; }
        public string lblAdresse { get; set; }
        public string lblEmail { get; set; }

        //Relation avec langue
        public int IdLangue { get; set; }
        public virtual Langue Langue { get; set; }
    }
}