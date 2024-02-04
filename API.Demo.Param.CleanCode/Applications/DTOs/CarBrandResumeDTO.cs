namespace API.Demo.Param.CleanCode.Applications.DTOs
{
    public class CarBrandResumeDTO
    {
        #region properties 
        // > ID généré par code <
        public string ID_CarBrand { get; set; }

        // > ID Généré par la base <
        public int ID_auto { get; set; }

        // > Libellé marque voiture <
        public string Libelle { get; set; }

        // > Nbre de modèle pour la marque <
        public int NbModels { get; set; }

        // > Message erreur pour l'appelant <
        public string ErrorMsge { get; set; }

        #endregion


    }
}
