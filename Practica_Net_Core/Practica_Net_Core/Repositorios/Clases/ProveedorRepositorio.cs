using Microsoft.Data.SqlClient;
using Practica_Net_Core.Entidades;
using Practica_Net_Core.Recursos;
using Practica_Net_Core.Repositorios.Interfaces;
using System.Data;

namespace Practica_Net_Core.Repositorios.Clases
{
    public class ProveedorRepositorio : IProveedorRepositorio
    {
        private readonly SqlServer _sqlServer;

        public ProveedorRepositorio(SqlServer sqlServer)
        {
            _sqlServer = sqlServer;
        }

        #region CRUD
        public List<Proveedor> ObtenerTodos()
        {
            var parametros = new List<SqlParameter>();
            string procedimiento = "inventario.proveedores_ObtenerProveedores";
            return _sqlServer.EjecutarLector(parametros, procedimiento, ConvertirProveedor);
        }

        public Proveedor ObtenerPorClave(string clave)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@clave", clave)
            };
            string procedimiento = "inventario.proveedores_ObtenerProveedorPorClave";
            return _sqlServer.EjecutarLector(parametros, procedimiento, ConvertirProveedor)[0];
        }

        public void Agregar(Proveedor proveedor)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@clave", proveedor.clave),
                new SqlParameter("@nombre", proveedor.nombre),
                new SqlParameter("@descripcion", proveedor.descripcion==null?DBNull.Value:proveedor.descripcion)
            };
            string procedimiento = "inventario.proveedores_AgregarProveedor";
            _sqlServer.EjecutarUnico<object>(parametros, procedimiento);
        }

        public void Actualizar(Proveedor proveedor)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@clave", proveedor.clave),
                new SqlParameter("@nombre", proveedor.nombre),
                new SqlParameter("@descripcion", proveedor.descripcion==null?DBNull.Value:proveedor.descripcion)
            };
            string procedimiento = "inventario.proveedores_ActualizarProveedor";
            _sqlServer.EjecutarUnico<object>(parametros, procedimiento);
        }

        public void Eliminar(string clave)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@clave", clave)
            };
            string procedimiento = "inventario.proveedores_EliminarProveedor";
            _sqlServer.EjecutarUnico<object>(parametros, procedimiento);
        }
        #endregion

        #region Método auxiliar
        private Proveedor ConvertirProveedor(IDataRecord record)
        {
            return new Proveedor
            {
                clave = record["clave"].ToString() ?? "",
                nombre = record["nombre"].ToString() ?? "",
                descripcion = record["descripcion"].ToString()
            };
        }
        #endregion
    }
}
