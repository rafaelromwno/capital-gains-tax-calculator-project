using CapitalGainsTax.Application.DTOs;
using CapitalGainsTax.Application.Interfaces;
using CapitalGainsTax.Domain.Entities;
using MediatR;

namespace CapitalGainsTax.Application.Queries
{
    public class CreatePortfolioCommandHandler : IRequestHandler<CreatePortfolioCommand, PortfolioSummaryDTO>
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public CreatePortfolioCommandHandler(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<PortfolioSummaryDTO> Handle(CreatePortfolioCommand request, CancellationToken cancellationToken)
        {
            var portfolio = new Portfolio(0, request.InvestorName);
            await _portfolioRepository.AddAsync(portfolio);

            return new PortfolioSummaryDTO
            {
                PortfolioId = portfolio.PortfolioId,
                InvestorName = portfolio.InvestorName,
                WeightedAveragePrice = portfolio.WeightedAveragePrice,
                CurrentQuantity = portfolio.CurrentQuantity,
                LossCarryForward = portfolio.LossCarryForward,
                LastProfit = 0,
                LastTaxDue = 0
            };
        }
    }
}