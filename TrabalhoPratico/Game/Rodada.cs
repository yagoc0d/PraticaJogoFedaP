using System;
using System.Collections.Generic;
using TrabalhoPratico.Data;
using TrabalhoPratico.Models;
using System.Threading;
namespace TrabalhoPratico.Game
{
    public class Rodada
    {
        FilaPlayer filaPlayer;
        int quantidadeJogadores;
        List<Jogada> jogadas = new List<Jogada>();
        public Jogador ganhador;
        public Rodada(FilaPlayer filaPlayer, int quantidadeJogadores)
        {
            this.filaPlayer = filaPlayer;
            this.quantidadeJogadores = quantidadeJogadores;
        }
        public void TempoParaLimpar()
        {
            Thread.Sleep(3000);
            Console.Clear();
        }
        public void IniciaRodada()
        {
            for (int i = 0; i < quantidadeJogadores; i++)
            {
                Jogador jogador = filaPlayer.Remover();
                jogador.ImprimirMaoLadoALado();
                Console.WriteLine($"{jogador.nome}, digite o ID da carta que quer jogar:");
                int idCarta = int.Parse(Console.ReadLine());
                bool contemCarta = false;
                Carta carta = new Carta();
                for (int j = 0; j < jogador.MaoAtual.Count; j++)
                {
                    if (jogador.MaoAtual[j].IdCarta == idCarta)
                    {
                        contemCarta = true;
                        carta = jogador.MaoAtual[j];
                    }
                }
                while (!contemCarta)
                {
                    Console.WriteLine("Você não possui uma carta com esse ID. Digite novamente:");
                    idCarta = int.Parse(Console.ReadLine());

                    for (int j = 0; j < jogador.MaoAtual.Count; j++)
                    {
                        if (jogador.MaoAtual[j].IdCarta == idCarta)
                        {
                            contemCarta = true;
                            carta = jogador.MaoAtual[j];
                        }
                    }
                }
                Log.Registrar($"{jogador.nome} jogou a carta {carta.Valor}{carta.Naipe.naipe} (ID {carta.IdCarta}).");
                jogador.MaoAtual.Remove(carta);
                Jogada jogada = new Jogada(jogador, carta);
                jogadas.Add(jogada);
                filaPlayer.Inserir(jogador);
            }
            SetGanhador();
            TempoParaLimpar();
        }
        public void SetGanhador()
        {
            if (jogadas.Count == 0) return;
            Jogada jogadaGanhadora = jogadas[0];
            for (int i = 1; i < jogadas.Count; i++)
            {
                if (jogadas[i].carta.Peso > jogadaGanhadora.carta.Peso)
                {
                    jogadaGanhadora = jogadas[i];
                }
                else if (jogadas[i].carta.Peso == jogadaGanhadora.carta.Peso)
                {
                    if (jogadas[i].carta.Naipe.peso > jogadaGanhadora.carta.Naipe.peso)
                    {
                        jogadaGanhadora = jogadas[i];
                    }
                }
            }
            Jogador ganhadorTemp = jogadaGanhadora.jogador;
            Jogador ganhadorOriginal = null;
            int total = quantidadeJogadores;
            for (int i = 0; i < total; i++)
            {
                Jogador jogadorFila = filaPlayer.Remover();
                if (jogadorFila == ganhadorTemp)
                {
                    ganhadorOriginal = jogadorFila;
                }
                filaPlayer.Inserir(jogadorFila);
            }
            if (ganhadorOriginal != null)
            {
                this.ganhador = ganhadorOriginal;
                Console.WriteLine($"O ganhador da rodada é {ganhadorOriginal.nome}");
                Log.Registrar($"Rodada vencida por {ganhadorOriginal.nome}.");
            }
            else
            {
                this.ganhador = ganhadorTemp;
                Console.WriteLine($"O ganhador da rodada é {ganhadorTemp.nome} (sem correspondência na fila)");
            }

        }

    }

}
