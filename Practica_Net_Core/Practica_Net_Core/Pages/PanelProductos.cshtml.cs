using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Practica_Net_Core.Entidades;
using Practica_Net_Core.Repositorios.Interfaces;

namespace Practica_Net_Core.Pages
{
    public class PanelProductos : PageModel
    {
        private readonly IProductoRepositorio _productoRepositorio;
        private readonly IProductoProveedorRepositorio _productoProveedorRepositorio;
        public List<Producto> productos { get; set; }

        public PanelProductos(IProductoRepositorio productoRepositorio, IProductoProveedorRepositorio productoProveedorRepositorio)
        {
            _productoRepositorio = productoRepositorio;
            _productoProveedorRepositorio = productoProveedorRepositorio;
        }

        public void OnGet()
        {
            productos = _productoRepositorio.ObtenerTodos();
        }

        public IActionResult OnPostDelete(string clave)
        {
            _productoProveedorRepositorio.EliminarPorProducto(clave);
            _productoRepositorio.Eliminar(clave); TempData["SuccessMessage"] = "Producto eliminado con éxito.";
            return RedirectToPage();
        }
    }
}