namespace ApiCity.Utils
{
    public class ProjMongoDBApiSettings : IProjMongoDBApiSettings
    {
        public string CityCollectionName { get ; set ; }
        public string ConnectionString { get ; set  ; }
        public string DatabaseName { get ; set; }
    }
}
