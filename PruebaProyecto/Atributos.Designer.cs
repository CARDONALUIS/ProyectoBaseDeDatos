namespace PruebaProyecto
{
    partial class Atributos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxEntidades = new System.Windows.Forms.ComboBox();
            this.textBoxNomAtri = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxAtrTipo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxAtriLong = new System.Windows.Forms.TextBox();
            this.comboBoxAtrTip_Ind = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.GridAtributos = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.comboBoxModAtri = new System.Windows.Forms.ComboBox();
            this.cambiaAtribu = new System.Windows.Forms.Button();
            this.buttonEliAtri = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GridAtributos)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Atributos";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Entidad: ";
            // 
            // comboBoxEntidades
            // 
            this.comboBoxEntidades.FormattingEnabled = true;
            this.comboBoxEntidades.Location = new System.Drawing.Point(84, 60);
            this.comboBoxEntidades.Name = "comboBoxEntidades";
            this.comboBoxEntidades.Size = new System.Drawing.Size(121, 21);
            this.comboBoxEntidades.TabIndex = 2;
            this.comboBoxEntidades.SelectedIndexChanged += new System.EventHandler(this.CambioIndiceAtri);
            // 
            // textBoxNomAtri
            // 
            this.textBoxNomAtri.Location = new System.Drawing.Point(87, 118);
            this.textBoxNomAtri.Name = "textBoxNomAtri";
            this.textBoxNomAtri.Size = new System.Drawing.Size(100, 20);
            this.textBoxNomAtri.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Atributo:";
            // 
            // comboBoxAtrTipo
            // 
            this.comboBoxAtrTipo.FormattingEnabled = true;
            this.comboBoxAtrTipo.Items.AddRange(new object[] {
            "E",
            "C"});
            this.comboBoxAtrTipo.Location = new System.Drawing.Point(261, 118);
            this.comboBoxAtrTipo.Name = "comboBoxAtrTipo";
            this.comboBoxAtrTipo.Size = new System.Drawing.Size(121, 21);
            this.comboBoxAtrTipo.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(215, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Tipo:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(477, 121);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(407, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Longitud:";
            // 
            // textBoxAtriLong
            // 
            this.textBoxAtriLong.Location = new System.Drawing.Point(476, 121);
            this.textBoxAtriLong.Name = "textBoxAtriLong";
            this.textBoxAtriLong.Size = new System.Drawing.Size(100, 20);
            this.textBoxAtriLong.TabIndex = 11;
            // 
            // comboBoxAtrTip_Ind
            // 
            this.comboBoxAtrTip_Ind.FormattingEnabled = true;
            this.comboBoxAtrTip_Ind.Items.AddRange(new object[] {
            "0-Sin indice",
            "1-Clave busqueda",
            "2-Llave Primaria",
            "3-Llave Secundaria",
            "4-Indice Primario Arbol B+",
            "5-Indice Hash Estatico"});
            this.comboBoxAtrTip_Ind.Location = new System.Drawing.Point(678, 122);
            this.comboBoxAtrTip_Ind.Name = "comboBoxAtrTip_Ind";
            this.comboBoxAtrTip_Ind.Size = new System.Drawing.Size(144, 21);
            this.comboBoxAtrTip_Ind.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(605, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "Tipo_Ind:";
            // 
            // GridAtributos
            // 
            this.GridAtributos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridAtributos.Location = new System.Drawing.Point(20, 184);
            this.GridAtributos.Name = "GridAtributos";
            this.GridAtributos.Size = new System.Drawing.Size(896, 295);
            this.GridAtributos.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(342, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Agregar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ActualizarAtributo);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(241, 61);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "VerActuales";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.VerActualesAtri);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(477, 60);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 17;
            this.button3.Text = "Modificar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.ModificarEnti);
            // 
            // comboBoxModAtri
            // 
            this.comboBoxModAtri.FormattingEnabled = true;
            this.comboBoxModAtri.Location = new System.Drawing.Point(678, 59);
            this.comboBoxModAtri.Name = "comboBoxModAtri";
            this.comboBoxModAtri.Size = new System.Drawing.Size(121, 21);
            this.comboBoxModAtri.TabIndex = 18;
            // 
            // cambiaAtribu
            // 
            this.cambiaAtribu.Location = new System.Drawing.Point(585, 29);
            this.cambiaAtribu.Name = "cambiaAtribu";
            this.cambiaAtribu.Size = new System.Drawing.Size(75, 23);
            this.cambiaAtribu.TabIndex = 19;
            this.cambiaAtribu.Text = "Cambia";
            this.cambiaAtribu.UseVisualStyleBackColor = true;
            this.cambiaAtribu.Click += new System.EventHandler(this.cambiaAtributo);
            // 
            // buttonEliAtri
            // 
            this.buttonEliAtri.Location = new System.Drawing.Point(585, 74);
            this.buttonEliAtri.Name = "buttonEliAtri";
            this.buttonEliAtri.Size = new System.Drawing.Size(75, 23);
            this.buttonEliAtri.TabIndex = 20;
            this.buttonEliAtri.Text = "Eliminar";
            this.buttonEliAtri.UseVisualStyleBackColor = true;
            this.buttonEliAtri.Click += new System.EventHandler(this.eliminaAtri);
            // 
            // Atributos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 491);
            this.Controls.Add(this.buttonEliAtri);
            this.Controls.Add(this.cambiaAtribu);
            this.Controls.Add(this.comboBoxModAtri);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.GridAtributos);
            this.Controls.Add(this.comboBoxAtrTip_Ind);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxAtriLong);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxAtrTipo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxNomAtri);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxEntidades);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Atributos";
            this.Text = "Atributos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Atributos_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.GridAtributos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxEntidades;
        private System.Windows.Forms.TextBox textBoxNomAtri;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxAtrTipo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxAtriLong;
        private System.Windows.Forms.ComboBox comboBoxAtrTip_Ind;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView GridAtributos;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox comboBoxModAtri;
        private System.Windows.Forms.Button cambiaAtribu;
        private System.Windows.Forms.Button buttonEliAtri;
    }
}