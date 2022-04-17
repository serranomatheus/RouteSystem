using System.Collections.Generic;
using System.Threading.Tasks;
using ApiCity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ApiCity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly CityService _cityService;
        public CitiesController(CityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]


        public ActionResult<List<City>> Get() =>
            _cityService.Get();


        [HttpGet("{id:length(24)}", Name = "GetCity")]


        public ActionResult<City> Get(string id)
        {
            var city = _cityService.Get(id);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        [HttpPost]


        public ActionResult<City> Create(City city)
        {
            _cityService.Create(city);
            return CreatedAtRoute("GetCity", new { id = city.Id.ToString() }, city);

        }


        [HttpPut("{id:length(24)}")]


        public IActionResult Update(string id, City cityIn)
        {
            var city = _cityService.Get(id);

            if (city == null)
            {
                return NotFound();
            }            

            _cityService.Update(id, cityIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]


        public IActionResult Delete(string id)
        {
            var city = _cityService.Get(id);

            if (city == null)
            {
                return NotFound();
            }          

            _cityService.Remove(city.Id);

            return NoContent();
        }
    }
}
