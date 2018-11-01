using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    class Token
    {
        protected string palabra, tipo;
        protected int columna, fila;

        public Token()
        {
            palabra = "";
            tipo = "";
            columna = 0;
            fila = 0;
        }
        public Token(string der, string iz, int co, int fi)
        {
            this.palabra = der;
            this.tipo = iz;
            this.columna = co;
            this.fila = fi;
        }

        public string GetPalabra()
        {
            return palabra;
        }

        public string GetTipo()
        {
            return tipo;
        }

        public int GetColumna()
        {
            return columna;
        }

        public int GetFila()
        {
            return fila;
        }
    }
}
