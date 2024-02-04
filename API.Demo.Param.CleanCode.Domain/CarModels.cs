using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Demo.Param.CleanCode.Domain
{
    public class CarModels
    {
        #region properties 
        // > ID généré par code <
        public string ID_CarModel { get; set; }

        // > ID Généré par la base <
        public int ID { get; set; }

        // > Libellé modèle de la voiture  <
        public string Libelle { get; set; }

        // > Clé étrangère : Marque de la voiture  <
        public string FK_ID_CarBrand { get; set; }

        // ---- Une modèle de voirure ne PEUT qu'une marque ---
        // > pour la relation dans "CFG_CarModels" ) <
        [JsonIgnore]
        public CarBrand Brand { get; set; }

        #endregion

    }
}
