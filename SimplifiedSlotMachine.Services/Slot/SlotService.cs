using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SimplifiedSlotMachine.Models;
using SimplifiedSlotMachine.Services.Validation;

namespace SimplifiedSlotMachine.Services
{
    public class SlotService : ISlotService
    {
        private readonly ILogger<SlotService> _logger;
        private readonly IValidationService _validationService;
        private readonly IBalanceService _balanceService;
        private readonly IConfiguration _configuration;

        public SlotService(ILogger<SlotService> logger, IValidationService validationService, IBalanceService balanceService, IConfiguration configuration)
        {
            _logger = logger;
            _validationService = validationService;
            _balanceService = balanceService;
            _configuration = configuration;
        }

        public void Play(User user)
        {
            Console.WriteLine("Please deposit money you would like to play with: ");
            var input = Console.ReadLine();
            var isValidDeposit = _validationService.ValidateDeposit(input);

            while (!isValidDeposit)
            {
                Console.WriteLine("Please enter a valid deposit amount you would like to play with: ");
                isValidDeposit = _validationService.ValidateDeposit(input);
            }

            user.Balance = decimal.Parse(input);

            while (user.Balance > 0)
            {
                Console.WriteLine("Enter stake amount: ");
                input = Console.ReadLine();
                var isValidStake = _validationService.ValidateStake(input, user.Balance);

                while (!isValidStake)
                {
                    Console.WriteLine("Please enter a valid stake amount: ");
                    input = Console.ReadLine();
                    isValidStake = _validationService.ValidateStake(input, user.Balance);
                }

                var stake = decimal.Parse(input);

                List<Symbol> selectedSymbols = new List<Symbol>();

                for (int s = 0; s < _configuration.GetValue<int>("Rows"); s++)
                {
                    for (int i = 0; i < _configuration.GetValue<int>("SymbolsPerRow"); i++)
                    {
                        _logger.LogInformation("Calling SlotService to generate rows of symbols");

                        var currentSymbol = GenerateSymbol(_configuration.GetSection("Symbols").Get<List<Symbol>>());
                        selectedSymbols.Add(currentSymbol);
                        Console.Write(currentSymbol.Name);
                    }
                    Console.WriteLine();
                }

                Console.WriteLine();

                _logger.LogInformation("Sending results off to the BalanceService to calculate winnings");

                user.Balance = _balanceService.CalculateBalance(selectedSymbols, user.Balance, stake, _configuration.GetValue<int>("SymbolsPerRow"), _configuration.GetValue<int>("Rows"));

                Console.WriteLine("Current Balance is: £" + string.Format("{0:0.##}", user.Balance));
                Console.WriteLine();
            }
        }

        /** 
            This method recieves a list of symbols from the appsettings
            and generates a symbol from this list taking into account the probability
        **/
        public Symbol GenerateSymbol(List<Symbol>? possibleSymbols)
        {
            _logger.LogInformation("Generating a symbol from the list of possible symbols for the row");

            if (possibleSymbols != null)
            {
                var rnd = new Random();

                var totalWeight = possibleSymbols.Sum(x => x.Probability);

                var num = rnd.NextDouble() * totalWeight;

                foreach (Symbol symbol in possibleSymbols)
                {
                    if (num < symbol.Probability)
                    {
                        return symbol;
                    }
                    num -= symbol.Probability;
                }
            }
            else 
            {
                _logger.LogError("An issue has occurred that has caused null to be returned from GenerateSymbol");
            }

            return null;
        }
    }
}
