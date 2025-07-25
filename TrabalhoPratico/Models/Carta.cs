
namespace TrabalhoPratico.Models
{
    public class Carta
    {
        public Naipe Naipe;
        public string Valor;
        public int Peso;
        private int idCarta;
        public Carta(Naipe Naipe, string Valor, int Peso)
        {
            this.Naipe = Naipe;
            this.Valor = Valor;
            this.Peso = Peso;
        }
        public Carta()
        {
            this.Naipe = null;
            this.Valor = null;
            this.Peso = 0;
        }
        public int IdCarta
        {
            get { return idCarta; }
            set { idCarta = value; }
        }


    }
}
