using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

// > Pour "IUnitOfWork" <
using API.Demo.Param.CleanCode.Framework;

// > Pour "DbContext" <
using Microsoft.EntityFrameworkCore;

// > Pour "NotNullAttribute" <
using System.Diagnostics.CodeAnalysis;

// > Pour les configurations de table ("CFG_xxxx") <
using API.Demo.Param.CleanCode.Infrastructures.Data.TypeConfiguration;

// > Pour les définitions de tables <
using API.Demo.Param.CleanCode.Domain;

namespace API.Demo.Param.CleanCode.Infrastructures
{
    public  class DataContext : DbContext, IUnitOfWork

    {
        // > Constructeur <
        //    ( Hérite de "DbContext" => F12 sur ":base()" ) 
        //       ( Execute le constructeur de "DbContext" )
        public DataContext() : base()
        {

        }

        // > Constructeur <
        public DataContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        // #####=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // ##### -- Installe les configurations pour les tables 
        // #####=-=-=-=-=-=-=-=-=-=-=-=-=-=
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // > On déclare la classe de configuration pour la table CarBrand (Marque de voiture) <
            modelBuilder.ApplyConfiguration(new CFG_CarBrand());

            // > On déclare la classe de configuration pour la table CarModels (Models de voiture) <
            modelBuilder.ApplyConfiguration(new CFG_CarModels());

            // > On déclare la classe de configuration pour la table Car (voiture) <
            modelBuilder.ApplyConfiguration(new CFG_Car());

        }

        // > Table CarBrand (Marque de voiture)  <
        public DbSet<CarBrand> Brand { get; set; }

        // > Table CarModels (Models de voiture)  <
        public DbSet<CarModels> Models { get; set; }

        // > Table Car (voiture)  <
        public DbSet<Car> Car { get; set; }
    }
}
