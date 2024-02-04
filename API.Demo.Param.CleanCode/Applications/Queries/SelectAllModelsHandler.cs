using API.Demo.Param.CleanCode.Applications.DTOs;
using API.Demo.Param.CleanCode.Domain;
using API.Demo.Param.CleanCode.Framework;
using MediatR;

namespace API.Demo.Param.CleanCode.Applications.Queries
{
    public class SelectAllModelsHandler : IRequestHandler<SelectAllModelsQuery, List<DTOs.CarModelDTO>>
    {
        #region Fieleds 
        private readonly IParamRepository _repository = null;
        #endregion

        #region Constructor 
        public SelectAllModelsHandler(IParamRepository repository) 
        {
            _repository = repository;
        }
        #endregion

       
        
        public Task<List<CarModelDTO>> Handle(SelectAllModelsQuery request, CancellationToken cancellationToken)
        {
            // > Lance l'extraction de la liste <
            // ( Si un ID est transmis, on ne renvoie que l'enregistrement...
            //   ...correspondant ).

            // Remarque : la clause "Where" sur le paramètre...
            // ..."ID_CarBrand" se fait dans la méthode "GetAllCarModel" du...
            // ... _repository.
            var ModelsCarList = this._repository.GetAllCarModelCQRS(request.ID_CarBrandQry);

            // > On construit les colonnes de la liste d'objets que l'on renvoie <
            //    ( On renvoie une Liste de "CarBrandDTO()" ) 
            var elements = ModelsCarList.Select(item => new CarModelDTO()
            {
                ID_auto = item.ID,
                ID_CarModel = item.ID_CarModel,
                Libelle = item.Libelle,
                FK_ID_CarBrand = item.FK_ID_CarBrand,
            }).ToList();

            return Task.FromResult(elements);
        }
    }
}
