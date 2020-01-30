using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PruebaProyecto
{
    public partial class VenEntidades : Form
    {
        Diccionario dic;
        int r = 0;
        

        public VenEntidades()
        {
            InitializeComponent();
        }

        private void VenEntidades_Load(object sender, EventArgs e)
        {
            dic = new Diccionario();
        }

        private void Button1_Click(object sender, EventArgs e)//Botono de agregar Entidad
        {
            String nomEntid = "";

            //dic.vaActEnt = dic.vaActEnt + dic.tamEntidad;
            dic.cab = dic.vaActEnt;
            nomEntid = textEntid.Text;
            Entidad en;

            if (dic.listEntidad.Count == 0)
            {
                en = new Entidad(2568589, nomEntid, dic.vaActEnt, dic.vaActEnt + dic.tamEntidad, -1, -1);
            }
            else
            {
                en = new Entidad(2568589, nomEntid, dic.vaActEnt, dic.vaActEnt + dic.tamEntidad, -1, -1);
            }
            r = 0;
            dic.listEntidad.Add(en);
            

        }

        private void AgregarAtri_Click(object sender, EventArgs e)
        {
            if (textNomAtri.Text != "")
            {
                string nom = textNomAtri.Text;
                if (textTipo.Text != "")
                {
                    if (textTipo.Text.Length == 1)
                    {
                        char tI = textTipo.Text[0];
                        if (textTipIn.Text != "")
                        {
                            int tipoInd = Int32.Parse(textTipIn.Text);
                            if (textTama.Text != "")
                            {
                                int ta = Int32.Parse(textTama.Text);
                                //Atributo at = new Atributo(55, nom, tI, ta, dic.vaActAtr, tipoInd, -1, dic.vaActAtr + dic.tamAtrib);
                                
                            }
                            else
                                MessageBox.Show("Falta rellenar tamaño");
                        }
                        else   
                            MessageBox.Show("Falta rellenar Tipo Indice");

                    }
                    else
                        MessageBox.Show("Su tipo debe espesificarse con C = caracter o E = entero");
                }
                else
                    MessageBox.Show("Falta rellenar Tipo");
            }
            else
                MessageBox.Show("Falta rellenar Nombre");


        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }
    }
}
