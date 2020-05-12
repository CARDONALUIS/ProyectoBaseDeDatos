using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PruebaProyecto
{
    public partial class Datos : Form
    {
        Diccionario dic;
        public FileStream archivoDatos;
        public String nomArchivoDatos;
        public int tamYFormDat;
        int r;
        public List<FileStream> listEntDat;
        public List<IndicePrimario> listIndPri = new List<IndicePrimario>();
        Entidad entAct;
        int lonRegisAct = 16;
        bool bandElim = false;
        bool bandModi = false;
        int posAtrBus = 0;
        bool bandAtrBus = false;
        bool bandAtrPri = false;
        bool bandAtrSec = false;
        bool bandModAtrBus = false;
        bool bandUltModiAtr = false;
        bool bandAtrArb = false;
        bool bandAtrHashEsta = false;
        VentanaIndPrim vIP = new VentanaIndPrim();
        venIndiceSec vIS = new venIndiceSec();
        ventanaArbol vAR = new ventanaArbol();
        ventanaHashEsta vHE = new ventanaHashEsta();
        int indColIndPri = 0;
        long dirAuxElimReg = 0;
        long dirSigAuxElimReg = 0;
        bool bandCajonRep = false;
        ArbolB_Primario arbol;
        HashEstatico hash;





        public Datos()
        {
            InitializeComponent();
            listEntDat = new List<FileStream>();
            AplicaCambio.Visible = false;
        }

        public void actualizaDicc(Diccionario d)
        {
            int r = 0;
            dic = d;

            foreach (Entidad a in dic.listEntidad)
            {
                comboBoxEntiDatos.Items.Add(a.nombre);
            }
        }

        public void creaArchivosDat(Entidad ent)
        {
            ent.archivoDat = (new FileStream(BitConverter.ToString(ent.id_enti) + ".dat", FileMode.Create));
            entAct.archivoDat.Close();
        }


        /* 
        AQUI EMPIEZAN METDOS DE INDICES SECUNDARIOS     
        */

        /*
         Elimina en indices secundarios
             */

        public void cajonVacio(int dirCajon, int contIndSec)//Elimina la referencia a ese cajon en el bloque principla del archivo secundario
        {
            r = 0;
            entAct.lisIndSec.ElementAt(contIndSec).archSec = File.Open(entAct.lisIndSec.ElementAt(contIndSec).archSec.Name, FileMode.Open);
            BinaryReader br2 = new BinaryReader(entAct.lisIndSec.ElementAt(contIndSec).archSec);
            entAct.lisIndSec.ElementAt(contIndSec).archSec.Seek(dirCajon, SeekOrigin.Begin);

            string findEnd = br2.ReadInt32().ToString();
            int pos = 0;
            int posAElim = 0;

            r = 0;
            if(findEnd == "-1")
            {
                entAct.lisIndSec.ElementAt(contIndSec).archSec.Seek(pos +entAct.lisIndSec.ElementAt(contIndSec).longAtrSec, SeekOrigin.Begin);
                findEnd = br2.ReadInt32().ToString();

                r = 0;

                while (findEnd != dirCajon.ToString())
                {
                    pos += entAct.lisIndSec.ElementAt(contIndSec).longBloqSec;
                    entAct.lisIndSec.ElementAt(contIndSec).archSec.Seek(pos+entAct.lisIndSec.ElementAt(contIndSec).longAtrSec, SeekOrigin.Begin);
                    findEnd = br2.ReadInt32().ToString();
                    posAElim++;
                    r = 0;
                }
                r = 0;
                pos += entAct.lisIndSec.ElementAt(contIndSec).longBloqSec;
                string finArch;

                r = 0;
           

                entAct.lisIndSec.ElementAt(contIndSec).archSec.Seek(pos, SeekOrigin.Begin);
                if(entAct.lisIndSec.ElementAt(contIndSec).tipo == 'C')
                {
                    finArch = new string(br2.ReadChars(entAct.lisIndSec.ElementAt(contIndSec).longAtrSec));
                }
                else
                {
                    finArch = br2.ReadInt32().ToString();
                }

                int dirCajoMov = br2.ReadInt32();
                r = 0;

                while (finArch != "-1" && char.IsLetterOrDigit(finArch[0]))
                {
                    r = 0;
                    entAct.lisIndSec.ElementAt(contIndSec).archSec.Close();
                    using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.lisIndSec.ElementAt(contIndSec).archSec.Name, FileMode.Open)))
                    {
                        bw.Seek(pos- entAct.lisIndSec.ElementAt(contIndSec).longBloqSec, SeekOrigin.Begin);
                        if (entAct.lisIndSec.ElementAt(contIndSec).tipo == 'E')
                            bw.Write(Int32.Parse(finArch));
                        else
                            bw.Write(finArch.ToCharArray());
                        bw.Write(dirCajoMov);
                    }
                    r = 0;
                    pos += entAct.lisIndSec.ElementAt(contIndSec).longBloqSec;

                    r = 0;

                    entAct.lisIndSec.ElementAt(contIndSec).archSec = File.Open(entAct.lisIndSec.ElementAt(contIndSec).archSec.Name, FileMode.Open);
                    BinaryReader br3 = new BinaryReader(entAct.lisIndSec.ElementAt(contIndSec).archSec);
                    entAct.lisIndSec.ElementAt(contIndSec).archSec.Seek(pos, SeekOrigin.Begin);

                    if (entAct.lisIndSec.ElementAt(contIndSec).tipo == 'C')
                    {
                        finArch = new string(br3.ReadChars(entAct.lisIndSec.ElementAt(contIndSec).longAtrSec));
                    }
                    else
                    {
                        finArch = br3.ReadInt32().ToString();
                    }

                    dirCajoMov = br3.ReadInt32();


                    r = 0;
                    entAct.lisIndSec.ElementAt(contIndSec).archSec.Close();

                }
                r = 0;
                entAct.lisIndSec.ElementAt(contIndSec).archSec.Close();
                using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.lisIndSec.ElementAt(contIndSec).archSec.Name, FileMode.Open)))
                {
                    r = 0;
                    bw.Seek(pos - entAct.lisIndSec.ElementAt(contIndSec).longBloqSec, SeekOrigin.Begin);

                    if (entAct.lisIndSec.ElementAt(contIndSec).tipo == 'E')
                        bw.Write(Int32.Parse(finArch));
                    else
                        bw.Write(finArch.ToCharArray());

                    bw.Write(dirCajoMov);
                }
            }
            
            entAct.lisIndSec.ElementAt(contIndSec).archSec.Close();
        }


        public void eliminaBloquSec(string clavpri, int dirEli, int contIndSec)
        {
            entAct.lisIndSec.ElementAt(contIndSec).archSec = File.Open(entAct.lisIndSec.ElementAt(contIndSec).archSec.Name, FileMode.Open);
            BinaryReader br1 = new BinaryReader(entAct.lisIndSec.ElementAt(contIndSec).archSec);
            string busInd;

            int longCad = clavpri.Length;

            int pos = 0;


            entAct.lisIndSec.ElementAt(contIndSec).archSec.Seek(pos, SeekOrigin.Begin);

            if (entAct.lisIndSec.ElementAt(contIndSec).tipo == 'C')
                busInd = new string(br1.ReadChars(longCad));
            else
                busInd = br1.ReadInt32().ToString();

            int posIndP = entAct.longRegIndPri + entAct.longClvPrim;
            

            pos += entAct.lisIndSec.ElementAt(contIndSec).longBloqSec;

            r = 0;
            while (busInd != clavpri)
            {
                r = 0;
                entAct.lisIndSec.ElementAt(contIndSec).archSec.Seek(pos, SeekOrigin.Begin);

                if (entAct.lisIndSec.ElementAt(contIndSec).tipo == 'C')
                    busInd = new string(br1.ReadChars(longCad));
                else
                    busInd = br1.ReadInt32().ToString();

                pos += entAct.lisIndSec.ElementAt(contIndSec).longBloqSec;
                r = 0;
            }

            entAct.lisIndSec.ElementAt(contIndSec).archSec.Seek(pos-8, SeekOrigin.Begin);
            int dirCajonBorr = br1.ReadInt32();
            
            r = 0;
           
            entAct.lisIndSec.ElementAt(contIndSec).archSec.Seek(dirCajonBorr, SeekOrigin.Begin);
            int dirCom = br1.ReadInt32();

            r = 0;
            pos = dirCajonBorr;
            r = 0;
            while (dirCom != dirEli)
            {
                r = 0;
                entAct.lisIndSec.ElementAt(contIndSec).archSec.Seek(pos+8, SeekOrigin.Begin);

                dirCom = br1.ReadInt32();///ERROR/////////////////////////////////////////////////////////

                pos += 8;
                r = 0;
            }

            r = 0;



            string findEnd;

            entAct.lisIndSec.ElementAt(contIndSec).archSec.Seek(pos+8, SeekOrigin.Begin);
            findEnd = br1.ReadInt32().ToString();

            r = 0;


            while (findEnd != "-1")
            {
                entAct.lisIndSec.ElementAt(contIndSec).archSec.Close();
                using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.lisIndSec.ElementAt(contIndSec).archSec.Name, FileMode.Open)))
                {
                    bw.Seek(pos, SeekOrigin.Begin);
                    bw.Write(Int32.Parse(findEnd));
                }
                r = 0;
                pos +=  8;


                entAct.lisIndSec.ElementAt(contIndSec).archSec = File.Open(entAct.lisIndSec.ElementAt(contIndSec).archSec.Name, FileMode.Open);
                BinaryReader br2 = new BinaryReader(entAct.lisIndSec.ElementAt(contIndSec).archSec);
                entAct.lisIndSec.ElementAt(contIndSec).archSec.Seek(pos+8, SeekOrigin.Begin);

                findEnd = br2.ReadInt32().ToString();

                r = 0;
                entAct.lisIndSec.ElementAt(contIndSec).archSec.Close();

            }

            r = 0;
            entAct.lisIndSec.ElementAt(contIndSec).archSec.Close();
            using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.lisIndSec.ElementAt(contIndSec).archSec.Name, FileMode.Open)))
            {
                r = 0;
                bw.Seek(pos, SeekOrigin.Begin);
                bw.Write(Int32.Parse(findEnd));
            }

            cajonVacio(dirCajonBorr, contIndSec);
            

            r = 0;
            
        }

        public void colocaEnCajon(int dirCajon, int dirReg, int indArchSec)
        {
            r = 0;
            string valorCajon = "";

            entAct.lisIndSec.ElementAt(indArchSec).archSec = File.Open(entAct.lisIndSec.ElementAt(indArchSec).archSec.Name, FileMode.Open);
            BinaryReader br1 = new BinaryReader(entAct.lisIndSec.ElementAt(indArchSec).archSec);

            r = 0;
            entAct.lisIndSec.ElementAt(indArchSec).archSec.Seek(dirCajon,SeekOrigin.Begin);
            valorCajon = br1.ReadInt32().ToString();

            r = 0;
            int dirAct = dirCajon;

            while (valorCajon != "-1")
            {
                r = 0;
                dirAct += 8;
                entAct.lisIndSec.ElementAt(indArchSec).archSec.Seek(dirAct, SeekOrigin.Begin);
                valorCajon = br1.ReadInt32().ToString();
                r = 0;

            }

            r = 0;

            entAct.lisIndSec.ElementAt(indArchSec).archSec.Close();

            r = 0;
            using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.lisIndSec.ElementAt(indArchSec).archSec.Name, FileMode.Open)))
            {
                r = 0;
                bw.Seek(dirAct, SeekOrigin.Begin);
                bw.Write(dirReg);
                r = 0;
            }

            bandCajonRep = false;
        }
        
        /*Evento para el llamdo a la ventana de IndiSecundario*/
        private void eventoIndiSec_Click(object sender, EventArgs e)
        {
            vIS = new venIndiceSec(); 
            if (entAct != null)
            {
                vIS.entAct = entAct;
                vIS.asignaValores();
                vIS.Show();
            }
            else
                MessageBox.Show("Selecciona una entidad");
           
        }


        /*
        Este metodo reacomoda segun las claves secundaria que se ingresaron 
        */
        public void reacomodaPorPosiSec(int pos, int indArchSec)
        {
            string indS;
            int dir;
            int posOrg = pos;

            entAct.lisIndSec.ElementAt(indArchSec).archSec = File.Open(entAct.lisIndSec.ElementAt(indArchSec).archSec.Name, FileMode.Open);
            BinaryReader br1 = new BinaryReader(entAct.lisIndSec.ElementAt(indArchSec).archSec);
            r = 0;
            entAct.lisIndSec.ElementAt(indArchSec).archSec.Seek(pos * entAct.lisIndSec.ElementAt(indArchSec).longBloqSec, SeekOrigin.Begin);


            if (entAct.lisIndSec.ElementAt(indArchSec).tipo== 'E')
                indS = br1.ReadInt32().ToString();
            else
                indS = new string(br1.ReadChars(entAct.lisIndSec.ElementAt(indArchSec).longAtrSec));

            r = 0;
            while (indS != "-1" && char.IsLetterOrDigit(indS.ElementAt(0)))
            {
                BinaryReader br2 = new BinaryReader(entAct.lisIndSec.ElementAt(indArchSec).archSec);
                r = 0;
                entAct.lisIndSec.ElementAt(indArchSec).archSec.Seek((pos + 1) * entAct.lisIndSec.ElementAt(indArchSec).longBloqSec, SeekOrigin.Begin);


                if (entAct.lisIndSec.ElementAt(indArchSec).tipo == 'E')
                    indS = br2.ReadInt32().ToString();
                else
                    indS = new string(br2.ReadChars(entAct.lisIndSec.ElementAt(indArchSec).longAtrSec));

                pos++;
                r = 0;
            }
            r = 0;
            int posAreco = pos - posOrg;
            r = 0;

            while (posAreco > 0)
            {
                entAct.lisIndSec.ElementAt(indArchSec).archSec.Close();
                entAct.lisIndSec.ElementAt(indArchSec).archSec = File.Open(entAct.lisIndSec.ElementAt(indArchSec).archSec.Name, FileMode.Open);
                BinaryReader br3 = new BinaryReader(entAct.lisIndSec.ElementAt(indArchSec).archSec);
                r = 0;
                entAct.lisIndSec.ElementAt(indArchSec).archSec.Seek((pos - 1) * entAct.lisIndSec.ElementAt(indArchSec).longBloqSec, SeekOrigin.Begin);


                if (entAct.lisIndSec.ElementAt(indArchSec).tipo== 'E')
                    indS = br3.ReadInt32().ToString();
                else
                    indS = new string(br3.ReadChars(entAct.lisIndSec.ElementAt(indArchSec).longAtrSec));

                dir = br3.ReadInt32();

                r = 0;
                entAct.lisIndSec.ElementAt(indArchSec).archSec.Close();
                using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.lisIndSec.ElementAt(indArchSec).archSec.Name, FileMode.Open)))
                {
                    bw.Seek(pos * entAct.lisIndSec.ElementAt(indArchSec).longBloqSec, SeekOrigin.Begin);

                    if (entAct.lisIndSec.ElementAt(indArchSec).tipo == 'E')
                        bw.Write(Int32.Parse(indS));
                    else
                        bw.Write(indS.ToCharArray());

                    bw.Write(dir);
                }
                r = 0;

                posAreco--;
                pos--;
            }
            entAct.lisIndSec.ElementAt(indArchSec).archSec.Close();
            r = 0;
        }


        /*Este metodo me obtiene la posicion a escribir en el archivo para el indice secundario*/
        public int obtenPoscionAEscribirIDS(Object clvPrim, int indArch)
        {
            r = 0;
            entAct.lisIndSec.ElementAt(indArch).archSec.Close();
            entAct.lisIndSec.ElementAt(indArch).archSec = File.Open(entAct.lisIndSec.ElementAt(indArch).archSec.Name, FileMode.Open);

            BinaryReader brBy = new BinaryReader(entAct.lisIndSec.ElementAt(indArch).archSec);
            BinaryReader brCo = new BinaryReader(entAct.lisIndSec.ElementAt(indArch).archSec);

            int posicion = 0;

            r = 0;
            entAct.lisIndSec.ElementAt(indArch).archSec.Seek(0, SeekOrigin.Begin);

            string valoACom = "";

            if (entAct.lisIndSec.ElementAt(indArch).tipo == 'C')
            {
                r = 0;
                valoACom = new string(brCo.ReadChars(entAct.lisIndSec.ElementAt(indArch).longAtrSec));
                r = 0;
            }
            else
            {
                r = 0;
                valoACom = brCo.ReadInt32().ToString();
                r = 0;
            }

            r = 0;
            string comp = clvPrim.ToString();
            int posCom = entAct.lisIndSec.ElementAt(indArch).longBloqSec;
            int dirCom = 0;
            r = 0;

            if (entAct.lisIndSec.ElementAt(indArch).tipo == 'E')
            {
                string[] nuev = adaptNumero(valoACom, comp);

                valoACom = nuev.ElementAt(0);
                comp = nuev.ElementAt(1);
            }

            r = 0;
          
            while (string.CompareOrdinal(valoACom, comp) < 0 && valoACom != "-1")//Aqui aplicar el de que si son iguales las guarde iguales
            {
                r = 0;
                entAct.lisIndSec.ElementAt(indArch).archSec.Seek(posCom, SeekOrigin.Begin);

                if (entAct.lisIndSec.ElementAt(indArch).tipo == 'C')
                {
                    valoACom = new string(brCo.ReadChars(entAct.lisIndSec.ElementAt(indArch).longAtrSec));
                }
                else
                {
                    valoACom = brCo.ReadInt32().ToString();
                }
                posicion++;
                posCom += entAct.lisIndSec.ElementAt(indArch).longBloqSec;
                dirCom = brCo.ReadInt32();
                r = 0;
              

                if (entAct.lisIndSec.ElementAt(indArch).tipo == 'E')
                {
                    string[] nuev = adaptNumero(valoACom, comp);

                    valoACom = nuev.ElementAt(0);
                    comp = nuev.ElementAt(1);
                }
            }

            if (string.CompareOrdinal(valoACom, comp) == 0)
            {
                r = 0;
                bandCajonRep = true;
            }
            else
            {
                r = 0;

                dirCom = posicion;
                entAct.lisIndSec.ElementAt(indArch).creaCajon((int)entAct.lisIndSec.ElementAt(indArch).archSec.Length);
                r = 0;
            }

            
            r = 0;

            entAct.lisIndSec.ElementAt(indArch).archSec.Close();

            return dirCom;

        }


        /*Escribe en el archivos secundario el valor de su llave secundaria*/
        public void guardaArchivosIndSec()
        {
            r = 0;
            foreach(IndiceSecundario a in entAct.lisIndSec)
            {
                entAct.archivoDat = File.Open(entAct.archivoDat.Name, FileMode.Open);
                BinaryReader br = new BinaryReader(entAct.archivoDat);
                int dirUltReg;

                r = 0;
                if (!bandModAtrBus)
                    dirUltReg = (int)entAct.archivoDat.Length - entAct.longAtributos;
                else
                    dirUltReg = (int)dirAuxElimReg;


                entAct.archivoDat.Seek(a.posAtrSec + dirUltReg, SeekOrigin.Begin);


                //Esta es una clase generica para guardar valores de estructuras de llaves
                IndicePrimario indP = new IndicePrimario();
                int posAGuardReg = 0;


                r = 0;

                if (a.tipo == 'C')
                {
                    r = 0;
                    indP.clv_prim = new string(br.ReadChars(a.longAtrSec));
                }
                else
                {
                    indP.clv_prim = br.ReadInt32();
                }

                indP.dir_reg = dirUltReg;

                r = 0;

                posAGuardReg = obtenPoscionAEscribirIDS(indP.clv_prim, a.contIndSec);

                r = 0;
                if (!bandCajonRep)
                {
                    r = 0;
                    reacomodaPorPosiSec(posAGuardReg, a.contIndSec);
                    r = 0;
                    a.archSec = File.Open(a.archSec.Name, FileMode.Open);
                    long longArchSec = a.archSec.Length - 2048;
                    a.archSec.Close();
                    r = 0;
                    using (BinaryWriter bw = new BinaryWriter(File.Open(a.archSec.Name, FileMode.Open)))
                    {

                        bw.Seek(posAGuardReg * a.longBloqSec, SeekOrigin.Begin);
                        r = 0;
                        if (a.tipo == 'C')
                        {

                            bw.Write(indP.clv_prim.ToString().ToCharArray());
                            int contChar = 0;
                            while (contChar < a.longAtrSec - indP.clv_prim.ToString().ToCharArray().Length)
                            {
                                bw.Write('-');
                                contChar++;
                                r = 0;
                            }
                            contChar = 0;
                        }
                        else
                        {
                            int algo = Convert.ToInt32(indP.clv_prim);
                            r = 0;
                            bw.Write(algo);
                        }
                        bw.Write(longArchSec);

                         r = 0;

                        bw.Seek((int)longArchSec, SeekOrigin.Begin);
                        bw.Write(dirUltReg);
                    }

                }
                else
                {
                    if (posAGuardReg == 0)
                    {
                        r = 0;
                        a.archSec = File.Open(a.archSec.Name, FileMode.Open);
                        BinaryReader br2 = new BinaryReader(a.archSec);
                        a.archSec.Seek(a.longAtrSec, SeekOrigin.Begin);

                        posAGuardReg = br2.ReadInt32();
                        a.archSec.Close();
                        r = 0;
                    }
                    r = 0;
                    colocaEnCajon(posAGuardReg , dirUltReg, a.contIndSec);
                }
                
                r = 0;
     
                entAct.archivoDat.Close();

            }
        }


        public void datosIndSec()
        {

            r = 0;
            foreach (Atributo a in entAct.listAtrib)
            {
                if (a.dirIndi == -1 && a.tipoIndi == 3)
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

            guardaArchivosIndSec();
        }
        /*
         * TERMINAN METODOS DE INDICES SECUNDARIOS
         */



        #region  MetodosindPrim
        public void eliminaRegClvPrim(int dirEli)
        {
            r = 0;
            entAct.archivoIndPri.Close();
            entAct.archivoIndPri = File.Open(entAct.archivoIndPri.Name, FileMode.Open);
            BinaryReader br = new BinaryReader(entAct.archivoIndPri);

            entAct.archivoIndPri.Seek(entAct.longClvPrim, SeekOrigin.Begin);
            int dirComp = br.ReadInt32();

            int posIndP = entAct.longRegIndPri + entAct.longClvPrim;

            r = 0;

            while (dirComp != dirEli)
            {
                r = 0;
                entAct.archivoIndPri.Seek(posIndP, SeekOrigin.Begin);
                dirComp = br.ReadInt32();
                posIndP += entAct.longRegIndPri;
                r = 0;
            }
            r = 0;
            posIndP -= entAct.longClvPrim;

            string findEnd;


            entAct.archivoIndPri.Seek(posIndP, SeekOrigin.Begin);

            if (entAct.tipoCvePrima == 'E')
                findEnd = br.ReadInt32().ToString();
            else
                findEnd = br.ReadChars(entAct.longClvPrim).ToString();

            int dirSig = br.ReadInt32();

            while (findEnd != "-1")
            {
                entAct.archivoIndPri.Close();
                using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.archivoIndPri.Name, FileMode.Open)))
                {
                    bw.Seek(posIndP - entAct.longRegIndPri, SeekOrigin.Begin);

                    if (entAct.tipoCvePrima == 'E')
                        bw.Write(Int32.Parse(findEnd));
                    else
                        bw.Write(findEnd);

                    bw.Write(dirSig);
                }
                r = 0;
                posIndP += entAct.longRegIndPri;

                entAct.archivoIndPri = File.Open(entAct.archivoIndPri.Name, FileMode.Open);
                BinaryReader br2 = new BinaryReader(entAct.archivoIndPri);
                entAct.archivoIndPri.Seek(posIndP, SeekOrigin.Begin);

                if (entAct.tipoCvePrima == 'E')
                    findEnd = br2.ReadInt32().ToString();
                else
                    findEnd = new string(br2.ReadChars(entAct.longClvPrim));

                dirSig = br2.ReadInt32();
                r = 0;

                entAct.archivoIndPri.Close();

            }

            entAct.archivoIndPri.Close();
            using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.archivoIndPri.Name, FileMode.Open)))
            {
                bw.Seek(posIndP - entAct.longRegIndPri, SeekOrigin.Begin);
                if (entAct.tipoCvePrima == 'E')
                    bw.Write(Int32.Parse(findEnd));
                else
                    bw.Write(findEnd);

                bw.Write(dirSig);
            }


            r = 0;
            entAct.archivoIndPri.Close();

        }


        private void EventoIndPrim_Click(object sender, EventArgs e)
        {

            string al = entAct.archivoIndPri.Name;
            r = 0;
            entAct.archivoIndPri.Close();
            entAct.archivoIndPri = File.Open(entAct.archivoIndPri.Name, FileMode.Open);
            BinaryReader br = new BinaryReader(entAct.archivoIndPri);
            Object comp;

            entAct.archivoIndPri.Seek(0, SeekOrigin.Begin);

            if (entAct.tipoCvePrima == 'C')
            {
                comp = new string(br.ReadChars(entAct.longClvPrim));
            }
            else
            {
                comp = br.ReadInt32();
            }

            int posIni = 0;




            while (comp.ToString() != "-1" && char.IsLetterOrDigit(comp.ToString().ElementAt(0)))
            {

                IndicePrimario indP = new IndicePrimario();

                entAct.archivoIndPri.Seek(posIni, SeekOrigin.Begin);
                if (entAct.tipoCvePrima == 'C')
                {
                    indP.clv_prim = new string(br.ReadChars(entAct.longClvPrim));
                }
                else
                {
                    indP.clv_prim = br.ReadInt32();
                }


                indP.dir_reg = br.ReadInt32();
                r = 0;
                listIndPri.Add(indP);

                r = 0;
                posIni = posIni + entAct.longRegIndPri;

                entAct.archivoIndPri.Seek(posIni, SeekOrigin.Begin);
                r = 0;
                if (entAct.tipoCvePrima == 'C')
                {
                    comp = new string(br.ReadChars(entAct.longClvPrim));
                }
                else
                {
                    comp = br.ReadInt32();
                }

                r = 0;
            }

            entAct.archivoIndPri.Close();
            vIP.asignaListInd(listIndPri);
            vIP.ShowDialog();
            vIP.dataGridIndPrim.Rows.Clear();
            listIndPri.Clear();



        }

        public void leeIndPrim()
        {
            r = 0;
            guardaArchivosIndPri();
        }

        public void guardaArchivosIndPri()
        {
            entAct.archivoDat.Close();
            entAct.archivoDat = File.Open(entAct.archivoDat.Name, FileMode.Open);
            BinaryReader br = new BinaryReader(entAct.archivoDat);
            int dirUltReg;
            r = 0;
            if (!bandModAtrBus)
                dirUltReg = (int)entAct.archivoDat.Length - entAct.longAtributos;
            else
                dirUltReg = (int)dirAuxElimReg;

            entAct.archivoDat.Seek(entAct.posCvePrima + dirUltReg, SeekOrigin.Begin);

            r = 0;

            IndicePrimario indP = new IndicePrimario();
            int posAGuardReg = 0;


            r = 0;

            if (entAct.tipoCvePrima == 'C')
            {
                r = 0;
                indP.clv_prim = new string(br.ReadChars(entAct.longClvPrim));
            }
            else
            {
                indP.clv_prim = br.ReadInt32();
            }
            r = 0;

            posAGuardReg = obtenPoscionAEscribirIDP(indP.clv_prim);
            reacomodaPorPosi(posAGuardReg);

            indP.dir_reg = dirUltReg;
            entAct.contIndPrims++;

            entAct.archivoIndPri.Close();
            r = 0;
            r = 0;
            r = 0;
            using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.archivoIndPri.Name, FileMode.Open)))
            {

                bw.Seek(posAGuardReg * entAct.longRegIndPri, SeekOrigin.Begin);

                r = 0;
                if (entAct.tipoCvePrima == 'C')
                {
                    bw.Write(indP.clv_prim.ToString().ToCharArray());
                    int contChar = 0;
                    while (contChar < entAct.longClvPrim - indP.clv_prim.ToString().ToCharArray().Length)
                    {
                        bw.Write('-');
                        contChar++;
                        r = 0;
                    }
                    contChar = 0;
                }
                else
                {
                    int algo = Convert.ToInt32(indP.clv_prim);
                    r = 0;
                    bw.Write(algo);
                }
                bw.Write(indP.dir_reg);
                r = 0;

            }
            entAct.archivoIndPri.Close();
            entAct.archivoDat.Close();
            r = 0;
        }


        public int obtenPoscionAEscribirIDP(Object clvPrim)
        {
            r = 0;
            entAct.archivoIndPri.Close();
            entAct.archivoIndPri = File.Open(entAct.archivoIndPri.Name, FileMode.Open);


            BinaryReader brBy = new BinaryReader(entAct.archivoIndPri);
            BinaryReader brCo = new BinaryReader(entAct.archivoIndPri);
            Byte[] valor = new byte[entAct.longRegIndPri];

            int posicion = 0;

            valor = brBy.ReadBytes(entAct.longRegIndPri);

            r = 0;

            entAct.archivoIndPri.Seek(0, SeekOrigin.Begin);

            string valoACom = "";

            if (entAct.tipoCvePrima == 'C')
            {
                r = 0;
                valoACom = new string(brCo.ReadChars(entAct.longClvPrim));
                r = 0;
            }
            else
            {
                r = 0;
                valoACom = brCo.ReadInt32().ToString();
                r = 0;
            }

            r = 0;
            string comp = clvPrim.ToString();
            int posCom = entAct.longRegIndPri;
            r = 0;

            if(valoACom != "-1")
            if (entAct.tipoCvePrima == 'E')
            {
                string[] nuev = adaptNumero(valoACom, comp);

                valoACom = nuev.ElementAt(0);
                comp = nuev.ElementAt(1);
            }

            r = 0;
            if (valoACom != "-1")
            {
                r = 0;
                while (string.CompareOrdinal(valoACom, comp) < 0 && valoACom != "-1")
                {
                    r = 0;
                    entAct.archivoIndPri.Seek(posCom, SeekOrigin.Begin);

                    if (entAct.tipoCvePrima == 'C')
                    {
                        valoACom = new string(brCo.ReadChars(entAct.longClvPrim));
                    }
                    else
                    {
                        valoACom = brCo.ReadInt32().ToString();
                    }
                    
                    posicion++;
                    posCom += entAct.longRegIndPri;
                    r = 0;

                    if (valoACom != "-1")
                        if (entAct.tipoCvePrima == 'E')
                        {
                            string[] nuev = adaptNumero(valoACom, comp);

                            valoACom = nuev.ElementAt(0);
                            comp = nuev.ElementAt(1);
                        }
                }
            }
            r = 0;

            entAct.archivoIndPri.Close();
            return posicion;


        }



        public void datosIndiPrim()
        {
            bool bandPrimario = false;
            string idIndPri = "";

            foreach (Atributo a in entAct.listAtrib)
            {
                if (a.dirIndi == -1 && a.tipoIndi == 2)
                {
                    r = 0;
                    idIndPri = BitConverter.ToString(a.id_atri) + ".idx";
                    bandPrimario = true;
                    using (BinaryWriter bw = new BinaryWriter(File.Open(dic.nomArchivo, FileMode.Open)))
                    {
                        bw.Seek((int)a.dirAtri + 52, SeekOrigin.Begin);
                        a.dirIndi = 0;
                        bw.Write(0);
                    }
                    break;
                }
            }
            r = 0;
            if (bandPrimario == true)
            {
                guardaArchivosIndPri();
            }
            else
                leeIndPrim();

            entAct.archivoIndPri.Close();
        }


        #endregion

        public void reacomodaPorPosi(int pos)
        {
            string indP;
            int dir;
            int posOrg = pos;

            r = 0;

            entAct.archivoIndPri.Close();
            entAct.archivoIndPri = File.Open(entAct.archivoIndPri.Name, FileMode.Open);
            BinaryReader br1 = new BinaryReader(entAct.archivoIndPri);
            r = 0;
            entAct.archivoIndPri.Seek(pos * entAct.longRegIndPri, SeekOrigin.Begin);


            if (entAct.tipoCvePrima == 'E')
                indP = br1.ReadInt32().ToString();
            else
                indP = new string(br1.ReadChars(entAct.longClvPrim));


            while (indP != "-1" && char.IsLetterOrDigit(indP.ElementAt(0)))
            {
                entAct.archivoIndPri.Close();
                entAct.archivoIndPri = File.Open(entAct.archivoIndPri.Name, FileMode.Open);
                BinaryReader br2 = new BinaryReader(entAct.archivoIndPri);
                r = 0;
                entAct.archivoIndPri.Seek((pos + 1) * entAct.longRegIndPri, SeekOrigin.Begin);


                if (entAct.tipoCvePrima == 'E')
                    indP = br2.ReadInt32().ToString();
                else
                    indP = new string(br2.ReadChars(entAct.longClvPrim));

                pos++;
                r = 0;
                entAct.archivoIndPri.Close();
            }

            int posAreco = pos - posOrg;


            while (posAreco > 0)
            {
                entAct.archivoIndPri.Close();
                entAct.archivoIndPri = File.Open(entAct.archivoIndPri.Name, FileMode.Open);
                BinaryReader br3 = new BinaryReader(entAct.archivoIndPri);
                r = 0;
                entAct.archivoIndPri.Seek((pos - 1) * entAct.longRegIndPri, SeekOrigin.Begin);


                if (entAct.tipoCvePrima == 'E')
                    indP = br3.ReadInt32().ToString();
                else
                    indP = new string(br3.ReadChars(entAct.longClvPrim));

                dir = br3.ReadInt32();

                r = 0;
                entAct.archivoIndPri.Close();
                using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.archivoIndPri.Name, FileMode.Open)))
                {
                    bw.Seek(pos * entAct.longRegIndPri, SeekOrigin.Begin);

                    if (entAct.tipoCvePrima == 'E')
                        bw.Write(Int32.Parse(indP));
                    else
                        bw.Write(indP.ToCharArray());

                    bw.Write(dir);
                }
                r = 0;

                posAreco--;
                pos--;
                entAct.archivoIndPri.Close();
            }
            entAct.archivoIndPri.Close();
        }

        public bool cohesionDatos()
        {
            bool bandInt = true;

            for(int i = 0; i < entAct.listAtrib.Count && bandInt;i++)
            {
                if (char.IsDigit(RegistroRellDataGrid.Rows[0].Cells[i].Value.ToString().ElementAt(0)))
                    if (entAct.listAtrib.ElementAt(i).tipo != 'E')
                        bandInt = false;

                if (char.IsLetter(RegistroRellDataGrid.Rows[0].Cells[i].Value.ToString().ElementAt(0)))
                    if (entAct.listAtrib.ElementAt(i).tipo != 'C')
                        bandInt = false;
            }

            return bandInt;
            
        }

        public bool verifcaValorPrim()
        {
            int valorClave = Int32.Parse(RegistroRellDataGrid.Rows[0].Cells[0].Value.ToString());//Asmimilo que el valor primario siempre ira al principio

            r = 0;
            foreach(Atributo a in entAct.listAtrib)
            {
                if(a.tipoIndi == 4 || a.tipoIndi == 5)
                {
                    switch(a.tipoIndi)
                    {                       
                        case 4:

                            if (arbol.lisNodo.Count != 0)
                            {
                                Nodo prueba = arbol.encuentraNodoHoja(valorClave);

                                int ind = prueba.K.FindIndex(x => x == valorClave);

                                r = 0;
                                if (ind != -1)
                                {
                                    r = 0;
                                    return true;
                                }
                                else
                                {
                                    r = 0;
                                    return false;
                                }
                            }
                            else
                                return false;
                                
                        case 5:
                            r = 0;
                            if (hash.hayClaveRepetida(valorClave))
                            {
                                r = 0;
                                return true;
                            }
                            else
                            {
                                r = 0;
                                return false;
                            }
                    }
                }

            }


            return true;
        }




        private void GuardaRegistros_Click(object sender, EventArgs e)
        {
            string valor = "";
            bool bandSigue;
            bool bandRegValidoPrim;

            bandSigue = cohesionDatos();
            bandRegValidoPrim = verifcaValorPrim();

            r = 0;

            if (!bandSigue || bandRegValidoPrim)
            {
                r = 0;
                if(!bandSigue)
                MessageBox.Show("Hay datos que no concuerdan");
                else
                    MessageBox.Show("Estas ingresando un registro con valor primario repetido");
            }
            else
            if (entAct != null)
            {
                r = 0;
                entAct.archivoDat = File.Open(entAct.archivoDat.Name, FileMode.Open);
                long lonArDat = entAct.archivoDat.Length;

                entAct.archivoDat.Close();

                if (!bandModAtrBus)
                    RegisInserdataGridView.Rows[RegisInserdataGridView.Rows.Count - 1].Cells[0].Value = (int)lonArDat;
                else
                    RegisInserdataGridView.Rows[RegisInserdataGridView.Rows.Count - 1].Cells[0].Value = (int)dirAuxElimReg;
                r = 0;




                using (BinaryWriter bw = new BinaryWriter(File.Open(BitConverter.ToString(entAct.id_enti) + ".dat", FileMode.Open)))
                {
                    if (bandModAtrBus == true)
                    {
                        r = 0;
                        bw.Seek((int)dirAuxElimReg, SeekOrigin.Begin);
                        bw.Write(dirAuxElimReg);

                    }
                    else
                    {
                        bw.Seek((int)lonArDat, SeekOrigin.Begin);
                        bw.Write(lonArDat);
                    }

                    r = 0;
                    for (int col = 0; col < RegistroRellDataGrid.Rows[0].Cells.Count; col++)
                    {
                        r = 0;
                        Atributo act = entAct.listAtrib.ElementAt(col);
                        r = 0;

                        RegisInserdataGridView.Rows[RegisInserdataGridView.Rows.Count - 1].Cells[col + 1].Value = RegistroRellDataGrid.Rows[0].Cells[col].Value.ToString();

                        valor = RegistroRellDataGrid.Rows[0].Cells[col].Value.ToString();

                        r = 0;
                        if (act.tipo == 'C')
                        {
                            bw.Write(valor.ToCharArray());
                            int contChar = 0;
                            r = 0;

                            while (contChar < act.longitud - valor.Length)
                            {
                                bw.Write('-');
                                contChar++;
                                r = 0;
                            }
                            r = 0;
                            contChar = 0;
                        }
                        else
                        {
                            r = 0;
                            bw.Write(Int32.Parse(valor));
                            r = 0;
                        }

                    }

                    r = 0;
                    if (!bandModAtrBus)
                    {
                        bw.Write((long)-1);

                        r = 0;

                        RegisInserdataGridView.Rows[RegisInserdataGridView.Rows.Count - 1].Cells[entAct.listAtrib.Count + 1].Value = -1;
                    }


                    /*if(bandModAtrBus && !bandAtrArb)///ESTO RECIEN LO AGREGUE
                    {
                        bw.Write((long)dirSigAuxElimReg);
                        r = 0;
                        RegisInserdataGridView.Rows[RegisInserdataGridView.Rows.Count - 1].Cells[entAct.listAtrib.Count + 1].Value = dirSigAuxElimReg;
                    }*/
                   

                    if (!bandAtrBus)
                        compruebaFinal(bw);


                    r = 0;
                }

                if (entAct.dirDat == -1)
                    actualizaPrimerRegEnt(entAct);

                if (bandAtrBus)
                {
                    if (!bandModAtrBus)
                        ordenaPorClv((int)lonArDat, RegisInserdataGridView.Rows.Count - 1);
                    else
                        ordenaPorClv((int)dirAuxElimReg, RegisInserdataGridView.Rows.Count - 1);

                }

                if (bandAtrPri)
                {
                    r = 0;
                    datosIndiPrim();
                }

                if(bandAtrSec)
                {
                    r = 0;
                    datosIndSec();
                }

                if(bandAtrArb)
                {
                    r = 0;
                    

                    if (!bandModAtrBus)
                    {
                        arbol.setEntYDic(entAct, dic);
                        arbol.ObtengRegistro();
                    }
                    else
                        arbol.inserta(Int32.Parse(RegistroRellDataGrid.Rows[0].Cells[0].Value.ToString()), dirAuxElimReg);
                    r = 0;
                    //arbol.inserta();
                    
                }

                if(bandAtrHashEsta)
                {
                    if (!bandModAtrBus)
                    {
                        hash.setEntYDic(entAct, dic);
                        hash.ObtengRegistro();
                    }
                    else
                    {
                        hash.insertaEnCajon(hash.obtenCajonModulo(Int32.Parse(RegistroRellDataGrid.Rows[0].Cells[0].Value.ToString())),Int32.Parse(RegistroRellDataGrid.Rows[0].Cells[0].Value.ToString()), dirAuxElimReg);
                    }
                    
                }


                MessageBox.Show("Tu informacion fue guardada satisfactoriamente");
                limpiaGridRellReg();
            }
            else
                MessageBox.Show("Elige una entidad");

        }


        public string creaCadenaCorrecta(string cad)
        {
            string cadCorrecta = "";
            foreach (char a in cad)
            {
                if (char.IsLetterOrDigit(a) || a == '-' || a == ' ')
                {
                    cadCorrecta += a;
                }
            }
            return cadCorrecta;
        }



        public string[] adaptNumero(string cad1, string cad2)
        {

            int ceroAgre = cad2.Length - cad1.Length;
            r = 0;

            if (ceroAgre < 0)
                for (int i = 0; i < Math.Abs(ceroAgre); i++)
                {
                    cad2 = "0" + cad2;
                }


            for (int i = 0; i < ceroAgre; i++)
            {
                cad1 = "0" + cad1;
            }
            r = 0;
            string[] arrStr = new string[2];
            arrStr[0] = cad1;
            arrStr[1] = cad2;

            return arrStr;
        }


        public void ordenaPorClv(int posUltReg, int posDataGrid)
        {
            string cadCveAct;

            entAct.archivoDat = File.Open(BitConverter.ToString(entAct.id_enti) + ".dat", FileMode.Open);
            BinaryReader br = new BinaryReader(entAct.archivoDat);

            entAct.archivoDat.Seek(posUltReg + entAct.posCveBus, SeekOrigin.Begin);
            if (entAct.tipoCveBus == 'C')
            {
                cadCveAct = new string(br.ReadChars(10));
            }
            else
            {
                int num = br.ReadInt32();
                cadCveAct = num.ToString();
            }

            int posRegCom = (int)entAct.dirDat;
            entAct.archivoDat.Seek(posRegCom + entAct.posCveBus, SeekOrigin.Begin);
            string cadComp = "";
            int dataGridRen = 0;
            bool bandUlt = false;
            bool bandPri = true;

            if (entAct.tipoCveBus == 'C')
            {
                cadComp = new string(br.ReadChars(10));
            }
            else
            {
                int cad = br.ReadInt32();
                cadComp = cad.ToString();
            }

            r = 0;
            cadComp = creaCadenaCorrecta(cadComp);
            cadCveAct = creaCadenaCorrecta(cadCveAct);

            r = 0;


            if (entAct.tipoCveBus == 'E')
            {
                string[] nuev = adaptNumero(cadComp, cadCveAct);

                cadComp = nuev.ElementAt(0);
                cadCveAct = nuev.ElementAt(1);
            }


            while (string.CompareOrdinal(cadComp, cadCveAct) < 0)
            {
                bandPri = false;

                int posRegAct = posRegCom;
                entAct.archivoDat.Seek(posRegCom + entAct.longAtributos - 8, SeekOrigin.Begin);
                posRegCom = posRegCom + entAct.longAtributos - 8;

                int direSigReg = br.ReadInt32();

                r = 0;

                if (direSigReg == -1)//llego al registro final
                {
                    entAct.archivoDat.Close();
                    using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.archivoDat.Name, FileMode.Open)))
                    {
                        RegisInserdataGridView.Rows[dataGridRen].Cells[entAct.listAtrib.Count + 1].Value = posUltReg;
                        RegisInserdataGridView.Rows[posDataGrid].Cells[entAct.listAtrib.Count + 1].Value = direSigReg;

                        r = 0;
                        int p = posRegAct + entAct.longAtributos - 8;
                        r = 0;

                        bw.Seek(posRegAct + entAct.longAtributos - 8, SeekOrigin.Begin);
                        bw.Write(posUltReg);

                        bandUlt = true;

                    }
                    entAct.archivoDat = File.Open(entAct.archivoDat.Name, FileMode.Open);
                    break;
                }
                else//Si no llega hasta el ultimo registro//No necesita confirmacion
                {
                    r = 0;
                    entAct.archivoDat.Seek(direSigReg + entAct.posCveBus, SeekOrigin.Begin);
                    if (entAct.tipoCveBus == 'C')
                    {
                        cadComp = new string(br.ReadChars(10));
                    }
                    else
                    {
                        int cad = br.ReadInt32();
                        cadComp = cad.ToString();
                    }
                    posRegCom = direSigReg;
                    r = 0;
                }

                if (entAct.tipoCveBus == 'E')
                {
                    string[] nuev = adaptNumero(cadComp, cadCveAct);

                    cadComp = nuev.ElementAt(0);
                    cadCveAct = nuev.ElementAt(1);
                }
                dataGridRen++;

            }

            r = 0;
            if (string.CompareOrdinal(cadComp, cadCveAct) == 0 && RegisInserdataGridView.Rows.Count != 1 && !bandUlt && !bandPri)//Si son iguales a otro registro//+
            {
                entAct.archivoDat.Close();
                using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.archivoDat.Name, FileMode.Open)))
                {
                    bw.Seek(Int32.Parse(RegisInserdataGridView.Rows[dataGridRen - 1].Cells[0].Value.ToString()) + entAct.longAtributos - 8, SeekOrigin.Begin);
                    bw.Write(posUltReg);

                    bw.Seek(posUltReg + entAct.longAtributos - 8, SeekOrigin.Begin);
                    bw.Write(posRegCom);

                    RegisInserdataGridView.Rows[dataGridRen - 1].Cells[entAct.listAtrib.Count + 1].Value = posUltReg;
                    RegisInserdataGridView.Rows[posDataGrid].Cells[entAct.listAtrib.Count + 1].Value = posRegCom;

                }
                entAct.archivoDat = File.Open(entAct.archivoDat.Name, FileMode.Open);


            }
            else
            if (bandPri == true && RegisInserdataGridView.Rows.Count != 1)//Va antes del primero//+
            {
                int valEnt = (int)entAct.dirDat;
                entAct.dirDat = posUltReg;
                using (BinaryWriter bw = new BinaryWriter(File.Open(dic.nomArchivo, FileMode.Open)))
                {
                    bw.Seek((int)entAct.dirEnti + 51, SeekOrigin.Begin);
                    bw.Write(posUltReg);

                }
                entAct.archivoDat.Close();
                using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.archivoDat.Name, FileMode.Open)))
                {
                    bw.Seek(Int32.Parse(RegisInserdataGridView.Rows[posDataGrid].Cells[0].Value.ToString()) + entAct.longAtributos - 8, SeekOrigin.Begin);
                    bw.Write(posRegCom);
                }
                RegisInserdataGridView.Rows[posDataGrid].Cells[entAct.listAtrib.Count + 1].Value = posRegCom;
                entAct.archivoDat = File.Open(entAct.archivoDat.Name, FileMode.Open);
                r = 0;

            }
            else
            if (!bandUlt && RegisInserdataGridView.Rows.Count != 1)//Va enmedio//+
            {
                entAct.archivoDat.Close();
                using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.archivoDat.Name, FileMode.Open)))
                {
                    bw.Seek(Int32.Parse(RegisInserdataGridView.Rows[dataGridRen - 1].Cells[0].Value.ToString()) + entAct.longAtributos - 8, SeekOrigin.Begin);
                    bw.Write(posUltReg);
                    bw.Seek(posUltReg + entAct.longAtributos - 8, SeekOrigin.Begin);
                    bw.Write(posRegCom);
                    RegisInserdataGridView.Rows[dataGridRen - 1].Cells[entAct.listAtrib.Count + 1].Value = posUltReg;
                    RegisInserdataGridView.Rows[posDataGrid].Cells[entAct.listAtrib.Count + 1].Value = posRegCom;
                }
                entAct.archivoDat = File.Open(entAct.archivoDat.Name, FileMode.Open);


            }
            dataGridRen = 0;
            entAct.archivoDat.Close();
        }

        public void actualizaPrimerRegEnt(Entidad entAct)
        {
            entAct.archivoDat = File.Open(BitConverter.ToString(entAct.id_enti) + ".dat", FileMode.Open);
            BinaryReader br = new BinaryReader(entAct.archivoDat);
            long valorPri = br.ReadInt32();


            if (valorPri >= 0)
            {
                using (BinaryWriter bw = new BinaryWriter(File.Open(dic.nomArchivo, FileMode.Open)))
                {
                    r = 0;
                    bw.Seek((int)entAct.dirEnti + 51, SeekOrigin.Begin);
                    bw.Write(valorPri);
                    entAct.dirDat = valorPri;
                    r = 0;
                }
            }
            entAct.archivoDat.Close();
        }



        public void compruebaFinal(BinaryWriter bw)
        {
            for (int i = 1; i < RegisInserdataGridView.Rows.Count; i++)
            {
                r = 0;
                if (i != RegisInserdataGridView.Rows.Count)
                {
                    RegisInserdataGridView.Rows[i - 1].Cells[entAct.listAtrib.Count + 1].Value = RegisInserdataGridView.Rows[i].Cells[0].Value;
                    bw.Seek(Int32.Parse(RegisInserdataGridView.Rows[i - 1].Cells[0].Value.ToString()) + entAct.longAtributos - 8, SeekOrigin.Begin);
                    bw.Write(Int32.Parse(RegisInserdataGridView.Rows[i].Cells[0].Value.ToString()));
                }
            }

            r = 0;
        }

        public void limpiaGridRellReg()
        {
            RegistroRellDataGrid.Rows.Clear();
            RegistroRellDataGrid.Columns.Clear();
        }

        public void limpiaGridInsertadosReg()
        {
            RegisInserdataGridView.Rows.Clear();
            RegisInserdataGridView.Columns.Clear();
        }

        private void CambiaEntiReg(object sender, EventArgs e)
        {

            bandAtrBus = false;
            bandAtrPri = false;
            bandAtrSec = false;

            if (!bandModAtrBus)
            {
                limpiaGridRellReg();
            }
            limpiaGridInsertadosReg();
            Object inEn = comboBoxEntiDatos.SelectedItem;
            r = 0;

            if (inEn != null)
            {
                //Columna 1 del grid de registrosRellenados
                DataGridViewTextBoxColumn Columna1 = new DataGridViewTextBoxColumn();
                Columna1.HeaderText = "Dir_Registro";
                RegisInserdataGridView.Columns.Add(Columna1);


                Entidad entReg = dic.listEntidad.Find(x => x.nombre == inEn.ToString());
                entAct = entReg;

                posAtrBus = 0;
                foreach (Atributo a in entAct.listAtrib)
                {
                    r = 0;
                    if (a.tipoIndi == 1)
                    {
                        r = 0;
                        //MessageBox.Show("El atributo de busqueda es " + a.nombre);
                        bandAtrBus = true;
                        entAct.tipoCveBus = a.tipo;
                        indColIndPri++;
                        break;
                    }
                    else
                        posAtrBus += a.longitud;
                }

                entAct.indColIndPrim = indColIndPri;
                indColIndPri = 0;
                entAct.posCveBus = posAtrBus + 8;


                /*
                 * Aqui me busca si hay clave primaria
                 * 
                 * */
                int posAtrPri = 0;

                foreach (Atributo a in entAct.listAtrib)
                {
                    r = 0;
                    if (a.tipoIndi == 2)
                    {
                        r = 0;
                        //MessageBox.Show("El atributo primario es " + a.nombre);
                        bandAtrPri = true;
                        entAct.tipoCvePrima = a.tipo;
                        entAct.longClvPrim = a.longitud;

                        if (a.dirIndi == -1)
                        {
                            entAct.archivoIndPri = new FileStream(BitConverter.ToString(a.id_atri) + ".idx", FileMode.Create);
                            BinaryWriter bw = new BinaryWriter(entAct.archivoIndPri);

                            Byte[] bloque = new Byte[2048];
                            for (int i = 0; i < 2048; i++)
                            {
                                bloque[i] = 0xFF;
                            }
                            bw.Write(bloque);
                            entAct.archivoIndPri.Close();
                        }
                        else
                        {
                            entAct.archivoIndPri = File.Open(BitConverter.ToString(a.id_atri) + ".idx", FileMode.Open);
                            entAct.archivoIndPri.Close();
                        }


                        entAct.capacidadRegIndPri = 2048 / entAct.longClvPrim + 8;
                        entAct.longRegIndPri = entAct.longClvPrim + 8;

                        break;
                    }
                    else
                        posAtrPri += a.longitud;
                }
                entAct.posCvePrima = posAtrPri + 8;


                /*Aqui me obtiene si tiene inidices secundarios*/

                int posAtrSec = 0;
                entAct.lisIndSec.Clear();
                entAct.contIndSec = 0;
                

                foreach (Atributo a in entAct.listAtrib)
                {
                    r = 0;
                    if (a.tipoIndi == 3)
                    {
                        r = 0;
                        //MessageBox.Show("El atributo Secundario es " + a.nombre);
                        bandAtrSec = true;

                        if (a.dirIndi == -1)
                        {
                            r = 0;
                            IndiceSecundario indSec = new IndiceSecundario(a.tipo,a.longitud, new FileStream(BitConverter.ToString(a.id_atri) + ".idx", FileMode.Create), 2048/ (a.longitud+8),a.longitud+8, entAct.contIndSec,0, posAtrSec+8);

                            entAct.lisIndSec.Add(indSec);
                     
                        }
                        else
                        {
                            r = 0;
                           
                            FileStream archSec = File.Open(BitConverter.ToString(a.id_atri) + ".idx", FileMode.Open);

                            IndiceSecundario indSec = new IndiceSecundario(a.tipo, a.longitud, archSec , 2048 / (a.longitud + 8), a.longitud + 8, entAct.contIndSec, 1, posAtrSec+8);
                            entAct.lisIndSec.Add(indSec);
                            archSec.Close();

                        }



                        posAtrSec += a.longitud;
                        entAct.contIndSec++;

                    }
                    else
                        posAtrSec += a.longitud;
                }

                /*
                 * Encuentra si hay un indice de arbol primario
                 */

                int posIndArb = 0;

                foreach (Atributo a in entAct.listAtrib)
                {
                    if (a.tipoIndi == 4)
                    {
                        MessageBox.Show("Indice de arbol B+ " + a.nombre);
                        bandAtrArb = true;
                        

                        if (a.dirIndi == -1)
                        {
                            r = 0;
                            arbol = new ArbolB_Primario(new FileStream(BitConverter.ToString(a.id_atri) + ".idx", FileMode.Create), false, posIndArb+8);
                            
                            r = 0;
                        }
                        else
                        {
                            r = 0;
                            FileStream archArb = File.Open(BitConverter.ToString(a.id_atri) + ".idx", FileMode.Open);
                            arbol = new ArbolB_Primario(archArb, true, posIndArb+8);


                            archArb.Close();
                        }
                    }
                    else
                        posIndArb += a.longitud;
                }

                /*
                 * Encuentra si hay un indice Hash
                 * */

               int posHashEsta = 0;

                foreach (Atributo a in entAct.listAtrib)
                {
                    if (a.tipoIndi == 5)
                    {
                        MessageBox.Show("Indice de Hash Estatico" + a.nombre);
                        bandAtrHashEsta = true;
                        
                        if (a.dirIndi == -1)
                        {
                            r = 0;

                            //hash.archivHash.Close();
                            hash = new HashEstatico(new FileStream(BitConverter.ToString(a.id_atri) + ".idx", FileMode.Create), posHashEsta+8, false);
                            r = 0;

                        }
                        else
                        {
                            r = 0;
                            FileStream archHash = File.Open(BitConverter.ToString(a.id_atri) + ".idx", FileMode.Open);
                            hash = new HashEstatico(archHash, posHashEsta + 8, true);
                            archHash.Close();
                        }

                    }
                    else
                        posHashEsta += a.longitud;
                }

                
                /*
                    * Obtiene la longitud de los registros
                    * */
                r = 0;
                foreach (Atributo a in entReg.listAtrib)
                {
                    lonRegisAct += a.longitud;
                }
                entAct.longAtributos = lonRegisAct;
                r = 0;


                foreach (Atributo a in entReg.listAtrib)
                {
                    //Columna 1 del grid de registros por rellenar
                    if (!bandModAtrBus)
                    {
                        DataGridViewTextBoxColumn Columna2 = new DataGridViewTextBoxColumn();
                        Columna2.HeaderText = a.nombre;
                        RegistroRellDataGrid.Columns.Add(Columna2);
                    }

                    //Columnas del grid de registrosRellenados
                    DataGridViewTextBoxColumn Columna3 = new DataGridViewTextBoxColumn();
                    Columna3.HeaderText = a.nombre;
                    RegisInserdataGridView.Columns.Add(Columna3);

                }

                if (!bandModAtrBus)
                    RegistroRellDataGrid.Rows.Add();



                //Columna ultima del grid de registrosRellenados
                DataGridViewTextBoxColumn Columna4 = new DataGridViewTextBoxColumn();
                Columna4.HeaderText = "Dir_Sig_Registro";
                RegisInserdataGridView.Columns.Add(Columna4);



                if (entReg.dirDat == -1)
                {
                    r = 0;
                    creaArchivosDat(entReg);
                }
                else
                {
                    r = 0;
                    //MessageBox.Show("Ya tiene datos");
                    agregaRegisExistentes(entReg);

                }
            }
            else
                MessageBox.Show("Escoge una entidad");

            lonRegisAct = 0;
        }


        public void agregaRegisExistentes(Entidad ent)
        {
            r = 0;
            int com = 0;
            bool bandSigReg = true;
            int conRen = 0;
            int dirReg = (int)ent.dirDat, dirRegAct, dirSigReg;
            //if()


            ent.archivoDat = File.Open(BitConverter.ToString(ent.id_enti) + ".dat", FileMode.Open);////////
            BinaryReader br = new BinaryReader(ent.archivoDat);

            int contAtrLon = 0;

            foreach (Atributo a in entAct.listAtrib)
            {
                contAtrLon += a.longitud;
            }
            entAct.longAtributos = contAtrLon + 16;

            r = 0;
            while (bandSigReg)
            {
                r = 0;
                RegisInserdataGridView.Rows.Add();
                r = 0;

                if (conRen == 0)
                    dirReg = (int)entAct.dirDat;
                else
                    dirReg = br.ReadInt32();

                dirRegAct = dirReg;
                r = 0;
                RegisInserdataGridView.Rows[conRen].Cells[0].Value = dirReg;

                dirReg = dirReg + 8;

                for (int i = 0; i < ent.listAtrib.Count; i++)
                {
                    ent.archivoDat.Seek(dirReg, SeekOrigin.Begin);
                    r = 0;
                    Atributo atr = ent.listAtrib.ElementAt(i);
                    r = 0;
                    switch (atr.tipo)
                    {
                        case 'C':
                            foreach (char a in br.ReadChars(atr.longitud))
                            {
                                if (char.IsDigit(a) || char.IsLetter(a) || a == ' ')
                                {
                                    RegisInserdataGridView.Rows[conRen].Cells[i + 1].Value += a.ToString();
                                    r = 0;
                                }
                            }
                            r = 0;
                            break;
                        case 'E':

                            int valor = br.ReadInt32();
                            RegisInserdataGridView.Rows[conRen].Cells[i + 1].Value = valor;
                            r = 0;
                            break;

                    }

                    dirReg += atr.longitud;
                    r = 0;
                    ent.archivoDat.Seek(dirReg, SeekOrigin.Begin);

                    if (br.ReadInt32() == -1)
                    {
                        r = 0;
                        bandSigReg = false;
                    }

                }
                ent.archivoDat.Seek(dirReg, SeekOrigin.Begin);

                dirSigReg = br.ReadInt32();
                RegisInserdataGridView.Rows[conRen].Cells[ent.listAtrib.Count + 1].Value = dirSigReg;
                r = 0;

                if (bandSigReg)
                {
                    ent.archivoDat.Seek(dirSigReg, SeekOrigin.Begin);
                    r = 0;
                }

                r = 0;
                conRen++;
            }

            ent.archivoDat.Close();
        }

        private void RegisInserdataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //PROBAR
            if (bandModi)
            {
                for (int i = 0; i < entAct.listAtrib.Count; i++)
                {
                    RegistroRellDataGrid.Rows[0].Cells[i].Value = RegisInserdataGridView.CurrentRow.Cells[i + 1].Value;
                }
            }

            bandModi = false;
        }

        private void EliminarReg_Click(object sender, EventArgs e)
        {
            bandElim = true;
            MessageBox.Show("Selecciona una celda del registro a eliminar");
        }



        private void RegisInserdataGridView_CellClick(object sender, DataGridViewCellEventArgs e)//Elimina Registro
        {
            int indFilEli;

            if (bandElim == true)
            {

                indFilEli = RegisInserdataGridView.CurrentRow.Index;

                r = 0;

                using (BinaryWriter bw = new BinaryWriter(File.Open(BitConverter.ToString(entAct.id_enti) + ".dat", FileMode.Open)))
                {
                    r = 0;

                    if ((int)RegisInserdataGridView.Rows[indFilEli].Cells[0].Value == entAct.dirDat)//Es el primer registro//Si es un unico registro va a mandar un mensaje
                    {
                        r = 0;
                        entAct.dirDat = (int)RegisInserdataGridView.Rows[indFilEli + 1].Cells[0].Value;
                        using (BinaryWriter bwD = new BinaryWriter(File.Open(dic.nomArchivo, FileMode.Open)))
                        {
                            bwD.Seek((int)entAct.dirEnti + 51, SeekOrigin.Begin);
                            bwD.Write(entAct.dirDat);
                        }


                    }
                    else
                    if ((int)RegisInserdataGridView.Rows[indFilEli].Cells[entAct.listAtrib.Count + 1].Value == -1)//Es el ultimo registro
                    {
                        r = 0;
                        int algo = (int)RegisInserdataGridView.Rows[indFilEli - 1].Cells[entAct.listAtrib.Count + 1].Value - 8;

                        bw.Seek((int)RegisInserdataGridView.Rows[indFilEli - 1].Cells[0].Value + entAct.longAtributos - 8, SeekOrigin.Begin);
                        bw.Write(-1);
                    }
                    else//Es cualquier registro de en medio
                    {
                        bw.Seek((int)RegisInserdataGridView.Rows[indFilEli - 1].Cells[0].Value + entAct.longAtributos - 8, SeekOrigin.Begin);
                        bw.Write((int)RegisInserdataGridView.Rows[indFilEli + 1].Cells[0].Value);
                    }

                    r = 0;
                }

                r = 0;

                if (bandAtrPri)
                {
                    eliminaRegClvPrim((int)RegisInserdataGridView.Rows[indFilEli].Cells[0].Value);
                }

                if(bandAtrSec)
                {
                    List<int> lisPosIndSec = new List<int>();
                    for (int i = 0; i < entAct.listAtrib.Count; i++)
                    {
                        if(entAct.listAtrib.ElementAt(i).tipoIndi == 3)
                        {
                            lisPosIndSec.Add(i + 1);
                        }
                    }

                    int j = 0;

                    
                    foreach (IndiceSecundario a in entAct.lisIndSec)
                    {
                        r = 0;
                        eliminaBloquSec(RegisInserdataGridView.Rows[indFilEli].Cells[lisPosIndSec.ElementAt(j)].Value.ToString(), (int)RegisInserdataGridView.Rows[indFilEli].Cells[0].Value, a.contIndSec);
                        j++;
                    }
                      
                    
                }

                if(bandAtrArb)
                {
                    arbol.borrar((int)RegisInserdataGridView.Rows[indFilEli].Cells[1].Value, (long)(int)RegisInserdataGridView.Rows[indFilEli].Cells[0].Value);
                }

                if(bandAtrHashEsta)
                {
                    r = 0;
                    hash.elimina((int)RegisInserdataGridView.Rows[indFilEli].Cells[1].Value);
                }

                limpiaGridInsertadosReg();

                bandElim = false;
            }


            r = 0;
        }

        private void Datos_FormClosing(object sender, FormClosingEventArgs e)
        {
            lonRegisAct = 0;
            AplicaCambio.Visible = false;
            limpiaGridRellReg();
            limpiaGridInsertadosReg();
            comboBoxEntiDatos.Items.Clear();
        }

        private void ModificaRegistro_Click(object sender, EventArgs e)
        {
            bandModi = true;
            MessageBox.Show("Selecciona el renglon a modificar");
            AplicaCambio.Visible = true;

        }

        private void AplicaCambio_Click(object sender, EventArgs e)
        {
            
            int posI = 0;
            int contChar = 0;

            if (!verifcaValorPrim())
            {

                using (BinaryWriter bw = new BinaryWriter(File.Open(BitConverter.ToString(entAct.id_enti) + ".dat", FileMode.Open)))
                {

                    int longAcumReg = Int32.Parse(RegisInserdataGridView.CurrentRow.Cells[0].Value.ToString()) + 8;
                    r = 0;
                    bw.Seek(longAcumReg, SeekOrigin.Begin);

                    r = 0;
                    for (int i = 0; i < entAct.listAtrib.Count; i++)
                    {
                        r = 0;
                        if (entAct.listAtrib.ElementAt(i).tipo == 'C')
                        {
                            bw.Seek(longAcumReg, SeekOrigin.Begin);
                            r = 0;
                            char[] valCad = RegistroRellDataGrid.Rows[0].Cells[i].Value.ToString().ToCharArray();
                            r = 0;
                            bw.Write(valCad);
                            while (contChar < entAct.listAtrib.ElementAt(i).longitud - RegistroRellDataGrid.Rows[0].Cells[i].Value.ToString().Length)
                            {
                                bw.Write('-');
                                contChar++;
                                r = 0;
                            }
                            r = 0;
                            contChar = 0;
                            longAcumReg += entAct.listAtrib.ElementAt(i).longitud;
                        }
                        else
                        {
                            bw.Seek(longAcumReg, SeekOrigin.Begin);

                            posI = Int32.Parse(RegistroRellDataGrid.Rows[0].Cells[i].Value.ToString());
                            r = 0;
                            bw.Write(Int32.Parse(RegistroRellDataGrid.Rows[0].Cells[i].Value.ToString()));

                            longAcumReg += 4;
                            r = 0;

                        }

                        r = 0;

                    }
                    longAcumReg = 0;

                }

                r = 0;

                if (bandAtrBus == true)//Si hay clave de busqueda
                {
                    r = 0;
                    bandModAtrBus = true;
                    dirAuxElimReg = Int32.Parse(RegisInserdataGridView.CurrentRow.Cells[0].Value.ToString());

                    dirSigAuxElimReg = Int32.Parse(RegisInserdataGridView.CurrentRow.Cells[entAct.listAtrib.Count + 1].Value.ToString());
                    r = 0;
                    EliminarReg_Click(this, null);//Elimina Registro
                    RegisInserdataGridView_CellClick(this, null);//Elimina Registro

                    CambiaEntiReg(this, null);//ayuda a inserta Reg
                    GuardaRegistros_Click(this, null);//ayuda a insertar Reg
                    bandModAtrBus = false;


                }
                else// Si no hay clave de busqueda
                {

                    bandModAtrBus = true;
                    dirAuxElimReg = Int32.Parse(RegisInserdataGridView.CurrentRow.Cells[0].Value.ToString());
                    dirSigAuxElimReg = Int32.Parse(RegisInserdataGridView.CurrentRow.Cells[entAct.listAtrib.Count + 1].Value.ToString());
                    //r = 0;
                    //EliminarReg_Click(this, null);//Elimina Registro



                    //RegisInserdataGridView_CellClick(this, null);//Elimina Registro
                    if (bandAtrPri)
                    {
                        eliminaRegClvPrim((int)dirAuxElimReg);
                        guardaArchivosIndPri();
                    }

                    if (bandAtrSec)
                    {

                        List<int> lisPosIndSec = new List<int>();
                        for (int i = 0; i < entAct.listAtrib.Count; i++)
                        {
                            if (entAct.listAtrib.ElementAt(i).tipoIndi == 3)
                            {
                                lisPosIndSec.Add(i + 1);
                            }
                        }

                        int j = 0;
                        ////(string clavpri, int dirEli, int contIndSec)
                        foreach (IndiceSecundario a in entAct.lisIndSec)
                        {
                            r = 0;
                            //eliminaBloquSec(RegisInserdataGridView.CurrentRow.Cells[lisPosIndSec.ElementAt(j)].Value.ToString(), (int)RegisInserdataGridView.CurrentRow.Cells[0].Value, a.contIndSec);
                            eliminaBloquSec(RegisInserdataGridView.CurrentRow.Cells[lisPosIndSec.ElementAt(j)].Value.ToString(), (int)RegisInserdataGridView.CurrentRow.Cells[0].Value, a.contIndSec);
                            j++;
                        }
                        guardaArchivosIndSec();
                    }


                    if (bandAtrArb)
                    {
                        r = 0;
                        arbol.borrar(Int32.Parse(RegisInserdataGridView.CurrentRow.Cells[1].Value.ToString()), (int)dirAuxElimReg);
                        arbol.inserta(Int32.Parse(RegistroRellDataGrid.Rows[0].Cells[0].Value.ToString()), (int)dirAuxElimReg);
                    }

                    if (bandAtrHashEsta)
                    {
                        int cajon = hash.obtenCajonModulo(Int32.Parse(RegistroRellDataGrid.CurrentRow.Cells[0].Value.ToString()));

                        r = 0;
                        hash.elimina(Int32.Parse(RegisInserdataGridView.CurrentRow.Cells[1].Value.ToString()));
                        hash.insertaEnCajon(cajon, Int32.Parse(RegistroRellDataGrid.CurrentRow.Cells[0].Value.ToString()), (int)dirAuxElimReg);
                    }


                    for (int i = 0; i < entAct.listAtrib.Count; i++)
                    {
                        RegisInserdataGridView.CurrentRow.Cells[i + 1].Value = RegistroRellDataGrid.Rows[0].Cells[i].Value;
                    }

                    bandModAtrBus = false;




                }

                AplicaCambio.Visible = false;
            }
            else
                MessageBox.Show("Tu valor a modificar esta repetido\nVuelve a intentar");
        }



        private void RegisInserdataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            r = 0;
        }

        private void venArbolPri_Click(object sender, EventArgs e)
        {
            vAR = new ventanaArbol();
            if(entAct != null)
            {
                //arbol.actualizaListaNodo();
                vAR.setListaNodo(arbol.lisNodo);
                vAR.archArbol = arbol.archArb;
                vAR.agregaValoresTabla();
                
                vAR.Show();
                
                

            }
            //vAR.Show();

        }

        private void HashEstatico_Click(object sender, EventArgs e)
        {

            vHE = new ventanaHashEsta();
            if (entAct != null)
            {

                r = 0;
                vHE.setDirectorio(hash.DirectorioHash);
                vHE.archHashEsta = hash.archivHash;
                vHE.agregaValoresTabla();
                vHE.Show();
            }
        }
    }
}
