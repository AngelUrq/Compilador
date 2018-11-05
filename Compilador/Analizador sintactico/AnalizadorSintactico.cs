using Compilador.Analizador_sintactico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Compilador
{
    class AnalizadorSintactico
    {
        private List<Produccion> listaProducciones;

        private List<string> terminales;
        private List<string> noTerminales;

        private List<Token> listaTokens;

        private string simboloInicial;

        private List<Conjunto> primeros;
        private List<Conjunto> siguientes;

        public string[,] tabla;

        public AnalizadorSintactico(List<Token> listaTokens)
        {
            primeros = new List<Conjunto>();
            siguientes = new List<Conjunto>();
            terminales = new List<string>();
            noTerminales = new List<string>();

            this.listaTokens = listaTokens;

            IniciarProducciones();
            CalcularPrimeros();
            CalcularSiguientes();

            tabla = new string[noTerminales.Count + 1, terminales.Count + 2];

            InicializarTabla();
            RellenarTabla();
            MostrarMatriz();
        }

        private void CalcularPrimeros()
        {
            List<Produccion> proximasProducciones = new List<Produccion>();
            List<bool> calculados = new List<bool>();

            foreach (Produccion produccion in listaProducciones)
            {
                string primerElementoLadoDerecho = produccion.LadoDerecho[0].Nombre;

                if (Pertenece(primerElementoLadoDerecho, terminales))
                {
                    if (!primerElementoLadoDerecho.Equals("€"))
                    {
                        AgregarLista(primeros, primerElementoLadoDerecho, produccion.LadoIzquierdo);
                        BuscarEpsilonPrimero(produccion.LadoIzquierdo);
                    }
                }
                else if (Pertenece(primerElementoLadoDerecho, noTerminales))
                {
                    proximasProducciones.Add(produccion);
                    calculados.Add(false);
                }
            }

            bool huboSinCalcular = true;
            while (huboSinCalcular)
            {
                huboSinCalcular = false;

                for(int j = 0; j < proximasProducciones.Count; j++)
                {
                    Produccion produccion = proximasProducciones[j];

                    string primerElementoLadoDerecho = produccion.LadoDerecho[0].Nombre;

                    for(int i = 0; i < proximasProducciones.Count; i++)
                    {
                        if (proximasProducciones[i].LadoIzquierdo.Equals(produccion.LadoDerecho[0].Nombre))
                        {
                            if (!calculados[i])
                            {
                                huboSinCalcular = true;
                                continue;
                            }
                        }
                    }

                    CalcularPrimeroNoTerminal(produccion.LadoIzquierdo, produccion.LadoDerecho, primerElementoLadoDerecho);
                    calculados[j] = true;
                }
            }

            EliminarRepetidos(primeros);

            Console.WriteLine("----------------PRIMEROS----------------");
            foreach (Conjunto primero in primeros)
            {
                Console.WriteLine(primero.ToString());
            }
            Console.WriteLine("----------------------------------------");
        }

        private void CalcularPrimeroNoTerminal(string ladoIzquierdo,Simbolo[] ladoDerecho, string noTerminal)
        {
            bool noTerminalEncontrado = false;

            Conjunto misPrimeros = new Conjunto("","");

            while (!noTerminalEncontrado)
            {
                misPrimeros = BuscarPrimeros(noTerminal);

                if (misPrimeros.LadoIzquierdo.Equals("") || misPrimeros.LadoDerecho.Equals(""))
                {
                    foreach (Produccion produccion in listaProducciones)
                    {
                        if (produccion.LadoIzquierdo.Equals(noTerminal))
                        {
                            noTerminal = produccion.LadoDerecho[0].Nombre;
                            break;
                        }
                    }
                }
                else
                {
                    noTerminalEncontrado = true;
                }
            }

            string[] primerosNoTerminal = misPrimeros.LadoDerecho.Split(',');

            for (int i = 0; i < primerosNoTerminal.Length; i++)
            {
                if (primerosNoTerminal[i].Equals("€"))
                {
                    Simbolo[] nuevoLadoDerecho = new Simbolo[ladoDerecho.Length - 1];
                    for(int j = 0; j < nuevoLadoDerecho.Length; j++)
                    {
                        nuevoLadoDerecho[j] = ladoDerecho[j+1];
                    }

                    if (nuevoLadoDerecho.Length > 0)
                    {
                        if (Pertenece(nuevoLadoDerecho[0].Nombre, terminales))
                        {
                            AgregarLista(primeros, nuevoLadoDerecho[0].Nombre, ladoIzquierdo);
                        }
                        else
                        {
                            CalcularPrimeroNoTerminal(ladoIzquierdo, nuevoLadoDerecho, nuevoLadoDerecho[0].Nombre);
                        }
                    }
                    else
                    {
                        AgregarLista(primeros, "€", ladoIzquierdo);
                    }
                }
                else
                {
                    AgregarLista(primeros, primerosNoTerminal[i], ladoIzquierdo);
                }
            }
        }

        private bool PerteneceAlArreglo(string[] lista, string elemento)
        {
            for(int i = 0; i < lista.Length; i++)
            {
                if (lista[i].Equals(elemento))
                {
                    return true;
                }
            }

            return false;
        }

        private void AgregarLista(List<Conjunto> lista, string simbolo, string ladoIzquierdo)
        {
            foreach (Conjunto produccion in lista)
            {
                if (produccion.LadoIzquierdo.Equals(ladoIzquierdo))
                {
                    string nuevoLadoDerecho = produccion.LadoDerecho;
                    if (!produccion.LadoDerecho.Equals(""))
                    {
                        nuevoLadoDerecho += ",";
                    }

                    produccion.LadoDerecho = nuevoLadoDerecho + simbolo;
                    break;
                }
            }
        }

        private void BuscarEpsilonPrimero(string ladoIzquierdo)
        {
            foreach (Produccion produccion in listaProducciones)
            {
                if (ladoIzquierdo.Equals(produccion.LadoIzquierdo) && produccion.LadoDerecho[0].Nombre.Equals("€"))
                {
                    AgregarLista(primeros, "€", ladoIzquierdo);
                    break;
                }
            }
        }

        private bool Pertenece(string elemento, List<string> lista)
        {
            foreach (string elementoLista in lista)
            {
                if (elemento.Equals(elementoLista))
                {
                    return true;
                }
            }

            return false;
        }

        private void CalcularSiguientes()
        {
            AgregarLista(siguientes, "$", simboloInicial);

            foreach(string noTerminal in noTerminales)
            {
                if (noTerminal.Equals("EXTRA"))
                {
                    int a = 3;
                }

                CalcularSiguiente(noTerminal);
            }

            ReemplazarSiguientes();
            EliminarRepetidos(siguientes);

            Console.WriteLine("----------------SIGUIENTES--------------");
            foreach (Conjunto siguiente in siguientes)
            {
                Console.WriteLine(siguiente.ToString());
            }
            Console.WriteLine("----------------------------------------");
        }

        private void EliminarRepetidos(List<Conjunto> conjunto)
        {
            foreach(Conjunto siguiente in conjunto)
            {
                string[] ladoDerecho = siguiente.LadoDerecho.Split(',');
                string nuevoLadoDerecho = "";

                for(int i = 0; i < ladoDerecho.Length; i++)
                {
                    string[] nuevosLadosDerechos = nuevoLadoDerecho.Split(',');

                    bool pertenece = false;
                    for(int j = 0; j < nuevosLadosDerechos.Length; j++)
                    {
                        if (ladoDerecho[i].Equals(nuevosLadosDerechos[j]))
                        {
                            pertenece = true;
                            break;
                        }
                    }

                    if (!pertenece)
                    {
                        nuevoLadoDerecho += ladoDerecho[i] + ",";
                    }
                }

                if (nuevoLadoDerecho.Length > 0)
                {
                    if (nuevoLadoDerecho[nuevoLadoDerecho.Length - 1].Equals(','))
                    {
                        nuevoLadoDerecho = nuevoLadoDerecho.Substring(0, nuevoLadoDerecho.Length - 1);
                    }

                    siguiente.LadoDerecho = nuevoLadoDerecho;
                }
            }
        }

        private void CalcularSiguiente(string ladoIzquierdo)
        {
            foreach (Produccion produccion in listaProducciones)
            {
                for (int i = 0; i < produccion.LadoDerecho.Length; i++)
                {
                    if (produccion.LadoDerecho[i].Nombre.Equals(ladoIzquierdo))
                    {
                        if (i + 1 < produccion.LadoDerecho.Length)
                        {
                            if (Pertenece(produccion.LadoDerecho[i+1].Nombre,terminales))
                            {
                                AgregarLista(siguientes, produccion.LadoDerecho[i + 1].Nombre, ladoIzquierdo);
                            }
                            else
                            {
                                AgregarSiguienteNoTerminal(i+1, produccion, ladoIzquierdo);
                            }
                        }
                        else
                        {
                            if (!produccion.LadoIzquierdo.Equals(ladoIzquierdo))
                            {
                                Conjunto siguiente = BuscarSiguientes(produccion.LadoIzquierdo);

                                if (siguiente.LadoDerecho.Equals("") || siguiente.LadoIzquierdo.Equals(""))
                                {
                                    AgregarLista(siguientes, "S(" + produccion.LadoIzquierdo + ")", ladoIzquierdo);
                                }
                                else
                                {
                                    string[] listaSiguientes = siguiente.LadoDerecho.Split(',');
                                    for(int j = 0; j < listaSiguientes.Length; j++)
                                    {
                                        AgregarLista(siguientes, listaSiguientes[j], ladoIzquierdo);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void AgregarSiguienteNoTerminal(int i, Produccion produccion, string ladoIzquierdo)
        {
            string[] nuevosPrimeros = BuscarPrimeros(produccion.LadoDerecho[i].Nombre).LadoDerecho.Split(',');

            if (Pertenece(produccion.LadoDerecho[i].Nombre,terminales))
            {
                AgregarLista(siguientes, produccion.LadoDerecho[i].Nombre, ladoIzquierdo);
            }
            else
            {
                for (int j = 0; j < nuevosPrimeros.Length; j++)
                {
                    if (nuevosPrimeros[j].Equals("€"))
                    {
                        if (i + 1 < produccion.LadoDerecho.Length)
                        {
                            AgregarSiguienteNoTerminal(i + 1, produccion, ladoIzquierdo);
                        }
                        else
                        {
                            Conjunto siguiente = BuscarSiguientes(produccion.LadoIzquierdo);

                            if (siguiente.LadoIzquierdo.Equals(""))
                            {
                                AgregarLista(siguientes, "S(" + produccion.LadoIzquierdo + ")", ladoIzquierdo);
                            }
                            else
                            {
                                string[] listaSiguientes = siguiente.LadoDerecho.Split(',');
                                for (int k = 0; k < listaSiguientes.Length; k++)
                                {
                                    AgregarLista(siguientes, listaSiguientes[k], ladoIzquierdo);
                                }
                            }
                        }
                    }
                    else
                    {
                        AgregarLista(siguientes, nuevosPrimeros[j], ladoIzquierdo);
                    }
                }
            }
        }

        private void ReemplazarSiguientes()
        {
            bool seguirReemplazando = false;
            do
            {
                seguirReemplazando = false;

                foreach (Conjunto siguiente in siguientes)
                {
                    string[] listaSiguientes = siguiente.LadoDerecho.Split(',');

                    for (int i = 0; i < listaSiguientes.Length; i++)
                    {
                        if (Regex.IsMatch(listaSiguientes[i], "S\\(([a-zA-Z0-9])+\\)"))
                        {
                            seguirReemplazando = true;

                            Conjunto siguientes = BuscarSiguientes(listaSiguientes[i][2].ToString());

                            siguiente.SetLadoDerecho(siguiente.LadoDerecho.Replace(listaSiguientes[i], siguientes.LadoDerecho));
                        }
                    }
                }

            } while (seguirReemplazando);
        }

        private bool ExisteEn(string[] lista, string elemento)
        {
            for (int i = 0; i < lista.Length; i++)
            {
                if (elemento.Equals(lista[i]))
                {
                    return true;
                }
            }

            return false;
        }
       
        private bool DerivaEnEpsilon(string noTerminal)
        {
            Conjunto primerosNoTerminal = BuscarPrimeros(noTerminal);

            string[] listaPrimeros = primerosNoTerminal.LadoDerecho.Split(',');

            for (int i = 0; i < listaPrimeros.Length; i++)
            {
                if (listaPrimeros[i].Equals("€"))
                {
                    return true;
                }
            }

            return false;
        }

        private Conjunto BuscarPrimeros(string noTerminal)
        {
            foreach (Conjunto produccion in primeros)
            {
                if (produccion.LadoIzquierdo.Equals(noTerminal))
                {
                    return produccion;
                }
            }

            return new Conjunto("", "");
        }

        private Conjunto BuscarSiguientes(string noTerminal)
        {
            foreach (Conjunto produccion in siguientes)
            {
                if (produccion.LadoIzquierdo.Equals(noTerminal))
                {
                    return produccion;
                }
            }

            return new Conjunto("", "");
        }

        public void InicializarTabla()
        {
            for (int i = 0; i < tabla.GetLength(0); i++)
            {
                for (int j = 0; j < tabla.GetLength(1); j++)
                {
                    tabla[i, j] = " ";
                }
            }

            int contador = 0;
            int noTerm = 0;

            for (int i = 0; i < tabla.GetLength(0); i++)
            {
                for (int j = 0; j < tabla.GetLength(1); j++)
                {
                    if (i == 0 && j >= 1)
                    {
                        if (contador == terminales.Count)
                        {
                            tabla[i, j] = "$";
                        }
                        else
                        {
                            if (contador < terminales.Count)
                            {
                                tabla[i, j] = terminales[contador].ToString();
                                contador++;
                            }
                        }
                    }
                    else if (i > 0 && j == 0)
                    {
                        tabla[i, j] = noTerminales[noTerm];
                        noTerm++;
                    }
                }
            }
        }

        public void MostrarMatriz()
        {
            Console.WriteLine("----------------TABLA-------------------");
            for (int i = 0; i < tabla.GetLength(0); i++)
            {
                for (int j = 0; j < tabla.GetLength(1); j++)
                {
                    Console.Write(tabla[i, j] + " | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("----------------------------------------");
        }

        public void RellenarTabla()
        {
            List<string> prim = new List<string>();
            List<string> noTerminalesPrimeros = new List<string>();
            List<string> noTerminalesSiguientes = new List<string>();
            List<string> listaDeLosSiguientes = new List<string>();

            int numeroColumna = 0;
            int numeroFila = 0;

            foreach (Conjunto produccionDePrimero in primeros)
            {
                string primero = produccionDePrimero.LadoDerecho;
                string noTermPrimeros = produccionDePrimero.LadoIzquierdo;
                prim.Add(primero);
                noTerminalesPrimeros.Add(noTermPrimeros);
            }

            foreach (Conjunto produccionSiguiente in siguientes)
            {
                string siguiente = produccionSiguiente.LadoIzquierdo;
                string noTermSiguientes = produccionSiguiente.LadoDerecho;
                noTerminalesSiguientes.Add(siguiente);
                listaDeLosSiguientes.Add(noTermSiguientes);
            }


            foreach (Produccion regla in listaProducciones)
            {
                if (regla.LadoDerecho[0].Nombre.Equals("€"))
                {
                    for (int k = 0; k < noTerminalesSiguientes.Count; k++)
                    {
                        if (regla.LadoIzquierdo.ToString().Equals(noTerminalesSiguientes[k]))
                        {
                            string[] elementosSiguientes = listaDeLosSiguientes[k].Split(',');

                            for (int l = 0; l < elementosSiguientes.Length; l++)
                            {
                                for (int m = 1; m < tabla.GetLength(1); m++)
                                {
                                    if (elementosSiguientes[l].Equals(tabla[0, m]))
                                    {
                                        int i = 0;

                                        for (int n = 0; n < tabla.GetLength(0); n++)
                                        {
                                            if (regla.LadoIzquierdo.ToString().Equals(tabla[n, 0]))
                                            {
                                                i = n;
                                                break;
                                            }
                                        }

                                        tabla[i, m] = "€";
                                    }
                                }
                            }
                        }
                    }
                }
                else if (Pertenece(regla.LadoDerecho[0].Nombre, noTerminales))
                {
                    for (int i = 0; i < noTerminalesPrimeros.Count; i++)
                    {
                        if (noTerminalesPrimeros[i].Equals(regla.LadoDerecho[0].Nombre))
                        {
                            string[] elementosPrimeros = prim[i].Split(',');

                            for (int j = 0; j < elementosPrimeros.Length; j++)
                            {
                                if (elementosPrimeros[j].Equals("€"))
                                {
                                    for (int k = 0; k < noTerminalesSiguientes.Count; k++)
                                    {
                                        if (regla.LadoDerecho[0].Nombre.Equals(noTerminalesSiguientes[k]))
                                        {
                                            string[] elementosSiguientes = listaDeLosSiguientes[k].Split(',');

                                            for (int l = 0; l < elementosSiguientes.Length; l++)
                                            {
                                                for (int m = 1; m < tabla.GetLength(1); m++)
                                                {
                                                    if (elementosSiguientes[l].Equals(tabla[0, m]))
                                                    {
                                                        bool reglaEpsilon = false;

                                                        for (int n = 0; n < listaProducciones.Count; n++)
                                                        {
                                                            if (listaProducciones[n].LadoIzquierdo.Equals(regla.LadoIzquierdo) && listaProducciones[n].LadoDerecho[0].Equals("€"))
                                                            {
                                                                tabla[k, m] = "€";
                                                                reglaEpsilon = true;
                                                                break;
                                                            }
                                                        }

                                                        if (!reglaEpsilon)
                                                        {
                                                            tabla[k, m] = regla.GetLadoDerecho();
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    for (int k = 0; k < tabla.GetLength(1); k++)
                                    {
                                        if (tabla[0, k].Equals(elementosPrimeros[j]))
                                        {
                                            for (int l = 0; l < tabla.GetLength(0); l++)
                                            {
                                                if (tabla[l, 0].Equals(regla.LadoIzquierdo))
                                                {
                                                    tabla[l, k] = regla.GetLadoDerecho();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < tabla.GetLength(0); i++)
                    {
                        if (i == 0)
                        {
                            for (int j = 1; j < tabla.GetLength(1); j++)
                            {
                                if (regla.LadoDerecho[0].Nombre.Equals(tabla[0, j]))
                                {
                                    numeroColumna = j;
                                }
                            }
                        }
                        else
                        {
                            if (regla.LadoIzquierdo.Equals(tabla[i, 0]))
                            {
                                numeroFila = i;
                            }
                        }
                    }

                    tabla[numeroFila, numeroColumna] = regla.GetLadoDerecho();
                }
            }
        }

        private void IniciarProducciones()
        {
            listaProducciones = new List<Produccion>();

            string[] producciones = Archivo.LeerArchivo("../../gramatica.txt").Split('\n');
            foreach (string produccion in producciones)
            {
                if (!produccion.Equals(""))
                {
                    string[] produccionDividida = produccion.Split(',');
                    string[] simbolosLadoDerecho = produccionDividida[1].Split(' ');

                    Simbolo[] simbolos = new Simbolo[simbolosLadoDerecho.Length];

                    for (int i = 0; i < simbolos.Length; i++)
                    {
                        simbolos[i] = new Simbolo(simbolosLadoDerecho[i]);
                    }

                    listaProducciones.Add(new Produccion(produccionDividida[0], simbolos));
                }
            }

            Console.WriteLine("--------------PRODUCCIONES--------------");
            foreach (Produccion produccion in listaProducciones)
            {
                Console.WriteLine(produccion.ToString());
            }
            Console.WriteLine("----------------------------------------");

            terminales.Add("start");
            terminales.Add("xid");
            terminales.Add(":");
            terminales.Add("final");
            terminales.Add("juice");
            terminales.Add("xmain");
            terminales.Add("void");
            terminales.Add("->");
            terminales.Add("{");
            terminales.Add("}");
            terminales.Add("give");
            terminales.Add("!");
            terminales.Add("coma");
            terminales.Add("");
            terminales.Add("tint");
            terminales.Add("tbool");
            terminales.Add("tstring");
            terminales.Add("tfloat");
            terminales.Add("tchar");
            terminales.Add("id");
            terminales.Add("0");

            noTerminales.Add("PROGRAM");
            noTerminales.Add("BODY");
            noTerminales.Add("RETORNO");
            noTerminales.Add("EXTRA");
            noTerminales.Add("FUNCTION");
            noTerminales.Add("EXPRESION");
            noTerminales.Add("ARGS");
            noTerminales.Add("ARGS2");
            noTerminales.Add("TYPE");
            noTerminales.Add("ID");
            noTerminales.Add("VALOR");

            simboloInicial = "PROGRAM";

            foreach (string noTerminal in noTerminales)
            {
                primeros.Add(new Conjunto(noTerminal, ""));
                siguientes.Add(new Conjunto(noTerminal, ""));
            }
        }
        
        public bool ProbarCadena()
        {
            listaTokens.Add(new Token("$", "", -1, -1));
            listaTokens.Reverse();
			for(int x=0;x<listaTokens.Count;x++)
			{
				Console.Write(" "+x+" " + listaTokens[x].GetPalabra());
			}
            //inicia con el simbolo inicial modificar el simbolo inicial a probar
            List<String> cadenareglas = new List<string>();
            cadenareglas.Add("$");
            //simbolo inicial
            cadenareglas.Add(simboloInicial);

            bool seguir_camino = true;

            while (seguir_camino)
            {
                int count1 = 0;
                int count2 = 0;
               
                if (("" + cadenareglas[cadenareglas.Count - 1]) != "$")
                {
					if (cadenareglas[cadenareglas.Count - 1] == listaTokens[listaTokens.Count - 1].GetPalabra())
					{
						cadenareglas.RemoveAt(cadenareglas.Count - 1);
						listaTokens.RemoveAt(listaTokens.Count - 1);
					}
					else
					{


						for (int x = 1; x < tabla.GetLength(0); x++)
						{
							if (("" + cadenareglas[cadenareglas.Count - 1]) == tabla[x, 0])
							{
								count1 = x;
							}
						}
						for (int x = 1; x < tabla.GetLength(1); x++)
						{
							if ("" + listaTokens[listaTokens.Count - 1].GetPalabra() == tabla[0, x])
							{
								count2 = x;
							}
						}

						Console.WriteLine(count1 + " " + count2);
						if (count2 != 0&&count1!=0)
						{
							if (tabla[count1, count2] != " ")
							{
								if (tabla[count1, count2] == "€")
								{
									cadenareglas.RemoveAt(cadenareglas.Count - 1);
								}
								else
								{
									bool ver = true;
									for (int y = 0; y < tabla.GetLength(1); y++)
									{
										if (tabla[0, y] == tabla[count1, count2])
										{
											ver = false;
										}
									}
									if (!ver)
									{
										cadenareglas.RemoveAt(cadenareglas.Count - 1);
										cadenareglas.Add(tabla[count1, count2]);
									}
									else
									{
										cadenareglas.RemoveAt(cadenareglas.Count - 1);
										char[] delimiters = new char[] { ' ' };
										string[] cadena = tabla[count1, count2].Split(delimiters,
														 StringSplitOptions.RemoveEmptyEntries);
										for (int x = cadena.Length - 1; x >= 0; x--)
										{
											cadenareglas.Add("" + cadena[x]);
										}
									}
									if (cadenareglas[cadenareglas.Count - 1] == listaTokens[listaTokens.Count - 1].GetPalabra())
									{
										cadenareglas.RemoveAt(cadenareglas.Count - 1);
										listaTokens.RemoveAt(listaTokens.Count - 1);
									}
								}
							}
							else
							{
								seguir_camino = !seguir_camino;
							}
						}
						else
						{
							seguir_camino = !seguir_camino;
						}
					}
                }
                else
                {
                    seguir_camino = !seguir_camino;
                }
            }

            if (cadenareglas[cadenareglas.Count - 1] == "$" && listaTokens[listaTokens.Count - 1].GetPalabra() == "$")
            {
                Console.WriteLine("El programa es correcto");
                return true;
            }

            else
            {
                Console.WriteLine("Error en '" + listaTokens[listaTokens.Count - 1].GetPalabra() + "' fila: " + listaTokens[listaTokens.Count - 1].GetFila() + " columna " + listaTokens[listaTokens.Count - 1].GetColumna());
                return false;
            }
        }
    }
}