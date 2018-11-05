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
            List<string> v = new List<string>();
            string funcion = "";
            string cadenadevar = "";

            for (int i = listaPalabras.Count - 1; i > 0; i--)
            {
                bool existe = false;
                try
                {
                    if (listaPalabras[i].GetPalabra().Equals("{"))
                    {
                        funcion = listaPalabras[i + 5].GetPalabra();
                    }
                    if (listaPalabras[i].GetPalabra().Equals("tint") && listaPalabras[i - 2].GetPalabra().Equals("="))
                    {
                        int ind = i - 3;
                        for (int k = ind; listaPalabras[k].GetPalabra() != "!"; k--)
                        {
                            v.Add(listaPalabras[k].GetPalabra());
                            cadenadevar += listaPalabras[k].GetPalabra();
                        }
                        Variable variable = new Variable(listaPalabras[i - 1].GetPalabra(), listaPalabras[i].GetPalabra(), cadenadevar, listaPalabras[i].GetFila(), listaPalabras[i].GetColumna(), funcion);

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

                    else if (listaPalabras[i].GetTipo().Equals("Identificador") && listaPalabras[i - 1].GetPalabra().Equals("=") && !listaPalabras[i + 1].GetPalabra().Equals("tint"))
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
                    Regex aritmetica = new Regex(@"([-+]?[0 - 9] *\.?[0 - 9] +[\/\+\-\*])+([-+]?[0 - 9] *\.?[0-9]+)");
                    if (numerosEnteros.IsMatch(valor))
                    {
                        correcto = true;
                    }
                    else if (aritmetica.IsMatch(valor))
                    {
                        correcto = true;
                    }
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

        public void VerificarCondiciones(String CodigoFuente, int NumeroLinea)
        {
            String error = "false";
            int ParenthesisIzq = 0;
            int ParenthesisDer = 0;
            String CodigoReemplazado = CodigoFuente.Replace("(", " ( ").Replace(")", " ) ");
            Console.WriteLine(CodigoReemplazado);
            char[] delimitadoresChars = { ' ', '\t' };
            string[] condicion = CodigoReemplazado.Split(delimitadoresChars, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < condicion.Length; i++)
            {
                //Verifica que la palabra pertenesca a las condiciones
                if (condicion[i].Equals("true") || condicion[i].Equals("false") || condicion[i].Equals("and") || condicion[i].Equals("or") || condicion[i].Equals("not") ||
                    condicion[i].Equals("xqcThonk") || condicion[i].Equals("Agane") || condicion[i].Equals("(") || condicion[i].Equals(")"))
                {
                    //Pertenece
                }
                else
                {
                    Form1._Form1.update("Error Semantico: *" + condicion[i] + "* No Es Valido |Fila: " + NumeroLinea.ToString() + "\n");
                    error = "true";
                }
                //Verifica si se esta abriendo otro parenthesis "()" Ej: Agane(true)(true)
                if (condicion[i].Equals("("))
                {
                    Console.WriteLine("");
                    if (ParenthesisIzq != 0 && ParenthesisDer != 0)
                    {
                        if (ParenthesisIzq == ParenthesisDer)
                        {
                            Form1._Form1.update("Error Semantico: Error en los Parenthesis" + "|Linea : " + NumeroLinea.ToString() + " \n");
                            error = "true";
                        }
                    }
                }
                //Se encarga de contar los parenthesis
                if (condicion[i].Equals("("))
                {
                    ParenthesisIzq++;
                }
                else if (condicion[i].Equals(")"))
                {
                    ParenthesisDer++;
                }


            }

            for (int i = 0; i < condicion.Length - 1; i++)
            {

                //Esto verifica que si despues de la palabra reservada hay un parenthesis abierto "("
                if (condicion[i].Equals("xqcThonk") && condicion[i + 1].Equals(")"))
                {
                    Form1._Form1.update("Error Semantico: Error de Parenthesis despues de *" + condicion[i] + "* |Fila: " + NumeroLinea.ToString() + "\n");
                    error = "true";
                }
                else if (condicion[i].Equals("Agane") && condicion[i + 1].Equals(")"))
                {
                    Form1._Form1.update("Error Semantico: Error de Parenthesis despues de *" + condicion[i] + "* |Fila: " + NumeroLinea.ToString() + "\n");
                    error = "true";
                }
                //Verifica que si despues de "(" hay un ")" que significa que el contenido dentro del parenthesis esta vacio
                if (condicion[i].Equals("(") && condicion[i + 1].Equals(")"))
                {
                    Form1._Form1.update("Error Semantico: No Existen Datos dentro los Parenthesis |Fila: " + NumeroLinea + "\n");
                    error = "true";
                }
                //Verifica si existan duplicados que esten a lado ej: xqcThonk(true true), xqcThonk(false true) etc..
                if (condicion[i].Equals("true") && condicion[i + 1].Equals("true") || condicion[i].Equals("true") && condicion[i + 1].Equals("false"))
                {
                    Form1._Form1.update("Error Semantico: *" + condicion[i] + " " + condicion[i + 1] + "* No es Valido" + " |Fila: " + NumeroLinea + "\n");
                    error = "true";
                }
                else if (condicion[i].Equals("false") && condicion[i + 1].Equals("true") || condicion[i].Equals("false") && condicion[i + 1].Equals("false"))
                {
                    Form1._Form1.update("Error Semantico: *" + condicion[i] + " " + condicion[i + 1] + "* No es Valido" + " |Fila: " + NumeroLinea + "\n");
                    error = "true";
                }
                else if (condicion[i].Equals("not") && condicion[i + 1].Equals("not"))
                {
                    Form1._Form1.update("Error Semantico: *" + condicion[i] + " " + condicion[i + 1] + "* No es Valido" + " |Fila: " + NumeroLinea + "\n");
                    error = "true";
                }
                //Verifica si existen duplicados del "and" y "or" ej: xqcThonk(true and and true), xqcThonk(false and or true) etc..
                if (condicion[i].Equals("and") && condicion[i + 1].Equals("or") || condicion[i].Equals("and") && condicion[i + 1].Equals("and"))
                {
                    Form1._Form1.update("Error Semantico: *" + condicion[i] + " " + condicion[i + 1] + "* No es Valido" + " |Fila: " + NumeroLinea + "\n");
                    error = "true";
                }
                else if (condicion[i].Equals("or") && condicion[i + 1].Equals("and") || condicion[i].Equals("or") && condicion[i + 1].Equals("or"))
                {
                    Form1._Form1.update("Error Semantico: *" + condicion[i] + " " + condicion[i + 1] + "* No es Valido" + " |Fila: " + NumeroLinea + "\n");
                    error = "true";
                }
                //Verifica si se abre parenthesis despues de un true o false
                if (condicion[i].Equals("true") && condicion[i + 1].Equals("(") || condicion[i].Equals("false") && condicion[i + 1].Equals("("))
                {
                    Form1._Form1.update("Error Semantico: *" + condicion[i] + " " + condicion[i + 1] + "* No es Valido" + " |Fila: " + NumeroLinea + "\n");
                    error = "true";
                }
                //Verifica si existe un not despues de un true o false
                if (condicion[i].Equals("true") && condicion[i + 1].Equals("not") || condicion[i].Equals("false") && condicion[i + 1].Equals("not"))
                {
                    Form1._Form1.update("Error Semantico: *" + condicion[i] + " " + condicion[i + 1] + "* No es Valido" + " |Fila: " + NumeroLinea + "\n");
                    error = "true";
                }
                //Verifica si se despues de un "not" hay un ")"
                if (condicion[i].Equals("not") && condicion[i + 1].Equals(")"))
                {
                    Form1._Form1.update("Error Semantico: *" + condicion[i] + " " + condicion[i + 1] + "* No es Valido" + " |Fila: " + NumeroLinea + "\n");
                    error = "true";
                }


            }
            if (ParenthesisIzq != ParenthesisDer)
            {
                Form1._Form1.update("Error Semantico: Los Parenthesis no estan Balanceados" + " |Fila: " + NumeroLinea + "\n");
                error = "true";
                Console.WriteLine(ParenthesisIzq.ToString() + " = " + ParenthesisDer.ToString());
            }
            
        }
		public void Separarcomprobaciones(List<Token> valores)
		{
			int posinit = 0;
			for (int x = 0; x < valores.Count; x++)
			{
				List<Token> nuevalinea = new List<Token>();
				if (valores[x].GetPalabra() == "!")
				{
					for (int a = posinit; a < x; a++)
					{
						nuevalinea.Add(new Token(valores[a].GetPalabra(), valores[a].GetTipo(), valores[a].GetColumna(), valores[a].GetFila()));
					}
					Comprobarvariablesasignaciones(nuevalinea);
					nuevalinea.Clear();
					posinit = x + 1;
				}

			}
		}
		public void Comprobarvariablesasignaciones(List<Token> lista)
		{
			switch (lista[0].GetTipo())
			{
				case "tbool":

					if ((lista.Count - 3) == 1)
					{
						if (lista[lista.Count - 1].GetTipo() == lista[0].GetTipo())
						{
							Console.WriteLine("es correcto");
						}
						else
						{
							Console.WriteLine("error en fila: " + lista[lista.Count - 1].GetFila() + " columna " + lista[lista.Count - 1].GetColumna() + " palabra  " + lista[lista.Count - 1].GetPalabra());
						}
					}
					else if ((lista.Count - 3) > 1)
					{
						bool comp = true;
						int pos = 0;
						for (int x = 3; x < lista.Count; x++)
						{
							if ((lista[x].GetTipo() != lista[0].GetTipo()) && x % 2 != 0)
							{
								comp = false;
								pos = x;
								break;
							}
						}
						if (comp)
						{
							Console.WriteLine("es correcto");
						}
						else
						{
							Console.WriteLine("error en fila: " + lista[pos].GetFila() + " columna " + lista[pos].GetColumna() + " palabra  " + lista[pos].GetPalabra());
						}
					}
					else
					{
						Console.WriteLine("error de asignación ");
					}
					break;
				case "tint":
					if ((lista.Count - 3) == 1)
					{
						if (lista[lista.Count - 1].GetTipo() == lista[0].GetTipo())
						{
							Console.WriteLine("es correcto");
						}
						else
						{
							Console.WriteLine("error en fila: " + lista[lista.Count - 1].GetFila() + " columna " + lista[lista.Count - 1].GetColumna() + " palabra  " + lista[lista.Count - 1].GetPalabra());
						}
					}
					else if ((lista.Count - 3) > 1)
					{
						bool comp = true;
						int pos = 0;
						for (int x = 3; x < lista.Count; x++)
						{
							if ((lista[x].GetTipo() != lista[0].GetTipo()) && x % 2 != 0)
							{
								comp = false;
								pos = x;
								break;
							}
						}
						if (comp)
						{
							Console.WriteLine("es correcto");
						}
						else
						{
							Console.WriteLine("error en fila: " + lista[pos].GetFila() + " columna " + lista[pos].GetColumna() + " palabra  " + lista[pos].GetPalabra());
						}
					}
					else
					{
						Console.WriteLine("error de asignación ");
					}
					break;
				case "tfloat":
					if ((lista.Count - 3) == 1)
					{
						if (lista[lista.Count - 1].GetTipo() == lista[0].GetTipo())
						{
							Console.WriteLine("es correcto");
						}
						else
						{
							Console.WriteLine("error en fila: " + lista[lista.Count - 1].GetFila() + " columna " + lista[lista.Count - 1].GetColumna() + " palabra  " + lista[lista.Count - 1].GetPalabra());
						}
					}
					else if ((lista.Count - 3) > 1)
					{
						bool comp = true;
						int pos = 0;
						for (int x = 3; x < lista.Count; x++)
						{
							if ((lista[x].GetTipo() != lista[0].GetTipo()) && x % 2 != 0)
							{
								comp = false;
								pos = x;
								break;
							}
						}
						if (comp)
						{
							Console.WriteLine("es correcto");
						}
						else
						{
							Console.WriteLine("error en fila: " + lista[pos].GetFila() + " columna " + lista[pos].GetColumna() + " palabra  " + lista[pos].GetPalabra());
						}
					}
					else
					{
						Console.WriteLine("error de asignación ");
					}
					break;
				case "tstring":
					if ((lista.Count - 3) == 1)
					{
						if (lista[lista.Count - 1].GetTipo() == lista[0].GetTipo())
						{
							Console.WriteLine("es correcto");
						}
						else
						{
							Console.WriteLine("error en fila: " + lista[lista.Count - 1].GetFila() + " columna " + lista[lista.Count - 1].GetColumna() + " palabra  " + lista[lista.Count - 1].GetPalabra());
						}
					}
					else if ((lista.Count - 3) > 1)
					{
						bool comp = true;
						int pos = 0;
						for (int x = 3; x < lista.Count; x++)
						{
							if ((lista[x].GetTipo() != lista[0].GetTipo()) && x % 2 != 0)
							{
								comp = false;
								pos = x;
								break;
							}
						}
						if (comp)
						{
							Console.WriteLine("es correcto");
						}
						else
						{
							Console.WriteLine("error en fila: " + lista[pos].GetFila() + " columna " + lista[pos].GetColumna() + " palabra  " + lista[pos].GetPalabra());
						}
					}
					else
					{
						Console.WriteLine("error de asignación ");
					}
					break;
				case "tchar":
					if ((lista.Count - 3) == 1)
					{
						if (lista[lista.Count - 1].GetTipo() == lista[0].GetTipo())
						{
							Console.WriteLine("es correcto");
						}
						else
						{
							Console.WriteLine("error en fila: " + lista[lista.Count - 1].GetFila() + " columna " + lista[lista.Count - 1].GetColumna() + " palabra  " + lista[lista.Count - 1].GetPalabra());
						}
					}
					else if ((lista.Count - 3) > 1)
					{
						bool comp = true;
						int pos = 0;
						for (int x = 3; x < lista.Count; x++)
						{
							if ((lista[x].GetTipo() != lista[0].GetTipo()) && x % 2 != 0)
							{
								comp = false;
								pos = x;
								break;
							}
						}
						if (comp)
						{
							Console.WriteLine("es correcto");
						}
						else
						{
							Console.WriteLine("error en fila: " + lista[pos].GetFila() + " columna " + lista[pos].GetColumna() + " palabra  " + lista[pos].GetPalabra());
						}
					}
					else
					{
						Console.WriteLine("error de asignación ");
					}
					break;
				default:
					Console.WriteLine("no hay tipo");
					break;
			}
		}
	}
}
