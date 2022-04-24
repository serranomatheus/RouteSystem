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
        public RoutesController(IWebHostEnvironment env)
        {
            _appEnvironment = env;
        }

        private static int _columnService;
        private static List<List<string>> _routeFile;
        private static List<string> _servicesList;
        private static List<string> _headerList;
        private static string _routeCity;
       
        //GET: Routes
        [HttpPost]
        public async Task<IActionResult> Index(IFormFile pathFile)
        {
            _routeCity = Request.Form["routeCity"];
            (_routeFile, _servicesList, _headerList,_columnService) = ReaderFiles.ReaderFileXlsx(pathFile, _routeCity);
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
                WriterFiles.WriterFileXlsx(routeService, _routeCity, routeTeams, selectColumn, _routeFile, _appEnvironment.WebRootPath,_columnService, _headerList);

                return RedirectToRoute(new { controller = "Home", Action = "Index" });
            }
            return View();
        }
    }
}


