namespace Practica_Net_Core.Entidades
{
    public class Proveedor
    {
        public string clave { get; set; }
        public string nombre { get; set; }
        public string? descripcion { get; set; }
        public Proveedor()
        {
            clave = string.Empty;
            nombre = string.Empty;
            descripcion = null;
        }
    }
}
