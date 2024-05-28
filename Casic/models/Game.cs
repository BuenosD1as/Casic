using System.Linq;

namespace Casic
{
    public class Game
    {
        private Sector[] sectors;

        public Game()
        {
            sectors = new Sector[]
            {
                new Sector("1$", 2), new Sector("2$", 3), new Sector("5$", 4), new Sector("10$", 5), new Sector("1$", 2), new Sector("1$", 2),
                new Sector("5$", 4), new Sector("2$", 3), new Sector("1$", 2), new Sector("1$", 2), new Sector("10$", 5), new Sector("2$", 3),
                new Sector("1$", 2), new Sector("1$", 2), new Sector("5$", 4), new Sector("2$", 3), new Sector("1$", 2), new Sector("1$", 2),
                new Sector("20$", 6), new Sector("2$", 3), new Sector("1$", 2), new Sector("1$", 2), new Sector("5$", 4), new Sector("2$", 3),
                new Sector("1$", 2), new Sector("1$", 2), new Sector("10$", 5), new Sector("2$", 3), new Sector("1$", 2), new Sector("1$", 2),
                new Sector("5$", 4), new Sector("2$", 3), new Sector("1$", 2), new Sector("1$", 2), new Sector("20$", 6), new Sector("2$", 3),
                new Sector("1$", 2), new Sector("1$", 2), new Sector("5$", 4), new Sector("2$", 3), new Sector("1$", 2), new Sector("1$", 2),
                new Sector("Joker", 10), new Sector("1$", 2), new Sector("10$", 5), new Sector("1$", 2), new Sector("2$", 3), new Sector("Joker", 10)
            };
        }

        public Sector GetSectorBySymbol(string symbol)
        {
            return sectors.FirstOrDefault(sector => sector.Symbol == symbol);
        }
    }
}
