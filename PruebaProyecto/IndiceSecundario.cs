using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaProyecto
{
    public class IndiceSecundario
    {
        public char tipo;
        public int longAtrSec;
        public FileStream archSec;
        public int capcidadTotBloq;
        public int longBloqSec;
        public int contIndSec;
        public int posAtrSec;

        

        public IndiceSecundario(char _tipo, int _longAtrSec, FileStream _archSec, int _capacidadTotBloq, int _longBloqSec, int _contIndSec, int tipoOpArch, int _posAtrSec)
        {
            tipo = _tipo;
            longAtrSec = _longAtrSec;
            archSec = _archSec;
            capcidadTotBloq = _capacidadTotBloq;
            longBloqSec = _longBloqSec;
            contIndSec = _contIndSec;
            posAtrSec = _posAtrSec;
            

            if (tipoOpArch == 0)
            {
                int r = 0;
                BinaryWriter bw = new BinaryWriter(archSec);

                Byte[] bloque = new Byte[2048];
                for (int i = 0; i < 2048; i++)
                {
                    bloque[i] = 0xFF;
                }
                bw.Write(bloque);
            }
            else
            {
                _archSec.Close();
                archSec = File.Open(_archSec.Name, FileMode.Open);
                archSec.Close();
            }

        }

        public void creaCajon(int dirDeCajon)
        {           
            BinaryWriter bw = new BinaryWriter(archSec);

            bw.Seek(dirDeCajon, SeekOrigin.Begin);

            Byte[] bloque = new Byte[2048];
            for (int i = 0; i < 2048; i++)
            {
                bloque[i] = 0xFF;
            }
            bw.Write(bloque);
            int r = 0;
        }

        

    }
}
