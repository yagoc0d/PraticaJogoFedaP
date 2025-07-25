# 🃏 Jogo Fedaput (variação do Truco Mineiro)

Projeto acadêmico desenvolvido em C# para a disciplina de Algoritmos e Estruturas de Dados na PUC Minas, sob orientação do Prof. Edwaldo Soares Rodrigues.

---

## 🎯 Objetivo

Simular um jogo de cartas baseado no Truco Mineiro, com regras adaptadas, utilizando exclusivamente estruturas de dados implementadas manualmente, como listas, filas e pilhas. O jogo roda no console e registra todas as ações em log.

---

## 🧠 Regras principais

- São utilizadas 40 cartas (A, 2, 3, 4, 5, 6, 7, Q, J, K) de 4 naipes
- As manilhas são fixas e têm maior peso:  
  `4♣️`, `7♥️`, `A♠️`, `7♦️` (ordem de maior para menor)
- Número de jogadores: mínimo 2, máximo 8
- Cada jogador começa com 5 vidas
- O número de cartas por rodada segue o padrão: 5 → 4 → 3 → 2 → 1 → 2 → 3 → 4 → 5 → ...
- Antes da rodada, cada jogador faz um **palpite** de quantas rodadas vai vencer
- Se errar o palpite, perde vidas proporcionalmente
- Rodadas seguem ordem circular (fila) e com regras de **desempate por naipe**
- O jogo termina quando restar apenas 1 jogador com vidas

---
## 💻 Tecnologias utilizadas
- **C# Console Application (.NET Framework)**
- **Orientação a Objetos**
- **Estruturas de Dados manuais**
- **Manipulação de arquivos com `StreamWriter`**
- **Sistema de Log**
- **Organização modular por namespace/pasta**
---

## 🗂 Estrutura do projeto

```
TrabalhoPratico/
├── Program.cs                        # Execução principal
├── App.config
├── log_partida.txt                  # Registro da execução do jogo
├── Data/
│   └── Log.cs                       # Sistema de log
├── Game/
│   ├── Baralho.cs                   # Lógica do baralho e cartas
│   ├── Jogada.cs                    # Representa uma jogada
│   ├── Mesa.cs                      # Lógica central do jogo
│   └── Palpite.cs                   # Sistema de palpites dos jogadores
├── Model/
│   ├── Carta.cs
│   ├── CelulaCarta.cs
│   ├── Jogador.cs
│   └── Naipe.cs
```

---

## ▶️ Como executar
1. Clone o repositório ou baixe os arquivos  
2. Abra com Visual Studio (ou outro editor C#)  
3. Compile e execute `Program.cs`  
4. Escolha o número de jogadores e acompanhe o jogo via console e log

---

## 📚 Conceitos Aplicados e Aprendizados

Este projeto consolidou os seguintes conhecimentos de programação e estruturas de dados:

- **Programação orientada a objetos (POO)**: encapsulamento, classes, métodos e atributos com responsabilidade única  
- **Estruturas de dados lineares e não lineares**: implementação manual de **pilhas**, **filas circulares** e **listas encadeadas**  
- **Lógica de jogo**: controle de estado, turnos, regras de verificação, desempate e ranking  
- **Manipulação de arquivos**: geração de logs detalhados com `StreamWriter`  
- **Modularização**: organização do código por camadas e separação por responsabilidade (`Model`, `Game`, `Data`)  
- **Sorteio pseudoaleatório com controle de duplicidade**  
- **Uso de enums e constantes** para modelar regras fixas do jogo  
- **Prática com reaproveitamento de estruturas** para fluxo de jogo dinâmico (ciclos de 5→1→5)

---

## 👨‍💻 Autor

Yago Marques — Desenvolvedor em formação na PUC Minas.

📌 GitHub: [https://github.com/yagoc0d]  

---
