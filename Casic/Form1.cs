using System;
using System.Linq;
using System.Windows.Forms;

namespace Casic
{
    public partial class Form1 : Form
    {
        private Game game;
        private Player player;

        public Form1()
        {
            InitializeComponent();
            game = new Game();
            player = new Player(1000);
            UpdateBalanceLabel();
            wheelControl.SpinCompleted += WheelControl_SpinCompleted;
        }

        private void spinButton_Click(object sender, EventArgs e)
        {
            double betAmount;
            if (!double.TryParse(betAmountTextBox.Text, out betAmount) || betAmount <= 0)
            {
                MessageBox.Show("Please enter a valid bet amount.");
                return;
            }

            var selectedSymbols = betSymbolComboBox.CheckedItems.Cast<string>().ToList();
            if (!selectedSymbols.Any())
            {
                MessageBox.Show("Please select at least one symbol to bet on.");
                return;
            }

            var selectedSectors = selectedSymbols.Select(symbol => game.GetSectorBySymbol(symbol)).ToList();
            if (selectedSectors.Any(sector => sector == null))
            {
                MessageBox.Show("Invalid symbol selected.");
                return;
            }

            var bets = selectedSectors.Select(sector => new Bet(betAmount, sector)).ToList();
            player.MakeBets(bets);
            UpdateBalanceLabel();

            wheelControl.SpinWheel();
        }

        private void WheelControl_SpinCompleted(object sender, SpinEventArgs e)
        {
            string message;

            if (player.ConfirmWinning(e.WinningSymbol))
            {
                double payout = game.GetSectorBySymbol(e.WinningSymbol).Payout * player.CurrentBets.First(bet => bet.Sector.Symbol == e.WinningSymbol).Amount;
                player.AddBalance(payout);
                message = $"You won! The wheel landed on {e.WinningSymbol}. Your new balance is ${player.Balance:F2}";
            }
            else
            {
                message = $"You lost! The wheel landed on {e.WinningSymbol}. Your new balance is ${player.Balance:F2}";
            }

            UpdateBalanceLabel();
            MessageBox.Show(message);
        }

        private void UpdateBalanceLabel()
        {
            balanceLabel.Text = $"Balance: ${player.Balance:F2}";
        }
    }
}

