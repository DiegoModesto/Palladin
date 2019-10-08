using Palladin.Data.Repository;
using Palladin.Services.ApiContract.V1.Responses;
using Palladin.Services.LogicService.Contracts;
using System.Threading.Tasks;

namespace Palladin.Services.LogicService.Services
{
    public class DashboardService : IDashboardService
    {
        public async Task<SummaryResponse> GetCriticalSummaryAsync()
        {
            using (var uow = new UnitOfWork())
            {
                return new SummaryResponse
                {
                    Title = "_CRITICAL_",
                    ShortDescription = "_CRITICAL_VULNERABILITIES_",
                    Value = uow._vultR.GetCountByRiskFactor(Enums.RiskFactor.Critical),
                    Percentage = uow._vultR.GetPercentageByRiskFactor(Enums.RiskFactor.Critical).ToString(),
                    Level = 0
                };
            }
        }

        public async Task<SummaryResponse> GetHighSummaryAsync()
        {
            using (var uow = new UnitOfWork())
            {
                return new SummaryResponse
                {
                    Title = "_CRITICAL_",
                    ShortDescription = "_HIGH_VULNERABILITIES_",
                    Value = uow._vultR.GetCountByRiskFactor(Enums.RiskFactor.High),
                    Percentage = uow._vultR.GetPercentageByRiskFactor(Enums.RiskFactor.High).ToString(),
                    Level = 1
                };
            }
        }

        public async Task<SummaryResponse> GetLowSummaryAsync()
        {
            using (var uow = new UnitOfWork())
            {
                return new SummaryResponse
                {
                    Title = "_LOW_",
                    ShortDescription = "_LOW_VULNERABILITIES_",
                    Value = uow._vultR.GetCountByRiskFactor(Enums.RiskFactor.Low),
                    Percentage = uow._vultR.GetPercentageByRiskFactor(Enums.RiskFactor.Low).ToString(),
                    Level = 3
                };
            }
        }

        public async Task<SummaryResponse> GetMiddleSummaryAsync()
        {
            using (var uow = new UnitOfWork())
            {
                return new SummaryResponse
                {
                    Title = "_MIDDLE_",
                    ShortDescription = "_MIDDLE_VULNERABILITIES_",
                    Value = uow._vultR.GetCountByRiskFactor(Enums.RiskFactor.Mid),
                    Percentage = uow._vultR.GetPercentageByRiskFactor(Enums.RiskFactor.Mid).ToString(),
                    Level = 2
                };
            }
        }
    }
}
