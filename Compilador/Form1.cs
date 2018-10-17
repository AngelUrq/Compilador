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
                dGV1.Columns.Add("Fila", "Fila");
                dGV1.Columns.Add("Columna", "Columna");
                dGV1.Columns.Add("codigo", "Codigo");
                dGV1.Columns.Add("Tipo", "Tipo");
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
                    {
                        dGV1.Rows.Add(fi, col, al.palabra[i], al.tipoda[i]);
                        col++;
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
