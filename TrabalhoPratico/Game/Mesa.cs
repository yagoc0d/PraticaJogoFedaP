using System;
using System.Collections.Generic;
using TrabalhoPratico.Data;
using TrabalhoPratico.Models;

namespace TrabalhoPratico.Game
{
    public class Mesa
    {
        public List<Jogador> Jogadores;
        public int quantidadeCartasAtual = 5;
        public int quantidadeCartasInicial = 5;
        public bool deveDecrementar = true;
        public int quantidadeJogadores;
        FilaPlayer filaPlayer;
        public Mesa()
        {
            this.Jogadores = new List<Jogador>();
            this.filaPlayer = new FilaPlayer();
        }

        public void InserirJogadores()
        {
            int numero;
            bool verificar= VerificarValorEntrada(Console.ReadLine(), out numero);

            while (!verificar)
            {
                Console.WriteLine("Digite um valor valido entre 2 e 8 jogadores");
                verificar=VerificarValorEntrada(Console.ReadLine(), out numero);
            }
            this.quantidadeJogadores = numero;
            for (int i = 0; i < numero; i++)
            {
                Console.WriteLine($"Digite o nome do {i + 1}º Jogador");
                string nomeJogador = Console.ReadLine();
                Jogador player = new Jogador(nomeJogador, i);

                this.Jogadores.Add(player);
                this.filaPlayer.Inserir(player);
                Log.Registrar($"Jogador inserido: {nomeJogador}");
            }

        }
        public bool VerificarValorEntrada(string entrada,out int numero)
        {
           
            if (int.TryParse(entrada, out numero) && numero > 0&&numero<8)
            {
                return true;
            }
            return false;
        }
        public void ComecarJogo()
        {
            Baralho baralho = new Baralho();
            GerenciaPartida(baralho);
        }
        public void ComecarPartida(Baralho baralho)
        {

            baralho.Embaralhar();
            int quantidadeCartas = GetQuantidadeCartas();
            DistribuirCartas(baralho, quantidadeCartas);
            Partida partida = new Partida(quantidadeCartas, filaPlayer, quantidadeJogadores, Jogadores);
            RotacaoJogador();

        }
        private void RotacaoJogador()
        {
            Jogador RotacaoJogador = filaPlayer.Remover();
            filaPlayer.Inserir(RotacaoJogador);
        }
        public void DistribuirCartas(Baralho baralho, int quantidadeCartas)
        {
            for (int i = 0; i < quantidadeJogadores; i++)
            {
                List<Carta> cartas = baralho.DarCartas(quantidadeCartas);
                Jogador jogador = filaPlayer.Remover();
                Log.Registrar($"Cartar distribuidas para{jogador.nome}");
                jogador.MaoAtual = cartas;
                Console.WriteLine(jogador.nome);
                ImprimirMaoLadoALado(cartas);
                filaPlayer.Inserir(jogador);
            }
        }
        public int GetQuantidadeCartas()
        {
            int quantidadeCartas = quantidadeCartasAtual;
            if (quantidadeCartasAtual == 1)
            {
                deveDecrementar = false;
            }
            else if (quantidadeCartasAtual == 5)
            {
                deveDecrementar = true;
            }

            if (deveDecrementar)
            {
                this.quantidadeCartasAtual--;
            }
            else
            {

                this.quantidadeCartasAtual++;
            }

            return quantidadeCartas;
        }
        private void GerenciaPartida(Baralho baralho)
        {
            while (this.Jogadores.Count > 1)
            {
                ComecarPartida(baralho);
                RemovePerdedores();
            }
        }
        private void RemovePerdedores()
        {
            Jogadores.RemoveAll(jogador => jogador.vidas == 0);
            int total = quantidadeJogadores;
            FilaPlayer novaFila = new FilaPlayer(quantidadeJogadores);
            for (int i = 0; i < total; i++)
            {
                Jogador j = filaPlayer.Remover();
                if (Jogadores.Contains(j))
                {
                    novaFila.Inserir(j);
                }
            }
            filaPlayer = novaFila;
            quantidadeJogadores = Jogadores.Count;
            if (Jogadores.Count == 1)
            {
                AnunciaGanhador();
            }
        }
        private void AnunciaGanhador()
        {
            Console.WriteLine($"Parabéns Jogador,{Jogadores[0].nome}, \U0001f973✨ Você ganhou! Parabéns! 👑🎉🏆😄");
            Log.Registrar($"🏆 Jogo encerrado. Ganhador: {Jogadores[0].nome} com {Jogadores[0].vidas} vida(s).");

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
        public void TelaInicial()
        {
            int opcao;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("===================================");
                Console.WriteLine("        JOGO FEDAPUT 🎴           ");
                Console.WriteLine("===================================");
                Console.ResetColor();
                Console.WriteLine("1. Iniciar novo jogo");
                Console.WriteLine("2. Ver regras do jogo");
                Console.WriteLine("3. Sair");
                Console.Write("\nEscolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Opção inválida. Pressione Enter para tentar novamente...");
                    Console.ReadKey();
                    continue;
                }

                switch (opcao)
                {
                    case 1:
                        {
                            // Chamar a lógica principal do jogo
                            Console.WriteLine("\nIniciando o jogo...");
                            Console.WriteLine("\nQuantos jogadores vão jogar?");
                            InserirJogadores();
                            ComecarJogo();
                            break;
                        }

                    case 2:
                        {
                            MostrarRegras();
                            break;
                        }

                    case 3:
                        {
                            Console.WriteLine("\nSaindo do jogo... Até logo!");
                            break;
                        }
                }

            } while (opcao != 3);
        }
        public void MostrarRegras()
        {
            Console.Clear();
            Console.WriteLine("========== REGRAS DO JOGO ==========\n");
            Console.WriteLine("- Cada jogador começa com 5 vidas");
            Console.WriteLine("- Rodadas com 5 → 1 cartas e voltam a subir");
            Console.WriteLine("- Jogadores apostam quantas rodadas vão vencer");
            Console.WriteLine("- Se errar o palpite, perde vidas");
            Console.WriteLine("- Manilhas fixas: 4♣️, 7♥️, A♠️, 7♦️");
            Console.WriteLine("- Vence o último sobrevivente com vidas");
            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
        }
        public static void ExibirCabecalhoRodada(int numeroRodada)
        {
            Console.Clear();
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
    }
}

