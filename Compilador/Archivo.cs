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
        public static List<Produccion> LeerArchivo(string direccionArchivo)
        {
            List<Produccion> producciones = new List<Produccion>();

            FileStream fileStream = new FileStream(direccionArchivo, FileMode.Open, FileAccess.Read);
            using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] linea = line.Split(',');
                    producciones.Add(new Produccion(linea[0], linea[1]));
                }
            }

            return producciones;
        }
    }
}
