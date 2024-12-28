namespace Practica_Net_Core.Entidades
{
    public class ProductoProveedor
    {
        public string claveProveedor { get; set; }
        public string claveProducto { get; set; }
        public string clave { get; set; }
        public decimal costo { get; set; }

        #region Propiedades auxiliares 
        public string? proveedor { get; set; }
        #endregion

        public ProductoProveedor()
        {
            claveProveedor = string.Empty;
            claveProducto = string.Empty;
            clave = string.Empty;
            costo = 0;
        }
    }
}
