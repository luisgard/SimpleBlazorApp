using SimpleModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApiResponse
{
    public class ProductoResponse : ResponseBase
    {
        public Producto? Resultado { get; set; }
    }
}
