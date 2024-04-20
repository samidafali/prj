using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetEpîcerie.Models
{
    public class Panier
    {
        [Key]
        public  int IdPanier { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La quantité doit être d'au moins 1.")]
        public int Quantité { get; set; }

        //Relation avec Utilisateur
        public int IdUtilisateur { get; set; }
        public virtual Utilisateur Utilisateur { get; set; }

        //Relation avec Produits
        public int IdProduits { get; set; }
        public virtual Produits Produits  { get; set; }


    }
}