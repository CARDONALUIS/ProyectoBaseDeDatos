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
    /*
     * Clase que se utiliza para visualizar los indices primarios
     */
    public partial class VentanaIndPrim : Form
    {
        List<IndicePrimario> listIndPrim = new List<IndicePrimario>();


       //Constructor de la clase lo utilizo para agregar columnas a los Datagrid
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


        //Metodo que agrega a los dataGrid la correspondiente informacion para su mejor visualizacion
        public void agregaIndTabla()
        {
            
            int r = 0;
            r = 0;
            

            for(int i = 0; i < listIndPrim.Count; i++)
            {
                dataGridIndPrim.Rows.Add();
                dataGridIndPrim.Rows[i].Cells[0].Value = listIndPrim.ElementAt(i).clv_prim;
                dataGridIndPrim.Rows[i].Cells[1].Value = listIndPrim.ElementAt(i).dir_reg;
            }
            r = 0;
        }

        //Metodo para que la clase consoca la lista de indices y empieze a agregar los valores
        public void asignaListInd(List<IndicePrimario> LIP)
        {
            listIndPrim = LIP;
            agregaIndTabla();
        }

    }
}
