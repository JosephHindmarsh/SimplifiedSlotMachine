using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SimplifiedSlotMachine.Models;
using SimplifiedSlotMachine.Services;
using SimplifiedSlotMachine.Services.Validation;

namespace SimplifiedSlothMachineTests
{
    public class SlotServiceTests
    {
        private readonly ISlotService _slotService;
        private readonly Mock<ILogger<SlotService>> _logger;
        private readonly Mock<IValidationService> _validationService;
        private readonly Mock<IBalanceService> _balanceService;
        private readonly Mock<IConfiguration> _configuration;

        public SlotServiceTests() 
        { 
            _logger= new Mock<ILogger<SlotService>>();
            _validationService = new Mock<IValidationService>();
            _balanceService= new Mock<IBalanceService>();  
            _configuration= new Mock<IConfiguration>();

            _slotService= new SlotService(_logger.Object, _validationService.Object, _balanceService.Object, _configuration.Object);
        }

        [Fact]
        public void GenerateSymbol_ReturnsValidResponse()
        {
            //Arrange
            var possibleSymbols = new List<Symbol>()
            { 
                new Symbol { Name = "A" , Coefficient = 0.4m, Probability = 0.45 },
                new Symbol { Name = "B" , Coefficient = 0.4m, Probability = 0.35 },
                new Symbol { Name = "P" , Coefficient = 0.4m, Probability = 0.15 },
                new Symbol { Name = "*" , Coefficient = 0.4m, Probability = 0.05 }
            };

            //Act
            var symbol = _slotService.GenerateSymbol(possibleSymbols);

            //Assert
            Assert.NotNull(symbol);
            Assert.IsType<Symbol>(symbol);
        }
}
}
