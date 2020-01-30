using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaProyecto
{
    [Serializable]
    public class Atributo
    {
        private int ID_ATRI;
        public int id_atri { get { return ID_ATRI; } set { ID_ATRI = value; } }

        private string NOMBRE;
        public string nombre { get { return NOMBRE; } set { NOMBRE = value; } }

        private char TIPO;
        public char tipo { get { return TIPO; } set { TIPO = value; } }

        private int LONGITUD;
        public int longitud { get { return LONGITUD; } set { LONGITUD = value; } }

        private long DIRATRI;
        public long dirAtri { get { return DIRATRI; } set { DIRATRI = value; } }

        private int TIPOINDI;
        public int tipoIndi { get { return TIPOINDI; } set { TIPOINDI = value; } }

        private long DIRINDI;
        public long dirIndi { get { return DIRINDI; } set { DIRINDI = value; } }

        private long DIRSIGATRI;
        public long dirSigAtri { get { return DIRSIGATRI; } set { DIRSIGATRI = value; } }

        public Atributo(int _idAtr, string _nom, char _tipoDa, int _long, int _dirAtr, int _tipIn, int _dirInd, int _dirSigAtr)
        {
            ID_ATRI = _idAtr;
            NOMBRE = _nom;
            TIPO = _tipoDa;
            LONGITUD = _long;
            DIRATRI = _dirAtr;
            TIPOINDI = _tipIn;
            DIRINDI = _dirInd;
            DIRSIGATRI = _dirSigAtr;

        }
    }
}
