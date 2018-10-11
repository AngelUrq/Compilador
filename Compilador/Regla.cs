using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    class Regla
    {
        private String estadoActual;
        private String entrada;
        private String cimaPila;
        private String estadoNuevo;
        private String accion;

        public Regla(String estActual, String entrada, String cima, String estNuevo, String accion)
        {
            this.estadoActual = estActual;
            this.entrada = entrada;
            this.cimaPila = cima;
            this.estadoNuevo = estNuevo;
            this.accion = accion;
        }

        public String GetEstadoActual()
        {
            return estadoActual;
        }

        public void SetEstadoActual(String estadoActual)
        {
            this.estadoActual = estadoActual;
        }

        public String GetEntrada()
        {
            return entrada;
        }

        public void SetEntrada(String entrada)
        {
            this.entrada = entrada;
        }

        public String GetCimaPila()
        {
            return cimaPila;
        }

        public void SetCimaPila(String cimaPila)
        {
            this.cimaPila = cimaPila;
        }

        public String GetEstadoNuevo()
        {
            return estadoNuevo;
        }

        public void SetEstadoNuevo(String estadoNuevo)
        {
            this.estadoNuevo = estadoNuevo;
        }

        public String GetAccion()
        {
            return accion;
        }

        public void SetAccion(String accion)
        {
            this.accion = accion;
        }

        public string AString()
        {
            return estadoActual + "," + entrada + "," + cimaPila
                    + "," + estadoNuevo + "," + accion;
        }
    }
}
