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
        public bool bandT = false;
         
        
        public void creaEspNodo(char tipo)
        {

            r = 0;
            archArb = File.Open(archArb.Name, FileMode.Open);
            long finArchivo = archArb.Length;
            archArb.Close();
            using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
            {


                bw.Seek((int)finArchivo, SeekOrigin.Begin);

                Byte[] bloque = new Byte[57];
                for (int i = 0; i < 57; i++)
                {
                    bloque[i] = 0xFF;
                }
                //bw.Seek((int)finArchivo, SeekOrigin.Begin);
                r = 0;
                bw.Write(bloque);
                if(tipo == 'H')
                {
                    bw.Write((long)-1);
                }
                else
                {
                    Byte[] bloque2 = new Byte[8];
                    for (int i = 0; i < 8; i++)
                    {
                        bloque2[i] = 0xFF;
                    }
                    bw.Write(bloque2);
                }


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
               

                if(no.P.Count == n-1)
                {
                    r = 0;
                    archArb.Seek(posIni + 57, SeekOrigin.Begin);
                    no.P.Add(br.ReadInt64());
                    r = 0;
                }


                archArb.Seek(posIni+57, SeekOrigin.Begin);
                if (!bandFinClv && br.ReadInt64() == -1)//llego al final y ya no tiene mas campos que agregar
                {
                    //no.dirSigNod = -1;
                    lisNodo.Add(no);
                    break;

                }
                else
                {
                    //no.dirSigNod = -1;
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
                    creaEspNodo('H');

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
                //hoja.dirSigNod = -1;
                hoja.dirNodo = P;
                lisNodo.Add(hoja);
                hojaActual_L = hoja;

                
                r = 0;
            }
          else//Encontrar el nodo hoja en  que debera contener el valor de llave K
            {
                r = 0;

                hojaActual_L = encuentraNodoHoja(K);
                
                r = 0;

            }

            r = 0;
            if(hojaActual_L.K.Count() < n-1 )
            {
                r = 0;
               inset_in_leaf(hojaActual_L, K, P);
            }
            else//Dividir el nodo
            {
                r = 0;
                //Crear Nodo LPrima
                Nodo LPri = new Nodo();
                creaNodo(hojaActual_L, LPri);
                Nodo T = new Nodo();
                copiaValAT(hojaActual_L, T);
                bandT = true;
                //T = hojaActual_L;

                r = 0;
                inset_in_leaf(T, K, P);
                bandT = false;
                r = 0;
                setPunteroSigHoja(hojaActual_L, LPri);
                r = 0;
                //borrHojaActua(hojaActual_L);
                hojaActual_L.K.Clear();
                hojaActual_L.P.Clear();
                r = 0;
                repartirValoresT(T, hojaActual_L, LPri);
                actulizarDivisionArchivo(hojaActual_L, LPri);
                r = 0;
                int KPri = LPri.K.Min();
                r = 0;
                insert_in_parent(hojaActual_L.dirNodo, KPri, LPri.dirNodo);

            }
            r = 0;
            
        }

        public void actulizarDivisionArchivo(Nodo L, Nodo LPri)
        {
            Byte[] bloque = new Byte[48];
            for (int i = 0; i < 48; i++)
            {
                bloque[i] = 0xFF;
            }

            using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
            {   
                //Vaciamos los dos nodos
                bw.Seek((int)L.dirNodo+9, SeekOrigin.Begin);
                bw.Write(bloque);

                bw.Seek((int)L.dirNodo + 9, SeekOrigin.Begin);
                for (int i = 0; i < L.K.Count; i++)
                {
                    bw.Write(L.P.ElementAt(i));
                    bw.Write(L.K.ElementAt(i));
                }

                bw.Seek((int)LPri.dirNodo + 9, SeekOrigin.Begin);
                for (int i = 0; i < LPri.K.Count; i++)
                {
                    bw.Write(LPri.P.ElementAt(i));
                    bw.Write(LPri.K.ElementAt(i));
                }
            }
        }

        public void copiaValAT(Nodo L, Nodo T)
        {
            foreach (int a in L.K)
                T.K.Add(a);
            foreach (int b in L.P)
                T.P.Add(b);
        }

        public void insert_in_parent(long L,int KPri, long LPri)
        {

        }

        public void repartirValoresT(Nodo T, Nodo L, Nodo LPri)
        {
            r = 0;
            for(int i = 0; i < (T.K.Count()-1) - n/2;i++)
            {
                L.P.Add(T.P.ElementAt(i));
                L.K.Add(T.K.ElementAt(i));
            }

            r = 0;
            for (int i = n / 2 ; i <= T.K.Count()-1 ; i++)
            {
                LPri.P.Add(T.P.ElementAt(i));
                LPri.K.Add(T.K.ElementAt(i));
            }
            r = 0;

         }



        public void creaNodo(Nodo L, Nodo LPri)
        {
            creaEspNodo('H');

            LPri.tipo = 'H';
            LPri.dirNodo = L.dirNodo + 65;
            using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
            {
                bw.Seek((int)LPri.dirNodo, SeekOrigin.Begin);
                bw.Write(L.dirNodo);
                bw.Write(L.tipo);
            }


        }

        /*
         * Metodo que actualiza los puntero a siguientes nodos en los nodos actuales
         */
        public void setPunteroSigHoja(Nodo L, Nodo LPri)
        {
            r = 0;
            archArb.Close();
            archArb = File.Open(archArb.Name, FileMode.Open);
            BinaryReader br = new BinaryReader(archArb);

            r = 0;
            
            archArb.Seek(L.dirNodo+57, SeekOrigin.Begin);
            long LPn = br.ReadInt64();

            r = 0;
            //archArb.Seek(LPri.dirNodo + 57, SeekOrigin.Begin);
            archArb.Close();
            using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
            {
                bw.Seek((int)LPri.dirNodo + 57, SeekOrigin.Begin);
                bw.Write(LPn);
                r = 0;

                bw.Seek((int)L.dirNodo + 57, SeekOrigin.Begin);
                bw.Write(LPri.dirNodo);
                L.P.RemoveAt(n-1);
                r = 0;
                L.P.Add(LPri.dirNodo);
                r = 0;

            }

            archArb.Close();
        }

        public Nodo encuentraNodoHoja(int K)
        {
            Nodo a = new Nodo();
            int index = 0;
            int valMax = 0;

            Nodo noRaiz = lisNodo.Find(x => x.tipo == 'R');

            //Nodo noHoj = lisNodo.Find(x => x.K.ElementAt(x) == 8); 
            //Nodo noHoja = lisNodo.Find(x =>  == K && x.P.ElementAt(0) == 5);
            

            foreach (Nodo b in lisNodo)
            {
                r = 0;


                if (b.tipo == 'H')
                {
                    r = 0;
                    valMax = b.K.Max();
                    r = 0;
                    if (K <= valMax)
                    {
                        r = 0;
                        break;
                    }
                }
                else
                if (b.dirNodo == 0 && b.tipo == 'R')
                {
                    r = 0;
                    break;
                }
                else
                {
                    r = 0;
                    index++;
                }
            }

            a = lisNodo.ElementAt(index);

            r = 0;
            //a = lisNodo.Max(x => x.K);

            return a;
        }



        public void inset_in_leaf(Nodo L, int K, long P)
        {
            r = 0;
            if (L.K.Count == 0)
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
            if (K < L.K.ElementAt(0))
            {
                r = 0;
                L.P.Insert(0, P);
                L.K.Insert(0, K);
                r = 0;
            }
            else
            {
                r = 0;

                for (int i = L.K.Count() - 1; i >= 0; i--)
                {
                    r = 0;
                    if (L.K.ElementAt(i) <= K)
                    {
                        r = 0;
                        L.K.Insert(i+1, K);
                        L.P.Insert(i+1, P);
                        r = 0;
                        

                        if (!bandT)
                        {
                            r = 0;
                            recorreDatos(L, i + 1);
                            using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
                            {
                                bw.Seek(((int)L.dirNodo + 9) + ((i + 1) * 12), SeekOrigin.Begin);
                                r = 0;
                                bw.Write(P);
                                bw.Write(K);
                                r = 0;
                            }
                        }
                        r = 0;
                        break;
                    }
                    


                }

                if(L.P.Count == n-1)
                {
                    r = 0;
                    L.P.Add(-1);
                }
                r = 0;
            }
        }

        public void recorreDatos(Nodo L, int indice)
        {
            //entAct.archivoIndPri.Close();
            int indLimitador = n - 1;
            r = 0;
            archArb = File.Open(archArb.Name, FileMode.Open);
            BinaryReader br2 = new BinaryReader(archArb);

            archArb.Seek(((int)L.dirNodo + 9) + (( indice) * 12), SeekOrigin.Begin);

            long longDir = br2.ReadInt64();
            int clave = br2.ReadInt32();

            int auxPos = L.K.Count-1;

            r = 0;

            while (longDir != -1 && indice != auxPos )
            {
                r = 0;
                archArb.Seek(((int)L.dirNodo + 9) + ((auxPos-1) * 12), SeekOrigin.Begin);

                longDir = br2.ReadInt64();
                clave = br2.ReadInt32();

                r = 0;
                archArb.Close();
                using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
                {
                    bw.Seek(((int)L.dirNodo + 9) + ((auxPos) * 12), SeekOrigin.Begin);
                    bw.Write(longDir);
                    bw.Write(clave);
                    r = 0;
                }

                r = 0;
                auxPos--;
                r = 0;
                archArb = File.Open(archArb.Name, FileMode.Open);
                BinaryReader br3 = new BinaryReader(archArb);

                archArb.Seek(((int)L.dirNodo + 9) + ((auxPos - 1) * 12), SeekOrigin.Begin);
                longDir = br3.ReadInt64();
                clave = br3.ReadInt32();
                r = 0;
                


            }
            archArb.Close();
            r = 0;
        }
    }
}
