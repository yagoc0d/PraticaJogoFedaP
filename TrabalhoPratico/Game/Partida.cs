using System;
using System.Collections.Generic;
using System.Linq;
using TrabalhoPratico.Data;
using TrabalhoPratico.Models;
namespace TrabalhoPratico.Game
{
    public class Partida
    {
        public int idPartida;
        public int posicaoPrimeiroJogador;
        public int quantidadeRodadas;
        public int quantidadeJogadores;
        public List<Jogador> jogadores;
        public FilaPlayer filaPlayer;
        public List<Palpite> palpites = new List<Palpite>();
        public List<Rodada> rodadas = new List<Rodada>();
        List<Jogador> ganhadores = new List<Jogador>();

        public Partida(int quantidadeRodadasTotal, FilaPlayer filaPlayer, int quantidadeJogadores, List<Jogador> jogadores)
        {
            this.quantidadeRodadas = quantidadeRodadasTotal;
            this.filaPlayer = filaPlayer;
            this.quantidadeJogadores = quantidadeJogadores;
            this.jogadores = jogadores;

            SolicitarPalpite();
            GerenciaRodadas();

        }
        public void IniciaRodada()
        {
            Rodada novaRodada = new Rodada(filaPlayer, quantidadeJogadores);
            novaRodada.IniciaRodada();
            this.rodadas.Add(novaRodada);

        }

        public void SolicitarPalpite()
        {
            int somaPalpites = 0;
            int valorProibido = 0;
            for (int i = 0; i < quantidadeJogadores; i++)
            {
                Jogador jogador = filaPlayer.Remover();
                bool eUltimoJogador = (i == quantidadeJogadores - 1);
                if (eUltimoJogador)
                {
                    valorProibido = quantidadeRodadas - somaPalpites;
                }
                Console.WriteLine($"{jogador.nome}, insira seu palpite: quantas cartas você 'mata'?");
                int palpiteValor = int.Parse(Console.ReadLine());
                bool ePalpiteProibido = eUltimoJogador && palpiteValor == valorProibido;
                while (palpiteValor < 0 || palpiteValor > quantidadeRodadas || ePalpiteProibido)
                {
                    if (!ePalpiteProibido)
                    {
                        Console.WriteLine($"{jogador.nome}, valor inválido. Por favor insira um número de 0 a {quantidadeRodadas}.");
                    }
                    else
                    {
                        Console.WriteLine($"{jogador.nome}, {palpiteValor} é o valor proibido. Escolha outro.");
                    }
                    palpiteValor = int.Parse(Console.ReadLine());
                    ePalpiteProibido = eUltimoJogador && palpiteValor == valorProibido;
                }
                somaPalpites += palpiteValor;
                Palpite palpite = new Palpite(jogador, palpiteValor);
                palpites.Add(palpite);
                Log.Registrar($"{jogador.nome} declarou palpite: {palpite.palpite}");
                filaPlayer.Inserir(jogador);
            }
        }
        public void GerenciaRodadas()
        {
            while (quantidadeRodadas > 0)
            {
                ExibirCabecalhoRodada(quantidadeRodadas);
                IniciaRodada();
                quantidadeRodadas--;
            }
            if (quantidadeRodadas == 0)
            {
                SetGanhadores();
            }
        }
        public static void ExibirCabecalhoRodada(int numeroRodada)
        {
           
            Console.ForegroundColor = ConsoleColor.Cyan;
            string titulo = $"♣️ RODADA {numeroRodada} ♥️";
            string separador = new string('═', titulo.Length + 10);
            int espaco = (Console.WindowWidth - separador.Length) / 2;
            Console.WriteLine(new string(' ', espaco) + separador);
            Console.WriteLine(new string(' ', espaco) + $"     {titulo}     ");
            Console.WriteLine(new string(' ', espaco) + separador);
            Console.ResetColor();
            Console.WriteLine(); 
        }
        public List<Jogador> SetGanhadores()
        {
            for (int i = 0; i < quantidadeJogadores; i++)
            {
                Jogador jogador = filaPlayer.Remover();
                Palpite palpite = palpites.Find(p => p.jogador == jogador);
                int quantidadeVitorias = 0;
                foreach (Rodada rodada in rodadas)
                {
                    if (rodada.ganhador == jogador)
                    {
                        quantidadeVitorias++;
                    }
                    Log.Registrar($"{jogador.nome} acertou o palpite.");
                }
                if (palpite != null && quantidadeVitorias == palpite.palpite)
                {
                    this.ganhadores.Add(jogador);
                }
                else
                {
                    int diferenca = Math.Abs(quantidadeVitorias - palpite.palpite);
                    for (int j = 0; j < diferenca; j++)
                    {
                        jogador.PerdeVida();
                    }
                    Log.Registrar($"{jogador.nome} errou o palpite. Perdeu{diferenca} vida(s).");
                }
                filaPlayer.Inserir(jogador);
                Log.Registrar($"{jogador.nome}: {quantidadeVitorias} vitória(s), palpite era {palpite.palpite}.");
            }
            foreach (Jogador ganhador in ganhadores)
            {
                Console.WriteLine($"Ganhou {ganhador.nome}");
            }
            List<(Jogador jogador, int erro)> classificacao = new List<(Jogador, int)>();
            foreach (Jogador jogador in jogadores)
            {
                int vitorias = rodadas.Count(r => r.ganhador == jogador);
                Palpite palpite = palpites.FirstOrDefault(p => p.jogador == jogador);
                Log.Registrar($"{jogador.nome}: {vitorias} vitória(s), palpite era {palpite.palpite}.");
                if (palpite != null && vitorias == palpite.palpite)
                {
                    Log.Registrar($"{jogador.nome} acertou o palpite.");
                }
                else
                {
                    int erro = Math.Abs(vitorias - palpite.palpite);
                    Log.Registrar($"{jogador.nome} errou o palpite. Perdeu {erro} vida(s).");

                    for (int i = 0; i < erro; i++)
                    {
                        jogador.PerdeVida();
                    }
                }
                int erroAbsoluto = Math.Abs(vitorias - palpite.palpite);
                classificacao.Add((jogador, erroAbsoluto));
            }
            List<(Jogador jogador, int erro)> rankingOrdenado;
            rankingOrdenado = classificacao.OrderBy(c => c.erro).ToList();
            for (int i = 0; i < rankingOrdenado.Count; i++)
            {
                Jogador jogador = rankingOrdenado[i].jogador;
                if (jogador.rankingUltimasPartidas.Count == 5)
                    jogador.rankingUltimasPartidas.Dequeue();
                jogador.rankingUltimasPartidas.Enqueue(i + 1);
            }
            foreach (Jogador jogador in jogadores)
            {
                Console.WriteLine($"Ranking de {jogador.nome}:");
                Log.Registrar($"Ranking atualizado de {jogador.nome}");
                foreach (int rank in jogador.rankingUltimasPartidas)
                {
                    Console.Write(rank + " ");
                    Log.Registrar(rank + " ");
                }
                Console.WriteLine();
            }
            return ganhadores;
        }
    }
}
