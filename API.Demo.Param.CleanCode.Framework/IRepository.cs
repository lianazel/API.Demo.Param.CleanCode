using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Demo.Param.CleanCode.Framework
{
    /// <summary>
    ///  Use it to define class as repository 
    /// </summary>

    public interface IRepository
    {
        //  > !! TOUT CE QUI SERA REPOSITORY IMPLEMENTERA CELA !!  <
        // > On renvoie l'interface "IUnitOfWork" <
        //   ( Pas de "set" car on ne pas l'affecter, ...
        //     ...car ici one fait que renvoyer )
        IUnitOfWork UnitOfWork { get; } 
      
    }
}
