
using Tarefass.Model;

namespace Tarefass.View
{
    public class ClienteView
    {
        public string CadastrarCliente()
        {
            Cliente cliente = new Cliente();
            Console.Write("Insire seu nome: ");
            cliente.Nome = Console.ReadLine();
            return cliente.Nome;
        }
    }
}
