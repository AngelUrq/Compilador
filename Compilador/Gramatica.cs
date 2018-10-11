using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    class Gramatica
    {
        private string simboloInicial;
        private List<Produccion> producciones;
        private List<string> terminales;
        private List<string> noTerminales;

        public Gramatica(string simboloInicial, List<Produccion> producciones, List<string> terminales, List<string> noTerminales)
        {
            this.simboloInicial = simboloInicial;
            this.producciones = producciones;
            this.terminales = terminales;
            this.noTerminales = noTerminales;
        }

        public string GetSimboloInicial()
        {
            return simboloInicial;
        }

        public List<Produccion> GetProducciones()
        {
            return producciones;
        }

        public List<string> GetTerminales()
        {
            return terminales;
        }

        public List<string> GetNoTerminales()
        {
            return noTerminales;
        }
    }
}
