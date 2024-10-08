using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Cupon
    {
        public int IdCupon { get; set; }

        public String Codigo { get; set; }

        public int Descuento { get; set; }

        public bool Estado { get; set; }    
    }
}
