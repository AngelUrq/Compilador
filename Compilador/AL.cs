using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    class AL
    {
        public ArrayList palabra = new ArrayList();
        public ArrayList tipoda = new ArrayList();
        public ArrayList tamanio = new ArrayList();
        ArrayList palabrares = new ArrayList();
        ArrayList operadores = new ArrayList();
        ArrayList delimitadores = new ArrayList();
        ArrayList tipo = new ArrayList();

        private void Listas()
        {
            palabrares.Add("start");
            palabrares.Add("final");
            palabrares.Add("juice");
            palabrares.Add("give");
            palabrares.Add("xqcThonk");
            palabrares.Add("xqcWut");
            palabrares.Add("agane");

            operadores.Add("equals");
            operadores.Add("bigger");
            operadores.Add("lower");
            operadores.Add("lowerOrEqual");
            operadores.Add("biggerOrEqual");
            operadores.Add("and");
            operadores.Add("or");
            operadores.Add("not");
            operadores.Add("+");
            operadores.Add("-");
            operadores.Add("/");
            operadores.Add("*");
            operadores.Add("^");
            operadores.Add("->");

            delimitadores.Add(":");
            delimitadores.Add("!");
            delimitadores.Add("(");
            delimitadores.Add(")");
            delimitadores.Add("[");
            delimitadores.Add("]");
            delimitadores.Add("{");
            delimitadores.Add("}");
            delimitadores.Add(",");

            tipo.Add("xint");
            tipo.Add("xfloat");
            tipo.Add("xbool");
            tipo.Add("void");
            tipo.Add("xtring");
            tipo.Add("xchar");
        }

        public void Separar(string cadena)
        {
            int asa = 0;
            long asalong = 0;
            byte asabyte = 0;
            decimal asadec = 0;
            Listas();

            string aa = cadena.Replace("+", " + ").Replace("-", " - ").Replace("!", " ! ").Replace("->", " -> ")
                .Replace("^", " ^ ").Replace("*", " * ").Replace("/", " / ").Replace("(", " ( ").Replace(")", " ) ")
                .Replace("{", " { ").Replace("}", " } ").Replace("[", " [ ").Replace("]", " ] ").Replace(":", " : ")
                .Replace("\n", " #$ ").Replace("\r", " ").Replace(","," , ");
            char[] delimiterChars = { ' ', '\t' };
            string[] arr = aa.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

            foreach (string i in arr)
            {
                if (tipo.Contains(i))
                {
                    palabra.Add(i);
                    tipoda.Add("Tipo de Dato");
                    tamanio.Add(i.Length);
                }
                else if (palabrares.Contains(i))
                {
                    palabra.Add(i);
                    tipoda.Add("Palabra clave");
                    tamanio.Add(i.Length);
                }
                else if (operadores.Contains(i))
                {
                    palabra.Add(i);
                    tipoda.Add("Operador o funcion");
                    tamanio.Add(i.Length);
                }
                else if ((int.TryParse(i, out asa)) || (long.TryParse(i, out asalong)) || (byte.TryParse(i, out asabyte)) || (decimal.TryParse(i, out asadec)))
                {
                    palabra.Add(i);
                    tipoda.Add("Numero");
                    tamanio.Add(i.Length);
                }
                else if (i[0].Equals('\"') && i[i.Length - 1].Equals('\"'))
                {
                    palabra.Add(i);
                    tipoda.Add("Cadena");
                    tamanio.Add(i.Length);
                }
                else if (i.Equals("#$"))
                {
                    palabra.Add(i);
                    tipoda.Add("Salto");
                    tamanio.Add(i.Length);
                }
                else if (delimitadores.Contains(i))
                {
                    palabra.Add(i);
                    tipoda.Add("Delimitador");
                    tamanio.Add(i.Length);
                }
                else if (i[0].ToString().Equals("x"))
                {
                    palabra.Add(i);
                    tipoda.Add("Identificador");
                    tamanio.Add(i.Length);
                }
                else
                {
                    palabra.Add(i);
                    tipoda.Add("Error");
                    tamanio.Add(i.Length);
                }
            }
        }
    }
}