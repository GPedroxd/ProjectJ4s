namespace ProjectJ4s.DAO
{
    public interface IProjectJ4sDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
