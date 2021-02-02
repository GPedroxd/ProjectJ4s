using MongoDB.Driver;
using System.Collections.Generic;
using ProjectJ4s.Models;
namespace ProjectJ4s.DAO
{
    public class PersonDAO 
    {
        MongoClient Client { get; set; }
        IMongoDatabase DataBase { get; set; }
        IMongoCollection <Person> Tabela { get; set;} 
        public PersonDAO()
        {
            try{
                this.Client = Connection.connect();
                this.DataBase =  this.Client.GetDatabase("ProjectJ4s");
                this.Tabela = this.DataBase.GetCollection<Person>("person");
            }
            catch(MongoException e)
            {
                throw new MongoException(e.Message);
            }
        }
        public PersonDAO add(Person person)
        {
            try{
                this.Tabela.InsertOne(person);
                return this;
            }
            catch(MongoException e)
            {
                throw new MongoException(e.Message);
            }  
        }
        public List<Person> listall(params int[] param)
        {
            try
            {
                return this.Tabela.Find(pp => true).Skip(param[1]).Limit(param[0]).ToList();
            } 
            catch(MongoException e)
            {
                throw new MongoException(e.Message);
            }
        }
        public int GetTotal(){
            return this.Tabela.Find( a => true).ToList().Count;
        }
    }
}