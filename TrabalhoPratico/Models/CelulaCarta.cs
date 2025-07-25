
namespace TrabalhoPratico.Models
{
    public class CelulaCarta
    {
        private Carta elemento;
        private CelulaCarta prox;
        public CelulaCarta(Carta elemento)
        {
            this.elemento = elemento;
            this.prox = null;
        }
        public CelulaCarta()
        {
            this.elemento = null;
            this.prox = null;
        }
        public CelulaCarta Prox
        {
            get { return prox; }
            set { prox = value; }
        }
        public Carta Elemento
        {
            get { return elemento; }
            set { elemento = value; }
        }
    }

}
