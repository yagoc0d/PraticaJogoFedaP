using System;
namespace TrabalhoPratico.Models
{
    public class FilaPlayer
    {
        private Jogador[] array;
        private int primeiro, ultimo;
        public FilaPlayer() { Inicializar(5); }
        public FilaPlayer(int tamanho)
        {
            Inicializar(tamanho);
        }
        public void Inicializar(int tamanho)
        {
            array = new Jogador[tamanho + 1];
            primeiro = ultimo = 0;
        }
        public void Inserir(Jogador player)
        {
            if (((ultimo + 1) % array.Length == primeiro))
            {
                throw new Exception("Erro");
            }
            array[ultimo] = player;
            ultimo = (ultimo + 1) % array.Length;
        }
        public Jogador Remover()
        {
            if (primeiro == ultimo)
            {
                throw new Exception("Erro");
            }
            Jogador resp = array[primeiro];
            primeiro = (primeiro + 1) % array.Length;
            return resp;

        }

    }
}
