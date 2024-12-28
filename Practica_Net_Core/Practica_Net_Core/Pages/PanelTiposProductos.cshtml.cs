using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Practica_Net_Core.Entidades;
using Practica_Net_Core.Repositorios.Clases;
using Practica_Net_Core.Repositorios.Interfaces;

namespace Practica_Net_Core.Pages
{
    public class PanelTiposProductos : PageModel
    {
        private readonly ITipoProductoRepositorio _tipoProductoRepositorio;
        public List<TipoProducto> tiposProductos { get; set; }

        public PanelTiposProductos(ITipoProductoRepositorio tipoProductoRepositorio)
        {
            _tipoProductoRepositorio = tipoProductoRepositorio;
        }

        public void OnGet()
        {
            tiposProductos = _tipoProductoRepositorio.ObtenerTodos();
        }

        public IActionResult OnPostDelete(string clave)
        {
            _tipoProductoRepositorio.Eliminar(clave); TempData["SuccessMessage"] = "Tipo de producto eliminado con éxito.";
            return RedirectToPage();
        }
    }
}