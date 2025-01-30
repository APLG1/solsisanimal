
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sysanimalmodel;
using System.Data;

namespace sysanimalcontroller

{

    
    public class animais
    {
        private string stringConexao = @"Data Source=c:\\as\\sysespecie.db; Version=3;";
        private List<Animal> bancoAnimal = new List<Animal>();

        Animal animal;
        private string caminhoBanco = ConfigurationManager.AppSettings["caminhoBanco"];
        private string nomeBancoAnimal = ConfigurationManager.AppSettings["nomeBancoAnimal"];

        public void salvarAnimalEmCsv()

        {
            try
            {
                using (StreamWriter write = new StreamWriter(caminhoBanco + nomeBancoAnimal))
                {
                    write.WriteLine("nome, apelido, data de nascimento, observação");
                    foreach (var item in bancoAnimal)
                    {
                        write.WriteLine(value: ($"{item.nome},{item.apelido},{item.datanas},{item.obs}"));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro:{ex.Message}");
            }
        }
        static List<Animal> CarregarAnimalDoCsv(string caminho)
        {
            var Animal = new List<Animal>();
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
                                string apelido = partes[1];
                                string datanas = partes[2];
                                string obs = partes[3];

                                Animal.Add(new Animal
                                {
                                    nome = nome,
                                    apelido = apelido,
                                    datanas = datanas,
                                    obs = obs
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
            return Animal;
        }
        public animais()
        {
            bancoAnimal = CarregarAnimalDoCsv(caminhoBanco + nomeBancoAnimal);

        }

        public void inserir(Animal animal)
        {


            using (var conn = new SQLiteConnection(stringConexao))
            {
                conn.Open();
                string query = "INSERT INTO ANIMAL (NOMEANIMAL, APELIDO, DATANAS, OBS) VALUES (@NOMEANIMAL, @APELIDO, @DATANAS, @OBS)";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NOMEANIMAL", animal.nome);
                    cmd.Parameters.AddWithValue("@APELIDO", animal.apelido);
                    cmd.Parameters.AddWithValue("@DATANAS", animal.datanas);
                    cmd.Parameters.AddWithValue("@OBS", animal.obs);
                    cmd.ExecuteNonQuery();
                }

            }


            /*
            bancoAnimal.Add(Class1);
            */



        }
        public void alterar(string nome, string apelido, string datanas, string obs, Animal animal)
        {
            Console.WriteLine("digite o nome que deseja alterar");


            using (var conn = new SQLiteConnection(stringConexao))
            {
                conn.Open();
                string query = "UPDATE ANIMAL SET NOMEANIMAL = @NOMEANIMAL, APELIDO = @APELIDO, DATANAS = @DATANAS, OBS = @OBS WHERE OBS = OBS";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NOMEANIMAL", animal.nome);
                    cmd.Parameters.AddWithValue("@APELIDO", animal.apelido);
                    cmd.Parameters.AddWithValue("@DATANAS", animal.datanas);
                    cmd.Parameters.AddWithValue("@OBS", animal.obs);

                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("ANIMAL ALTERADA COM SUCESSO!");
            }
            /*
                foreach (var item in bancoAnimal)
                {
                    if (item.nome == nome)
                    {
                        Console.WriteLine("digite o nome que deseja alterar");
                        item.nome = Class1.nome;
                        item.apelido = Class1.apelido;
                    item.datanas = Class1.datanas;
                    item.obs = Class1.obs;

                        break;
                    }
                }
            */
        }

            

            public void excluir(string nome)
        {

            /*
                foreach (var item in bancoAnimal)
                {
                    if (item.nome == nome.ToString().Trim())
                    {
                        bancoAnimal.Remove(item);
                        Console.WriteLine("Animal excluido da lista ");
                        break;
                    }
               
            }
            */


            using (var conn = new SQLiteConnection(stringConexao))
            {
                conn.Open();
                string query = "DELETE FROM ANIMAL  WHERE NOMEANIMAL = @NOMEANIMAL";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NOMEANIMAL", nome);


                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("ANIMAL EXCLUIDO COM SUCESSO!");
            }
        
    }
        public Animal pesquisar(string nome)
        {
            var animal = new Animal();
            /*
                       foreach (var item in bancoAnimal)
                       {
                           if (item.nome == nome.ToString().Trim())
                           {
                               Console.WriteLine("\n");
                               Console.WriteLine("nome e apelido:");
                               Console.WriteLine(item.nome.ToString()

                               + " - " + (item.apelido.ToString()));
                               Console.WriteLine("\n");
                               Console.WriteLine("oberservção:");
                               Console.WriteLine(item.obs.ToString());



                           }
                       }
            */
            using (var conn = new SQLiteConnection(stringConexao))
            {

                conn.Open();
                string query = "SELECT NOMEANIMAL, APELIDO, DATANAS, OBS FROM ANIMAL WHERE NOMEANIMAL = @NOMEANIMAL";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NOMEANIMAL", nome);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            animal = new Animal
                            
                            {
                                nome = reader.GetString(0),
                                apelido = reader.GetString(1),
                                datanas = reader.GetString(2),
                                obs = reader.GetString(3)


                            };
                        }
                    }



                }
                return animal;
            }
           
        
    }
        /*
        public void exibir()
        {

            
            foreach (var item in bancoAnimal)
            {

                Console.WriteLine($"{item.nome} - {item.apelido}");

            }

        }
            */
        public List<Animal> exibirTodos()
            {
                List<Animal> animais = new List<Animal>();

                using (var conn = new SQLiteConnection(stringConexao))
                {

                    conn.Open();
                    string query = "SELECT NOMEANIMAL, APELIDO FROM ANIMAL ";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var animal = new Animal
                                {
                                    
                                    nome = reader.GetString(0),
                                    apelido = reader.GetString(1)
                                };
                                animais.Add(animal);
                            }
                        }



                    }

                }
                return animais;

            }
        }
    }



