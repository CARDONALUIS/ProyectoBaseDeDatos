using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System.IO;



namespace PruebaProyecto
{
    public partial class FormSMBD : Form
    {
        public Diccionario BD;
        public FormSMBD()
        {
            InitializeComponent();
        }

        private void FormSMBD_Load(object sender, EventArgs e)
        {
            iniColumna();
            
        }

        public void iniColumna()
        {
            dataGridViewResConsulta.RowHeadersVisible = false;

            //DataGridViewTextBoxColumn Columna1 = new DataGridViewTextBoxColumn();
            //Columna1.HeaderText = " ";
            //dataGridViewResConsulta.Columns.Add(Columna1);
        }

        private void botonEjecuta(object sender, EventArgs e)
        {
            string cadAnalisar = richTextBoxConsulta.Text.ToUpper();

            AntlrInputStream input = new AntlrInputStream(cadAnalisar);
            GramaticaSQLLexer lexer = new GramaticaSQLLexer(input);
            CommonTokenStream token = new CommonTokenStream(lexer);
            GramaticaSQLParser parser = new GramaticaSQLParser(token);

            IParseTree tree = parser.consulta();
            MessageBox.Show(tree.ToStringTree(parser));

            visitorSQL visitor = new visitorSQL(BD, dataGridViewResConsulta);
            visitor.Visit(tree);

            //agregaInfoAGrid(visitor);
            /*foreach(Entidad a in visitor.lisTabCons)
            {
                MessageBox.Show("Tabla: " + a.nombre+" Atributo: ");
                foreach(Atributo b in a.listAtrib)
                {
                    MessageBox.Show(b.nombre);
                }
                    
            }*/

        }

        
    }
}
