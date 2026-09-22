using PayFlowX.Models;

namespace PayFlowX.Services
{
    public interface ITransactionService


    {


        Task<List<Transaction>> GetAllAsync();
        Task<Transaction?> GetByIdAsync(int id);

        Task<Transaction> CreateAsync(Transaction transaction);

        Task<bool> UpdateAsync(int id, Transaction transaction);

        Task<bool> DeleteAsync(int id);





    }
}
