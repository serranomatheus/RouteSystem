using System.Collections.Generic;
using ApiCity.Utils;
using Models;
using MongoDB.Driver;

namespace ApiCity.Services
{
    public class CityService
    {
        private readonly IMongoCollection<City> _city;

        public CityService(IProjMongoDBApiSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _city = database.GetCollection<City>(settings.CityCollectionName);
        }
       
        public List<City> Get() =>
            _city.Find(city => true).ToList();

        public City Get(string id) =>
            _city.Find<City>(city => city.Id == id).FirstOrDefault();

        public City Create(City city)
        {
            _city.InsertOne(city);
            return city;
        }

        public void Update(string id, City cityIn) =>
            _city.ReplaceOne(city => city.Id == id, cityIn);

        public void Remove(City cityIn) =>
            _city.DeleteOne(city => city.Id == cityIn.Id);

        public void Remove(string id) =>
            _city.DeleteOne(city => city.Id == id);
    }
}

