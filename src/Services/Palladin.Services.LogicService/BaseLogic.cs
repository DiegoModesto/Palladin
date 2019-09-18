using Palladin.Services.LogicService.Contracts;

namespace Palladin.Services.LogicService
{
    public abstract class BaseLogic : IBaseLogic
    {
        public string ConnectionString { get; set; }
    }
}
