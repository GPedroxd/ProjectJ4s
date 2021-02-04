using System;
using MongoDB.Driver;
using System.Collections.Generic;
using ProjectJ4s.Models;
using ProjectJ4s.DAO.Interfaces;

namespace ProjectJ4s.DAO
{
    public class PersonDAO : IDAO
    {
        MongoClient Client { get; set; }
        IMongoDatabase DataBase { get; set; }
        readonly IMongoCollection<Person> _Colletion; 
        public PersonDAO(IProjectJ4sDatabaseSettings settings)
        {
            try{
                this.Client = Connection.connect(settings.ConnectionString);
                this.DataBase =  this.Client.GetDatabase(settings.DatabaseName);
                this._Colletion = this.DataBase.GetCollection<Person>("person");
            }
            catch(MongoException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public Person add(Person person)
        {
            try{
                this._Colletion.InsertOne(person);
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
                this._Colletion.ReplaceOne(d => d.Id == person.Id, person);
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
                this._Colletion.DeleteOne(d => d.Id == person.Id);
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
                return this._Colletion.Find(pp => true).Skip(param[1]).Limit(param[0]).ToList();
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
                return this._Colletion.Find(a => a.Id == id).Single();
            }
            catch(MongoException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public int GetTotal()
        {
            return this._Colletion.Find( a => true).ToList().Count;
        }
    }
}