namespace CapitalGainsTax.Application.DTOs
{
    public class OperationDTO
    {
        public int OperationId { get; set; }
        public string? Asset { get; set; }
        public decimal UnitCost { get; set; }
        public int Quantity { get; set; }
        public string? Type { get; set; } // "Buy" ou "Sell"
        public DateTime Date { get; set; }
    }
}
