using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Demo.Param.CleanCode.Domain
    {

    /// <summary>
    /// Table Marque de voiture
    /// </summary>
    public class CarBrand
    {
        #region properties 
        // > ID généré par code <
        public string ID_CarBrand { get; set; }

        // > ID Généré par la base <
        public int ID { get; set; }

        // > Libellé marque voiture <
        public string Libelle { get; set; }
        #endregion

        // --- Une marque peut Avoir N modeles de voiture --- 
        // > Pour permettre la configuration du context <
        //  ( Voir "CGF_CarModels" )
        [NotMapped]
        [JsonIgnore]
        public List<CarModels>? Models { get; set; }

    }
}