using CapitalGainsTax.Application.DTOs;
using MediatR;

namespace CapitalGainsTax.Application.Queries
{
    public class GetPortfolioOperationsQuery : IRequest<IEnumerable<OperationDTO>>
    {
        public int PortfolioId { get; set; }

        public GetPortfolioOperationsQuery(int portfolioId)
        {
            PortfolioId = portfolioId;
        }

    }
}