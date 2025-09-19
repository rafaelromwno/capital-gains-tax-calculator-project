using CapitalGainsTax.Application.DTOs;
using CapitalGainsTax.Application.Interfaces;
using CapitalGainsTax.Domain.Entities;
using CapitalGainsTax.Domain.Enums;
using MediatR;

namespace CapitalGainsTax.Application.Commands
{
    public class RegisterOperationCommandHandler : IRequestHandler<RegisterOperationCommand, PortfolioSummaryDTO>
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public RegisterOperationCommandHandler(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<PortfolioSummaryDTO> Handle(RegisterOperationCommand request, CancellationToken cancellationToken)
        {
            var portfolio = await _portfolioRepository.GetByIdAsync(request.PortfolioId) ?? throw new Exception("Portfolio not found.");

            var type = request.Type.Equals("Buy", StringComparison.OrdinalIgnoreCase)
                ? OperationType.Buy
                : OperationType.Sell;

            var operation = new Operation(0, request.UnitCost, request.Quantity, type);

            var tax = new Tax(1, "Capital Gains Tax", 0.20m); // regra fixa de 20%
            var result = portfolio.AddOperation(operation, tax);

            await _portfolioRepository.UpdateAsync(portfolio);

            return new PortfolioSummaryDTO
            {
                PortfolioId = portfolio.PortfolioId,
                InvestorName = portfolio.InvestorName,
                WeightedAveragePrice = portfolio.WeightedAveragePrice,
                CurrentQuantity = portfolio.CurrentQuantity,
                LossCarryForward = portfolio.LossCarryForward,
                LastProfit = result.Profit,
                LastTaxDue = result.TaxDue
            };
        }
    }
}