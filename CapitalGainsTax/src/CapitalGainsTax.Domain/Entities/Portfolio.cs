using CapitalGainsTax.Domain.Enums;
using CapitalGainsTax.Domain.Exceptions;
using CapitalGainsTax.Domain.ValueObjects;

namespace CapitalGainsTax.Domain.Entities
{
    public sealed class Portfolio
    {
        #region Propriedades

        public int PortfolioId { get; private set; }
        public string InvestorName { get; private set; }

        public decimal WeightedAveragePrice { get; private set; } = 0;
        public int CurrentQuantity { get; private set; } = 0;
        public decimal LossCarryForward { get; private set; } = 0;

        private readonly List<Operation> _operations = new();
        public IReadOnlyCollection<Operation> Operations => _operations.AsReadOnly();

        #endregion

        #region Construtores

        private Portfolio() { }

        public Portfolio(int portfolioId, string investorName)
        {
            ValidateDomain(portfolioId, investorName);

            PortfolioId = portfolioId;
            InvestorName = investorName;
        }

        #endregion

        #region Validação

        private void ValidateDomain(int portfolioId, string investorName)
        {
            DomainExceptionValidation.When(portfolioId < 0, "Invalid Id value.");
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(investorName), "Investor name is required.");
            DomainExceptionValidation.When(investorName.Length < 3, "Investor name must have at least 3 characters.");
        }

        #endregion

        #region Métodos de Negócio

        public TaxResult AddOperation(Operation operation, Tax tax)
        {
            DomainExceptionValidation.When(operation == null, "Operation cannot be null.");
            _operations.Add(operation);

            if (operation.Type == OperationType.Buy)
            {
                ApplyBuy(operation);
                return new TaxResult(0, 0); // compra não gera imposto
            }
            else if (operation.Type == OperationType.Sell)
            {
                return ApplySell(operation, tax);
            }

            return new TaxResult(0, 0);
        }

        private void ApplyBuy(Operation operation)
        {
            // novo preço médio ponderado
            WeightedAveragePrice =
                ((CurrentQuantity * WeightedAveragePrice) + (operation.Quantity * operation.UnitCost)) /
                (CurrentQuantity + operation.Quantity);

            CurrentQuantity += operation.Quantity;
        }

        private TaxResult ApplySell(Operation operation, Tax tax)
        {
            if (operation.Quantity > CurrentQuantity)
                throw new DomainExceptionValidation("Not enough shares to sell.");

            var saleValue = operation.GetTotal();
            var profit = (operation.UnitCost - WeightedAveragePrice) * operation.Quantity;

            decimal taxableProfit = profit;
            decimal taxDue = 0;

            // isenção até R$20.000 no valor da venda
            if (saleValue <= 20000)
            {
                if (profit < 0)
                    LossCarryForward += Math.Abs(profit);

                CurrentQuantity -= operation.Quantity;
                return new TaxResult(profit, 0);
            }

            // deduz prejuízos acumulados
            if (LossCarryForward > 0)
            {
                if (LossCarryForward >= taxableProfit)
                {
                    LossCarryForward -= taxableProfit;
                    taxableProfit = 0;
                }
                else
                {
                    taxableProfit -= LossCarryForward;
                    LossCarryForward = 0;
                }
            }

            if (taxableProfit > 0)
            {
                taxDue = tax.Calculate(taxableProfit);
            }
            else if (taxableProfit < 0)
            {
                LossCarryForward += Math.Abs(taxableProfit);
            }

            CurrentQuantity -= operation.Quantity;
            return new TaxResult(profit, taxDue);
        }

        #endregion
    }
}