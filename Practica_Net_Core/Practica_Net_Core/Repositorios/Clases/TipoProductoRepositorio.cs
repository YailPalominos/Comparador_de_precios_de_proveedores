using System.Data;
using Practica_Net_Core.Entidades;
using Practica_Net_Core.Recursos;
using Practica_Net_Core.Repositorios.Interfaces;
using Microsoft.Data.SqlClient;

namespace Practica_Net_Core.Repositorios.Clases
{
    public class TipoProductoRepositorio : ITipoProductoRepositorio
    {
        private readonly SqlServer _sqlServer;

        public TipoProductoRepositorio(SqlServer sqlServer)
        {
            _sqlServer = sqlServer;
        }

        #region CRUD
        public List<TipoProducto> ObtenerTodos()
        {
            var parametros = new List<SqlParameter>();
            string procedimiento = "inventario.tipos_productos_ObtenerTodos";
            return _sqlServer.EjecutarLector(parametros, procedimiento, ConvertirTipoProducto);
        }

        public TipoProducto ObtenerPorClave(string clave)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@clave", clave)
            };
            string procedimiento = "inventario.tipos_productos_ObtenerPorClave";
            return _sqlServer.EjecutarLector(parametros, procedimiento, ConvertirTipoProducto)[0];
        }

        public void Agregar(TipoProducto tipoProducto)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@clave", tipoProducto.clave),
                new SqlParameter("@nombre", tipoProducto.nombre),
                new SqlParameter("@descripcion", tipoProducto.descripcion==null?DBNull.Value:tipoProducto.descripcion)
            };
            string procedimiento = "inventario.tipos_productos_Agregar";
            _sqlServer.EjecutarUnico<object>(parametros, procedimiento);
        }

        public void Actualizar(TipoProducto tipoProducto)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@clave", tipoProducto.clave),
                new SqlParameter("@nombre", tipoProducto.nombre),
                new SqlParameter("@descripcion", tipoProducto.descripcion==null?DBNull.Value:tipoProducto.descripcion)
            };
            string procedimiento = "inventario.tipos_productos_Actualizar";
            _sqlServer.EjecutarUnico<object>(parametros, procedimiento);
        }

        public void Eliminar(string clave)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@clave", clave)
            };
            string procedimiento = "inventario.tipos_productos_Eliminar";
            _sqlServer.EjecutarUnico<object>(parametros, procedimiento);
        }
        #endregion

        #region Método auxiliar
        private TipoProducto ConvertirTipoProducto(IDataRecord record)
        {
            return new TipoProducto
            {
                clave = record["clave"].ToString() ?? "",
                nombre = record["nombre"].ToString() ?? "",
                descripcion = record["descripcion"].ToString()
            };
        }
        #endregion
    }
}