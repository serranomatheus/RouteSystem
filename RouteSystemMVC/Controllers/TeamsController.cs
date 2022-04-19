﻿using System;
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

                    var peopleNoTeam = Request.Form["person"].ToList();
                    var formCity = Request.Form["city"].FirstOrDefault();

                    teamIn.City = formCity;

                    if(peopleNoTeam.Count ==0)
                    {
                        teamIn.Error.Add("Nao é possivel cadastrar um time sem membros");
                        TempData["errorList"] = teamIn.Error;
                        return View(teamIn);

                    }
                    var resposta = await httpClient.PostAsJsonAsync("Teams", teamIn);

                    if (resposta.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        teamIn.Error.Add(resposta.Content.ReadAsStringAsync().Result);
                        TempData["errorList"] = teamIn.Error;
                        return View(teamIn);
                    }

                    foreach (var person in peopleNoTeam)
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
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,IsAvailable")] Team team)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction(nameof(Index));
            }
            HttpClient apiConnection = new HttpClient();

            var peopleNoTeam = Request.Form["peopleNoTeam"].ToList();
            var peopleTeam = Request.Form["peopleTeam"].ToList();
            var formCity = Request.Form["city"].FirstOrDefault();
            var teamMembers =await  ServicesApi.GetTeamMembers(team.Name);
            team.City = formCity;
            if(peopleNoTeam.Count ==0 && peopleTeam.Count == teamMembers.Count)
            {
                team.Error.Add("Nao é possivel deixar um time sem membros");
                TempData["errorList"] = team.Error;
                return View(team);
            }
            apiConnection.PutAsJsonAsync($"https://localhost:44390/api/Teams/{id}", team).Wait();
            
            foreach (var person in peopleNoTeam)
            {
                var personTeam = await ServicesApi.GetPerson(person);
                if (personTeam != null)
                    ServicesApi.UpdatePerson(person, new Person() { Id = personTeam.Id, Name = personTeam.Name, Team = team });

            }
            
            foreach (var person in peopleTeam)
            {
                var personTeam = await ServicesApi.GetPerson(person);
                if (personTeam != null)
                    ServicesApi.UpdatePerson(person, new Person() { Id = personTeam.Id, Name = personTeam.Name, Team = null });

            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction(nameof(Index));
            }
            HttpClient apiConnection = new HttpClient();

            Team team = await ServicesApi.GetTeam(id);

            apiConnection.DeleteAsync("https://localhost:44390/api/Teams/" + id).Wait();

            var members = await ServicesApi.GetTeamMembers(team.Name);
            foreach (Person person in members)
            {

                ServicesApi.UpdatePerson(person.Id, new Person() { Id = person.Id, Name = person.Name, Team = null });

            }

            return RedirectToAction(nameof(Index));

        }
    }
}
