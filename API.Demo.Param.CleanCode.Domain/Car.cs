using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Demo.Param.CleanCode.Domain
{
    /// <summary>
    /// Table des voitures 
    /// </summary>
    public class Car
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
        #endregion

    }
}
