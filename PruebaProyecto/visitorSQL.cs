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
            direccionRealAtr();
            abreArchivosDatosEntidades();
            lisAtrCon = new List<string>();
            lisTabCons = new List<Entidad>();
            

            //listEntRef = new List<Entidad>();
        }


        public override int VisitFinConsulta([NotNull] GramaticaSQLParser.FinConsultaContext context)
        {
            r = 0;
            agregaAtrSeleccionadosATab();
            agregaRegSeleccionadosATab();
            r = 0;
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

                    Entidad tablaSeleccionada;

                    tablaSeleccionada = (Entidad)BD.listEntidad.Find(x => x.nombre.ToUpper() == arEnt[i].ToUpper()).Clone();
                    
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
            foreach (string a in lisAtrCon)
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

        public void agregaRegSeleccionadosATab()
        {
            List<string> algo = new List<string>();

            foreach(Entidad a in lisTabCons)
            {
                
                r = 0;
                foreach (Atributo b in a.listAtrib)
                {

                    BinaryReader br = new BinaryReader(a.archivoDat);

                    int i = 0;
                    int tam = 12;
                    while (i < 2)
                    {
                        a.archivoDat.Seek(tam, SeekOrigin.Begin);
                        string algo2 = new string(br.ReadChars(20));
                        algo.Add(algo2);
                        tam += a.longAtributos;
                        i++;
                        

                    }
                }
                r = 0;
                //r = 0;
                //a.archivoDat.Seek(0, SeekOrigin.Begin);


            }                       
        }

        public void abreArchivosDatosEntidades()
        {
            foreach(Entidad a in BD.listEntidad)
            {
                a.archivoDat = File.Open(BitConverter.ToString(a.id_enti) + ".dat", FileMode.Open);
                r = 0;
                //entAct.archivoIndPri = File.Open(entAct.archivoIndPri.Name, FileMode.Open);
            }
        }

        public void direccionRealAtr()
        {
            int tamDatAtr;
            
            foreach (Entidad a in BD.listEntidad)
            {
                tamDatAtr = 8;
                foreach(Atributo b in a.listAtrib)
                {
                    b.dirArDat = tamDatAtr;
                    r = 0;
                    tamDatAtr = tamDatAtr + b.longitud;
                    r = 0;
                }
                r = 0;
                a.longAtributos = tamDatAtr + 8;
            }
            r = 0;
        }

    }    
}
