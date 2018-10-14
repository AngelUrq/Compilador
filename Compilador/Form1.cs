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
                dGV1.Columns.Add("codigo", "Codigo");
                dGV1.Columns.Add("Tipo", "Tipo");
                StreamReader leer = new StreamReader(abrir.FileName);
                rTB_AL.Text = leer.ReadToEnd();
                String ass = rTB_AL.Text;
                al.Separar(ass);
                for (int i = 0; i<al.palabra.Count; i++)
                {
                    dGV1.Rows.Add(al.palabra[i],al.tipoda[i]);
                }
                leer.Close();
            }
        }

        public void dGV1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
