using System;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

string nomeJogador = "";
int vitorias = 0;
int derrotas = 0;
int empates = 0;

IniciarJogador(ref nomeJogador);

bool continuar = true;

while (continuar)
{
    MostrarMenu();

    int opcaoMenu;
    if (!LerOpcao(out opcaoMenu, 0, 3))
        continue;

    switch (opcaoMenu)
    {
        case 1:
            Jogar(nomeJogador, ref vitorias, ref derrotas, ref empates);
            break;

        case 2:
            MostrarEstatisticas(nomeJogador, in vitorias, in derrotas, in empates);
            break;

        case 3:
            IniciarJogador(ref nomeJogador);
            vitorias = derrotas = empates = 0;
            break;

        case 0:
            continuar = false;
            break;
    }
}

Console.WriteLine("👋 Tchau! Até a próxima!");


// ===================== MÉTODOS =====================

void IniciarJogador(ref string nome)
{
    Console.Write("\nDigite o nome do jogador: ");
    nome = Console.ReadLine();

    while (string.IsNullOrWhiteSpace(nome))
    {
        Console.Write("Nome inválido. Digite novamente: ");
        nome = Console.ReadLine();
    }
}

void MostrarMenu()
{
    Console.WriteLine("\n===== MENU =====");
    Console.WriteLine("1 - Jogar");
    Console.WriteLine("2 - Ver Estatísticas");
    Console.WriteLine("3 - Trocar Jogador");
    Console.WriteLine("0 - Sair");
    Console.Write("Escolha uma opção: ");
}

bool LerOpcao(out int valor, int min, int max)
{
    if (!int.TryParse(Console.ReadLine(), out valor))
    {
        Console.WriteLine("Entrada inválida! Digite apenas números.");
        return false;
    }

    if (valor < min || valor > max)
    {
        Console.WriteLine("Opção fora do intervalo permitido.");
        return false;
    }

    return true;
}

void Jogar(string nome, ref int vitorias, ref int derrotas, ref int empates)
{
    Console.WriteLine("\nEscolha: 0 - Pedra ✊ | 1 - Papel ✋ | 2 - Tesoura ✌");
    Console.Write("Sua escolha: ");

    int opcaoJogador;
    if (!LerOpcao(out opcaoJogador, 0, 2))
        return;

    int opcaoPC = new Random().Next(3);

    MostrarEscolhaJogador(nome, opcaoJogador);
    MostrarEscolhaPC(opcaoPC);

    VerificarResultado(opcaoJogador, opcaoPC, ref vitorias, ref derrotas, ref empates);
}

void MostrarEscolhaJogador(string nome, int opcao)
{
    Console.WriteLine($"\n{nome} escolheu {ConverterOpcao(opcao)}");
}

void MostrarEscolhaPC(int opcao)
{
    Console.WriteLine($"Computador escolheu {ConverterOpcao(opcao)}");
}

string ConverterOpcao(in int opcao)
{
    return opcao switch
    {
        0 => "Pedra ✊",
        1 => "Papel ✋",
        2 => "Tesoura ✌",
        _ => "Desconhecido"
    };
}

void VerificarResultado(int jogador, int pc, ref int vitorias, ref int derrotas, ref int empates)
{
    if (jogador == pc)
    {
        Console.WriteLine("\n😀 Empate!");
        empates++;
        return;
    }

    bool ganhou =
        (jogador == 0 && pc == 2) ||
        (jogador == 1 && pc == 0) ||
        (jogador == 2 && pc == 1);

    if (ganhou)
    {
        Console.WriteLine("\n😀 Você venceu!");
        vitorias++;
    }
    else
    {
        Console.WriteLine("\n😀 Você perdeu!");
        derrotas++;
    }
}

void MostrarEstatisticas(string nome, in int vitorias, in int derrotas, in int empates)
{
    Console.WriteLine("\n===== ESTATÍSTICAS =====");
    Console.WriteLine($"Jogador: {nome}");
    Console.WriteLine($"Vitórias: {vitorias}");
    Console.WriteLine($"Derrotas: {derrotas}");
    Console.WriteLine($"Empates: {empates}");
}
