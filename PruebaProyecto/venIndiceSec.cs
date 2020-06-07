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
    /*
     * Clase que se utiliza para la visualizacion de forma grafica el indice secundario 
     */
    public partial class venIndiceSec : Form
    {
        int r = 0;
        public Entidad entAct;
        IndiceSecundario indActGlob;


        //Metodo constructor que se utiliza para inicializar algunas columnas en forma grafica
        public venIndiceSec()
        {
            InitializeComponent();

            DataGridViewTextBoxColumn Columna1 = new DataGridViewTextBoxColumn();
            Columna1.HeaderText = "Clave Secundaria";
            llaveSecGrid.Columns.Add(Columna1);

            DataGridViewTextBoxColumn Columna2 = new DataGridViewTextBoxColumn();
            Columna2.HeaderText = "Direccion cajon";
            llaveSecGrid.Columns.Add(Columna2);

            DataGridViewTextBoxColumn Columna3 = new DataGridViewTextBoxColumn();
            Columna3.HeaderText = "Direccion Reg";
            cajonGrid.Columns.Add(Columna3);

        }


        //Metodo que agrega las diferntes llaves secundarias de la entidad en las combobox
        public void asignaValores()
        {
            foreach (Atributo a in entAct.listAtrib)
            {
                if (a.tipoIndi == 3)
                {
                    atriLlavSec.Items.Add(a.nombre);
                }
            }
          

        }


        //Evento que actualiza las claves secundarias dado que selccion una en el comboBox y las muestra en los dataGrids
        private void cambAtrSec(object sender, EventArgs e)
        {

            llaveSecGrid.Rows.Clear();

            int atrSel = atriLlavSec.SelectedIndex;

            IndiceSecundario indAct = entAct.lisIndSec.Find(x => x.contIndSec == atrSel);
            indActGlob = indAct;

            r = 0;

            indAct.archSec = File.Open(indAct.archSec.Name, FileMode.Open);
            BinaryReader br = new BinaryReader(indAct.archSec);
            Object comp;

            indAct.archSec.Seek(0, SeekOrigin.Begin);

            if (indAct.tipo == 'C')
            {
                comp = new string(br.ReadChars(indAct.longAtrSec));
            }
            else
            {
                comp = br.ReadInt32();
            }

            int posIni = 0;


            int fila = 0;
            
            while (comp.ToString() != "-1" && char.IsLetterOrDigit(comp.ToString().ElementAt(0)))
            {

                IndicePrimario indP = new IndicePrimario();

                indAct.archSec.Seek(posIni, SeekOrigin.Begin);
                if (indAct.tipo== 'C')
                {
                    indP.clv_prim = new string(br.ReadChars(indAct.longAtrSec));
                }
                else
                {
                    indP.clv_prim = br.ReadInt32();
                }


                indP.dir_reg = br.ReadInt32();
                r = 0;
                llaveSecGrid.Rows.Add();
                llaveSecGrid.Rows[fila].Cells[0].Value = indP.clv_prim;
                llaveSecGrid.Rows[fila].Cells[1].Value = indP.dir_reg;


                r = 0;
                posIni = posIni + indAct.longBloqSec;

                indAct.archSec.Seek(posIni, SeekOrigin.Begin);
                r = 0;
                if (indAct.tipo == 'C')
                {
                    comp = new string(br.ReadChars(indAct.longAtrSec));
                }
                else
                {
                    comp = br.ReadInt32();
                }

                r = 0;
                fila++;
            }

            indAct.archSec.Close();
        }


        //Evento para dar click en la llave para agregar marcos//Es con fines de prueba
        private void agregaMarcos_evento(object sender, EventArgs e)
        {
            r = 0;
        }


        //Evento para mostrar el cajon que se quiere ver dado el click del renglon o fila de la llave secundaria
        private void agregaMarcosGrid_click(object sender, DataGridViewCellEventArgs e)
        {
            r = 0;
            cajonGrid.Rows.Clear();

            int dirLeerMarco = Int32.Parse(llaveSecGrid.CurrentRow.Cells[1].Value.ToString());

            indActGlob.archSec = File.Open(indActGlob.archSec.Name, FileMode.Open);
            BinaryReader br = new BinaryReader(indActGlob.archSec);
            Object comp;

            indActGlob.archSec.Seek(dirLeerMarco, SeekOrigin.Begin);

            comp = br.ReadInt32();
            int fila = 0;

            r = 0;
            while (comp.ToString() != "-1")
            {

                cajonGrid.Rows.Add();
                cajonGrid.Rows[fila].Cells[0].Value = comp;

                r = 0;

                dirLeerMarco += 8;
                indActGlob.archSec.Seek(dirLeerMarco, SeekOrigin.Begin);

                comp = br.ReadInt32();

                fila++;
                r = 0;

            }

            indActGlob.archSec.Close();


        }
    }
}
