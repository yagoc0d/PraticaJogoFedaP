
using TrabalhoPratico.Models;
namespace TrabalhoPratico.Game
{
    public class Palpite
    {
        public Jogador jogador;
        public int palpite;
        public Palpite(Jogador jogador, int palpite)
        {
            this.jogador = jogador;
            this.palpite = palpite;
        }
    }
}
