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
    public partial class ventanaArbol : Form
    {
        List<Nodo> lisNodo = new List<Nodo>();
        int r = 0;
        public ventanaArbol()
        {
            InitializeComponent();

           
            

        }

        public void agregaColumnas()
        {
            DataGridViewTextBoxColumn Columna1 = new DataGridViewTextBoxColumn();
            Columna1.HeaderText = "DirNodo";
            arbolGrid.Columns.Add(Columna1);

            DataGridViewTextBoxColumn Columna2 = new DataGridViewTextBoxColumn();
            Columna2.HeaderText = "Tipo";
            arbolGrid.Columns.Add(Columna2);

            DataGridViewTextBoxColumn Columna3 = new DataGridViewTextBoxColumn();
            Columna3.HeaderText = "DirReg";
            arbolGrid.Columns.Add(Columna3);

            DataGridViewTextBoxColumn Columna4 = new DataGridViewTextBoxColumn();
            Columna4.HeaderText = "Clave";
            arbolGrid.Columns.Add(Columna4);

            DataGridViewTextBoxColumn Columna5 = new DataGridViewTextBoxColumn();
            Columna5.HeaderText = "DirReg";
            arbolGrid.Columns.Add(Columna5);

            DataGridViewTextBoxColumn Columna6 = new DataGridViewTextBoxColumn();
            Columna6.HeaderText = "Clave";
            arbolGrid.Columns.Add(Columna6);

            DataGridViewTextBoxColumn Columna7 = new DataGridViewTextBoxColumn();
            Columna7.HeaderText = "DirReg";
            arbolGrid.Columns.Add(Columna7);

            DataGridViewTextBoxColumn Columna8 = new DataGridViewTextBoxColumn();
            Columna8.HeaderText = "Clave";
            arbolGrid.Columns.Add(Columna8);

            DataGridViewTextBoxColumn Columna9 = new DataGridViewTextBoxColumn();
            Columna9.HeaderText = "DirReg";
            arbolGrid.Columns.Add(Columna9);

            DataGridViewTextBoxColumn Columna10 = new DataGridViewTextBoxColumn();
            Columna10.HeaderText = "Clave";
            arbolGrid.Columns.Add(Columna10);

            DataGridViewTextBoxColumn Columna11 = new DataGridViewTextBoxColumn();
            Columna11.HeaderText = "DirSigNodo";
            arbolGrid.Columns.Add(Columna11);
        }

        public void setListaNodo(List<Nodo> _lisNodo)
        {
            lisNodo = _lisNodo;

        }

        public void agregaValoresTabla()
        {
            agregaColumnas();

            int auxK = 3;
            int auxP = 2;

            for (int i = 0; i < lisNodo.Count; i++)
            {
                arbolGrid.Rows.Add();
                arbolGrid.Rows[i].Cells[0].Value = lisNodo.ElementAt(i).dirNodo;
                arbolGrid.Rows[i].Cells[1].Value = lisNodo.ElementAt(i).tipo;

               // if (lisNodo.ElementAt(i).tipo == 'H' || lisNodo.ElementAt(i).dirNodo == 0)
                //{
                    r = 0;


                    for (int l = 0; l < lisNodo.ElementAt(i).P.Count(); l++)
                    {
                        r = 0;
                        arbolGrid.Rows[i].Cells[auxP].Value = lisNodo.ElementAt(i).P.ElementAt(l);
                        auxP += 2;
                        r = 0;
                    }

                    for (int j = 0; j < lisNodo.ElementAt(i).K.Count(); j++)
                    {
                        r = 0;
                        arbolGrid.Rows[i].Cells[auxK].Value = lisNodo.ElementAt(i).K.ElementAt(j);
                        auxK += 2;
                        r = 0;
                    }

                auxK = 3;
                auxP = 2;
                r = 0;

                /*}
                else
                {
                    
                    for (int j = 2; j < lisNodo.ElementAt(i).P.Count() + 2; i++)
                    {
                        arbolGrid.Rows[i].Cells[j].Value = lisNodo.ElementAt(j - 2).K;                   
                        arbolGrid.Rows[i].Cells[j + 1].Value = lisNodo.ElementAt(j - 2).P;
                        arbolGrid.Rows[i].Cells[j].Value = lisNodo.ElementAt(j - 1).K;
                    }
                    

                }*/

            }
        }

        private void ventanaArbol_FormClosing(object sender, FormClosingEventArgs e)
        {
            arbolGrid.Rows.Clear();
            
        }
    }
}
