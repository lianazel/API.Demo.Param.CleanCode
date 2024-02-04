namespace API.Demo.Param.CleanCode.Applications.DTOs
{
    /// <summary>
    /// Dto Marque voiture 
    /// </summary>
    public class CarBrandDTO
    {
        #region properties 
        // > ID généré par code <
        public string ID_CarBrand { get; set; }

        // > ID Généré par la base <
        public int ID_auto { get; set; }

        // > Libellé marque voiture <
        public string Libelle { get; set; }
               
        // > Message erreur pour l'appelant <
        public string ErrorMsge { get; set; }

        #endregion

    }
}
