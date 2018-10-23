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

namespace Compilador
{
    public partial class Form1 : Form
    {
        List<Confi> lista_palabras = new List<Confi>();
        public Form1()
        {
            InitializeComponent();
        }

        private void bEjecutar_Click(object sender, EventArgs e)
        {
            AL al = new AL();
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Documento de texto|*.txt";
            abrir.Title = "Guardar RichTextBox";
            //abrir.FileName = "Prueba";
            var resultado = abrir.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                StreamReader leer = new StreamReader(abrir.FileName);
                rTB_AL.Text = leer.ReadToEnd();
                String cadena1 = rTB_AL.Text;
                al.Separar(cadena1);
                int fi = 1;
                int col = 1;
                for (int i = 0; i<al.palabra.Count; i++)
                {
                    if (al.palabra[i].Equals("#$") && al.tipoda[i].Equals("Salto"))
                    {
                        fi++;
                        col = 1;
                    }
                    else
                    if(al.tipoda[i].Equals("Error"))
                    {
                        MessageBox.Show("El identificador que se encuentra en la fila # " + fi + " y en la columna " + col + " no cumple las reglas de un identificador " + al.palabra[i].ToString());
                    }
                    else
                    {
                        dGV1.Rows.Add(fi, col, al.palabra[i], al.tipoda[i]);
                        col++;
                        lista_palabras.Add(new Confi(al.palabra[i].ToString(), al.tipoda[i].ToString(), col, fi));
                    }
                }
                leer.Close();
            }
        }

        public void dGV1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
