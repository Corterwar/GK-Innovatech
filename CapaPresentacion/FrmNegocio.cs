using CapaEntidad;
using CapaNegocio;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmNegocio : Form
    {
        public FrmNegocio()
        {
            InitializeComponent();
        }

        public Image byteToImage(byte[] img)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(img, 0, img.Length);
            Image imagen = new Bitmap(ms);

            return imagen;
        }

        private void FrmNegocio_Load(object sender, EventArgs e)
        {
            bool obtenido = true;
            byte[] byteimg = new CN_Negocio().obtenerLogo(out obtenido);

            if (obtenido)
            {
                picLogo.Image = byteToImage(byteimg);

            }
            Negocio obj = new CN_Negocio().obtenerDatos();

            txtNombre.Texts = obj.Nombre;
            txtRuc.Texts = obj.RUC;
            txtDireccion.Texts = obj.Direccion;

        }

        private void btnSubir_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            OpenFileDialog open = new OpenFileDialog();
            open.FileName = "Files | *.jpg;*.jpeg;*.png";

            if (open.ShowDialog() == DialogResult.OK)
            {
                byte[] img = File.ReadAllBytes(open.FileName);
                bool respuesta = new CN_Negocio().actualizarLogo(img, out mensaje);

                if (respuesta)
                {
                    picLogo.Image = byteToImage(img);
                }            //Este deberia de ir arriba despues con la base de dato
                else
                {
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }

        }


        private bool Validaciones()
        {
            bool validacion = true;

            if (txtNombre.Texts == "")
            {
                validacion = false;
            }
            if (txtRuc.Texts == "")
            {
                validacion = false;
            }
            if (txtDireccion.Texts == "")
            {
                validacion = false;
            }

            return validacion;
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            if (Validaciones())
            {

                DialogResult confirmacion = MessageBox.Show("¿Seguro desea agregar el usuario?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacion == DialogResult.Yes)
                {
                    Negocio obj = new Negocio()
                    {
                        Nombre = txtNombre.Texts,
                        RUC = txtRuc.Texts,
                        Direccion = txtDireccion.Texts
                    };

                    bool respuesta = new CN_Negocio().guardarDatos(obj, out mensaje);


                    if (respuesta)
                    {
                        MessageBox.Show("Los cambios fueron guardados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se guardaron los cambios", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else
            {
                MessageBox.Show("Debe Completar todos los campos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


            
        }

        private void txtRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar si el carácter es un dígito
            bool esDigito = Char.IsDigit(e.KeyChar);

            // Verificar la longitud actual del texto y permitir solo hasta 8 dígitos
            bool longitudPermitida = txtRuc.Texts.Length < 11;

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
