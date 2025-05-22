using System.Globalization;
using System.Text;
using DesafioProjetoHospedagem.Models;

Console.OutputEncoding = Encoding.UTF8;
Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");

bool exibirMenu = true;
List<Pessoa> hospedes = new();
Suite? suite = null;
Reserva? reserva = null;

while (exibirMenu)
{
    Console.Clear();
    Console.WriteLine("Seja bem-vindo ao sistema de hospedagem do hotel.\nDigite uma opção:");
    Console.WriteLine("1 - Cadastrar hóspedes");
    Console.WriteLine("2 - Cadastrar suíte");
    Console.WriteLine("3 - Criar reserva");
    Console.WriteLine("4 - Encerrar");

    string? opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            Console.Clear();
            Console.Write("Quantos hóspedes deseja cadastrar? ");
            if (!int.TryParse(Console.ReadLine(), out int numeroHospedes))
            {
                Console.WriteLine("Número inválido.");
                break;
            }

            for (int i = 1; i <= numeroHospedes; i++)
            {
                Console.WriteLine($"Hóspede {i}:");
                string? nome;
                while (true)
                {
                    Console.Write("  Nome: ");
                    nome = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(nome) && nome.All(char.IsLetter))
                        break;
                    Console.WriteLine("  Nome inválido. Digite apenas letras.");
                }

                string? sobrenome;
                while (true)
                {
                    Console.Write("  Sobrenome: ");
                    sobrenome = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(sobrenome) && sobrenome.All(char.IsLetter))
                        break;
                    Console.WriteLine("  Sobrenome inválido. Digite apenas letras.");
                }
                hospedes.Add(new Pessoa(nome, sobrenome));
            }
            Console.WriteLine("Hóspedes cadastrados com sucesso!");
            break;
        case "2":
            Console.Clear();
            Console.Write("Insira o tipo da suíte: ");
            string? tipo = Console.ReadLine();

            int capacidade;
            while (true)
            {
                Console.Write("Qual é a capacidade da suíte? ");
                if (int.TryParse(Console.ReadLine(), out capacidade) && capacidade > 0)
                    break;
                Console.WriteLine("Capacidade inválida. Digite um número inteiro positivo.");
            }

            decimal valorDiaria;
            while (true)
            {
                Console.Write("Qual é o valor da diária: ");
                if (decimal.TryParse(Console.ReadLine(), out valorDiaria) && valorDiaria > 0)
                    break;
                Console.WriteLine("Valor inválido. Digite um valor decimal positivo.");
            }

            suite = new Suite(tipo ?? "Padrão", capacidade, valorDiaria);
            Console.WriteLine("Suíte cadastrada com sucesso!");
            break;
        case "3":
            Console.Clear();
            if (suite == null)
            {
                Console.WriteLine("Suíte não cadastrada. Cadastre a suíte primeiro.");
            }
            else if (hospedes.Count == 0)
            {
                Console.WriteLine("Nenhum hóspede cadastrado. Cadastre hóspedes primeiro.");
            }
            else
            {
                Console.Write("Quantos dias de reserva? ");
                if (!int.TryParse(Console.ReadLine(), out int dias))
                {
                    Console.WriteLine("Número inválido de dias.");
                    break;
                }

                try
                {
                    reserva = new Reserva(dias);
                    reserva.CadastrarSuite(suite);
                    reserva.CadastrarHospedes(hospedes);

                    Console.WriteLine("Reserva criada com sucesso!");
                    Console.WriteLine($"Hóspedes: {reserva.ObterQuantidadeHospedes()}");
                    Console.WriteLine($"Valor diária: {reserva.CalcularValorDiaria():C}");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Erro ao cadastrar hóspedes: {ex.Message}");
                }
            }
            break;
        case "4":
            exibirMenu = false;
            break;
        default:
            Console.WriteLine("Opção inválida");
            break;
    }

    Console.WriteLine("Pressione uma tecla para continuar...");
    Console.ReadLine();
}
Console.WriteLine("O programa se encerrou.");