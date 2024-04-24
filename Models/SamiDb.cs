using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjetEpîcerie.Models
{
    public class SamiDb : DbContext
    {
        public DbSet<Langue> Langues { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Produits> Produits { get; set; }

        public DbSet<Panier> Paniers { get; set; }

        public DbSet<Login> Logins { get; set; }

        public DbSet<Utilisateur> Utilisateur { get; set; }

        public DbSet<Inscription> Inscription { get; set; }


        public DbSet<CategoriesTraductions> CategoriesTraductions { get; set; }

        public DbSet<ProduitsTraductions> ProduitsTraductions { get; set; }

        public DbSet<PageConnexion> PageConnexions { get; set; }

        public DbSet<PageInscription> PageInscriptions { get; set; }

        public DbSet<TraduitAcceuil> TraduitAcceuils { get; set; }

        public DbSet<pageAfficherProduits> pageAfficherProduits { get; set; }
        
        public DbSet<PagePanier> PagePaniers { get; set; }
        public DbSet<PageUpdateQuantite> PageUpdateQuantite { get; set; }
        



        public SamiDb()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ProjetEpîcerie.Models.SamiDb, ProjetEpîcerie.Migrations.Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriesTraductions>()
                .HasRequired(ct => ct.Langue)
                .WithMany(l => l.CategoriesTraductions)
                .HasForeignKey(ct => ct.IdLangue)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Produits>()
                .HasRequired(p => p.Categorie)
                .WithMany(c => c.Produit)
                .HasForeignKey(p => p.IdCategorie)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProduitsTraductions>()
                .HasRequired(pt => pt.Langue)
                .WithMany()
                .HasForeignKey(pt => pt.IdLangue)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProduitsTraductions>()
                .HasRequired(pt => pt.Produits)
                .WithMany()
                .HasForeignKey(pt => pt.IdProduits)
                .WillCascadeOnDelete(false);
        }




    }
}