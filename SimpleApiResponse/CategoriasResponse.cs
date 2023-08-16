using SimpleModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApiResponse
{
    public class CategoriasResponse : ResponseBase
    {
        public List<Categoria>? Resultado { get; set; }
    }
}
