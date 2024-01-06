
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
                Console.Write("*****GERENCIADOR DE TAREFAS*****\n1 - Adicionar\n2 - Listar\n3 - Remover\n4 - Modificar status da tarefa\n5 - Pesquisar a partir do cliente\n0 - Sair\nDigite sua Resposta: ");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("***ADICIONAR TAREFAS***");
                        int clienteId = _clienteController.AdicionarCliente(_clienteView.CadastrarCliente());

                        if (clienteId != -1)
                        {
                            Console.Write("Insira a descrição da tarefa: ");
                            tarefa.Descricao = Console.ReadLine();
                            Console.Write("\nInsira o status da tarefa:\n1 - Pendente\n2 - Em andamento\n3 - Concluído\nResposta: ");
                            int op = int.Parse(Console.ReadLine());
                            tarefa.Status = op;
                            _tarefaController.AdicionarTarefa(tarefa.Descricao, tarefa.Status, clienteId);
                        }
                        break;
                    case 2:
                        Console.Clear();
                        _tarefaController.ListarTarefa();
                        break;
                    case 3:
                        Console.Clear();
                        _tarefaController.ListarTarefa();
                        Console.WriteLine();
                        Console.Write("Insira o código a ser removido: ");
                        tarefa.Id = int.Parse(Console.ReadLine());
                        _tarefaController.RemoverTarefa(tarefa.Id);
                        break;
                    case 4:
                        Console.Clear();
                        _tarefaController.ListarTarefa();
                        Console.WriteLine();
                        Console.WriteLine("***MODIFICAR STATUS DA TAREFA***");
                        Console.Write("Insira o código da tarefa a ser modificada: ");
                        int codigo = int.Parse(Console.ReadLine());
                        Console.Write("\nInsira o novo status da tarefa:\n1 - Pendente\n2 - Em andamento\n3 - Concluído\nResposta: ");
                        int opp = int.Parse(Console.ReadLine());
                        tarefa.Status = opp;
                        _tarefaController.ModificarTarefa(codigo,tarefa.Status);
                        break;
                    case 5:
                        Console.Clear();
                        _clienteController.ListarCliente();
                        Console.WriteLine("***PESQUISAR POR NOME***");
                        Console.Write("Insira o código do cliente: ");
                        int clienteID = int.Parse(Console.ReadLine());
                        _clienteController.ListarTarefasPorUsuario(clienteID);
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
