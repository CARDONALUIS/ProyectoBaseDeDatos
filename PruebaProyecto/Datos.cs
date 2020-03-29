using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
        VentanaIndPrim vIP = new VentanaIndPrim();
        int indColIndPri = 0;
        long dirAuxElimReg = 0;
        long dirSigAuxElimReg = 0;





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

            r = 0;

            //entAct.archivoIndPri.Close();

            while (findEnd != "-1")
            {
                entAct.archivoIndPri.Close();
                using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.archivoIndPri.Name, FileMode.Open)))
                {
                    //entAct.archivoIndPri = File.Open(entAct.archivoIndPri.Name, FileMode.Open);
                    bw.Seek(posIndP - entAct.longRegIndPri, SeekOrigin.Begin);
                    //entAct.archivoIndPri.Seek(posIndP-entAct.longRegIndPri, SeekOrigin.Begin);

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
                    findEnd = br2.ReadChars(entAct.longClvPrim).ToString();

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
                //if (entAct.tipoCvePrima == 'C')
                //{
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

                //comp.ToString() = new string(br.ReadChars(entAct.longClvPrim).ToString().ToCharArray());
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
                //}
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

        public void reacomodaPorPosi(int pos)
        {
            string indP, indP2;
            int dir, dir2;
            int posOrg = pos;
            //pos++;

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




            /*char al = indP.ElementAt(0);

            r = 0;

            bool es = char.IsLetterOrDigit(indP.ElementAt(0));*/
            r = 0;

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
            r = 0;
            int posAreco = pos - posOrg;
            r = 0;

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
                    //entAct.archivoIndPri = File.Open(entAct.archivoIndPri.Name, FileMode.Open);
                    bw.Seek(pos * entAct.longRegIndPri, SeekOrigin.Begin);
                    //entAct.archivoIndPri.Seek(posIndP-entAct.longRegIndPri, SeekOrigin.Begin);

                    if (entAct.tipoCvePrima == 'E')
                        bw.Write(Int32.Parse(indP));
                    else
                        bw.Write(indP.ToCharArray());
                    // bw.Write(indP);

                    bw.Write(dir);
                }
                r = 0;

                //posOrg++;
                posAreco--;
                pos--;
                //posOrg++;
                entAct.archivoIndPri.Close();
            }
            entAct.archivoIndPri.Close();
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
            //indP.clv_prim = br.ReadChars(entAct.longClvPrim).ToString();

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
            //indP.clv_prim = valorInd;
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
                //a.dirIndi = 0;
                //bw.Write();
                r = 0;
                if (entAct.tipoCvePrima == 'C')
                {
                    //string algo = indP.clv_prim.ToString().ToCharArray();
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

                //bw.Seek(posAGuardReg+entAct.longClvPrim, SeekOrigin.Begin);
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
                    //r = 0;
                    posicion++;
                    posCom += entAct.longRegIndPri;
                    r = 0;

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


        private void GuardaRegistros_Click(object sender, EventArgs e)
        {
            string valor = "";

            if (entAct != null)
            {
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
                        //RegisInserdataGridView.Rows[RegisInserdataGridView.Rows.Count - 1].Cells[0].Value = (int)dirAuxElimReg;

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

                        //if (!bandModAtrBus)
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
                    datosIndiPrim();
                }

                MessageBox.Show("Tu registros fueron guardados satisfactoriamente");
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
                if (char.IsLetterOrDigit(a) || a == '-')
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
            //List<string> lis = new List<string>();
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

                        //Ver si no altera
                        /*if (bandUltModiAtr)
                        {
                            RegisInserdataGridView.Rows[posDataGrid + 1].Cells[entAct.listAtrib.Count + 1].Value = posUltReg;
                            bw.Seek(posUltReg + entAct.longAtributos - 8, SeekOrigin.Begin);


                            bw.Write(direSigReg);
                            bandUltModiAtr = false;
                        }*/

                        bandUlt = true;

                    }
                    entAct.archivoDat = File.Open(entAct.archivoDat.Name, FileMode.Open);
                    break;
                }
                else//Si no llega hasta el ultimo registro//No necesita confirmacion
                {
                    /*r = 0;
                    if(bandModAtrBus && direSigReg == posUltReg)
                    {
                        r = 0;

                        entAct.archivoDat.Close();
                        using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.archivoDat.Name, FileMode.Open)))
                        {
                            bw.Seek(Int32.Parse(RegisInserdataGridView.Rows[dataGridRen].Cells[0].Value.ToString()) + entAct.longAtributos - 8, SeekOrigin.Begin);
                            bw.Write(Int32.Parse(RegisInserdataGridView.Rows[posDataGrid + 1].Cells[0].Value.ToString()));

                            RegisInserdataGridView.Rows[dataGridRen].Cells[entAct.listAtrib.Count + 1].Value = RegisInserdataGridView.Rows[posDataGrid + 1].Cells[0].Value;
                            //posUltReg =
                        }

                        entAct.archivoDat = File.Open(entAct.archivoDat.Name, FileMode.Open);
                        br = new BinaryReader(entAct.archivoDat);
                        direSigReg += entAct.longAtributos;

                        bandUltModiAtr = true;

                    }

                    r = 0;
                    entAct.archivoDat.Seek(direSigReg + entAct.posCveBus, SeekOrigin.Begin);
                    if (entAct.tipoCveBus == 'C')
                    {

                        cadComp = new string(br.ReadChars(10));
                        r = 0;
                    }
                    else
                    {
                        int cad = br.ReadInt32();
                        cadComp = cad.ToString();
                    }
                    posRegCom = direSigReg;*/
                    r = 0;
                    entAct.archivoDat.Seek(direSigReg + entAct.posCveBus, SeekOrigin.Begin);
                    //cadComp = new string(br.ReadChars(10));
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

                    /*if (bandModAtrBus)
                    {
                        bw.Seek(Int32.Parse(RegisInserdataGridView.Rows[dataGridRen].Cells[0].Value.ToString()) + entAct.longAtributos - 8, SeekOrigin.Begin);
                        bw.Write(Int32.Parse(RegisInserdataGridView.Rows[posDataGrid+1].Cells[0].Value.ToString()));

                        RegisInserdataGridView.Rows[dataGridRen].Cells[entAct.listAtrib.Count + 1].Value = RegisInserdataGridView.Rows[posDataGrid+1].Cells[0].Value;

                        bandModAtrBus = false;
                    }*/

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
                    /*if (bandModAtrBus)
                    {
                        bw.Seek(Int32.Parse(RegisInserdataGridView.Rows[posDataGrid-1].Cells[0].Value.ToString()) + entAct.longAtributos - 8, SeekOrigin.Begin);
                        bw.Write(Int32.Parse(RegisInserdataGridView.Rows[posDataGrid+1].Cells[0].Value.ToString()));
                        RegisInserdataGridView.Rows[posDataGrid-1].Cells[entAct.listAtrib.Count + 1].Value = RegisInserdataGridView.Rows[posDataGrid].Cells[entAct.listAtrib.Count + 1].Value;
                        bandModAtrBus = false;
                    }*/

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
                    /*if (bandModAtrBus)
                    {
                        //Este es cambio por modifiAtri
                        bw.Seek(posRegCom + entAct.longAtributos - 8, SeekOrigin.Begin);
                        bw.Write(Int32.Parse(RegisInserdataGridView.Rows[posDataGrid + 1].Cells[0].Value.ToString()));
                        RegisInserdataGridView.Rows[dataGridRen].Cells[entAct.listAtrib.Count + 1].Value = Int32.Parse(RegisInserdataGridView.Rows[posDataGrid + 1].Cells[0].Value.ToString());
                        bandModAtrBus = false;
                    }*/


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
                        MessageBox.Show("El atributo de busqueda es " + a.nombre);
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
                        MessageBox.Show("El atributo primario es " + a.nombre);
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

                foreach (Atributo a in entAct.listAtrib)
                {
                    r = 0;
                    if (a.tipoIndi == 3)
                    {
                        r = 0;
                        MessageBox.Show("El atributo Secundario es " + a.nombre);
                        bandAtrSec = true;
                        


                        if (a.dirIndi == -1)
                        {
                            //entAct.listipoCveSec.Add(a.tipo);
                            //entAct.lislongClvSec.Add(a.longitud);

                            //entAct.lisArchIndSec.Add(new FileStream(BitConverter.ToString(a.id_atri) + ".idx", FileMode.Create));
                            //BinaryWriter bw = new BinaryWriter(entAct.archivoIndPri);

                            /*
                            Byte[] bloque = new Byte[2048];
                            for (int i = 0; i < 2048; i++)
                            {
                                bloque[i] = 0xFF;
                            }
                            bw.Write(bloque);*/


                            //entAct.archivoIndPri.Close();

                            //entAct.lisCapacidadRegIndSec.Add(2048 / entAct.lislongClvSec.ElementAt(entAct.contIndSec) + 8);
                            //entAct.lisLongRegIndSec.Add(entAct.lisLongRegIndSec.ElementAt(entAct.contIndSec) + 8);

                            IndiceSecundario indSec = new IndiceSecundario(a.tipo,a.longitud, new FileStream(BitConverter.ToString(a.id_atri) + ".idx", FileMode.Create), 2048/ (a.longitud+8),a.longitud+8, entAct.contIndSec,0, posAtrSec+8);

                            entAct.lisIndSec.Add(indSec);
                        }
                        else
                        {
                            IndiceSecundario indSec = new IndiceSecundario(a.tipo, a.longitud, new FileStream(BitConverter.ToString(a.id_atri) + ".idx", FileMode.Create), 2048 / (a.longitud + 8), a.longitud + 8, entAct.contIndSec, 1, posAtrSec+8);

                            entAct.lisIndSec.Add(indSec);
                            /*entAct.archivoIndPri = File.Open(BitConverter.ToString(a.id_atri) + ".idx", FileMode.Open);
                            entAct.archivoIndPri.Close();*/
                        }

                        


                        entAct.contIndSec++;

                        //lisPosAtrSec.Add(posAtrSec + 8);

                    }
                    else
                        posAtrSec += a.longitud;
                }

                /*foreach (int a in lisPosAtrSec)
                {
                    //entAct.p = posAtrPri + 8;
                    entAct.lisposCveSec.Add(a);
                }*/


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
                    MessageBox.Show("Ya tiene datos");
                    agregaRegisExistentes(entReg);
                    //RegisInserdataGridView.Sort(RegisInserdataGridView.Columns[posAtrBus], ListSortDirection.Ascending);

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
                //ent.archivoDat.Seek(dirReg, SeekOrigin.Begin);
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
                    //com = br.ReadInt32();
                    r = 0;
                    Atributo atr = ent.listAtrib.ElementAt(i);
                    r = 0;
                    switch (atr.tipo)
                    {
                        case 'C':
                            foreach (char a in br.ReadChars(atr.longitud))
                            {
                                if (char.IsDigit(a) || char.IsLetter(a))
                                {
                                    RegisInserdataGridView.Rows[conRen].Cells[i + 1].Value += a.ToString();
                                    r = 0;
                                }
                            }
                            //String prueba = RegisInserdataGridView.Rows[conRen].Cells[i + 1].Value.ToString();
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
                        //compruebaFinal(bw);
                        bandSigReg = false;
                    }

                }
                //dirReg = dirReg + 8;
                ent.archivoDat.Seek(dirReg, SeekOrigin.Begin);

                dirSigReg = br.ReadInt32();
                RegisInserdataGridView.Rows[conRen].Cells[ent.listAtrib.Count + 1].Value = dirSigReg;
                r = 0;


                //ent.listReg.Add(new Registro(dirRegAct, dirSigReg));

                if (bandSigReg)
                {
                    ent.archivoDat.Seek(dirSigReg, SeekOrigin.Begin);
                    r = 0;
                }

                r = 0;
                conRen++;


            }
            //



            ent.archivoDat.Close();
            //RegisInserdataGridView.Sort(RegisInserdataGridView.Columns[posAtrBus], ListSortDirection.Ascending);
        }

        private void RegisInserdataGridView_SelectionChanged(object sender, EventArgs e)
        {
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
        }



        private void RegisInserdataGridView_CellClick(object sender, DataGridViewCellEventArgs e)//Elimina Registro
        {
            int indFilEli;

            if (bandElim == true)
            {
                //MessageBox.Show("Voy a cambiar el valor de " + RegisInserdataGridView.Rows[algo - 1].Cells[entAct.listAtrib.Count + 1].Value+ " por "+RegisInserdataGridView.Rows[algo+1].Cells[0].Value);
                //MessageBox.Show("Vas a eliminar el renglon " + algo+" valor: "+ RegisInserdataGridView.Rows[algo].Cells[1].Value);

                indFilEli = RegisInserdataGridView.CurrentRow.Index;

                //Prueba de direccion 
                //int dir = entAct.leerDatoReg((int)RegisInserdataGridView.Rows[indFilEli - 1].Cells[entAct.listAtrib.Count + 1].Value - 8);
                r = 0;

                using (BinaryWriter bw = new BinaryWriter(File.Open(BitConverter.ToString(entAct.id_enti) + ".dat", FileMode.Open)))
                {
                    r = 0;

                    if ((int)RegisInserdataGridView.Rows[indFilEli].Cells[0].Value == entAct.dirDat)//Es el primer registro
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

                        bw.Seek((int)RegisInserdataGridView.Rows[indFilEli - 1].Cells[entAct.listAtrib.Count + 1].Value - 8, SeekOrigin.Begin);
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
            for (int i = 0; i < entAct.listAtrib.Count; i++)
            {
                RegisInserdataGridView.CurrentRow.Cells[i + 1].Value = RegistroRellDataGrid.Rows[0].Cells[i].Value;
            }

            int posI = 0;
            int contChar = 0;

            //int dirRegaModi = 

            //RegisInserdataGridView.Sort(RegisInserdataGridView.Columns[posAtrBus], ListSortDirection.Ascending);

            using (BinaryWriter bw = new BinaryWriter(File.Open(BitConverter.ToString(entAct.id_enti) + ".dat", FileMode.Open)))
            {
                // bw.Seek(, SeekOrigin.Begin);

                int longAcumReg = Int32.Parse(RegisInserdataGridView.CurrentRow.Cells[0].Value.ToString()) + 8;
                r = 0;
                bw.Seek(longAcumReg, SeekOrigin.Begin);

                r = 0;
                for (int i = 0; i < entAct.listAtrib.Count; i++)
                {
                    r = 0;

                    /*if(entAct.listAtrib.ElementAt(i).tipoIndi == 2)
                    {
                        modifClvPrim(longAcumReg-8);
                        longAcumReg += entAct.longAtributos;
                    }*/

                    if (entAct.listAtrib.ElementAt(i).tipo == 'C')
                    {
                        bw.Seek(longAcumReg, SeekOrigin.Begin);
                        //posS = RegisInserdataGridView.CurrentRow.Cells[i + 1].Value.ToString();                 
                        r = 0;
                        char[] valCad = RegisInserdataGridView.CurrentRow.Cells[i + 1].Value.ToString().ToCharArray();
                        r = 0;
                        //bw.Seek(longAcumReg+ entAct.listAtrib.ElementAt(i).longitud, SeekOrigin.Begin);
                        //char[] val = valCad.ToCharArray();
                        bw.Write(valCad);
                        while (contChar < entAct.listAtrib.ElementAt(i).longitud - RegisInserdataGridView.CurrentRow.Cells[i + 1].Value.ToString().Length)
                        {
                            bw.Write('-');
                            contChar++;
                            r = 0;
                        }
                        r = 0;
                        contChar = 0;
                        longAcumReg += entAct.listAtrib.ElementAt(i).longitud;

                        //dir = Int32.Parse(RegisInserdataGridView.CurrentRow.Cells[0].Value.ToString()) + entAct.listAtrib.ElementAt(i).longitud;
                    }
                    else
                    {
                        bw.Seek(longAcumReg, SeekOrigin.Begin);
                        posI = Int32.Parse(RegisInserdataGridView.CurrentRow.Cells[i + 1].Value.ToString());
                        r = 0;
                        bw.Write(Int32.Parse(RegisInserdataGridView.CurrentRow.Cells[i + 1].Value.ToString()));

                        longAcumReg += 4;
                        r = 0;

                    }

                    r = 0;
                    //bw.Write((int)RegisInserdataGridView.CurrentRow.Cells[i+1].Value);
                    //bw.Seek((int)RegisInserdataGridView.CurrentRow.Cells[i].Value+entAct.listAtrib.ElementAt(i).longitud,SeekOrigin.Begin);

                    //RegisInserdataGridView.CurrentRow.Cells[i + 1].Value = RegistroRellDataGrid.Rows[0].Cells[i].Value;
                }
                longAcumReg = 0;
                //RegisInserdataGridView.CurrentRow.Cells[i + 1].Value;

            }


            if (bandAtrBus == true)
            {
                r = 0;
                bandModAtrBus = true;
                dirAuxElimReg = Int32.Parse(RegisInserdataGridView.CurrentRow.Cells[0].Value.ToString());
                dirSigAuxElimReg = Int32.Parse(RegisInserdataGridView.CurrentRow.Cells[entAct.listAtrib.Count + 1].Value.ToString());
                r = 0;
                //GuardaRegistros_Click(this, null);
                EliminarReg_Click(this, null);
                RegisInserdataGridView_CellClick(this, null);
                CambiaEntiReg(this, null);
                GuardaRegistros_Click(this, null);
                bandModAtrBus = false;

                //hasPuenteDeReg(Int32.Parse(RegisInserdataGridView.CurrentRow.Cells[0].Value.ToString()));
                //ordenaPorClv(Int32.Parse(RegisInserdataGridView.CurrentRow.Cells[0].Value.ToString()), RegisInserdataGridView.CurrentRow.Index);

            }
        }

        public void hasPuenteDeReg(int dirReacomo)
        {
            /*for(int i = 0; i< RegisInserdataGridView.Rows.Count;i++)
            {
                bw.Seek((int)RegisInserdataGridView.Rows[indFilEli - 1].Cells[0].Value + entAct.longAtributos - 8, SeekOrigin.Begin);
                bw.Write((int)RegisInserdataGridView.Rows[indFilEli + 1].Cells[0].Value);
            }*/
        }

        private void RegisInserdataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            r = 0;
        }

    }
}
