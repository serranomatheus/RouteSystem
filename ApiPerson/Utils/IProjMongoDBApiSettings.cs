namespace ApiPerson.Utils
{
    public interface IProjMongoDBApiSettings
    {
        string PersonCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
