
using Npgsql;

namespace Tarefass.Controller
{
    public class ClienteController
    {
        private string _connectionString;

        public ClienteController(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int AdicionarCliente(string nome)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("INSERT INTO tarefa.cliente (nome) VALUES (@nome) RETURNING id", connection))
                {
                    command.Parameters.AddWithValue("nome", nome);

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        int clienteId = Convert.ToInt32(result);
                        Console.WriteLine("\nCadastrado com sucesso!");
                        Console.WriteLine();
                        return clienteId;
                    }
                    else
                    {
                        Console.WriteLine("\nErro ao cadastrar cliente. Tente novamente.");
                        Console.WriteLine();
                        return -1;
                    }
                }
            }
        }

        public void ListarTarefasPorUsuario(int clienteId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT t.id, t.descricao, s.status " + "FROM tarefa.tarefa t " + "JOIN tarefa.status s ON t.status_id = s.id " + "WHERE t.cliente_id = @clienteId", connection))
                {
                    command.Parameters.AddWithValue("clienteId", clienteId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            string descricao = reader["descricao"].ToString();
                            string status = reader["status"].ToString();

                            Console.WriteLine($"\nID: {id}\nDescrição: {descricao}\nStatus: {status}");
                            Console.WriteLine();
                        }
                    }
                }
            }
        }

        public void ListarCliente()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM tarefa.cliente", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            string nome_Cliente = reader["nome"].ToString();

                            Console.WriteLine($"ID.......: {id}\nNome.....: {nome_Cliente}");
                            Console.WriteLine();
                        }
                    }
                }
            }
        }

        public bool VerificarClienteExistente(int clienteId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT COUNT(*) FROM tarefa.cliente WHERE id = @clienteId", connection))
                {
                    command.Parameters.AddWithValue("clienteId", clienteId);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }
    }
}
