using System.Collections.Generic;
using System.Threading.Tasks;
using ApiTeam.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ApiTeam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly TeamService _teamService;
        public TeamsController(TeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]


        public ActionResult<List<Team>> Get() =>
            _teamService.Get();


        [HttpGet("{id:length(24)}", Name = "GetTeam")]


        public ActionResult<Team> Get(string id)
        {
            var team = _teamService.Get(id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }
        [HttpGet("CityTeams")]
        public ActionResult<List<Team>> GetCityTeams(string city) =>
            _teamService.GetCityTeams(city);
        



        [HttpPost]
        public async Task<ActionResult<Team>> CreateAsync(Team team)
        {
            if (!CheckNameTeam.CheckName(team.Name, _teamService))
            {
                return BadRequest("Ja existe um time com esse nome");
            }

            if (!await ApiPersonServices.GetPeopleNoTeamAsync())
            {
                return BadRequest("Nao é possivel cadastrar um time sem membros");
            }
            _teamService.Create(team);
            return CreatedAtRoute("GetTeam", new { id = team.Id.ToString() }, team);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Team teamIn)
        {
            var team = _teamService.Get(id);

            if (team == null)
            {
                return NotFound();
            }

            _teamService.Update(id, teamIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]


        public IActionResult Delete(string id)
        {
            var team = _teamService.Get(id);

            if (team == null)
            {
                return NotFound();
            }

            _teamService.Remove(team.Id);

            return NoContent();
        }
    }
}
