using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmCupon : Form
    {
        public FrmCupon()
        {
            InitializeComponent();
        }

        private void rjTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si el carácter es una tecla de control (como Backspace).
            bool esControl = Char.IsControl(e.KeyChar);

            // Verifica si el carácter es un dígito.
            bool esDigito = Char.IsDigit(e.KeyChar);

            // Verifica la longitud actual del texto y permite solo hasta 80 caracteres.
            bool longitudPermitida = txtCodigo.Texts.Length < 10;

            // Permite el carácter solo si es una tecla de control o un carácter y la longitud permitida no se ha alcanzado.
            if (esControl ||  longitudPermitida)
            {
                e.Handled = false; // Permite el carácter.
            }
            else
            {
                e.Handled = true; // Bloquea el carácter.
            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si el carácter es una tecla de control (como Backspace).
            bool esControl = Char.IsControl(e.KeyChar);

            // Verifica si el carácter es un dígito.
            bool esDigito = Char.IsDigit(e.KeyChar);

            // Permite hasta tres caracteres para el número 100.
            bool longitudPermitida = txtDescuento.Texts.Length < 3;

            // Inicializamos el valor a 0
            int valor = 0;
            bool valorValido = int.TryParse(txtDescuento.Texts + e.KeyChar, out valor);

            // Permite el carácter solo si es una tecla de control, o si es un dígito dentro de la longitud permitida y el valor resultante es menor o igual a 100
            if (esControl || (esDigito && longitudPermitida && valorValido && valor <= 100))
            {
                e.Handled = false; // Permite el carácter.
            }
            else
            {
                e.Handled = true; // Bloquea el carácter.
            }
        }

        private void FrmCupon_Load(object sender, EventArgs e)
        {
            // Inicializa el comboEstado con opciones para el estado del Cupon (Activo, No Activo).
            comboEstado.Items.Add(new OpcionesCombo() { Valor = 1, Texto = "Activo" });
            comboEstado.Items.Add(new OpcionesCombo() { Valor = 0, Texto = "No Activo" });

            // Configura las propiedades de visualización del comboEstado.
            comboEstado.DisplayMember = "Texto";
            comboEstado.ValueMember = "Valor";
            comboEstado.SelectedIndex = 0;

            // Configura el comboBusqueda para filtrar los resultados en el DataGridView.
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnseleccionar")
                {
                    comboBusqueda.Items.Add(new OpcionesCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            comboBusqueda.DisplayMember = "Texto";
            comboBusqueda.ValueMember = "Valor";
            comboBusqueda.SelectedIndex = 0;


            List<Cupon> listaCupones = new CN_Cupon().obtenerCupones();

            foreach ( Cupon item in listaCupones)
            {
                    dgvData.Rows.Add(new object[] {
                        "", // Columna para el icono de selección
                        item.IdCupon,
                        item.Codigo,
                        item.Descuento,
                        item.Estado == true ? 1 : 0, // Estado como valor
                        item.Estado == true ? "Activo" : "No Activo" // Estado como texto
                    });
            }
        }


        private bool Validaciones()
        {
            bool confirmacion = true;

            // Verifica que el campo de Documento no esté vacío.
            if (txtCodigo.Texts == "")
            {
                confirmacion = false;
            }
            // Verifica que el campo de Dirección no esté vacío.
            if (txtDescuento.Texts == "")
            {
                confirmacion = false;
            }

            // Retorna el resultado de las validaciones.
            return confirmacion;
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            // Valida los campos antes de proceder.
            if (Validaciones())
            {
                DialogResult confirmacion;

                string mensaje = string.Empty;

                // Confirma si se va a agregar o editar un Cupon.
                if (Convert.ToInt32(txtid.Text) == 0)
                {
                    confirmacion = MessageBox.Show("¿Seguro desea agregar el Cupon?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                else
                {
                    confirmacion = MessageBox.Show("¿Seguro desea editar el Cupon?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }

                // Si la respuesta es afirmativa, se crea o edita el Cupon.
                if (confirmacion == DialogResult.Yes)
                {
                    Cupon objCupon = new Cupon()
                    {
                        IdCupon = Convert.ToInt32(txtid.Text),
                        Codigo = txtCodigo.Texts.ToString(),
                        Descuento = Convert.ToInt32(txtDescuento.Texts),
                        Estado = Convert.ToInt32(((OpcionesCombo)comboEstado.SelectedItem).Valor) == 1 ? true : false
                    };

                    // Si es un nuevo Cupon, se registra en la base de datos.
                    if (objCupon.IdCupon == 0)
                    {
                        int idCuponGenerado = new CN_Cupon().Registrar(objCupon, out mensaje);
                        if (idCuponGenerado != 0)
                        {
                            // Agrega el nuevo Cupon al DataGridView.
                            dgvData.Rows.Add(new object[] {
                        "", // Columna para el icono de selección
                        idCuponGenerado,
                        txtCodigo.Texts,
                        txtDescuento.Texts,
                        ((OpcionesCombo)comboEstado.SelectedItem).Valor.ToString(),
                        ((OpcionesCombo)comboEstado.SelectedItem).Texto.ToString()
                    });
                            LimpiarCampos(); // Limpia los campos del formulario.
                        }
                        else
                        {
                            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // Si se está editando, actualiza la información del Cupon.
                        bool resultado = new CN_Cupon().Editar(objCupon, out mensaje);
                        if (resultado == true)
                        {
                            DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtindice.Text)];
                            row.Cells["IdCupon"].Value = txtid.Text;
                            row.Cells["Codigo"].Value = txtCodigo.Texts;
                            row.Cells["Descuento"].Value = txtDescuento.Texts;
                            row.Cells["EstadoValor"].Value = ((OpcionesCombo)comboEstado.SelectedItem).Valor.ToString();
                            row.Cells["Estado"].Value = ((OpcionesCombo)comboEstado.SelectedItem).Texto.ToString();
                            LimpiarCampos(); // Limpia los campos del formulario.
                        }
                        else
                        {
                            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                // Muestra un mensaje si no se completan todos los campos obligatorios.
                MessageBox.Show("Debe Completar todos los campos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void LimpiarCampos()
        {
            txtindice.Text = "-1";
            txtid.Text = "0";
            txtCodigo.Texts = "";
            txtDescuento.Texts = "";
            comboEstado.SelectedIndex = 0;
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Evita la ejecución para el encabezado del DataGridView.
            if (e.RowIndex < 0)
            {
                return;
            }
            // Verifica si la columna es la de selección.
            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All); // Dibuja la celda.
                var w = Properties.Resources.comprobado.Width - 15; // Ancho del icono.
                var h = Properties.Resources.comprobado.Height - 15; // Alto del icono.

                // Calcula la posición para centrar el icono.
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.comprobado, new Rectangle(x, y, w, h)); // Dibuja el icono.
                e.Handled = true; // Marca la celda como manejada.
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica si se ha hecho clic en la columna de selección.
            if (dgvData.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    // Carga los datos del cliente seleccionado en los campos del formulario.
                    txtindice.Text = indice.ToString();
                    txtid.Text = dgvData.Rows[indice].Cells["IdCupon"].Value.ToString();
                    txtCodigo.Texts = dgvData.Rows[indice].Cells["Codigo"].Value.ToString();
                    txtDescuento.Texts = dgvData.Rows[indice].Cells["Descuento"].Value.ToString();


                    // Selecciona el estado correspondiente en el comboEstado.
                    foreach (OpcionesCombo oc in comboEstado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indiceCombo = comboEstado.Items.IndexOf(oc);
                            comboEstado.SelectedIndex = indiceCombo; // Selecciona el estado en el combo.
                            break;
                        }
                    }
                }
            }
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            // Limpia el campo de búsqueda y muestra todas las filas del DataGridView.
            txtBusqueda.Texts = "";
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true; // Muestra todas las filas.
            }
        }

        private void BtnEliminar2_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un cliente para eliminar.
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                // Pregunta al usuario si desea eliminar el cliente.
                if (MessageBox.Show("¿Desea eliminar el Cupon?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Cupon objCupon = new Cupon()
                    {
                        IdCupon = Convert.ToInt32(txtid.Text),
                    };

                    // Llama al método de eliminación en la base de datos.
                    bool respuesta = new CN_Cupon().Eliminar(objCupon, out mensaje);
                    if (respuesta)
                    {
                        // Cambia el estado del cliente en el DataGridView a "No Activo".
                        dgvData.Rows[Convert.ToInt32(txtindice.Text)].Cells["EstadoValor"].Value = 0;
                        dgvData.Rows[Convert.ToInt32(txtindice.Text)].Cells["Estado"].Value = "No Activo";
                        LimpiarCampos(); // Limpia los campos del formulario.
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            // Obtiene el nombre de la columna seleccionada para la búsqueda.
            string columnaFiltro = ((OpcionesCombo)comboBusqueda.SelectedItem).Valor.ToString();

            // Verifica si hay filas en el DataGridView.
            if (dgvData.Rows.Count > 0)
            {
                // Filtra las filas del DataGridView según el texto de búsqueda.
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Texts.Trim().ToUpper()))
                    {
                        row.Visible = true; // Muestra la fila si coincide.
                    }
                    else
                    {
                        row.Visible = false; // Oculta la fila si no coincide.
                    }
                }
            }
        }

        private void BtnLimpiar2_Click(object sender, EventArgs e)
        {
            // Limpia todos los campos del formulario.
            LimpiarCampos();
        }
    }
}
