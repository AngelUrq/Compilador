using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    class Confi
    {
        string orig, tip;
        int co, fi;
        public Confi()
        {
            orig = "";
            tip = "";
            co = 0;
            fi = 0;
        }
        public Confi(string der, string iz, int co, int fi)
        {
            this.orig = der;
            this.tip = iz;
            this.co = co;
            this.fi = fi;
        }

        public string Getorig()
        {
            return orig;
        }

        public string Gettip()
        {
            return tip;
        }

        public int Getco()
        {
            return co;
        }

        public int Getfi()
        {
            return fi;
        }
    }
}
