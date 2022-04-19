using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RouteSystemMVC.Data;
using RouteSystemMVC.Models;
using RouteSystemMVC.Services;

namespace RouteSystemMVC.Controllers
{
    public class RoutesController : Controller
    {
        private readonly RouteSystemMVCContext _context;
        
        public RoutesController(RouteSystemMVCContext context)
        {
            _context = context;
        }

        //GET: Routes
        [HttpPost]
        public async Task<IActionResult> Index(IFormFile pathFile)
        {
           
            var routes = ReaderFile.ReadFile(pathFile);
            string[] nameColum = new string[routes.GetLength(1)];
            for(int i=0; i<routes.GetLength(1); i++)
            {
                nameColum[i] = routes[0, i];
            }
            return View(nameColum);
        }

       //GET: Routes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Route
                .FirstOrDefaultAsync(m => m.Data == id);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        // GET: Routes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Routes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Data,Stats,Auditado,CopReverteu,Log,Pdf,Foto,Contrato,Wo,Os,Assinante,Tecnicos,Login,Matricula,Cop,UltimoAlterar,Local,PontoCasaApt,Cidade,Base,Horario,Segmento,Servico,TipoServico,TipoOs,GrupoServico,Endereco,Numero,Complemento,Cep,Node,Bairro,Pacote,Cod,Telefone1,Telefone2,Obs,ObsTecnico,Equipamento")] Route route)
        {
            if (ModelState.IsValid)
            {
                _context.Add(route);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(route);
        }
        
        //public  IActionResult WriterFileDocx()
        //{
        //    var pathFile = ReaderFile.ReadFile(@"C:\Users\matheus\Downloads\Rotas.xlsx");
        //    WriterFiles.WriterFile(pathFile);
        //    return RedirectToAction("Index");
        //}

        // GET: Routes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Route.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }
            return View(route);
        }

        // POST: Routes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Data,Stats,Auditado,CopReverteu,Log,Pdf,Foto,Contrato,Wo,Os,Assinante,Tecnicos,Login,Matricula,Cop,UltimoAlterar,Local,PontoCasaApt,Cidade,Base,Horario,Segmento,Servico,TipoServico,TipoOs,GrupoServico,Endereco,Numero,Complemento,Cep,Node,Bairro,Pacote,Cod,Telefone1,Telefone2,Obs,ObsTecnico,Equipamento")] Route route)
        {
            if (id != route.Data)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(route);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteExists(route.Data))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(route);
        }

        // GET: Routes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Route
                .FirstOrDefaultAsync(m => m.Data == id);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var route = await _context.Route.FindAsync(id);
            _context.Route.Remove(route);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RouteExists(string id)
        {
            return _context.Route.Any(e => e.Data == id);
        }
    }
}
