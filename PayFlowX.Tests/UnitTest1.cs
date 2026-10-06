using Microsoft.AspNetCore.Mvc;
using Moq;
using PayFlowX.Controllers;
using PayFlowX.Models;
using PayFlowX.Services;

namespace PayFlowX.Tests
{
    public class TransactionsControllerTests
    {
        [Fact]
        public async Task GetAll_ShouldReturnOk_WithTransactions()
        {
            var fakeTransactions = new List<Transaction>
            {
                new Transaction
                {
                    Amount = 100,
                    Currency = "GBP",
                    Status = "Active"
                },
                new Transaction
                {
                    Amount = 200,
                    Currency = "USD",
                    Status = "Active"
                }
            };

            var mockService = new Mock<ITransactionService>();

            mockService
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(fakeTransactions);

            var controller =
                new TransactionsController(mockService.Object);

            var result = await controller.GetAll();

            var okResult =
                Assert.IsType<OkObjectResult>(result);

            var transactions =
                Assert.IsAssignableFrom<IEnumerable<Transaction>>(
                    okResult.Value
                );

            Assert.Equal(2, transactions.Count());
        }
    }
}