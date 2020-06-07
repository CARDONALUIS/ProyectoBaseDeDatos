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
     * Clase que se encarga de mostrar en forma grafica con una ventana el archivo del arbol
     * */
    public partial class ventanaArbol : Form
    {
        List<Nodo> lisNodo = new List<Nodo>();
        int r = 0;
        public FileStream archArbol;
        int n = 5;//Grado
        bool bandElim = false;


        //Constructo de la clase 
        public ventanaArbol()
        {
            InitializeComponent();           
        }

        //Metodo que se utiliza para agregar las columnas del dataGrid
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

        //Metodo para setear la lista de nodos y la conosca la clase
        public void setListaNodo(List<Nodo> _lisNodo)
        {
            lisNodo = _lisNodo;

        }


        //Metodo que agrega los valores a la tabla para la visualisacion de estos
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


                archArbol.Close();
                archArbol = File.Open(archArbol.Name, FileMode.Open);
                BinaryReader br = new BinaryReader(archArbol);
                archArbol.Seek(lisNodo.ElementAt(i).dirNodo + 57, SeekOrigin.Begin);

                long valFinPad;
                int valFinHoja;

                if (lisNodo.ElementAt(i).tipo == 'H')
                {
                    valFinHoja = br.ReadInt32();
                    arbolGrid.Rows[i].Cells[10].Value = valFinHoja;
                }
                else
                {
                    valFinPad = br.ReadInt64();
                    r = 0;
                    if (valFinPad != -1 && n-1 == lisNodo.ElementAt(i).K.Count)
                        arbolGrid.Rows[i].Cells[10].Value = valFinPad;


                }




                archArbol.Close();

                auxK = 3;
                auxP = 2;
                r = 0;


            }
        }


        //Metodo para cuando se cierre la pantalla se borren los filas y cuando se vuelva abrir se creen denuevo
        private void ventanaArbol_FormClosing(object sender, FormClosingEventArgs e)
        {
            arbolGrid.Rows.Clear();
            
        }
    }
}
