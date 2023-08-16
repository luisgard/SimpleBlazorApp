using Microsoft.EntityFrameworkCore;
using SimpleDataAccess;
using SimpleInterfaces;
using SimpleModelos;

namespace SimpleLogica
{
    public class ProductLogic : IProductosLogic
    {
        private SimpleContexto _contexto;

        public ProductLogic(SimpleContexto contexto)
        {
            _contexto = contexto;

            _contexto.Database.EnsureCreated(); // Crea la base de datos si no existe

            if (!_contexto.Categorias.Any())
            {
                //llenar catalogo
                var categorias = new Categoria[]
                {
                new Categoria(){ Nombre= "Categoria A"},
                new Categoria(){ Nombre= "Categoria B"},
                new Categoria(){ Nombre= "Categoria C"},
                };
                _contexto.Categorias.AddRange(categorias);
                _contexto.SaveChangesAsync();
            }

            if (!_contexto.Productos.Any())
            {
                // Agrega algunos productos a la base de datos
                var productos = new[]
                {
                new Producto() { Id= Guid.NewGuid(), Descripcion="Producto1" , Cat= _contexto.Categorias.Where(x=> x.Id==1).FirstOrDefault(), Precio= 10.5},
                new Producto() { Id= Guid.NewGuid(), Descripcion="Producto2" , Cat= _contexto.Categorias.Where(x=> x.Id==1).FirstOrDefault(), Precio= 10.5},
                new Producto() { Id= Guid.NewGuid(), Descripcion="Producto3" , Cat= _contexto.Categorias.Where(x=> x.Id==2).FirstOrDefault(),Precio= 40},
                new Producto() { Id= Guid.NewGuid(), Descripcion="Producto4" , Cat= _contexto.Categorias.Where(x=> x.Id==2).FirstOrDefault(),Precio= 40},
                new Producto() { Id= Guid.NewGuid(), Descripcion="Producto5" , Cat= _contexto.Categorias.Where(x=> x.Id==3).FirstOrDefault(),Precio= 50},
                new Producto() { Id= Guid.NewGuid(), Descripcion="Producto6" , Cat= _contexto.Categorias.Where(x=> x.Id==3).FirstOrDefault(),Precio= 50}
                };

                _contexto.Productos.AddRange(productos);
                _contexto.SaveChanges();
            }
        }

        public async Task<(Producto, bool)> AddProductoAsync(Producto producto)
        {
            try
            {
                producto.Id = Guid.NewGuid();
                producto.Cat = _contexto.Categorias.Where(x => x.Id == producto.Cat.Id).FirstOrDefault();
                _contexto.Productos.Add(producto);
                await _contexto.SaveChangesAsync();
                return (producto, true); // Indica que la adición fue exitosa
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción que pueda ocurrir al agregar el producto
                return (null, false); // Indica que la adición no fue exitosa
            }
        }

        public async Task<bool> DelProductoAsync(Producto producto)
        {
            var productoAEliminar = await _contexto.Productos.FirstOrDefaultAsync(p => p.Id == producto.Id);

            if (productoAEliminar != null)
            {
                _contexto.Productos.Remove(productoAEliminar);
                await _contexto.SaveChangesAsync();

                return true; // Indica que la eliminación fue exitosa
            }

            return false; // Indica que el producto no fue encontrado
        }

        public async Task<bool> EditProductoAsync(Producto producto)
        {
            bool result = false;
            var productoBuscado = await GetProductoAsync(producto.Id);
            if (productoBuscado != null && productoBuscado.Id != Guid.Empty)
            {
                productoBuscado.Descripcion = producto.Descripcion;
                productoBuscado.Precio = producto.Precio;
                productoBuscado.Cat = _contexto.Categorias.Where(x => x.Id == producto.Cat.Id).FirstOrDefault();

                _contexto.Entry(productoBuscado).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();

                result = true; // Indica que la edición fue exitosa
            }
            return result;
        }

        public async Task<Producto> GetProductoAsync(Guid guid)
        {
            var producto = await _contexto.Productos.FirstOrDefaultAsync(p => p.Id == guid);
            return producto ?? new Producto(); // Devuelve un Producto vacío si no se encuentra ninguno
        }

        public async Task<List<Categoria>> GetCategoriasAsync()
        {
            var lisCat = await _contexto.Categorias.ToListAsync();
            return lisCat;
        }

        public async Task<List<Producto>> GetProductosAsync()
        {
            var listadoProductos = await _contexto.Productos.ToListAsync();
            foreach (var producto in listadoProductos)
            {
                producto.Cat = _contexto.Categorias.Where(x => x.Id == producto.CategoriaId).FirstOrDefault();
            }
            return listadoProductos;
        }
    }
}