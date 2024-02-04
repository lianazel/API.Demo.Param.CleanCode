using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// > Pour "IRepository" <
using API.Demo.Param.CleanCode.Framework;

namespace API.Demo.Param.CleanCode.Domain
{
    /// <summary>
    /// Interface Manage Repository 
    /// </summary>
    public  interface IParamRepository : IRepository
    {
       /// <summary>
       /// Ajout d'une marque de voiture 
       /// </summary>
       /// <param name="brand"></param>
       /// <returns></returns>
        public CarBrand AddOneCarBrand (CarBrand brand);


        /// <summary>
        /// Mettre à jour une marque 
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public CarBrand UpdateCarBrand(CarBrand brand);

        /// <summary>
        /// Renvoi liste de marque ( ou d'une marque ) 
        /// </summary>
        /// <param name="ID_CarBrand"></param>
        /// <returns></returns>
        public ICollection<CarBrand> GetAllCarBrand (string ID_CarBrand);
               

        /// <summary>
        /// Ajout d'un modèle de voiture 
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public CarModels AddOneCarModel(CarModels ModelDTO);

        /// <summary>
        /// Ajout d'un modèle de voiture ( avec CQRS )
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public CarModels AddOneCarModelCQRS(CarModels ModelDTO);


        /// <summary>
        /// Mettre à jour un modèle de voiture 
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public CarModels UpdateCarModel(CarModels Model);


        /// <summary>
        /// Liste des modèles pour une marque 
        /// </summary>
        /// <param name="ID_CarBrand"></param>
        /// <returns></returns>
        public ICollection<CarModels> GetAllCarModel(string ID_CarBrand);

        /// <summary>
        /// Liste des modèles pour une marque ( avec CQRS )
        /// 
        /// </summary>
        /// <param name="ID_CarBrand"></param>
        /// <returns></returns>
        public ICollection<CarModels> GetAllCarModelCQRS(string ID_CarBrand);

    }
}
