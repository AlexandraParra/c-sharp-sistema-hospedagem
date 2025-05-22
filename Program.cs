using System.Globalization;
using System.Text;
using DesafioProjetoHospedagem.Models;

Console.OutputEncoding = Encoding.UTF8;
Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");

bool exibirMenu = true;
List<Pessoa> hospedes = new();

while (exibirMenu)
{
    Console.Clear();
    Console.WriteLine("Seja bem-vindo ao sistema de hospedagem do hotel.\nDigite uma opção:");
    Console.WriteLine("1 - Cadastrar hóspedes");

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
            break;
        default:
            Console.WriteLine("Opção inválida");
            break;
    }
}