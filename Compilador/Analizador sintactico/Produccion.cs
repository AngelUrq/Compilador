using Compilador.Analizador_sintactico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    class Produccion
    {
        public string LadoIzquierdo { get; set; }
        public Simbolo[] LadoDerecho { get; set; }

        public Produccion(string ladoIzquierdo, Simbolo[] ladoDerecho)
        {
            LadoIzquierdo = ladoIzquierdo;
            LadoDerecho = ladoDerecho;
        }

        public string GetLadoDerecho()
        {
            string cadena = "";
            for (int i = 0; i < LadoDerecho.Length; i++)
            {
                cadena += LadoDerecho[i].Nombre + " ";
            }
            return cadena;
        }

        public override string ToString()
        {
            string cadena = LadoIzquierdo + "->";

            for (int i = 0; i < LadoDerecho.Length; i++)
                cadena += LadoDerecho[i].Nombre;

            return cadena;
        }
    }
}
