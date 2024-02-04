
// > Déclare "MediatR" <
using API.Demo.Param.CleanCode.Applications.DTOs;
using MediatR;

namespace API.Demo.Param.CleanCode.Applications.Queries
{
    /// <summary>
    /// Renvoie d'une liste de modèles de voiture pour un ID MARQUE ( ID_CarBrand => ID Marque de voiture )
    /// </summary>
    
    // > "IRequest" utilisé avec le "Using MediatR" <
    // > "IRequest<List<CarModelDTO>>" : on demande à ce que l'on nous renvoie une liste de "CarModelDTO" <
    public class SelectAllModelsQuery : IRequest<List<CarModelDTO>>
    {
        // > On déclare L'ID qui sera utilisé pour lister les modèles d'une Marque  <
        //   ( ID_CarBrand ==> ID d'une marque ( clé étrangère )
        #region Properties 
        public string ID_CarBrandQry {  get; set; }
        #endregion
    }
}
