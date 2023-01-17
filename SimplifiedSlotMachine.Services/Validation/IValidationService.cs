namespace SimplifiedSlotMachine.Services.Validation
{
    public interface IValidationService
    {
        public bool ValidateStake(string stake, decimal balance);

        public bool ValidateDeposit(string deposit);
    }
}
