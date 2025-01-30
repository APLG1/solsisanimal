
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sysanimalmodel;
using System.Data;
using System.Data.SQLite;
namespace sysanimalcontroller
{


    public class clientes
    {
        private string stringConexao = @"Data Source=c:\\as\\sysespecie.db; Version=3;";
        private List<Cliente> bancoCliente = new List<Cliente>();

        Cliente cliente;
        private string caminhoBanco = ConfigurationManager.AppSettings["caminhoBanco"];
        private string nomeBancoCliente = ConfigurationManager.AppSettings["nomeBancoCliente"];


        public void salvarClienteEmCsv()
        {
            try
            {
                using (StreamWriter write = new StreamWriter(caminhoBanco + nomeBancoCliente))
                {
                    write.WriteLine("nome,cpf,email,numero de telefone");
                    foreach (var item in bancoCliente)
                    {
                        write.WriteLine(value: ($"{item.nome}, {item.cpf},{item.email}, {item.numerot}"));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro:{ex.Message}");
            }
        }
        static List<Cliente> CarregarClienteDoCsv(string caminho)
        {
            var Cliente = new List<Cliente>();
            try
            {
                if (File.Exists(caminho) == true)
                {
                    using (StreamReader reader = new StreamReader(caminho))
                    {
                        string linha = reader.ReadLine();
                        while ((linha = reader.ReadLine()) != null)
                        {
                            var partes = linha.Split(',');
                            if (partes.Length == 4)
                            {
                                string nome = partes[0];
                                string cpf = partes[1];
                                string email = partes[2];
                                string numerot = partes[3];
                                Cliente.Add(new Cliente
                                {
                                    nome = nome,
                                    cpf = cpf,
                                    email = email,
                                    numerot = numerot
                                });
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }
            return Cliente;
        }
        public clientes()
        {
            bancoCliente = CarregarClienteDoCsv(caminhoBanco + nomeBancoCliente);

        }

        public void inserir(Cliente cliente)
        {


            using (var conn = new SQLiteConnection(stringConexao))
            {
                conn.Open();
                string query = "INSERT INTO CLIENTE (NOMECLIENTE, EMAIL, CPF, NUMEROT) VALUES (@NOMECLIENTE, @EMAIL, @CPF, @NUMEROT)";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NOMECLIENTE", cliente.nome);
                    cmd.Parameters.AddWithValue("@EMAIL", cliente.email);
                    cmd.Parameters.AddWithValue("@CPF", cliente.cpf);
                    cmd.Parameters.AddWithValue("@NUMEROT", cliente.numerot);
                    cmd.ExecuteNonQuery();
                }

            }


            /*
            bancoCliente.Add(cliente);
            */



        }

        public void alterar(string nome, string cpf, string email, string numerot, Cliente cliente)
        {
            Console.WriteLine("digite o nome do cliente que deseja alterar");
            using (var conn = new SQLiteConnection(stringConexao))
            {
                conn.Open();
                string query = "UPDATE CLIENTE SET NOMECLIENTE = @NOMECLIENTE, EMAIL = @EMAIL, CPF = @CPF, NUMEROT = @NUMEROT WHERE NUMEROT = NUMEROT";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NOMECLIENTE", cliente.nome);
                    cmd.Parameters.AddWithValue("@CPF", cliente.cpf);
                    cmd.Parameters.AddWithValue("@EMAIL", cliente.email);
                    cmd.Parameters.AddWithValue("@NUMEROT", cliente.numerot);

                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("CLIENTE ALTERADO COM SUCESSO!");
            }
            /*
                        foreach (var item in bancoCliente)
                        {

                            if (item.nome == nome)
                            {
                                Console.WriteLine("digite o nome do cliente que deseja alterar");
                                item.nome = cliente.nome;
                                item.cpf = cliente.cpf;
                                item.email = cliente.email;
                                item.numerot = cliente.numerot;

                                break;
                            }
                        }
            */
        }





        public void excluir(string nome)
        {
            /*
            foreach (var item in bancoCliente)
            {
                if (item.nome == nome.ToString().Trim())
                {
                    bancoCliente.Remove(item);
                    Console.WriteLine("cliente excluido da lista ");
                    break;
                }

            }
            */
            using (var conn = new SQLiteConnection(stringConexao))
            {
                conn.Open();
                string query = "DELETE FROM CLIENTE  WHERE NOMECLIENTE = @NOMECLIENTE";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NOMECLIENTE", nome);


                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("CLIENTE EXCLUIDO COM SUCESSO!");
            }


        }
        public Cliente pesquisar(string nome)
        {
            var cliente = new Cliente();
            /*
            foreach (var item in bancoCliente)
            {
                if (item.nome == nome.ToString().Trim())
                {
                    Console.WriteLine("nome do cliente:");
                    Console.WriteLine(item.nome.ToString());
                    Console.WriteLine("\n");
                    Console.WriteLine("cpf:");
                    Console.WriteLine(item.cpf.ToString());
                    Console.WriteLine("\n");
                    Console.WriteLine("email:");
                    Console.WriteLine(item.email.ToString());
                    Console.WriteLine("\n");
                    Console.WriteLine("numero de telefone:");
                    Console.WriteLine(item.numerot.ToString());
                    Console.WriteLine("\n");
                    break;
                }
            }
            */
            using (var conn = new SQLiteConnection(stringConexao))
            {

                conn.Open();
                string query = "SELECT NOMECLIENTE, CPF, EMAIL, NUMEROT FROM CLIENTE WHERE NOMECLIENTE = @NOMECLIENTE";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NOMECLIENTE", nome);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cliente = new Cliente

                            {
                                nome = reader.GetString(0),
                                cpf = reader.GetString(1),
                                email = reader.GetString(2),
                                numerot = reader.GetString(3)


                            };
                        }
                    }



                }
                return cliente;
            }
        }
        /*
            public void exibir()
        {


                    foreach (var item in bancoCliente)
                    {

                        Console.WriteLine($"{item.nome} - {item.cpf} - {item.email} - {item.numerot}");

                    }


        }
        */
        public List<Cliente> exibirTodos()
        {
            List<Cliente> clientes = new List<Cliente>();

            using (var conn = new SQLiteConnection(stringConexao))
            {

                conn.Open();
                string query = "SELECT NOMECLIENTE, CPF, EMAIL, NUMEROT FROM CLIENTE ";

                using (var cmd = new SQLiteCommand(query, conn))
                {

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cliente = new Cliente
                            {

                                nome = reader.GetString(0),
                                cpf = reader.GetString(1),
                                email = reader.GetString(2),
                                numerot = reader.GetString(3)

                            };
                            clientes.Add(cliente);
                        }
                    }



                }

            }
            return clientes;

        }
    }
}


    
   

