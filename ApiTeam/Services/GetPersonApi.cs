using System.Net.Http;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;

namespace ApiTeam.Services
{
    public class GetPersonApi
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
    }
}
