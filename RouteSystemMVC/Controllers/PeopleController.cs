using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RouteSystemMVC.Models;

namespace RouteSystemMVC.Controllers
{
    public class PeopleController : Controller
    {

        public async Task<IActionResult> Index()
        {
            HttpClient apiConnection = new HttpClient();

            HttpResponseMessage people = await apiConnection.GetAsync("https://localhost:44355/api/People");
            string responseBody = await people.Content.ReadAsStringAsync();
            List<Person> peopleList = JsonConvert.DeserializeObject<List<Person>>(responseBody);


            return View(peopleList);
        }

        public IActionResult Create(Person personIn)
        {
            if (string.IsNullOrEmpty(personIn.Name))
            {
                return View(personIn);
            }
            else
            {
                using (var httpClient = new HttpClient())
                {
                    if (httpClient.BaseAddress == null) httpClient.BaseAddress = new Uri("https://localhost:44355/api/");
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    httpClient.PostAsJsonAsync("People", personIn).Wait();


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

            HttpResponseMessage people = await apiConnection.GetAsync("https://localhost:44355/api/People/" + id);
            string responseBody = await people.Content.ReadAsStringAsync();
            var person = JsonConvert.DeserializeObject<Person>(responseBody);

            return View(person);
        }

        public async Task<IActionResult> Edit(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction(nameof(Index));
            }
            HttpClient apiConnection = new HttpClient();

            HttpResponseMessage people = await apiConnection.GetAsync("https://localhost:44355/api/People/" + id);
            string responseBody = await people.Content.ReadAsStringAsync();
            var person = JsonConvert.DeserializeObject<Person>(responseBody);

            return View(person);

        }

        [HttpPost]
        public IActionResult Edit(string id, [Bind("Id,Name,IsAvailable")] Person person)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction(nameof(Index));
            }
            HttpClient apiConnection = new HttpClient();

            apiConnection.PutAsJsonAsync($"https://localhost:44355/api/People/{id}", person).Wait();



            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction(nameof(Index));
            }
            HttpClient apiConnection = new HttpClient();

            apiConnection.DeleteAsync("https://localhost:44355/api/People/" + id).Wait();


            return RedirectToAction(nameof(Index));

        }
    }
}
