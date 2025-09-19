using CapitalGainsTax.Application.Commands;
using CapitalGainsTax.Application.DTOs;
using CapitalGainsTax.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CapitalGainsTax.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PortfolioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PortfolioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria um novo portfólio para um investidor.
        /// </summary>
        /// <param name="command">Nome do investidor.</param>
        /// <returns>Resumo do portfólio criado.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PortfolioSummaryDTO), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreatePortfolio([FromBody] CreatePortfolioCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetPortfolioSummary), new { id = result.PortfolioId }, result);
        }

        /// <summary>
        /// Retorna todos os portfólios cadastrados.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PortfolioSummaryDTO>), 200)]
        public async Task<IActionResult> GetAllPortfolios()
        {
            var query = new GetAllPortfoliosQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Registra uma operação de compra ou venda em um portfólio.
        /// </summary>
        [HttpPost("{id}/operation")]
        [ProducesResponseType(typeof(PortfolioSummaryDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RegisterOperation(int id, [FromBody] RegisterOperationCommand command)
        {
            command.PortfolioId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Retorna um resumo de um portfólio específico.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PortfolioSummaryDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPortfolioSummary(int id)
        {
            var query = new GetPortfolioSummaryQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Retorna todas as operações de um portfólio.
        /// </summary>
        [HttpGet("{id}/operations")]
        [ProducesResponseType(typeof(IEnumerable<OperationDTO>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetOperations(int id)
        {
            var query = new GetPortfolioOperationsQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Calcula o imposto devido de um portfólio.
        /// </summary>
        [HttpGet("{id}/tax")]
        [ProducesResponseType(typeof(PortfolioSummaryDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CalculateTax(int id)
        {
            var query = new CalculatePortfolioTaxQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}