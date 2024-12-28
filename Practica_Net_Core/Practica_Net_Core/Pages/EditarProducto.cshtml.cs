using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Practica_Net_Core.Entidades;
using Practica_Net_Core.Repositorios.Interfaces;

namespace Practica_Net_Core.Pages
{
    public class EditarProducto : PageModel
    {
        private readonly IProductoRepositorio _productoRepositorio;
        private readonly ITipoProductoRepositorio _tipoProductoRepositorio;
        private readonly IProveedorRepositorio _proveedorRepositorio;
        private readonly IProductoProveedorRepositorio _productoProveedorRepositorio;

        [BindProperty]
        public Producto producto { get; set; }
        public List<ProductoProveedor> proveedoresProductos { get; set; } = new List<ProductoProveedor>();

        #region Auxiliares
        public ProductoProveedor productoProveedor { get; set; }
        public List<TipoProducto> TiposProductos { get; set; } = new List<TipoProducto>();
        public List<Proveedor> proveedores { get; set; } = new List<Proveedor>();
        #endregion

        public EditarProducto(IProductoRepositorio productoRepositorio, ITipoProductoRepositorio tipoProductoRepositorio, IProveedorRepositorio proveedorRepositorio, IProductoProveedorRepositorio productoProveedorRepositorio)
        {
            _productoRepositorio = productoRepositorio;
            _tipoProductoRepositorio = tipoProductoRepositorio;
            _proveedorRepositorio = proveedorRepositorio;
            _productoProveedorRepositorio = productoProveedorRepositorio;
        }

        public IActionResult OnGet(string clave)
        {
            producto = _productoRepositorio.ObtenerPorClave(clave);
            if (producto == null)
            {
                return NotFound();
            }
            TiposProductos = _tipoProductoRepositorio.ObtenerTodos();
            proveedores = _proveedorRepositorio.ObtenerTodos();
            proveedoresProductos = _productoProveedorRepositorio.ObtenerPorProducto(clave);
            return Page();
        }

        public IActionResult OnPostAgregarRelacion([FromBody] ProductoProveedor productoProveedor)
        {
            if (productoProveedor != null)
            {
                var proveedor = _proveedorRepositorio.ObtenerPorClave(productoProveedor.claveProveedor);

                if (proveedor != null)
                {
                    productoProveedor.proveedor = proveedor.nombre;
                    proveedoresProductos.Add(productoProveedor);
                    return new JsonResult(productoProveedor);
                }

                return BadRequest("Proveedor no encontrado.");
            }

            return BadRequest("Error al agregar la relación");
        }

        public IActionResult OnPostEliminarRelacion([FromBody] ProductoProveedor productoProveedor)
        {
            proveedoresProductos.Remove(productoProveedor);
            return new JsonResult(new { success = true });
        }

        public IActionResult OnPostActualizar([FromBody] ProductoRequest request)
        {
            var producto = request.Producto;
            var proveedoresProductos = request.ProveedoresProductos;

            var proveedores = _proveedorRepositorio.ObtenerTodos();

            foreach (var productoProveedor in proveedoresProductos)
            {
                productoProveedor.claveProducto = producto.clave;
                productoProveedor.claveProveedor = proveedores.Find(proveedor => proveedor.nombre == productoProveedor.proveedor).clave;
            }

            _productoRepositorio.Actualizar(producto);
            _productoProveedorRepositorio.Actualizar(proveedoresProductos, producto.clave);

            TempData["SuccessMessage"] = "Producto actualizado con éxito.";
            return new JsonResult(new { success = true });
        }

        public class ProductoRequest
        {
            public Producto Producto { get; set; }
            public List<ProductoProveedor> ProveedoresProductos { get; set; }
        }
    }
}
