using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetEpîcerie.Models
{
    public class Login
    {
        [Key, ForeignKey("Utilisateur")]
        public int IdLogin { get; set; }


        [Required(ErrorMessage = "Renseigner ce champ !")]
        public string username { get; set; }

        [Required(ErrorMessage = "Renseigner ce champ !")]
        public string password { get; set; }

        // Relation avec Utilisateur
        public virtual Utilisateur Utilisateur { get; set; }
    }
}