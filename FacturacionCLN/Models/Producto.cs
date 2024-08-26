namespace FacturacionCLN.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioCordoba { get; set; }
        public decimal PrecioDolar { get; set; }
        public bool Activo { get; set; }
    }
}
