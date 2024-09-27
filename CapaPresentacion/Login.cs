using CapaEntidad;
using CapaNegocio;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
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

            Usuario ousuario = new CN_Usuario().Listar().Where(u => u.Documento == campoDNI.Text && u.Clave == campoClave.Text && u.Estado == true).FirstOrDefault();

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


        private void frm_closing(object sender, FormClosingEventArgs e)
        {
            campoClave.Text = "";
            campoDNI.Text = "";
            this.Show();
        }

       



        private void btnIngresar2_Click(object sender, EventArgs e)
        {


            Usuario ousuario = new CN_Usuario().Listar().Where(u => u.Documento == campoDNI.Texts && u.Clave == campoClave.Texts && u.Estado == true).FirstOrDefault();

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



        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void campoClave_KeyPress(object sender, KeyPressEventArgs e)
        {

            bool esControl = Char.IsControl(e.KeyChar);
            // Verificar la longitud actual del texto y permitir solo hasta 80 dígitos
            bool longitudPermitida = campoClave.Texts.Trim().Length < 80;

            // Permitir el carácter solo si es una tecla de control o un dígito y la longitud permitida no se ha alcanzado
            if (longitudPermitida || esControl)
            {
                e.Handled = false; // Permitir el carácter
            }
            else
            {
                e.Handled = true; // Bloquear el carácter
            }
        }
        private void campoDNI_KeyPress(object sender, KeyPressEventArgs e)
        {

            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar si el carácter es un dígito
            bool esDigito = Char.IsDigit(e.KeyChar);

            // Verificar la longitud actual del texto y permitir solo hasta 8 dígitos
            bool longitudPermitida = campoDNI.Texts.Length < 8;

            // Permitir el carácter solo si es una tecla de control o un dígito y la longitud permitida no se ha alcanzado
            if (esControl || (esDigito && longitudPermitida))
            {
                e.Handled = false; // Permitir el carácter
            }
            else
            {
                e.Handled = true; // Bloquear el carácter
            }



        }

    }


}
