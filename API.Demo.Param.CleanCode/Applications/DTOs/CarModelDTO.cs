namespace API.Demo.Param.CleanCode.Applications.DTOs
{
    /// <summary>
    /// Modèle pour une marque 
    /// </summary>
    public class CarModelDTO
    {

        #region properties 
        // > ID généré par code <
        public string ID_CarModel { get; set; }

        // > ID Généré par la base <
        public int ID_auto { get; set; }

        // > Libellé modèle de la voiture  <
        public string Libelle { get; set; }

        // > Clé étrangère : Marque de la voiture  <
        public string FK_ID_CarBrand { get; set; }

        // > Message erreur pour l'appelant <
        public string ErrorMsge { get; set; }

        #endregion

    }
}
