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
    public partial class venIndiceSec : Form
    {
        int r = 0;
        public Entidad entAct;

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

        private void cambAtrSec(object sender, EventArgs e)
        {

            llaveSecGrid.Rows.Clear();

            int atrSel = atriLlavSec.SelectedIndex;

            //Atributo atrMost = entAct.listAtrib.Find(x => x.nombre == atrSel);
            IndiceSecundario indAct = entAct.lisIndSec.Find(x => x.contIndSec == atrSel);


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
                //if (entAct.tipoCvePrima == 'C')
                //{

                //Objeto para guardar datos genericos
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

                //listIndPri.Add(indP);

                r = 0;
                posIni = posIni + indAct.longBloqSec;

                //comp.ToString() = new string(br.ReadChars(entAct.longClvPrim).ToString().ToCharArray());
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
                //}
            }

            indAct.archSec.Close();
            //int inEM = comboBoxModEnt.SelectedIndex;
        }
    }
}
