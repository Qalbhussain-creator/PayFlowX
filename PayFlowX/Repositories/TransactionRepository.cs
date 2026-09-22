using Microsoft.EntityFrameworkCore;
using PayFlowX.Data;
using PayFlowX.Models;

namespace PayFlowX.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext DbContext;
        public TransactionRepository(ApplicationDbContext _dbContext) { 
        
        
            DbContext = _dbContext;
        }

        public async Task<Transaction> AddAsync(Transaction transaction)
        {
        
            DbContext.transactions.Add(transaction);
            await DbContext.SaveChangesAsync(); 
        
            return transaction;
        }

        public async Task DeleteAsync(Transaction transaction)
        {

            DbContext.Remove(transaction);

            await DbContext.SaveChangesAsync();
}

        public async Task<List<Transaction>> GetAllAsync()
        {

            return await DbContext.transactions.ToListAsync();

        }

        public async Task<Transaction?> GetByIdAsync(int id)
        {
        
            return await DbContext.transactions.Where(option =>option.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            
            DbContext.transactions.Update(transaction);

            await DbContext.SaveChangesAsync();

        }
    }
}
