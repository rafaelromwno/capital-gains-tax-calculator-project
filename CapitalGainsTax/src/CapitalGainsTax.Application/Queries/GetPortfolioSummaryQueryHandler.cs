using CapitalGainsTax.Application.DTOs;
using CapitalGainsTax.Application.Interfaces;
using MediatR;

namespace CapitalGainsTax.Application.Queries
{
    public class GetPortfolioSummaryQueryHandler : IRequestHandler<GetPortfolioSummaryQuery, PortfolioSummaryDTO>
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public GetPortfolioSummaryQueryHandler(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<PortfolioSummaryDTO> Handle(GetPortfolioSummaryQuery request, CancellationToken cancellationToken)
        {
            var portfolio = await _portfolioRepository.GetByIdAsync(request.PortfolioId);

            if (portfolio == null)
                throw new Exception("Portfolio not found.");

            return new PortfolioSummaryDTO
            {
                PortfolioId = portfolio.PortfolioId,
                InvestorName = portfolio.InvestorName,
                WeightedAveragePrice = portfolio.WeightedAveragePrice,
                CurrentQuantity = portfolio.CurrentQuantity,
                LossCarryForward = portfolio.LossCarryForward,
                LastProfit = 0,   // apenas consulta, sem calcular nova operação
                LastTaxDue = 0
            };
        }
    }
}