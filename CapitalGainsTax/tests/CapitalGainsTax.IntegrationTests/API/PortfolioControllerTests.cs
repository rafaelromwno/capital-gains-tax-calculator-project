using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CapitalGainsTax.API;
using CapitalGainsTax.Application.Commands;
using CapitalGainsTax.Application.DTOs;
using CapitalGainsTax.Application.Queries;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CapitalGainsTax.IntegrationTests
{
    public class PortfolioControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public PortfolioControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        private static async Task<T?> TryReadJsonAsync<T>(HttpResponseMessage response)
        {
            var raw = await response.Content.ReadAsStringAsync();
            System.Console.WriteLine($"Response: {raw}");

            if (response.Content.Headers.ContentType?.MediaType == "application/json")
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }

            return default;
        }

        [Fact]
        public async Task CreatePortfolio_ShouldReturnCreated()
        {
            var command = new CreatePortfolioCommand { InvestorName = "Alice" };

            var response = await _client.PostAsJsonAsync("/api/portfolio", command);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var portfolio = await TryReadJsonAsync<PortfolioSummaryDTO>(response);
            Assert.NotNull(portfolio);
            Assert.Equal("Alice", portfolio!.InvestorName);
        }

        [Fact]
        public async Task GetAllPortfolios_ShouldReturnOk()
        {
            var response = await _client.GetAsync("/api/portfolio");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var portfolios = await TryReadJsonAsync<IEnumerable<PortfolioSummaryDTO>>(response);
            Assert.NotNull(portfolios);
        }

        [Fact]
        public async Task GetPortfolioSummary_ShouldReturnOk_WhenPortfolioExists()
        {
            var command = new CreatePortfolioCommand { InvestorName = "Bob" };
            var createResponse = await _client.PostAsJsonAsync("/api/portfolio", command);
            var created = await TryReadJsonAsync<PortfolioSummaryDTO>(createResponse);

            var response = await _client.GetAsync($"/api/portfolio/{created!.PortfolioId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var portfolio = await TryReadJsonAsync<PortfolioSummaryDTO>(response);
            Assert.NotNull(portfolio);
        }

        [Fact]
        public async Task RegisterOperation_ShouldReturnOk_WhenPortfolioExists()
        {
            var command = new CreatePortfolioCommand { InvestorName = "Charlie" };
            var createResponse = await _client.PostAsJsonAsync("/api/portfolio", command);
            var created = await TryReadJsonAsync<PortfolioSummaryDTO>(createResponse);

            var operation = new RegisterOperationCommand
            {
                Type = "BUY",
                Asset = "PETR4",
                Quantity = 10,
                UnitCost = 30.5m
            };

            var response = await _client.PostAsJsonAsync($"/api/portfolio/{created!.PortfolioId}/operation", operation);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var updated = await TryReadJsonAsync<PortfolioSummaryDTO>(response);
            Assert.NotNull(updated);
        }

        [Fact]
        public async Task GetOperations_ShouldReturnOk()
        {
            var command = new CreatePortfolioCommand { InvestorName = "Daniel" };
            var createResponse = await _client.PostAsJsonAsync("/api/portfolio", command);
            var created = await TryReadJsonAsync<PortfolioSummaryDTO>(createResponse);

            var response = await _client.GetAsync($"/api/portfolio/{created!.PortfolioId}/operations");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var operations = await TryReadJsonAsync<IEnumerable<OperationDTO>>(response);
            Assert.NotNull(operations);
        }

        [Fact]
        public async Task CalculateTax_ShouldReturnOk()
        {
            var command = new CreatePortfolioCommand { InvestorName = "Eva" };
            var createResponse = await _client.PostAsJsonAsync("/api/portfolio", command);
            var created = await TryReadJsonAsync<PortfolioSummaryDTO>(createResponse);

            var response = await _client.GetAsync($"/api/portfolio/{created!.PortfolioId}/tax");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var portfolio = await TryReadJsonAsync<PortfolioSummaryDTO>(response);
            Assert.NotNull(portfolio);
        }
    }
}