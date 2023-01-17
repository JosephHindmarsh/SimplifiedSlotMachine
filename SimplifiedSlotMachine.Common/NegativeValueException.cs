namespace SimplifiedSlotMachine.Common
{
    public class NegativeValueException : Exception
    {
        public decimal Value { get; init; }
    }
}