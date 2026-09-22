using PayFlowX.Models;

namespace PayFlowX.Repositories
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetAllAsync();

        Task<Transaction?> GetByIdAsync(int id);

        Task<Transaction> AddAsync(Transaction transaction);

        Task UpdateAsync(Transaction transaction);

        Task DeleteAsync(Transaction transaction);
    }
}