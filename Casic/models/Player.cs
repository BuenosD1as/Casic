using System.Collections.Generic;
using System.Linq;

namespace Casic
{
    public class Player
    {
        public double Balance { get; private set; }
        public List<Bet> CurrentBets { get; private set; }

        public Player(double initialBalance)
        {
            Balance = initialBalance;
            CurrentBets = new List<Bet>();
        }

        public void MakeBets(List<Bet> bets)
        {
            double totalBetAmount = bets.Sum(bet => bet.Amount);
            if (totalBetAmount > Balance)
                throw new System.Exception("Insufficient balance to place bets");

            Balance -= totalBetAmount;
            CurrentBets = bets;
        }

        public void AddBalance(double amount)
        {
            Balance += amount;
        }

        public bool ConfirmWinning(string winningSymbol)
        {
            return CurrentBets.Any(bet => bet.Sector.Symbol == winningSymbol);
        }
    }
}
