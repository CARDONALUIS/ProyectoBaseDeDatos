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
        private int ID_ENTI;
        public int id_enti { get { return ID_ENTI; } set { ID_ENTI = value; } }

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

        public Entidad(int _id_enti, String _nomb, int _dirEnt, int _dirAtr, int _dirDat, int _dirSigEnti)
        {
            ID_ENTI = _id_enti;
            NOMBRE = _nomb;
            DIRENTI = _dirEnt;
            DIRATRI = _dirAtr;
            DIRDAT = _dirDat;
            DIRSIGENTI = _dirSigEnti;
            listAtrib = new List<Atributo>();
        }

        /*
        public void escribeArchivoEntid(FileStream archivo, string nomArch)
        {
            string FileName = nomArch;

            long entero = 16;
            int entero2 = 14;

            long otroEntero = 19;
            double doble = 2.5678910;
            bool booleano = true;
            string cadena = "Hola!!!!!!";
            int contBytChar = 0;

            //Con la palabra reservada using puedes utilizar cierto recurso determinado por las llaves, en este caso nos permite abrir el archivo simplemente con un binaryWriter con la seguridad de que estaremos cerrando el archivo una vez que las llaves se cierren.  
            //Tomar en cuenta el FileMode lo que se escriba en este archivo se escribirá al principio de este aunque ya tenga información anteriormente.


            using (BinaryWriter bw = new BinaryWriter(File.Open(FileName, FileMode.Open)))
            {
            
                bw.Write();
                for (int i = 0; i < dic.listEntidad.Count; i++)
                {
                    bw.Seek((int)dic.listEntidad.ElementAt(i).dirEnti, SeekOrigin.Begin);
                    bw.Write(dic.listEntidad.ElementAt(i).id_enti);
                    bw.Write("");
                    bw.Write(dic.listEntidad.ElementAt(i).nombre);
                    r = 0;

                    if ((dic.listEntidad.ElementAt(i).nombre.Length) % 2 == 0)
                        while (contBytChar < (35 - dic.listEntidad.ElementAt(i).nombre.Length) / 2)
                        {
                            bw.Write("-");
                            contBytChar++;
                        }
                    else
                    {
                        while (contBytChar < (34 - (dic.listEntidad.ElementAt(i).nombre.Length)) / 2)
                        {
                            bw.Write("-");
                            contBytChar++;
                        }
                        bw.Write("");
                    }
                    contBytChar = 0;
                    bw.Write(dic.listEntidad.ElementAt(i).dirEnti);
                    bw.Write(dic.listEntidad.ElementAt(i).dirAtri);
                    bw.Write(dic.listEntidad.ElementAt(i).dirDat);
                    bw.Write(dic.listEntidad.ElementAt(i).dirSigEnti);

                }
            }
        }*/



    }
    
}
