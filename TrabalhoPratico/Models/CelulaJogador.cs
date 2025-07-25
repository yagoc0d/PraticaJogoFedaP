
namespace TrabalhoPratico.Models
{
    public class CelulaJogador
    {
        private Jogador elemento;
        private CelulaJogador prox;
        public CelulaJogador(Jogador elemento)
        {
            this.elemento = elemento;
            this.prox = null;
        }
        public CelulaJogador()
        {
            this.elemento = null;
            this.prox = null;
        }
        public CelulaJogador Prox
        {
            get { return prox; }
            set { prox = value; }
        }
        public Jogador Elemento
        {
            get { return elemento; }
            set { elemento = value; }
        }
    }

}
