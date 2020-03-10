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
        public List<FileStream> listEntDat ;
        Entidad entAct;
        int lonRegisAct = 16;
        bool bandElim = false;
        bool bandModi = false;
        int posAtrBus = 0;
        bool bandAtrBus = false;
        bool bandModAtrBus = false;
        bool bandUltModiAtr = false;



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

            foreach(Entidad a in dic.listEntidad)
            {
                comboBoxEntiDatos.Items.Add(a.nombre);
            }
        }

        public void creaArchivosDat(Entidad ent)
        {
            ent.archivoDat = (new FileStream(BitConverter.ToString(ent.id_enti) + ".dat", FileMode.Create));
            entAct.archivoDat.Close();

            /*int contAtrLon = 0;

            foreach (Atributo a in entAct.listAtrib)
            {
                contAtrLon += a.longitud;

            }


            entAct.longAtributos = contAtrLon+16;*/
        }


        public void GeneraArchivos()
        {

        }
        
        private void GuardaRegistros_Click(object sender, EventArgs e)
        {
            string valor = "";

            if (entAct != null)
            {
                entAct.archivoDat = File.Open(entAct.archivoDat.Name, FileMode.Open);
                long lonArDat = entAct.archivoDat.Length;
                //Registro reg = new Registro((int)lonArDat, -1);
                //entAct.listReg.Add(reg);                     
                entAct.archivoDat.Close();

                RegisInserdataGridView.Rows[RegisInserdataGridView.Rows.Count-1].Cells[0].Value = (int)lonArDat; 
                r = 0;


                using (BinaryWriter bw = new BinaryWriter(File.Open(BitConverter.ToString(entAct.id_enti) + ".dat", FileMode.Open)))
                {
                    bw.Seek((int)lonArDat, SeekOrigin.Begin);
                    bw.Write(lonArDat);
                    r = 0;
                    for (int col = 0; col < RegistroRellDataGrid.Rows[0].Cells.Count; col++)
                    {
                        r = 0;
                        Atributo act = entAct.listAtrib.ElementAt(col);
                        r = 0;
                        RegisInserdataGridView.Rows[RegisInserdataGridView.Rows.Count - 1].Cells[col + 1].Value = RegistroRellDataGrid.Rows[0].Cells[col].Value.ToString();
                        valor = RegistroRellDataGrid.Rows[0].Cells[col].Value.ToString();

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
                            bw.Write(Int32.Parse(valor));
                            r = 0;
                        }

                    }
                    //bw.Write((long)-1);
                    //RegisInserdataGridView.Rows[RegisInserdataGridView.Rows.Count - 1].Cells[entAct.listAtrib.Count + 1].Value = -1;

                    r = 0;
                    bw.Write((long)-1);
                    RegisInserdataGridView.Rows[RegisInserdataGridView.Rows.Count - 1].Cells[entAct.listAtrib.Count + 1].Value = -1;

                    if (!bandAtrBus)
                        compruebaFinal(bw);
                    //RegisInserdataGridView.Sort(RegisInserdataGridView.Columns[posAtrBus], ListSortDirection.Ascending);


                    
                    
                    r = 0;
                }

                //pasaDatosAGrid();

                if(entAct.dirDat == -1)
                actualizaPrimerRegEnt(entAct);

                if (bandAtrBus)
                {
                    ordenaPorClv((int)lonArDat, RegisInserdataGridView.Rows.Count - 1);                  
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
            foreach(char a in cad)
            {
                if(char.IsLetterOrDigit(a) || a == '-')
                {
                    cadCorrecta += a;
                }
            }
            return cadCorrecta;
        }




        public void ordenaPorClv(int posUltReg, int posDataGrid)
        {
            string cadCveAct;

            // "<" La cadena 1 va antes de la cadena 2
            entAct.archivoDat = File.Open(BitConverter.ToString(entAct.id_enti) + ".dat", FileMode.Open);
            BinaryReader br = new BinaryReader(entAct.archivoDat);

            //int posUltReg = (int)entAct.archivoDat.Length - entAct.longAtributos;

            r = 0;

            //ordenaCLV_C();


            entAct.archivoDat.Seek(posUltReg + entAct.posCveBus, SeekOrigin.Begin);
            if(entAct.tipoCveBus =='C')
            {
                cadCveAct = new string(br.ReadChars(10));
            }
            else
            {
                int num = br.ReadInt32();
                //cadCveAct = br.ReadInt32().ToString();
                cadCveAct = num.ToString();
                r = 0;
            }




            int posRegCom = (int)entAct.dirDat;
            
            r = 0;
            

            entAct.archivoDat.Seek(posRegCom+entAct.posCveBus, SeekOrigin.Begin);
            string cadComp = "";
            int dataGridRen = 0;
            bool bandUlt = false;
            bool bandPri = true;
            //int  = 0;

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

            while (string.CompareOrdinal(cadComp, cadCveAct) < 0)
                {
                    r = 0;
                
                    bandPri = false;

                    int posRegAct = posRegCom;
                    
                    entAct.archivoDat.Seek(posRegCom+entAct.longAtributos-8, SeekOrigin.Begin);
                    posRegCom = posRegCom + entAct.longAtributos - 8;

                    //int valor = br.ReadInt32();
                    int direSigReg = br.ReadInt32();

                    r = 0;

                    if(direSigReg == -1)//llego al registro final
                    {
                        r = 0;
                    /*if (bandUltModiAtr)
                    {

                    }
                    else
                    {*/
                    
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
                        if (bandUltModiAtr)
                        {
                            RegisInserdataGridView.Rows[posDataGrid + 1].Cells[entAct.listAtrib.Count + 1].Value = posUltReg;
                            bw.Seek(posUltReg + entAct.longAtributos - 8, SeekOrigin.Begin);
                            

                            bw.Write(direSigReg);
                        }

                            bandUlt = true;

                        }
                        entAct.archivoDat = File.Open(entAct.archivoDat.Name, FileMode.Open);
                        break;
                    //}
                        
                    }
                    else//Si no llega hasta el ultimo registro//No necesita confirmacion
                    {
                        r = 0;
                        if (bandModAtrBus && direSigReg == posUltReg)
                        {
                            r = 0;


                        entAct.archivoDat.Close();
                        using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.archivoDat.Name, FileMode.Open)))
                        {                            
                            bw.Seek(Int32.Parse(RegisInserdataGridView.Rows[dataGridRen].Cells[0].Value.ToString())+entAct.longAtributos-8, SeekOrigin.Begin);
                            bw.Write(Int32.Parse(RegisInserdataGridView.Rows[posDataGrid+1].Cells[0].Value.ToString()));

                            RegisInserdataGridView.Rows[dataGridRen].Cells[entAct.listAtrib.Count + 1].Value = RegisInserdataGridView.Rows[posDataGrid + 1].Cells[0].Value;
                            //posUltReg =
                        }

                        entAct.archivoDat = File.Open(entAct.archivoDat.Name, FileMode.Open);
                        br = new BinaryReader(entAct.archivoDat);

                        //entAct.archivoDat = File.Open(BitConverter.ToString(entAct.id_enti) + ".dat", FileMode.Open);
                        //BinaryReader br = new BinaryReader(entAct.archivoDat);

                        direSigReg += entAct.longAtributos;

                        bandUltModiAtr = true;
                        r = 0;/*
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
                            posRegCom = direSigReg;
                            */
                    }
                        //else
                        //{
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
                            posRegCom = direSigReg;
                       // }
                        r = 0;
                    }

                    dataGridRen++;

                    r = 0;
                }
                
                r = 0;
                if(string.CompareOrdinal(cadComp, cadCveAct)  == 0 && RegisInserdataGridView.Rows.Count!=1 && !bandUlt && !bandPri)//Si son iguales a otro registro//+
                {
                    entAct.archivoDat.Close();
                    using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.archivoDat.Name, FileMode.Open)))
                    {
                        r = 0;
                        //int p = posRegAct + entAct.longAtributos - 8;
                        r = 0;
                        int a = posRegCom - 8;

                        r = 0;
                        bw.Seek(Int32.Parse(RegisInserdataGridView.Rows[dataGridRen - 1].Cells[0].Value.ToString()) + entAct.longAtributos - 8, SeekOrigin.Begin);
                        bw.Write(posUltReg);

                        if (bandModAtrBus)
                        {
                            bw.Seek(Int32.Parse(RegisInserdataGridView.Rows[dataGridRen].Cells[0].Value.ToString()) + entAct.longAtributos - 8, SeekOrigin.Begin);
                            bw.Write(Int32.Parse(RegisInserdataGridView.Rows[posDataGrid+1].Cells[0].Value.ToString()));

                            RegisInserdataGridView.Rows[dataGridRen].Cells[entAct.listAtrib.Count + 1].Value = RegisInserdataGridView.Rows[posDataGrid+1].Cells[0].Value;

                            bandModAtrBus = false;
                        }


                    a = posUltReg - 8;
                        r = 0;
                        bw.Seek(posUltReg + entAct.longAtributos - 8, SeekOrigin.Begin);
                        bw.Write(posRegCom);

                        RegisInserdataGridView.Rows[dataGridRen - 1].Cells[entAct.listAtrib.Count + 1].Value = posUltReg;
                        RegisInserdataGridView.Rows[posDataGrid].Cells[entAct.listAtrib.Count + 1].Value = posRegCom;
                        
                        

                      
                        

                    }
                    entAct.archivoDat = File.Open(entAct.archivoDat.Name , FileMode.Open);

                   
                }
                else
                if(bandPri == true && RegisInserdataGridView.Rows.Count != 1)//Va antes del primero//+
                {
                    int valEnt = (int)entAct.dirDat;
                    entAct.dirDat = posUltReg;
                    using (BinaryWriter bw = new BinaryWriter(File.Open(dic.nomArchivo, FileMode.Open)))
                    {
                        bw.Seek((int)entAct.dirEnti+51, SeekOrigin.Begin);
                        bw.Write(posUltReg);

                    }

                r = 0;
                    
                r = 0;
                    entAct.archivoDat.Close();
                    using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.archivoDat.Name, FileMode.Open)))
                    {

                        bw.Seek(Int32.Parse(RegisInserdataGridView.Rows[posDataGrid].Cells[0].Value.ToString()) + entAct.longAtributos - 8, SeekOrigin.Begin);
                        bw.Write(posRegCom);
                    r = 0;
                        if (bandModAtrBus)
                        {
                            bw.Seek(Int32.Parse(RegisInserdataGridView.Rows[posDataGrid-1].Cells[0].Value.ToString()) + entAct.longAtributos - 8, SeekOrigin.Begin);
                            bw.Write(Int32.Parse(RegisInserdataGridView.Rows[posDataGrid+1].Cells[0].Value.ToString()));
                            RegisInserdataGridView.Rows[posDataGrid-1].Cells[entAct.listAtrib.Count + 1].Value = RegisInserdataGridView.Rows[posDataGrid].Cells[entAct.listAtrib.Count + 1].Value;
                            bandModAtrBus = false;
                        }

                    }
                r = 0;


                    RegisInserdataGridView.Rows[posDataGrid].Cells[entAct.listAtrib.Count + 1].Value = posRegCom;
                    entAct.archivoDat = File.Open(entAct.archivoDat.Name, FileMode.Open);
                    r = 0;

                }
                else
                if(!bandUlt && RegisInserdataGridView.Rows.Count != 1)//Va enmedio//+
                {
                    //Va despues de la actual
                    r = 0;
                    entAct.archivoDat.Close();
                    using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.archivoDat.Name, FileMode.Open)))
                    {
                        bw.Seek(Int32.Parse(RegisInserdataGridView.Rows[dataGridRen - 1].Cells[0].Value.ToString()) + entAct.longAtributos - 8, SeekOrigin.Begin);
                        bw.Write(posUltReg);
                    r = 0;

                        bw.Seek(posUltReg + entAct.longAtributos - 8, SeekOrigin.Begin);
                        bw.Write(posRegCom);

                    r = 0;

                    if (bandModAtrBus)
                    {
                        //Este es cambio por modifiAtri
                        bw.Seek(posRegCom + entAct.longAtributos - 8, SeekOrigin.Begin);
                        bw.Write(Int32.Parse(RegisInserdataGridView.Rows[posDataGrid + 1].Cells[0].Value.ToString()));
                        RegisInserdataGridView.Rows[dataGridRen].Cells[entAct.listAtrib.Count + 1].Value = Int32.Parse(RegisInserdataGridView.Rows[posDataGrid + 1].Cells[0].Value.ToString());
                        bandModAtrBus = false;
                    }


                        RegisInserdataGridView.Rows[dataGridRen - 1].Cells[entAct.listAtrib.Count + 1].Value = posUltReg;
                        RegisInserdataGridView.Rows[posDataGrid].Cells[entAct.listAtrib.Count + 1].Value = posRegCom;                  
                }
                    entAct.archivoDat = File.Open(entAct.archivoDat.Name , FileMode.Open);


                }


                dataGridRen = 0;


            entAct.archivoDat.Close();
        }

        public void actualizaPrimerRegEnt(Entidad entAct)
        {
            entAct.archivoDat = File.Open(BitConverter.ToString(entAct.id_enti) + ".dat", FileMode.Open);
            BinaryReader br = new BinaryReader(entAct.archivoDat);
            long valorPri = br.ReadInt32();

            r = 0;
            if (valorPri  >= 0)
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
                    RegisInserdataGridView.Rows[i-1].Cells[entAct.listAtrib.Count+1].Value = RegisInserdataGridView.Rows[i].Cells[0].Value;
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
            limpiaGridRellReg();
            limpiaGridInsertadosReg();
            Object inEn = comboBoxEntiDatos.SelectedItem;
            r = 0;

            if (inEn != null)
            {
                //Columna 1 del grid de registrosRellenados
                DataGridViewTextBoxColumn Columna1 = new DataGridViewTextBoxColumn();
                Columna1.HeaderText ="Dir_Registro";
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
                        break;
                    }
                    else
                        posAtrBus+= a.longitud;
                }
                entAct.posCveBus = posAtrBus+8;

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
                    DataGridViewTextBoxColumn Columna2 = new DataGridViewTextBoxColumn();
                    Columna2.HeaderText = a.nombre;
                    RegistroRellDataGrid.Columns.Add(Columna2);

                    //Columnas del grid de registrosRellenados
                    DataGridViewTextBoxColumn Columna3 = new DataGridViewTextBoxColumn();
                    Columna3.HeaderText = a.nombre;
                    RegisInserdataGridView.Columns.Add(Columna3);
                }
                RegistroRellDataGrid.Rows.Add();

                //Columna ultima del grid de registrosRellenados
                DataGridViewTextBoxColumn Columna4 = new DataGridViewTextBoxColumn();
                Columna4.HeaderText = "Dir_Sig_Registro";
                RegisInserdataGridView.Columns.Add(Columna4);

                

                if(entReg.dirDat == -1)
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
            
            ent.archivoDat = File.Open(BitConverter.ToString(ent.id_enti) + ".dat", FileMode.Open);
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
                            foreach(char a in br.ReadChars(atr.longitud))
                            {
                                if (char.IsDigit(a) || char.IsLetter(a))
                                {
                                    RegisInserdataGridView.Rows[conRen].Cells[i+1].Value += a.ToString();
                                }
                            }
                            String prueba = RegisInserdataGridView.Rows[conRen].Cells[i + 1].Value.ToString();
                            r = 0;
                            break;
                        case 'E':

                            int valor = br.ReadInt32();
                            RegisInserdataGridView.Rows[conRen].Cells[i+1].Value = valor;
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
                RegisInserdataGridView.Rows[conRen].Cells[ent.listAtrib.Count+1].Value = dirSigReg;
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
                    RegistroRellDataGrid.Rows[0].Cells[i].Value = RegisInserdataGridView.CurrentRow.Cells[i+1].Value;
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

                    if ((int)RegisInserdataGridView.Rows[indFilEli].Cells[0].Value  == entAct.dirDat)//Es el primer registro
                    {
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
                        int algo = (int)RegisInserdataGridView.Rows[indFilEli - 1].Cells[entAct.listAtrib.Count + 1].Value-8;
                        r = 0;


                        bw.Seek((int)RegisInserdataGridView.Rows[indFilEli - 1].Cells[entAct.listAtrib.Count + 1].Value-8, SeekOrigin.Begin);
                        bw.Write(-1);
                    }
                    else//Es cualquier registro de en medio
                    {
                        r = 0;
                        bw.Seek((int)RegisInserdataGridView.Rows[indFilEli - 1].Cells[entAct.listAtrib.Count + 1].Value - 8, SeekOrigin.Begin);
                        bw.Write((int)RegisInserdataGridView.Rows[indFilEli + 1].Cells[0].Value);
                    }
                                        
                    r = 0;
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
            for(int i = 0; i < entAct.listAtrib.Count;i++)
            {
                RegisInserdataGridView.CurrentRow.Cells[i + 1].Value = RegistroRellDataGrid.Rows[0].Cells[i].Value;
            }

            


            int posI = 0;
            int contChar = 0;
            //RegisInserdataGridView.Sort(RegisInserdataGridView.Columns[posAtrBus], ListSortDirection.Ascending);

            using (BinaryWriter bw = new BinaryWriter(File.Open(BitConverter.ToString(entAct.id_enti) + ".dat", FileMode.Open)))
            {
                // bw.Seek(, SeekOrigin.Begin);
                
                int longAcumReg = Int32.Parse(RegisInserdataGridView.CurrentRow.Cells[0].Value.ToString()) + 8;
                bw.Seek(longAcumReg, SeekOrigin.Begin);

                r = 0;
                for (int i = 0; i < entAct.listAtrib.Count; i++)
                {
                    r = 0;
                    if (entAct.listAtrib.ElementAt(i).tipo == 'C')
                    {
                        bw.Seek(longAcumReg, SeekOrigin.Begin);
                        //posS = RegisInserdataGridView.CurrentRow.Cells[i + 1].Value.ToString();                 
                        r = 0;
                        char[] valCad = RegisInserdataGridView.CurrentRow.Cells[i + 1].Value.ToString().ToCharArray() ;
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
                ordenaPorClv(Int32.Parse(RegisInserdataGridView.CurrentRow.Cells[0].Value.ToString()), RegisInserdataGridView.CurrentRow.Index);
            }
        }

        private void RegisInserdataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            r = 0;
            
        }
    }
}
