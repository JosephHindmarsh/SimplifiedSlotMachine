using SimplifiedSlotMachine.Models;

namespace SimplifiedSlotMachine.Services
{
    public interface IBalanceService
    {
        public decimal CalculateBalance(List<Symbol> selectedSymbols, decimal balance, decimal stake, int symbolsPerRow, int rows);
    }
}
