using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Console.WriteLine("Iniciando compilador...");
            Console.WriteLine("----------------------------------------");
            Gramatica gramatica = new Gramatica("S", Archivo.LeerArchivo("../../Prueba.xqc"),null,null);
            AnalizadorSintactico analizador = new AnalizadorSintactico(gramatica);
            analizador.MostrarProducciones();
            Console.WriteLine("----------------------------------------");
            analizador.Analizar();
            Console.WriteLine("----------------------------------------");
            analizador.MostrarMatriz();
        }
    }
}