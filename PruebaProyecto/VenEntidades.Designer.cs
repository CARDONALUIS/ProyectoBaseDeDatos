namespace PruebaProyecto
{
    partial class VenEntidades
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
            this.textEntid = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textNomAtri = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textTipo = new System.Windows.Forms.TextBox();
            this.textTama = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textTipIn = new System.Windows.Forms.TextBox();
            this.AgregarAtri = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Entidad";
            // 
            // textEntid
            // 
            this.textEntid.Location = new System.Drawing.Point(62, 22);
            this.textEntid.Name = "textEntid";
            this.textEntid.Size = new System.Drawing.Size(157, 20);
            this.textEntid.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 170);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(757, 256);
            this.dataGridView1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(240, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Agregar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Atributos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Nombre:";
            // 
            // textNomAtri
            // 
            this.textNomAtri.Location = new System.Drawing.Point(62, 144);
            this.textNomAtri.Name = "textNomAtri";
            this.textNomAtri.Size = new System.Drawing.Size(100, 20);
            this.textNomAtri.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(178, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Tipo:";
            // 
            // textTipo
            // 
            this.textTipo.Location = new System.Drawing.Point(205, 144);
            this.textTipo.Name = "textTipo";
            this.textTipo.Size = new System.Drawing.Size(100, 20);
            this.textTipo.TabIndex = 8;
            // 
            // textTama
            // 
            this.textTama.Location = new System.Drawing.Point(386, 144);
            this.textTama.Name = "textTama";
            this.textTama.Size = new System.Drawing.Size(100, 20);
            this.textTama.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(331, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Tamaño:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(509, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "TipoIndice:";
            // 
            // textTipIn
            // 
            this.textTipIn.Location = new System.Drawing.Point(575, 144);
            this.textTipIn.Name = "textTipIn";
            this.textTipIn.Size = new System.Drawing.Size(100, 20);
            this.textTipIn.TabIndex = 12;
            // 
            // AgregarAtri
            // 
            this.AgregarAtri.Location = new System.Drawing.Point(713, 141);
            this.AgregarAtri.Name = "AgregarAtri";
            this.AgregarAtri.Size = new System.Drawing.Size(75, 23);
            this.AgregarAtri.TabIndex = 13;
            this.AgregarAtri.Text = "Agregar";
            this.AgregarAtri.UseVisualStyleBackColor = true;
            this.AgregarAtri.Click += new System.EventHandler(this.AgregarAtri_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(98, 98);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 14;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 101);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "TipodeEntidad:";
            this.label7.Click += new System.EventHandler(this.Label7_Click);
            // 
            // VenEntidades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.AgregarAtri);
            this.Controls.Add(this.textTipIn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textTama);
            this.Controls.Add(this.textTipo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textNomAtri);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textEntid);
            this.Controls.Add(this.label1);
            this.Name = "VenEntidades";
            this.Text = "VenEntidades";
            this.Load += new System.EventHandler(this.VenEntidades_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textEntid;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textNomAtri;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textTipo;
        private System.Windows.Forms.TextBox textTama;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textTipIn;
        private System.Windows.Forms.Button AgregarAtri;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label7;
    }
}