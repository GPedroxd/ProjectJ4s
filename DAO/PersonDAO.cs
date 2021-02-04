using System;
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
                Console.WriteLine(e.Message);
            }
        }
        public Person add(Person person)
        {
            try{
                this.Tabela.InsertOne(person);
                return person;
            }
            catch(MongoException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }  
        }
        public Person edit(Person person)
        {
            try
            {
                this.Tabela.ReplaceOne(d => d.Id == person.Id, person);
                return person;
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public bool delete(Person person)
        {
            try
            {
                this.Tabela.DeleteOne(d => d.Id == person.Id);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
        public List<Person> ListAll(params int[] param)
        {
            try
            {
                return this.Tabela.Find(pp => true).Skip(param[1]).Limit(param[0]).ToList();
            } 
            catch(MongoException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public Person GetOne(string id)
        {
            try
            {
                return this.Tabela.Find(a => a.Id == id).Single();
            }
            catch(MongoException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public int GetTotal()
        {
            return this.Tabela.Find( a => true).ToList().Count;
        }
    }
}