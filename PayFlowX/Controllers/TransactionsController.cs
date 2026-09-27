using Microsoft.AspNetCore.Mvc;
using PayFlowX.Services;
using PayFlowX.Models;
using PayFlowX.Services ;
using Microsoft.AspNetCore.Authorization;

namespace PayFlowX.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
     //   [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        
        {
            var hold = await _transactionService.GetAllAsync();

            return Ok(hold);
        }



        [HttpPost("PostRecods")]
        public async Task<IActionResult> PostRecods([FromBody] CreateTransactionDto dd1)
        {

            var _transaction = new Transaction
            {

                Amount= dd1.Amount,
                Currency = dd1.Currency,

                Status = "Active",
            
           

            };

            await _transactionService.CreateAsync(_transaction);
            
            return  Ok(_transaction);
        }


        [HttpDelete("DeleteRecords/{id}")]
        public async Task<IActionResult> DeleteRecords(int id)
        {
            await _transactionService.DeleteAsync(id);

            return Ok();
        }


        [HttpPut("UpdateRecords/{id}")]

        public async  Task<IActionResult> UpdateRecords(int Id,[FromBody] Transaction transaction)
        {

            await _transactionService.UpdateAsync(Id, transaction);

            return Ok(true);
        }

    }
}