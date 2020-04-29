using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 *
 *  +++leer archivo
 *  
*   archArb.Close();
    archArb = File.Open(archArb.Name, FileMode.Open);
    BinaryReader br = new BinaryReader(archArb);
    .
    .
    .
    archArb.Close();

    +++Escribir archivo


    using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
    {
    .
    .
    .
    }


 * */

namespace PruebaProyecto
{
    class HashEstatico
    {
        public FileStream archivHash;
        public int archDirDatHash;
        public int valorModu = 7;
        public CajonHash[] DirectorioHash;
        public Entidad entAct;
        public Diccionario dic;
        int r = 0;



        public void insertaEnCajon(int cajon, int clave,long dirReg)
        {
            /*foreach(CajonHash a in DirectorioHash)
            {
                if(a.numCajon == cajon)
                {
                    a.apunReg.Add(dirReg);
                    a.listClv.Add(clave);
                    
                   
                }
            }*/


        }

        public void setEntYDic(Entidad _ent, Diccionario _dic)
        {
            entAct = _ent;
            dic = _dic;
        }


        public void ObtengRegistro()
        {

            foreach (Atributo a in entAct.listAtrib)
            {
                if (a.dirIndi == -1 && a.tipoIndi == 5)
                {
                    r = 0;
                    using (BinaryWriter bw = new BinaryWriter(File.Open(dic.nomArchivo, FileMode.Open)))
                    {
                        bw.Seek((int)a.dirAtri + 52, SeekOrigin.Begin);
                        a.dirIndi = 0;
                        bw.Write(0);

                    }
                }
                else
                    r = 0;
            }
            r = 0;


            //creaNodoArb();

            r = 0;
            entAct.archivoDat.Close();
            entAct.archivoDat = File.Open(entAct.archivoDat.Name, FileMode.Open);
            BinaryReader br = new BinaryReader(entAct.archivoDat);
            long dirUltReg;
            int clavAct;

            r = 0;
            dirUltReg = entAct.archivoDat.Length - entAct.longAtributos;
            entAct.archivoDat.Seek(dirUltReg + archDirDatHash, SeekOrigin.Begin);

            clavAct = br.ReadInt32();

            r = 0;
            entAct.archivoDat.Close();

            archivHash.Close();

            int cajon;

            cajon = obtenCajonModulo(clavAct);

            r = 0;
            insertaEnCajon(cajon, clavAct, dirUltReg);

        }

        public int obtenCajonModulo(int clave)
        {
            double modulo = Math.IEEERemainder(clave, 7);
            
            return (int)modulo;
        }

        public void creaDirectorioHash()
        {
            BinaryWriter bw = new BinaryWriter(archivHash);

            Byte[] bloque = new Byte[valorModu * 8];
            for (int i = 0; i < valorModu * 8; i++)
            {
                bloque[i] = 0xFF;
            }
            bw.Write(bloque);

            DirectorioHash = new CajonHash[valorModu]; 

        }

        public void actualizaDirectorio()
        {
            //Leer el direcorio hash
            r = 0;
            archivHash.Close();
            archivHash = File.Open(archivHash.Name, FileMode.Open);
            BinaryReader br = new BinaryReader(archivHash);

            int contCajon = 0;

            while(contCajon < valorModu)
            {
                long valorModu = br.ReadInt64();
                if (valorModu != -1)
                {
                    DirectorioHash[contCajon].dirCajon = (int)valorModu ;
                }    
                contCajon ++;
            }

            archivHash.Close();
        }

        public void actualizaListaHash()
        {

        }

        public HashEstatico(FileStream _arcHash, int _archDirHash, bool tieneDatos)
        {
            archivHash = _arcHash;
            archDirDatHash = _archDirHash;


            if(tieneDatos)
            {
                _arcHash.Close();
                archivHash = File.Open(_arcHash.Name, FileMode.Open);
                actualizaDirectorio();
                actualizaListaHash();
                archivHash.Close();
            }
            else
            {
                r = 0;
                creaDirectorioHash();
            }
        }
    }
}
