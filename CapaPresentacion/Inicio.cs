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
    // Clase que representa el formulario principal de la aplicación.
    public partial class Inicio : Form
    {
        // Variables estáticas para gestionar el usuario, el menú activo y el formulario activo.
        private static Usuario user; // Variable para almacenar el usuario actual.
        private static IconMenuItem menuActivo = null; // Almacena el menú que está activo.
        private static Form formActivo = null; // Almacena el formulario activo.

        // Constructor de la clase, recibe el objeto Usuario y lo asigna.
        public Inicio(Usuario objusuario)
        {
            user = objusuario; // Asigna el usuario recibido.
            InitializeComponent(); // Inicializa los componentes visuales del formulario.
            abrirFormulario2(new FrmInicio()); // Abre el formulario de inicio por defecto.
            lblRol.Text += " " + user.oRol.Descripcion; // Muestra el rol del usuario en la etiqueta.
        }

        // Método para abrir un formulario sin modificar el menú activo.
        private void abrirFormulario2(Form formulario)
        {
            // Restablece el color del menú activo si existe.
            if (menuActivo != null)
            {
                menuActivo.BackColor = Color.FromArgb(38, 50, 56); // Color por defecto.
            }

            // Cierra el formulario activo si ya hay uno abierto.
            if (formActivo != null)
            {
                formActivo.Close();
            }

            formActivo = formulario; // Asigna el nuevo formulario como activo.
            formulario.TopLevel = false; // Configura el formulario para que no sea de nivel superior.
            formulario.FormBorderStyle = FormBorderStyle.None; // Elimina los bordes del formulario.
            formulario.Dock = DockStyle.Fill; // Establece el formulario para que ocupe todo el contenedor.
            formulario.BackColor = Color.FromArgb(44, 53, 68); // Cambia el color de fondo.
            Contenedor.Controls.Add(formulario); // Añade el formulario al contenedor visual.
            formulario.Show(); // Muestra el formulario.
        }

        // Método para abrir un formulario y cambiar el menú activo.
        private void abrirFormulario(IconMenuItem menu, Form formulario)
        {
            // Restablece el color del menú activo si existe.
            if (menuActivo != null)
            {
                menuActivo.BackColor = Color.FromArgb(38, 50, 56);
            }

            // Cambia el color del menú seleccionado.
            menu.BackColor = Color.FromArgb(30, 30, 30);
            menuActivo = menu; // Asigna el nuevo menú como activo.

            // Cierra el formulario activo si ya hay uno abierto.
            if (formActivo != null)
            {
                formActivo.Close();
            }

            formActivo = formulario; // Asigna el nuevo formulario como activo.
            formulario.TopLevel = false; // Configura el formulario para que no sea de nivel superior.
            formulario.FormBorderStyle = FormBorderStyle.None; // Elimina los bordes del formulario.
            formulario.Dock = DockStyle.Fill; // Establece el formulario para que ocupe todo el contenedor.
            formulario.BackColor = Color.FromArgb(44, 53, 68); // Cambia el color de fondo.
            Contenedor.Controls.Add(formulario); // Añade el formulario al contenedor visual.
            formulario.Show(); // Muestra el formulario.
        }

        // Evento que se ejecuta al cargar el formulario principal.
        private void Inicio_Load(object sender, EventArgs e)
        {
            // Obtiene los permisos del usuario actual desde la capa de negocio.
            List<Permiso> listaPermisos = new CN_Permiso().Listar(user.IdUsuario);

            // Recorre los elementos del menú lateral y oculta aquellos que el usuario no tiene permiso para ver.
            foreach (IconMenuItem iconmenu in menuLateral.Items)
            {
                bool encontrado = listaPermisos.Any(m => m.NombreMenu == iconmenu.Name);

                if (!encontrado)
                {
                    iconmenu.Visible = false; // Oculta el menú si el usuario no tiene permiso.
                }
            }

            // Muestra el nombre completo del usuario en el formulario.
            nombreUser.Text = user.NombreCompleto;
        }

        // Evento que abre el formulario de productos cuando se hace clic en el menú correspondiente.
        private void Productos_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmProducto()); // Abre el formulario de productos.
        }

        // Evento que abre el formulario de inicio cuando se hace clic en el menú correspondiente.
        private void menuInicio_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmInicio()); // Abre el formulario de inicio.
        }

        // Evento que abre el formulario de clientes cuando se hace clic en el menú correspondiente.
        private void menuClientes_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmClientes()); // Abre el formulario de clientes.
        }

        // Evento que abre el formulario de proveedores cuando se hace clic en el menú correspondiente.
        private void menuProveedores_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmProveedores()); // Abre el formulario de proveedores.
        }

        // Evento que abre el formulario de usuarios cuando se hace clic en el menú correspondiente.
        private void menuUsuarios_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmUsuario()); // Abre el formulario de usuarios.
        }

        // Evento que abre el formulario de categorías cuando se hace clic en el menú correspondiente.
        private void Categorias_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmCategoria()); // Abre el formulario de categorías.
        }

        // Evento que abre el formulario para registrar ventas cuando se hace clic en el menú correspondiente.
        private void menuRegistrarVenta_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmVentas(user)); // Abre el formulario de registro de ventas.
        }

        // Evento que abre el formulario para registrar compras cuando se hace clic en el menú correspondiente.
        private void menuRegistrarCompra_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmCompras(user)); // Abre el formulario de registro de compras.
        }

        // Evento que abre el formulario de detalles de ventas cuando se hace clic en el menú correspondiente.
        private void menuVerDetalle_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmDetalleVenta()); // Abre el formulario de detalle de ventas.
        }

        // Evento que abre el formulario de detalles de compras cuando se hace clic en el menú correspondiente.
        private void menuVerDetalleCompra_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmDetalleCompra(user)); // Abre el formulario de detalle de compras.
        }

        // Evento que abre el formulario de configuración del negocio, si el usuario tiene permisos.
        private void Negocio_Click(object sender, EventArgs e)
        {
            if (user.oRol.IdRol == 1) // Verifica si el usuario es administrador.
            {
                abrirFormulario((IconMenuItem)sender, new FrmNegocio()); // Abre el formulario de configuración del negocio.
            }
            else
            {
                MessageBox.Show("No tiene los permisos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Muestra una alerta si no tiene permiso.
            }
        }

        // Evento para cerrar la sesión del usuario actual.
        private void iconMenuItem9_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Desea Cerrar la sesión?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                this.Close(); // Cierra el formulario y finaliza la sesión.
            }
        }

        // Evento que abre el submenú de reportes de ventas.
        private void subMenuRVentas_Click(object sender, EventArgs e)
        {
            abrirFormulario(menuReportes, new FrmReporteVentas()); // Abre el formulario de reportes de ventas.
        }

        // Evento que abre el submenú de reportes de compras.
        private void subMenuRCompras_Click(object sender, EventArgs e)
        {
            abrirFormulario(menuReportes, new FrmReporteCompra()); // Abre el formulario de reportes de compras.
        }

        // Evento que abre el formulario de gráficos cuando se hace clic en el menú correspondiente.
        private void menuGraficos_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmGraficos()); // Abre el formulario de gráficos.
        }
    }
}
