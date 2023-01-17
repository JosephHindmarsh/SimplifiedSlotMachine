using Microsoft.Extensions.Logging;
using SimplifiedSlotMachine.Models;

namespace SimplifiedSlotMachine.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly ILogger<BalanceService> _logger;

        public BalanceService(ILogger<BalanceService> logger) 
        { 
            _logger= logger;
        }

        /** 
            This method recieves the results from a spin and figures
            out if any of the horizontal rows are winners and adjusts the players
            balance accordingly
        **/
        public decimal CalculateBalance(List<Symbol> selectedSymbols, decimal balance, decimal stake, int symbolsPerRow, int rows)
        {
            _logger.LogInformation("Checking if any of the rows have a winning symbol combination");

            var winnings = 0m;

            for (int i = 0; i < rows; i++)
            {
                var selectedRow = selectedSymbols.Take(symbolsPerRow);
                selectedSymbols = selectedSymbols.Skip(symbolsPerRow).ToList();

                if (selectedRow.Where(s => s.Name != "*").Distinct().Count() <= 1)
                {
                    _logger.LogInformation("Winning row has been found");

                    //always round down
                    decimal multiplier = (decimal)Math.Pow(10, 2);
                    winnings += Math.Truncate(Math.Round(stake * selectedRow.Select(x => x.Coefficient).Sum(), 2) * multiplier) / multiplier;

                    balance += winnings;
                }
            }

            _logger.LogInformation("The user has won: £" + string.Format("{0:0.##}", winnings));

            Console.WriteLine("You have won: £" + string.Format("{0:0.##}", winnings));

            return balance - stake;  
        }
    }
}
