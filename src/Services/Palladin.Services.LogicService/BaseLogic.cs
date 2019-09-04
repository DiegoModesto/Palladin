using Palladin.Services.LogicService.Interfaces;

namespace Palladin.Services.LogicService
{
    public abstract class BaseLogic : IBaseLogic
    {
        public string ConnectionString { get; set; }
    }
}
