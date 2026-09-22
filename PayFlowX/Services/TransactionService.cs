using PayFlowX.Data;
using PayFlowX.Models;
using PayFlowX.Repositories;

namespace PayFlowX.Services
{
    public class TransactionService : ITransactionService


    {


        private readonly ITransactionRepository _repository;
        public TransactionService(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Transaction> CreateAsync(Transaction transaction)
        {


            return await _repository.AddAsync(transaction);

        }

        public async Task<bool> DeleteAsync(int id)
        {

            
            var hold= await _repository.GetByIdAsync(id);
            if (hold != null) 
            {

              await  _repository.DeleteAsync(hold);

                return true;
            }
            return false;


        }

        public async Task<List<Transaction>> GetAllAsync()
        {
            return await _repository.GetAllAsync();

        }

        public async Task<Transaction?> GetByIdAsync(int id)
        {

            return await _repository.GetByIdAsync(id);

        }

        public async Task<bool> UpdateAsync(int id, Transaction transaction)
        {
            var hold = await _repository.GetByIdAsync(id);

            if (hold != null )
            {
                
                hold.Amount = transaction.Amount;
                hold.Reference = transaction.Reference;
                hold.Status = transaction.Status;
                hold.Currency = transaction.Currency;

                await _repository.UpdateAsync(hold);

                return true;
            }

            return false;
        }
    }
}
