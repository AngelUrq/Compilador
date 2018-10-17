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
        ArrayList tipo = new ArrayList();
        ArrayList palabrares = new ArrayList();
        ArrayList delimitadores = new ArrayList();
        ArrayList oyf = new ArrayList();
        ArrayList ciclo = new ArrayList();
        ArrayList condicion = new ArrayList();

        private void Listas()
        {
            tipo.Add("Xint");
            tipo.Add("Xreal");
            tipo.Add("Xtring");
            tipo.Add("Xbool");

            oyf.Add("=");
            delimitadores.Add("(");
            delimitadores.Add(")");
            palabrares.Add("++");
            palabrares.Add("--");
            palabrares.Add(";");
            palabrares.Add("\"");
            palabrares.Add("\"");
            palabrares.Add("Juice");
            palabrares.Add("juice");
            palabrares.Add("Xgimme");
            palabrares.Add("Xtrue");
            palabrares.Add("Xfalse");
            oyf.Add(">");
            oyf.Add("<");
            delimitadores.Add("{");
            delimitadores.Add("}");
            delimitadores.Add("[");
            delimitadores.Add("]");
            palabrares.Add(",");
            palabrares.Add("Juice");

            oyf.Add("Xplus");
            oyf.Add("Xminus");
            oyf.Add("Xproduct");
            oyf.Add("Xdivide");
            oyf.Add("Xpow");
            oyf.Add("Xsmash");
            oyf.Add(">=");
            oyf.Add("<=");
            oyf.Add("+");
            oyf.Add("-");
            oyf.Add("*");
            oyf.Add("/");

            ciclo.Add("agane");

            condicion.Add("Xif");
            condicion.Add("Xelse");

        }

        public void Separar(string cadena)
        {
            int asa = 0;
            long asalong = 0;
            byte asabyte = 0;
            decimal asadec = 0;
            Listas();

            string aa = cadena.Replace("=", " = ").Replace("<", " < ").Replace(">", " > ").Replace("+", " + ").Replace("-", " - ")
                .Replace(">  =", " >= ").Replace("<  =", " <= ").Replace("*", " * ").Replace("/", " / ").Replace("(", " ( ").Replace(")", " ) ")
                .Replace("{", " { ").Replace("}", " } ").Replace("++", " ++ ").Replace("--", " -- ").Replace("[", " [ ").Replace("]", " ] ")
                .Replace(";", " ; ").Replace(",", " , ").Replace("\r\n", " ").Replace("\n", " #$ ").Replace("\r", " ");
            char[] delimiterChars = { ' ', '\t' };
            string[] arr = aa.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

            foreach (string i in arr)
            {
                if (ciclo.Contains(i))
                {
                    palabra.Add(i);
                    tipoda.Add("Ciclo");
                }
                else
                if (tipo.Contains(i))
                {
                    palabra.Add(i);
                    tipoda.Add("Tipo de Dato");
                }
                else
                    if (palabrares.Contains(i))
                {
                    palabra.Add(i);
                    tipoda.Add("Palabra clave");
                }
                else
                    if (oyf.Contains(i))
                {
                    palabra.Add(i);
                    tipoda.Add("Operador o funcion");
                }
                else
                    if (condicion.Contains(i))
                {
                    palabra.Add(i);
                    tipoda.Add("Condicion");
                }
                else
                if ((int.TryParse(i, out asa)) || (long.TryParse(i, out asalong)) || (byte.TryParse(i, out asabyte)) || (decimal.TryParse(i, out asadec)))
                {
                    palabra.Add(i);
                    tipoda.Add("Numero");
                }
                else
                if (i[0].Equals('\"') && i[i.Length - 1].Equals('\"'))
                {
                    palabra.Add(i);
                    tipoda.Add("Cadena");
                }
                else
                if (i.Equals("#$"))
                {
                    palabra.Add(i);
                    tipoda.Add("Salto");
                }
                else
                if (delimitadores.Contains(i))
                {
                    palabra.Add(i);
                    tipoda.Add("Delimitador");
                }
                else
                {
                    palabra.Add(i);
                    tipoda.Add("Identificador");
                }
            }
        }
    }
}
