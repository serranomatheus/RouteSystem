using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RouteSystemMVC.Models;
using RouteSystemMVC.Services;

namespace RouteSystemMVC.Controllers
{
    public class TeamsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            HttpClient apiConnection = new HttpClient();

            HttpResponseMessage teams = await apiConnection.GetAsync("https://localhost:44390/api/Teams");
            string responseBody = await teams.Content.ReadAsStringAsync();
            List<Team> teamsList = JsonConvert.DeserializeObject<List<Team>>(responseBody);


            return View(teamsList);
        }

        public async Task<IActionResult> Create(Team teamIn)
        {                      
                if (string.IsNullOrEmpty(teamIn.Name))
                {
                    return View(teamIn);
                }
                else
                {
                    using (var httpClient = new HttpClient())
                    {
                        if (httpClient.BaseAddress == null) httpClient.BaseAddress = new Uri("https://localhost:44390/api/");
                        httpClient.DefaultRequestHeaders.Accept.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                    var formCity = Request.Form["city"].ToList();
                    //foreach (var city in formCity)
                    //{
                    //    teamIn.City.Name = city;

                    //}
                    //httpClient.PostAsJsonAsync("Teams", teamIn).Wait();

                    var formPerson = Request.Form["person"].ToList();
                    foreach (var person in formPerson)
                        {
                            var personTeam = await ServicesApi.GetPerson(person);
                            if (personTeam != null)
                                ServicesApi.UpdatePerson(person, new Person() { Id = personTeam.Id, Name = personTeam.Name, Team = teamIn });

                        }
                   
                        return RedirectToAction(nameof(Index));
                    }
                
                }            
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction(nameof(Index));
            }
            HttpClient apiConnection = new HttpClient();

            HttpResponseMessage teams = await apiConnection.GetAsync("https://localhost:44390/api/Teams/" + id);
            string responseBody = await teams.Content.ReadAsStringAsync();
            var team = JsonConvert.DeserializeObject<Team>(responseBody);

            return View(team);
        }

        public async Task<IActionResult> Edit(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction(nameof(Index));
            }
            HttpClient apiConnection = new HttpClient();

            HttpResponseMessage teams = await apiConnection.GetAsync("https://localhost:44390/api/Teams/" + id);
            string responseBody = await teams.Content.ReadAsStringAsync();
            var team = JsonConvert.DeserializeObject<Team>(responseBody);

            return View(team);

        }

        [HttpPost]
        public IActionResult Edit(string id, [Bind("Id,Name,IsAvailable")] Team team)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction(nameof(Index));
            }
            HttpClient apiConnection = new HttpClient();

            apiConnection.PutAsJsonAsync($"https://localhost:44390/api/Teams/{id}", team).Wait();



            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction(nameof(Index));
            }
            HttpClient apiConnection = new HttpClient();

            apiConnection.DeleteAsync("https://localhost:44390/api/Teams/" + id).Wait();


            return RedirectToAction(nameof(Index));

        }
    }
}
