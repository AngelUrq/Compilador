using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Compilador.Analizador_semantico
{
    class AnalizadorSemantico
    {
        List<Token> listaPalabras;
        List<Variable> variables;

        public AnalizadorSemantico(List<Token> listaPalabras)
        {
            this.listaPalabras = listaPalabras;
            variables = new List<Variable>();
        }

        public void Analizar()
        {
            List<Variable> variables = new List<Variable>();
            string funcion = "";

            for (int i = listaPalabras.Count - 1; i > 0; i--)
            {
                bool existe = false;
                try
                {
                    if (listaPalabras[i].GetPalabra().Equals("{"))
                    {
                        funcion = listaPalabras[i + 5].GetPalabra();

                    }

                    if (listaPalabras[i].GetTipo().Equals("Identificador") && listaPalabras[i - 1].GetPalabra().Equals("="))
                    {
                        Variable variable = new Variable(listaPalabras[i].GetPalabra(), listaPalabras[i + 1].GetPalabra(), listaPalabras[i - 2].GetPalabra(), listaPalabras[i].GetFila(), listaPalabras[i].GetColumna(), funcion);

                        if (variables.Count > 0)
                        {
                            for (int j = 0; j < variables.Count; j++)
                            {
                                if (variable.GetFuncion().Equals(variables[j].GetFuncion()) && variable.GetPalabra().Equals(variables[j].GetPalabra()))
                                {
                                    existe = true;
                                    continue;
                                }
                            }
                            if (!existe)
                            {
                                if (TipoYValorCorrecto(variable.GetTipoIdentificador(), variable.GetValor()))
                                {
                                    variables.Add(variable);
                                }
                                else
                                {
                                    Console.WriteLine("El valor asignado al tipo de la variable " + variable.GetPalabra() + " no es correcto.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error semántico, existen dos variables con el mismo nombre en la función " + funcion);
                            }
                        }
                        else
                        {
                            variables.Add(variable);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            }

            foreach (Variable variable in variables)
            {
                Console.WriteLine(variable.ToString());
            }
        }

        private bool TipoYValorCorrecto(string tipo, string valor)
        {
            bool correcto = false;

            switch (tipo)
            {
                case "tint":
                    Regex numerosEnteros = new Regex(@"[0-9]+");
                    correcto = numerosEnteros.IsMatch(valor);
                    break;
                case "tfloat":
                    Regex numerosFlotantes = new Regex(@"[0-9]+\.[0-9]+");
                    correcto = numerosFlotantes.IsMatch(valor);
                    break;
                case "tbool":
                    if (valor.Equals("true") || valor.Equals("false"))
                    {
                        correcto = true;
                    }
                    break;
                case "tstring":
                    Regex cadenas = new Regex(@"[a-zA-Z]+");
                    correcto = cadenas.IsMatch(valor);
                    break;
                case "tchar":
                    Regex caracteres = new Regex(@"[a-zA-Z]");
                    correcto = caracteres.IsMatch(valor);
                    break;
                default:
                    if (valor.Equals("null"))
                    {
                        correcto = true;
                    }
                    break;
            }

            //Console.WriteLine("Tipo: " + tipo + ", valor: " + valor + ", accion: " + correcto.ToString());
            return correcto;
        }

        private void Tbool()
        {
            List<Variable> variables = new List<Variable>();
            string funcion = "";

            for (int i = listaPalabras.Count - 1; i > 0; i--)
            {
                bool existe = false;
                try
                {
                    if (listaPalabras[i].GetPalabra().Equals("{"))
                    {
                        funcion = listaPalabras[i + 5].GetPalabra();

                    }
                    
                    if (listaPalabras[i].GetTipo().Equals("Identificador") && listaPalabras[i - 1].GetPalabra().Equals("tbool") && listaPalabras[i + 1].GetPalabra().Equals("=") && (listaPalabras[i + 2].GetPalabra().Equals("false") || listaPalabras[i + 2].GetPalabra().Equals("true")))
                    {
                        Variable variable = new Variable(listaPalabras[i].GetPalabra(), listaPalabras[i + 1].GetPalabra(), listaPalabras[i - 2].GetPalabra(), listaPalabras[i].GetFila(), listaPalabras[i].GetColumna(), funcion);

                        if (variables.Count > 0)
                        {
                            for (int j = 0; j < variables.Count; j++)
                            {
                                if (variable.GetFuncion().Equals(variables[j].GetFuncion()) && variable.GetPalabra().Equals(variables[j].GetPalabra()))
                                {
                                    existe = true;
                                    continue;
                                }
                            }
                            if (!existe)
                            {
                                if (TipoYValorCorrecto(variable.GetTipoIdentificador(), variable.GetValor()))
                                {
                                    variables.Add(variable);
                                }
                                else
                                {
                                    Console.WriteLine("El valor asignado al tipo de la variable " + variable.GetPalabra() + " no es correcto.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error semántico, existen dos variables con el mismo nombre en la función " + funcion);
                            }
                        }
                        else
                        {
                            variables.Add(variable);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            }

            foreach (Variable variable in variables)
            {
                Console.WriteLine(variable.ToString());
            }
        }
    }
}
