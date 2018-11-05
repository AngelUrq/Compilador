using Compilador.Analizador_semantico;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador
{
    public partial class Form1 : Form
    {
        List<Token> listaPalabras = new List<Token>();
        ColoresInicio coloresInicio = new ColoresInicio();
        List<string> palabras;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void btnCargarArchivo_Click(object sender, EventArgs e)
        {
            Color textoColor = Color.Black, tipoDeDatoColor = Color.Green, tokenColor = Color.Red, palabraRColor = Color.Blue;

            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Documento de texto|*.txt";
            abrir.Title = "Archivo cargado";
            //abrir.FileName = "Prueba";

            var resultado = abrir.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                StreamReader leer = new StreamReader(abrir.FileName);
                txtTexto.Text = leer.ReadToEnd();

                /*string sLine = "";
                List<string> textos = new List<string>();

                while (sLine != null)
                {
                    sLine = leer.ReadLine();
                    if (sLine != null)
                        textos.Add(sLine);
                }

                leer.Close();*/


                /*for (int i = 0; i < textos.Count; i++)
                {
                    try
                    {

                        coloresInicio.SepararParabras(textos[i].ToString());
                        palabras = coloresInicio.palabrasSeparadas;

                        Console.WriteLine(palabras[i].ToString());

                        List<string> colores = coloresInicio.AsignarValorPalabra();

                        for (int j = 0; j < colores.Count; j++)
                        {
                            if (colores[j].ToString().Equals("rojo"))
                            {
                                txtTexto.SelectionColor = tokenColor;
                                txtTexto.AppendText(palabras[j].ToString());

                            }
                            else if (colores[j].ToString().Equals("azul"))
                            {

                                txtTexto.SelectionColor = palabraRColor;
                                txtTexto.AppendText(palabras[j].ToString());

                            }
                            else if (colores[j].ToString().Equals("verde"))
                            {
                                txtTexto.SelectionColor = tipoDeDatoColor;
                                txtTexto.AppendText(palabras[j].ToString());
                            }
                            else
                            {
                                txtTexto.SelectionColor = textoColor;
                            }
                        }
                        txtTexto.AppendText("\r\n");
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine(error.Message);
                    }
                }*/
            }
        }

        private void btnNuevoProyecto_Click(object sender, EventArgs e)
        {

        }

        public int getWidth()
        {
            int w = 25;

            int line = txtTexto.Lines.Length;

            if (line <= 99)
            {
                w = 20 + (int)txtTexto.Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)txtTexto.Font.Size;
            }
            else
            {
                w = 50 + (int)txtTexto.Font.Size;
            }
            return w;
        }

        public void AddLineNumbers()
        {
            Point pt = new Point(0, 0);
            int First_Index = txtTexto.GetCharIndexFromPosition(pt);
            int First_Line = txtTexto.GetLineFromCharIndex(First_Index);
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            int Last_Index = txtTexto.GetCharIndexFromPosition(pt);
            int Last_Line = txtTexto.GetLineFromCharIndex(Last_Index);
            LineNumberTextBox.SelectionAlignment = HorizontalAlignment.Center;
            LineNumberTextBox.Text = "";
            LineNumberTextBox.Width = getWidth();
            for (int i = First_Line; i <= Last_Line + 1; i++)
            {
                LineNumberTextBox.Text += i + 1 + "\n";
            }
        }
        public void AddLineNumbersLexico()
        {
            Point pt = new Point(0, 0);
            int First_Index = txtBoxLexico.GetCharIndexFromPosition(pt);
            int First_Line = txtBoxLexico.GetLineFromCharIndex(First_Index);
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            int Last_Index = txtBoxLexico.GetCharIndexFromPosition(pt);
            int Last_Line = txtBoxLexico.GetLineFromCharIndex(Last_Index);
            LineNumberLexTextBox.SelectionAlignment = HorizontalAlignment.Center;
            LineNumberLexTextBox.Text = "";
            LineNumberLexTextBox.Width = getWidth();
            for (int i = First_Line; i <= Last_Line + 1; i++)
            {
                LineNumberLexTextBox.Text += i + 1 + "\n";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LineNumberTextBox.Font = txtTexto.Font;
            txtTexto.Select();
            AddLineNumbers();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            AddLineNumbers();

        }

        private void txtTexto_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = txtTexto.GetPositionFromCharIndex(txtTexto.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers();
            }
        }

        private void txtTexto_VScroll(object sender, EventArgs e)
        {
            LineNumberTextBox.Text = "";
            AddLineNumbers();
            LineNumberTextBox.Invalidate();
        }

        private void txtTexto_TextChanged(object sender, EventArgs e)
        {
            if (txtTexto.Text == "")
            {
                AddLineNumbers();
            }
        }

        private void txtTexto_FontChanged(object sender, EventArgs e)
        {
            LineNumberTextBox.Font = txtTexto.Font;
            txtTexto.Select();
            AddLineNumbers();
        }

        private void LineNumberTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            txtTexto.Select();
            LineNumberTextBox.DeselectAll();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtBoxLexico_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = txtBoxLexico.GetPositionFromCharIndex(txtBoxLexico.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbersLexico();
            }
        }

        private void txtBoxLexico_VScroll(object sender, EventArgs e)
        {
            LineNumberLexTextBox.Text = "";
            AddLineNumbersLexico();
            LineNumberLexTextBox.Invalidate();
        }

        private void txtBoxLexico_TextChanged(object sender, EventArgs e)
        {
            if (txtBoxLexico.Text == "")
            {
                AddLineNumbersLexico();
            }
        }

        private void txtBoxLexico_FontChanged(object sender, EventArgs e)
        {
            LineNumberLexTextBox.Font = txtTexto.Font;
            txtBoxLexico.Select();
            AddLineNumbersLexico();
        }

        private void LineNumberLexTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            txtBoxLexico.Select();
            LineNumberLexTextBox.DeselectAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnCompilar_Click(object sender, EventArgs e)
        {
            txtBoxLexico.Text = txtTexto.Text;

            IniciarAnalizadorLexico();
            IniciarAnalizadorSintactico();
            IniciarAnalizadorSemantico();

        }

        private void IniciarAnalizadorLexico()
        {
            AnalizadorLexico al = new AnalizadorLexico();
            String cadena1 = txtTexto.Text;
            al.Separar(cadena1);
            int fi = 1;
            int col = 1;
            string fil = "";
            for (int i = 0; i < al.palabra.Count; i++)
            {
                if (al.palabra[i].Equals("#$") && al.tipoda[i].Equals("Salto"))
                {
                    fi++;
                    col = 1;
                }
                else
                if (al.tipoda[i].Equals("Error"))
                {
                    Console.WriteLine("El identificador que se encuentra en la fila # " + fi + " y en la columna " + col + " no cumple las reglas de un identificador " + al.palabra[i].ToString());
                    dGV1.Rows.Add(fi, col, al.palabra[i].ToString(), al.tipoda[i].ToString() + " de identificador");
                    col = col + Convert.ToInt32(al.tamanio[i].ToString());
                }
                else
                {
                    dGV1.Rows.Add(fi, col, al.palabra[i], al.tipoda[i]);
                    col = col + Convert.ToInt32(al.tamanio[i].ToString());
                    listaPalabras.Add(new Token(al.palabra[i].ToString(), al.tipoda[i].ToString(), col, fi));
                }
            }
            for (int i = 1; i < fi; i++)
            {
                fil = fil + i + "\n";
            }
            LineNumberLexTextBox.Text = fil;
        }

        private void IniciarAnalizadorSintactico()
        {
            AnalizadorSintactico analizadorSintactico = new AnalizadorSintactico(new List<Token>(listaPalabras));
            analizadorSintactico.ProbarCadena();
        }

        private void IniciarAnalizadorSemantico()
        {
            AnalizadorSemantico analizadorSemantico = new AnalizadorSemantico(listaPalabras);
            analizadorSemantico.Analizar();
        }
    }
}