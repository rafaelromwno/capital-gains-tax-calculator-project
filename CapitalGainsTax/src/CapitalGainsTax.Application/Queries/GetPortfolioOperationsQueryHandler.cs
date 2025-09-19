using CapitalGainsTax.Application.DTOs;
using CapitalGainsTax.Application.Interfaces;
using MediatR;

namespace CapitalGainsTax.Application.Queries
{
    public class GetPortfolioOperationsQueryHandler : IRequestHandler<GetPortfolioOperationsQuery, IEnumerable<OperationDTO>>
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public GetPortfolioOperationsQueryHandler(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<IEnumerable<OperationDTO>> Handle(GetPortfolioOperationsQuery request, CancellationToken cancellationToken)
        {
            var portfolio = await _portfolioRepository.GetByIdAsync(request.PortfolioId);

            if (portfolio == null)
                throw new Exception("Portfolio not found.");

            return portfolio.Operations.Select(o => new OperationDTO
            {
                OperationId = o.OperationId,
                Asset = "N/A", // se não estiver no domínio, pode ignorar ou extender operation
                UnitCost = o.UnitCost,
                Quantity = o.Quantity,
                Type = o.Type.ToString(),
                Date = DateTime.Now // se operation não tiver data, pode adicionar no domínio
            });
        }
    }
}