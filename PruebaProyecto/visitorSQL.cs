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
        string tabConSec;
        bool bandInnerJoin = false;
        List<List<string>> ListInnerJoin;

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
            if(!bandInnerJoin)
                muestraGridCon();
            else
            {
                muestraGridConInnerJoin();
            }

            
            cierraArchivosDatosEntidades();
           
            return base.VisitFinConsulta(context);
        }

        public void muestraGridConInnerJoin()
        {
            int ren = 0;
            int col = 0;
            int ind = 0;
            limpiaGridAct();

            List<int> refIn = agregColInner();

            for (ren = 0; ren <ListInnerJoin.Count;ren++)
            {
                gridTabla.Rows.Add();
                ind = 0;
                for(col = 0; col <ListInnerJoin.ElementAt(ren).Count;col++)
                {
                    if (refIn.FindIndex(x => x == col) != -1)
                    {
                        gridTabla.Rows[ren].Cells[ind].Value = ListInnerJoin.ElementAt(ren).ElementAt(col);
                        ind++;
                    }

                    
                }
            }

            /*
            foreach (Atrib a in t1.listAtrib)
            {
                //lisAtrCon.Find(x => x == t1.nombre.ToUpper() + "." + a.nombre.ToUpper());
                if (lisAtrCon.FindIndex(x => x == t1.nombre.ToUpper() + "." + a.nombre.ToUpper()) != -1)
                {
                    DataGridViewTextBoxColumn Columna1 = new DataGridViewTextBoxColumn();
                    Columna1.HeaderText = t1.nombre + "." + a.nombre;
                    gridTabla.Columns.Add(Columna1);
                }
            }*/

        }

        public List<int> agregColInner()
        {
            Entidad t1 = BD.listEntidad.Find(x => x.nombre.ToUpper() == tabConPrin);
            Entidad t2 = BD.listEntidad.Find(x => x.nombre.ToUpper() == tabConSec);
            List<int> ListIndRef = new List<int>();
            int i =0 , j = 0;

            foreach (Atributo a in t1.listAtrib)
            {
                //lisAtrCon.Find(x => x == t1.nombre.ToUpper() + "." + a.nombre.ToUpper());
                if (lisAtrCon.FindIndex(x => x == t1.nombre.ToUpper() + "." + a.nombre.ToUpper()) != -1)
                {
                    DataGridViewTextBoxColumn Columna1 = new DataGridViewTextBoxColumn();
                    Columna1.HeaderText = t1.nombre + "." + a.nombre;
                    gridTabla.Columns.Add(Columna1);

                    ListIndRef.Add(i);
                   
                }
                i++;
            }

            foreach (Atributo a in t2.listAtrib)
            {
                if (lisAtrCon.FindIndex(x => x == t2.nombre.ToUpper() + "." + a.nombre.ToUpper()) != -1)
                {
                    DataGridViewTextBoxColumn Columna1 = new DataGridViewTextBoxColumn();
                    Columna1.HeaderText = t2.nombre + "." + a.nombre;
                    gridTabla.Columns.Add(Columna1);
                    ListIndRef.Add(i);
                }
                i++;
                /*
                DataGridViewTextBoxColumn Columna1 = new DataGridViewTextBoxColumn();
                Columna1.HeaderText = t1.nombre + "." + a.nombre;
                gridTabla.Columns.Add(Columna1);
                */
            }
            r = 0;

            return ListIndRef;
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
            bandInnerJoin = true;
            ListInnerJoin = new List<List<string>>();

            string tab2 = context.NOM(0).GetText();
            string [,] combTablas  = hazCombinacionReg(tab2);
            //List<List<string>> tablaFinal = new List<List<string>>();
            
            string tab1A = context.NOM(1).GetText();
            string tab2A = context.NOM(3).GetText();
            tabConSec = tab2A;
            string atr = context.NOM(2).GetText();

            Entidad t1 = BD.listEntidad.Find(x => x.nombre.ToUpper() == tab1A);
            Entidad t2 = BD.listEntidad.Find(x => x.nombre.ToUpper() == tab2A);

            int IndAtr1 = t1.listAtrib.FindIndex(x => x.nombre.ToUpper() == atr);
            int IndAtr2 = t2.listAtrib.FindIndex(x => x.nombre.ToUpper() == atr);


            int indReg = 0;
            for(int i = 0; i< combTablas.GetLength(0);i++)
            {

                if (combTablas[i, IndAtr1] == combTablas[i, t1.listAtrib.Count+IndAtr2])
                {
                    List<string> liSt = new List<string>();
                    for (int j = 0; j < combTablas.GetLength(1); j++)
                    {
                        liSt.Add(combTablas[i, j]);
                    }
                    //tablaFinal.Add(liSt);
                    ListInnerJoin.Add(liSt);

                }

            }
            r = 0;


            return base.VisitConInnerJoin(context);
        }

        public string[,] hazCombinacionReg(string tabla2)
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

            string[,] combFinales = formaCombinacion(listD, listD2);

            r = 0;
            return combFinales;
        }

        public string[,] formaCombinacion(List<List<string>> lis1, List<List<string>> lis2)
        {
            List<List<string>> lisFinCom = new List<List<string>>();
            int con = 0;

            string[,] arrayCombinacionFin = new string[lis1.Count * lis2.Count, lis2.ElementAt(0).Count+ lis1.ElementAt(0).Count];

            for (int j = 0; j < lis2.Count; j++)
            {

                for(int i  = 0; i <lis1.Count;i++)
                {
                    for(int k = 0; k < lis2.ElementAt(j).Count;k++)
                    {
                        arrayCombinacionFin[(i+j)+j, k] = lis2.ElementAt(j).ElementAt(k);
                    }

                    for (int m = lis2.ElementAt(j).Count; m < lis2.ElementAt(0).Count + lis1.ElementAt(0).Count ; m++)
                    {
                        arrayCombinacionFin[(i+j)+j, m] = lis1.ElementAt(i).ElementAt(m - lis1.ElementAt(0).Count);
                    }
                    r = 0;
                }
                
            }

            return arrayCombinacionFin;
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
            bandInnerJoin = false;

            bandAst = true;
            return base.VisitAste(context);
        }



        public override int VisitAtrTab([NotNull] GramaticaSQLParser.AtrTabContext context)
        {
            limpiaGridAct();
            bandInnerJoin = false;

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
