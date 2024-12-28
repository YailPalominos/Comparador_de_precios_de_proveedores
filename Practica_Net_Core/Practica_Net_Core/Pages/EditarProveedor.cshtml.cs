using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Practica_Net_Core.Entidades;
using Practica_Net_Core.Repositorios.Interfaces;

namespace Practica_Net_Core.Pages
{
    public class EditarProveedor : PageModel
    {
        private readonly IProveedorRepositorio _proveedorRepositorio;

        [BindProperty]
        public Proveedor proveedor { get; set; }

        public EditarProveedor(IProveedorRepositorio proveedorRepositorio)
        {
            _proveedorRepositorio = proveedorRepositorio;
        }

        public IActionResult OnGet(string clave)
        {
            proveedor = _proveedorRepositorio.ObtenerPorClave(clave);
            if (proveedor == null)
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
            _proveedorRepositorio.Actualizar(proveedor);
            TempData["SuccessMessage"] = "Proveedor actualizado con éxito.";
            return RedirectToPage("PanelProveedores");
        }
    }
}
