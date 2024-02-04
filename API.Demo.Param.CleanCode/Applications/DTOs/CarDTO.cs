namespace API.Demo.Param.CleanCode.Applications.DTOs
{
    public class CarDTO
    {
        #region properties 
        // > ID généré par code <
        public string ID_Car { get; set; }

        // > ID Généré par la base <
        public int ID_auto { get; set; }

        // > Clé étrangère : Marque de la voiture  <
        public string FK_ID_CarBrand { get; set; }

        // > Clé étrangère : Model de la voiture  <
        public string FK_ID_CarModel { get; set; }

        // > Message erreur pour l'appelant <
        public string ErrorMsge { get; set; }
        #endregion

    }
}
