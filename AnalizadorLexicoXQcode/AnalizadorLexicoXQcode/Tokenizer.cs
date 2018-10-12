using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexicoXQcode
{
    class Tokenizer
    {

        public void leerArchivo()
        {
            Form1 frm = new Form1();

            int counter = 0;
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\Rodrigo\Desktop\test.txt");
            while ((line = file.ReadLine()) != null)
            {
                frm.lstBoxMostrarArchivo.Items.Add(line);
                counter++;
            }

            
        }
           

    }
}
