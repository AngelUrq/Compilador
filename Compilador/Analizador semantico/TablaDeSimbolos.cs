using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.Analizador_semantico
{
    class TablaDeSimbolos
    {
        private List<Variable> variables;

        public TablaDeSimbolos()
        {
            variables = new List<Variable>();
        }
    }
}
