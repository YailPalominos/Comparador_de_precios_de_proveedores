using System.ComponentModel.DataAnnotations;

namespace Practica_Net_Core.Entidades
{
    public class Producto
    {
        public string clave { get; set; }
        public string nombre { get; set; }
        public string tipoProducto { get; set; }
        public bool esActivo { get; set; }
        public decimal precio { get; set; }

        #region Propiedades auxiliares 
        public string? estatus { get; set; }
        public string? tipo { get; set; }
        #endregion

        public Producto()
        {
            clave = string.Empty;
            nombre = string.Empty;
            tipoProducto = string.Empty;
            esActivo = true;
            precio = 0;
        }
    }
}
