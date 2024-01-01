
using Tarefass.Controller;
using Tarefass.Model;

namespace Tarefass.View
{
    public class TarefaView
    {
        private TarefaController _tarefaController;
        public TarefaView(TarefaController tarefaController)
        {
            _tarefaController = tarefaController;
        }
       
        public void MenuTarefa()
        {
            Tarefa tarefa = new Tarefa();
            int opcao;
            do
            {
                Console.Write("*****GERENCIADOR DE TAREFAS*****\n1 - Adicionar\n2 - Listar\n3 - Remover\n4 - Modificar status da tarefa\n0 - Sair\nDigite sua Resposta: ");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("***ADICIONAR TAREFAS***");

                        Console.Write("Insira a descrição da tarefa: ");
                        tarefa.Descricao = Console.ReadLine();

                        Console.Write("Insira o status da tarefa: ");
                        tarefa.Estado = Console.ReadLine();
                        _tarefaController.Adicionar(tarefa.Descricao, tarefa.Estado);
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

                        /*Console.Write("Insira a nova descrição da tarefa: ");
                        string novaDescricao = Console.ReadLine();*/

                        Console.Write("Insira o novo estado da tarefa: ");
                        string novoEstado = Console.ReadLine();
                        _tarefaController.ModificarTarefa(codigo,/*novaDescricao*/ novoEstado);
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
