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

        private List<string> palabrasSeparadas;

        public ColoresInicio()
        {
            palabrasReservadas = new List<string>();
            tokens = new List<string>();
            tiposDeDatos = new List<string>();
            palabrasSeparadas = new List<string>();

            tokens.Add("=");
            tokens.Add("(");
            tokens.Add(")");
            tokens.Add("++");
            tokens.Add("--");
            tokens.Add(">");
            tokens.Add("<");
            tokens.Add(">=");
            tokens.Add("<=");
            tokens.Add("{");
            tokens.Add("}");
            tokens.Add(";");
            tokens.Add(",");

            palabrasReservadas.Add("Juice");
            palabrasReservadas.Add("Xplus");
            palabrasReservadas.Add("Xminus");
            palabrasReservadas.Add("Xdivide");
            palabrasReservadas.Add("Xpow");
            palabrasReservadas.Add("Xswash");
            palabrasReservadas.Add("agane");
            palabrasReservadas.Add("xqcThonk");
            palabrasReservadas.Add("xqcWut");

            tiposDeDatos.Add("Xint");
            tiposDeDatos.Add("Xreal");
            tiposDeDatos.Add("Xtring");
            tiposDeDatos.Add("Xbool");
        }

        public void SepararParabras(string texto)
        {
            string palabra = "";

            for (int i = 0; i < texto.Length; i++)
            {
                if (texto[i].Equals(" ") || texto[i].Equals("\n"))
                {
                    palabrasSeparadas.Add(palabra);
                    palabra = "";
                }
                else if (texto[i].Equals("{") || texto[i].Equals("}") || texto[i].Equals("(") || texto[i].Equals("="))
                {
                    palabrasSeparadas.Add(palabra);
                    palabrasSeparadas.Add(texto[i].ToString());
                    palabra = "";
                }
                else if (texto[i].Equals(">") || texto[i].Equals("<"))
                {
                    if (texto[i+1].Equals("="))
                    {
                        palabrasSeparadas.Add(palabra);
                        string operador = texto[i].ToString() + texto[i + 1];
                        palabrasSeparadas.Add(operador);
                        i++;
                        palabra = "";
                    }
                }
                else if (texto[i].Equals("+"))
                {
                    if (texto[i + 1].Equals("+"))
                    {
                        palabrasSeparadas.Add(palabra);
                        string operador = texto[i].ToString() + texto[i + 1];
                        palabrasSeparadas.Add(operador);
                        palabra = "";
                        i++;
                    }
                }
                else if (texto[i].Equals("-"))
                {
                    if (texto[i + 1].Equals("-"))
                    {
                        palabrasSeparadas.Add(palabra);
                        string operador = texto[i].ToString() + texto[i + 1];
                        palabrasSeparadas.Add(operador);
                        palabra = "";
                        i++;
                    }
                }
                else
                {
                    palabra += texto[i];
                }
            }
        }

        public string asignarValorPalabra()
        {
            string valor = "";
            bool esToken = false;
            bool esTipoDeDato = false;
            bool esPalabraReservada = true;


            for (int i = 0; i < palabrasSeparadas.Count; i++)
            {
                if (esPalabraReservada)
                {
                    for (int j = 0; j < palabrasReservadas.Count; j++)
                    {
                        if (palabrasSeparadas[i].Equals(palabrasReservadas[j]))
                        {
                            esPalabraReservada = true;
                        }
                    }
                }
                
            }

            return valor;
        }

        public string AsignarColor(string valorPalabra)
        {
            string color = "";

            switch (valorPalabra)
            {
                case "Palabra reservada":
                    color = "azul";
                    break;
                case "token":
                    color = "rojo";
                    break;
                case "tipo de dato":
                    color = "verde";
                    break;
                default:
                    color = "negro";
                    break;
            }
            return color;
        }

        public string ReconstruirOracion()
        {
            string linea = "";

            return linea;
        }

    }
}
