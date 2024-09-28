using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
using CapaPresentacion.Utilidades;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmVentas : Form
    {
        private Usuario usuarioActual;
        public FrmVentas(Usuario oUsuario = null)
        {
            usuarioActual = oUsuario;
            InitializeComponent();
            
        }

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            comboDocumento.Items.Add(new OpcionesCombo() { Valor = "Recibo", Texto = "Recibo" });
            comboDocumento.Items.Add(new OpcionesCombo() { Valor = "Factura", Texto = "Factura" });

            
            comboDocumento.BackColor = Color.Black;
            comboDocumento.ForeColor = Color.White;
            comboDocumento.DisplayMember = "Texto";
            comboDocumento.ValueMember = "Valor";
            comboDocumento.SelectedIndex = 0;


            txtFecha.Texts = DateTime.Now.ToString("dd/MM/yyyy");
            txtIdProd.Text = "0";

            txtPaga.Texts = "";
            txtCambio.Texts = "";
            txtTotal.Texts = "0";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (var modal = new mdCliente())
            {
                var result = modal.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtDocumento.Texts = modal._Cliente.Documento.ToString();
                    txtNombreC.Texts = modal._Cliente.NombreCompleto.ToString();
                    txtCod.Select();
                }
                else
                {
                    txtDocumento.Select();
                }
            }
        }

        private void btnBuscarProd_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProducto())
            {
                var result = modal.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtIdProd.Text = modal._Producto.IdProducto.ToString();
                    txtCod.Texts = modal._Producto.Codigo.ToString();
                    txtProducto.Texts = modal._Producto.Nombre.ToString();
                    txtPrecio.Texts = modal._Producto.PrecioVenta.ToString("0.00");
                    txtStock.Texts = modal._Producto.Stock.ToString();
                    txtCantidad.Select();
                    txtCantidad.Minimum = 1;
                    txtCantidad.Maximum = modal._Producto.Stock;
                }
                else
                {
                    txtCod.Select();
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            decimal precio = 0;
            bool prodExist = false;

            if (int.Parse(txtIdProd.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un producto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtCod.Texts == "")
            {
                MessageBox.Show("No ingreso un codigo al producto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtProducto.Texts == "")
            {
                MessageBox.Show("No ingreso un nombre al producto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrecio.Texts, out precio))
            {
                MessageBox.Show("Precio - Formato de moneda incorrecto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Select();
                return;
            }

            if (Convert.ToInt32(txtStock.Texts) < Convert.ToInt32(txtCantidad.Value.ToString()))
            {
                MessageBox.Show("La cantidad no puede ser mayor que el stock", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Select();
                return;
            }

            if (Convert.ToInt32(txtCantidad.Value.ToString()) == 0)
            {
                MessageBox.Show("No hay Stock suficiente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Select();
                return;
            }


            foreach (DataGridViewRow fila in dgvData.Rows)
            {
                if (fila.Cells["IdProducto"].Value.ToString() == txtIdProd.Text)
                {
                    prodExist = true;
                    return;
                }
            }

            if (!prodExist)
            {
                Producto oProducto = new CN_Producto().Listar().Where(p => p.Codigo == txtCod.Texts && p.Estado == true).FirstOrDefault();

                bool respuesta = new CN_Venta().RestarStock(Convert.ToInt32(txtIdProd.Text), Convert.ToInt32(txtCantidad.Value.ToString()));

                if (respuesta)
                {

                    dgvData.Rows.Add(new object[]
                {
                    txtIdProd.Text,
                    txtProducto.Texts,
                    oProducto.Descripcion,
                    precio.ToString("0.00"),
                    txtCantidad.Value.ToString(),
                    (txtCantidad.Value * precio).ToString("0.00")
                });

                calcularTotal();
                limpiarCampos();
                txtCod.Select();
                }

            }
        }

        private void txtCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Producto oProducto = new CN_Producto().Listar().Where(p => p.Codigo == txtCod.Texts && p.Estado == true).FirstOrDefault();

                if (oProducto != null)
                {
                    txtCod.ForeColor = Color.Honeydew;
                    txtIdProd.Text = oProducto.IdProducto.ToString();
                    txtProducto.Texts = oProducto.Nombre;
                    txtPrecio.Texts = oProducto.PrecioVenta.ToString("0.00");
                    txtStock.Texts = oProducto.Stock.ToString();
                    txtPrecio.Select();
                        txtCantidad.Minimum = 1;
                        txtCantidad.Maximum = oProducto.Stock;
                }
                else
                {
                    txtCod.ForeColor = Color.MistyRose;
                    txtIdProd.Text = "0";
                    txtProducto.Texts = "";
                    txtPrecio.Texts = "";
                    txtStock.Texts = "";
                    txtCantidad.Value = 1;
                }

            }
        }

        private void limpiarCampos()
        {
            txtIdProd.Text = "0";
            txtCod.Texts = "";
           
            txtProducto.Texts = "";
            txtPrecio.Texts = "";
            txtStock.Texts = "";
        
        }


        private void calcularTotal()
        {
            decimal total = 0;


            if (dgvData.Rows.Count > 0)
            {
                foreach (DataGridViewRow rows in dgvData.Rows)
                {
                    total += Convert.ToDecimal(rows.Cells["SubTotal"].Value.ToString());
                }

            }
            txtTotal.Texts = total.ToString("0.00");
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 6)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var w = Properties.Resources.borrar_1_.Width - 8;
                var h = Properties.Resources.borrar_1_.Height - 8;

                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.borrar_1_, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dgvData.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {

                    bool respuesta = new CN_Venta().SumarStock(Convert.ToInt32(dgvData.Rows[indice].Cells["IdProducto"].Value.ToString()), Convert.ToInt32(dgvData.Rows[indice].Cells["Cantidad"].Value.ToString()));

                    if (respuesta)
                    {
                        dgvData.Rows.RemoveAt(indice);
                        calcularTotal();
                    }

                }

            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar si el carácter es un dígito
            bool esDigito = Char.IsDigit(e.KeyChar);

            bool longitudPermitida = txtPrecio.Texts.Length < 8;
            // Permitir solo dígitos, control (como Backspace), o una coma decimal
            if (esControl || (esDigito && longitudPermitida))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == ',' && (txtPrecio.Texts.Trim().Length > 0 && longitudPermitida) )
            {
                // Permitir una coma decimal solo si no existe ya una en el Textso
                if (txtPrecio.Texts.Contains(","))
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
            else
            {
                // Bloquear cualquier otro carácter
                e.Handled = true;
            }
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtPaga_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo dígitos, control (como Backspace), o una coma decimal
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == ',' && txtPrecio.Texts.Trim().Length > 0)
            {
                // Permitir una coma decimal solo si no existe ya una en el Textso
                if (txtPrecio.Texts.Contains(","))
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
            else
            {
                // Bloquear cualquier otro carácter
                e.Handled = true;
            }
        }

        private void calcularCambio()
        {
            if (txtTotal.Texts.Trim() == "")
            {
                MessageBox.Show("No existen productos en la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            decimal pagacon;
            decimal total = Convert.ToDecimal(txtTotal.Texts);

            if (txtPaga.Texts.Trim() == "")
            {
                txtPaga.Texts = "0";
            }

            if (decimal.TryParse(txtPaga.Texts.Trim(), out pagacon))
            {
                if (pagacon < total)
                {
                    txtCambio.Texts = "0.00";
                }
                else
                {
                    decimal cambio = pagacon - total;
                    txtCambio.Texts = cambio.ToString("0.00");
                }
            }
        }

        private void txtPaga_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                calcularCambio();
            }
        }

        private bool validaciones()
        {
            bool bandera = true;

            if (txtDocumento.Texts == "")
            {
                bandera = false;
            }
            if (txtNombreC.Texts == "")
            {
                bandera = false;
            }
            if (dgvData.Rows.Count < 1)
            {
                bandera = false;
            }
            if (txtPaga.Texts == "" || txtPaga.Texts == "0.00")
            {
                bandera = false;
            }

            return bandera;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (validaciones())
            {
               DialogResult confirmacion =  MessageBox.Show("¿Seguro que desea registrar la venta?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(confirmacion == DialogResult.Yes){


                    DataTable detalle_Venta = new DataTable();
                    detalle_Venta.Columns.Add("IdProducto", typeof(int));
                    detalle_Venta.Columns.Add("PrecioVenta", typeof(decimal));
                    detalle_Venta.Columns.Add("Cantidad", typeof(int));
                    detalle_Venta.Columns.Add("SubTotal", typeof(decimal));

                    foreach (DataGridViewRow row in dgvData.Rows)
                    {
                        detalle_Venta.Rows.Add(
                            new Object[]
                            {
                        Convert.ToInt32(row.Cells["IdProducto"].Value.ToString()),
                        Convert.ToDecimal(row.Cells["Precio"].Value.ToString()),
                         Convert.ToInt32(row.Cells["Cantidad"].Value.ToString()),
                         Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString())
                            }
                            );
                    }

                    int idCorrelativo = new CN_Venta().obtenerCorrelativo();
                    string numeroDocumento = string.Format("{0:00000}", idCorrelativo);
                    calcularCambio();
                    Venta oVenta = new Venta()
                    {
                        oUsuario = new Usuario() { IdUsuario = usuarioActual.IdUsuario },
                        TipoDocumento = ((OpcionesCombo)comboDocumento.SelectedItem).Texto,
                        NumeroDocumento = numeroDocumento,
                        DocumentoCliente = txtDocumento.Texts,
                        NombreCliente = txtNombreC.Texts,
                        MontoPago = Convert.ToDecimal(txtPaga.Texts),
                        MontoCambio = Convert.ToDecimal(txtCambio.Texts),
                        MontoTotal = Convert.ToDecimal(txtTotal.Texts)
                    };

                    string Mensaje = string.Empty;
                    bool respuesta = new CN_Venta().Registrar(oVenta, detalle_Venta, out Mensaje);

                    if (respuesta)
                    {
                        var result = MessageBox.Show("Numero de Venta generada:\n" + numeroDocumento + "\n\n¿Desea copiar al portapapeles?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            Clipboard.SetText(numeroDocumento);
                        }

                        txtDocumento.Texts = "";
                        txtNombreC.Texts = "";
                        dgvData.Rows.Clear();
                        calcularTotal();
                        txtPaga.Texts = "";
                        txtCambio.Texts = "";
                    }
                    else
                    {
                        MessageBox.Show(Mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }


                }


            }
            else
            {
                MessageBox.Show("No se pudo registrar la compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }



        }


        private void txtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar si el carácter es un dígito
            bool esDigito = Char.IsDigit(e.KeyChar);

            // Verificar la longitud actual del texto y permitir solo hasta 8 dígitos
            bool longitudPermitida = txtDocumento.Texts.Length < 8;

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

        private void txtDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Cliente oCliente = new CN_Cliente().Listar().Where(p => p.Documento == txtDocumento.Texts && p.Estado == true).FirstOrDefault();

                if (oCliente != null)
                {
                    txtDocumento.ForeColor = Color.Honeydew;
                    txtIdCliente.Text = oCliente.IdCliente.ToString();
                    txtNombreC.Texts = oCliente.NombreCompleto;

                }
                else
                {
                    txtDocumento.ForeColor = Color.MistyRose;
                    txtIdCliente.Text = "0";
                    txtNombreC.Texts = "";

                }

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtCantidad_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtIdCliente_TextChanged(object sender, EventArgs e)
        {

        }

        private void rjTextBox2__TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
