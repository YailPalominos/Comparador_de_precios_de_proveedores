using Microsoft.Data.SqlClient;
using Practica_Net_Core.Entidades;
using Practica_Net_Core.Recursos;
using Practica_Net_Core.Repositorios.Interfaces;
using System.Data;

namespace Practica_Net_Core.Repositorios.Clases
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly SqlServer _sqlServer;

        public ProductoRepositorio(SqlServer sqlServer)
        {
            _sqlServer = sqlServer;
        }

        #region CRUD
        public List<Producto> ObtenerTodos()
        {
            var parametros = new List<SqlParameter>();
            string procedimiento = "inventario.productos_ObtenerProductos";
            return _sqlServer.EjecutarLector(parametros, procedimiento, ConvertirProducto);
        }

        public Producto ObtenerPorClave(string clave)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@clave", clave)
            };
            string procedimiento = "inventario.productos_ObtenerProductoPorClave";
            return _sqlServer.EjecutarLector(parametros, procedimiento, ConvertirProducto)[0];
        }

        public void Agregar(Producto producto)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@clave", producto.clave),
                new SqlParameter("@nombre", producto.nombre),
                new SqlParameter("@tipoProducto", producto.tipoProducto),
                new SqlParameter("@esActivo", producto.esActivo),
                new SqlParameter("@precio", producto.precio)
            };
            string procedimiento = "inventario.productos_AgregarProducto";
            _sqlServer.EjecutarUnico<object>(parametros, procedimiento);
        }

        public void Actualizar(Producto producto)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@clave", producto.clave),
                new SqlParameter("@nombre", producto.nombre),
                new SqlParameter("@tipoProducto", producto.tipoProducto),
                new SqlParameter("@esActivo", producto.esActivo),
                new SqlParameter("@precio", producto.precio)
            };
            string procedimiento = "inventario.productos_ActualizarProducto";
            _sqlServer.EjecutarUnico<object>(parametros, procedimiento);
        }

        public void Eliminar(string clave)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@clave", clave)
            };
            string procedimiento = "inventario.productos_EliminarProducto";
            _sqlServer.EjecutarUnico<object>(parametros, procedimiento);
        }
        #endregion

        #region Método auxiliar
        private Producto ConvertirProducto(IDataRecord record)
        {
            return new Producto
            {
                clave = record["clave"].ToString() ?? "",
                nombre = record["nombre"].ToString() ?? "",
                tipoProducto = record["tipo_producto"].ToString() ?? "",
                esActivo = (bool)record["es_activo"],
                precio = (decimal)record["precio"],

                estatus= record["estatus"]?.ToString() ?? "",
                tipo= record["tipo"]?.ToString() ?? "",
            };
        }
        #endregion
    }
}
