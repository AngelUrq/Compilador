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

            List<string> terminales = new List<string>();
            List<string> noTerminales = new List<string>();

            noTerminales.Add("S");
            noTerminales.Add("E");

            terminales.Add("1");

            Gramatica gramatica = new Gramatica("S", Archivo.LeerArchivo("../../Prueba.xqc"), terminales, noTerminales);

            AnalizadorSintactico analizador = new AnalizadorSintactico(gramatica, "aabb");

            for (int x = 0; x < analizador.GetMatriz().GetLength(0); x++)
            {

                for (int y = 0; y < analizador.GetMatriz().GetLength(1); y++)
                {
                    txtTexto.Text += analizador.GetMatriz()[x, y] + " ";
                }
                txtTexto.Text += '\n';
            }
        }

        
      
    }
}
