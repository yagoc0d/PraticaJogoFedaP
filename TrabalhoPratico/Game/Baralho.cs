using System;
using System.Collections.Generic;
using TrabalhoPratico.Models;
using TrabalhoPratico.Data;
namespace TrabalhoPratico.Game
{
    public class Baralho
    {
        public Carta[] Cartas = new Carta[40];
        public int Manilhas;
        private string[] ListaCartas = { "4", "5", "6", "7", "Q", "J", "K", "A", "2", "3" };
        public PilhaBaralho PilhaBaralho;

        public Baralho()
        {

            Naipe Paus = new Naipe(4, "♣️");
            Naipe Copas = new Naipe(3, "♥️");
            Naipe Espadas = new Naipe(2, "♠️");
            Naipe Ouros = new Naipe(1, "♦️");
            Carta Zap = new Carta(Paus, "4", 13);
            Carta seteCopas = new Carta(Copas, "7", 12);
            Carta Espadilha = new Carta(Espadas, "A", 11);
            Carta SeteOuros = new Carta(Ouros, "7", 10);
            Naipe[] arrayNaipe = new Naipe[4];

            arrayNaipe[0] = Paus;
            arrayNaipe[1] = Copas;
            arrayNaipe[2] = Espadas;
            arrayNaipe[3] = Ouros;
            this.Cartas[0] = Zap;
            this.Cartas[1] = seteCopas;
            this.Cartas[2] = Espadilha;
            this.Cartas[3] = SeteOuros;
            int indiceAtual = 4;
            for (int peso = 0; peso < ListaCartas.Length; peso++)
            {
                for (int j = 0; j < arrayNaipe.Length; j++)
                {
                    Naipe naipeAtual = arrayNaipe[j];
                    string valorCartaAtual = ListaCartas[peso];
                    bool encontrado = false;
                    foreach (Carta carta in this.Cartas)
                    {
                        if (carta != null)
                        {
                            if (carta.Naipe.peso == naipeAtual.peso && valorCartaAtual == carta.Valor)
                            {
                                encontrado = true;

                                break;
                            }
                        }

                    }
                    if (!encontrado)
                    {
                        Carta novaCarta = new Carta(naipeAtual, valorCartaAtual, peso);
                        this.Cartas[indiceAtual] = novaCarta;
                        indiceAtual++;
                    }
                }
            }
            Log.Registrar("Baralho criado com 40 cartas (incluindo manilhas fixas)");

            for (int i = 0; i < Cartas.Length; i++)
            {
                if (this.Cartas[i] != null)
                {
                    this.Cartas[i].IdCarta = i;
                }
            }
        }
        public void Embaralhar()
        {
            this.PilhaBaralho = new PilhaBaralho();
            Carta[] baralhoCarta = new Carta[Cartas.Length];
            int[] posi = new int[Cartas.Length];
            for (int i = 0; i < posi.Length; i++)
            {
                posi[i] = i;
            }

            int posit = 0;
            while (posit < 40)
            {
                Random random = new Random();
                int num = random.Next(0, 40);
                for (int j = 0; j < posi.Length; j++)
                {
                    if (num == posi[j])
                    {
                        this.PilhaBaralho.Inserir(Cartas[num]);
                        baralhoCarta[num] = Cartas[posit];
                        posit++;
                        posi[j] = -1;
                    }
                }
            }
            Log.Registrar("Baralho Embaralhado.");

        }
        public List<Carta> DarCartas(int quantidadeCartas)
        {
            List<Carta> cartas = new List<Carta>();
            for (int i = 0; i < quantidadeCartas; i++)
            {
                cartas.Add(this.PilhaBaralho.Remover());
            }
            return cartas;
        }
        public void ImprimirMaoLadoALado(List<Carta> cartas)
        {
            if (cartas == null || cartas.Count == 0)
            {
                Console.WriteLine("(Mão vazia)");
                return;
            }

            List<string> linhas = new List<string>() { "", "", "", "", "", "", "", "" };

            foreach (var carta in cartas)
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
