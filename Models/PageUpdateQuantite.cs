using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetEpîcerie.Models
{
    public class PageUpdateQuantite
    {
        [Key]
        public int IdPageUpdateQuantite { get; set; }

        [Range(1, 100, ErrorMessage ="La quantité doit être suppérieur à 1")]
        public int BtnQuantité { get; set; }
        public string BtnModifie { get; set; }

        //Relation avec langue
        public int IdLangue { get; set; }
        public virtual Langue Langue { get; set; }
    }
}