using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
// > Pour "IParamRepository" <
using API.Demo.Param.CleanCode.Domain;
using API.Demo.Param.CleanCode.Applications.DTOs;
using MediatR;
using API.Demo.Param.CleanCode.Applications.Queries;
using API.Demo.Param.CleanCode.Applications.Commands;

namespace API.Demo.Param.CleanCode.Controllers
{    
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        #region fields
        // > Pour récupérer l'injection de dépandance <
        //   ( Voir la classe "Program.cs" )
        private readonly IParamRepository _repository = null;

        // > On déclare un membre pour récupérer l'envireonnement d'exécution <
        //   ( ici on en aura pas besoin, mais c'est pour l'exemple )
        private readonly IWebHostEnvironment _webHostEnvironment = null;

        // > Récuparation du médiateur <
        private readonly IMediator _mediator = null;
        #endregion

        /// <summary>
        /// Constructeur du contrôleur API 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="webHost"></param>
        //   (Installation injection dépendance & récupération environnement exécution)
        #region Constructor
        public ApiController(IMediator mediator, IParamRepository repository, IWebHostEnvironment webHost)
        {
            // > Récupération du repository < 
            _repository = repository;

            // > Récupération environnement exécution <
            _webHostEnvironment = webHost;

            // > Récupération du médiateur ( via l'injection de dépendances )
            _mediator = mediator;

        }
        #endregion

        #region Methods
        /// <summary>
        /// Add CarBrand : Ajouter une marque de voiture 
        /// </summary>
        /// <param name="BrandDto"></param>
        /// <returns></returns>
        [Route("PostCarBrand")]
        [HttpPost]
        public IActionResult AddCarBrand([FromBody] CarBrandDTO BrandDto)
        {
            // > Par défaut, on est pessimiste <
            IActionResult result = this.BadRequest();

            // > (1B) ==> Création ID
            Guid IDGuid = Guid.NewGuid();

            // > On appelle la méthode "AddOneCarBrand" < du repository <
            //   ( On passe à la méthode "AddOneCarBrand"...
            //     ...un paramétre de type "CarBrand" ).
            //   ( La méthode "AddOneCarBrand" renvoie un objet "CarBrand" )
            CarBrand brand = this._repository.AddOneCarBrand(new CarBrand()
            {
                // > Libellé reçu dans le DTO <
                Libelle = BrandDto.Libelle,

                // > Génération de l'ID  par programme <
                ID_CarBrand = IDGuid.ToString()
            });

            // > On effectue la validation l'ajout en base de données <
            int toto = this._repository.UnitOfWork.SaveChanges();

            // > On verifie que l'ajout s'est bien passée <
            //  ( La méthode "AddOneCarBrand" renvoie l'objet inséré par l'ORM )
            if (brand != null)
            {
                // > On récupère dans le DTO l'ID auto calculé par l'ORM <
                BrandDto.ID_auto = brand.ID;
                // > On récupère aussi l'ID calculé par code <
                BrandDto.ID_CarBrand = brand.ID_CarBrand;

                // > renvoie un objet DTO <
                result = this.Ok(BrandDto);
            }

            // > On renvoie le résultat <
            return result;
        }

        /// <summary>
        /// UpdateCarBrand : Mettre à jour une marque 
        /// </summary>
        /// <param name="BrandDto"></param>
        /// <returns></returns>
        [Route("UpdCarBrand")]
        [HttpPost]
        public IActionResult UpdateCarBrand([FromBody] CarBrandDTO BrandDto)
        {
            // > Par défaut, on est pessimiste <
            IActionResult result = this.BadRequest();

            // > On appelle la méthode "UpdateCarBrand" du repository <
            //   ( On passe à la méthode "UpdateCarBrand"...
            //     ...un paramétre de type "CarBrand" ).
            CarBrand brand = this._repository.UpdateCarBrand(new CarBrand()
            {
                // > On passe la clé pour la lecture dans la méthode "UpdateCarBrand" <
                ID_CarBrand = BrandDto.ID_CarBrand,
                // > Libellé à mettre à jour <
                Libelle = BrandDto.Libelle,
            });

            // > On effectue la validation de la mise à jour en base de données <
            this._repository.UnitOfWork.SaveChanges();

            // > On verifie que la mise à jour s'est bien passée <
            //  ( La méthode "UpdateCarBrand" renvoie l'objet màj par l'ORM )
            if (brand != null)
            {
                // > renvoie un objet DTO <
                result = this.Ok(BrandDto);
            }

            // > On renvoie le résultat <
            return result;
        }



