namespace PruebaProyecto
{
    partial class Datos
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
            this.comboBoxEntiDatos = new System.Windows.Forms.ComboBox();
            this.RegistroRellDataGrid = new System.Windows.Forms.DataGridView();
            this.GuardaRegistros = new System.Windows.Forms.Button();
            this.RegisInserdataGridView = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.ModificaRegistro = new System.Windows.Forms.Button();
            this.EliminarReg = new System.Windows.Forms.Button();
            this.AplicaCambio = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.RegistroRellDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RegisInserdataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Archivo Datos";
            // 
            // comboBoxEntiDatos
            // 
            this.comboBoxEntiDatos.FormattingEnabled = true;
            this.comboBoxEntiDatos.Location = new System.Drawing.Point(35, 55);
            this.comboBoxEntiDatos.Name = "comboBoxEntiDatos";
            this.comboBoxEntiDatos.Size = new System.Drawing.Size(121, 21);
            this.comboBoxEntiDatos.TabIndex = 1;
            this.comboBoxEntiDatos.SelectedIndexChanged += new System.EventHandler(this.CambiaEntiReg);
            // 
            // RegistroRellDataGrid
            // 
            this.RegistroRellDataGrid.AllowUserToAddRows = false;
            this.RegistroRellDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RegistroRellDataGrid.Location = new System.Drawing.Point(35, 116);
            this.RegistroRellDataGrid.Name = "RegistroRellDataGrid";
            this.RegistroRellDataGrid.Size = new System.Drawing.Size(741, 142);
            this.RegistroRellDataGrid.TabIndex = 3;
            // 
            // GuardaRegistros
            // 
            this.GuardaRegistros.Location = new System.Drawing.Point(222, 55);
            this.GuardaRegistros.Name = "GuardaRegistros";
            this.GuardaRegistros.Size = new System.Drawing.Size(97, 24);
            this.GuardaRegistros.TabIndex = 4;
            this.GuardaRegistros.Text = "GuardaRegistros";
            this.GuardaRegistros.UseVisualStyleBackColor = true;
            this.GuardaRegistros.Click += new System.EventHandler(this.GuardaRegistros_Click);
            // 
            // RegisInserdataGridView
            // 
            this.RegisInserdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RegisInserdataGridView.Location = new System.Drawing.Point(35, 309);
            this.RegisInserdataGridView.Name = "RegisInserdataGridView";
            this.RegisInserdataGridView.Size = new System.Drawing.Size(741, 170);
            this.RegisInserdataGridView.TabIndex = 5;
            this.RegisInserdataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RegisInserdataGridView_CellClick);
            this.RegisInserdataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.RegisInserdataGridView_ColumnHeaderMouseClick);
            this.RegisInserdataGridView.SelectionChanged += new System.EventHandler(this.RegisInserdataGridView_SelectionChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 278);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Vista de Registros";
            // 
            // ModificaRegistro
            // 
            this.ModificaRegistro.Location = new System.Drawing.Point(367, 21);
            this.ModificaRegistro.Name = "ModificaRegistro";
            this.ModificaRegistro.Size = new System.Drawing.Size(75, 23);
            this.ModificaRegistro.TabIndex = 7;
            this.ModificaRegistro.Text = "Modificar";
            this.ModificaRegistro.UseVisualStyleBackColor = true;
            this.ModificaRegistro.Click += new System.EventHandler(this.ModificaRegistro_Click);
            // 
            // EliminarReg
            // 
            this.EliminarReg.Location = new System.Drawing.Point(491, 21);
            this.EliminarReg.Name = "EliminarReg";
            this.EliminarReg.Size = new System.Drawing.Size(75, 23);
            this.EliminarReg.TabIndex = 8;
            this.EliminarReg.Text = "Eliminar";
            this.EliminarReg.UseVisualStyleBackColor = true;
            this.EliminarReg.Click += new System.EventHandler(this.EliminarReg_Click);
            // 
            // AplicaCambio
            // 
            this.AplicaCambio.Location = new System.Drawing.Point(367, 65);
            this.AplicaCambio.Name = "AplicaCambio";
            this.AplicaCambio.Size = new System.Drawing.Size(83, 23);
            this.AplicaCambio.TabIndex = 9;
            this.AplicaCambio.Text = "AplicaCambio";
            this.AplicaCambio.UseVisualStyleBackColor = true;
            this.AplicaCambio.Click += new System.EventHandler(this.AplicaCambio_Click);
            // 
            // Datos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 491);
            this.Controls.Add(this.AplicaCambio);
            this.Controls.Add(this.EliminarReg);
            this.Controls.Add(this.ModificaRegistro);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RegisInserdataGridView);
            this.Controls.Add(this.GuardaRegistros);
            this.Controls.Add(this.RegistroRellDataGrid);
            this.Controls.Add(this.comboBoxEntiDatos);
            this.Controls.Add(this.label1);
            this.Name = "Datos";
            this.Text = "Datos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Datos_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.RegistroRellDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RegisInserdataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxEntiDatos;
        private System.Windows.Forms.DataGridView RegistroRellDataGrid;
        private System.Windows.Forms.Button GuardaRegistros;
        private System.Windows.Forms.DataGridView RegisInserdataGridView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ModificaRegistro;
        private System.Windows.Forms.Button EliminarReg;
        private System.Windows.Forms.Button AplicaCambio;
    }
}