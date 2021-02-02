using System;
using ProjectJ4s.DAO;
using ProjectJ4s.Models;
using System.Collections.Generic;
using ProjectJ4s.Middlewares;

namespace ProjectJ4s.Controllers
{
    public class PersonController 
    {
        PersonDAO personDAO { get; set; }
        public PersonController(){
            this.personDAO = new PersonDAO();
        }
        public String add (string name, string dateBirth)
        {
            Person person = new PersonMiddleware().ValidadeDataPerson(name, dateBirth);
            if (person == null)
            {
                return "dados invalidos, tente novamente!";
            }
            
            if (this.personDAO.add(person) == null)
            {
                return "erro ao cadastrar! tento novamente mais tarde.";
            }
            return "Cadastrado com sucesso!";
        }
        public List<Person> list (params int[] param)
        {
            if(param[0] == 0){
                param[0] = 4;
            }
            param[1] = param[0] * (param[1] - 1);
            return personDAO.listall(param);
        }
        public Person GetOne(string id)
        {
            return new PersonMiddleware().ValidadeId(id);
        }
        public int GetTotalPages(int perpage){
            double a = (double)personDAO.GetTotal() / (double)perpage;
            return (int)Math.Ceiling(a);
        }
        public string edit(string name, string dateBirth, Person person)
        {
           person = new PersonMiddleware().ValidadeDataPerson(name, dateBirth, person);
            if (person == null)
            {
                return "dados invalidos, tente novamente!";
            }

            if (this.personDAO.edit(person) == null)
            {
                return "erro ao editar! tento novamente mais tarde.";
            }
            return "Editado com sucesso!";
        }
        public string delete(string id)
        {
            Person person = new PersonMiddleware().ValidadeId(id);
            if (person == null)
            {
                return "Pessoa não encontrada!";
            }
            if (!this.personDAO.delete(person))
            {
                return "Erro ao deletar! tente novamente mais tarde.";
            }
            return "Deletado com sucesso";
        }
    }
}