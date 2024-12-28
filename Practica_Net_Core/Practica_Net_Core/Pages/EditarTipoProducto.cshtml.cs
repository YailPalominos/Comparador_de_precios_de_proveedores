using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Practica_Net_Core.Entidades;
using Practica_Net_Core.Repositorios.Interfaces;

namespace Practica_Net_Core.Pages
{
    public class EditarTipoProducto : PageModel
    {
        private readonly ITipoProductoRepositorio _tipoProductoRepositorio;

        [BindProperty]
        public TipoProducto tipoProducto { get; set; }

        public EditarTipoProducto(ITipoProductoRepositorio tipoProductoRepositorio)
        {
            _tipoProductoRepositorio = tipoProductoRepositorio;
        }

        public IActionResult OnGet(string clave)
        {
            tipoProducto = _tipoProductoRepositorio.ObtenerPorClave(clave);
            if (tipoProducto == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _tipoProductoRepositorio.Actualizar(tipoProducto);
            TempData["SuccessMessage"] = "Tipo de Producto actualizado con éxito.";
            return RedirectToPage("PanelTiposProductos");
        }
    }
}
