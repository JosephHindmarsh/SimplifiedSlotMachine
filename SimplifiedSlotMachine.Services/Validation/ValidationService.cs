using SimplifiedSlotMachine.Common;

namespace SimplifiedSlotMachine.Services.Validation
{
    public class ValidationService : IValidationService
    {
        public bool ValidateDeposit(string deposit)
        {
            decimal depositValue;

            try
            {
                if (!decimal.TryParse(deposit, out depositValue))
                {
                    throw new InvalidDecimalException();
                }
                if (depositValue < 0)
                {
                    throw new NegativeValueException();
                }
            }
            catch (InvalidDecimalException)
            {
                Console.WriteLine("This is an invalid number please try again. \r\n");
                return false;
            }
            catch (NegativeValueException)
            {
                Console.WriteLine("You cannot deposit a negative amount of money. \r\n");
                return false;
            }

            return true;
        }

        public bool ValidateStake(string stake, decimal balance)
        {
            decimal stakeValue;

            try
            {
                if (!decimal.TryParse(stake, out stakeValue))
                {
                    throw new InvalidDecimalException();
                }
                if (stakeValue < 0)
                {
                    throw new NegativeValueException();
                }
                if (stakeValue > balance)
                {
                    throw new StakeGreaterThanBalanceException();
                }
            }
            catch (InvalidDecimalException)
            {
                Console.WriteLine("This is an invalid number please try again. \r\n");
                return false;
            }
            catch (NegativeValueException)
            {
                Console.WriteLine("You cannot stake a negative amount of money. \r\n");
                return false;
            }
            catch (StakeGreaterThanBalanceException)
            {
                Console.WriteLine("You cannot have a stake greater than your balance. \r\n");
                return false;
            }

            return true;
        }
    }
}
