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

            creaArchivosDat();

        }

        public void creaArchivosDat()
        {
            //"GUARDAR UN ARCHIVO"

            foreach(Entidad a in dic.listEntidad)
            {
                listEntDat.Add(new FileStream(a.nombre+".dat", FileMode.Create));
            }
            //dic.archivo = new FileStream(dic.nomArchivo, FileMode.Create);
            r = 0;
            /*
            //Abrir "Guardar como"
            SaveFileDialog saveDialog = new SaveFileDialog();
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                //Guardar el nombre seleccionado del cuadro de dialogo
                //nomArchivo = saveDialog.FileName;
                dic.nomArchivo = saveDialog.FileName;

                //Crear el nuevo archivo, asignado el nombre elegido

                dic.archivo = new FileStream(dic.nomArchivo, FileMode.Create);


                dic.archivo.Close();


            }

            label1.Text = "";

            label1.Text = "Archivo Diccionario:" + dic.nomArchivo;


            using (BinaryWriter bw = new BinaryWriter(File.Open(dic.nomArchivo, FileMode.Open)))
            {
                bw.Write(dic.cab);
            }*/
        }

        private void GeneraCol_Click(object sender, EventArgs e)
        {
            Object inEn = comboBoxEntiDatos.SelectedItem;
            r = 0;



            if (inEn != null)
            {
                Entidad entReg = dic.listEntidad.Find(x => x.nombre == inEn.ToString());
                foreach (Atributo a in entReg.listAtrib)
                {
                    DataGridViewTextBoxColumn Columna1 = new DataGridViewTextBoxColumn();
                    Columna1.HeaderText = a.nombre;
                    RegistroDataGrid.Columns.Add(Columna1);
                }
            }
            else
                MessageBox.Show("Escoge una entidad");
        }

        public void GeneraArchivos()
        {

        }

        private void GuardaRegistros_Click(object sender, EventArgs e)
        {
            int f = 0;
            int c = 0;

            for (int fila = 0; fila < RegistroDataGrid.Rows.Count - 1; fila++)//Saca los valores de la matriz de adyacencia 1
            {

                for (int col = 0; col < RegistroDataGrid.Rows[fila].Cells.Count; col++)
                {
                    string valor = RegistroDataGrid.Rows[fila].Cells[col].Value.ToString();
                    
                 
                    c++;
                }
                c = 0;
                f++;
            }

            MessageBox.Show("Tu registros fueron guardados satisfactoriamente");
            RegistroDataGrid.Rows.Clear();
            RegistroDataGrid.Columns.Clear();
            //RegistroDataGrid.Refresh();
            //RegistroDataGrid.DataSource = null;

        }
    }
}
