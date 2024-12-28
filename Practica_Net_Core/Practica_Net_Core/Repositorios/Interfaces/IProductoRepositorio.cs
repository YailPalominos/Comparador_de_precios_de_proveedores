using Practica_Net_Core.Entidades;

namespace Practica_Net_Core.Repositorios.Interfaces
{
    public interface IProductoRepositorio
    {
        public List<Producto> ObtenerTodos();
        public Producto ObtenerPorClave(string clave);
        public void Agregar(Producto producto);
        public void Actualizar(Producto producto);
        public void Eliminar(string clave);
    }
}
