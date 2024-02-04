using API.Demo.Param.CleanCode.Applications.DTOs;
using MediatR;


namespace API.Demo.Param.CleanCode.Applications.Commands
{
    /// <summary>
    ///   Command to ADD a Car Model
    /// </summary>
    // > Renvoie un "CarModelDTO" <  
    public class AddModelCommand : IRequest<CarModelDTO>
    {
        #region Properties
        public CarModelDTO ItemCarModel { get; set; }
        #endregion
    }
}
