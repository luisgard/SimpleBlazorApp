using SimpleModelos;

namespace SimpleInterfaces
{
    public interface IProductosLogic
    {
        Task<List<Producto>> GetProductosAsync();

        Task<Producto> GetProductoAsync(Guid guid);

        Task<(Producto produc, bool respueta)> AddProductoAsync(Producto producto);

        Task<bool> DelProductoAsync(Producto producto);

        Task<bool> EditProductoAsync(Producto producto);

        Task<List<Categoria>> GetCategoriasAsync();
    }
}