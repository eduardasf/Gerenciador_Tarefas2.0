
using Tarefass.Controller;
using Tarefass.Model;

namespace Tarefass.View
{
    public class TarefaView
    {
        private TarefaController _tarefaController;
        private ClienteView _clienteView;
        public ClienteController _clienteController;
        public TarefaView(TarefaController tarefaController, ClienteView clienteView, ClienteController clienteController)
        {
            _tarefaController = tarefaController;
            _clienteView = clienteView;
            _clienteController = clienteController;
        }

        public void MenuTarefa()
        {
            Tarefa tarefa = new Tarefa();
            int opcao;
            do
            {
                Console.Write("*****GERENCIADOR DE TAREFAS*****\n1 - Cadastrar Cliente\n2 - Adicionar Tarefa\n3 - Listar Tarefa\n4 - Remover Tarefa\n5 - Modificar status da tarefa\n6 - Pesquisar a partir do cliente\n7 - Listar Clientes\n0 - Sair\nDigite sua Resposta: ");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("***CADASTRO DO CLIENTE***");
                        Console.WriteLine();
                        _clienteController.AdicionarCliente(_clienteView.CadastrarCliente());
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("***ADICIONAR TAREFAS***");
                        _clienteController.ListarCliente();
                        Console.Write("Insira o código do cliente: ");
                        int clienteId = -1;

                        try
                        {
                            clienteId = int.Parse(Console.ReadLine());

                            if (!_clienteController.VerificarClienteExistente(clienteId))
                            {
                                Console.WriteLine("\nCliente com o ID especificado não encontrado. Não é possível cadastrar a tarefa.");
                                Console.WriteLine();
                                break;
                            }

                            Console.Write("Insira a descrição da tarefa: ");
                            tarefa.Descricao = Console.ReadLine();
                            Console.Write("\nInsira o status da tarefa:\n1 - Pendente\n2 - Em andamento\n3 - Concluído\nResposta: ");
                            int op = int.Parse(Console.ReadLine());
                            tarefa.Status = op;
                            _tarefaController.AdicionarTarefa(tarefa.Descricao, tarefa.Status, clienteId);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("\nFormato inválido para o ID do cliente. Certifique-se de inserir um número inteiro.");
                            Console.WriteLine();
                        }
                        break;
                    case 3:
                        Console.Clear();
                        try
                        {
                            _tarefaController.ListarTarefa();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"\nErro ao listar tarefas: {ex.Message}");
                        }
                        break;

                    case 4:
                        Console.Clear();
                        try
                        {
                            _tarefaController.ListarTarefa();
                            Console.WriteLine();
                            Console.Write("Insira o código a ser removido: ");
                            tarefa.Id = int.Parse(Console.ReadLine());
                            _tarefaController.RemoverTarefa(tarefa.Id);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("\nFormato inválido para o ID da tarefa. Certifique-se de inserir um número inteiro.");
                            Console.WriteLine();
                        }
                        break;

                    case 5:
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine("***MODIFICAR STATUS DA TAREFA***");
                        try
                        {
                            _tarefaController.ListarTarefa();
                            Console.Write("Insira o código da tarefa a ser modificada: ");
                            int codigo = int.Parse(Console.ReadLine());
                            Console.Write("\nInsira o novo status da tarefa:\n1 - Pendente\n2 - Em andamento\n3 - Concluído\nResposta: ");
                            int opp = int.Parse(Console.ReadLine());
                            tarefa.Status = opp;
                            _tarefaController.ModificarTarefa(codigo, tarefa.Status);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("\nFormato inválido para o ID da tarefa ou status. Certifique-se de inserir números inteiros.");
                            Console.WriteLine();
                        }
                        break;

                    case 6:
                        Console.Clear();
                        Console.WriteLine("***PESQUISAR POR NOME***");
                        try
                        {
                            _clienteController.ListarCliente();
                            Console.Write("Insira o código do cliente: ");
                            int clienteID = int.Parse(Console.ReadLine());
                            _clienteController.ListarTarefasPorUsuario(clienteID);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("\nFormato inválido para o ID do cliente. Certifique-se de inserir um número inteiro.");
                            Console.WriteLine();
                        }
                        break;

                    case 7:
                        Console.Clear();
                        try
                        {
                            _clienteController.ListarCliente();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"\nErro ao listar clientes: {ex.Message}");
                        }
                        break;

                    case 0:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida, tente novamente!");
                        Console.WriteLine();
                        break;
                }
            } while (opcao != 0);
        }
    }
}
