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

        public Datos()
        {
            InitializeComponent();
            listEntDat = new List<FileStream>();

            

        }
        
        public void actualizaDicc(Diccionario d)
        {
            int r = 0;
            dic = d;

            foreach(Entidad a in dic.listEntidad)
            {
                comboBoxEntiDatos.Items.Add(a.nombre);
            }
            r = 0;

            //creaArchivosDat();

        }

        public void creaArchivosDat(Entidad ent)
        {

            ent.archivoDat = (new FileStream(ent.nombre+".dat", FileMode.Create));
            entAct.archivoDat.Close();

        }


       

        public void GeneraArchivos()
        {

        }

        private void GuardaRegistros_Click(object sender, EventArgs e)
        {
            if (entAct != null)
            {
                r = 0;

                    r = 0;
                    entAct.archivoDat = File.Open(entAct.archivoDat.Name, FileMode.Open);
                    long lonArDat = entAct.archivoDat.Length;
                    //Registro reg = new Registro((int)lonArDat, -1);
                    //entAct.listReg.Add(reg);                     
                    entAct.archivoDat.Close();

                    r = 0;
                    using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.nombre + ".dat", FileMode.Open)))
                    {
                        bw.Seek((int)lonArDat, SeekOrigin.Begin);
                        bw.Write(lonArDat);
                        r = 0;
                        for (int col = 0; col < RegistroRellDataGrid.Rows[0].Cells.Count; col++)
                        {

                            r = 0;
                            Atributo act = entAct.listAtrib.ElementAt(col);
                            r = 0;
                            string valor = RegistroRellDataGrid.Rows[0].Cells[col].Value.ToString();
                            if (act.tipo == 'C')
                            {
                                bw.Write(valor);
                                int contChar = 0;
                                r = 0;
                                
                                while (contChar < act.longitud - 1 - valor.Length)
                                {
                                    bw.Write('-');
                                    contChar++;
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
                        bw.Write((long)-1);
                        compruebaFinal(bw);
                    }

                
                /*if (entAct.listReg.Count == 1)
                {
                    using (BinaryWriter bw = new BinaryWriter(File.Open(dic.nomArchivo, FileMode.Open)))
                    {
                        r = 0;
                        bw.Seek((int)entAct.dirEnti + 51, SeekOrigin.Begin);
                        bw.Write(entAct.listReg.ElementAt(0).dirReg);
                        entAct.dirDat = entAct.listReg.ElementAt(0).dirReg;
                        r = 0;
                    }
                }*/


                MessageBox.Show("Tu registros fueron guardados satisfactoriamente");
                limpiaGridRellReg();
            }
            else
                MessageBox.Show("Elige una entidad");
            //RegistroDataGrid.Refresh();
            //RegistroDataGrid.DataSource = null;
        }

        public void escribeRegistro()
        {
            
        }

        public void compruebaFinal(BinaryWriter bw)
        {

            
            //using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.archivoDat.Name, FileMode.Open)))
            //{
            for (int i = 0; i < entAct.listReg.Count; i++)
            {
                if (i != entAct.listReg.Count - 1)
                {
                    entAct.listReg.ElementAt(i).dirSigReg = entAct.listReg.ElementAt(i + 1).dirReg;
                r = 0;
                int algo = entAct.listReg.ElementAt(i).dirSigReg - 8;
                r = 0;
                    bw.Seek(entAct.listReg.ElementAt(i).dirSigReg - 8, SeekOrigin.Begin);
                    bw.Write(entAct.listReg.ElementAt(i).dirSigReg);
                }
                

            }
            //}
            //entAct.archivoDat.Close();
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

                r = 0;
                foreach (Atributo a in entReg.listAtrib)
                {
                    lonRegisAct += a.longitud;
                }
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
                    agregaRegisExistentes(entReg);
                }
            }
            else
                MessageBox.Show("Escoge una entidad");
        }


        public void agregaRegisExistentes(Entidad ent)
        {
            r = 0;
            int com = 0;
            bool bandSigReg = true;
            int conRen = 0;
            int dirReg = (int)ent.dirDat, dirRegAct, dirSigReg;
            //if()
            ent.archivoDat = File.Open(ent.nombre+".dat", FileMode.Open);
            BinaryReader br = new BinaryReader(ent.archivoDat);

            r = 0;
            while (bandSigReg)
            {
                r = 0;
                //ent.archivoDat.Seek(dirReg, SeekOrigin.Begin);
                RegisInserdataGridView.Rows.Add();
                r = 0;
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

                ent.listReg.Add(new Registro(dirRegAct, dirSigReg));

                if(bandSigReg)
                ent.archivoDat.Seek(dirSigReg, SeekOrigin.Begin);

                r = 0;
                conRen++;
                

            }
            //

           

            ent.archivoDat.Close();
        }
    }
}
