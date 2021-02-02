using System;
using System.Collections.Generic;
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
                    Console.WriteLine("Digite a quantidade por pagina");
                    int.TryParse(Console.ReadLine(), out int perpage1);
                    list(perpage1, 1);
                    break;
                case menu1.Editar:
                    Console.WriteLine("editando");
                    break;
                case menu1.Deletar:
                    Console.WriteLine("deletando");
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
            pc.add(name, dateBirth);
            Console.WriteLine("Cadastrado com sucesso!");
        }
        public static void list(int perpage, int page)
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
            if(page == 1 && perpage > peaple.Count)
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
            list(perpage: perpage, page: page);
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
