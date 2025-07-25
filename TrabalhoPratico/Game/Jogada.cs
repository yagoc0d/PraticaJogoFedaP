
using TrabalhoPratico.Models;
namespace TrabalhoPratico.Game
{
    public class Jogada
    {
        public Jogador jogador;
        public Carta carta;
        public Jogada(Jogador jogador, Carta carta)
        {
            this.jogador = jogador;
            this.carta = carta;
        }
    }
}
