using Palladin.Services.ApiContract.V1.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Palladin.Services.LogicService.Contracts
{
    public interface IDashboardService
    {
        Task<SummaryResponse> GetCriticalSummaryAsync();
        Task<SummaryResponse> GetHighSummaryAsync();
        Task<SummaryResponse> GetMiddleSummaryAsync();
        Task<SummaryResponse> GetLowSummaryAsync();
    }
}
