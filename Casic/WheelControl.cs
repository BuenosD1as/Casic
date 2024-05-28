using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Casic
{
    public class WheelControl : Control
    {
        private const int NumSectors = 54;
        private static readonly string[] Symbols = new string[]
        {
            "1$", "1$", "1$", "1$", "1$", "1$", "1$", "1$", "1$", "1$", "1$", "1$", "1$", "1$", "1$", "1$", "1$", "1$", "1$", "1$", "1$", "1$", "1$", "1$",
            "2$", "2$", "2$", "2$", "2$", "2$", "2$", "2$", "2$", "2$", "2$", "2$", "2$", "2$", "2$",
            "5$", "5$", "5$", "5$", "5$", "5$", "5$",
            "10$", "10$", "10$", "10$",
            "20$", "20$",
            "Joker", "Joker"
        };

        private static readonly string[] ShuffledSymbols;

        static WheelControl()
        {
            Random rng = new Random();
            ShuffledSymbols = Symbols.OrderBy(x => rng.Next()).ToArray();
        }

        private float angle;
        private Timer timer;
        private float targetAngle;
        private bool spinning;
        private float sectorAngle;

        public event EventHandler<SpinEventArgs> SpinCompleted;

        public WheelControl()
        {
            this.DoubleBuffered = true;
            this.Size = new Size(800, 800); // Увеличение размера колеса
            timer = new Timer();
            timer.Interval = 20;
            timer.Tick += Timer_Tick;
            sectorAngle = 360f / NumSectors;
        }

        public void SpinWheel()
        {
            if (spinning) return;
            spinning = true;
            angle = 0;

            Random rng = new Random();
            int spins = rng.Next(5, 10); // Минимум 5 полных оборотов
            targetAngle = spins * 360 + rng.Next(360); // Добавляем случайный угол

            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (angle < targetAngle)
            {
                angle += 10; // Увеличение угла вращения
                if (angle > targetAngle)
                {
                    angle = targetAngle;
                }
                Invalidate();
            }
            else
            {
                timer.Stop();
                spinning = false;
                float finalAngle = angle % 360;
                int winningIndex = (int)((finalAngle / sectorAngle) % NumSectors);
                string winningSymbol = ShuffledSymbols[winningIndex];
                SpinCompleted?.Invoke(this, new SpinEventArgs(winningSymbol));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            float currentAngle = angle;

            for (int i = 0; i < NumSectors; i++)
            {
                using (Brush brush = new SolidBrush(i % 2 == 0 ? Color.White : Color.LightGray))
                {
                    g.FillPie(brush, 0, 0, ClientSize.Width, ClientSize.Height, currentAngle, sectorAngle);
                }
                g.DrawPie(Pens.Black, 0, 0, ClientSize.Width, ClientSize.Height, currentAngle, sectorAngle);
                float midAngle = currentAngle + sectorAngle / 2;
                DrawSymbol(g, ShuffledSymbols[i], midAngle);
                currentAngle += sectorAngle;
            }

            // Рисуем стрелку
            DrawArrow(g);
        }

        private void DrawSymbol(Graphics g, string symbol, float midAngle)
        {
            float radius = Math.Min(ClientSize.Width, ClientSize.Height) / 2f;
            float angleInRadians = midAngle * (float)Math.PI / 180;
            float x = ClientSize.Width / 2f + (float)Math.Cos(angleInRadians) * radius * 0.75f;
            float y = ClientSize.Height / 2f + (float)Math.Sin(angleInRadians) * radius * 0.75f;

            using (Font font = new Font("Arial", 12, FontStyle.Bold))
            using (Brush brush = new SolidBrush(Color.Black))
            using (StringFormat format = new StringFormat())
            {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                g.DrawString(symbol, font, brush, x, y, format);
            }
        }

        private void DrawArrow(Graphics g)
        {
            float arrowSize = 40; // Увеличение размера стрелки
            PointF[] arrowPoints = new PointF[]
            {
                new PointF(ClientSize.Width / 2f - arrowSize / 2, 0),
                new PointF(ClientSize.Width / 2f + arrowSize / 2, 0),
                new PointF(ClientSize.Width / 2f, arrowSize)
            };
            using (Brush brush = new SolidBrush(Color.Red))
            {
                g.FillPolygon(brush, arrowPoints);
            }
        }

        public static string[] GetSymbols()
        {
            return Symbols;
        }

        public static string[] GetShuffledSymbols()
        {
            return ShuffledSymbols;
        }
    }

    public class SpinEventArgs : EventArgs
    {
        public string WinningSymbol { get; }

        public SpinEventArgs(string winningSymbol)
        {
            WinningSymbol = winningSymbol;
        }
    }
}
