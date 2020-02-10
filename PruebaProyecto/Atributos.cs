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
    public partial class Atributos : Form
    {
        public Diccionario dic;
        int r = 0;
        public int contAtributo = 0;
        Entidad ent;
        public Atributos(Diccionario dic)
        {
            InitializeComponent();
            inisializaColumnas();
            comboBoxModAtri.Visible = false;
            cambiaAtribu.Visible = false;
            buttonEliAtri.Visible = false;
        }

        public void actualizaDicc(Diccionario d)
        {
            dic = d;
            r = 0;

            comboBoxEntidades.Items.Clear();
            if(File.Exists(dic.nomArchivo))
            for (int i = 0; i < dic.listEntidad.Count; i++)
            {
                comboBoxEntidades.Items.Add(dic.listEntidad.ElementAt(i).nombre);
            }
        }

        private void ActualizarAtributo(object sender, EventArgs e)
        {
            //Obtenemos la entidad actual
            
            int entSel = comboBoxEntidades.SelectedIndex;

            if (entSel != -1 )
            {
                if (comboBoxAtrTipo.SelectedItem != null && textBoxAtriLong.Text != "" && comboBoxAtrTip_Ind.SelectedItem != null)
                {
                    ent = dic.listEntidad.ElementAt(entSel);
                    Atributo atriActual;

                    String nomAtri = "";
                    int longitud;

                    nomAtri = textBoxNomAtri.Text;

                    dic.archivo = File.Open(dic.nomArchivo, FileMode.Open, FileAccess.Read);
                    long dirAtri = dic.archivo.Length;

                    dic.archivo.Close();

                    if (comboBoxAtrTipo.SelectedItem.ToString() == "E")
                        longitud = 4;
                    else
                        longitud = Int32.Parse(textBoxAtriLong.Text);

                    r = 0;
                    ent.varSigAtri = ent.varSigAtri + dic.tamAtrib;
                    byte[] buffer = new byte[5];
                    new Random().NextBytes(buffer);

                    
                    Atributo atributo = new Atributo(buffer, nomAtri, comboBoxAtrTipo.SelectedItem.ToString().ElementAt(0), longitud, /*(int)ent.dirAtri*/(int)dirAtri, Int32.Parse(comboBoxAtrTip_Ind.SelectedIndex.ToString()), -1, (int)dirAtri + dic.tamAtrib);
                    ent.listAtrib.Add(atributo);
                    atriActual = atributo;
                    actualizaUltima(ent);
                    actualizaGridAtri(ent);
                    escribeAtrib(atributo, ent);

                    r = 0;
                }
                else
                    MessageBox.Show("Rellena todo los campos");
            }
            else
                MessageBox.Show("Selecciona una entidad\nSi no tienes entidades crea una");
        }

        public void escribeAtrib(Atributo atr, Entidad ent)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(dic.nomArchivo, FileMode.Open)))
            {
                if (ent.listAtrib.Count == 1)
                {
                    bw.Seek((int)ent.dirEnti + 43, SeekOrigin.Begin);
                    bw.Write(atr.dirAtri);
                    ent.dirAtri = atr.dirAtri;
                    r = 0;
                }

                int contBytChar = 0;

                bw.Seek((int)atr.dirAtri, SeekOrigin.Begin);
                bw.Write(atr.id_atri);
                bw.Write(atr.nombre);
                while (contBytChar < (29 - atr.nombre.Length))
                {
                    bw.Write('-');
                    contBytChar++;
                }
                contBytChar = 0;
                bw.Write(atr.tipo);
                bw.Write(atr.longitud);
                bw.Write(atr.dirAtri);
                bw.Write(Int32.Parse(atr.tipoIndi.ToString().ElementAt(0).ToString()));
                bw.Write(atr.dirIndi);
                bw.Write(atr.dirSigAtri);
                if (ent.listAtrib.Count != 1)
                {
                    r = 0;
                    int index = ent.listAtrib.FindIndex(x => x.nombre == atr.nombre);
                    bw.Seek((int)ent.listAtrib.ElementAt(index - 1).dirAtri + 60, SeekOrigin.Begin);
                    bw.Write(atr.dirAtri);
                    r = 0;
                }
            } 
        }

        public void actualizaGridAtri(Entidad ent)
        {
            GridAtributos.Columns.Clear();
            GridAtributos.Rows.Clear();
            inisializaColumnas();

           
            for (int i = 0; i < ent.listAtrib.Count; i++)
            {
                GridAtributos.Rows.Add();
                r = 0;
                GridAtributos.Rows[i].Cells[0].Value = BitConverter.ToString(ent.listAtrib.ElementAt(i).id_atri);
                r = 0;
                GridAtributos.Rows[i].Cells[1].Value = ent.listAtrib.ElementAt(i).nombre;
                GridAtributos.Rows[i].Cells[2].Value = ent.listAtrib.ElementAt(i).tipo;
                GridAtributos.Rows[i].Cells[3].Value = ent.listAtrib.ElementAt(i).longitud;
                GridAtributos.Rows[i].Cells[4].Value = ent.listAtrib.ElementAt(i).dirAtri;
                GridAtributos.Rows[i].Cells[5].Value = ent.listAtrib.ElementAt(i).tipoIndi;
                GridAtributos.Rows[i].Cells[6].Value = ent.listAtrib.ElementAt(i).dirIndi;
                GridAtributos.Rows[i].Cells[7].Value = ent.listAtrib.ElementAt(i).dirSigAtri;
            }
        

            r = 0;
        }

        public void actualizaUltima(Entidad ent)
        {
            for (int i = 0; i < ent.listAtrib.Count; i++)
            {
                if (i == ent.listAtrib.Count - 1)
                {
                    ent.listAtrib.ElementAt(i).dirSigAtri = -1;
                }
                else
                {    
                    ent.listAtrib.ElementAt(i).dirSigAtri = (int)ent.listAtrib.ElementAt(i+1).dirAtri;
                }
            }
        }

        private void Atributos_FormClosing(object sender, FormClosingEventArgs e)
        {
            GridAtributos.Columns.Clear();
            GridAtributos.Rows.Clear();
            inisializaColumnas();
            comboBoxModAtri.Visible = false;
            buttonEliAtri.Visible = false;
            cambiaAtribu.Visible = false;
            comboBoxModAtri.Text = "";
        }

        public void inisializaColumnas()
        {
            dic = dic;

            DataGridViewTextBoxColumn Columna1 = new DataGridViewTextBoxColumn();
            Columna1.HeaderText = "ID";
            GridAtributos.Columns.Add(Columna1);

            DataGridViewTextBoxColumn Columna2 = new DataGridViewTextBoxColumn();
            Columna2.HeaderText = "Atributo";
            GridAtributos.Columns.Add(Columna2);

            DataGridViewTextBoxColumn Columna3 = new DataGridViewTextBoxColumn();
            Columna3.HeaderText = "Tipo";
            GridAtributos.Columns.Add(Columna3);

            DataGridViewTextBoxColumn Columna4 = new DataGridViewTextBoxColumn();
            Columna4.HeaderText = "Longitud";
            GridAtributos.Columns.Add(Columna4);

            DataGridViewTextBoxColumn Columna5 = new DataGridViewTextBoxColumn();
            Columna5.HeaderText = "Dir_Atri";
            GridAtributos.Columns.Add(Columna5);

            DataGridViewTextBoxColumn Columna6 = new DataGridViewTextBoxColumn();
            Columna6.HeaderText = "Tipo_Ind";
            GridAtributos.Columns.Add(Columna6);

            DataGridViewTextBoxColumn Columna7 = new DataGridViewTextBoxColumn();
            Columna7.HeaderText = "Dir_Ind";
            GridAtributos.Columns.Add(Columna7);

            DataGridViewTextBoxColumn Columna8 = new DataGridViewTextBoxColumn();
            Columna8.HeaderText = "Dir_Sig_Atri";
            GridAtributos.Columns.Add(Columna8);
        }

        private void VerActualesAtri(object sender, EventArgs e)
        {
            GridAtributos.Columns.Clear();
            GridAtributos.Rows.Clear();
            inisializaColumnas();

            int entSel = comboBoxEntidades.SelectedIndex;

            if (entSel != -1)
            {
                ent = dic.listEntidad.ElementAt(entSel);
                for (int i = 0; i < ent.listAtrib.Count; i++)
                {
                    GridAtributos.Rows.Add();
                    r = 0;
                    GridAtributos.Rows[i].Cells[0].Value = BitConverter.ToString(ent.listAtrib.ElementAt(i).id_atri);
                    r = 0;
                    GridAtributos.Rows[i].Cells[1].Value = ent.listAtrib.ElementAt(i).nombre;
                    GridAtributos.Rows[i].Cells[2].Value = ent.listAtrib.ElementAt(i).tipo;
                    GridAtributos.Rows[i].Cells[3].Value = ent.listAtrib.ElementAt(i).longitud;
                    GridAtributos.Rows[i].Cells[4].Value = ent.listAtrib.ElementAt(i).dirAtri;
                    GridAtributos.Rows[i].Cells[5].Value = ent.listAtrib.ElementAt(i).tipoIndi;
                    GridAtributos.Rows[i].Cells[6].Value = ent.listAtrib.ElementAt(i).dirIndi;
                    GridAtributos.Rows[i].Cells[7].Value = ent.listAtrib.ElementAt(i).dirSigAtri;
                }
            }
            else
                MessageBox.Show("Selecciona un entidad");
        }

        private void ModificarEnti(object sender, EventArgs e)
        {
            VerActualesAtri(this, null);
            if (ent != null)
            {
                
                comboBoxModAtri.Visible = true;
                cambiaAtribu.Visible = true;
                buttonEliAtri.Visible = true;

                comboBoxModAtri.Items.Clear();
                for (int i = 0; i < ent.listAtrib.Count; i++)
                {
                    comboBoxModAtri.Items.Add(ent.listAtrib.ElementAt(i).nombre);
                }

                textBoxNomAtri.Text = "";
                comboBoxAtrTipo.Text = "";
                textBoxAtriLong.Text = "";
                comboBoxAtrTip_Ind.Text = "";
            }
            else
                MessageBox.Show("Elige una entidad y asegurate de que tenga atributos por modificar, si no crealos");
        }

        private void cambiaAtributo(object sender, EventArgs e)
        {
            int inAM = comboBoxModAtri.SelectedIndex;

            if (inAM != -1)
            {
                Atributo atM = ent.listAtrib.ElementAt(inAM);

                atM.nombre = textBoxNomAtri.Text;
                atM.tipo = comboBoxAtrTipo.Text.ElementAt(0);
                atM.longitud = Int32.Parse(textBoxAtriLong.Text);
                atM.tipoIndi = Int32.Parse(comboBoxAtrTip_Ind.SelectedIndex.ToString());

                buttonEliAtri.Visible = false;
                cambiaAtribu.Visible = false;
                comboBoxModAtri.Visible = false;
                comboBoxModAtri.Text = "";
                VerActualesAtri(this, null);
            }
            else
                MessageBox.Show("Selecciona un atributos, si no los hay crealos");

            


        }

        private void eliminaAtri(object sender, EventArgs e)
        {
            int inAM = comboBoxModAtri.SelectedIndex;


            if (inAM != -1)
            {

                using (BinaryWriter bw = new BinaryWriter(File.Open(dic.nomArchivo, FileMode.Open)))
                {
                    if (ent.listAtrib.Count != 1)
                    {
                        if (inAM - 1 != -1 && inAM + 1 < ent.listAtrib.Count())
                        {
                            bw.Seek((int)ent.listAtrib.ElementAt(inAM - 1).dirAtri + 60, SeekOrigin.Begin);
                            bw.Write(ent.listAtrib.ElementAt(inAM + 1).dirAtri);
                            r = 0;
                        }
                        else
                        {
                            if (inAM + 1 == ent.listAtrib.Count())
                            {
                                bw.Seek((int)ent.listAtrib.ElementAt(inAM - 1).dirAtri + 60, SeekOrigin.Begin);
                                bw.Write(-1);
                                r = 0;
                            }
                            else
                            {
                                bw.Seek((int)ent.dirEnti + 43, SeekOrigin.Begin);
                                bw.Write(ent.listAtrib.ElementAt(1).dirAtri);
                                r = 0;
                            }
                        }

                    }
                    else
                    {
                        r = 0;
                        bw.Seek((int)ent.dirEnti + 43, SeekOrigin.Begin);
                        bw.Write(-1);
                        ent.dirAtri = -1;


                    }
                }

                if (ent.listAtrib.Count != 1)
                {
                    r = 0;
                    dic.actualizaDiccionario(dic.archivo);
                }
                else
                    ent.listAtrib.Clear();
            }
            else
                MessageBox.Show("Selecciona un atributos, si no los hay crealos");

            buttonEliAtri.Visible = false;
            cambiaAtribu.Visible = false;
            comboBoxModAtri.Text = "";
            comboBoxModAtri.Visible = false;

            VerActualesAtri(this, null);
            r = 0;
        }

        private void CambioIndiceAtri(object sender, EventArgs e)
        {
            VerActualesAtri(this, null);
        }
    }
}
