using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaProyecto
{
    class ArbolB_Primario
    {
        int r = 0;
        public FileStream archArb;
        public int dirArchDatArb;
        public Entidad entAct;
        public Diccionario dic;
        public List<Nodo> lisNodo  = new List<Nodo>();
        public Nodo hojaActual_L;
        public int n = 5;//Numero de grado del arbol
         
        
        public void creaEspNodo()
        {

            r = 0;
            long finArchivo = archArb.Length;
            archArb.Close();
            using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
            {


                bw.Seek((int)finArchivo, SeekOrigin.Begin);

                Byte[] bloque = new Byte[65];
                for (int i = 0; i < 65; i++)
                {
                    bloque[i] = 0xFF;
                }
                //bw.Seek((int)finArchivo, SeekOrigin.Begin);
                r = 0;
                bw.Write(bloque);

            }
        }
        

        public void actualizaListaNodo()
        {
            BinaryReader br = new BinaryReader(archArb);
            long dicNod;
            char[] Tipo;
            long dirPun;
            int clv;
            bool bandFinClv = true;


            r = 0;
            int posAct = 0;
            int posIni;

            archArb.Seek(posAct, SeekOrigin.Begin);
            

            while (true)
            {
                posIni = posAct;
                r = 0;
                Nodo no = new Nodo();

                dicNod = br.ReadInt64();
                Tipo = br.ReadChars(1);

                no.dirNodo = dicNod;
                no.tipo = Tipo.ElementAt(0);
                posAct += 9;
                r = 0;

                while (br.ReadInt64() != -1)
                {
                    archArb.Seek(posAct, SeekOrigin.Begin);

                    dirPun = br.ReadInt64();
                    clv = br.ReadInt32();

                    r = 0;


                    no.K.Add(clv);
                    no.P.Add(dirPun);

                    posAct += 12;

                    r = 0;

                    if (br.ReadInt64() == -1)//No hay mas claves
                        bandFinClv = false;
                    r = 0;
                    archArb.Seek(posAct, SeekOrigin.Begin);

                }
                r = 0;
               
                archArb.Seek(posIni+57, SeekOrigin.Begin);
                if (!bandFinClv && br.ReadInt64() == -1)//llego al final y ya no tiene mas campos que agregar
                {
                    no.dirSigNod = -1;
                    lisNodo.Add(no);
                    break;

                }
                else
                {
                    no.dirSigNod = -1;
                    lisNodo.Add(no);
                    posIni += 65;
                }
                    

                posAct = posIni;

                
            }
            r = 0;

        }
        
        public ArbolB_Primario(FileStream _archArb, bool recInf, int _dirArchDatArb)//El archivo, y si lo abrieron con informacion ya almacenada
        {
            archArb = _archArb;
            dirArchDatArb = _dirArchDatArb;

            if (recInf)
            {
                _archArb.Close();
                archArb = File.Open(_archArb.Name, FileMode.Open);
                actualizaListaNodo();
                archArb.Close();
                
               
            }
            else
            {
                
            }
        }

        public void setEntYDic(Entidad _ent, Diccionario _dic)
        {
            entAct = _ent;
            dic = _dic;
            
            r = 0;

        }


        public void ObtengRegistro()
        {
            
            foreach (Atributo a in entAct.listAtrib)
            {
                if (a.dirIndi == -1 && a.tipoIndi == 4)
                {
                    r = 0;
                    using (BinaryWriter bw = new BinaryWriter(File.Open(dic.nomArchivo, FileMode.Open)))
                    {
                        bw.Seek((int)a.dirAtri + 52, SeekOrigin.Begin);
                        a.dirIndi = 0;
                        bw.Write(0);

                    }
                    creaEspNodo();

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
            entAct.archivoDat.Seek(dirUltReg + dirArchDatArb,SeekOrigin.Begin);

            clavAct = br.ReadInt32();

            r = 0;
            entAct.archivoDat.Close();

            
            archArb.Close();
            inserta(clavAct, dirUltReg);

        }

        public void inserta(int K, long P)
        {
            r = 0;
          if(lisNodo.Count == 0)
            {
                r = 0;
                Nodo hoja = new Nodo();
                hoja.tipo = 'R';
                hoja.dirSigNod = -1;
                hoja.dirNodo = P;
                lisNodo.Add(hoja);
                hojaActual_L = hoja;

                
                r = 0;
            }
          else//Encontrar el nodo hoja en  que debera contener el valor de llave K
            {
                r = 0;
            }

            r = 0;
            if(hojaActual_L.K.Count() < n-1 )
            {
                r = 0;
               inset_in_leaf(hojaActual_L, K, P);
            }
            r = 0;
            
        }

        public void inset_in_leaf(Nodo L, int K, long P)
        {
            r = 0;
            if(L.K.Count == 0)
            {
                r = 0;
                L.P.Insert(0, P);
                L.K.Insert(0, K);
                r = 0;
                //archArb.Close();
                using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
                {
                    bw.Seek((int)L.dirNodo, SeekOrigin.Begin);
                    bw.Write(L.dirNodo);
                    bw.Write(L.tipo);
                    bw.Write(P);
                    bw.Write(K);
                }


                r = 0;
            }
            else
            if(K < L.K.ElementAt(0))
            {
                r = 0;
                L.P.Insert(0, P);
                L.K.Insert(0, K);
                r = 0;               
            }
            else
            {
                r = 0;
                List<int> mayores = new List<int>();
                mayores = L.K.FindAll(x => K > L.K.ElementAt(x));
                r = 0;
                //int ind = L.K.FindIndex(x => x == lisAux.ElementAt(i).nombre);

                //L.P.FindIndex(x => No)
            }


        }

    }
}
