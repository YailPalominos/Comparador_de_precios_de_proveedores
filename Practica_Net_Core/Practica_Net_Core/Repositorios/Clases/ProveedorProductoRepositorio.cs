using System.Data;
using Practica_Net_Core.Entidades;
using Practica_Net_Core.Recursos;
using Practica_Net_Core.Repositorios.Interfaces;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Practica_Net_Core.Repositorios.Clases
{
    public class ProveedorProductoRepositorio : IProductoProveedorRepositorio
    {
        private readonly SqlServer _sqlServer;

        public ProveedorProductoRepositorio(SqlServer sqlServer)
        {
            _sqlServer = sqlServer;
        }

        #region CRUD

        public List<ProductoProveedor> ObtenerPorProducto(string producto)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@producto", producto)
            };
            string procedimiento = "inventario.proveedores_productos_ObtenerPorProducto";
            return _sqlServer.EjecutarLector(parametros, procedimiento, ConvertirProveedorProducto);
        }

        public void Actualizar(List<ProductoProveedor> proveedoresProductos, string producto)
        {
            string proveedoresProductosJson = JsonConvert.SerializeObject(proveedoresProductos);

            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@claveProducto", producto),
                new SqlParameter("@proveedoresProductos", proveedoresProductosJson)
            };

            string procedimiento = "inventario.proveedores_productos_Actualizar";
            _sqlServer.EjecutarUnico<object>(parametros, procedimiento);
        }
        public void EliminarPorProducto(string producto)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@producto", producto)
            };
            string procedimiento = "inventario.proveedores_productos_EliminarPorProducto";
            _sqlServer.EjecutarUnico<object>(parametros, procedimiento);
        }
        #endregion

        #region Método auxiliar
        private ProductoProveedor ConvertirProveedorProducto(IDataRecord record)
        {
            return new ProductoProveedor
            {
                claveProveedor = record["clave_proveedor"].ToString() ?? "",
                claveProducto = record["clave_producto"].ToString() ?? "",
                clave = record["clave"].ToString() ?? "",
                costo = (decimal)record["costo"],
                proveedor = record["proveedor"].ToString() ?? "",
            };
        }
        #endregion
    }
}

