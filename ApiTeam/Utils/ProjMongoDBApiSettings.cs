namespace ApiTeam.Utils
{
    public class ProjMongoDBApiSettings : IProjMongoDBApiSettings
    {
        public string TeamCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
