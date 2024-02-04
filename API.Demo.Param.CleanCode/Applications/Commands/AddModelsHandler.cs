using API.Demo.Param.CleanCode.Applications.DTOs;
using API.Demo.Param.CleanCode.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Demo.Param.CleanCode.Applications.Commands
{
    //   > On reçoit  : 
    //    => "AddModelCommand",
    //   > On retourne 
    //    => "CarModelDTO".
    //      Rappel ==> On reçoit un "AddModelCommand" et on, retourne un "CarModelDTO" 
    public class AddModelsHandler: IRequestHandler<AddModelCommand,CarModelDTO>
    {
        #region Fieleds 
        private readonly IParamRepository _repository = null;
        #endregion

        #region Constructor 
        public AddModelsHandler(IParamRepository repository)
        {
            _repository = repository;
        }

        // > Le "AddModelCommand" contient juste un modèle "CarModelDTO" <
        public Task<CarModelDTO> Handle(AddModelCommand request, CancellationToken cancellationToken)
        {
                  
            CarModelDTO modelDto = null;

            // > (1B) ==> Création ID
            Guid IDGuid = Guid.NewGuid();

            // > On appelle la méthode "AddOneCarModel" < du repository <
            //   ( On passe à la méthode "AddOneCarModel"...
            //     ...un paramétre de type "CarModel" ).
            //   ( La méthode "AddOneCarModel" renvoie un objet "CarModel" )
            CarModels ModelCar = this._repository.AddOneCarModel(new CarModels()
            {
                // > Libellé  <
                Libelle = request.ItemCarModel.Libelle,

                // > Clé étrangère Marque Voiture  <
                FK_ID_CarBrand = request.ItemCarModel.FK_ID_CarBrand,

                // > Génération de l'ID  par programme <
                ID_CarModel = IDGuid.ToString()
            });

            // > On effectue la validation l'ajout en base de données <
            this._repository.UnitOfWork.SaveChanges();

            // > On verifie que l'ajout s'est bien passée <
            //  ( La méthode "AddOneCarModel" renvoie l'objet inséré par l'ORM )
            if (ModelCar != null)
            {
                // > On charge dans le modele DTO le model 
                modelDto = request.ItemCarModel;

                // > On récupère dans le DTO l'ID auto calculé par l'ORM <
                modelDto.ID_auto = ModelCar.ID;

                // > On récupère aussi l'ID calculé par code <
                modelDto.ID_CarModel = ModelCar.ID_CarModel;
                                
            }
            // > On renvoie le résultat <
            return Task.FromResult(modelDto);


        }
        #endregion

    }
}
