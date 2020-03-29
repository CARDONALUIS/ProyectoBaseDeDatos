namespace PruebaProyecto
{
    partial class venIndiceSec
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
            this.atriLlavSec = new System.Windows.Forms.ComboBox();
            this.llaveSecGrid = new System.Windows.Forms.DataGridView();
            this.cajonGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.llaveSecGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cajonGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // atriLlavSec
            // 
            this.atriLlavSec.FormattingEnabled = true;
            this.atriLlavSec.Location = new System.Drawing.Point(12, 23);
            this.atriLlavSec.Name = "atriLlavSec";
            this.atriLlavSec.Size = new System.Drawing.Size(121, 21);
            this.atriLlavSec.TabIndex = 0;
            // 
            // llaveSecGrid
            // 
            this.llaveSecGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.llaveSecGrid.Location = new System.Drawing.Point(12, 87);
            this.llaveSecGrid.Name = "llaveSecGrid";
            this.llaveSecGrid.Size = new System.Drawing.Size(329, 214);
            this.llaveSecGrid.TabIndex = 1;
            // 
            // cajonGrid
            // 
            this.cajonGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cajonGrid.Location = new System.Drawing.Point(375, 87);
            this.cajonGrid.Name = "cajonGrid";
            this.cajonGrid.Size = new System.Drawing.Size(310, 214);
            this.cajonGrid.TabIndex = 2;
            // 
            // venIndiceSec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cajonGrid);
            this.Controls.Add(this.llaveSecGrid);
            this.Controls.Add(this.atriLlavSec);
            this.Name = "venIndiceSec";
            this.Text = "venIndiceSec";
            ((System.ComponentModel.ISupportInitialize)(this.llaveSecGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cajonGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox atriLlavSec;
        private System.Windows.Forms.DataGridView llaveSecGrid;
        private System.Windows.Forms.DataGridView cajonGrid;
    }
}