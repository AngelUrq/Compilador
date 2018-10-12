using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace AnalizadorLexicoXQcode
{
    public partial class Form1 : Form
    {

        OpenFileDialog openfile = new OpenFileDialog();
        string line = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEscojerArchivo_Click(object sender, EventArgs e)
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\Rodrigo\Desktop\xqtest.txt");
            while ((line = file.ReadLine()) != null)
            {
                lstBoxMostrarArchivo.Items.Add(line);
            }

            string[] cadena = File.ReadLines(@"C:\Users\Rodrigo\Desktop\xqtest.txt").ToArray();
            char[] cadenachar = string.Join(string.Empty, cadena).ToCharArray();

            ArrayList arrList = new ArrayList();
            //Este ciclo divide las cadenas por espacios y otros operadors/delimitadores
            for (int i = 0; i < cadena.Length; i++)
            {
                arrList.AddRange(cadena[i].Split(new string[] { " ", "(", ")", ",",";","+","-","<",">","=","{", "}" }, StringSplitOptions.RemoveEmptyEntries));


            }

            string[] array = arrList.ToArray(typeof(string)) as string[];

            //este metodo recorre las palabras
            foreach (string value in array)
            {

                //Para los identificadores que empiezan con x
                if(value.StartsWith("x"))
                {
                    listBox1.Items.Add("IDENTIFICADOR: "+value);
                }
                //Para las palabras reservadas
                if (value.Equals("Xint")|| value.Equals("Xreal") || value.Equals("Xtring") || value.Equals("Xbool") || value.Equals("Juice") || value.Equals("agane"))
                {
                    listBox1.Items.Add("RESERVADO: "+value);
                }
               

                if (value.Equals("Juice"))
                {
                    listBox1.Items.Add("RESERVADO: " + value);
                }
                if (value.StartsWith("0")|| value.StartsWith("1")|| value.StartsWith("2")|| value.StartsWith("3")|| value.StartsWith("4")|| value.StartsWith("5")|| value.StartsWith("6")
                   || value.StartsWith("7")|| value.StartsWith("8")|| value.StartsWith("9"))
                {
                    listBox1.Items.Add("CONSTANTE: " + value);
                }

                if (value.StartsWith("  "))
                {
                    listBox1.Items.Add("CADENA: " + value);
                }
            }
            //Este metodo recorre por caracteres
            foreach (char a in cadenachar)
            {
                //Para los Delimitadores
                if(a.Equals('(')|| a.Equals(')')|| a.Equals('{')|| a.Equals('}'))
                {
                    listBox1.Items.Add("DELIMITADOR: " + a);
                }

                //Para los operadores
                if (a.Equals('=')|| a.Equals('+') || a.Equals('-') || a.Equals('/')|| a.Equals('*') || a.Equals('<') || a.Equals('>') )
                {
                    listBox1.Items.Add("OPERADOR: " + a);
                }
            }

            //Este ciclo verifica si hay o no hay un error en la palabra
            foreach(string str in array)
            {

                if (str.StartsWith("X") || str.Equals("Juice"))
                {

                }
                else if (str.StartsWith("x"))
                {

                }
                else if(str.StartsWith("0") || str.StartsWith("1") || str.StartsWith("2") || str.StartsWith("3") || str.StartsWith("4") || str.StartsWith("5") || str.StartsWith("6")
                   || str.StartsWith("7") || str.StartsWith("8") || str.StartsWith("9"))
                {

                }
                else
                {
                    //Si Existe un error este metodo busca la palabra y verifica en que linea esta del texto
                    foreach (var match in File.ReadLines(@"C:\Users\Rodrigo\Desktop\xqtest.txt")
                             .Select((text, index) => new { text, lineNumber = index + 1 })
                             .Where(x => x.text.StartsWith(str)))
                    {
                        Console.WriteLine("Hay en error en la palabra: " + str + " Linea: "+ "{0}", match.lineNumber);
                    }

                }
                    

                
            }


        }

        private void lstBoxMostrarArchivo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
