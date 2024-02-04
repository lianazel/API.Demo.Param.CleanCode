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
    ///  Configuration Table "CarBrand" 
    /// </summary>

    // Remarque : Ces informations seront utilisées si l'on ...
    //... met en place un projet "Migrations" avec 
    //... dotnet ef core ( cli => Comand Line Interface ).
    public class CFG_CarBrand : IEntityTypeConfiguration<CarBrand>
    {
        public void Configure(EntityTypeBuilder<CarBrand> builder)
        {
            // > Ici, on indique ici que le nom de la table sera bien
            //  ... "CarBrand"
            builder.ToTable("CarBrand");

            // > On définit une clé à la table "CarBrand" : <
            //  => La clé sera l'ID du modèle calculée par le code 
            builder.HasKey(item => item.ID);
            builder.HasKey(item => item.ID_CarBrand);

            // > On définit une valeur par défaut pour le libellé <
            builder.Property(item => item.Libelle)
                    .HasDefaultValueSql("('Marque à renseigner')");
        }
    }
}
