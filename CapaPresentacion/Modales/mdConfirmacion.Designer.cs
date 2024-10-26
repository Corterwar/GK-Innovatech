namespace CapaPresentacion.Modales
{
    partial class mdConfirmacion
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
            this.campoToken = new CustomControls.RJControls.RJTextBox();
            this.rjButton1 = new CustomControls.RJControls.RJButton();
            this.SuspendLayout();
            // 
            // campoToken
            // 
            this.campoToken.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(31)))), ((int)(((byte)(84)))));
            this.campoToken.BorderColor = System.Drawing.Color.Transparent;
            this.campoToken.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.campoToken.BorderRadius = 10;
            this.campoToken.BorderSize = 1;
            this.campoToken.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.campoToken.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.campoToken.ForeColor = System.Drawing.Color.White;
            this.campoToken.IsReadOnly = false;
            this.campoToken.Location = new System.Drawing.Point(58, 74);
            this.campoToken.Margin = new System.Windows.Forms.Padding(4);
            this.campoToken.Multiline = false;
            this.campoToken.Name = "campoToken";
            this.campoToken.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.campoToken.PasswordChar = false;
            this.campoToken.PlaceholderColor = System.Drawing.Color.White;
            this.campoToken.PlaceholderText = "Token";
            this.campoToken.ShortcutsEnabled = true;
            this.campoToken.Size = new System.Drawing.Size(198, 32);
            this.campoToken.TabIndex = 4;
            this.campoToken.Texts = "";
            this.campoToken.UnderlinedStyle = true;
            // 
            // rjButton1
            // 
            this.rjButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.rjButton1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.rjButton1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.rjButton1.BorderRadius = 20;
            this.rjButton1.BorderSize = 2;
            this.rjButton1.FlatAppearance.BorderSize = 0;
            this.rjButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjButton1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rjButton1.ForeColor = System.Drawing.Color.White;
            this.rjButton1.Location = new System.Drawing.Point(58, 113);
            this.rjButton1.Name = "rjButton1";
            this.rjButton1.Size = new System.Drawing.Size(198, 44);
            this.rjButton1.TabIndex = 6;
            this.rjButton1.Text = "Comprobar";
            this.rjButton1.TextColor = System.Drawing.Color.White;
            this.rjButton1.UseVisualStyleBackColor = false;
            this.rjButton1.Click += new System.EventHandler(this.rjButton1_Click);
            // 
            // mdConfirmacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 194);
            this.Controls.Add(this.rjButton1);
            this.Controls.Add(this.campoToken);
            this.Name = "mdConfirmacion";
            this.Text = "mdConfirmacion";
            this.Load += new System.EventHandler(this.mdConfirmacion_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControls.RJControls.RJTextBox campoToken;
        private CustomControls.RJControls.RJButton rjButton1;
    }
}