using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    class ColoresInicio
    {
        private List<string> palabrasReservadas;
        private List<string> tokens;
        private List<string> tiposDeDatos;

        public List<string> palabrasSeparadas;
        // public List<string> colores;

        public ColoresInicio()
        {
            palabrasReservadas = new List<string>();
            tokens = new List<string>();
            tiposDeDatos = new List<string>();
            palabrasSeparadas = new List<string>();
            //colores = new List<string>();

            tokens.Add("(");
            tokens.Add(")");
            tokens.Add("++");
            tokens.Add("--");
            tokens.Add("bigger");
            tokens.Add("lower");
            tokens.Add("biggerOrEqual");
            tokens.Add("lowerOrEqual");
            tokens.Add("{");
            tokens.Add("}");
            tokens.Add(";");
            tokens.Add(":");
            tokens.Add(",");
            tokens.Add("->");
            tokens.Add("!");

            palabrasReservadas.Add("juice");
            palabrasReservadas.Add("agane");
            palabrasReservadas.Add("xqcThonk");
            palabrasReservadas.Add("xqcWut");
            palabrasReservadas.Add("give");
            palabrasReservadas.Add("start");
            palabrasReservadas.Add("xmain");
            palabrasReservadas.Add("final");

            tiposDeDatos.Add("tint");
            tiposDeDatos.Add("tfloat");
            tiposDeDatos.Add("tchar");
            tiposDeDatos.Add("tstring");
            tiposDeDatos.Add("tbool");
            tiposDeDatos.Add("void");
        }

        public void SepararParabras(string texto)
        {
            string palabra = "";
            List<string> palabras = new List<string>();

            for (int i = 0; i < texto.Length; i++)
            {
                if (texto[i] == '}')
                {
                    int a = 5;
                }
                if ((texto[i].ToString().Equals(" ") || texto[i].Equals('\n')) && !palabra.ToString().Equals(""))
                {
                    palabras.Add(palabra);
                    palabra = "";
                }
                else if ((texto[i].ToString().Equals("{") || texto[i].ToString().Equals("}") || texto[i].ToString().Equals("(") || texto[i].ToString().Equals(")")))
                {
                    palabras.Add(palabra);
                    palabras.Add(texto[i].ToString());
                    palabra = "";
                }
                else if (texto[i].ToString().Equals("+") && !palabra.ToString().Equals(""))
                {
                    if (texto[i + 1].ToString().Equals("+"))
                    {
                        palabras.Add(palabra);
                        string operador = texto[i].ToString() + texto[i + 1];
                        palabras.Add(operador);
                        palabra = "";
                        i++;
                    }
                }
                else if (texto[i].ToString().Equals("-") && !palabra.ToString().Equals(""))
                {
                    if (texto[i + 1].ToString().Equals("-") || texto[i + 1].ToString().Equals(">"))
                    {
                        palabras.Add(palabra);
                        string operador = texto[i].ToString() + texto[i + 1];
                        palabras.Add(operador);
                        palabra = "";
                        i++;
                    }
                }
                else if (texto[i].ToString().Equals(":") && !palabra.ToString().Equals(""))
                {
                    palabras.Add(palabra);
                    string operador = texto[i].ToString();
                    palabras.Add(operador);
                    palabra = "";

                }
                else if (texto[i].ToString().Equals("!") && !palabra.ToString().Equals(""))
                {
                    palabras.Add(palabra);
                    string operador = texto[i].ToString();
                    palabras.Add(operador);
                    palabra = "";

                }
                else if (!palabra.ToString().Equals("") && i == texto.Length - 1)
                {
                    palabra += texto[i];
                    palabras.Add(palabra);
                    palabra = "";
                }
                else
                {
                    palabra += texto[i];
                }
            }

            palabrasSeparadas = palabras;

        }

        public List<string> AsignarValorPalabra()
        {
            List<string> colores = new List<string>();
            for (int i = 0; i < palabrasSeparadas.Count; i++)
            {
                if (EsPalabraReservada(palabrasSeparadas[i]))
                {
                    colores.Add("azul");
                }
                else if (EsTipoDeDatos(palabrasSeparadas[i]))
                {
                    colores.Add("verde");
                }
                else if (EsToken(palabrasSeparadas[i]))
                {
                    colores.Add("rojo");
                }
                else
                {
                    colores.Add("negro");
                }
            }
            return colores;
        }

        public bool EsPalabraReservada(string palabra)
        {
            for (int i = 0; i < palabrasReservadas.Count; i++)
            {
                if (palabra.Equals(palabrasReservadas[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public bool EsTipoDeDatos(string palabra)
        {
            for (int i = 0; i < tiposDeDatos.Count; i++)
            {
                if (palabra.Equals(tiposDeDatos[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public bool EsToken(string palabra)
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                if (palabra.Equals(tokens[i]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
