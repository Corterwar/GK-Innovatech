using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using CapaNegocio;
using CapaEntidad;
using Point = System.Drawing.Point;


namespace CapaPresentacion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }



        private void Login_Load(object sender, EventArgs e)
        {
            Titulo.Parent = Fondo;
            Titulo.BackColor = Color.Transparent; //141; 191
            Titulo.Location = new Point(120, 200);
            Desc.Parent = Fondo;
            Desc.BackColor = Color.Transparent;
            Desc.Location = new Point(134, 296);
            btnSalir.Parent = Fondo;
            btnSalir.BackColor = Color.Transparent;
            btnSalir.Location = new Point(528, 0);

        }



        private void Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {

            Usuario ousuario = new CN_Usuario().Listar().Where(u => u.Documento == campoDni.Text && u.Clave == campoClave.Text && u.Estado == true).FirstOrDefault();

            if(ousuario != null)
            {
                Inicio form = new Inicio(ousuario);
                form.Show();
                this.Hide();

                form.FormClosing += frm_closing;
            }
            else
            {
                System.Windows.MessageBox.Show("No se encuentra el Usuario","Alerta",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }

   

        }


        private void frm_closing(object sender, FormClosingEventArgs e)
        {
            campoClave.Text = "";
            campoDni.Text = "";
            this.Show();        }

        private void campoDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo dígitos, control (como Backspace), o una coma decimal
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                // Bloquear cualquier otro carácter
                e.Handled = true;
            }
        }

        private void campoClave_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnIngresar2_Click(object sender, EventArgs e)
        {

            Usuario ousuario = new CN_Usuario().Listar().Where(u => u.Documento == campoDni.Text && u.Clave == campoClave.Text && u.Estado == true).FirstOrDefault();

            if (ousuario != null)
            {
                Inicio form = new Inicio(ousuario);
                form.Show();
                this.Hide();

                form.FormClosing += frm_closing;
            }
            else
            {
                System.Windows.MessageBox.Show("No se encuentra el Usuario", "Alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
