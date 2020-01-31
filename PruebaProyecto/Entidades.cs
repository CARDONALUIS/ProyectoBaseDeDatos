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
    public partial class Entidades : Form 
    {
        int r = 0;
        public Diccionario dic;
        public int contEntidad = 0;
        

        public Entidades(Diccionario dic2)
        {
            InitializeComponent();
            dic = dic2;

            labelCab.Text = dic.cab.ToString();
            


            DataGridViewTextBoxColumn Columna1 = new DataGridViewTextBoxColumn();
            Columna1.HeaderText = "ID";
            GridEntidades.Columns.Add(Columna1);

            DataGridViewTextBoxColumn Columna2 = new DataGridViewTextBoxColumn();
            Columna2.HeaderText = "Entidad";
            GridEntidades.Columns.Add(Columna2);

            DataGridViewTextBoxColumn Columna3 = new DataGridViewTextBoxColumn();
            Columna3.HeaderText = "Dir_Enti";
            GridEntidades.Columns.Add(Columna3);

            DataGridViewTextBoxColumn Columna4 = new DataGridViewTextBoxColumn();
            Columna4.HeaderText = "Dir_Atri";
            GridEntidades.Columns.Add(Columna4);

            DataGridViewTextBoxColumn Columna5 = new DataGridViewTextBoxColumn();
            Columna5.HeaderText = "Dir_Dat";
            GridEntidades.Columns.Add(Columna5);

            DataGridViewTextBoxColumn Columna6 = new DataGridViewTextBoxColumn();
            Columna6.HeaderText = "Dir_Sig_Enti";
            GridEntidades.Columns.Add(Columna6);

            buttonEliEnti.Visible = false;
            buttonCamEnt.Visible = false;
            comboBoxModEnt.Visible = false;

        }

        private void botonActualizar(object sender, EventArgs e)
        {
            String nomEnti = "";

            nomEnti = textBoxEntidad.Text;

            

            /*if (dic.listEntidad.Count == 0)
                {
                    dic.cab = 8;

                    Entidad ent = new Entidad(45, nomEnti, (int)dic.cab, -1, -1, -1);
                    dic.listEntidad.Add(ent);

                    labelCab.Text = dic.cab.ToString();
                    r = 0;
                    dic.vaActEnt = 80;
                    //ent.varSigEnti = 80;
                    //ent.dirSigEnti = -1;
                    //ent.dirAtri = 80;
                    //ent.dirDat = -1;
                    //ent.dirEnti = 


                    //ent.escribeArchivoEntid(dic.archivo, dic.nomArchivo);

                    

                    escribeEntidad(bw, ent);
                    //bw.Write(dic.cab);
                    
                    actualizaGridEnt();
                    
                }
                else
                {*/
                    r = 0;

                    //dic.archivo.Close();
                    //dic.archivo = File.Open(dic.nomArchivo, FileMode.Open, FileAccess.Read);

                    //dic.archivo = File.Open(dic.nomArchivo, FileMode.Open, FileAccess.Read);
                    //dic.archivo.Close();
                    dic.archivo = File.Open(dic.nomArchivo, FileMode.Open, FileAccess.Read);
                    long dirEntiNueva = dic.archivo.Length;
                    
                    dic.archivo.Close();

            //dic.archivo.Close();

            /*if ((int)dic.listEntidad.ElementAt(dic.listEntidad.Count - 1).listAtrib.Count != 0)
            dirEntiNueva = (int)dic.listEntidad.ElementAt(dic.listEntidad.Count - 1).listAtrib.ElementAt(dic.listEntidad.ElementAt(dic.listEntidad.Count - 1).listAtrib.Count - 1).dirAtri + dic.tamAtrib;
            else
                dirEntiNueva = (
                    int)dic.listEntidad.ElementAt(dic.listEntidad.Count - 1).dirEnti + dic.tamEntidad;
                        */
            //string iden = "";

            byte[] buffer = new byte[5];
            new Random().NextBytes(buffer);

            //iden = BitConverter.ToString(buffer);

            //r = 0;

            Entidad ent = new Entidad(buffer, nomEnti,(int)dirEntiNueva, -1, -1, -1);

                    dic.listEntidad.Add(ent);

                    

                    
                    

                    //ent = dic.listEntidad.Find(x => x.nombre == nomEnti);

            r = 0;


            escribeEntidad(ent);
            actualizaSigEnti();
            actualizaGridEnt();
            r = 0;

                //}
           


            //dic.archivo.Close();
            //dic.archivo = File.Open(dic.nomArchivo, FileMode.Open, FileAccess.Read);
            //long algo = dic.archivo.Length;

            
            //lista = lista.OrderBy(o => o.nombre).ToList();
            r = 0;
        }

        public void actualizaGridEnt()
        {
            GridEntidades.Rows.Add();
            for (int i = 0; i < dic.listEntidad.Count; i++)
            {
                GridEntidades.Rows[i].Cells[0].Value = BitConverter.ToString(dic.listEntidad.ElementAt(i).id_enti);
                GridEntidades.Rows[i].Cells[1].Value = dic.listEntidad.ElementAt(i).nombre;
                GridEntidades.Rows[i].Cells[2].Value = dic.listEntidad.ElementAt(i).dirEnti;
                GridEntidades.Rows[i].Cells[3].Value = dic.listEntidad.ElementAt(i).dirAtri;
                GridEntidades.Rows[i].Cells[4].Value = dic.listEntidad.ElementAt(i).dirDat;
                GridEntidades.Rows[i].Cells[5].Value = dic.listEntidad.ElementAt(i).dirSigEnti;
            }           
            labelCab.Text = dic.cab.ToString();
        }

        public void actualizaDicc(Diccionario d)
        {
            
        }

        public void inisializaEnti()
        {
            labelCab.Text = dic.cab.ToString();

            DataGridViewTextBoxColumn Columna1 = new DataGridViewTextBoxColumn();
            Columna1.HeaderText = "ID";
            GridEntidades.Columns.Add(Columna1);

            DataGridViewTextBoxColumn Columna2 = new DataGridViewTextBoxColumn();
            Columna2.HeaderText = "Entidad";
            GridEntidades.Columns.Add(Columna2);

            DataGridViewTextBoxColumn Columna3 = new DataGridViewTextBoxColumn();
            Columna3.HeaderText = "Dir_Enti";
            GridEntidades.Columns.Add(Columna3);

            DataGridViewTextBoxColumn Columna4 = new DataGridViewTextBoxColumn();
            Columna4.HeaderText = "Dir_Atri";
            GridEntidades.Columns.Add(Columna4);

            DataGridViewTextBoxColumn Columna5 = new DataGridViewTextBoxColumn();
            Columna5.HeaderText = "Dir_Dat";
            GridEntidades.Columns.Add(Columna5);

            DataGridViewTextBoxColumn Columna6 = new DataGridViewTextBoxColumn();
            Columna6.HeaderText = "Dir_Sig_Enti";
            GridEntidades.Columns.Add(Columna6);
        }

        public void escribeEntidad(Entidad ent)
        {
            int contBytChar = 0;

            using(BinaryWriter bw = new BinaryWriter(File.Open(dic.nomArchivo, FileMode.Open)))
            {
                
                r = 0;
            bw.Seek((int)ent.dirEnti, SeekOrigin.Begin);
            bw.Write(ent.id_enti);
                //bw.Write("");
                r = 0;
               
            bw.Write(ent.nombre);
            r = 0;
                while (contBytChar < (29 - ent.nombre.Length))
                {
                    bw.Write('-');
                    contBytChar++;
                }

                contBytChar = 0;

                bw.Write(ent.dirEnti);
                bw.Write(ent.dirAtri);
                bw.Write(ent.dirDat);
                bw.Write(ent.dirSigEnti);

                r = 0;

                    if(dic.listEntidad.Count() != 1)
                    {
                        r = 0;
                        int index = dic.listEntidad.FindIndex(x => x.nombre == ent.nombre);
                        bw.Seek((int)dic.listEntidad.ElementAt(index - 1).dirEnti + 59, SeekOrigin.Begin);
                        bw.Write(ent.dirEnti);
                    r = 0;

                    }
                    
            }
        }

        public void actualizaSigEnti()
        {
            r = 0;
            List<Entidad> lisAux = dic.listEntidad;
            lisAux = lisAux.OrderBy(o => o.nombre).ToList();

            for (int i = 0; i < lisAux.Count; i++)
            {
                int ind = dic.listEntidad.FindIndex(x => x.nombre == lisAux.ElementAt(i).nombre);

                if (i != lisAux.Count - 1)
                {

                    r = 0;
                    dic.listEntidad.ElementAt(ind).dirSigEnti = lisAux.ElementAt(i + 1).dirEnti;


                    /*using (BinaryWriter bw = new BinaryWriter(File.Open(dic.nomArchivo, FileMode.Open)))
                    {
                        r = 0;
                        bw.Seek((int)dic.listEntidad.ElementAt(ind).dirEnti+64, SeekOrigin.Begin);
                        bw.Write(lisAux.ElementAt(i + 1).dirEnti);
                    }*/
                }
                else
                {
                    dic.listEntidad.ElementAt(ind).dirSigEnti = -1;
                    r = 0;
                }
            }
            r = 0;
            //dic.listEntidad.ElementAt(dic.listEntidad.Count-1).dirSigEnti = -1; 
            r = 0;
            dic.cab = (int)lisAux.ElementAt(0).dirEnti;

            for (int i = 0; i < dic.listEntidad.Count; i++)
            {
                using (BinaryWriter bw = new BinaryWriter(File.Open(dic.nomArchivo, FileMode.Open)))
                {
                    r = 0;
                    
                    bw.Seek((int)dic.listEntidad.ElementAt(i).dirEnti + 59, SeekOrigin.Begin);

                    //bw.Write(lisAux.ElementAt(i+1).dirEnti);
                    bw.Write(dic.listEntidad.ElementAt(i).dirSigEnti);

                    /*if (i != dic.listEntidad.Count - 1)
                    {
                        r = 0;
                        bw.Write(dic.listEntidad.ElementAt(i + 1).dirEnti);
                    }
                    else
                    {
                        bw.Write(-1);
                        r = 0;
                    }*/

                    bw.Seek(0, SeekOrigin.Begin);
                    bw.Write(dic.cab);

                }
            }

            r = 0;
             //dic.cab = (int)lisAux.ElementAt(0).dirEnti;

            /*
            using (BinaryWriter bw = new BinaryWriter(File.Open(dic.nomArchivo, FileMode.Open)))
            {
                //Actualiza la cabezera en el archivo
                
                for(int i = 0; i < dic. )

            }*/
        }

        private void VerActualesEnti(object sender, EventArgs e)
        {
            GridEntidades.Columns.Clear();
            GridEntidades.Rows.Clear();
            inisializaEnti();

            for (int i = 0; i < dic.listEntidad.Count; i++)
            {
                GridEntidades.Rows.Add();
                GridEntidades.Rows[i].Cells[0].Value = BitConverter.ToString(dic.listEntidad.ElementAt(i).id_enti);
                GridEntidades.Rows[i].Cells[1].Value = dic.listEntidad.ElementAt(i).nombre;
                GridEntidades.Rows[i].Cells[2].Value = dic.listEntidad.ElementAt(i).dirEnti;
                GridEntidades.Rows[i].Cells[3].Value = dic.listEntidad.ElementAt(i).dirAtri;
                GridEntidades.Rows[i].Cells[4].Value = dic.listEntidad.ElementAt(i).dirDat;
                GridEntidades.Rows[i].Cells[5].Value = dic.listEntidad.ElementAt(i).dirSigEnti;
                r = 0;
               
            }
        }

        private void ButtonModEnti_Click(object sender, EventArgs e)
        {
            buttonEliEnti.Visible = true;
            buttonCamEnt.Visible = true;
            comboBoxModEnt.Visible = true;

            textBoxEntidad.Text = "";

            comboBoxModEnt.Items.Clear();

            foreach(Entidad a in dic.listEntidad)
            {
                comboBoxModEnt.Items.Add(a.nombre);
            }


        }

        private void ButtonCamEnt_Click(object sender, EventArgs e)
        {
            int inEM = comboBoxModEnt.SelectedIndex;

            dic.listEntidad.ElementAt(inEM).nombre = textBoxEntidad.Text;

            using (BinaryWriter bw = new BinaryWriter(File.Open(dic.nomArchivo, FileMode.Open)))
            {
                bw.Seek((int)dic.listEntidad.ElementAt(inEM).dirEnti+5, SeekOrigin.Begin);

                bw.Write(dic.listEntidad.ElementAt(inEM).nombre);

                int contBytChar = 0;

                if ((dic.listEntidad.ElementAt(inEM).nombre.Length) % 2 == 0)
                    while (contBytChar < (35 - dic.listEntidad.ElementAt(inEM).nombre.Length) / 2)
                    {
                        bw.Write("-");
                        contBytChar++;
                    }
                else
                {
                    while (contBytChar < (34 - (dic.listEntidad.ElementAt(inEM).nombre.Length)) / 2)
                    {
                        bw.Write("-");
                        contBytChar++;
                    }
                    bw.Write("");
                }
                contBytChar = 0;
            }


            comboBoxModEnt.Visible = false;
            buttonCamEnt.Visible = false;
            buttonEliEnti.Visible = false;

            actualizaSigEnti();
        }

        private void ButtonEliEnti_Click(object sender, EventArgs e)
        {
            int inEM = comboBoxModEnt.SelectedIndex;


            
            if(dic.cab == dic.listEntidad.ElementAt(inEM).dirEnti)
            {
                dic.cab = dic.listEntidad.ElementAt(inEM + 1).dirEnti;
            }
            else
            {
                if(dic.listEntidad.ElementAt(inEM).dirSigEnti == -1)
                {
                    dic.listEntidad.ElementAt(inEM - 1).dirSigEnti = -1;
                }
                else
                {
                    dic.listEntidad.ElementAt(inEM - 1).dirSigEnti = dic.listEntidad.ElementAt(inEM + 1).dirEnti;
                }
            }

            dic.listEntidad.RemoveAt(inEM);
            r = 0;



            actualizaSigEnti();

            comboBoxModEnt.Visible = false;
            buttonCamEnt.Visible = false;
            buttonEliEnti.Visible = false;
        }
    }
}
