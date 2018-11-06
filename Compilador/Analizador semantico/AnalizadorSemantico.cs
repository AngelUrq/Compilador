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
        List<string> funciones;

        public AnalizadorSemantico(List<Token> listaPalabras)
        {
            this.listaPalabras = listaPalabras;
            variables = new List<Variable>();
            funciones = new List<string>();
        }

        public void AnalizarFunciones()
        {
            List<Variable> variables = new List<Variable>();
            List<string> v = new List<string>();

            bool existe = false;
            bool funcionUnica = true;

            string funcion = "";
            string tipoFuncion = "";

            for (int i = listaPalabras.Count - 1; i > 0; i--)
            {
                try
                {
                    string cadenadevar = "";
                    bool comprobado = false;

                    if (listaPalabras[i].GetPalabra().Equals("juice"))
                    {
                        funcion = listaPalabras[i - 1].GetPalabra();
                        if (!funciones.Contains(funcion))
                        {
                            funciones.Add(funcion);
                            funcionUnica = true;
                        }
                        else
                        {
                            Form1._Form1.AñadirConsola("Existen dos funciones con el nombre " + funcion);
                            funcionUnica = false;
                        }
                    }

                    if (listaPalabras[i].GetPalabra().Equals("->") && funcionUnica)
                    {
                        tipoFuncion = listaPalabras[i - 1].GetPalabra();
                    }

                    if (listaPalabras[i].GetPalabra().Equals("give") && funcionUnica)
                    {
                        for (int j = 0; j < variables.Count; j++)
                        {
                            if (listaPalabras[i - 1].GetPalabra().Equals(variables[j].GetPalabra()))
                            {
                                if (!TipoYValorCorrecto(tipoFuncion, variables[j].GetValor()))
                                {
                                    Form1._Form1.AñadirConsola("¡Error!, la función " + funcion + " de tipo " + tipoFuncion + " no devuelve un tipo de datos coherente.");
                                }
                                else
                                {
                                    Form1._Form1.AñadirConsola("La función " + funcion + " devuelve el tipo de datos correcto");
                                }
                                existe = true;
                                continue;
                            }
                        }

                        if (!existe)
                        {
                            if (!TipoYValorCorrecto(tipoFuncion, listaPalabras[i - 1].GetPalabra()))
                            {
                                Form1._Form1.AñadirConsola("¡Error!, la función " + funcion + " de tipo " + tipoFuncion + " no devuelve un tipo de datos coherente.");
                            }
                            else
                            {

                                Form1._Form1.AñadirConsola("La función " + funcion + " devuelve el tipo de datos correcto");
                            }
                        }

                    }


                   if (listaPalabras[i].GetTipo().Equals("Identificador") && listaPalabras[i - 1].GetPalabra().Equals("=") && !listaPalabras[i + 1].GetPalabra().Equals("tint") && funcionUnica && listaPalabras[i - 3].GetPalabra().Equals("!"))
                    {
                        Variable variable = new Variable(listaPalabras[i].GetPalabra(), listaPalabras[i + 1].GetPalabra(), listaPalabras[i - 2].GetPalabra(), listaPalabras[i].GetFila(), listaPalabras[i].GetColumna(), funcion);

                        if (variables.Count > 0)
                        {
                            if (!ContieneVariable(variables, variable))
                            {
                                if (TipoYValorCorrecto(variable.GetTipoIdentificador(), variable.GetValor()))
                                {
                                    variables.Add(variable);
                                }
                                else
                                {
                                    Form1._Form1.AñadirConsola("El valor asignado al tipo de la variable " + variable.GetPalabra() + " no es correcto. Fila: " + variable.GetFila() + ", columna: " + variable.GetColumna());
                                }
                            }
                            else
                            {
                                Form1._Form1.AñadirConsola("Error semántico, existen dos variables con el mismo nombre en la función " + funcion);
                            }
                        }
                        else
                        {
                            variables.Add(variable);
                        }
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

                        if (variables.Count > 0)
                        {
                            if (!ContieneVariable(variables, variable))
                            {
                                if (TipoYValorCorrecto(variable.GetTipoIdentificador(), variable.GetValor()))
                                {
                                    variables.Add(variable);
                                }
                                else
                                {
                                    Console.WriteLine("El valor asignado al tipo de la variable " + variable.GetPalabra() + " no es correcto. Fila: " + variable.GetFila() + ", columna: " + variable.GetColumna());
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
                //Form1._Form1.AñadirConsola(variable.ToString());
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
                    Regex cadenas = new Regex('\x22' + ".*" + '\x22');
                    correcto = cadenas.IsMatch(valor);
                    break;
                case "tchar":
                    Regex caracteres = new Regex(@"\'[a-zA-Z]\'");
                    correcto = caracteres.IsMatch(valor);
                    break;
                default:
                    if (valor.Equals("null"))
                    {
                        correcto = true;
                    }
                    break;
            }

            //Form1._Form1.AñadirConsola("Tipo: " + tipo + ", valor: " + valor + ", accion: " + correcto.ToString());
            return correcto;
        }

        private bool ContieneVariable(List<Variable> variables, Variable variable)
        {
            for (int j = 0; j < variables.Count; j++)
            {
                if (variable.GetFuncion().Equals(variables[j].GetFuncion()) && variable.GetPalabra().Equals(variables[j].GetPalabra()))
                {
                    return true;
                }
            }
            return false;
        }

        /*private void Tbool()
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
                                    Form1._Form1.AñadirConsola("El valor asignado al tipo de la variable " + variable.GetPalabra() + " no es correcto.");
                                }
                            }
                            else
                            {
                                Form1._Form1.AñadirConsola("Error semántico, existen dos variables con el mismo nombre en la función " + funcion);
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
                Form1._Form1.AñadirConsola(variable.ToString());
            }
        }*/

        public void VerificarCondiciones(String CodigoFuente, int NumeroLinea)
        {
            String error = "false";
            int ParenthesisIzq = 0;
            int ParenthesisDer = 0;
            String CodigoReemplazado = CodigoFuente.Replace("(", " ( ").Replace(")", " ) ");
            Form1._Form1.AñadirConsola(CodigoReemplazado);
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
                    Form1._Form1.AñadirConsola("Error Semantico: *" + condicion[i] + "* No Es Valido |Fila: " + NumeroLinea.ToString() + "\n");
                    error = "true";
                }
                //Verifica si se esta abriendo otro parenthesis "()" Ej: Agane(true)(true)
                if (condicion[i].Equals("("))
                {
                    Form1._Form1.AñadirConsola("");
                    if (ParenthesisIzq != 0 && ParenthesisDer != 0)
                    {
                        if (ParenthesisIzq == ParenthesisDer)
                        {
                            Form1._Form1.AñadirConsola("Error Semantico: Error en los Parenthesis" + "|Linea : " + NumeroLinea.ToString() + " \n");
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
                    Form1._Form1.AñadirConsola("Error Semantico: Error de Parenthesis despues de *" + condicion[i] + "* |Fila: " + NumeroLinea.ToString() + "\n");
                    error = "true";
                }
                else if (condicion[i].Equals("Agane") && condicion[i + 1].Equals(")"))
                {
                    Form1._Form1.AñadirConsola("Error Semantico: Error de Parenthesis despues de *" + condicion[i] + "* |Fila: " + NumeroLinea.ToString() + "\n");
                    error = "true";
                }
                //Verifica que si despues de "(" hay un ")" que significa que el contenido dentro del parenthesis esta vacio
                if (condicion[i].Equals("(") && condicion[i + 1].Equals(")"))
                {
                    Form1._Form1.AñadirConsola("Error Semantico: No Existen Datos dentro los Parenthesis |Fila: " + NumeroLinea + "\n");
                    error = "true";
                }
                //Verifica si existan duplicados que esten a lado ej: xqcThonk(true true), xqcThonk(false true) etc..
                if (condicion[i].Equals("true") && condicion[i + 1].Equals("true") || condicion[i].Equals("true") && condicion[i + 1].Equals("false"))
                {
                    Form1._Form1.AñadirConsola("Error Semantico: *" + condicion[i] + " " + condicion[i + 1] + "* No es Valido" + " |Fila: " + NumeroLinea + "\n");
                    error = "true";
                }
                else if (condicion[i].Equals("false") && condicion[i + 1].Equals("true") || condicion[i].Equals("false") && condicion[i + 1].Equals("false"))
                {
                    Form1._Form1.AñadirConsola("Error Semantico: *" + condicion[i] + " " + condicion[i + 1] + "* No es Valido" + " |Fila: " + NumeroLinea + "\n");
                    error = "true";
                }
                else if (condicion[i].Equals("not") && condicion[i + 1].Equals("not"))
                {
                    Form1._Form1.AñadirConsola("Error Semantico: *" + condicion[i] + " " + condicion[i + 1] + "* No es Valido" + " |Fila: " + NumeroLinea + "\n");
                    error = "true";
                }
                //Verifica si existen duplicados del "and" y "or" ej: xqcThonk(true and and true), xqcThonk(false and or true) etc..
                if (condicion[i].Equals("and") && condicion[i + 1].Equals("or") || condicion[i].Equals("and") && condicion[i + 1].Equals("and"))
                {
                    Form1._Form1.AñadirConsola("Error Semantico: *" + condicion[i] + " " + condicion[i + 1] + "* No es Valido" + " |Fila: " + NumeroLinea + "\n");
                    error = "true";
                }
                else if (condicion[i].Equals("or") && condicion[i + 1].Equals("and") || condicion[i].Equals("or") && condicion[i + 1].Equals("or"))
                {
                    Form1._Form1.AñadirConsola("Error Semantico: *" + condicion[i] + " " + condicion[i + 1] + "* No es Valido" + " |Fila: " + NumeroLinea + "\n");
                    error = "true";
                }
                //Verifica si se abre parenthesis despues de un true o false
                if (condicion[i].Equals("true") && condicion[i + 1].Equals("(") || condicion[i].Equals("false") && condicion[i + 1].Equals("("))
                {
                    Form1._Form1.AñadirConsola("Error Semantico: *" + condicion[i] + " " + condicion[i + 1] + "* No es Valido" + " |Fila: " + NumeroLinea + "\n");
                    error = "true";
                }
                //Verifica si existe un not despues de un true o false
                if (condicion[i].Equals("true") && condicion[i + 1].Equals("not") || condicion[i].Equals("false") && condicion[i + 1].Equals("not"))
                {
                    Form1._Form1.AñadirConsola("Error Semantico: *" + condicion[i] + " " + condicion[i + 1] + "* No es Valido" + " |Fila: " + NumeroLinea + "\n");
                    error = "true";
                }
                //Verifica si se despues de un "not" hay un ")"
                if (condicion[i].Equals("not") && condicion[i + 1].Equals(")"))
                {
                    Form1._Form1.AñadirConsola("Error Semantico: *" + condicion[i] + " " + condicion[i + 1] + "* No es Valido" + " |Fila: " + NumeroLinea + "\n");
                    error = "true";
                }


            }
            if (ParenthesisIzq != ParenthesisDer)
            {
                Form1._Form1.AñadirConsola("Error Semantico: Los Parenthesis no estan Balanceados" + " |Fila: " + NumeroLinea + "\n");
                error = "true";
                Form1._Form1.AñadirConsola(ParenthesisIzq.ToString() + " = " + ParenthesisDer.ToString());
            }
            
        }

		public void Separarcomprobaciones(List<Token> valores, String funcion)
		{
			int posfinal = 0;
			int posinit = 0;
			for (int x = 0; x < valores.Count; x++)
			{
				List<Token> nuevalinea = new List<Token>();
				if (valores[x].GetPalabra() == "=")
				{
					posinit = x - 2;
					for (int a = posinit; a < valores.Count; a++)
					{
						if (valores[a].GetPalabra() != "!")
						{
							nuevalinea.Add(new Token(valores[a].GetPalabra(), valores[a].GetTipo(), valores[a].GetColumna(), valores[a].GetFila()));
						}
						else
						{
							posfinal = a;
							break;
						}
					}
					Comprobarvariablesasignaciones(nuevalinea, funcion);
					nuevalinea.Clear();
					x = posfinal;

				}

			}
		}
		public void Comprobarvariablesasignaciones(List<Token> lista, String funcion)
		{

			switch (lista[0].GetPalabra())
			{
				case "tbool":

					if ((lista.Count - 3) == 1)
					{
						if (Ver_tipo(lista[lista.Count - 1], "tbool"))
						{
							Form1._Form1.AñadirConsola("es correcto");
							bool nr = true;
							for (int m = 0; m < variables.Count; m++)
							{
								if (variables[m].GetPalabra() == lista[ 1].GetPalabra())
								{
									nr = false;
								}
							}
							if (nr)
							variables.Add(new Variable(lista[1].GetPalabra(), lista[0].GetPalabra(), lista[3].GetPalabra(), lista[1].GetFila(), lista[1].GetColumna(), funcion));

						}
						else
						{
							Form1._Form1.AñadirConsola("error en fila: " + lista[lista.Count - 1].GetFila() + " columna " + lista[lista.Count - 1].GetColumna() + " palabra  " + lista[lista.Count - 1].GetPalabra());
						}
					}
					else if ((lista.Count - 3) > 1)
					{
						String valor = "";
						bool comp = true;
						int pos = 0;
						for (int x = 3; x < lista.Count; x++)
						{
							if (!Ver_tipo(lista[x], "tbool") && x % 2 != 0)
							{
								comp = false;
								pos = x;
								break;
							}
							valor += lista[x].GetPalabra() + " ";
						}
						if (comp)
						{
							Form1._Form1.AñadirConsola("es correcto");
							bool nr = true;
							for (int m = 0; m < variables.Count; m++)
							{
								if (variables[m].GetPalabra() == lista[1].GetPalabra())
								{
									nr = false;
								}
							}
							if (nr)
							variables.Add(new Variable(lista[1].GetPalabra(), lista[0].GetPalabra(), valor, lista[1].GetFila(), lista[1].GetColumna(), funcion));

						}
						else
						{
							Form1._Form1.AñadirConsola("error en fila: " + lista[pos].GetFila() + " columna " + lista[pos].GetColumna() + " palabra  " + lista[pos].GetPalabra());
						}
					}
					else
					{
						Form1._Form1.AñadirConsola("error de asignación ");
					}
					break;
				case "tint":
					if ((lista.Count - 3) == 1)
					{
						if (Ver_tipo(lista[lista.Count - 1], "tint"))
						{
							Form1._Form1.AñadirConsola("es correcto");
							bool nr = true;
							for (int m = 0; m < variables.Count; m++)
							{
								if (variables[m].GetPalabra() == lista[1].GetPalabra())
								{
									nr = false;
								}
							}
							if (nr)
							variables.Add(new Variable(lista[1].GetPalabra(), lista[0].GetPalabra(), lista[3].GetPalabra(), lista[1].GetFila(), lista[1].GetColumna(), funcion));

						}
						else
						{
							Form1._Form1.AñadirConsola("error en fila: " + lista[lista.Count - 1].GetFila() + " columna " + lista[lista.Count - 1].GetColumna() + " palabra  " + lista[lista.Count - 1].GetPalabra());
						}
					}
					else if ((lista.Count - 3) > 1)
					{
						bool comp = true;
						int pos = 0;
						string valor = "";
						for (int x = 3; x < lista.Count; x++)
						{
							if (!Ver_tipo(lista[x], "tint") && x % 2 != 0)
							{
								comp = false;
								pos = x;
								break;
							}
							valor += lista[x].GetPalabra() + " ";
						}
						if (comp)
						{
							Form1._Form1.AñadirConsola("es correcto");
							bool nr = true;
							for (int m = 0; m < variables.Count; m++)
							{
								if (variables[m].GetPalabra() == lista[1].GetPalabra())
								{
									nr = false;
								}
							}
							if (nr)
							variables.Add(new Variable(lista[1].GetPalabra(), lista[0].GetPalabra(), valor, lista[1].GetFila(), lista[1].GetColumna(), funcion));

						}
						else
						{
							Form1._Form1.AñadirConsola("error en fila: " + lista[pos].GetFila() + " columna " + lista[pos].GetColumna() + " palabra  " + lista[pos].GetPalabra());
						}
					}
					else
					{
						Form1._Form1.AñadirConsola("error de asignación ");
					}
					break;
				case "tfloat":
					if ((lista.Count - 3) == 1)
					{
						if (Ver_tipo(lista[lista.Count - 1], "tfloat"))
						{
							Form1._Form1.AñadirConsola("es correcto");
							bool nr = true;
							for (int m = 0; m < variables.Count; m++)
							{
								if (variables[m].GetPalabra() == lista[1].GetPalabra())
								{
									nr = false;
								}
							}
							if (nr)
							variables.Add(new Variable(lista[1].GetPalabra(), lista[0].GetPalabra(), lista[3].GetPalabra(), lista[1].GetFila(), lista[1].GetColumna(), funcion));

						}
						else
						{
							Form1._Form1.AñadirConsola("error en fila: " + lista[lista.Count - 1].GetFila() + " columna " + lista[lista.Count - 1].GetColumna() + " palabra  " + lista[lista.Count - 1].GetPalabra());
						}
					}
					else if ((lista.Count - 3) > 1)
					{
						bool comp = true;
						int pos = 0;
						string valor = "";
						for (int x = 3; x < lista.Count; x++)
						{
							if (!Ver_tipo(lista[x], "tfloat") && x % 2 != 0)
							{
								comp = false;
								pos = x;
								break;
							}
							valor += lista[x].GetPalabra() + " ";
						}
						if (comp)
						{
							Form1._Form1.AñadirConsola("es correcto");
							bool nr = true;
							for (int m = 0; m < variables.Count; m++)
							{
								if (variables[m].GetPalabra() == lista[1].GetPalabra())
								{
									nr = false;
								}
							}
							if (nr)
							variables.Add(new Variable(lista[1].GetPalabra(), lista[0].GetPalabra(), valor, lista[1].GetFila(), lista[1].GetColumna(), funcion));

						}
						else
						{
							Form1._Form1.AñadirConsola("error en fila: " + lista[pos].GetFila() + " columna " + lista[pos].GetColumna() + " palabra  " + lista[pos].GetPalabra());
						}
					}
					else
					{
						Form1._Form1.AñadirConsola("error de asignación ");
					}
					break;
				case "tstring":
					if ((lista.Count - 3) == 1)
					{
						if (Ver_tipo(lista[lista.Count - 1], "tstring"))
						{
							Form1._Form1.AñadirConsola("es correcto");
							bool nr = true;
							for (int m = 0; m < variables.Count; m++)
							{
								if (variables[m].GetPalabra() == lista[1].GetPalabra())
								{
									nr = false;
								}
							}
							if (nr)
							variables.Add(new Variable(lista[1].GetPalabra(), lista[0].GetPalabra(), lista[3].GetPalabra(), lista[1].GetFila(), lista[1].GetColumna(), funcion));

						}
						else
						{
							Form1._Form1.AñadirConsola("error en fila: " + lista[lista.Count - 1].GetFila() + " columna " + lista[lista.Count - 1].GetColumna() + " palabra  " + lista[lista.Count - 1].GetPalabra());
						}
					}
					else if ((lista.Count - 3) > 1)
					{
						bool comp = true;
						int pos = 0;
						string valor = "";
						for (int x = 3; x < lista.Count; x++)
						{
							if (!Ver_tipo(lista[x], "string") && x % 2 != 0)
							{
								comp = false;
								pos = x;
								break;
							}
							valor += lista[x].GetPalabra() + " ";
						}
						if (comp)
						{
							Form1._Form1.AñadirConsola("es correcto");
							bool nr = true;
							for (int m = 0; m < variables.Count; m++)
							{
								if (variables[m].GetPalabra() == lista[1].GetPalabra())
								{
									nr = false;
								}
							}
							if (nr)
							variables.Add(new Variable(lista[1].GetPalabra(), lista[0].GetPalabra(), valor, lista[1].GetFila(), lista[1].GetColumna(), funcion));

						}
						else
						{
							Form1._Form1.AñadirConsola("error en fila: " + lista[pos].GetFila() + " columna " + lista[pos].GetColumna() + " palabra  " + lista[pos].GetPalabra());
						}
					}
					else
					{
						Form1._Form1.AñadirConsola("error de asignación ");
					}
					break;
				case "tchar":
					if ((lista.Count - 3) == 1)
					{
						if (Ver_tipo(lista[lista.Count - 1], "tchar"))
						{
							Form1._Form1.AñadirConsola("es correcto");
							bool nr = true;
							for (int m = 0; m < variables.Count; m++)
							{
								if (variables[m].GetPalabra() == lista[1].GetPalabra())
								{
									nr = false;
								}
							}
							if (nr)
							variables.Add(new Variable(lista[1].GetPalabra(), lista[0].GetPalabra(), lista[3].GetPalabra(), lista[1].GetFila(), lista[1].GetColumna(), funcion));

						}
						else
						{
							Form1._Form1.AñadirConsola("error en fila: " + lista[lista.Count - 1].GetFila() + " columna " + lista[lista.Count - 1].GetColumna() + " palabra  " + lista[lista.Count - 1].GetPalabra());
						}
					}
					else if ((lista.Count - 3) > 1)
					{
						bool comp = true;
						int pos = 0;
						string valor = "";
						for (int x = 3; x < lista.Count; x++)
						{
							if ((!Ver_tipo(lista[x], "tchar") && x % 2 != 0))
							{
								comp = false;
								pos = x;
								break;
							}
							valor += lista[x].GetPalabra() + " ";
						}
						if (comp)
						{
							Form1._Form1.AñadirConsola("es correcto");
							bool nr = true;
							for (int m = 0; m < variables.Count; m++)
							{
								if (variables[m].GetPalabra() == lista[1].GetPalabra())
								{
									nr = false;
								}
							}
							if (nr)
							variables.Add(new Variable(lista[1].GetPalabra(), lista[0].GetPalabra(), valor, lista[1].GetFila(), lista[1].GetColumna(), funcion));

						}
						else
						{
							Form1._Form1.AñadirConsola("error en fila: " + lista[pos].GetFila() + " columna " + lista[pos].GetColumna() + " palabra  " + lista[pos].GetPalabra());
						}
					}
					else
					{
						Form1._Form1.AñadirConsola("error de asignación ");
					}
					break;
				default:
					Form1._Form1.AñadirConsola("no hay tipo");
					break;
			}
		}
		public Boolean Ver_tipo(Token a, String tipo)
		{
			bool ver = false;
			if (a.GetTipo() == "Identificador")
			{
				for (int x = 0; x < variables.Count; x++)
				{
					if (a.GetPalabra() == variables[x].GetPalabra() && tipo == variables[x].GetTipoIdentificador())
					{
						ver = true;
					}
				}
			}
			else
			{
				if (TipoYValorCorrecto(tipo, a.GetPalabra()))
				{
					ver = true;
				}
			}
			return ver;
		}
		public void Separarmetodosaprobar()
		{
			List<Token> lista2 = new List<Token>();
			lista2 = listaPalabras;
			//lista2.RemoveAt(lista2.Count - 1);
			//lista2.Reverse();
			int posinit, posfinal = 0;
			String funnombre;
			for (int x = 0; x < lista2.Count; x++)
			{


				if (lista2[x].GetPalabra() == "juice")
				{
					funnombre = lista2[x + 1].GetPalabra();
					List<Token> fun = new List<Token>();
					List<Token> listadatos = new List<Token>();
					posinit = x + 3;
					for (int y = posinit; y < lista2.Count; y++)
					{
						if (lista2[y].GetPalabra() != "->")
						{
							fun.Add(new Token(lista2[y].GetPalabra(), lista2[y].GetTipo(), lista2[y].GetColumna(), lista2[y].GetFila()));

						}
						else
						{
							posfinal = y + 3;
							break;
						}
					}

					Inicializarvariablesmetodo(fun, funnombre);
					for (int a = posfinal; a < lista2.Count; a++)
					{
						if (lista2[a].GetPalabra() != "}")
						{
							listadatos.Add(new Token(lista2[a].GetPalabra(), lista2[a].GetTipo(), lista2[a].GetColumna(), lista2[a].GetFila()));

						}
						else
						{
							posfinal = a;
							break;

						}
					}

					Separarcomprobaciones(listadatos, funnombre);
					fun.Clear();
					listadatos.Clear();
					x = posfinal;


				}
			}

			for (int y = 0; y < variables.Count; y++)
			{
				Form1._Form1.AñadirConsola(variables[y].GetPalabra() + "  " + variables[y].GetTipo() + "  " + variables[y].GetTipoIdentificador() + " " + variables[y].GetValor());
			}
		}

		public void Inicializarvariablesmetodo(List<Token> datos, String funcionnombre)
		{
			List<Token> tokens = new List<Token>();
			if (datos[0].GetPalabra() != "void")
			{
				for (int x = 0; x < datos.Count; x++)
				{
					if ((x + 1) % 3 != 0)
					{

						tokens.Add(new Token(datos[x].GetPalabra(), datos[x].GetTipo(), datos[x].GetColumna(), datos[x].GetFila()));

					}
				}
				for (int x = 0; x < tokens.Count; x = x + 2)
				{
					Form1._Form1.AñadirConsola("es correcto  variable funcion");
					bool nr = true;
					for(int m=0;m<variables.Count;m++)
					{
						if(variables[m].GetPalabra()==tokens[x+1].GetPalabra())
						{
							nr = false;
						}
					}
					if (nr)
					{
						variables.Add(new Variable(tokens[x + 1].GetPalabra(), tokens[x].GetPalabra(), "", tokens[x + 1].GetFila(), tokens[x + 1].GetColumna(), funcionnombre));
					}
				}
			}

		}

	}
}
