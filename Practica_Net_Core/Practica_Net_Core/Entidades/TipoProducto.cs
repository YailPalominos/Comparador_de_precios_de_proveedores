namespace Practica_Net_Core.Entidades
{
    public class TipoProducto
    {
        public string clave { get; set; }
        public string nombre { get; set; }
        public string? descripcion { get; set; }
        public TipoProducto()
        {
            clave = string.Empty;
            nombre = string.Empty;
            descripcion = null;
        }
    }
}