        /// <summary>
        /// GetListCarBrand : Renvoyer liste marques de voiture 
        /// </summary>
        /// <param name="ID_CarBrand"></param>
        /// <returns></returns>
        [Route("GetCarBrand")]
        [HttpGet]
        public IActionResult GetListCarBrand([FromQuery] string? ID_CarBrand)
        {
            // > Lance l'extraction de la liste <
            // ( Si un ID est transmis, on ne renvoie que l'enregistrement...
            //   ...correspondant ).

            // Remarque : la clause "Where" sur le paramètre...
            // ..."ID_CarBrand" se fait dans la méthode "GetAllCarBrand" du...
            // ... _repository.
            var BrandList = this._repository.GetAllCarBrand(ID_CarBrand);

            // > On construit les colonnes de la liste d'objets que l'on renvoie <
            //    ( On renvoie une Liste de "CarBrandDTO()" ) 
            var elements = BrandList.Select(item => new CarBrandDTO()
            {
                ID_auto = item.ID,
                ID_CarBrand = item.ID_CarBrand,
                Libelle = item.Libelle,
            }).ToList();

            return this.Ok(elements);
        }


        /// <summary>
        /// Ajouter un Modele d'une marque de voiture 
        /// </summary>
        /// <param name="ModelDto"></param>
        /// <returns></returns>
        [Route("PostCarModel")]
        [HttpPost]
        public IActionResult AddCarModel([FromBody] CarModelDTO ModelDto)
        {
            // > Par défaut, on est pessimiste <
            IActionResult result = this.BadRequest();

            // > (1B) ==> Création ID
            Guid IDGuid = Guid.NewGuid();

            // > On appelle la méthode "AddOneCarModel" < du repository <
            //   ( On passe à la méthode "AddOneCarModel"...
            //     ...un paramétre de type "CarModel" ).
            //   ( La méthode "AddOneCarModel" renvoie un objet "CarModel" )
            CarModels ModelCar = this._repository.AddOneCarModel(new CarModels()
            {
                // > Libellé reçu dans le DTO <
                Libelle = ModelDto.Libelle,

                // > Clé étrangère Marque Voiture  <
                FK_ID_CarBrand = ModelDto.FK_ID_CarBrand,

                // > Génération de l'ID  par programme <
                ID_CarModel = IDGuid.ToString()
            });

            // > On effectue la validation l'ajout en base de données <
            this._repository.UnitOfWork.SaveChanges();

            // > On verifie que l'ajout s'est bien passée <
            //  ( La méthode "AddOneCarModel" renvoie l'objet inséré par l'ORM )
            if (ModelCar != null)
            {
                // > On récupère dans le DTO l'ID auto calculé par l'ORM <
                ModelDto.ID_auto = ModelCar.ID;
                // > On récupère aussi l'ID calculé par code <
                ModelDto.ID_CarModel = ModelCar.ID_CarModel;

                // > renvoie un objet DTO <
                result = this.Ok(ModelDto);
            }
            // > On renvoie le résultat <
            return result;
        }

