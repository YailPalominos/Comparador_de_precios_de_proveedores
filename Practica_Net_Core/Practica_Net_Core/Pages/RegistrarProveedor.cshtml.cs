using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Practica_Net_Core.Entidades;
using Practica_Net_Core.Repositorios.Interfaces;

namespace Practica_Net_Core.Pages
{
    public class RegistrarProveedor : PageModel
    {
        private readonly IProveedorRepositorio _proveedorRepositorio;

        [BindProperty]
        public Proveedor Proveedor { get; set; }

        public RegistrarProveedor(IProveedorRepositorio proveedorRepositorio)
        {
            _proveedorRepositorio = proveedorRepositorio;
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
            _proveedorRepositorio.Agregar(Proveedor);
            TempData["SuccessMessage"] = "Proveedor registrado con éxito.";
            return RedirectToPage("RegistrarProveedor");
        }
    }
}
