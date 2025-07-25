
using System;
using System.Collections.Generic;
namespace TrabalhoPratico.Models
{
    public class Jogador
    {
        public string nome;
        public int posicao;
        public int vidas;
        private List<Carta> maoAtual;
        public Queue<int> rankingUltimasPartidas = new Queue<int>();


        public List<Carta> MaoAtual
        {
            get { return maoAtual; }
            set { maoAtual = value; }

        }

        public Jogador(string nome, int posicao)
        {
            this.nome = nome;
            this.posicao = posicao;
            this.vidas = 5;
        }

        public int PerdeVida()
        {
            this.vidas--;
            return this.vidas;
        }
        public void ImprimirMaoLadoALado()
        {
            if (this.MaoAtual == null || this.MaoAtual.Count == 0)
            {
                Console.WriteLine("(Mão vazia)");
                return;
            }

            List<string> linhas = new List<string>() { "", "", "", "", "", "", "", "" };

            foreach (var carta in this.MaoAtual)
            {
                string valorEsquerda = carta.Valor.PadRight(2);
                string valorDireita = carta.Valor.PadLeft(2);
                string naipe = carta.Naipe.naipe;

                linhas[0] += "┌─────────┐ ";
                linhas[1] += $"│{valorEsquerda}       │ ";
                linhas[2] += "│         │ ";
                linhas[3] += $"│    {naipe}   │ ";
                linhas[4] += "│         │ ";
                linhas[5] += $"│       {valorDireita}│ ";
                linhas[6] += "└─────────┘ ";
                linhas[7] += $"   ID: {carta.IdCarta.ToString("D2")}   ";
            }
            foreach (string linha in linhas)
            {
                Console.WriteLine(linha);
            }
        }
    }
}
