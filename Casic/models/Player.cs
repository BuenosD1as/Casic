using System;
using System.Collections.Generic;
using System.Linq;

namespace Casic
{
    public class Player
    {
        private double balance;
        private List<Bet> currentBets;

        public Player(double initialBalance)
        {
            balance = initialBalance;
            currentBets = new List<Bet>();
        }

        public double Balance => balance;

        public List<Bet> CurrentBets => currentBets;

        public void MakeBets(List<Bet> bets)
        {
            currentBets.AddRange(bets);
            foreach (var bet in bets)
            {
                balance -= bet.Amount;
            }
        }

        public bool ConfirmWinning(string winningSymbol)
        {
            return currentBets.Any(bet => bet.Sector.Symbol == winningSymbol);
        }

        public void AddBalance(double amount)
        {
            balance += amount;
        }

        public void ClearBets()
        {
            currentBets.Clear();
        }
    }
}