        /// <summary>
        /// Ajouter un Modele d'une marque de voiture (VIA CQRS)
        /// </summary>
        /// <param name="ModelDto"></param>
        /// <returns></returns>
        [Route("PostCarModelCQRS")]
        [HttpPost]
        public IActionResult AddCarModelCQRS([FromBody] CarModelDTO ModelDto)
        {
            // - - - - - - - - 
            // Remarque : Le système va appeler la classe "AddModelsHandler" (via la méthode "Send") 
            // - - - - - - - - 

            // > On charge le membre "ItemCarModel" de l'instance "AddModelCommand"...
            //   ...avec le "ModelDto" que l'on a reçu en paramètre ( dans le Body ).
            this._mediator.Send(new AddModelCommand() { ItemCarModel = ModelDto });

            return  this.Ok(ModelDto); 
        }


        [Route("UpdateCarBrand")]
        [HttpPost]
        public IActionResult UpdCarModel([FromBody] CarModelDTO ModelDto)
        {

            // > Par défaut, on est pessimiste <
            IActionResult result = this.BadRequest();

            // > On appelle la méthode "UpdateCarModel" du repository <
            //   ( On passe à la méthode "UpdateCarModel"...
            //     ...un paramétre de type "CarModels" ).
            CarModels ModelCar = this._repository.UpdateCarModel(new CarModels()
            {
                // > On passe la clé pour la lecture dans la méthode "UpdateCarModel" <
                ID_CarModel = ModelDto.ID_CarModel,
                // > Libellé à mettre à jour <
                Libelle = ModelDto.Libelle,
            });

            // > On effectue la validation de la mise à jour en base de données <
            this._repository.UnitOfWork.SaveChanges();

            // > On verifie que la mise à jour s'est bien passée <
            //  ( La méthode "UpdateCarModel" renvoie l'objet màj par l'ORM )
            if (ModelCar != null)
            {
                // > renvoie un objet DTO <
                result = this.Ok(ModelDto);
            }

            // > On renvoie le résultat <
            return result;
        }

        /// <summary>
        /// Méthode "GetListCarModels" SANS CQRS 
        /// </summary>
        /// <param name="ID_CarBrand"></param>
        /// <returns></returns>
        [Route("GetCarModel")]
        [HttpGet]
        public IActionResult GetListCarModels([FromQuery] string ID_CarBrand)
        {
            // > Lance l'extraction de la liste <
            // ( Si un ID est transmis, on ne renvoie que l'enregistrement...
            //   ...correspondant ).

            // Remarque : la clause "Where" sur le paramètre...
            // ..."ID_CarBrand" se fait dans la méthode "GetAllCarModel" du...
            // ... _repository.
            var ModelsCarList = this._repository.GetAllCarModel(ID_CarBrand);

            // > On construit les colonnes de la liste d'objets que l'on renvoie <
            //    ( On renvoie une Liste de "CarBrandDTO()" ) 
            var elements = ModelsCarList.Select(item => new CarModelDTO()
            {
                ID_auto = item.ID,
                ID_CarModel = item.ID_CarModel,
                Libelle = item.Libelle,
                FK_ID_CarBrand = item.FK_ID_CarBrand,
            }).ToList();

            return this.Ok(elements);

        }

        
        /// <summary>
        /// Méthode "GetListCarModels" AVEC CQRS 
        /// </summary>
        /// <param name="ID_CarBrand"></param>
        /// <returns></returns>
        [Route("GetCarModelCQRS")]
        [HttpGet]
        public IActionResult GetListCarModelsCQRS([FromQuery] string ID_CarBrand)
        {
            // > "Send" ==> On envoie une demande <

            // > On charge dans le membre "ID_CarBrandQry" de la classe ==>...
            //  ...On récupère la liste des modèles d'une marque <
            
            // > On récupère une liste ("elements") car la classe "SelectAllModelsQuery()"...
            //    renvoie une liste de "CarModelDto" ( voir le "IRequest<List<CarModelDTO>>" ) 
            var elements = this._mediator.Send(new SelectAllModelsQuery() { ID_CarBrandQry = ID_CarBrand });

            return this.Ok(elements);

        }

        #endregion


    }
}
