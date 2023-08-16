namespace SimpleModelos
{
    public class Producto
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int CategoriaId { get; set; } // Clave foránea para la relación con Categoria
        public Categoria Cat { get; set; } // Propiedad de navegación}
    }
}