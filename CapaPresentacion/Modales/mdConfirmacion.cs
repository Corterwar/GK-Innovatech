using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Modales
{
    public partial class mdConfirmacion : Form
    {
        String Token;
        int intentos;
        public mdConfirmacion(string token)
        {
            InitializeComponent();
            this.Token = token;
            this.intentos = 0;
        }

        private void mdConfirmacion_Load(object sender, EventArgs e)
        {

        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            if (campoToken.Texts != "" && intentos != 3)
            {
                if (campoToken.Texts == Token)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }else
                {
                    MessageBox.Show("Token Incorrecto","Alerta",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    this.intentos++;
                }
            }
            else
            {
                MessageBox.Show("Supero la maxima cantidad de intentos","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
