using SimplifiedSlotMachine.Models;

namespace SimplifiedSlotMachine.Services
{
    public interface ISlotService
    {
        public void Play(User user);
        public Symbol GenerateSymbol(List<Symbol>? possibleSymbols);
    }
}
