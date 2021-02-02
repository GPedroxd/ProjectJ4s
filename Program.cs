using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProjectJ4s.Controllers;
using ProjectJ4s.Models;
namespace ProjectJ4s
{
    public class Program
    {
        enum menu1 { Default = 0, Cadastra = 1, Listar = 2, Editar = 3, Deletar = 4, Sair = 9}
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            /*PersonController ps = new PersonController();
            Console.WriteLine("Digite seu nome...");
            string name = Console.ReadLine();
            Console.WriteLine("Digite sua data de nascimento separado por barra. Ex. dd/mm/aaaa");
            string dateBirth = Console.ReadLine();
            ps.add(name, dateBirth);*/
            int index = 12;
            do {
                Console.WriteLine("1-Cadastrar\n2-Listar\n3-Editar\n4-Deletar\n9-sair");
                int.TryParse(Console.ReadLine(), out index);
                menu1 opselected = (menu1) index;
            switch(opselected){
                case menu1.Cadastra:
                    add();
                    break;
                case menu1.Listar:
                        list();
                    break;
                case menu1.Editar:
                        edit();
                    break;
                case menu1.Deletar:
                        delete();
                    break;
                case menu1.Sair:
                Console.WriteLine("Até a proxima");
                    break;
                case menu1.Default:
                    Console.WriteLine("opção invalida"); 
                    break;
                default:
                     Console.WriteLine("opção invalida");
                     break;  
            }

            }while(index != 9);
            
        }
        public static void add()
        {
            PersonController pc = new PersonController();
            Console.WriteLine("Digite seu nome...");
            string name = Console.ReadLine();
            Console.WriteLine("Digite sua data de nascimento separado por barra. Ex. dd/mm/aaaa");
            string dateBirth = Console.ReadLine();
            Console.WriteLine(pc.add(name, dateBirth));
        }
        public static void listAll(int perpage, int page)
        {
            PersonController pc = new PersonController();
            string menu = "9-Sair\t1-Proxima";
            List<Person> peaple = pc.list(perpage, page);
            Console.WriteLine($"=========List=========page {page}");
            foreach(Person p in peaple)
            {
                Console.WriteLine($"Id: {p.Id} - Nome: {p.Name} - Data de Nascimento: {p.dateBirth.ToString("dd/MM/yyyy")}");
            }
            Console.WriteLine("=======End List=======");
            if(pc.GetTotalPages(perpage)  == 1)
            {
                return;
            }
            if(page > 1 && perpage == peaple.Count)
            {
                menu = "9-Sair\t1-Proxima\t2-anterior";
            }
            if(page  == pc.GetTotalPages(perpage) && perpage >= peaple.Count)
            {
                menu = "9-Sair\t2-anterior";
            }
            
            Console.WriteLine(menu);
            int.TryParse(Console.ReadLine(), out int op);
            if(op == 9)
            {
                return;
            }
            if(op == 1)
            {
                page++;
                if(page >= pc.GetTotalPages(perpage))
                {
                    page =  pc.GetTotalPages(perpage);
                }
            }
            if(op == 2)
            {
                if(page != 1)
                {
                    page--;
                }
            }
            listAll(perpage: perpage, page: page);
        }
        public static void list(){
            int op = 0;
            do
            {
                Console.WriteLine("1-listar todos\n2-Buscar pessoa\n9-sair");
                int.TryParse(Console.ReadLine(), out op);
                switch(op){
                case 1:
                    Console.WriteLine("digite a quantidede por pagina");
                    int.TryParse(Console.ReadLine(), out int o);
                        if(o == 0)
                        {
                            listAll(4, 1);
                        }
                        listAll(o, 1);
                    break;
                case 2:
                    GetOne();
                    break;
                case 9:
                    continue;
                case 0:
                    break;
            }
            }while(op != 9);
            
        }
        public static void GetOne()
        {
            PersonController pc = new PersonController();
            Console.WriteLine("Digite o id");
            string id = Console.ReadLine();
            Person person = pc.GetOne(id);
            Console.WriteLine($"=========Buscando id:{id}");
            if(person == null)
            {
                Console.WriteLine("pessoa não encotrada");
                Console.WriteLine("======= Fim Lista =======");
                return;
            }
                Console.WriteLine($"Id: {person.Id} - Nome: {person.Name} - Data de Nascimento: {person.dateBirth.ToString("dd/MM/yyyy")}");
                Console.WriteLine("======= Fim Lista ======="); 
        }
        public static void edit()
        {
            PersonController pc = new PersonController();
            Console.WriteLine("Digite o  Id de que deseja editar");
            string id = Console.ReadLine();
            Person person = pc.GetOne(id);
            if (person == null)
            {
                Console.WriteLine("pessoa não encotrada");
                Console.WriteLine("======================");
                return;
            }
            Console.WriteLine("Digite o novo nome");
            string name = Console.ReadLine();
            Console.WriteLine("Digite a nova data de nascimento");
            string date = Console.ReadLine();
            Console.WriteLine(pc.edit(name, date, person));

        }
        public static void delete()
        {
            PersonController pc = new PersonController();
            Console.WriteLine("Digite o  Id de que deseja deletar");
            string id = Console.ReadLine();
            Console.WriteLine(pc.delete(id));
        }
        /*public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });*/
    }
}
