using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prueba.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prueba.ViewsComponent
{
    public class ListasComponent: ViewComponent
    {
        private readonly PruebaContext _db;
        public ListasComponent( PruebaContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //remover usarios atendidos
            var usariosatendidos = await _db.T_ListasClientes
                .Where(r => r.Fecha < DateTime.Now).ToListAsync();

            if (usariosatendidos.Any())
            {
                _db.T_ListasClientes.RemoveRange(usariosatendidos);
                await _db.SaveChangesAsync();

            }


            return View(await _db.T_ListasClientes
                .Include(r => r.ID_ClientesNavigation)
                .Include(r => r.ID_ListaNavigation)
                .Where(r => r.Fecha >= DateTime.Now).ToListAsync());

        }

    }
}
