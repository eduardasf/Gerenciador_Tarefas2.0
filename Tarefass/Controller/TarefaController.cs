
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

        public void AdicionarTarefa(string descricao, int status)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("INSERT INTO tarefa.tarefa (descricao, status_id) VALUES (@descricao, @status)", connection))
                {
                    command.Parameters.AddWithValue("descricao", descricao);
                    command.Parameters.AddWithValue("status", status);

                    int rowsAffected = command.ExecuteNonQuery();

                    Verificar(rowsAffected);
                }
            }
        }

        public void ListarTarefa()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT tarefa.id, tarefa.descricao, status.status FROM tarefa.tarefa JOIN tarefa.status ON tarefa.status_id = status.id", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            string descricao = reader["descricao"].ToString();
                            string status = reader["status"].ToString();

                            Console.WriteLine($"ID.......: {id}\nDescrição: {descricao}\nStatus: {status}");
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
                using (var command = new NpgsqlCommand("DELETE FROM tarefa.tarefa WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    VerificarExcluir(rowsAffected);
                }
            }
        }

        public void ModificarTarefa(int id, /*string descricao,*/ int status)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("UPDATE tarefa.tarefa SET status_id = @status_id WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    command.Parameters.AddWithValue("status_id", status);

                    int rowsAffected = command.ExecuteNonQuery();

                    VerificarModificador(rowsAffected);
                }
            }
        }


        public void Verificar(int rowsAffected)
        {
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

        public void VerificarExcluir(int rowsAffected)
        {
            if (rowsAffected > 0)
            {
                Console.WriteLine("\nCadastro excluído com sucesso!");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("\nNenhum cadastro encontrado com o ID especificado.");
                Console.WriteLine();
            }
        }

        public void VerificarModificador(int rowsAffected)
        {
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

