using System;
using ProjectJ4s.DAO.Interfaces;

namespace ProjectJ4s.DAO
{
    public class ProjectJ4sDatabaseSettings : IProjectJ4sDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }
}
