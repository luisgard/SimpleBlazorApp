using SimpleApiResponse;
using SimpleModelos;

namespace SimpleBlazorApp
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Producto>> GetProductosAsync()
        {
            List<Producto> productos= new List<Producto>();
            var result = await _httpClient.GetFromJsonAsync<ProductosResponse>("api/Productos");
            if (result!=null && result.Success)
            {
                productos = result.Resultado ?? new List<Producto>();
            }
            return productos;
        }
    }
}
