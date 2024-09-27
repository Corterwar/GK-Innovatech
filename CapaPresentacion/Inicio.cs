using CapaEntidad;
using CapaNegocio;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace CapaPresentacion
{
    public partial class Inicio : Form
    {
        private static Usuario user;
        private static IconMenuItem menuActivo = null;
        private static Form formActivo = null;


        public Inicio(Usuario objusuario)
        {

            user = objusuario;
            InitializeComponent();
            abrirFormulario2(new FrmInicio());
        }

        private void abrirFormulario2(Form formulario)
        {
            if (menuActivo != null)
            {
                menuActivo.BackColor = Color.FromArgb(38, 50, 56);
            }


            if (formActivo != null)
            {
                formActivo.Close();
            }

            formActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.FromArgb(44, 53, 68);
            Contenedor.Controls.Add(formulario);
            formulario.Show();
        }

        private void abrirFormulario(IconMenuItem menu, Form formulario)
        {
            if (menuActivo != null)
            {
                menuActivo.BackColor = Color.FromArgb(38, 50, 56);
            }
            menu.BackColor = Color.FromArgb(30, 30, 30);
            menuActivo = menu;

            if (formActivo != null)
            {
                formActivo.Close();
            }

            formActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.FromArgb(44, 53, 68);
            //formulario.BackColor = Color.FromArgb(38, 50, 56);
            Contenedor.Controls.Add(formulario);
            formulario.Show();
        }


        private void Inicio_Load(object sender, EventArgs e)
        {
            List<Permiso> listaPermisos = new CN_Permiso().Listar(user.IdUsuario);

            foreach (IconMenuItem iconmenu in menuLateral.Items)
            {
                bool encontrado = listaPermisos.Any(m => m.NombreMenu == iconmenu.Name);

                if (encontrado == false)
                {
                    iconmenu.Visible = false;
                }
            }

            nombreUser.Text = user.NombreCompleto;
        }

        

        private void Productos_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmProducto());
        }

      

        private void menuInicio_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmInicio());
        }

        private void menuClientes_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmClientes());
        }



        private void menuProveedores_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmProveedores());
        }

        private void menuUsuarios_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmUsuario());
        }

        private void Categorias_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmCategoria());
        }

        private void menuRegistrarVenta_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmVentas(user));
        }

        private void menuRegistrarCompra_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmCompras(user));
        }

        private void menuVerDetalle_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmDetalleVenta());
        }

        private void menuVerDetalleCompra_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmDetalleCompra());
        }

        private void Negocio_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmNegocio());
        }

        private void iconMenuItem9_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Desea Cerrar la sesión?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                this.Close();
            }


        }

        private void subMenuRVentas_Click(object sender, EventArgs e)
        {
            abrirFormulario(menuReportes, new FrmReporteVentas());
        }

        private void subMenuRCompras_Click(object sender, EventArgs e)
        {
            abrirFormulario(menuReportes, new FrmReporteCompra());
        }

        private void menuLateral_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuGraficos_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmGraficos());
        }
    }
}
