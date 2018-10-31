using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.Analizador_semantico
{
    class Variable : Token
    {
        private string tipoIdentificador;
        private string valor;

        public Variable(string nombre, string tipoI, string valor, int fila, int columna): base(nombre, "Identificador", columna, fila)
        {
            this.tipoIdentificador = tipoI;
            this.valor = valor;
        }

        public string GetTipoIdentificador()
        {
            return this.tipoIdentificador;
        }

        public void SetTipo(string tipo)
        {
            this.tipoIdentificador = tipo;
        }

        public string GetValor()
        {
            return this.valor;
        }

        public void SetValor(string valor)
        {
            this.valor = valor;
        }
    }
}
