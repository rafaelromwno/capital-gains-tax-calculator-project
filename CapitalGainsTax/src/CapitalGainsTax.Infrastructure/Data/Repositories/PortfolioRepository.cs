using CapitalGainsTax.Application.Interfaces;
using CapitalGainsTax.Domain.Entities;
using CapitalGainsTax.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CapitalGainsTax.Infrastructure.Data.Repositories
{
    internal class PortfolioRepository : IPortfolioRepository
    {
        private readonly CapitalGainsContext _context;

        public PortfolioRepository(CapitalGainsContext context)
        {
            _context = context;
        }

        public async Task<Portfolio> GetByIdAsync(int id)
        {
            return await _context.Portfolios
                .Include(p => p.Operations)
                .FirstOrDefaultAsync(p => p.PortfolioId == id);
        }

        public async Task AddAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Portfolio portfolio)
        {
            _context.Portfolios.Update(portfolio);
            await _context.SaveChangesAsync();
        }
    }
}