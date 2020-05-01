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
    public partial class ventanaHashEsta : Form
    {
        CajonHash[] DirectorioHash;
        public FileStream archHashEsta;
        int r = 0;


        public ventanaHashEsta()
        {
            InitializeComponent();

            
        }

        public void agregaColumnas()
        {
            DataGridViewTextBoxColumn Columna1 = new DataGridViewTextBoxColumn();
            Columna1.HeaderText = "indiceDirectorio";
            directorioGrid.Columns.Add(Columna1);

            DataGridViewTextBoxColumn Columna2 = new DataGridViewTextBoxColumn();
            Columna2.HeaderText = "Direccion cajon";
            directorioGrid.Columns.Add(Columna2);

            DataGridViewTextBoxColumn Columna3 = new DataGridViewTextBoxColumn();
            Columna3.HeaderText = "Clave";
            cajonGrid.Columns.Add(Columna3);

            DataGridViewTextBoxColumn Columna4 = new DataGridViewTextBoxColumn();
            Columna4.HeaderText = "Direccion registro";
            cajonGrid.Columns.Add(Columna4);
        }

        public void setDirectorio(CajonHash[] Direc)
        {
            DirectorioHash = Direc;
        }

        public void agregaValoresTabla()
        {
            agregaColumnas();

            r = 0;
            
            for (int i = 0; i < DirectorioHash.Length; i++)
            {
                directorioGrid.Rows.Add();
                directorioGrid.Rows[i].Cells[0].Value = i;
                directorioGrid.Rows[i].Cells[1].Value = DirectorioHash[i].dirCajon;
            }
        }

        private void ventanaHashEsta_FormClosing(object sender, FormClosingEventArgs e)
        {
            directorioGrid.Rows.Clear();
            cajonGrid.Rows.Clear();
        }

        private void eventoCajonVer(object sender, DataGridViewCellEventArgs e)
        {
            r = 0;
            cajonGrid.Rows.Clear();

            int indCajon = Int32.Parse(directorioGrid.CurrentRow.Cells[0].Value.ToString());

            r = 0;
            List<campoCajonHash> campoMostr = DirectorioHash[indCajon].listaCampoCajonHash;
            r = 0;

            for (int i = 0; i < campoMostr.Count; i++)
            {
                cajonGrid.Rows.Add();
                cajonGrid.Rows[i].Cells[0].Value = campoMostr.ElementAt(i).clave;
                cajonGrid.Rows[i].Cells[1].Value = campoMostr.ElementAt(i).apunReg;
            }

            r = 0;
        }
    }
}
