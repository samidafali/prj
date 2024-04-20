using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetEpîcerie.Models
{
    public class TraduitAcceuil
    {
        [Key]
        public int IdTraduitAcceuil { get; set; }
        public string btnDeconnecter { get; set; }
        public string txt1 { get; set; }
        public string txt2 { get; set; }

        //Relation avec langue
        public int IdLangue { get; set; }
        public virtual Langue Langue { get; set; }

    }
}