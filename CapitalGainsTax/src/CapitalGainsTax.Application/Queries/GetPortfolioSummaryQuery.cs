using CapitalGainsTax.Application.DTOs;
using MediatR;

namespace CapitalGainsTax.Application.Queries
{
    public class GetPortfolioSummaryQuery : IRequest<PortfolioSummaryDTO>
    {
        public int PortfolioId { get; set; }

        public GetPortfolioSummaryQuery(int portfolioId)
        {
            PortfolioId = portfolioId;
        }
    }
}