using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PruebaProyecto
{
    [Serializable]
    public class Entidad
    {
        private Byte[] ID_ENTI;
        public Byte[] id_enti { get { return ID_ENTI; } set { ID_ENTI = value; } }

        private string NOMBRE;
        public string nombre { get { return NOMBRE; } set { NOMBRE = value; } }


        private long DIRENTI;
        public long dirEnti { get { return DIRENTI; } set { DIRENTI = value; } }

        private long DIRATRI;
        public long dirAtri { get { return DIRATRI; } set { DIRATRI = value; } }

        private long DIRDAT;
        public long dirDat { get { return DIRDAT; } set { DIRDAT = value; } }

        
        private long DIRSIGENTI;
        public long dirSigEnti { get { return DIRSIGENTI; } set { DIRSIGENTI = value; } }

        public List<Atributo> listAtrib;
        public int varSigAtri;
        public int varSigEnti;
        public FileStream archivoDat;
        public List<Registro> listReg;
        

        public Entidad(Byte[] _id_enti, String _nomb, int _dirEnt, int _dirAtr, int _dirDat, int _dirSigEnti)
        {
            ID_ENTI = _id_enti;
            NOMBRE = _nomb;
            DIRENTI = _dirEnt;
            DIRATRI = _dirAtr;
            DIRDAT = _dirDat;
            DIRSIGENTI = _dirSigEnti;
            listAtrib = new List<Atributo>();
            listReg = new List<Registro>();
        }
    }    
}
