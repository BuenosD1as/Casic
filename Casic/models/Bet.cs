namespace Casic
{
    public class Bet
    {
        public double Amount { get; }
        public Sector Sector { get; }

        public Bet(double amount, Sector sector)
        {
            Amount = amount;
            Sector = sector;
        }
    }
}
