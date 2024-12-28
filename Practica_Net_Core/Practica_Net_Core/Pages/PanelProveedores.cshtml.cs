using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Practica_Net_Core.Entidades;
using Practica_Net_Core.Repositorios.Clases;
using Practica_Net_Core.Repositorios.Interfaces;

namespace Practica_Net_Core.Pages
{
    public class PanelProveedores : PageModel
    {
        private readonly IProveedorRepositorio _proveedorRepositorio;
        public List<Proveedor> proveedores { get; set; }

        public PanelProveedores(IProveedorRepositorio proveedorRepositorio)
        {
            _proveedorRepositorio = proveedorRepositorio;
        }

        public void OnGet()
        {
            proveedores = _proveedorRepositorio.ObtenerTodos();
        }

        public IActionResult OnPostDelete(string clave)
        {
            _proveedorRepositorio.Eliminar(clave); TempData["SuccessMessage"] = "Proveedor eliminado con éxito.";
            return RedirectToPage();
        }
    }
}