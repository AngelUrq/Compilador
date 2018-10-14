using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DerivationTree
{
    public partial class Form1 : Form
    {
        string value = "";
        
        char[] entry;
        char[] NonTerminals = {'A','B','C','D','E','F','G','H','I','J','K','L',
                               'M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
        string B1 = "abc";
        char[] B;

        public Form1()
        {
            InitializeComponent();
        }

        private void ViewTreeBtn_Click(object sender, EventArgs e)
        {
            value = textBox1.Text;
            entry = value.ToCharArray();
            B = B1.ToCharArray();

            Bitmap surface = new Bitmap(pictureBox1.Width,pictureBox1.Height);
            Graphics GFX = Graphics.FromImage(surface);

            Rectangle rectangulo0 = new Rectangle((surface.Width / 2) - 50, 0, 50, 50);

            GFX.DrawEllipse(Pens.Black, rectangulo0);
            GFX.FillEllipse(Brushes.LightGreen, rectangulo0);

            for (int i = 0; i < entry.Length; i++)
            {

                if (entry.Length == 1)
                {
                    Rectangle rectangulo1 = new Rectangle((surface.Width / 2) - 50, 100, 50, 50);

                    GFX.DrawLine(Pens.Black, rectangulo0.Right-rectangulo0.Width/2, rectangulo0.Bottom, rectangulo1.Right-rectangulo1.Width/2, rectangulo1.Top);
                    GFX.DrawEllipse(Pens.Black, rectangulo1);
                    GFX.FillEllipse(Brushes.Green, rectangulo1);
                    using (Font font = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Pixel))
                    {
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        GFX.DrawString(entry[i].ToString(), font, Brushes.Black, rectangulo1, stringFormat);

                    }
                }
                else
                {
                    Rectangle rectangulo2 = new Rectangle(((surface.Width - 100) * i) / (entry.Length - 1), 100, 50, 50);

                    GFX.DrawLine(Pens.Black, rectangulo0.Right - rectangulo0.Width / 2, rectangulo0.Bottom, rectangulo2.Right - rectangulo2.Width / 2, rectangulo2.Top);
                    GFX.DrawEllipse(Pens.Black, rectangulo2);
                    GFX.FillEllipse(Brushes.Green, rectangulo2);
                    using (Font font = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point))
                    {
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        GFX.DrawString(entry[i].ToString(), font, Brushes.Black, rectangulo2, stringFormat);
                    }
                }
                if (NonTerm(entry[i]))
                {
                   for (int j = 0; j < B.Length; j++)
                    {
                        if (B.Length == 1)
                        {

                            Rectangle rectangulo2 = new Rectangle(((surface.Width - 100) * i) / (entry.Length - 1), 100, 50, 50);
                            Rectangle rectangulo3 = new Rectangle(((surface.Width - 100) * i) / (entry.Length - 1), 200, 50, 50);
                            GFX.DrawLine(Pens.Black, rectangulo2.Right - rectangulo2.Width / 2, rectangulo2.Bottom, rectangulo3.Right - rectangulo3.Width / 2, rectangulo3.Top);
                            GFX.DrawEllipse(Pens.Black, rectangulo3);
                            GFX.FillEllipse(Brushes.Green, rectangulo3);
                            using (Font font = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point))
                            {
                                StringFormat stringFormat = new StringFormat();
                                stringFormat.Alignment = StringAlignment.Center;
                                stringFormat.LineAlignment = StringAlignment.Center;

                                GFX.DrawString(B[j].ToString(), font, Brushes.Black, rectangulo3, stringFormat);
                            }

                        }
                        else
                        {
                            Rectangle rectangulo2 = new Rectangle(((surface.Width - 100) * i) / (entry.Length - 1), 100, 50, 50);
                            Rectangle rectangulo4 = new Rectangle(((surface.Width - 100) * j) / (B.Length - 1), 200, 50, 50);

                            GFX.DrawLine(Pens.Black, rectangulo2.Right - rectangulo2.Width / 2, rectangulo2.Bottom, rectangulo4.Right - rectangulo4.Width / 2, rectangulo4.Top);
                            GFX.DrawEllipse(Pens.Black, rectangulo4);
                            GFX.FillEllipse(Brushes.Green, rectangulo4);
                            using (Font font = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point))
                            {
                                StringFormat stringFormat = new StringFormat();
                                stringFormat.Alignment = StringAlignment.Center;
                                stringFormat.LineAlignment = StringAlignment.Center;

                                GFX.DrawString(B[j].ToString(), font, Brushes.Black, rectangulo4, stringFormat);
                            }
                        }
                    }
                }
            }
            pictureBox1.Image = surface;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public Boolean NonTerm(char palabra)
        {
            return NonTerminals.Contains(palabra);
        }
        

    }
}
