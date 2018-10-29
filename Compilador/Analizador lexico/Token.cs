using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    class Token
    {
        string orig, tip;
        int co, fi;
        public Token()
        {
            orig = "";
            tip = "";
            co = 0;
            fi = 0;
        }
        public Token(string der, string iz, int co, int fi)
        {
            this.orig = der;
            this.tip = iz;
            this.co = co;
            this.fi = fi;
        }

        public string GetPalabra()
        {
            return orig;
        }

        public string GetTipo()
        {
            return tip;
        }

        public int GetColumna()
        {
            return co;
        }

        public int GetFila()
        {
            return fi;
        }
    }
}
