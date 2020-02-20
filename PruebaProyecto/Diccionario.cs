using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaProyecto
{
    [Serializable]
    public class Diccionario
    {
        public long cab = -1;
        public int tamEntidad = 67;
        public int tamAtrib = 68;
        public int vaActEnt = 8;
        public int vaActAtr = 0;
        public List<Entidad> listEntidad;
        public FileStream archivo;
        public string nomArchivo;



        public Diccionario()
        {
            listEntidad = new List<Entidad>();

        }

        public void borraDic()
        {
            foreach(Entidad a in listEntidad)
            {
                a.listAtrib.Clear();
            }
            listEntidad.Clear();
        }
        

        public void actualizaDiccionario(FileStream archivolleg)
        {
            borraDic();
            
            int r = 0;
            //Variables de entidad
            Byte[] idEnti;
            char[] nombreEnti;
            int dirEnt;
            int dirAtri;
            int dirDat;
            int dirSigEnt;
            string noEn = "";
            //Variables de atributos
            Byte[] idAtriL;
            char[] nombreAtriL;
            char[] tipoDL;
            int longiL;
            int dirAtrL;
            int tipoIndL;
            int dirIndL;
            int dirSigAtL;
            string noAt = "";
            bool bandSigEnt = true;
            bool bandSigAtri = true;



            this.archivo = File.Open(nomArchivo, FileMode.Open, FileAccess.Read);
            // archivo.Seek(8,SeekOrigin.Begin);
            BinaryReader br = new BinaryReader(this.archivo);


            archivo.Seek(0, SeekOrigin.Begin);
            int dire = br.ReadInt32();
            cab = dire;
            //dire = 8;

            r = 0;
            if (cab != -1)
                while (bandSigEnt)
                {

                    archivo.Seek(dire, SeekOrigin.Begin);
                    idEnti = br.ReadBytes(5);
                    r = 0;
                    dire += 5;
                    archivo.Seek(dire, SeekOrigin.Begin);
                    //BinaryReader br = new BinaryReader(archivo);
                    nombreEnti = br.ReadChars(30);
                    r = 0;
                    dire += 30;
                    archivo.Seek(dire, SeekOrigin.Begin);
                    dirEnt = br.ReadInt32();
                    r = 0;
                    dire += 8;
                    archivo.Seek(dire, SeekOrigin.Begin);
                    dirAtri = br.ReadInt32();
                    r = 0;
                    dire += 8;
                    archivo.Seek(dire, SeekOrigin.Begin);
                    dirDat = br.ReadInt32();
                    r = 0;
                    dire += 8;
                    archivo.Seek(dire, SeekOrigin.Begin);
                    dirSigEnt = br.ReadInt32();
                    r = 0;
                    dire = dirAtri;
                    for (int i = 0; i < nombreEnti.Length; i++)
                    {
                        if (char.IsLetter(nombreEnti.ElementAt(i)))
                            noEn += nombreEnti.ElementAt(i);
                    }
                    r = 0;
                    Entidad ent = new Entidad(idEnti, noEn, dirEnt, dirAtri, dirDat, dirSigEnt);
                    noEn = "";
                    listEntidad.Add(ent);
                    r = 0;
                    if (dirAtri != -1)
                        while (bandSigAtri)
                        {
                            r = 0;
                            archivo.Seek(dire, SeekOrigin.Begin);
                            idAtriL = br.ReadBytes(5);
                            dire += 5;
                            archivo.Seek(dire, SeekOrigin.Begin);
                            nombreAtriL = br.ReadChars(30);
                            dire += 30;
                            r = 0;
                            archivo.Seek(dire, SeekOrigin.Begin);
                            tipoDL = br.ReadChars(1);
                            dire += 1;
                            r = 0;
                            archivo.Seek(dire, SeekOrigin.Begin);
                            longiL = br.ReadInt32();
                            dire += 4;
                            r = 0;
                            archivo.Seek(dire, SeekOrigin.Begin);
                            dirAtrL = br.ReadInt32();
                            dire += 8;
                            r = 0;
                            archivo.Seek(dire, SeekOrigin.Begin);
                            tipoIndL = br.ReadInt32();
                            dire += 4;
                            r = 0;
                            archivo.Seek(dire, SeekOrigin.Begin);
                            dirIndL = br.ReadInt32();
                            dire += 8;
                            r = 0;
                            archivo.Seek(dire, SeekOrigin.Begin);
                            dirSigAtL = br.ReadInt32();
                            dire = dirSigAtL;
                            r = 0;
                            for (int i = 0; i < nombreAtriL.Length; i++)
                            {
                                if (char.IsLetter(nombreAtriL.ElementAt(i)) || nombreAtriL.ElementAt(i) == '_')
                                    noAt += nombreAtriL.ElementAt(i);
                            }
                            Atributo atr = new Atributo(idAtriL, noAt, tipoDL[0], longiL, dirAtrL, tipoIndL, dirIndL, dirSigAtL);
                            noAt = "";
                            ent.listAtrib.Add(atr);
                            r = 0;


                            if (dirSigEnt == -1 && dirSigAtL == -1)
                            {
                                bandSigEnt = false;
                                r = 0;
                            }

                            if (dirSigAtL == -1)
                            {
                                bandSigAtri = false;
                                r = 0;
                                dire = dirSigEnt;
                            }
                            
                            
                    }
                    else
                    {

                        dire = dirSigEnt;
                        if (dire == -1)
                            break;
                    }
                    bandSigAtri = true;

                }
            
            
            bandSigEnt = true;
            r = 0;
            this.archivo.Close();
        }
        
       

    }
}
