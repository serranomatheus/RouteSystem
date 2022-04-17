using System.Collections.Generic;
using ApiTeam.Utils;
using Models;
using MongoDB.Driver;

namespace ApiTeam.Services
{
    public class TeamService
    {
        private readonly IMongoCollection<Team> _team;

        public TeamService(IProjMongoDBApiSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _team = database.GetCollection<Team>(settings.TeamCollectionName);
        }

        public Team GetName(string name) =>
            _team.Find<Team>(team => team.Name == name).FirstOrDefault();
        public List<Team> Get() =>
            _team.Find(team => true).ToList();

        public Team Get(string id) =>
            _team.Find<Team>(team => team.Id == id).FirstOrDefault();

        public Team Create(Team team)
        {
            _team.InsertOne(team);
            return team;
        }

        public void Update(string id, Team teamIn) =>
            _team.ReplaceOne(team => team.Id == id, teamIn);

        public void Remove(Team teamIn) =>
            _team.DeleteOne(team => team.Id == teamIn.Id);

        public void Remove(string id) =>
            _team.DeleteOne(team => team.Id == id);
    }
}
