using Microsoft.AspNetCore.Mvc;
using Moq;
using PayFlowX.Controllers;
using PayFlowX.Models;
using PayFlowX.Services;

namespace PayFlowX.Tests
{
    public class TransactionsControllerTests
    {
        // =========================
        // GET ALL TEST
        // =========================
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


        // =========================
        // POST TEST
        // =========================
        [Fact]
        public async Task PostRecods_ShouldCreateTransaction_AndReturnOk()
        {
            var mockService = new Mock<ITransactionService>();

            var controller =
                new TransactionsController(mockService.Object);

            var dto = new CreateTransactionDto
            {
                Amount = 500,
                Currency = "GBP"
            };

            var result = await controller.PostRecods(dto);

            var okResult =
                Assert.IsType<OkObjectResult>(result);

            var transaction =
                Assert.IsType<Transaction>(okResult.Value);

            Assert.Equal(500, transaction.Amount);
            Assert.Equal("GBP", transaction.Currency);
            Assert.Equal("Active", transaction.Status);

            mockService.Verify(
                x => x.CreateAsync(
                    It.Is<Transaction>(t =>
                        t.Amount == 500 &&
                        t.Currency == "GBP" &&
                        t.Status == "Active"
                    )
                ),
                Times.Once
            );
        }


        // =========================
        // DELETE TEST
        // =========================
        [Fact]
        public async Task DeleteRecords_ShouldCallDelete_AndReturnOk()
        {
            var mockService = new Mock<ITransactionService>();

            var controller =
                new TransactionsController(mockService.Object);

            int transactionId = 10;

            var result =
                await controller.DeleteRecords(transactionId);

            Assert.IsType<OkResult>(result);

            mockService.Verify(
                x => x.DeleteAsync(transactionId),
                Times.Once
            );
        }


        // =========================
        // UPDATE TEST
        // =========================
        [Fact]
        public async Task UpdateRecords_ShouldCallUpdate_AndReturnOk()
        {
            var mockService = new Mock<ITransactionService>();

            var controller =
                new TransactionsController(mockService.Object);

            int transactionId = 5;

            var updatedTransaction = new Transaction
            {
                Amount = 900,
                Currency = "USD",
                Status = "Active"
            };

            var result =
                await controller.UpdateRecords(
                    transactionId,
                    updatedTransaction
                );

            var okResult =
                Assert.IsType<OkObjectResult>(result);

            Assert.Equal(true, okResult.Value);

            mockService.Verify(
                x => x.UpdateAsync(
                    transactionId,
                    updatedTransaction
                ),
                Times.Once
            );
        }
    }
}