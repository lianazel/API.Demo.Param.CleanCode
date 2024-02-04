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
    ///  Configuration Table "Car" 
    /// </summary>
    public class CFG_Car : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            // > Ici, on indique ici que le nom de la table sera bien
            //  ... "Car"
            builder.ToTable("Car");

            // > On définit une clé à la table "Car" : <
            //  => La clé sera l'ID du modèle calculée par le code 
            builder.HasKey(item => item.ID_Car);
                        
        }
    }

}
