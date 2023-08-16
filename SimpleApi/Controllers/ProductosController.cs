using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleApiResponse;
using SimpleInterfaces;
using SimpleModelos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosLogic _porductoslogica;

        public ProductosController(IProductosLogic productosLogic)
        {
            _porductoslogica= productosLogic;
        }

        /// <summary>
        /// Obtener listado de productos
        /// </summary>
        /// <returns>Obtener listado de productos</returns>
        [ProducesResponseType(typeof(ProductosResponse), 200)]
        [ProducesResponseType(typeof(ProductosResponse), 400)]
        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            ProductosResponse respuesta = new ProductosResponse();
            try
            {
                respuesta.Resultado = await _porductoslogica.GetProductosAsync();
                respuesta.Success = true;
                respuesta.ResponseType = ResponseType.Okay;
            }
            catch (Exception exp)
            {
                respuesta.Success = false;
                respuesta.ErrorMessage = "Error al obtener el listado de productos: " + exp.Message; ;
                respuesta.ResponseType = ResponseType.InternalError;
                respuesta.ErrorCode= (int)ResponseType.InternalError;

            }
            return new RestfulResponse().GetResponse(respuesta);
        }

        /// <summary>
        ///     AgregarProducto
        /// </summary>
        /// <remarks>
        ///     Agrega un nuevo producto
        /// </remarks>
        /// <example>
        /// </example>
        /// <param name="model">Entidad 'AddProductoRequest'</param>
        /// <returns>Entidad 'ResponseBase_ProductoResponse'</returns>
        /// <response code="200">Operacion exitosa</response>
        /// <response code="400">Error en la peticion</response>
        [ProducesResponseType(typeof(ProductoResponse), 200)]
        [ProducesResponseType(typeof(ProductoResponse), 400)]
        [HttpPost("AgregarProducto")]
        public async Task<IActionResult> AgregarProducto([FromBody] AddProductoRequest model)
        {
            ProductoResponse respuesta = new ProductoResponse();

            try
            {
                Producto nuevo = new Producto() { Descripcion = model.Descripcion, Precio = model.Precio, Cat = model.Cat };
               
                var (productoAgregado, exito) = await _porductoslogica.AddProductoAsync(nuevo);
                if (exito)
                {
                    respuesta.Success = true;
                    respuesta.ResponseType = ResponseType.Okay;
                    respuesta.Description = "Producto agregado exitosamente";
                    respuesta.Resultado = productoAgregado;
                }
                else
                {
                    respuesta.Success = false;
                    respuesta.ResponseType = ResponseType.InternalError;
                    respuesta.ErrorMessage = "Error al agregar el producto";
                    respuesta.ErrorCode = (int)ResponseType.InternalError;
                }
            }
            catch (Exception ex)
            {
                respuesta.Success = false;
                respuesta.ResponseType = ResponseType.InternalError;
                respuesta.ErrorMessage = "Error al agregar el producto: " + ex.Message;
                respuesta.ErrorCode = (int)ResponseType.InternalError;
            }
            return new RestfulResponse().GetResponse(respuesta);
        }


        /// <summary>
        ///     EditarProducto
        /// </summary>
        /// <remarks>
        ///     Editar un producto existente
        /// </remarks>
        /// <example>
        /// </example>
        /// <param name="model">Entidad 'ProductoRequest'</param>
        /// <returns>Entidad 'ResponseBase_ProductoResponse'</returns>
        /// <response code="200">Operacion exitosa</response>
        /// <response code="400">Error en la peticion</response>
        [ProducesResponseType(typeof(ProductosResponse), 200)]
        [ProducesResponseType(typeof(ProductosResponse), 400)]
        [HttpPut("EditarProducto")]
        public async Task<IActionResult> EditarProducto([FromBody] ProductoRequest model)
        {
            ProductoResponse respuesta = new ProductoResponse();

            try
            {
                var productoBuscado= await _porductoslogica.GetProductoAsync(model.Id);
      
                if (productoBuscado != null && productoBuscado.Id != Guid.Empty)
                {
                    Producto productoEditado = new Producto() { Id = model.Id, Descripcion = model.Descripcion, Precio = model.Precio, Cat = model.Cat };
                    var exitoEdicion = await _porductoslogica.EditProductoAsync(productoEditado);
                    if (exitoEdicion)
                    {
                        respuesta.Success = true;
                        respuesta.ResponseType = ResponseType.Okay;
                        respuesta.Description = "Producto editado exitosamente";
                        respuesta.Resultado = productoEditado;
               
                    }
                    else
                    {
                        respuesta.Success = false;
                        respuesta.ResponseType = ResponseType.BadRequest;
                        respuesta.ErrorMessage = "Producto no encontrado";
                        respuesta.ErrorCode = (int)ResponseType.BadRequest;
                       
                    }
                }
                else
                {
                    respuesta.Success = false;
                    respuesta.ResponseType = ResponseType.BadRequest;
                    respuesta.ErrorCode = (int)ResponseType.BadRequest;
                    respuesta.ErrorMessage = "Producto no encontrado";
                    
                }
            }
            catch (Exception ex)
            {
                respuesta.Success = false;
                respuesta.ResponseType = ResponseType.InternalError;
                respuesta.ErrorCode = (int)ResponseType.InternalError;
                respuesta.ErrorMessage = "Error al editar el producto: " + ex.Message;
            }
            return new RestfulResponse().GetResponse(respuesta);
        }

        /// <summary>
        ///     BorrarProducto
        /// </summary>
        /// <remarks>
        ///     Borrar un producto existente
        /// </remarks>
        /// <example>
        /// </example>
        /// <param name="model">Entidad 'GUID'</param>
        /// <returns>Entidad 'ResponseBase_ProductoResponse'</returns>
        /// <response code="200">Operacion exitosa</response>
        /// <response code="400">Error en la peticion</response>
        [ProducesResponseType(typeof(ProductosResponse), 200)]
        [ProducesResponseType(typeof(ProductosResponse), 400)]
        [HttpDelete("BorrarProducto")]
        public async Task<IActionResult> BorrarProducto([FromBody] Guid id)
        {
            ProductoResponse respuesta = new ProductoResponse();

            try
            {
                var productoBuscado = await _porductoslogica.GetProductoAsync(id);

                if (productoBuscado != null && productoBuscado.Id != Guid.Empty)
                {
                   
                    var exitoEliminacion = await _porductoslogica.DelProductoAsync(productoBuscado);
                    if (exitoEliminacion)
                    {
                        respuesta.Success = true;
                        respuesta.ResponseType = ResponseType.Okay;
                        respuesta.Description = "Producto eliminado exitosamente";
                        respuesta.Resultado = productoBuscado;

                    }
                    else
                    {
                        respuesta.Success = false;
                        respuesta.ResponseType = ResponseType.BadRequest;
                        respuesta.ErrorMessage = "Producto no encontrado";
                        respuesta.ErrorCode = (int)ResponseType.BadRequest;

                    }
                }
                else
                {
                    respuesta.Success = false;
                    respuesta.ResponseType = ResponseType.BadRequest;
                    respuesta.ErrorCode = (int)ResponseType.BadRequest;
                    respuesta.ErrorMessage = "Producto no encontrado";

                }
            }
            catch (Exception ex)
            {
                respuesta.Success = false;
                respuesta.ResponseType = ResponseType.InternalError;
                respuesta.ErrorCode = (int)ResponseType.InternalError;
                respuesta.ErrorMessage = "Error al borrar el producto: " + ex.Message;
            }
            return new RestfulResponse().GetResponse(respuesta);
        }


        /// <summary>
        /// Obtener listado de categorias
        /// </summary>
        /// <returns>Obtener listado de Categorias</returns>
        [ProducesResponseType(typeof(CategoriasResponse), 200)]
        [ProducesResponseType(typeof(CategoriasResponse), 400)]
        [HttpGet("GetCategorias")]
        public async Task<IActionResult> GetCategorias()
        {
            CategoriasResponse respuesta = new CategoriasResponse();
            try
            {
                respuesta.Resultado = await _porductoslogica.GetCategoriasAsync();
                respuesta.Success = true;
                respuesta.ResponseType = ResponseType.Okay;
            }
            catch (Exception exp)
            {
                respuesta.Success = false;
                respuesta.ErrorMessage = "Error al obtener el listado de Categorias: " + exp.Message; ;
                respuesta.ResponseType = ResponseType.InternalError;
                respuesta.ErrorCode = (int)ResponseType.InternalError;

            }
            return new RestfulResponse().GetResponse(respuesta);
        }
    }
}
