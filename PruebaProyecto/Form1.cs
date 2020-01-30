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
    public partial class Form1 : Form
    {
        
        Entidades vEnti;
        Atributos vAtri;
        
        Diccionario dic;

        int r = 0;
        string nomArchivo;



        public Form1()
        {
            InitializeComponent();  
            dic = new Diccionario();


        }
        

        
        private void AbrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hola");
            string nomArchivo;
            
            OpenFileDialog abrir = new OpenFileDialog();

            if(abrir.ShowDialog() ==  DialogResult.OK)
            {
                //Guarda el nombre del archivo
                nomArchivo = abrir.FileName;

                label1.Text = "";

                label1.Text = "Archivo:" +nomArchivo;
                //Abre el archivo
                
                dic.archivo = File.Open(nomArchivo, FileMode.Open, FileAccess.Read);
                dic.nomArchivo = nomArchivo;
                



                
                //"LEER ARCHIVO"
                //dic.archivo.Seek(0, SeekOrigin.Begin);
                //BinaryReader br = new BinaryReader(dic.archivo);
                //Lee los siguientes 8 caracteres y los guarda en cab
                //long cab = br.ReadInt64();
                //int cab2 = br.ReadInt32();
                //long algo = dic.archivo.Length;
                //long algo = dic.archivo.Length;
                r = 0;
                actualizaDiccionario(dic.archivo);

               
              
            

                dic.archivo.Close();
            }
            

            /*
            string nomArchivo;
            FileStream archivo;
            //Abrir ventana "guardar como"
            SaveFileDialog saveDialog = new SaveFileDialog();//Crear-guardar archivo
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                //Guarda el nombre selesccionado del cuadro de dialogo
                nomArchivo = saveDialog.FileName;

                //Crea el nuevo archivo, asignando el nombre elegido
                archivo = new FileStream(nomArchivo, FileMode.Create);
                archivo.Close();
            }*/

            /*
            string nomArchivo;
            FileStream archivo;
            OpenFileDialog abrir = new OpenFileDialog();
            if (abrir.ShowDialog() == DialogResult.OK)
            {
                //Guarda el nombre del archivo seleccionado
                nomArchivo = abrir.FileName;
                label1.Text = "";
                
                //Abre el archivo y tiene un acceso de solo lectura
                archivo = File.Open(nomArchivo, FileMode.Open, FileAccess.Read);

                //Para leerlo

                //Abre el archivo y lo posiciona al inicio
                archivo.Seek(0, SeekOrigin.Begin);
                BinaryReader br = new BinaryReader(archivo);

                //Lee los siguientes 8 caracteres y los guarda en cab
                int cab = br.ReadInt32();

                label1.Text = cab.ToString();

                r = 0;
                
                //Cierra el archivo
                archivo.Close();
            }*/
            


            /*
            string FileName = "binary.bin";
            BinaryWriter bw;

            int entero = 13;
            double doble = 2.56317;
            bool booleano = true;
            string cadena = "Hola saludos";

            //Crear el archivo
            FileStream archivo = new FileStream(FileName, FileMode.Create, FileAccess.Write);

            bw = new BinaryWriter(archivo);

            //Escribe en el archivo
            bw.Write(entero);
            //bw.Write(doble);
            //bw.Write(booleano);
            //bw.Write(cadena);

            archivo.Close();
            */

            r = 0;
        }
        



        public void actualizaDiccionario(FileStream archivo)
        {
            r = 0;
            dic.borraDic();
            //Variables de entidad
            int idEnti;
            char[] nombreEnti;
            int dirEnt;
            int dirAtri;
            int dirDat;
            int dirSigEnt;
            string noEn = "";
            //Variables de atributos
            int idAtriL;
            char[] nombreAtriL;
            char[] tipoDL;
            int longiL;
            int dirAtrL;
            int tipoIndL;
            int dirIndL;
            int dirSigAtL;
            string noAt = "";
            bool bandSigEnt = true;
            bool bandSigAtri = true;





            // archivo.Seek(8,SeekOrigin.Begin);
            BinaryReader br = new BinaryReader(archivo);


            archivo.Seek(0, SeekOrigin.Begin);
            int dire = br.ReadInt32();
            dic.cab = dire;
            //dire = 8;

            r = 0;
            while(bandSigEnt)
            {
                archivo.Seek(dire, SeekOrigin.Begin);
                idEnti = br.ReadInt32();
                r = 0;
                dire += 5;
                archivo.Seek(dire, SeekOrigin.Begin);
                //BinaryReader br = new BinaryReader(archivo);
                nombreEnti = br.ReadChars(35);
                r = 0;
                dire += 35;
                archivo.Seek(dire, SeekOrigin.Begin);
                dirEnt = br.ReadInt32();
                r = 0;
                dire += 8;
                archivo.Seek(dire, SeekOrigin.Begin);
                dirAtri = br.ReadInt32();
                r = 0;
                dire += 8;
                archivo.Seek(dire, SeekOrigin.Begin);
                dirDat = br.ReadInt32();
                r = 0;
                dire += 8;
                archivo.Seek(dire, SeekOrigin.Begin);
                dirSigEnt = br.ReadInt32();
                r = 0;
                dire = dirAtri;
                for (int i = 0; i < nombreEnti.Length; i++)
                {
                    if (char.IsLetter(nombreEnti.ElementAt(i)))
                        noEn += nombreEnti.ElementAt(i);
                }
                r = 0;
                Entidad ent = new Entidad(idEnti, noEn, dirEnt, dirAtri, dirDat, dirSigEnt);
                noEn = "";
                dic.listEntidad.Add(ent);
                r = 0;
                while (bandSigAtri)
                {
                    r = 0;
                    archivo.Seek(dire, SeekOrigin.Begin);
                    idAtriL = br.ReadInt32();
                    dire += 5;
                    archivo.Seek(dire, SeekOrigin.Begin);
                    nombreAtriL = br.ReadChars(35);
                    dire += 35;
                    r = 0;
                    archivo.Seek(dire, SeekOrigin.Begin);
                    tipoDL = br.ReadChars(1);
                    dire += 1;
                    r = 0;
                    archivo.Seek(dire, SeekOrigin.Begin);
                    longiL = br.ReadInt32();
                    dire += 4;
                    r = 0;
                    archivo.Seek(dire, SeekOrigin.Begin);
                    dirAtrL = br.ReadInt32();
                    dire += 8;
                    r = 0;
                    archivo.Seek(dire, SeekOrigin.Begin);
                    tipoIndL = br.ReadInt32();
                    dire += 4;
                    r = 0;
                    archivo.Seek(dire, SeekOrigin.Begin);
                    dirIndL = br.ReadInt32();
                    dire += 8;
                    r = 0;
                    archivo.Seek(dire, SeekOrigin.Begin);
                    dirSigAtL = br.ReadInt32();
                    dire = dirSigAtL;
                    r = 0;
                    for (int i = 0; i < nombreAtriL.Length; i++)
                    {
                        if (char.IsLetter(nombreAtriL.ElementAt(i)) || nombreAtriL.ElementAt(i) == '_')
                            noAt += nombreAtriL.ElementAt(i);
                    }
                    Atributo atr = new Atributo(idAtriL, noAt, tipoDL[0], longiL, dirAtrL, tipoIndL, dirIndL, dirSigAtL);
                    noAt = "";
                    ent.listAtrib.Add(atr);
                    r = 0;


                    if (dirSigEnt == -1 && dirSigAtL == -1)
                    {
                        bandSigEnt = false;
                        r = 0;
                    }

                    if (dirSigAtL == -1)
                    {
                        bandSigAtri = false;
                        r = 0;
                        dire = dirSigEnt;
                    }
                }
                bandSigAtri = true;

            }
            bandSigEnt = true;
            r = 0;
        }


        private void DiccionarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //vDic.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //vDic = new VenEntidades();
            vEnti = new Entidades(dic);
            vAtri = new Atributos(dic);
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }


        private void VerDiccionario(object sender, EventArgs e)
        {

        }

        private void ventanaEntidades(object sender, EventArgs e)
        { 
            vEnti.actualizaDicc(dic);
            vEnti.ShowDialog();
        }

        private void VentanaAtributos_Click(object sender, EventArgs e)
        {
            vAtri.actualizaDicc(dic);
            vAtri.ShowDialog();   
        }

        private void NuevoArchivo(object sender, EventArgs e)
        {
                      
            //"GUARDAR UN ARCHIVO"
            FileStream archivo;

            //Abrir "Guardar como"
            SaveFileDialog saveDialog = new SaveFileDialog();
            if(saveDialog.ShowDialog() == DialogResult.OK)
            {
                //Guardar el nombre seleccionado del cuadro de dialogo
                //nomArchivo = saveDialog.FileName;
                dic.nomArchivo = saveDialog.FileName;

                //Crear el nuevo archivo, asignado el nombre elegido
                
                dic.archivo = new FileStream(dic.nomArchivo, FileMode.Create);


                dic.archivo.Close();
                

            }

            label1.Text = "";

            label1.Text = "Archivo:" + dic.nomArchivo;

            using (BinaryWriter bw = new BinaryWriter(File.Open(dic.nomArchivo, FileMode.Open)))
            {
                bw.Write(dic.cab);
            }

            /*
             //"ESCRIBIR EN UN ARCHIVO"
            string FileName = "binary2.bin";

            long entero = 16;
            int entero2 = 14;
            long otroEntero = 19;
            double doble = 2.5678910;
            bool booleano = true;
            string cadena = "Hola!!!!!!";

            //Con la palabra reservada using puedes utilizar cierto recurso determinado por las llaves, en este caso nos permite abrir el archivo simplemente con un binaryWriter con la seguridad de que estaremos cerrando el archivo una vez que las llaves se cierren.  
            //Tomar en cuenta el FileMode lo que se escriba en este archivo se escribirá al principio de este aunque ya tenga información anteriormente.

            
            using (BinaryWriter bw = new BinaryWriter(File.Open(FileName, FileMode.Open)))
            {
                //bw.Write(entero);
                //bw.Write(otroEntero);
                bw.Write(entero2);
                //bw.Write(doble);
                //bw.Write(booleano);
                //bw.Write(cadena);
            }
            */

            /*
            //"LEER ARCHIVO"
            //Abre el archivo y la posiscion inicio
            string nomArchivo = "binary2.bin";
            FileStream archivo = File.Open(nomArchivo, FileMode.Open, FileAccess.Read);
            archivo.Seek(0, SeekOrigin.Begin);
            BinaryReader br = new BinaryReader(archivo);

            //Lee los siguientes 8 caracteres y los guarda en cab
            //long cab = br.ReadInt64();
            int cab2 = br.ReadInt32();

            r = 0;
            archivo.Close();
            */

        }

        private void Guardar_Archivo(object sender, EventArgs e)
        {
            //dic.archivo.Close();
            //"ESCRIBIR EN UN ARCHIVO"

            /*
            string FileName = nomArchivo;

            long entero = 16;
            int entero2 = 14;
            
            long otroEntero = 19;
            double doble = 2.5678910;
            bool booleano = true;
            string cadena = "Hola!!!!!!";
            int contBytChar = 0;

            //Con la palabra reservada using puedes utilizar cierto recurso determinado por las llaves, en este caso nos permite abrir el archivo simplemente con un binaryWriter con la seguridad de que estaremos cerrando el archivo una vez que las llaves se cierren.  
            //Tomar en cuenta el FileMode lo que se escriba en este archivo se escribirá al principio de este aunque ya tenga información anteriormente.

            
            using (BinaryWriter bw = new BinaryWriter(File.Open(FileName, FileMode.Open)))
            {
                bw.Write(dic.cab);
                for (int i = 0; i < dic.listEntidad.Count; i++)
                {
                    bw.Seek((int)dic.listEntidad.ElementAt(i).dirEnti, SeekOrigin.Begin);
                    bw.Write(dic.listEntidad.ElementAt(i).id_enti);
                    bw.Write("");               
                    bw.Write(dic.listEntidad.ElementAt(i).nombre);
                    r = 0;
     
                    if ((dic.listEntidad.ElementAt(i).nombre.Length) % 2 == 0)
                        while (contBytChar < (35 - dic.listEntidad.ElementAt(i).nombre.Length) / 2)
                        {
                            bw.Write("-");
                            contBytChar++;
                        }
                    else
                    {
                        while (contBytChar < (34 - (dic.listEntidad.ElementAt(i).nombre.Length)) / 2)
                        {
                            bw.Write("-");
                            contBytChar++;
                        }
                        bw.Write("");
                    }
                    contBytChar = 0;
                    bw.Write(dic.listEntidad.ElementAt(i).dirEnti);
                    bw.Write(dic.listEntidad.ElementAt(i).dirAtri);
                    bw.Write(dic.listEntidad.ElementAt(i).dirDat);
                    bw.Write(dic.listEntidad.ElementAt(i).dirSigEnti);

                                      
                    for (int j = 0; j < dic.listEntidad.ElementAt(i).listAtrib.Count; j++)
                    {
                        bw.Seek((int)dic.listEntidad.ElementAt(i).listAtrib.ElementAt(j).dirAtri, SeekOrigin.Begin);

                        bw.Write(dic.listEntidad.ElementAt(i).listAtrib.ElementAt(j).id_atri);
                        bw.Write("");
                        
                        bw.Write(dic.listEntidad.ElementAt(i).listAtrib.ElementAt(j).nombre);
                        if ((dic.listEntidad.ElementAt(i).listAtrib.ElementAt(j).nombre.Length) % 2 == 0)
                            while (contBytChar < (35 - dic.listEntidad.ElementAt(i).listAtrib.ElementAt(j).nombre.Length) / 2)
                            {
                                bw.Write("-");
                                contBytChar++;
                            }
                        else
                        {
                            while (contBytChar < (34 - dic.listEntidad.ElementAt(i).listAtrib.ElementAt(j).nombre.Length) / 2)
                            {
                                bw.Write("-");
                                contBytChar++;
                            }
                            bw.Write("");
                        }
                     
                        contBytChar = 0;
                        r = 0;

                        
                        
                        bw.Write(dic.listEntidad.ElementAt(i).listAtrib.ElementAt(j).tipo);
                        bw.Write(dic.listEntidad.ElementAt(i).listAtrib.ElementAt(j).longitud);
                        r = 0;
                        bw.Write(dic.listEntidad.ElementAt(i).listAtrib.ElementAt(j).dirAtri);
                        bw.Write(Int32.Parse(dic.listEntidad.ElementAt(i).listAtrib.ElementAt(j).tipoIndi.ToString().ElementAt(0).ToString()));
                        bw.Write(dic.listEntidad.ElementAt(i).listAtrib.ElementAt(j).dirIndi);
                        bw.Write(dic.listEntidad.ElementAt(i).listAtrib.ElementAt(j).dirSigAtri);
                        
                    }
                    
                }

            }
            r = 0;*/
            
        }
    }
}
