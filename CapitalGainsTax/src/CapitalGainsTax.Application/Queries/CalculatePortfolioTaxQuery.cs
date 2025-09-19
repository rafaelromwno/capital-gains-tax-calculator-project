using CapitalGainsTax.Application.DTOs;
using MediatR;

namespace CapitalGainsTax.Application.Queries
{
    public class CalculatePortfolioTaxQuery : IRequest<PortfolioSummaryDTO>
    {
        public int PortfolioId { get; }

        public CalculatePortfolioTaxQuery(int portfolioId)
        {
            PortfolioId = portfolioId;
        }
    }
}