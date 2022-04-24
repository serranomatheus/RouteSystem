using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCRouteSystem.Data;
using MVCRouteSystem.Models;
using MVCRouteSystem.Services;

namespace MVCRouteSystem.Controllers
{
    [Authorize]
    public class RoutesController : Controller
    {
        IWebHostEnvironment _appEnvironment;
        private static string _pathFile;
        public RoutesController(IWebHostEnvironment env)
        {
            _appEnvironment = env;
        }

        private static int _columnService;
        private static List<List<string>> _routeFile;
        private static List<string> _servicesList;
        private static List<string> _headerList;
        private static string _routeCity;
        public static List<string> addressList = new List<string>{
            "numero",
            "cep",
            "bairro",
            "complemento"
        };

        //GET: Routes
        [HttpPost]
        public async Task<IActionResult> Index(IFormFile pathFile)
        {
            _routeCity = Request.Form["routeCity"];
            string routeCity;

            if (_routeCity == "selectCity")
            {
                string error = ("Favor selecionar uma cidade");
                TempData["errorList"] = error;
                return RedirectToRoute(new { controller = "Home", Action = "Index" });

            }

            if (pathFile == null)
            {
                string error = ("Favor importar o arquivo");
                TempData["errorList"] = error;
                return RedirectToRoute(new { controller = "Home", Action = "Index" });

            }

            (_routeFile, _servicesList, _headerList, _columnService, routeCity) = ReaderFiles.ReaderFileXlsx(pathFile, _routeCity);

            if (routeCity == "error")
            {

                string error = ("Cidade selecionada nao possui Servicos no arquivo. Favor selecionar outra cidade");
                TempData["errorList"] = error;
                return RedirectToRoute(new { controller = "Home", Action = "Index" });
            }

            var cityTeams = await ServicesApi.GetCityTeams(_routeCity);

            if (cityTeams == null)
            {
                string error = ("Cidade selecionada nao possui times. Favor cadastrar times antes de gerar rotas");
                TempData["errorList"] = error;
                return RedirectToRoute(new { controller = "Home", Action = "Index" });

            }
          
            ViewBag.servicesList = _servicesList;
            ViewBag.routeCity = _routeCity;

            return View(_headerList);
        }

        //[HttpPost]

        public IActionResult Create()
        {
            var routeService = Request.Form["routeService"];
            var routeTeams = Request.Form["routeTeams"].ToList();
            var selectColumn = Request.Form["selectColumn"].ToList();

            if (ModelState.IsValid)
            {
                _pathFile = WriterFiles.WriterFileXlsx(routeService, _routeCity, routeTeams, selectColumn, _routeFile, _appEnvironment.WebRootPath, _columnService, _headerList);



                return View();
            }

            return View();
        }
        
        
        public async Task<FileContentResult> DownloadFile()
        {
            var fileName = _pathFile.Split("//").ToList();
            var file = System.IO.File.ReadAllBytes(_pathFile);
            return  File(file, "application/octet-stream", fileName.Last().ToString());
        }
    }
}


