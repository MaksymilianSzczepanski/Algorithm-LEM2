namespace DaneZPlikuOkienko
{
    partial class DaneZPliku
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
            this.btnWybierzPlikSystemu = new System.Windows.Forms.Button();
            this.tbSciezkaDoSystemuDecyzyjnego = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.btnStart = new System.Windows.Forms.Button();
            this.tbSystemDecyzyjny = new System.Windows.Forms.TextBox();
            this.wynik = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnWybierzPlikSystemu
            // 
            this.btnWybierzPlikSystemu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWybierzPlikSystemu.Location = new System.Drawing.Point(763, 12);
            this.btnWybierzPlikSystemu.Name = "btnWybierzPlikSystemu";
            this.btnWybierzPlikSystemu.Size = new System.Drawing.Size(42, 23);
            this.btnWybierzPlikSystemu.TabIndex = 0;
            this.btnWybierzPlikSystemu.Text = "...";
            this.btnWybierzPlikSystemu.UseVisualStyleBackColor = true;
            this.btnWybierzPlikSystemu.Click += new System.EventHandler(this.btnWybierzPlik_Click);
            // 
            // tbSciezkaDoSystemuDecyzyjnego
            // 
            this.tbSciezkaDoSystemuDecyzyjnego.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSciezkaDoSystemuDecyzyjnego.Location = new System.Drawing.Point(193, 12);
            this.tbSciezkaDoSystemuDecyzyjnego.Name = "tbSciezkaDoSystemuDecyzyjnego";
            this.tbSciezkaDoSystemuDecyzyjnego.ReadOnly = true;
            this.tbSciezkaDoSystemuDecyzyjnego.Size = new System.Drawing.Size(564, 22);
            this.tbSciezkaDoSystemuDecyzyjnego.TabIndex = 1;
            this.tbSciezkaDoSystemuDecyzyjnego.Click += new System.EventHandler(this.btnWybierzPlik_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ścieżka do pliku systemu";
            // 
            // btnStart
            // 
            this.btnStart.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnStart.Location = new System.Drawing.Point(338, 403);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(139, 43);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "Pracuj";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tbSystemDecyzyjny
            // 
            this.tbSystemDecyzyjny.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSystemDecyzyjny.Location = new System.Drawing.Point(12, 62);
            this.tbSystemDecyzyjny.Multiline = true;
            this.tbSystemDecyzyjny.Name = "tbSystemDecyzyjny";
            this.tbSystemDecyzyjny.Size = new System.Drawing.Size(140, 321);
            this.tbSystemDecyzyjny.TabIndex = 10;
            // 
            // wynik
            // 
            this.wynik.Location = new System.Drawing.Point(158, 62);
            this.wynik.Multiline = true;
            this.wynik.Name = "wynik";
            this.wynik.Size = new System.Drawing.Size(647, 321);
            this.wynik.TabIndex = 11;
            // 
            // DaneZPliku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 458);
            this.Controls.Add(this.wynik);
            this.Controls.Add(this.tbSystemDecyzyjny);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbSciezkaDoSystemuDecyzyjnego);
            this.Controls.Add(this.btnWybierzPlikSystemu);
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "DaneZPliku";
            this.Text = "Dane z pliku";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnWybierzPlikSystemu;
        private System.Windows.Forms.TextBox tbSciezkaDoSystemuDecyzyjnego;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox tbSystemDecyzyjny;
        private System.Windows.Forms.TextBox wynik;
    }
}

