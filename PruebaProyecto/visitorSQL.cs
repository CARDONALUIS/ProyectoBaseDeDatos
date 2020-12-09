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
        public List<string> lisAtrCon;
        public bool banRecDatoAtr = true;
        public bool bandRecDatoEnt = true;
        public List<Entidad> lisTabCons;
        
        

        

        public visitorSQL(Diccionario bd)
        {
            BD = bd;
            lisAtrCon = new List<string>();
            lisTabCons = new List<Entidad>();

            //listEntRef = new List<Entidad>();
        }


        public override int VisitFinConsulta([NotNull] GramaticaSQLParser.FinConsultaContext context)
        {
            r = 0;
            agregaAtrSeleccionadosATab();

            //agregaInfoGrid();



            
            //Entidad tablaSeleccionada = new Entidad();
            //tablaSeleccionada = BD.listEntidad.Find(x => x.nombre.ToUpper() == nomTab);


            return base.VisitFinConsulta(context);
        }

        public override int VisitAste([NotNull] GramaticaSQLParser.AsteContext context)
        {
            r = 0;   
            return base.VisitAste(context);
        }

        public override int VisitAtrTab([NotNull] GramaticaSQLParser.AtrTabContext context)
        {
            if (banRecDatoAtr)
            {
                string cadAtr = context.GetText();
                cadAtr = cadAtr.Substring(1, cadAtr.Length - 1);


                string[] arAtr = cadAtr.Split(' ');

                for (int i = 0; i < arAtr.Length; i++)
                {
                    lisAtrCon.Add(arAtr[i]);
                }

                banRecDatoAtr = false;
            }

           
            r = 0;
            return base.VisitAtrTab(context);
        }

        /*public override int VisitRecurTab([NotNull] GramaticaSQLParser.RecurTabContext context)
        {
            string nomTab = context.GetText();
            r = 0;

            return base.VisitRecurTab(context);
        }*/

        public override int VisitNomTabla([NotNull] GramaticaSQLParser.NomTablaContext context)
        {
            if (bandRecDatoEnt)
            {
                string cadTab = context.GetText();
                cadTab = cadTab.Substring(1, cadTab.Length - 1);


                string[] arEnt = cadTab.Split(' ');

                for (int i = 0; i < arEnt.Length; i++)
                {

                    Entidad tablaSeleccionada = new Entidad();

                    tablaSeleccionada.nombre = BD.listEntidad.Find(x => x.nombre.ToUpper() == arEnt[i].ToUpper()).nombre;
                    //tablaSeleccionada = asignaAtributosTabSele(BD.listEntidad.Find(x => x.nombre.ToUpper() == arEnt[i].ToUpper()), tablaSeleccionada);
                    lisTabCons.Add(tablaSeleccionada);
                    
                }

                bandRecDatoEnt = false;
            }


            

            /*
            ///
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
            */

            r = 0;



            return base.VisitNomTabla(context);
        }


        public void agregaAtrSeleccionadosATab()
        {
            foreach(string a in lisAtrCon)
            {
                Entidad enSe = lisTabCons.Find(x => x.nombre.ToUpper() == a.Split('.')[0]);
                enSe.listAtrib = new List<Atributo>();
                enSe.listAtrib.Add((Atributo)BD.listEntidad.Find(x => x.nombre == enSe.nombre).listAtrib.Find(x => x.nombre.ToUpper() == a.Split('.')[1]).Clone());
                r = 0;     
            }

        }

        public Entidad asignaAtributosTabSele(Entidad tabCon, Entidad tabNue)
        {
            tabNue.nombre = tabCon.nombre;
            tabNue.listAtrib = new List<Atributo>();

            foreach (Atributo a in tabCon.listAtrib)
            {
                Atributo nu = (Atributo)a.Clone();
                tabNue.listAtrib.Add(nu);
                r = 0;
            }

            return tabNue;
        }       

    }    
}
