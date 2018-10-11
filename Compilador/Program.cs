using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        /// 

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            List<string> terminales = new List<string>();
            List<string> noTerminales = new List<string>();

            noTerminales.Add("S");
            noTerminales.Add("E");

            terminales.Add("1");

            Gramatica gramatica = new Gramatica("S", Archivo.LeerArchivo("../../Prueba.xqc"), terminales, noTerminales);


            AnalizadorSintactico analizadorSintactico = new AnalizadorSintactico(gramatica, "1+1");
        }


    }
}
