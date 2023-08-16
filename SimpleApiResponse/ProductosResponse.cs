using SimpleModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApiResponse
{
    public class ProductosResponse: ResponseBase
    {
        public List<Producto>? Resultado { get; set; }
    }
}
