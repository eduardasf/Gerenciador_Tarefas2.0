
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

        public void Adicionar(string descricao, string estado)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("INSERT INTO tarefa.tarefa (descricao, estado) VALUES (@descricao, @estado)", connection))
                {
                    command.Parameters.AddWithValue("descricao", descricao);
                    command.Parameters.AddWithValue("estado", estado);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("\nTarefa cadastrada com sucesso!");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("\nErro ao cadastrar a tarefa. Tente novamente.");
                        Console.WriteLine();
                    }
                }
            }
        }

        public void ListarTarefa()
        {
            using(var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using(var command = new NpgsqlCommand("SELECT * FROM tarefa.tarefa", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            string descricao = reader["descricao"].ToString();
                            string estado = reader["estado"].ToString();

                            Console.WriteLine($"ID.......: {id}\nDescrição: {descricao}\nStatus: {estado}");
                            Console.WriteLine();
                        }
                    }
                }
            }
        }

        public void RemoverTarefa(int id)
        {
            using(var connection=  new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using( var command = new NpgsqlCommand("DELETE FROM tarefa.tarefa WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("\nTarefa excluída com sucesso!");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("\nNenhuma tarefa encontrada com o ID especificado.");
                        Console.WriteLine();
                    }
                }
            }
        }

        public void ModificarTarefa(int id, /*string descricao,*/string estado)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("UPDATE tarefa.tarefa SET estado = @estado WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    //command.Parameters.AddWithValue("descricao", descricao);
                    command.Parameters.AddWithValue("estado", estado);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("\nTarefa modificada com sucesso!");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("\nNenhuma tarefa encontrada com o ID especificado.");
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}

