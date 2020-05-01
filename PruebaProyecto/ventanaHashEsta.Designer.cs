namespace PruebaProyecto
{
    partial class ventanaHashEsta
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
            this.directorioGrid = new System.Windows.Forms.DataGridView();
            this.cajonGrid = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.directorioGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cajonGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // directorioGrid
            // 
            this.directorioGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.directorioGrid.Location = new System.Drawing.Point(35, 36);
            this.directorioGrid.Name = "directorioGrid";
            this.directorioGrid.Size = new System.Drawing.Size(250, 312);
            this.directorioGrid.TabIndex = 0;
            this.directorioGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.eventoCajonVer);
            // 
            // cajonGrid
            // 
            this.cajonGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cajonGrid.Location = new System.Drawing.Point(305, 120);
            this.cajonGrid.Name = "cajonGrid";
            this.cajonGrid.Size = new System.Drawing.Size(247, 187);
            this.cajonGrid.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Directorio";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(392, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Cajon";
            // 
            // ventanaHashEsta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 379);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cajonGrid);
            this.Controls.Add(this.directorioGrid);
            this.Name = "ventanaHashEsta";
            this.Text = "ventanaHashEsta";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ventanaHashEsta_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.directorioGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cajonGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView directorioGrid;
        private System.Windows.Forms.DataGridView cajonGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}