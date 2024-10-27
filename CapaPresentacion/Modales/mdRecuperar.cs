using CapaNegocio;
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
    public partial class mdRecuperar : Form
    {
        String Token {  get; set; }
        public int intentos {  get; set; }
        string Documento { get; set; }

        public mdRecuperar(string documento, string token)
        {
            InitializeComponent();
            //Configura la opacidad del formulario(50 % de transparencia)
            this.Opacity = 1;

            // Establece un color clave de transparencia (el color de fondo del formulario será transparente)
       

            this.Documento = documento;
            this.Token = token;
            this.intentos = 0;


            this.lbl1.Visible = false;
            this.lbl2.Visible = false;

            this.campoClave.Visible = false;
            this.campoClave2.Visible = false;
            this.btnRegistrar.Visible = false;
        }

        private void FormularioModal_Shown(object sender, EventArgs e)
        {
            intentos = 0; // Reiniciar el contador de intentos
        }
        private void FormularioModal_FormClosing(object sender, FormClosingEventArgs e)
        {
            intentos = 0; // Reiniciar el contador al cerrar el formulario
        }

        private bool Validaciones()
        {
            bool resultado = true;

            if (campoClave.Texts == "")
            {
                resultado = false;
            }
            if(campoClave.Texts == "")
            {
                resultado = false;
            }
            if(campoClave.Texts != campoClave2.Texts)
            {
                resultado = false;
            }

            return resultado;
        }


        private void rjButton1_Click(object sender, EventArgs e)
        {
            if (Validaciones())
            {
                bool resultado = new CN_Usuario().Recuperar(this.Documento,campoClave.Texts.ToString());

                if (resultado)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al actualizar los datos","Alerta",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Credenciales ingresadas incorrectamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void mdRecuperar_Load(object sender, EventArgs e)
        {
            this.intentos = 0;
        }

        private void btnComprobar_Click(object sender, EventArgs e)
        {
            if (campoToken.Texts.Trim() != "" && intentos < 3)
            {
                if (campoToken.Texts.Trim() == Token )
                {
                    this.campoToken.Visible = false;
                    this.btnComprobar.Visible = false;
                    this.lblToken.Visible = false;

                    this.lbl1.Visible = true;
                    this.lbl2.Visible = true;
                    this.campoClave.Visible = true;
                    this.campoClave2.Visible = true;
                    this.btnRegistrar.Visible = true;
                }
                if (intentos < 3)
                {
                    MessageBox.Show("Token Incorrecto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.intentos = this.intentos + 1;
                }
            }
            else
            {
                if (intentos < 3)
                {
                    MessageBox.Show("Token Incorrecto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.intentos = this.intentos + 1;
                }
                else
                {
                    MessageBox.Show("Supero la maxima cantidad de intentos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.Cancel;
                    this.intentos = 0;
                    this.Close();
                }


            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.DialogResult |= DialogResult.Cancel;
            this.Close();
        }

        private void campoToken__TextChanged(object sender, EventArgs e)
        {

        }

        private void campoClave2__TextChanged(object sender, EventArgs e)
        {

        }

        private void campoClave__TextChanged(object sender, EventArgs e)
        {

        }

        private void rjTextBox2__TextChanged(object sender, EventArgs e)
        {

        }

        private void nombreUser_Click(object sender, EventArgs e)
        {

        }
    }
}
