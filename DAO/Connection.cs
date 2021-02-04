using MongoDB.Driver;

namespace ProjectJ4s.DAO
{
    public static class Connection 
    {
        public static MongoClient connect(string connection)
        {
            
            try{
                return  new MongoClient(connection);
            }
            catch(MongoException e)
            {
                throw new MongoException(e.Message);
            }
           
        }
        
    }
}