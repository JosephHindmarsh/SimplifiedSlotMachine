namespace SimplifiedSlotMachine.Models
{
    public class User
    {
        //Not used but in a future implementation could look to store who is playing etc to keep track of multiple players
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
    }
}
