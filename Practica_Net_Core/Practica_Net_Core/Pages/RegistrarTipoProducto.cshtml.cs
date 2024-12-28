using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Practica_Net_Core.Entidades;
using Practica_Net_Core.Repositorios.Interfaces;

namespace Practica_Net_Core.Pages
{
    public class RegistrarTipoProducto : PageModel
    {
        private readonly ITipoProductoRepositorio _tipoProductoRepositorio;

        [BindProperty]
        public TipoProducto TipoProducto { get; set; }

        public RegistrarTipoProducto(ITipoProductoRepositorio tipoProductoRepositorio)
        {
            _tipoProductoRepositorio = tipoProductoRepositorio;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _tipoProductoRepositorio.Agregar(TipoProducto);
            TempData["SuccessMessage"] = "Tipo de Producto registrado con éxito.";
            return RedirectToPage("RegistrarTipoProducto");
        }
    }
}
