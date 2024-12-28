using Practica_Net_Core.Entidades;

namespace Practica_Net_Core.Repositorios.Interfaces
{
    public interface IProveedorRepositorio
    {
        List<Proveedor> ObtenerTodos();
        Proveedor ObtenerPorClave(string clave);
        void Agregar(Proveedor proveedor);
        void Actualizar(Proveedor proveedor);
        void Eliminar(string clave);
    }
}