using Practica_Net_Core.Entidades;

namespace Practica_Net_Core.Repositorios.Interfaces
{
    public interface IProductoProveedorRepositorio
    {
        List<ProductoProveedor> ObtenerPorProducto(string clave);
        public void Actualizar(List<ProductoProveedor> proveedoresProductos, string producto);
        public void EliminarPorProducto(string clave);
    }
}
