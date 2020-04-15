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
        public List<Nodo> lisNodPad;
         
        
        public void creaEspNodo(char tipo)
        {

            r = 0;
            archArb.Close();
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
            int posIni = 0;
            bool masNodos = true;

            //archArb.Seek(posAct, SeekOrigin.Begin);
            

            while (true)
            {
                archArb.Seek((int)posIni, SeekOrigin.Begin);
                posIni = posAct;
                r = 0;
                Nodo no = new Nodo();

                dicNod = br.ReadInt64();
                Tipo = br.ReadChars(1);

                no.dirNodo = dicNod;
                no.tipo = Tipo.ElementAt(0);
                posAct += 9;
                r = 0;

                while (br.ReadInt64() != -1 && no.K.Count < n-1)
                {
                    archArb.Seek(posAct, SeekOrigin.Begin);

                    dirPun = br.ReadInt64();
                    

                    r = 0;

                    if (no.tipo == 'H' || no.dirNodo == 0)
                    {
                        //dirPun = br.ReadInt64();
                        clv = br.ReadInt32();

                        no.K.Add(clv);
                        no.P.Add(dirPun);
                        posAct += 12;

                        r = 0;
                    }
                    else
                    {
                        //clv = br.ReadInt32();
                        //dirPun = br.ReadInt64();

                        r = 0;
                        clv = br.ReadInt32();

                        if (clv!= -1)
                        no.K.Add(clv);

                        no.P.Add(dirPun);
                        //dirPun = br.ReadInt64();
                        //no.P.Add(dirPun);
                        posAct += 12;
                        r = 0;
                    }

                    r = 0;



                    if (br.ReadInt64() == -1)//No hay mas claves
                    {
                        bandFinClv = false;
                        r = 0;
                    }

                    r = 0;
                    archArb.Seek(posAct, SeekOrigin.Begin);
                    

                }
                r = 0;
               

                /*if(no.tipo == 'H')
                {
                    archArb.Seek(posIni + 57, SeekOrigin.Begin);
                    int valFinal = br.ReadInt64()
                    no.P.Add(br.ReadInt64());
                }*/
                if(no.P.Count == n-1 && no.tipo == 'H')
                {
                    r = 0;
                    archArb.Seek(posIni + 57, SeekOrigin.Begin);
                    no.P.Add(br.ReadInt64());
                    r = 0;
                }

                if(no.P.Count == n-1  && no.tipo != 'H')
                {
                    archArb.Seek(posIni + 57, SeekOrigin.Begin);
                    long val = br.ReadInt64();
                    if(val!= -1)
                    no.P.Add(val);
                    r = 0;
                }


                if (posIni + 65 == archArb.Length)
                {
                    masNodos = false;

                    r = 0;
                }

                r = 0;
                //archArb.Seek(posIni+57, SeekOrigin.Begin);
                //if (!bandFinClv && br.ReadInt64() == -1 && !masNodos )//llego al final y ya no tiene mas campos que agregar
                if (!masNodos)//llego al final y ya no tiene mas campos que agregar
                {
                    //no.dirSigNod = -1;
                    lisNodo.Add(no);
                    masNodos = true;
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
                creaNodo(LPri, 'H');
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
                repartirValoresT(T, hojaActual_L, LPri,'H');
                r = 0;
                actulizarDivisionArchivo(hojaActual_L, LPri);
                r = 0;
                int KPri = LPri.K.Min();
                r = 0;
                insert_in_parent(hojaActual_L, KPri, LPri);
                r = 0;

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

            r = 0;
            using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
            {   
                //Vaciamos los dos nodos
                bw.Seek((int)L.dirNodo+9, SeekOrigin.Begin);
                bw.Write(bloque);
                r = 0;

                if (L.tipo == 'H')
                {
                    bw.Seek((int)L.dirNodo + 9, SeekOrigin.Begin);
                    for (int i = 0; i < L.K.Count; i++)
                    {
                        bw.Write(L.P.ElementAt(i));
                        bw.Write(L.K.ElementAt(i));
                    }
                    r = 0;

                    bw.Seek((int)LPri.dirNodo + 9, SeekOrigin.Begin);
                    for (int i = 0; i < LPri.K.Count; i++)
                    {
                        bw.Write(LPri.P.ElementAt(i));
                        bw.Write(LPri.K.ElementAt(i));
                    }
                }
                else
                {
                    r = 0;

                    bw.Seek((int)L.dirNodo + 9, SeekOrigin.Begin);
                    for (int i = 0; i < L.P.Count; i++)
                    {
                        bw.Write(L.P.ElementAt(i));
                        if (i != L.P.Count-1 )
                        {
                            r = 0;
                            bw.Write(L.K.ElementAt(i));
                        }
                        r = 0;
                    }
                    r = 0;

                    bw.Seek((int)LPri.dirNodo + 9, SeekOrigin.Begin);
                    for (int i = 0; i < LPri.P.Count; i++)
                    {
                        bw.Write(LPri.P.ElementAt(i));
                        if (i != L.P.Count - 1)
                        {
                            r = 0;
                            bw.Write(LPri.K.ElementAt(i));
                        }
                        r = 0;
                    }


                }

                r = 0;
            }
        }

        public void copiaValAT(Nodo L, Nodo T)
        {
            foreach (int a in L.K)
                T.K.Add(a);
            foreach (int b in L.P)
                T.P.Add(b);
        }

        public void actualizaNodoRaizArchivo(Nodo n)
        {
            /*long valorCom;
            int pos;

            archArb.Close();
            archArb = File.Open(archArb.Name, FileMode.Open);
            BinaryReader br = new BinaryReader(archArb);

            pos = (int)n.dirNodo + 9;
            archArb.Seek(pos, SeekOrigin.Begin);
            valorCom = br.ReadInt64();

            while(valorCom != -1)
            {
                pos += 12;

            }*/

            /*
            valorComp = br.ReadInt64();
            archArb.Close();
            */
            r = 0;
            using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
            {
                bw.Seek((int)n.dirNodo, SeekOrigin.Begin);
                bw.Write(n.dirNodo);
                bw.Write(n.tipo.ToString().ToCharArray());
                bw.Write(n.P.ElementAt(0));
                bw.Write(n.K.ElementAt(0));
                bw.Write(n.P.ElementAt(1));
                r = 0;
            }
            r = 0;

        }

        public void insert_in_parent(Nodo N,int KPri, Nodo NPri)
        {
            r = 0;
            if(N.tipo == 'R')
            {
                r = 0;
                Nodo R = new Nodo();
                R.dirNodo = NPri.dirNodo + 65;
                R.tipo = 'R';
                R.P.Add(N.dirNodo);
                R.K.Add(KPri);
                R.P.Add(NPri.dirNodo);
                r = 0;
                creaEspNodo(R.tipo);
                actualizaNodoRaizArchivo(R);
                r = 0;
                if(N.dirNodo == 0 && N.tipo == 'R')
                {
                    N.tipo = 'H';
                    r = 0;
                    using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
                    {
                        char[] tipoNu = N.tipo.ToString().ToCharArray();
                        bw.Seek((int)N.dirNodo + 8, SeekOrigin.Begin);
                        bw.Write(tipoNu);
                    }
                    r = 0;

                }
                else
                {
                    r = 0;
                    N.tipo = 'I';
                    using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
                    {
                        bw.Seek((int)N.dirNodo+8,SeekOrigin.Begin);
                        bw.Write(N.tipo.ToString().ToCharArray());
                        bw.Seek((int)N.dirNodo + 57, SeekOrigin.Begin);
                        bw.Write((long)-1);

                    }
                    r = 0;


                    //actualizo el anterior a un tipo intermedio

                }
                r = 0;
                lisNodo.Add(R);
                r = 0;
                return;
            }

            Nodo Pad = buscaPadre(N); 

            r = 0;
            if (Pad.P.Count < n)
            {
                int index = Pad.P.FindIndex(x => x == N.dirNodo);
                Pad.K.Insert(index, KPri);
                Pad.P.Insert(index + 1, NPri.dirNodo);
                //Pad.K.Add(KPri);
                //Pad.P.Add(NPri.dirNodo);
                r = 0;
                recorreDatosPad(Pad, index);
                
                r = 0;

                using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
                {
                    bw.Seek(((int)Pad.dirNodo + 17) + ((index ) * 12), SeekOrigin.Begin);
                    r = 0;
                    bw.Write(KPri);
                    bw.Write(NPri.dirNodo);
                    r = 0;
                }
            }
            else
            {//dividi Padre
                r = 0;
                Nodo TP = new Nodo();
                copiaValAT(Pad, TP);
                r = 0;

                int index = Pad.P.FindIndex(x => x == N.dirNodo);
                TP.K.Insert(index, KPri);
                TP.P.Insert(index + 1, NPri.dirNodo);

                r = 0;

                Pad.K.Clear();
                Pad.P.Clear();

                
                r = 0;

                Nodo PadPrim = new Nodo();
                creaNodo(PadPrim, 'I');
                r = 0;

                repartirValoresT(TP, Pad, PadPrim,'P');
                r = 0;
                actulizarDivisionArchivo(Pad, PadPrim);
                r = 0;
                int KPadPri = TP.K.ElementAt(((n + 1) / 2)-1);

                r = 0;
                
                insert_in_parent(Pad, KPadPri, PadPrim);

                r = 0;

                


            }

            r = 0;

        }

        public Nodo buscaPadre(Nodo n)
        {

            int index = lisNodPad.FindIndex(x => x.dirNodo == n.dirNodo);
            Nodo padre = lisNodPad.ElementAt(index-1);

            r = 0;
            return padre;
        }

        public void repartirValoresT(Nodo T, Nodo L, Nodo LPri,char tipo)
        {
            r = 0;
            if (tipo == 'H')
            {
                for (int i = 0; i < (T.K.Count() - 1) - n / 2; i++)
                {
                    L.P.Add(T.P.ElementAt(i));
                    L.K.Add(T.K.ElementAt(i));
                }

                r = 0;
                for (int i = n / 2; i <= T.K.Count() - 1; i++)
                {
                    LPri.P.Add(T.P.ElementAt(i));
                    LPri.K.Add(T.K.ElementAt(i));
                }
                r = 0;
            }
            else
            {
                r = 0;
                for (int i = 0; i < (T.P.Count()) - (n+1) / 2; i++)
                {
                    L.P.Add(T.P.ElementAt(i));
                    L.K.Add(T.K.ElementAt(i));
                    
                }
                L.K.RemoveAt(L.K.Count - 1);

                r = 0;
                for (int i = (n+1) / 2; i <= T.P.Count()-1; i++)
                {
                    LPri.P.Add(T.P.ElementAt(i));
                    if(i != T.P.Count -1 )
                    LPri.K.Add(T.K.ElementAt(i));
                }
                r = 0;
            }

         }



        public void creaNodo(Nodo LPri, char tipo)
        {
            if (tipo == 'H')
            {
                creaEspNodo('H');
                LPri.tipo = 'H';
            }
            else
            {
                creaEspNodo('I');
                LPri.tipo = 'I';
            }

            LPri.dirNodo = lisNodo.ElementAt(lisNodo.Count - 1).dirNodo + 65;
            using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
            {
                bw.Seek((int)LPri.dirNodo, SeekOrigin.Begin);
                bw.Write(LPri.dirNodo);
                bw.Write(LPri.tipo.ToString().ToCharArray());
            }

            lisNodo.Add(LPri);


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

        public Nodo encuentraNodoHoja(int V)
        {
            Nodo a = new Nodo();
            int valI = -1;
            long Pm = -1;
            int i = 0;
            lisNodPad = new List<Nodo>();
            r = 0;
            Nodo C = lisNodo.Find(x => x.tipo == 'R');
            lisNodPad.Add(C);

            r = 0;
            if (C.dirNodo == 0 && C.tipo == 'R')
            {
                r = 0;
                return C;
                
            }

            r = 0;

            while (C.tipo != 'H') 
            {
                r = 0;
                i = 0;
                valI = -1;
                Pm = -1;

                for (; i < C.K.Count; i++)
                {
                    if (V <= C.K.ElementAt(i))
                    {
                        r = 0;
                        valI = C.K.ElementAt(i);
                        break;
                    }
                }

                r = 0;

                if (C.tipo != 'H' && valI == -1 )
                    i--;

                r = 0;


                if (valI == -1)
                {
                    r = 0;
                    Pm = C.P.ElementAt(C.P.Count - 1);
                    C = lisNodo.Find(x => x.dirNodo == Pm);
                    r = 0;
                }
                else
                {
                    if (V == C.K.ElementAt(i))
                    {
                        r = 0;
                        C = lisNodo.Find(x => x.dirNodo == C.P.ElementAt(i + 1));
                        r = 0;
                    }
                    else
                    {
                        r = 0;
                        C = lisNodo.Find(x => x.dirNodo == C.P.ElementAt(i));
                        r = 0;
                    }
                }
                
                lisNodPad.Add(C);
                r = 0;

            }
            r = 0;

            return C;
            /*
            //Esto es para el findValor
            foreach (int b in C.K)
            {
                if (b == V)
                {
                    r = 0;
                    return C;
                }
            }

            r = 0;
            return null;
            */

            //Nodo noHoj = lisNodo.Find(x => x.K.ElementAt(x) == 8); 
            //Nodo noHoja = lisNodo.Find(x =>  == K && x.P.ElementAt(0) == 5);
            

            /*foreach (Nodo b in lisNodo)
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

            return a;*/
        }



        public void inset_in_leaf(Nodo L, int K, long P)
        {
            r = 0;
            if (L.K.Count == 0)//Es el primer nodo
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
                    bw.Write(L.tipo.ToString().ToCharArray());
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
                    archArb.Close();
                    archArb = File.Open(archArb.Name, FileMode.Open);
                    BinaryReader br = new BinaryReader(archArb);
                    archArb.Seek((int)L.dirNodo + 57, SeekOrigin.Begin);
                    L.P.Add(br.ReadInt64());
                    archArb.Close();
                    
                }
                r = 0;
            }
        }

        public void recorreDatos(Nodo L, int indice)
        {
            //entAct.archivoIndPri.Close();

            r = 0;
            archArb = File.Open(archArb.Name, FileMode.Open);
            BinaryReader br2 = new BinaryReader(archArb);

            archArb.Seek(((int)L.dirNodo + 9) + (( indice) * 12), SeekOrigin.Begin);

            long longDir;
            int clave;


            longDir = br2.ReadInt64();
            clave = br2.ReadInt32();


            int auxPos = L.K.Count-1;

            r = 0;

            while (longDir != -1 && indice != auxPos )
            {
                r = 0;

                //archArb.Seek(((int)L.dirNodo + 9) + ((auxPos-1) * 12), SeekOrigin.Begin);
                archArb.Close();
                archArb = File.Open(archArb.Name, FileMode.Open);
                br2 = new BinaryReader(archArb);
                archArb.Seek(((int)L.dirNodo + 9) + ((auxPos - 1) * 12), SeekOrigin.Begin);
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
                //clave = br3.ReadInt32();
                r = 0;
                
                


            }
            archArb.Close();
            r = 0;
        }

        public void recorreDatosPad(Nodo L, int indice)
        {
            //entAct.archivoIndPri.Close();

            r = 0;
            archArb = File.Open(archArb.Name, FileMode.Open);
            BinaryReader br2 = new BinaryReader(archArb);

            archArb.Seek(((int)L.dirNodo + 9) + ((indice) * 12), SeekOrigin.Begin);

            long longDir;
            int clave;
            bool hayClave = true;


            longDir = br2.ReadInt64();



            int auxPos = L.P.Count - 1;

            r = 0;

            while (longDir != -1 && indice != auxPos)
            {
                r = 0;

                //archArb.Seek(((int)L.dirNodo + 9) + ((auxPos-1) * 12), SeekOrigin.Begin);
                archArb.Close();
                archArb = File.Open(archArb.Name, FileMode.Open);
                br2 = new BinaryReader(archArb);
                archArb.Seek(((int)L.dirNodo + 9) + ((auxPos-1)*12), SeekOrigin.Begin);
                longDir = br2.ReadInt64();

                clave = br2.ReadInt32();
                r = 0;

                if (clave == -1)
                {
                    hayClave = false;
                }
                r = 0;

                archArb.Close();
                using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
                {
                    bw.Seek(((int)L.dirNodo + 9) + ((auxPos) * 12), SeekOrigin.Begin);
                    bw.Write(longDir);

                    if (hayClave)
                    {
                        bw.Write(clave);
                    }
                    r = 0;
                }

                hayClave = true;
                r = 0;
                auxPos--;
                r = 0;
                archArb = File.Open(archArb.Name, FileMode.Open);
                BinaryReader br3 = new BinaryReader(archArb);

                archArb.Seek(((int)L.dirNodo + 9) + ((auxPos - 1) * 12), SeekOrigin.Begin);
                longDir = br3.ReadInt64();

                r = 0;

            }
            archArb.Close();
            r = 0;
        }

    }
}
