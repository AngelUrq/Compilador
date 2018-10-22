using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    class AnalizadorSintactico
    {
        private String entrada;
        private String[,] matriz;

        private Gramatica gramatica;

        private List<string> miLista;

        private List<string> entradas;
        private List<string> informacionEntradas;

        public AnalizadorSintactico(Gramatica gramatica)
        {
            this.gramatica = gramatica;
            this.miLista = new List<string>();
            entradas = new List<string>();
            informacionEntradas = new List<string>();

            entradas.Add("5 + 3");
            entradas.Add("5 + + 3");
            entradas.Add("8 / 8");
            entradas.Add("8 / * 8");
            entradas.Add("20000 / 1");

            informacionEntradas.Add("1,2");
            informacionEntradas.Add("2,5");
            informacionEntradas.Add("3,2");
            informacionEntradas.Add("5,6");
            informacionEntradas.Add("4,3");

        }

        public void Analizar()
        {
            for (int i = 0; i < entradas.Count; i++)
            {
                entrada = entradas[i];
                Console.WriteLine("Cadena: " + entradas[i]);

                System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();

                ConstruccionMatriz();
                RellenaMatriz();

                try
                {
                    if (matriz[0, 0].Contains(gramatica.GetSimboloInicial()))
                    {
                        Console.WriteLine("Cadena aceptada");
                    }
                    else
                    {
                        Console.WriteLine("Cadena no aceptada");
                        BusquedaError(entradas[i], entradas, informacionEntradas);
                    }
                }
                catch
                {
                    Console.WriteLine("Ocurrió una excepción");
                }


                watch.Stop();

                Console.WriteLine("Se generó en " + watch.ElapsedMilliseconds + " ms.");
            }
        }

        private string[,] RellenaMatriz()
        {
            int j = 0;
            string resultadoProduccion = "";

            List<string> listaComprobacion = new List<string>();

            for (int x = matriz.GetLength(0) - 2; x >= 0; x--)
            {
                for (int y = 0; y < matriz.GetLength(1) - j; y++)
                {
                    string resultado = "";
                    listaComprobacion = ObtenerPosibilidades(x, y);

                    for (int i = 0; i < listaComprobacion.Count; i++)
                    {
                        resultadoProduccion = BuscarProduccion(listaComprobacion[i]);

                        string[] lista = resultadoProduccion.Split(',');
                        for(int k = 0; k < lista.Length; k++)
                        {
                            if (lista[k].Equals("S") && x == 0)
                            {
                                int xed = 3;
                            }
                        }


                        if (resultado.Length > 0 && !resultado[resultado.Length - 1].Equals(','))
                        {
                            resultado += ",";
                        }

                        resultado += resultadoProduccion;
                    }

                    if (!resultado.Equals(""))
                    {
                        if (resultado[resultado.Length - 1].Equals(','))
                        {
                            resultado = resultado.Substring(0, resultado.Length - 1);
                        }

                        matriz[x, y] = BuscarRepetido(resultado);
                    }
                    else
                    {
                        matriz[x, y] = "-";
                    }
                }
                j++;
            }
            return matriz;
        }

        private string[,] ConstruccionMatriz()
        {
			String[] en = entrada.Split(' ');
            matriz = new string[en.Length + 1, en.Length];

            for (int x = 0; x < matriz.GetLength(0); x++)
            {
                for (int y = 0; y < matriz.GetLength(1); y++)
                {
                    matriz[x, y] = " ";
                }

            }
            int indice = matriz.GetLength(1);
            for (int x = matriz.GetLength(0) - 1; x >= 0; x--)
            {
                for (int y = 0 + indice; y < matriz.GetLength(1); y++)
                {
                    matriz[x, y] = " ";
                }
                indice -= 1;
            }

            for (int i = 0; i < en.Length; i++)
            {
                matriz[matriz.GetLength(0) - 1, i] = en[i].ToString();
                matriz[matriz.GetLength(0) - 2, i] = BuscarProduccion(en[i].ToString());
            }

            return matriz;
        }

        private string BuscarProduccion(string cadena)
        {
            string camino = "";

            List<Produccion> producciones = gramatica.GetProducciones();

            for (int i = 0; i < producciones.Count; i++)
            {
                if (producciones[i].GetLadoDerecho().Contains(cadena))
                {
                    camino += producciones[i].GetLadoIzquierdo() + ",";
                }
            }

            if (camino.Length - 1 >= 0 && camino[camino.Length - 1].Equals(','))
            {
                camino = camino.Substring(0, camino.Length - 1);
            }

            return camino;

        }

        private List<string> ProductoCartesiano(string cadena1, string cadena2)
        {
            List<string> resultados = new List<string>();
            string resultado = "";
            string[] cad1 = cadena1.Split(',');
            string[] cad2 = cadena2.Split(',');

            for (int i = 0; i < cad1.Length; i++)
            {
                for (int j = 0; j < cad2.Length; j++)
                {
                    resultado = cad1[i].ToString() + cad2[j].ToString();
                    resultados.Add(resultado);
                }
            }

            return resultados;
        }

        private List<String> MostrarCasilla(int x, int y)
        {
            List<String> lista = new List<String>();
            String[,] datos = new String[2, 2];

            try
            {
                if (x < matriz.GetLength(0) - 1)
                {
                    if (x <= (matriz.GetLength(0) - 1))
                    {
                        lista = MostrarDatos(x, y);
                    }
                    else
                    {
                        //   Console.WriteLine("Lineas no permitidas ");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return lista;

        }

        private List<String> MostrarDatos(int x1, int y)
        {
            List<String> lista = new List<String>();
            List<String> posibilidades = new List<String>();

            int longitud = matriz.GetLength(0) - x1 - 1;

            if (longitud >= 3)
            {
                posibilidades = ObtenerCombinacionCadenas(longitud, y);

                int x = 0;
                while (x < posibilidades.Count)
                {
                    int subir;
                    int pos = 0;
                    for (int j = 0; j < 2; j++)
                    {
                        subir = posibilidades[x].Length;
                        lista.Add(matriz[matriz.GetLength(0) - subir - 1, pos + y]);
                        pos = subir;
                        x++;
                    }

                }
            }
            else
            {
                for (int j = y; j < (longitud + y); j++)
                {
                    if (j < matriz.GetLength(1))
                    {
                        lista.Add(matriz[x1 + 1, j]);
                    }
                }
            }

            return lista;
        }

        private List<String> ObtenerCombinacionCadenas(int longitud, int y)
        {
            int posicion = 1;
            String cadena = "";
            List<String> lista = new List<String>();

            for (int x = 0; x < matriz.GetLength(1); x++)
            {
                cadena += matriz[matriz.GetLength(0) - 1, x];
            }

            cadena = cadena.Substring(y, longitud);
            bool continuar = true;

            while (continuar)
            {

                if (posicion < cadena.Length)
                {
                    String aux = cadena.Substring(0, posicion);
                    String aux1 = cadena.Substring(posicion);
                    if (aux.Equals("") || aux1.Equals(""))
                    {
                        continuar = !continuar;
                    }
                    else
                    {
                        lista.Add(aux);
                        lista.Add(aux1);
                        posicion++;
                    }
                }
                else

                {
                    continuar = !continuar;
                }
            }
            return lista;
        }

        private List<string> ObtenerPosibilidades(int x, int y)
        {
            List<string> listaOpciones = MostrarCasilla(x, y);
            List<string> listaResultados = new List<string>();
            List<string> listaAux = new List<string>();

            for (int i = 0; i < listaOpciones.Count; i += 2)
            {
                if (x <= matriz.GetLength(0) - 3)
                {
                    listaAux = ProductoCartesiano(listaOpciones[i], listaOpciones[i + 1]);
                    for (int j = 0; j < listaAux.Count; j++)
                    {
                        listaResultados.Add(listaAux[j]);
                    }
                }
                else if (x == matriz.GetLength(0) - 2)
                {
                    listaResultados = listaOpciones;
                }
                else
                {
                    Console.WriteLine("Error");
                }
            }

            return listaResultados;
        }

        public string[,] GetMatriz()
        {
            return matriz;
        }

        private string BuscarRepetido(string palabra)
        {

            string[] palabras = palabra.Split(',');
            string palabraFinal = "";

            for (int i = 0; i < palabras.Length; i++)
            {
                if (!palabraFinal.Contains(palabras[i]))
                {
                    palabraFinal += palabras[i] + ",";
                }
            }

            if (palabraFinal[palabraFinal.Length - 1].Equals(','))
            {
                palabraFinal = palabraFinal.Substring(0, palabraFinal.Length - 1);
            }

            return palabraFinal;

        }

        public void MostrarMatriz()
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    Console.Write(matriz[i, j] + "||");
                }
                Console.WriteLine();
            }
        }

        public void MostrarProducciones()
        {
            for(int i = 0; i < gramatica.GetProducciones().Count; i++)
            {
                Console.WriteLine(gramatica.GetProducciones()[i].GetLadoIzquierdo() + " -> " + gramatica.GetProducciones()[i].GetLadoDerecho());
            }
        }

        public void BusquedaError(string cadena, List<string> cadenas, List<String> informacionCadena)
        {
            List<string> filas = new List<string>();
            List<string> columnas = new List<string>();
            string posicionError = "";
            string cadenaErronea = "";
            bool cadenaEncontrada = false;
            int indice = 0;

            for (int i = 0; i < informacionCadena.Count; i++)
            {
                try
                {
                    string fila = informacionCadena[i].ToString().Substring(0, 1);
                    string columna = informacionCadena[i].ToString().Substring(2, 1);

                    filas.Add(fila);
                    columnas.Add(columna);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
            }

            for (int j = 0; j < cadenas.Count; j++)
            {
                if (cadena.Equals(cadenas[j].ToString()))
                {
                    cadenaEncontrada = true;
                    cadenaErronea = cadenas[j];
                    indice = j;
                }
            }

            if (cadenaEncontrada)
            {
                for (int k = 0; k < filas.Count; k++)
                {
                    for (int l = 0; l < columnas.Count; l++)
                    {
                        if (k == indice && l == indice)
                        {
                            posicionError = "fila: " + filas[k] + ", cerca a la columna: " + columnas[l];
                        }
                    }
                }
            }

            Console.WriteLine("Error encontrado en la " + posicionError + ", en la palabra: " + cadenaErronea);
        }

    }
}
