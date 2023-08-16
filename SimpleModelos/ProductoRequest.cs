using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleModelos
{
    public class ProductoRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string? Descripcion { get; set; }

        [Required]
        public double Precio { get; set; }

        [Required]
        public Categoria? Cat { get; set; }
    }
}
