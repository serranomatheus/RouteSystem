using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MVCRouteSystem.Models;
using Newtonsoft.Json;

namespace MVCRouteSystem.Services
{
	public class ServicesApi
    {
        public static async Task<List<Person>> GetPeopleNoTeam()
        {
            HttpClient apiConnection = new HttpClient();

            HttpResponseMessage people = await apiConnection.GetAsync("https://localhost:44355/api/People/People No Team");
            string responseBody = await people.Content.ReadAsStringAsync();
            List<Person> peopleList = JsonConvert.DeserializeObject<List<Person>>(responseBody);
            if (peopleList == null)
                return null;
            return peopleList;
        }

        public static async Task<Person> GetPerson(string id)
        {
            HttpClient apiConnection = new HttpClient();

            HttpResponseMessage people = await apiConnection.GetAsync("https://localhost:44355/api/People/" + id);
            string responseBody = await people.Content.ReadAsStringAsync();
            var person = JsonConvert.DeserializeObject<Person>(responseBody);
            if (person == null)
                return null;
            return person;
        }
        public static async Task<Team> GetTeam(string id)
        {
            HttpClient apiConnection = new HttpClient();

            HttpResponseMessage teams = await apiConnection.GetAsync("https://localhost:44390/api/Teams/" + id);
            string responseBody = await teams.Content.ReadAsStringAsync();
            var team = JsonConvert.DeserializeObject<Team>(responseBody);
            if (team == null)
                return null;
            return team;
        }
        public static async Task<List<Team>> GetTeams()
		{
            HttpClient apiConnection = new HttpClient();

            HttpResponseMessage teams = await apiConnection.GetAsync("https://localhost:44390/api/Teams");
            string responseBody = await teams.Content.ReadAsStringAsync();
            List<Team> teamsList = JsonConvert.DeserializeObject<List<Team>>(responseBody);


            return teamsList;
        }
        public static async Task<List<Team>> GetCityTeams(string city)
        {
            HttpClient apiConnection = new HttpClient();

            HttpResponseMessage teams = await apiConnection.GetAsync("https://localhost:44390/api/Teams/CityTeams?city="+city);
            string responseBody = await teams.Content.ReadAsStringAsync();
            List<Team> teamsList = JsonConvert.DeserializeObject<List<Team>>(responseBody);
            if (teamsList.Count == 0)
                return null;
            return teamsList;
        }
        public static void UpdatePerson(string id, Person person)
        {
            HttpClient apiConnection = new HttpClient();

            apiConnection.PutAsJsonAsync($"https://localhost:44355/api/People/{id}", person).Wait();
        }

        public static async Task<List<Person>> GetTeamMembers(string teamName)
        {
            HttpClient apiConnection = new HttpClient();

            HttpResponseMessage people = await apiConnection.GetAsync("https://localhost:44355/api/People/TeamMembers?teamName=" + teamName);
            string responseBody = await people.Content.ReadAsStringAsync();
            List<Person> peopleList = JsonConvert.DeserializeObject<List<Person>>(responseBody);
            if (peopleList == null)
                return null;
            return peopleList;
        }

        public static async Task<List<City>> GetCities()
        {
            HttpClient apiConnection = new HttpClient();

            HttpResponseMessage cities = await apiConnection.GetAsync("https://localhost:44378/api/Cities");
            string responseBody = await cities.Content.ReadAsStringAsync();
            List<City> citiesList = JsonConvert.DeserializeObject<List<City>>(responseBody);
            if (citiesList == null)
                return null;
            return citiesList;
        }

        public static async void UpdateTeam(string id, Team team)
        {
            HttpClient apiConnection = new HttpClient();
            apiConnection.PutAsJsonAsync($"https://localhost:44390/api/Teams/{id}", team).Wait();
        }

        public static async Task<City> GetCity(string id)
        {
            HttpClient apiConnection = new HttpClient();

            HttpResponseMessage cities = await apiConnection.GetAsync("https://localhost:44378/api/Cities/" + id);
            string responseBody = await cities.Content.ReadAsStringAsync();
            var city = JsonConvert.DeserializeObject<City>(responseBody);
            return city;
        }
    }
}
