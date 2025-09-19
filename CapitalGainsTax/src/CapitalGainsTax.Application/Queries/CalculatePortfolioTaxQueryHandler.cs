using CapitalGainsTax.Application.DTOs;
using CapitalGainsTax.Application.Interfaces;
using CapitalGainsTax.Domain.Entities;
using CapitalGainsTax.Domain.Enums;
using MediatR;

namespace CapitalGainsTax.Application.Queries
{
    public class CalculatePortfolioTaxQueryHandler : IRequestHandler<CalculatePortfolioTaxQuery, PortfolioSummaryDTO>
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public CalculatePortfolioTaxQueryHandler(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<PortfolioSummaryDTO> Handle(CalculatePortfolioTaxQuery request, CancellationToken cancellationToken)
        {
            var portfolio = await _portfolioRepository.GetByIdAsync(request.PortfolioId);

            if (portfolio == null)
                throw new Exception("Portfolio not found.");

            // imposto fixo de 20%
            var tax = new Tax(1, "Capital Gains Tax", 0.20m);

            // para simplificação pego só o último resultado
            var lastOperation = portfolio.Operations.LastOrDefault();
            decimal lastProfit = 0;
            decimal lastTaxDue = 0;

            if (lastOperation != null && lastOperation.Type == OperationType.Sell)
            {
                var result = portfolio.AddOperation(
                    new Operation(
                        lastOperation.OperationId,
                        lastOperation.UnitCost,
                        lastOperation.Quantity,
                        lastOperation.Type
                    ), tax
                );

                lastProfit = result.Profit;
                lastTaxDue = result.TaxDue;
            }

            return new PortfolioSummaryDTO
            {
                PortfolioId = portfolio.PortfolioId,
                InvestorName = portfolio.InvestorName,
                WeightedAveragePrice = portfolio.WeightedAveragePrice,
                CurrentQuantity = portfolio.CurrentQuantity,
                LossCarryForward = portfolio.LossCarryForward,
                LastProfit = lastProfit,
                LastTaxDue = lastTaxDue
            };
        }
    }
}