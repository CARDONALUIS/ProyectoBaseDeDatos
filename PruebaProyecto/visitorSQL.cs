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
        public Entidad TabCons;
        bool bandAst = false;
        DataGridView gridTabla;
        string tabConPrin;

        public visitorSQL(Diccionario bd, DataGridView tab)
        {
            BD = bd;
            gridTabla = tab;
            direccionRealAtr();
            abreArchivosDatosEntidades();
            vinculaLlavesForaneas();
            agregRegBD();
            lisAtrCon = new List<string>();
            TabCons = new Entidad();
            
        }


        public void limpiaGridAct()
        {
            gridTabla.Rows.Clear();
            gridTabla.Columns.Clear();
        }


        public override int VisitFinConsulta([NotNull] GramaticaSQLParser.FinConsultaContext context)
        {
            r = 0;
            muestraGridCon();
            
            
            
            cierraArchivosDatosEntidades();
           
            return base.VisitFinConsulta(context);
        }

        public void muestraGridCon()
        {
            int ren = 0;
            int col = 0;
            bool bandRengAg = true;


            foreach(Atributo b in TabCons.listAtrib)
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
              
        }


        public override int VisitConInnerJoin([NotNull] GramaticaSQLParser.ConInnerJoinContext context)
        {
            string tab2 = context.NOM(0).GetText();
            hazCombinacionReg(tab2);
            
            
            r = 0;


            return base.VisitConInnerJoin(context);
        }

        public void hazCombinacionReg(string tabla2)
        {
            //ValidarLlaveForanea
            Entidad tab1 = BD.listEntidad.Find(x => x.nombre.ToUpper() == tabConPrin);
            Entidad tab2 = BD.listEntidad.Find(x => x.nombre.ToUpper() == tabla2);
            

            Entidad entCom = new Entidad();
            entCom.listAtrib = new List<Atributo>();

            foreach(Atributo a in tab1.listAtrib)
            {
                Atributo nu = (Atributo)a.Clone();
                nu.regAtr = new List<string>();

                entCom.listAtrib.Add(nu);
            }
            foreach (Atributo a in tab2.listAtrib)
            {
                Atributo nu = (Atributo)a.Clone();
                nu.regAtr = new List<string>();

                entCom.listAtrib.Add(nu);
            }

            int ren = 0;
            int col = 0;
            int ren2 = 0;
            int col2 = 0;

            
            
            
            /*
            foreach (Atributo b in tab1.listAtrib)
            {
                List<string> cad = new List<string>();
                foreach (string c in b.regAtr)
                {
                    cad.Add(c);
                    
                }
                listD.Add(cad);
            }*/

            string[,] arrayCombinacionT2 = new string[40,20];

            for(int i =0; i < tab2.listAtrib.Count;i++)
            {
                for(int j =0; j<tab2.listAtrib.ElementAt(i).regAtr.Count;j++)
                {
                    arrayCombinacionT2[j,i] = tab2.listAtrib.ElementAt(i).regAtr.ElementAt(j);        
                }
            }

            List<List<string>> listD = new List<List<string>>();
            for (int i = 0; i < tab2.listAtrib.ElementAt(i).regAtr.Count ; i++)
            {
                List<string> cad = new List<string>();
                for (int j = 0; j < tab2.listAtrib.Count ; j++)
                {
                    if(arrayCombinacionT2[i, j] != null)
                      cad.Add(arrayCombinacionT2[i, j]);
                }
                listD.Add(cad);
            }


            r = 0;

            string[,] arrayCombinacionT1 = new string[40, 20];

            for (int i = 0; i < tab1.listAtrib.Count; i++)
            {
                for (int j = 0; j < tab1.listAtrib.ElementAt(i).regAtr.Count; j++)
                {
                    arrayCombinacionT1[j, i] = tab1.listAtrib.ElementAt(i).regAtr.ElementAt(j);
                }
            }

            List<List<string>> listD2 = new List<List<string>>();
            for (int i = 0; i < 6; i++)//////////////PENDIENTE
            {
                List<string> cad = new List<string>();
                for (int j = 0; j < tab1.listAtrib.Count; j++)
                {
                    if (arrayCombinacionT1[i, j] != null)
                        cad.Add(arrayCombinacionT1[i, j]);
                }
                listD2.Add(cad);
            }

            r = 0;

            formaCombinacion(listD, listD2);


        }

        public void formaCombinacion(List<List<string>> lis1, List<List<string>> lis2)
        {
            List<List<string>> lisFinCom = new List<List<string>>();
            int con = 0;

            string[,] arrayCombinacionFin = new string[lis2.ElementAt(0).Count+ lis1.ElementAt(0).Count , lis1.Count * lis2.Count];

            for (int j = 0; j < lis1.Count * lis2.Count; j++)
            {

                //arrayCombinacionFin[j,j] = lis2.ElementAt(0)
                for (int i = 0; i < lis2.ElementAt(0).Count + lis1.ElementAt(0).Count; i++)
                {
                    if (lis2.ElementAt(0).Count < i)
                    {

                    }
                    else
                    {

                    }

                }
            }

            r = 0;
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

                   
                foreach (Atributo b in BD.listEntidad.Find(x=>x.nombre == TabCons.nombre).listAtrib)
                {                     
                    i = 0;

                    if (TabCons.nombre.ToUpper() + "." + b.nombre.ToUpper() == refTab)
                    {
                        foreach (string c in b.regAtr)
                        {

                            bool valAc = hazComparacion(operador, c.ToUpper(), aComparar, b.tipo);
                            if (valAc)
                            {
                                       
                                indSele.Add(i);
                            }
                              
                            i++;
                        }
                        bandAtrEncon = true;

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

            
            foreach(Atributo b in TabCons.listAtrib)
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

        public override int VisitNomTabla([NotNull] GramaticaSQLParser.NomTablaContext context)
        {
            if (bandRecDatoEnt)
            {
                string cadTab = context.GetText();
                cadTab = cadTab.Substring(1, cadTab.Length - 1);


                string[] arEnt = cadTab.Split(' ');

                tabConPrin = arEnt[0];////////AQUI ME QUEDE

                for (int i = 0; i < arEnt.Length; i++)
                {                    
                    TabCons = (Entidad)BD.listEntidad.Find(x => x.nombre.ToUpper() == arEnt[i].ToUpper()).Clone();

                }

                agregaAtrSeleccionadosATab();
                agregaColuAGrid();
                bandRecDatoEnt = false;
            }
            r = 0;
            return base.VisitNomTabla(context);
        }


        public void agregaAtrSeleccionadosATab()
        {
            if (!bandAst)
            {
                TabCons.listAtrib = new List<Atributo>();

                foreach (string a in lisAtrCon)
                {
                    //TabCons.listAtrib.Add((Atributo)BD.listEntidad.Find(x => x.nombre == TabCons.nombre).listAtrib.Find(x => x.nombre.ToUpper() == a.Split('.')[1]).Clone());
                    //TabCons.listAtrib.Add((Atributo)BD.listEntidad.Find(x => x.nombre == TabCons.nombre).listAtrib.Find(x => x.nombre.ToUpper() == a.Split('.')[1]).Clone());
                    Entidad act = BD.listEntidad.Find(x => x.nombre.ToUpper() == a.Split('.')[0]);
                    
                    TabCons.listAtrib.Add((Atributo)act.listAtrib.Find(x => x.nombre.ToUpper() == a.Split('.')[1]).Clone());
                    
                    r = 0;
                }

            }
            else
            {
                bandAst = false;

            }
            

        }

        public void agregRegBD()
        {
            int ren = 0;
            int col = 0;

            foreach (Entidad a in BD.listEntidad)
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
                        string dato = "";

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
            foreach (Atributo b in TabCons.listAtrib)
            {
                DataGridViewTextBoxColumn Columna1 = new DataGridViewTextBoxColumn();
                Columna1.HeaderText = TabCons.nombre+"."+b.nombre;
                gridTabla.Columns.Add(Columna1);
            }
        }

        public void vinculaLlavesForaneas()
        {
            foreach(Entidad a in BD.listEntidad)
            {
                foreach(Atributo b in a.listAtrib)
                {
                    if(b.tipoIndi == 6)
                    {
                        r = 0;
                        BD.archivo.Close();
                        
                        BD.archivo = File.Open(BD.nomArchivo, FileMode.Open);
                        BinaryReader br = new BinaryReader(BD.archivo);

                        BD.archivo.Seek(b.dirAtri+48, SeekOrigin.Begin);
                        string dirCom = br.ReadInt32().ToString();
                        int dirEntiForanea = Convert.ToInt32(dirCom.Substring(1, dirCom.Length - 1));
                        r = 0;
                        b.enForanea = new Entidad();
                        Entidad entFo = BD.listEntidad.Find(x => x.dirEnti == dirEntiForanea);
                        b.enForanea = (Entidad)entFo.Clone();
                        r = 0;

                    }
                }
            }

        }

    }    
}
