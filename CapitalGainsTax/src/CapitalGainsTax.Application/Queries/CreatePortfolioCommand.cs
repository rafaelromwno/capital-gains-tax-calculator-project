using CapitalGainsTax.Application.DTOs;
using MediatR;

namespace CapitalGainsTax.Application.Queries
{
    public class CreatePortfolioCommand : IRequest<PortfolioSummaryDTO>
    {
        public string InvestorName { get; set; }
    }
}