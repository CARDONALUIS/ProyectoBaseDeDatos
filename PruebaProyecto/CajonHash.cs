using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaProyecto
{

    /*
     Clase que se encarga de solo establecer ciertos atributos que se utlizan en otras clases
         */

    public class CajonHash
    {
        public int dirCajon { get; set; }
        //public int numCajon { get; set; }
        public List<campoCajonHash> listaCampoCajonHash = new List<campoCajonHash>();


    }
}
