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
        bool bandAst = false;
        DataGridView gridTabla;

        public visitorSQL(Diccionario bd, DataGridView tab)
        {
            BD = bd;
            gridTabla = tab;
            direccionRealAtr();
            abreArchivosDatosEntidades();
            lisAtrCon = new List<string>();
            lisTabCons = new List<Entidad>();
            

            //listEntRef = new List<Entidad>();
        }


        public void limpiaGridAct()
        {
            gridTabla.Rows.Clear();
            gridTabla.Columns.Clear();
        }


        public override int VisitFinConsulta([NotNull] GramaticaSQLParser.FinConsultaContext context)
        {
            r = 0;
            //agregaAtrSeleccionadosATab();
            //agregaColuAGrid();
            //agregaRegSeleccionadosATab();
            muestraGridCon();
            
            
            
            cierraArchivosDatosEntidades();
           
            return base.VisitFinConsulta(context);
        }

        public void muestraGridCon()
        {
            int ren = 0;
            int col = 0;
            bool bandRengAg = true;

            foreach(Entidad a in lisTabCons)
            {
                
                foreach(Atributo b in a.listAtrib)
                {
                    

                    
                    foreach(string c in b.regAtr)
                    {
                        if (bandRengAg)
                            gridTabla.Rows.Add();

                        gridTabla.Rows[ren].Cells[col].Value = c;
                        ren++;
                    }

                    if (bandRengAg)
                        bandRengAg = false;

                    ren = 0;
                    col++;
                }
                col++;
            }
        }


        public override int VisitConCondicion([NotNull] GramaticaSQLParser.ConCondicionContext context)
        {
            //string algo = 
            string cadeCondi = context.GetText();
           

            if (cadeCondi != "")
            {
                string parTab = cadeCondi.Split(context.operador().GetText()[0])[0];
                parTab = parTab.Substring(1, parTab.Length - 1);
                string refTab = parTab.Split(' ')[1];

                string aComparar = cadeCondi.Split(context.operador().GetText()[0])[1];
                aComparar = aComparar.Substring(1, aComparar.Length - 1);

                string operador = context.operador().GetText();

                bool bandAtrEncon = false;
                List<int> indSele = new List<int>(); ;
                int i = 0;

                foreach (Entidad a in lisTabCons)
                {
                    foreach (Atributo b in a.listAtrib)
                    {
                        //List<string> nuevReg = new List<string>();                        
                        i = 0;

                        if (a.nombre.ToUpper() + "." + b.nombre.ToUpper() == refTab)
                        {
                            foreach (string c in b.regAtr)
                            {
                                //if (c.ToUpper() == aComparar)
                                //{
                                    r = 0;
                                    //////////////////////////////////


                                    bool valAc = hazComparacion(operador, c.ToUpper(), aComparar, b.tipo);
                                    if (valAc)
                                    {
                                        //nuevReg.Add(c);
                                        indSele.Add(i);
                                    }
                                    
                                //}                                
                                i++;
                            }
                            bandAtrEncon = true;

                        }
                        if(bandAtrEncon)
                            break;
                    }
                    if(bandAtrEncon)
                        break;
                }
                actualizaRegCons(indSele);
            }

            r = 0;
            return base.VisitConCondicion(context);
        }

        public void actualizaRegCons(List<int> indSele)
        {
            int i = 0;
            List<string> nuevCadReg = new List<string>();

            foreach (Entidad a in lisTabCons)
            {
                i = 0;
                foreach(Atributo b in a.listAtrib)
                {
                    i = 0;
                    nuevCadReg = new List<string>();
                    foreach(string c in b.regAtr)
                    {
                        int ind = indSele.FindIndex(x => x == i);
                        if (ind != -1)
                        {
                            nuevCadReg.Add(c);
                        }
                        i++;
                    }
                    b.regAtr = nuevCadReg;
                    

                    i++;

                }
            }
        }


        public bool hazComparacion(string op, string datoACom, string comparador, char tipoDato)
        {
            bool band = false;

            switch (op)
            {
                case "=":                                                         
                    if(datoACom == comparador)
                    {
                        band = true;
                    }
                    break;
                case "<>":
                    if (datoACom != comparador.Trim(' '))
                    {
                        band = true;
                    }
                    break;
                case ">":
                    if(tipoDato == 'C')
                    {
                        MessageBox.Show("No puedes saber si un cadena es mayor que otra");
                    }
                    else
                    {
                        int dCom = Convert.ToInt32(datoACom);
                        int comp = Convert.ToInt32(comparador);

                        if(dCom > comp)
                        {
                            band = true;
                        }
                    }
                    break;
                case "<":
                    if (tipoDato == 'C')
                    {
                        MessageBox.Show("No puedes saber si un cadena es mayor que otra");
                    }
                    else
                    {
                        int dCom = Convert.ToInt32(datoACom);
                        int comp = Convert.ToInt32(comparador);

                        if (dCom < comp)
                        {
                            band = true;
                        }
                    }
                    break;
                case ">=":
                    if (tipoDato == 'C')
                    {
                        MessageBox.Show("No puedes saber si un cadena es mayor igual que otra");
                    }
                    else
                    {
                        int dCom = Convert.ToInt32(datoACom);
                        int comp = Convert.ToInt32(comparador);

                        if (dCom >= comp)
                        {
                            band = true;
                        }
                    }
                    break;
                case "<=":
                    if (tipoDato == 'C')
                    {
                        MessageBox.Show("No puedes saber si un cadena es mayor que otra");
                    }
                    else
                    {
                        int dCom = Convert.ToInt32(datoACom);
                        int comp = Convert.ToInt32(comparador);

                        if (dCom <= comp)
                        {
                            band = true;
                        }
                    }
                    break;
            }

            return band;
        }


        public override int VisitAste([NotNull] GramaticaSQLParser.AsteContext context)
        {
            r = 0;
            limpiaGridAct();

            bandAst = true;
            return base.VisitAste(context);
        }



        public override int VisitAtrTab([NotNull] GramaticaSQLParser.AtrTabContext context)
        {
            limpiaGridAct();

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

                agregaAtrSeleccionadosATab();
                agregaColuAGrid();
                agregaRegSeleccionadosATab();
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

            

            if (!bandAst)
            {
                foreach (string a in lisAtrCon)
                {
                    Entidad enSe = lisTabCons.Find(x => x.nombre.ToUpper() == a.Split('.')[0]);
                    enSe.listAtrib = new List<Atributo>();
                }

                foreach (string a in lisAtrCon)
                {
                    Entidad enSe = lisTabCons.Find(x => x.nombre.ToUpper() == a.Split('.')[0]);
                    enSe.listAtrib.Add((Atributo)BD.listEntidad.Find(x => x.nombre == enSe.nombre).listAtrib.Find(x => x.nombre.ToUpper() == a.Split('.')[1]).Clone());
                    r = 0;
                }
            }
            else
            {
                //foreach ()
                //{
                //Entidad enSe = lisTabCons.Find(x => x.nombre.ToUpper() == a.Split('.')[0]);
                //enSe.listAtrib = new List<Atributo>();
                //}
                bandAst = false;

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
            int ren = 0;
            int col = 0;
            

            foreach (Entidad a in lisTabCons)
            {
                
                r = 0;
                
                foreach (Atributo b in a.listAtrib)
                {

                    BinaryReader br = new BinaryReader(a.archivoDat);
                    ren = 0;
                    b.regAtr = new List<string>(); 
                   
                    int tam = b.dirArDat;
                    
                    
                    

                    while (tam <= a.archivoDat.Length)
                    {
                        a.archivoDat.Seek(tam, SeekOrigin.Begin);
                        string dato="";

                        switch (b.tipo)
                        {
                            case 'C':
                                r = 0;
                                dato = new string(br.ReadChars(b.longitud)).Trim('-');
                                //algo.Add(dato);
                                break;
                            case 'E':
                                dato = br.ReadInt32().ToString();
                                r = 0;
                                break;
                            case 'F':
                                dato = br.ReadSingle().ToString();
                                r = 0;
                                break;
                        }
                        tam += a.longAtributos;
                        //gridTabla.Rows[ren].Cells[col].Value = dato;
                        b.regAtr.Add(dato);
                        
                        r = 0;
                        ren++;
                        
                    }
                    col++;

                }
                col++;
                r = 0;
            }                       
        }

        public void cierraArchivosDatosEntidades()
        {
            foreach (Entidad a in BD.listEntidad)
            {
                a.archivoDat.Close();
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

        public void agregaColuAGrid()
        {
            foreach (Entidad a in lisTabCons)
            {
                foreach (Atributo b in a.listAtrib)
                {
                    DataGridViewTextBoxColumn Columna1 = new DataGridViewTextBoxColumn();
                    Columna1.HeaderText = a.nombre+"."+b.nombre;
                    gridTabla.Columns.Add(Columna1);
                }
            }
        }

    }    
}
