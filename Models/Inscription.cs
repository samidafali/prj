using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetEpîcerie.Models
{
    public class Inscription
    {
        [Key]
        public int IdInscription { get; set; }

        [Required(ErrorMessage ="Renseigner ce champ !")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Renseigner ce champ !")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Renseigner ce champ !")]
        public string Adresse { get; set; }

        [Required(ErrorMessage = "Renseigner ce champ !")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Renseigner ce champ !")]
        public string username { get; set; }

        [Required(ErrorMessage = "Renseigner ce champ !")]
        public string password { get; set; }

    }
}