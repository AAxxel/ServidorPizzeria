namespace POSPizzeria.DTOs
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }
        public string? NombreProducto { get; set; }
        public decimal? PrecioProducto { get; set; }
        public decimal? Impuesto { get; set; }
        public int? Stock { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdProveedor { get; set; }
    }

    public class CreateProductoDTO
    {
        public string? NombreProducto { get; set; }
        public decimal? PrecioProducto { get; set; }
        public decimal? Impuesto { get; set; }
        public int? Stock { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdProveedor { get; set; }
    }

    public class UpdateProductoDTO
    {
        public int IdProducto { get; set; }
        public string? NombreProducto { get; set; }
        public decimal? PrecioProducto { get; set; }
        public decimal? Impuesto { get; set; }
        public int? Stock { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdProveedor { get; set; }
    }
}
