using CapitalGainsTax.Application.DTOs;
using MediatR;

namespace CapitalGainsTax.Application.Queries
{
    public class GetAllPortfoliosQuery : IRequest<IEnumerable<PortfolioSummaryDTO>>
    {
    }
}