using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaProyecto
{
    /*
     * Clase para crear objetos de la clase nodo los cuales permiten una mejor manipulacion del arbol
     */
    public class Nodo
    {
        public List<int> K = new List<int>();
        public List<long> P = new List<long>();
        public char tipo { get; set; }
        //public long dirSigNod { get; set; }
        public long dirNodo { get; set; }

        public Nodo()
        {

        }

    }
}
