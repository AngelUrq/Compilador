using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    class Produccion
    {
        private string ladoDerecho;
        private string ladoIzquierdo;

        public Produccion(string ladoIzquierdo, string ladoDerecho)
        {
            this.ladoIzquierdo = ladoIzquierdo;
            this.ladoDerecho = ladoDerecho;
        }

        public string GetLadoIzquierdo()
        {
            return this.ladoIzquierdo;
        }

        public string GetLadoDerecho()
        {
            return this.ladoDerecho;
        }
    }
}
