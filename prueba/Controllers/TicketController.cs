using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using prueba.Data;
using prueba.Interface;
using prueba.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace prueba.Controllers
{
    public class TicketController : Controller
    {
        private readonly PruebaContext _db;
        private readonly INotify _serv;
        public TicketController(PruebaContext db, INotify serv)
        {
            _db = db;
            _serv = serv;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddCliente()
        {

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddClienteAsync(T_Clientes model)
        {
            if (!ModelState.IsValid) return PartialView(model);

            var existsClient =await _db.T_Clientes
                .Include(r=>r.T_ListasClientes).ThenInclude(r=>r.ID_ListaNavigation)
                .FirstOrDefaultAsync(r => r.Cedula == model.Cedula);

            // Se crea o se actualiza el cliente identificado
            try
            {
                if (existsClient == null)
                {
                    existsClient = new T_Clientes
                    {
                        ID_Cliente = Guid.NewGuid(),
                        Cedula = model.Cedula,
                        CreatedOn = DateTime.Now,
                        Nombre = model.Nombre

                    };
                    _db.T_Clientes.Add(existsClient);
                }
                else
                {
                    existsClient.Nombre = model.Nombre;
                    _db.T_Clientes.Update(existsClient);

                    // comprobar si cliente tiene se encuentra en una lista
                    var IsInlist = existsClient.T_ListasClientes?.OrderByDescending(r => r.Fecha).FirstOrDefault();

                    if (IsInlist != null && IsInlist.Fecha >= DateTime.Now)
                    {
                        TempData["Notify"] = _serv.Notificacion("Usuario ya en cola", Tipo.alert);
                        return RedirectToAction("MostraInfo", new { id = existsClient.ID_Cliente });
                    }

                }



                var listaMenorMinutos = _db.T_Listas
                   .Include(r => r.T_ListasClientes)
                   .OrderBy(r => r.Duracion)
                   .ToList().Select(m => new
                   {
                       TotalTiempo = m.T_ListasClientes.Where(r => r.Fecha >= DateTime.Now).Sum(r => r.Duracion),
                       Lista = m
                   }).OrderBy(r=>r.TotalTiempo).FirstOrDefault().Lista;



                var ultimoCliente = await _db.T_ListasClientes.OrderByDescending(r => r.Fecha).FirstOrDefaultAsync(r => r.ID_Lista == listaMenorMinutos.ID_lista);

                _db.T_ListasClientes.Add(new T_ListasClientes
                {
                    Duracion = listaMenorMinutos.Duracion,
                    ID_Cliente = existsClient.ID_Cliente,
                    ID_Lista = listaMenorMinutos.ID_lista,
                    ID_ListaCliente = Guid.NewGuid(),
                    Fecha = ultimoCliente == null ? DateTime.Now.AddMinutes(listaMenorMinutos.Duracion) : ultimoCliente.Fecha.AddMinutes(listaMenorMinutos.Duracion)
                }); 



                await _db.SaveChangesAsync();


            }
            catch (Exception ex)
            {

                return NotFound($"Ha ocurrido un error intente nuevamete :{ex.Message}");
            }

          



            return RedirectToAction("MostraInfo", new { id = existsClient.ID_Cliente});
        }
     
        public async Task<IActionResult> MostraInfoAsync(Guid id)
        {
            var cliente =await _db.T_Clientes
                .Include(r=>r.T_ListasClientes).ThenInclude(r=>r.ID_ListaNavigation)
                .FirstOrDefaultAsync(r => r.ID_Cliente == id);

            return PartialView("_InfoPartial", cliente);

        }

        public IActionResult CargarTablas()
        {

            return ViewComponent("ListasComponent");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
