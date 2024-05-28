namespace Casic
{
    public class Sector
    {
        public string Symbol { get; }
        public double Payout { get; }

        public Sector(string symbol, double payout)
        {
            Symbol = symbol;
            Payout = payout;
        }
    }
}
