
using Npgsql;

namespace Tarefass.Controller
{
    public class TarefaController
    {
        private string _connectionString;

        public TarefaController(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AdicionarTarefa(string descricao, int status, int clienteId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("INSERT INTO tarefa.tarefa (descricao, status_id, cliente_id) VALUES (@descricao, @status, @clienteId)", connection))
                {
                    command.Parameters.AddWithValue("descricao", descricao);
                    command.Parameters.AddWithValue("status", status);
                    command.Parameters.AddWithValue("clienteId", clienteId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("\nCadastrado com sucesso!");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("\nErro ao cadastrar. Tente novamente.");
                        Console.WriteLine();
                    }
                }
            }
        }

        public void ListarTarefa()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT tarefa.id, cliente.nome, tarefa.descricao, status.status FROM tarefa.tarefa JOIN tarefa.status ON tarefa.status_id = status.id" + " JOIN tarefa.cliente ON tarefa.cliente_id = cliente.id", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            string nome_Cliente = reader["nome"].ToString();
                            string descricao = reader["descricao"].ToString();
                            string status = reader["status"].ToString();

                            Console.WriteLine($"ID.......: {id}\nNome: {nome_Cliente}\nDescrição: {descricao}\nStatus: {status}");
                            Console.WriteLine();
                        }
                    }
                }
            }
        }
        public void RemoverTarefa(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                int clienteId = ObterClienteIdPorTarefaId(id, connection);

                using (var command = new NpgsqlCommand("DELETE FROM tarefa.tarefa WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("\nTarefa excluída com sucesso!");
                        Console.WriteLine();

                        if (clienteId != -1)
                        {
                            RemoverCliente(clienteId, connection);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nNenhuma tarefa encontrada com o ID especificado.");
                        Console.WriteLine();
                    }
                }
            }
        }
        private int ObterClienteIdPorTarefaId(int tarefaId, NpgsqlConnection connection)
        {
            using (var command = new NpgsqlCommand("SELECT cliente_id FROM tarefa.tarefa WHERE id = @id", connection))
            {
                command.Parameters.AddWithValue("id", tarefaId);

                object resultado = command.ExecuteScalar();

                if (resultado != null)
                {
                    return Convert.ToInt32(resultado);
                }

                return -1;
            }
        }
        private void RemoverCliente(int clienteId, NpgsqlConnection connection)
        {
            using (var command = new NpgsqlCommand("DELETE FROM tarefa.cliente WHERE id = @id", connection))
            {
                command.Parameters.AddWithValue("id", clienteId);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Cliente associado removido com sucesso!");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Erro ao remover o cliente associado.");
                    Console.WriteLine();
                }
            }
        }


        public void ModificarTarefa(int id,int status)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("UPDATE tarefa.tarefa SET status_id = @status_id WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    command.Parameters.AddWithValue("status_id", status);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("\nCadastro modificado com sucesso!");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("\nNenhuma cadastro encontrado com o ID especificado.");
                        Console.WriteLine();
                    }
                }
            }
        }

    }
}

