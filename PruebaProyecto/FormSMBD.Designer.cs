namespace PruebaProyecto
{
    partial class FormSMBD
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
            this.richTextBoxConsulta = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridViewResConsulta = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResConsulta)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBoxConsulta
            // 
            this.richTextBoxConsulta.Location = new System.Drawing.Point(12, 42);
            this.richTextBoxConsulta.Name = "richTextBoxConsulta";
            this.richTextBoxConsulta.Size = new System.Drawing.Size(723, 33);
            this.richTextBoxConsulta.TabIndex = 0;
            this.richTextBoxConsulta.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(781, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Ejecutar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.botonEjecuta);
            // 
            // dataGridViewResConsulta
            // 
            this.dataGridViewResConsulta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResConsulta.Location = new System.Drawing.Point(13, 90);
            this.dataGridViewResConsulta.Name = "dataGridViewResConsulta";
            this.dataGridViewResConsulta.Size = new System.Drawing.Size(743, 321);
            this.dataGridViewResConsulta.TabIndex = 2;
            // 
            // FormSMBD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 450);
            this.Controls.Add(this.dataGridViewResConsulta);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBoxConsulta);
            this.Name = "FormSMBD";
            this.Text = "FormSMBD";
            this.Load += new System.EventHandler(this.FormSMBD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResConsulta)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxConsulta;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.DataGridView dataGridViewResConsulta;
    }
}