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
    public partial class VentanaIndPrim : Form
    {
        List<IndicePrimario> listIndPrim = new List<IndicePrimario>();
        public VentanaIndPrim()
        {
            InitializeComponent();


            DataGridViewTextBoxColumn Columna1 = new DataGridViewTextBoxColumn();
            Columna1.HeaderText = "Clave Primaria";
            dataGridIndPrim.Columns.Add(Columna1);

            DataGridViewTextBoxColumn Columna2 = new DataGridViewTextBoxColumn();
            Columna2.HeaderText = "Direccion";
            dataGridIndPrim.Columns.Add(Columna2);

            
        }

        public void agregaIndTabla()
        {
            
            int r = 0;
            //List<IndicePrimario> orde = new List<IndicePrimario>();
            //orde = listIndPrim;
            //orde = orde.OrderBy(x => x.clv_prim).ToList();
            r = 0;
            

            for(int i = 0; i < listIndPrim.Count; i++)
            {
                dataGridIndPrim.Rows.Add();
                dataGridIndPrim.Rows[i].Cells[0].Value = listIndPrim.ElementAt(i).clv_prim;
                dataGridIndPrim.Rows[i].Cells[1].Value = listIndPrim.ElementAt(i).dir_reg;
            }
            r = 0;
        }

        public void asignaListInd(List<IndicePrimario> LIP)
        {
            listIndPrim = LIP;
            agregaIndTabla();
        }

    }
}
