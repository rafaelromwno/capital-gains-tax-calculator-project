using CapitalGainsTax.Application.Commands;
using CapitalGainsTax.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CapitalGainsTax.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PortfolioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("operation")]
        public async Task<IActionResult> RegisterOperation([FromBody] RegisterOperationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPortfolioSummary(int id)
        {
            var query = new GetPortfolioSummaryQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}