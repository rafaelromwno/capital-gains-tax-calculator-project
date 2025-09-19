using CapitalGainsTax.Application.DTOs;
using CapitalGainsTax.Application.Interfaces;
using MediatR;

namespace CapitalGainsTax.Application.Queries
{
    public class GetAllPortfoliosQueryHandler : IRequestHandler<GetAllPortfoliosQuery, IEnumerable<PortfolioSummaryDTO>>
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public GetAllPortfoliosQueryHandler(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<IEnumerable<PortfolioSummaryDTO>> Handle(GetAllPortfoliosQuery request, CancellationToken cancellationToken)
        {
            var portfolios = await _portfolioRepository.GetAllAsync();

            return portfolios.Select(p => new PortfolioSummaryDTO
            {
                PortfolioId = p.PortfolioId,
                InvestorName = p.InvestorName,
                WeightedAveragePrice = p.WeightedAveragePrice,
                CurrentQuantity = p.CurrentQuantity,
                LossCarryForward = p.LossCarryForward,
                LastProfit = 0,
                LastTaxDue = 0
            });
        }
    }
}
