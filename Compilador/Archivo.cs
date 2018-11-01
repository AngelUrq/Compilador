using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    class Archivo
    {
        public static string LeerArchivo(string direccionArchivo)
        {
            string cadena = "";

            FileStream fileStream = new FileStream(direccionArchivo, FileMode.Open, FileAccess.Read);
            using (StreamReader streamReader = new StreamReader(fileStream, Encoding.Default, true))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    cadena += line + "\n";
                }
            }

            return cadena;
        }
    }
}
