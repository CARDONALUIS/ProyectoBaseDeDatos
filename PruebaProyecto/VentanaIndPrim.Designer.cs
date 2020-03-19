namespace PruebaProyecto
{
    partial class VentanaIndPrim
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
            this.dataGridIndPrim = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridIndPrim)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridIndPrim
            // 
            this.dataGridIndPrim.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridIndPrim.Location = new System.Drawing.Point(43, 31);
            this.dataGridIndPrim.Name = "dataGridIndPrim";
            this.dataGridIndPrim.Size = new System.Drawing.Size(240, 389);
            this.dataGridIndPrim.TabIndex = 0;
            // 
            // VentanaIndPrim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 450);
            this.Controls.Add(this.dataGridIndPrim);
            this.Name = "VentanaIndPrim";
            this.Text = "VentanaIndPrim";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridIndPrim)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGridIndPrim;
    }
}