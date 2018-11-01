using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.Analizador_sintactico
{
    class Conjunto
    {
        public string LadoIzquierdo { get; set; }
        public string LadoDerecho { get; set; }

        public Conjunto(string ladoIzquierdo, string ladoDerecho)
        {
            LadoIzquierdo = ladoIzquierdo;
            LadoDerecho = ladoDerecho;
        }

        public void SetLadoDerecho(string ladoDerecho)
        {
            LadoDerecho = ladoDerecho;
        }

        public override string ToString()
        {
            return LadoIzquierdo + " = {" + LadoDerecho + "}";
        }
    }
}
