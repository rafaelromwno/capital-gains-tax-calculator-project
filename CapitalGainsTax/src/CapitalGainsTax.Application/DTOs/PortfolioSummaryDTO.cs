namespace CapitalGainsTax.Application.DTOs
{
    public class PortfolioSummaryDTO
    {
        public int PortfolioId { get; set; }
        public string? InvestorName { get; set; }
        public decimal WeightedAveragePrice { get; set; }
        public int CurrentQuantity { get; set; }
        public decimal LossCarryForward { get; set; }
        public decimal LastProfit { get; set; }
        public decimal LastTaxDue { get; set; }
    }
}