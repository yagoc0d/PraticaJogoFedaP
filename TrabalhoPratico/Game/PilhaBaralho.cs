using System;
using TrabalhoPratico.Models;
namespace TrabalhoPratico.Game
{
    public class PilhaBaralho
    {
        private CelulaCarta topo;
        public PilhaBaralho()
        {
            topo = null;
        }
        public void Inserir(Carta carta)
        {
            CelulaCarta tmp = new CelulaCarta(carta);
            tmp.Prox = topo;
            topo = tmp;
            tmp = null;

        }
        public Carta Remover()
        {
            if (topo == null)
                throw new Exception("Baralho Vazio");
            Carta elemento = topo.Elemento;
            CelulaCarta tmp = topo;
            topo = topo.Prox;
            tmp.Prox = null;
            tmp = null;
            return elemento;
        }
    }
}
