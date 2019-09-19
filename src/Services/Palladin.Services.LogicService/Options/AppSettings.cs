using System;

namespace Palladin.Services.LogicService.Options
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public TimeSpan TokenLifeTime { get; set; }
        public string ConString { get; set; }
        public string DomainName { get; set; }
    }
}
