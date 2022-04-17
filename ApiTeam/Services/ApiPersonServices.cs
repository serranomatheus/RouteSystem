using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;

namespace ApiTeam.Services
{
    public class ApiPersonServices
    {
        public static async Task<Person> GetPerson(string name)
        {

            HttpClient ApiConnection = new HttpClient();

            HttpResponseMessage person = await ApiConnection.GetAsync("https://localhost:44355/api/People/Get Person for name?name=" + name);
            string responseBody = await person.Content.ReadAsStringAsync();
            var personIn = JsonConvert.DeserializeObject<Person>(responseBody);
            if (personIn.Name == null)
            {

                return null;
            }
            else
            {
                return personIn;

            }
        }

        public static async Task<bool> GetPeopleNoTeamAsync()
        {
           
            HttpClient apiConnection = new HttpClient();

            HttpResponseMessage people = await apiConnection.GetAsync("https://localhost:44355/api/People/People No Team");
            string responseBody = await people.Content.ReadAsStringAsync();
            List<Person> peopleList = JsonConvert.DeserializeObject<List<Person>>(responseBody);
            if (peopleList.Count == 0)
                return false;
            return true;

        }



    }
}
