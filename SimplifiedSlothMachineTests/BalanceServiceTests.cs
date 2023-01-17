using Microsoft.Extensions.Logging;
using Moq;
using SimplifiedSlotMachine.Models;
using SimplifiedSlotMachine.Services;

namespace SimplifiedSlothMachineTests
{
    public class BalanceServiceTests
    {
        private readonly IBalanceService _balanceService;
        private readonly Mock<ILogger<BalanceService>> _logger;

        public BalanceServiceTests()
        {
            _logger = new Mock<ILogger<BalanceService>>();
            _balanceService = new BalanceService(_logger.Object);
        }

        [Fact]
        public void CalculateBalance_AAA_ReturnsWin()
        {
            //Arrange
            var symbol = new Symbol { Name = "A", Coefficient = 0.4m, Probability = 0.45 };

            var rowOfSymbols = new List<Symbol>
            {
                symbol,
                symbol,
                symbol
            };

            //Act
            var result = _balanceService.CalculateBalance(rowOfSymbols, 10, 5, 3, 1);

            //Assert
            Assert.IsType<decimal>(result);
            Assert.Equal(11, result);
        }

        [Fact]
        public void CalculateBalance_BBB_ReturnsWin()
        {
            //Arrange
            var symbol = new Symbol { Name = "B", Coefficient = 0.6m, Probability = 0.35 };

            var rowOfSymbols = new List<Symbol>
            {
                symbol,
                symbol,
                symbol
            };

            //Act
            var result = _balanceService.CalculateBalance(rowOfSymbols, 10, 5, 3, 1);

            //Assert
            Assert.IsType<decimal>(result);
            Assert.Equal(14, result);
        }

        [Fact]
        public void CalculateBalance_PPP_ReturnsWin()
        {
            //Arrange
            var symbol = new Symbol { Name = "P", Coefficient = 0.8m, Probability = 0.15 };

            var rowOfSymbols = new List<Symbol>
            {
                symbol,
                symbol,
                symbol
            };

            //Act
            var result = _balanceService.CalculateBalance(rowOfSymbols, 10, 5, 3, 1);

            //Assert
            Assert.IsType<decimal>(result);
            Assert.Equal(17, result);
        }

        [Fact]
        public void CalculateBalance_Wildcard_ReturnsWin()
        {
            //Arrange
            var symbol = new Symbol { Name = "P", Coefficient = 0.8m, Probability = 0.15 };
            var wildcard = new Symbol { Name = "*", Coefficient = 0m, Probability = 0.05 };

            var rowOfSymbols = new List<Symbol>
            {
                symbol,
                symbol,
                wildcard
            };

            //Act
            var result = _balanceService.CalculateBalance(rowOfSymbols, 10, 5, 3, 1);

            //Assert
            Assert.IsType<decimal>(result);
            Assert.Equal(13, result);
        }
    }
}