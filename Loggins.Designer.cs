namespace LocationDeVoiture
{
    partial class Loggins
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Loggins));
            this.nomUtilisateur = new System.Windows.Forms.TextBox();
            this.motDePasse = new System.Windows.Forms.TextBox();
            this.lbl_courriel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnConnexion = new System.Windows.Forms.Button();
            this.BtnAnnuler = new System.Windows.Forms.Button();
            this.imgDisconnected = new System.Windows.Forms.PictureBox();
            this.imgConnected = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgDisconnected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgConnected)).BeginInit();
            this.SuspendLayout();
            // 
            // nomUtilisateur
            // 
            this.nomUtilisateur.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.nomUtilisateur.Location = new System.Drawing.Point(303, 55);
            this.nomUtilisateur.Name = "nomUtilisateur";
            this.nomUtilisateur.Size = new System.Drawing.Size(234, 29);
            this.nomUtilisateur.TabIndex = 0;
            // 
            // motDePasse
            // 
            this.motDePasse.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.motDePasse.Location = new System.Drawing.Point(303, 99);
            this.motDePasse.Name = "motDePasse";
            this.motDePasse.Size = new System.Drawing.Size(234, 29);
            this.motDePasse.TabIndex = 1;
            this.motDePasse.UseSystemPasswordChar = true;
            // 
            // lbl_courriel
            // 
            this.lbl_courriel.AutoSize = true;
            this.lbl_courriel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbl_courriel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbl_courriel.Location = new System.Drawing.Point(163, 102);
            this.lbl_courriel.Name = "lbl_courriel";
            this.lbl_courriel.Size = new System.Drawing.Size(117, 21);
            this.lbl_courriel.TabIndex = 1;
            this.lbl_courriel.Text = "Mot de passe :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(204, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Courriel :";
            // 
            // BtnConnexion
            // 
            this.BtnConnexion.Location = new System.Drawing.Point(462, 152);
            this.BtnConnexion.Name = "BtnConnexion";
            this.BtnConnexion.Size = new System.Drawing.Size(75, 23);
            this.BtnConnexion.TabIndex = 2;
            this.BtnConnexion.Text = "Connexion";
            this.BtnConnexion.UseVisualStyleBackColor = true;
            this.BtnConnexion.Click += new System.EventHandler(this.BtnConnexion_Click);
            // 
            // BtnAnnuler
            // 
            this.BtnAnnuler.Location = new System.Drawing.Point(303, 152);
            this.BtnAnnuler.Name = "BtnAnnuler";
            this.BtnAnnuler.Size = new System.Drawing.Size(75, 23);
            this.BtnAnnuler.TabIndex = 2;
            this.BtnAnnuler.Text = "Annuler";
            this.BtnAnnuler.UseVisualStyleBackColor = true;
            this.BtnAnnuler.Click += new System.EventHandler(this.BtnAnnuler_Click);
            // 
            // imgDisconnected
            // 
            this.imgDisconnected.Image = global::LocationDeVoiture.Properties.Resources.disconnect;
            this.imgDisconnected.Location = new System.Drawing.Point(0, 0);
            this.imgDisconnected.Margin = new System.Windows.Forms.Padding(0);
            this.imgDisconnected.Name = "imgDisconnected";
            this.imgDisconnected.Size = new System.Drawing.Size(72, 60);
            this.imgDisconnected.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgDisconnected.TabIndex = 3;
            this.imgDisconnected.TabStop = false;
            // 
            // imgConnected
            // 
            this.imgConnected.Image = global::LocationDeVoiture.Properties.Resources.connect;
            this.imgConnected.Location = new System.Drawing.Point(0, 60);
            this.imgConnected.Margin = new System.Windows.Forms.Padding(0);
            this.imgConnected.Name = "imgConnected";
            this.imgConnected.Size = new System.Drawing.Size(72, 60);
            this.imgConnected.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgConnected.TabIndex = 3;
            this.imgConnected.TabStop = false;
            this.imgConnected.Visible = false;
            // 
            // Loggins
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LocationDeVoiture.Properties.Resources.fingerprint;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CancelButton = this.BtnAnnuler;
            this.ClientSize = new System.Drawing.Size(602, 213);
            this.Controls.Add(this.imgConnected);
            this.Controls.Add(this.imgDisconnected);
            this.Controls.Add(this.BtnAnnuler);
            this.Controls.Add(this.BtnConnexion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_courriel);
            this.Controls.Add(this.motDePasse);
            this.Controls.Add(this.nomUtilisateur);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Loggins";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.imgDisconnected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgConnected)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox nomUtilisateur;
        private TextBox motDePasse;
        private Label lbl_courriel;
        private Label label1;
        private Button BtnConnexion;
        private Button BtnAnnuler;
        private PictureBox imgDisconnected;
        private PictureBox imgConnected;
    }
}