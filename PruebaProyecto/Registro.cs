using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaProyecto
{

    /*
     Clase con fines de prueba no la llegue a utilizar
     */
    [Serializable]
    public class Registro
    {
        private int DIRREG;
        public int dirReg { get { return DIRREG; } set { DIRREG = value; } }

        private int DIRSIGREG;
        public int dirSigReg { get { return DIRSIGREG; } set { DIRSIGREG = value; } }


        private int TAMREG;
        public int tamaReg { get { return TAMREG; } set { TAMREG = value; } }


        public Registro(int _dirReg, int _dirSigReg)
        {
            DIRREG = _dirReg;
            DIRSIGREG = _dirSigReg;           
        }


    }
}
