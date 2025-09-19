using CapitalGainsTax.Application.DTOs;
using MediatR;

namespace CapitalGainsTax.Application.Commands
{
    public class RegisterOperationCommand : IRequest<PortfolioSummaryDTO>
    {
        public int PortfolioId { get; set; }
        public string Asset { get; set; }
        public decimal UnitCost { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; } // buy ou sell
    }
}