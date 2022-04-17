namespace ApiCity.Utils
{
    public interface IProjMongoDBApiSettings
    {
        string CityCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
