
using Tarefass.Controller;
using Tarefass.View;

namespace GerenciadorTarefas
{
    class Program
    {
        static void Main()
        {
            string connectionString = "Host=localhost;Port=5432;Database=Banco_Tarefas;Username=postgres;Password=398201;";
            TarefaController tarefaController = new TarefaController(connectionString);
            ClienteController clienteController = new ClienteController(connectionString);
            ClienteView clienteView = new ClienteView();
            TarefaView tarefaView = new TarefaView(tarefaController, clienteView, clienteController);
            
            tarefaView.MenuTarefa();

        }
    }
}