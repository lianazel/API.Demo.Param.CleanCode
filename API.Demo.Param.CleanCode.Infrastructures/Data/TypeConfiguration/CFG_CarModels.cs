using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// > pour "CarBrand" <
using API.Demo.Param.CleanCode.Domain;

// > pour "IEntityTypeConfiguration" <
using Microsoft.EntityFrameworkCore;

// > Pour "EntityTypeBuilder" 
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace API.Demo.Param.CleanCode.Infrastructures.Data.TypeConfiguration
{
    /// <summary>
    /// Configuration Table "CarModels" 
    /// </summary>

    public class CFG_CarModels : IEntityTypeConfiguration<CarModels>
    {
        public void Configure(EntityTypeBuilder<CarModels> builder)
        {
            // > Ici, on indique ici que le nom de la table sera bien
            //  ... "CarModels"
            builder.ToTable("CarModels");

            // > On définit une clé à la table "CarModels" : <
            //  => La clé sera l'ID du modèle calculée par le code 
            builder.HasKey(item => item.ID);
            builder.HasKey(item => item.ID_CarModel);

            // > On définit la relation manuellement  :
            //   => Notre model de voiture  ne peut avaoir...
            //   ... qu'une seule marque de voiture (".HasOne...")

            //   => Et ensuite, cette  marque de voiture  peut...
            //    avoir 0 ou N modèles de voitures (".WithMany...)
            builder.HasOne(item => item.Brand)
                  .WithMany(item => item.Models);

            // > On définit une valeur par défaut pour le libellé <
            builder.Property(item => item.Libelle)
                    .HasDefaultValueSql("('Modèle à renseigner')");

        }
    }
}
