namespace AnalizadorLexicoXQcode
{
    partial class Form1
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
            this.lstBoxMostrarArchivo = new System.Windows.Forms.ListBox();
            this.btnEscojerArchivo = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.lblCodFuente = new System.Windows.Forms.Label();
            this.lblTokens = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstBoxMostrarArchivo
            // 
            this.lstBoxMostrarArchivo.FormattingEnabled = true;
            this.lstBoxMostrarArchivo.ItemHeight = 16;
            this.lstBoxMostrarArchivo.Location = new System.Drawing.Point(50, 203);
            this.lstBoxMostrarArchivo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstBoxMostrarArchivo.Name = "lstBoxMostrarArchivo";
            this.lstBoxMostrarArchivo.Size = new System.Drawing.Size(436, 244);
            this.lstBoxMostrarArchivo.TabIndex = 0;
            this.lstBoxMostrarArchivo.SelectedIndexChanged += new System.EventHandler(this.lstBoxMostrarArchivo_SelectedIndexChanged);
            // 
            // btnEscojerArchivo
            // 
            this.btnEscojerArchivo.Location = new System.Drawing.Point(411, 90);
            this.btnEscojerArchivo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEscojerArchivo.Name = "btnEscojerArchivo";
            this.btnEscojerArchivo.Size = new System.Drawing.Size(129, 45);
            this.btnEscojerArchivo.TabIndex = 1;
            this.btnEscojerArchivo.Text = "Tokenize";
            this.btnEscojerArchivo.UseVisualStyleBackColor = true;
            this.btnEscojerArchivo.Click += new System.EventHandler(this.btnEscojerArchivo_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(511, 203);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(433, 244);
            this.listBox1.TabIndex = 2;
            // 
            // lblCodFuente
            // 
            this.lblCodFuente.AutoSize = true;
            this.lblCodFuente.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodFuente.Location = new System.Drawing.Point(44, 155);
            this.lblCodFuente.Name = "lblCodFuente";
            this.lblCodFuente.Size = new System.Drawing.Size(215, 32);
            this.lblCodFuente.TabIndex = 3;
            this.lblCodFuente.Text = "Codigo Fuente";
            // 
            // lblTokens
            // 
            this.lblTokens.AutoSize = true;
            this.lblTokens.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTokens.Location = new System.Drawing.Point(506, 158);
            this.lblTokens.Name = "lblTokens";
            this.lblTokens.Size = new System.Drawing.Size(114, 32);
            this.lblTokens.TabIndex = 4;
            this.lblTokens.Text = "Tokens";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 510);
            this.Controls.Add(this.lblTokens);
            this.Controls.Add(this.lblCodFuente);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnEscojerArchivo);
            this.Controls.Add(this.lstBoxMostrarArchivo);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListBox lstBoxMostrarArchivo;
        private System.Windows.Forms.Button btnEscojerArchivo;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label lblCodFuente;
        private System.Windows.Forms.Label lblTokens;
    }
}

