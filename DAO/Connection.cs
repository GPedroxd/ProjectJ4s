using MongoDB.Driver;

namespace ProjectJ4s.DAO
{
    public static class Connection 
    {
        static MongoClient client { get; set; }
        public static MongoClient connect()
        {
            //mongodb://127.0.0.1:27017/
            //mongodb+srv://gpedroxd:BrKPzxKUlU4euCjf@cluster0.0zszt.mongodb.net/?retryWrites=true&w=majority
            try{
                return  new MongoClient("mongodb+srv://gpedroxd:pcZZ3a9K1jk8gfbj@cluster0.0zszt.mongodb.net/");
            }
            catch(MongoException e)
            {
                throw new MongoException(e.Message);
            }
           
        }
        
    }
}