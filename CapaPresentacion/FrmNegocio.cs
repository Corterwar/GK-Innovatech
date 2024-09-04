using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            if (obtenido )
            {
                picLogo.Image = byteToImage(byteimg);

            }
            Negocio obj = new CN_Negocio().obtenerDatos();

            txtNombre.Text = obj.Nombre;
            txtRuc.Text = obj.RUC;
            txtDireccion.Text = obj.Direccion;

        }

        private void btnSubir_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            OpenFileDialog open = new OpenFileDialog();
            open.FileName = "Files | *.jpg;*.jpeg;*.png";

            if(open.ShowDialog() == DialogResult.OK)
            {
                byte[] img = File.ReadAllBytes(open.FileName);
                bool respuesta = new CN_Negocio().actualizarLogo(img,out mensaje);

                if (respuesta)
                {
                    picLogo.Image = byteToImage(img);
                }
                else
                {
                    MessageBox.Show(mensaje,"Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Negocio obj = new Negocio()
            {
                Nombre = txtNombre.Text,
                RUC = txtRuc.Text,
                Direccion = txtDireccion.Text
            };

            bool respuesta = new CN_Negocio().guardarDatos(obj, out mensaje);

            if (respuesta)
            {
                MessageBox.Show("Los cambios fueron guardados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se guardador los cambios", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
