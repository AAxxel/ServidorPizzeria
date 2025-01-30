namespace POSPizzeria.DTOs
{
    public class CategoriaProductoDTO
    {
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public string Descripcion { get; set; }
    }

    public class CreateCategoriaProductoDTO
    {
        public string NombreCategoria { get; set; }
        public string Descripcion { get; set; }
    }

    public class UpdateCategoriaProductoDTO
    {
        public int IdCategoria { get; set; }
        public string? NombreCategoria { get; set; }
        public string? Descripcion { get; set; }
    }
}
