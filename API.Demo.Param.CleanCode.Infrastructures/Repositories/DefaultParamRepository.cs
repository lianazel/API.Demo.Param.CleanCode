using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// > Pour "IParamRepository" <
using API.Demo.Param.CleanCode.Domain;

// > Pour "IUnitOfWork" <
using API.Demo.Param.CleanCode.Framework;

// > Pour "Include" <
using Microsoft.EntityFrameworkCore;

namespace API.Demo.Param.CleanCode.Infrastructures.Repositories
{
    public  class DefaultParamRepository : IParamRepository
    {
        #region fields
        // > Déclare le context qui sera alimenté par injection de dépendance <
        public readonly DataContext _Context = null;
        #endregion

        #region property
        // > Installe le "SaveChange".
        public IUnitOfWork UnitOfWork => this._Context;
        #endregion  

        #region methods
        /// <summary>
        /// Constructeur de la classe 
        /// </summary>
        /// <param name="Context"></param>
        public DefaultParamRepository(DataContext Context)
        {
            // > Récupération du context < 
            this._Context = Context;
        }

        /// <summary>
        /// Ajout d'une Marque de voiture 
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        public CarBrand AddOneCarBrand(CarBrand Item)
        {          
            // > On retourne l'entité tracé par l'ORM <
            return this._Context.Brand.Add(Item).Entity;
        }


        /// <summary>
        /// Renvoie liste de marque ( ou une marque si ID transmis ).
        /// </summary>
        /// <param name="ID_CarBrand"></param>
        /// <returns></returns>
        public ICollection<CarBrand> GetAllCarBrand (string ID_CarBrand)
        {
            // > Extraction collection des CarBrand (MARQUE DE VOITURES)  <
            //    ( "AsQueryable()" pour autoriser derrière une clause "where" ) 
            var Elements = this._Context.Brand.AsQueryable();

            // > Si un ID est transmis, on ajoute une clause "where" 
            if (ID_CarBrand != "")
            {
                Elements = Elements.Where(item => item.ID_CarBrand == ID_CarBrand);
            }
            return Elements.ToList();
        }


        /// <summary>
        /// Mettre à jour une voiture 
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        public CarBrand UpdateCarBrand(CarBrand Item)
        {
            // > Lecture d'un enregistrement et mise ç jour de celui ci <
            var Brand = this._Context.Brand.FirstOrDefault(f => f.ID_CarBrand == Item.ID_CarBrand);

            // > Si l'enregistrement LU n'est PAS null <
            if (Brand != null)
            {
                // > Libellé  <
                Brand.Libelle = Item.Libelle;  
            }
            else
            {
                Brand = null;
            }
            // > On retourne l'entité tracé par l'ORM <
            return this._Context.Brand.Update(Brand).Entity;
                      
        }

        /// <summary>
        /// Ajout d'un modèle de voiture 
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        public CarModels AddOneCarModel(CarModels Item)
        {
            // > On retourne l'entité tracé par l'ORM <
            return this._Context.Models.Add(Item).Entity;

        }


        /// <summary>
        /// Ajout d'un modèle de voiture - CQRS
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>

        public CarModels AddOneCarModelCQRS(CarModels Item)
        {
            // > On retourne l'entité tracé par l'ORM <
            return this._Context.Models.Add(Item).Entity;

        }



        /// <summary>
        /// Màj d'un modèle de voiture 
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        public CarModels UpdateCarModel(CarModels Item)
        {
            // > Lecture d'un enregistrement et mise à jour de celui ci <
            var ModelCar = this._Context.Models.FirstOrDefault(f => f.ID_CarModel== Item.ID_CarModel);

            // > Si l'enregistrement LU n'est PAS null <
            if (ModelCar != null)
            {
                // > clé étrangère Marque de voiture   <
                ModelCar.FK_ID_CarBrand = Item.FK_ID_CarBrand;

                // > Libellé  <
                ModelCar.Libelle = Item.Libelle;
            }
            else
            {
                ModelCar = null;
            }
            // > On retourne l'entité tracé par l'ORM <
            return this._Context.Models.Update(ModelCar).Entity;

        }


        /// <summary>
        /// Renvoyer liste modèles pour une marque 
        /// </summary>
        /// <param name="ID_CarBrand"></param>
        /// <returns></returns>
        public ICollection<CarModels> GetAllCarModel(string ID_CarBrand)
        {
            // > "Include" se sert des fichiers de configuration "CFG_xxx" pour fonctionner <
            //    ( La relation est en effet définie dans ces tables de configuration )
            var ListModelCar = this._Context.Models.Include(Item => Item.Brand).AsQueryable();

            // > Pour la sélection, on s'appuie ici (pour l'exemple) sur le membre...
            //   ... du modèle "Carbrand" inclue dans la requête <

            // > Mais On aurait pu aussi travailler avec la cré étrangère "FK_ID_CarBrand" <
            if (ID_CarBrand != null )
            {
                ListModelCar = ListModelCar.Where(f => f.Brand.ID_CarBrand == ID_CarBrand);
            }

            return ListModelCar.ToList();

        }

        /// <summary>
        ///    Pour Appel via CQRS 
        /// </summary>
        /// <param name="ID_CarBrand"></param>
        /// <returns></returns>
        public ICollection<CarModels> GetAllCarModelCQRS(string ID_CarBrand)
        {
            // > "Include" se sert des fichiers de configuration "CFG_xxx" pour fonctionner <
            //    ( La relation est en effet définie dans ces tables de configuration )
            var ListModelCar = this._Context.Models.Include(Item => Item.Brand).AsQueryable();

            // > Pour la sélection, on s'appuie ici (pour l'exemple) sur le membre...
            //   ... du modèle "Carbrand" inclue dans la requête <

            // > Mais On aurait pu aussi travailler avec la cré étrangère "FK_ID_CarBrand" <
            if (ID_CarBrand != null)
            {
                ListModelCar = ListModelCar.Where(f => f.Brand.ID_CarBrand == ID_CarBrand);
            }

            return ListModelCar.ToList();

        }


        #endregion

    }
}
