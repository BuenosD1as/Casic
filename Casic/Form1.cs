using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Casic
{
    public partial class Form1 : Form
    {
        private Game game;
        private Player player;
        private PictureBox tablePictureBox;
        private Dictionary<string, Rectangle> betRectangles;
        private Dictionary<string, double> bets;

        public Form1()
        {
            InitializeComponent();
            game = new Game();
            player = new Player(1000);
            bets = new Dictionary<string, double>
            {
                { "1$", 0 },
                { "2$", 0 },
                { "5$", 0 },
                { "10$", 0 },
                { "20$", 0 },
                { "Joker", 0 }
            };
            UpdateBalanceLabel();
            wheelControl.SpinCompleted += WheelControl_SpinCompleted;

            InitializeTable();
        }

        private void InitializeTable()
        {
            tablePictureBox = new PictureBox
            {
                Image = Image.FromFile("C:\\Casic\\Casic\\Casic\\Properties\\Resources\\table.png"),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(20, 200), 
                Size = new Size(500, 500) 
            };
            tablePictureBox.Paint += TablePictureBox_Paint;
            tablePictureBox.MouseClick += TablePictureBox_MouseClick;
            this.Controls.Add(tablePictureBox);

            betRectangles = new Dictionary<string, Rectangle>
    {
        { "1$", new Rectangle(140, 180, 60, 60) }, 
        { "2$", new Rectangle(230, 180, 60, 60) },
        { "5$", new Rectangle(320, 180, 60, 60) },
        { "10$", new Rectangle(140, 270, 60, 60) },
        { "20$", new Rectangle(230, 270, 60, 60) },
        { "Joker", new Rectangle(320, 270, 60, 60) }
    };
        }

        private void TablePictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            using (Font font = new Font("Arial", 12, FontStyle.Bold))
            using (Brush brush = new SolidBrush(Color.Black))
            using (StringFormat format = new StringFormat())
            {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                foreach (var kvp in betRectangles)
                {
                    g.DrawRectangle(Pens.Black, kvp.Value);
                    g.DrawString(kvp.Key, font, brush, kvp.Value, format);
                    string betAmount = $"${bets[kvp.Key]:F2}";
                    g.DrawString(betAmount, font, Brushes.Red, kvp.Value.X + kvp.Value.Width / 2, kvp.Value.Y + kvp.Value.Height - 15, format);
                }
            }
        }

        private void TablePictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (var kvp in betRectangles)
            {
                if (kvp.Value.Contains(e.Location))
                {
                    string symbol = kvp.Key;
                    string input = ShowInputDialog($"Enter your bet amount for {symbol}:");
                    if (double.TryParse(input, out double betAmount) && betAmount > 0)
                    {
                        if (betAmount > player.Balance)
                        {
                            MessageBox.Show($"You don't have enough balance to place the bet on {symbol}.");
                            return;
                        }

                        bets[symbol] = betAmount;
                        tablePictureBox.Invalidate();

                        var sector = game.GetSectorBySymbol(symbol);
                        if (sector != null)
                        {
                            player.MakeBets(new List<Bet> { new Bet(betAmount, sector) });
                            UpdateBalanceLabel();
                        }
                    }
                }
            }
        }

        private string ShowInputDialog(string text)
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Place Bet",
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox inputBox = new TextBox() { Left = 50, Top = 50, Width = 300 };
            Button confirmation = new Button() { Text = "Ok", Left = 250, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : "";
        }

        private void spinButton_Click(object sender, EventArgs e)
        {
            if (!player.CurrentBets.Any())
            {
                MessageBox.Show("Please place at least one bet before spinning.");
                return;
            }
            wheelControl.SpinWheel();
        }

        private void WheelControl_SpinCompleted(object sender, SpinEventArgs e)
        {
            string message;
            bool isWin = false;

            if (player.ConfirmWinning(e.WinningSymbol))
            {
                double payout = game.GetSectorBySymbol(e.WinningSymbol).Payout * player.CurrentBets.First(bet => bet.Sector.Symbol == e.WinningSymbol).Amount;
                player.AddBalance(payout);
                message = $"You won! The wheel landed on {e.WinningSymbol}. Your new balance is ${player.Balance:F2}";
                isWin = true;
            }
            else
            {
                message = $"You lost! The wheel landed on {e.WinningSymbol}. Your new balance is ${player.Balance:F2}";
            }

            Console.WriteLine($"Debug: SpinCompleted - WinningSymbol: {e.WinningSymbol}");
            MessageBox.Show(message);

            if (isWin)
            {
                
                foreach (var key in bets.Keys.ToList())
                {
                    bets[key] = 0;
                }
                player.ClearBets();
                tablePictureBox.Invalidate();
            }

            UpdateBalanceLabel();
        }

        private void UpdateBalanceLabel()
        {
            balanceLabel.Text = $"Balance: ${player.Balance:F2}";
        }
    }
}
