
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using sysanimalmodel;
using System.Data;
using System.Data.SQLite;

namespace sysanimalcontroller

{

    
    public class especies
    {
        private string stringConexao = @"Data Source=c:\\as\\sysespecie.db; Version=3;";
        private List<Especie> bancoEspecie = new List<Especie>();

        
        Especie especie;

       private string caminhoBanco = ConfigurationManager.AppSettings["caminhoBanco"];
       private string nomeBancoEspecie = ConfigurationManager.AppSettings["nomeBancoEspecie"];
        
        public void salvarEspeciesEmCsv()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(caminhoBanco + nomeBancoEspecie))
                {
                    writer.WriteLine("codigo,nome");
                    foreach (var item in bancoEspecie)
                    {
                        writer.WriteLine(value: ($"{item.codigo},{item.nome}"));
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Ocorreu um erro:   {ex.Message}");
            }
        }

        private List<Especie> CarregarEspeciesDoCsv(string caminho)
        {
            var Especies = new List<Especie>();

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
                            if (partes.Length == 2)
                            {
                                int codigo = int.Parse(partes[0]);
                                string nome = partes[1];
                                Especies.Add(new Especie
                                {
                                    codigo = codigo,
                                    nome = nome
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
            return Especies;
            }
       

        public especies()
        {
            bancoEspecie = CarregarEspeciesDoCsv(caminhoBanco + nomeBancoEspecie);

        }

        
        public void inserir(Especie especie)
        {

            using (var conn = new SQLiteConnection(stringConexao))
            {
                conn.Open();
                string query = "INSERT INTO ESPECIE (CODIGO, NOME) VALUES (@CODIGO, @NOME)";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CODIGO", especie.codigo);
                    cmd.Parameters.AddWithValue("@NOME", especie.nome);
                    cmd.ExecuteNonQuery();
                }

            }
                     
            /*
                    bancoEspecie.Add(especie);
            */

                   

                }  
       
        public void alterar(string nome, Especie especie)
        {
            

            using (var conn = new SQLiteConnection(stringConexao))
            {
                conn.Open();
                string query = "UPDATE ESPECIE SET NOME = @NOME, CODIGO = @CODIGO WHERE CODIGO = CODIGO";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                   
                    cmd.Parameters.AddWithValue("@CODIGO", especie.codigo);
                  
                    cmd.Parameters.AddWithValue("@NOME", especie.nome);
                   
                    
                    
                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("ESPECIE ALTERADA COM SUCESSO!");
            }
            /*
            foreach (var item in bancoEspecie)
            {
               if (item.nome ==nome)
                {
                    
                    item.nome = especie.nome; 
                    item.codigo = especie.codigo;

                    break;
                }
            }
            */
        }

    
        


        public void excluir (string nome)
        {
            /*
                                   foreach (var item in bancoEspecie)
                                   {
                                       if (item.nome == nome.ToString().Trim())
                                       {
                                           bancoEspecie.Remove(item);
                                           Console.WriteLine("Especie excluida da lista ");
                                           break;
                                       }
                                   }

                                 */
            using (var conn = new SQLiteConnection(stringConexao))
            {
                conn.Open();
                string query = "DELETE FROM ESPECIE  WHERE NOME = @NOME";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NOME", nome);
                   

                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("ESPECIE EXCLUIDA COM SUCESSO!");
            }
        }
        public Especie  pesquisar  (string nome)
        {
           
            /*
             foreach (var item in bancoEspecie)
             {
                 if (item.nome == nome.ToString().Trim())
                 {
                     Console.WriteLine(item.codigo.ToString()
                         + " - " + item.nome.ToString()
                         + " - " + (item.nome.ToString()));
     break;
                 }
             }
            */
            using (var conn = new SQLiteConnection(stringConexao))
            {
                
                conn.Open();
                string query = "SELECT CODIGO, NOME FROM ESPECIE WHERE NOME = @NOME";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NOME", nome);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            especie = new Especie
                            {
                                codigo = reader.GetInt32(0),
                                nome = reader.GetString(1)
                                
                                
                            };
                        }
                    }


                    
                }
           
            }
            return especie;
        }

        /*
        public void exibir()
        {
 

                                foreach (var item in bancoEspecie)
                                {
                                    Console.WriteLine($"{item.codigo.ToString()} - {item.nome}");

                                }

                                
                            }

        */

        public List<Especie> exibirTodos()
        {
            List<Especie > especies = new List<Especie>();

            using (var conn = new SQLiteConnection(stringConexao))
            {

                conn.Open();
                string query = "SELECT CODIGO, NOME FROM ESPECIE ";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                   
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())                       
                        {
                           var especie = new Especie
                            {
                                codigo = reader.GetInt32(0),
                                nome = reader.GetString(1)
                            };
                            especies.Add(especie);
                        }
                    }



                }

            }
            return especies;

        }
    }
}

