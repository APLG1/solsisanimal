using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using sysanimalmodel;
using sysanimalcontroller;


namespace sysanimalview_csa
{
    public class Menu
    {
       private especies especies = new especies();
        private Especie especie;

        private animais animais = new animais();

        private clientes clientes = new clientes();
        int subopcao = 0;
        
        private void exibirSubMenuCliente()

        {
            var nome = "";
            while (subopcao != 39)
            {
                Console.WriteLine("\n");
                Console.WriteLine("*****CLIENTE*****");
                Console.WriteLine("30. Inserir");
                Console.WriteLine("31. Alterar");
                Console.WriteLine("32. Excluir");
                Console.WriteLine("33. Pesquisar");
                Console.WriteLine("34. Exibir");
                Console.WriteLine("39. Sair");
                Console.WriteLine("Digite a opcao: ");
                subopcao = int.Parse(Console.ReadLine());

                switch (subopcao)
                {
                    case 30:
                        Console.WriteLine("digite nome do cliente");
                        Cliente cliente = new Cliente();
                        cliente.nome = Console.ReadLine();
                        Console.WriteLine("\n");
                        Console.WriteLine("digite cpf");
                        cliente.cpf = Console.ReadLine();
                        Console.WriteLine("\n");
                        Console.WriteLine("digite email para contato");
                        cliente.email = Console.ReadLine();
                        Console.WriteLine("\n");
                        Console.WriteLine("digite numero para contato");
                        cliente.numerot = Console.ReadLine();
                        Console.WriteLine("\n");

                        clientes.inserir(cliente);
                        break;

                    case 31:
                        Console.WriteLine("digite o nome do cliente que deseja alterar");
                        nome = Console.ReadLine();



                        Cliente cliente1 = new Cliente();
                        cliente1.nome = Console.ReadLine();
                        cliente1.cpf = Console.ReadLine();
                        cliente1.email = Console.ReadLine();
                        cliente1.numerot = Console.ReadLine();

                        clientes.alterar(nome, cliente1.cpf, cliente1.email, cliente1.numerot, cliente1);



                        break;
                    case 32:

                        Console.WriteLine("Digite o cliente que deseja excluir");
                        nome = Console.ReadLine();
                        clientes.excluir(nome);

                        break;
                    case 33:

                        Console.WriteLine("digite o nome do cliente  que deseja pesquisar");
                        nome = Console.ReadLine();
                        var cliente2 = clientes.pesquisar(nome);
                        if (cliente2.nome == nome)
                        {
                            Console.WriteLine(cliente2.nome + "\n");
                            Console.WriteLine(cliente2.cpf + "\n");
                            Console.WriteLine(cliente2.email + "\n");
                            Console.WriteLine(cliente2.numerot + "\n");
                        }
                        break;





                    case 34:
                        var listacliente = clientes.exibirTodos();
                        Console.WriteLine("lista de todos os clientes e suas respectivas informações");
                        foreach (var item in listacliente)
                        {
                            
                            Console.WriteLine(item.nome + " - " + item.cpf + " - " + item.email + " - " + item.numerot + "\n");

                        }
                        break;

                }
            }
        }
        private void exibirSubMenuAnimal()
        {
            var nome = "";
            while (subopcao != 29)
            {
                Console.WriteLine("\n");
                Console.WriteLine("*****ANIMAL*****");
                Console.WriteLine("20. Inserir");
                Console.WriteLine("21. Alterar");
                Console.WriteLine("22. Excluir");
                Console.WriteLine("23. Pesquisar");
                Console.WriteLine("24. Exibir");
                Console.WriteLine("29. Sair");
                Console.WriteLine("Digite a opcao: ");
                subopcao = int.Parse(Console.ReadLine());

                switch (subopcao)
                {
                    case 20:
                        Console.WriteLine("Digite o nome do animal");
                        Animal animal = new Animal();
                        animal.nome = Console.ReadLine();
                        Console.WriteLine("Digite o apelido do animal");
                        animal.apelido = Console.ReadLine();
                        Console.WriteLine("Digite a data de nascimento");
                        animal.datanas = Console.ReadLine();
                        Console.WriteLine("Observações");
                        animal.obs = Console.ReadLine();


                        animais.inserir(animal);
                        break;

                    case 21:
                        Console.WriteLine("digite o nome que deseja alterar");
                        nome = Console.ReadLine();


                        Animal Class1 = new Animal();
                        Class1.nome = Console.ReadLine();
                        Class1.apelido = Console.ReadLine();
                        Class1.datanas = Console.ReadLine();
                        Class1.obs = Console.ReadLine();

                        animais.alterar(nome, Class1.apelido, Class1.datanas, Class1.obs, Class1);


                        break;

                    case 22:

                        Console.WriteLine("Digite o animal que deseja excluir");
                        nome = Console.ReadLine();
                        animais.excluir(nome);



                        break;

                    case 23:
                       
                        Console.WriteLine("digite o nome do animal  que deseja pesquisar");
                        nome = Console.ReadLine();
                        var animal1 = animais.pesquisar(nome);
                        if (animal1.nome == nome)
                        {
                            Console.WriteLine( animal1.nome + "\n");
                            Console.WriteLine(animal1.apelido + "\n");
                            Console.WriteLine(animal1.datanas + "\n");
                            Console.WriteLine(animal1.obs + "\n");
                        }
                        break;

                    case 24:

                        var listaanimal = animais.exibirTodos();
                        foreach (var item in listaanimal)
                        {
                            Console.WriteLine(item.nome + " - " + item.apelido + "\n");

                        }
                        break;
                }
            }
        }
        private void exibirSubMenuEspecie()
        {
            var nome = "";
            while (subopcao != 19)
            {
                Console.WriteLine("\n");
                Console.WriteLine("*****ESPECIE*****");
                Console.WriteLine("10. Inserir");
                Console.WriteLine("11. Alterar");
                Console.WriteLine("12. Excluir");
                Console.WriteLine("13. Pesquisar");
                Console.WriteLine("14. Exibir");
                Console.WriteLine("19. Sair");
                Console.WriteLine("Digite a opcao: ");
                subopcao = int.Parse(Console.ReadLine());

                switch (subopcao)
                {
                    case 10:
                        Console.WriteLine("insira o codigo que sera introduzida a especie");
                        especie = new Especie();
                        especie.codigo = int.Parse(Console.ReadLine());
                        Console.WriteLine("digite o nome da especie");

                        especie.nome = Console.ReadLine();

                        especies.inserir(especie);



                        break;

                    case 11:

                        Console.WriteLine("digite o nome da especie que deseja alterar");
                        nome = Console.ReadLine();

                        Especie especie1 = new Especie();
                        Console.WriteLine("digite o novo codigo para especie");
                        especie1.codigo = int.Parse(Console.ReadLine());
                        Console.WriteLine("digite o nome da especie ");
                        especie1.nome = Console.ReadLine();
                        

                        especies.alterar(nome, especie1);



                        break;


                    case 12:

                        Console.WriteLine("Digite a especie que deseja excluir");
                        nome = Console.ReadLine();
                        especies.excluir(nome);

                        break;

                    case 13:
                        
                        Console.WriteLine("digite o nome da especie que deseja pesquisar");
                        nome = Console.ReadLine();
                          var Especie = especies.pesquisar(nome);
                        if(Especie.nome == nome)
                        {
                            Console.WriteLine(Especie.codigo + " " + Especie.nome +"\n");
                        }
                        break;

                    case 14:
                        
                        var listaespecie = especies.exibirTodos();
                        foreach (var item in listaespecie)
                        {
                            Console.WriteLine(item.codigo + " " + item.nome + "\n");

                        }
                        break;
                }
                
            }
                
        }
        public void exibir()
        {
            int opcao = 0;
            while (opcao != 9)
            {

                Console.WriteLine("SISVET");
                Console.WriteLine("1. Especie");
                Console.WriteLine("2. Animal");
                Console.WriteLine("3. Cliente");
                Console.WriteLine("4. animalcliente");
                Console.WriteLine("9. Sair");
                Console.WriteLine("Digite a opcao: ");
                opcao = int.Parse(Console.ReadLine());

                if (opcao == 1)
                {

                    exibirSubMenuEspecie();

                }
                else if (opcao == 2)
                {

                    exibirSubMenuAnimal();

                }
                else if (opcao == 3)
                {

                    exibirSubMenuCliente();

                }
                else if (opcao == 4)
                {

                }

            }
            especies.salvarEspeciesEmCsv();
            animais.salvarAnimalEmCsv();
            clientes.salvarClienteEmCsv();

        }
    }
}
    
  
        
    

