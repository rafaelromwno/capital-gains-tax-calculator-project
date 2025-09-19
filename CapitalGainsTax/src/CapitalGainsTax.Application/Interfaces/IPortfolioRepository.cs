using CapitalGainsTax.Domain.Entities;

namespace CapitalGainsTax.Application.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<Portfolio> GetByIdAsync(int id);
        Task AddAsync(Portfolio portfolio);
        Task UpdateAsync(Portfolio portfolio);
    }
}