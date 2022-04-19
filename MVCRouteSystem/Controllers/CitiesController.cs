using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCRouteSystem.Models;
using Newtonsoft.Json;

namespace MVCRouteSystem.Controllers
{
	public class CitiesController : Controller
	{
        public async Task<IActionResult> Index()
        {
            HttpClient apiConnection = new HttpClient();

            HttpResponseMessage cities = await apiConnection.GetAsync("https://localhost:44378/api/Cities");
            string responseBody = await cities.Content.ReadAsStringAsync();
            List<City> citiesList = JsonConvert.DeserializeObject<List<City>>(responseBody);


            return View(citiesList);
        }

        public IActionResult Create(City cityIn)
        {
            if (string.IsNullOrEmpty(cityIn.Name))
            {
                return View(cityIn);
            }
            else
            {
                using (var httpClient = new HttpClient())
                {
                    if (httpClient.BaseAddress == null) httpClient.BaseAddress = new Uri("https://localhost:44378/api/");
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    httpClient.PostAsJsonAsync("Cities", cityIn).Wait();


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

            HttpResponseMessage cities = await apiConnection.GetAsync("https://localhost:44378/api/Cities/" + id);
            string responseBody = await cities.Content.ReadAsStringAsync();
            var city = JsonConvert.DeserializeObject<City>(responseBody);

            return View(city);
        }

        public async Task<IActionResult> Edit(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction(nameof(Index));
            }
            HttpClient apiConnection = new HttpClient();

            HttpResponseMessage cities = await apiConnection.GetAsync("https://localhost:44378/api/Cities/" + id);
            string responseBody = await cities.Content.ReadAsStringAsync();
            var city = JsonConvert.DeserializeObject<City>(responseBody);

            return View(city);

        }

        [HttpPost]
        public IActionResult Edit(string id, [Bind("Id,Name,IsAvailable")] City city)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction(nameof(Index));
            }
            HttpClient apiConnection = new HttpClient();

            apiConnection.PutAsJsonAsync($"https://localhost:44378/api/Cities/{id}", city).Wait();



            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction(nameof(Index));
            }
            HttpClient apiConnection = new HttpClient();

            apiConnection.DeleteAsync("https://localhost:44378/api/Cities/" + id).Wait();


            return RedirectToAction(nameof(Index));

        }
    }
}
