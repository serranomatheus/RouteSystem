namespace ApiTeam.Utils
{
    public interface IProjMongoDBApiSettings
    {
        string TeamCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
