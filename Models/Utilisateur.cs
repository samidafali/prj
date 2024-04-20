using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetEpîcerie.Models
{
    public class Utilisateur
    {
        [Key]
        public int IdUtilisateur { get; set; }
        [Required]
        public string  Name { get; set; }
        [Required]
        public string Telephone  { get; set; }
        [Required]
        public string Adresse  { get; set; }
        [Required]
        public string Email { get; set; }

        //relation avec login 
        public virtual Login Login { get; set; }

        //Relation avec   panier 
        public virtual List<Panier> Panier { get; set; }


    }
}