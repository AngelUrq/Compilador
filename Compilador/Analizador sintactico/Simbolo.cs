using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.Analizador_sintactico
{
    struct Simbolo
    {
        public string Nombre { get; set; }

        public Simbolo(string nombre)
        {
            Nombre = nombre;
        }
    }
}
