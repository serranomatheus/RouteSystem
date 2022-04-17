using System.Collections.Generic;
using ApiPerson.Utils;
using Models;
using MongoDB.Driver;

namespace ApiPerson.Services
{
    public class PersonService
    {
        private readonly IMongoCollection<Person> _person;

        public PersonService(IProjMongoDBApiSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _person = database.GetCollection<Person>(settings.PersonCollectionName);
        }

        public List<Person> Get() =>
            _person.Find(person => true).ToList();

        public Person GetName(string name) =>
            _person.Find(person => person.Name.ToLower() == name.ToLower() && person.Team == null).FirstOrDefault();
        public List<Person> GetPeopleNoTeam() =>
            _person.Find(person => person.Team == null).ToList();
        public Person Get(string id) =>
            _person.Find<Person>(person => person.Id == id).FirstOrDefault();
        public List<Person> GetTeamMembers(string teamName) =>
            _person.Find(person => person.Team.Name.ToLower() == teamName.ToLower()).ToList();
        public Person Create(Person person)
        {
            _person.InsertOne(person);
            return person;
        }

        public void Update(string id, Person personIn) =>
            _person.ReplaceOne(person => person.Id == id, personIn);

        public void Remove(Person personIn) =>
            _person.DeleteOne(person => person.Id == personIn.Id);

        public void Remove(string id) =>
            _person.DeleteOne(person => person.Id == id);
    }
}
