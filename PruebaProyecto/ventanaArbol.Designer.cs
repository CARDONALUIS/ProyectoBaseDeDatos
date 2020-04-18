namespace PruebaProyecto
{
    partial class ventanaArbol
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
            this.arbolGrid = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.arbolGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // arbolGrid
            // 
            this.arbolGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.arbolGrid.Location = new System.Drawing.Point(40, 59);
            this.arbolGrid.Name = "arbolGrid";
            this.arbolGrid.Size = new System.Drawing.Size(1183, 372);
            this.arbolGrid.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Arbol B+ Primario";
            // 
            // ventanaArbol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1235, 443);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.arbolGrid);
            this.Name = "ventanaArbol";
            this.Text = "ventanaArbol";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ventanaArbol_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.arbolGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView arbolGrid;
        private System.Windows.Forms.Label label1;
    }
}