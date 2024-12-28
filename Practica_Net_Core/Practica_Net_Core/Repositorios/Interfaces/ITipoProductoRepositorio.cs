using Practica_Net_Core.Entidades;

namespace Practica_Net_Core.Repositorios.Interfaces
{
    public interface ITipoProductoRepositorio
    {
        List<TipoProducto> ObtenerTodos();
        TipoProducto ObtenerPorClave(string clave);
        void Agregar(TipoProducto tipoProducto);
        void Actualizar(TipoProducto tipoProducto);
        void Eliminar(string clave);
    }
}
