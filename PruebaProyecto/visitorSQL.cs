using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;



namespace PruebaProyecto
{
    public class visitorSQL : GramaticaSQLBaseVisitor<int>
    {
        int r = 0;
        public Diccionario BD;
        public List<string> lisatrCon;

        

        public visitorSQL(Diccionario bd)
        {
            BD = bd;
            lisatrCon = new List<string>();
        }


        public override int VisitAste([NotNull] GramaticaSQLParser.AsteContext context)
        {
            r = 0;   
            return base.VisitAste(context);
        }

        public override int VisitAtrTab([NotNull] GramaticaSQLParser.AtrTabContext context)
        {
            string algo = context.NOM().GetText();

            lisatrCon.Add(algo);
            r = 0;
            return base.VisitAtrTab(context);
        }

        public override int VisitNomTabla([NotNull] GramaticaSQLParser.NomTablaContext context)
        {
            string nomTab = context.GetText();

            r = 0;
            /* List<Entidad> nueav = new List<Entidad>(BD.listEntidad);
             r = 0;
             nueav.ElementAt(0).nombre = "OTRA";*/

            /*List<Entidad> nueav = (BD.listEntidad as IEnumerable<Entidad>).ToList();
            r = 0;
            nueav.ElementAt(0).nombre = "OTRA";
            */

            /*
            IFormatter f = new BinaryFormatter();
            List<Entidad> nuevaLista = null;
            using (Stream ms = new MemoryStream())
            {
                f.Serialize(ms, BD.listEntidad);
                //Ahora se des-serializa, creando una copia profunda y 100% independiente:
                nuevaLista = f.Deserialize(ms) as List<Entidad>;
            }

            nuevaLista.ElementAt(0).nombre = "OT";*/


            Entidad tablaSeleccionada = new Entidad();
            tablaSeleccionada = BD.listEntidad.Find(x => x.nombre.ToUpper() == nomTab);


            MessageBox.Show("Tabla Seleccionada "+tablaSeleccionada.nombre);


            for (int i = 0; i < lisatrCon.Count; i++)
            {
                foreach (Atributo a in tablaSeleccionada.listAtrib)
                {
                    if(a.nombre.ToUpper() == lisatrCon[i])
                    {
                        MessageBox.Show("Atributo seleccionado " + a.nombre);
                        r = 0;
                        //VOY ABRIR EL ARCHIVO DE RESGISTROS CON a.INDI  ME QUEDE AQUI
                    }
                }
            }

            

            r = 0;



            return base.VisitNomTabla(context);
        }



    }
}
