using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            comboDocumento.DisplayMember = "Texto";
            comboDocumento.ValueMember = "Valor";
            comboDocumento.SelectedIndex = 0;


            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtIdProd.Text = "0";

            txtPaga.Text = "";
            txtCambio.Text = "";
            txtTotal.Text = "0";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (var modal = new mdCliente())
            {
                var result = modal.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtDocumento.Text = modal._Cliente.Documento.ToString();
                    txtNombreC.Text = modal._Cliente.NombreCompleto.ToString();
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
                    txtCod.Text = modal._Producto.Codigo.ToString();
                    txtProducto.Text = modal._Producto.Nombre.ToString();
                    txtPrecio.Text = modal._Producto.PrecioVenta.ToString("0.00");
                    txtStock.Text = modal._Producto.Stock.ToString();
                    txtCantidad.Select();
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

            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("Precio - Formato de moneda incorrecto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Select();
                return;
            }

            if (Convert.ToInt32(txtStock.Text) < Convert.ToInt32(txtCantidad.Value.ToString()))
            {
                MessageBox.Show("La cantidad no puede ser mayor que el stock", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                bool respuesta = new CN_Venta().RestarStock(Convert.ToInt32(txtIdProd.Text), Convert.ToInt32(txtCantidad.Value.ToString()));

                if (respuesta)
                {

                    dgvData.Rows.Add(new object[]
                    {
                    txtIdProd.Text,
                    txtProducto.Text,
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
                Producto oProducto = new CN_Producto().Listar().Where(p => p.Codigo == txtCod.Text && p.Estado == true).FirstOrDefault();

                if (oProducto != null)
                {
                    txtCod.BackColor = Color.Honeydew;
                    txtIdProd.Text = oProducto.IdProducto.ToString();
                    txtProducto.Text = oProducto.Nombre;
                    txtPrecio.Text = oProducto.PrecioVenta.ToString("0.00");
                    txtStock.Text = oProducto.Stock.ToString();
                    txtCantidad.Select();
                }
                else
                {
                    txtCod.BackColor = Color.MistyRose;
                    txtIdProd.Text = "0";
                    txtProducto.Text = "";
                    txtPrecio.Text = "";
                    txtStock.Text = "";
                    txtCantidad.Value = 1;
                }

            }
        }

        private void limpiarCampos()
        {
            txtIdProd.Text = "0";
            txtCod.Text = "";
            txtCod.BackColor = Color.White;
            txtProducto.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            txtCantidad.Value = 1;
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
                txtTotal.Text = total.ToString("0.00");
            }
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 5)
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
            // Permitir solo dígitos, control (como Backspace), o una coma decimal
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == ',' && txtPrecio.Text.Trim().Length > 0)
            {
                // Permitir una coma decimal solo si no existe ya una en el texto
                if (txtPrecio.Text.Contains(","))
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
            else if (e.KeyChar == ',' && txtPrecio.Text.Trim().Length > 0)
            {
                // Permitir una coma decimal solo si no existe ya una en el texto
                if (txtPrecio.Text.Contains(","))
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
            if(txtTotal.Text.Trim() == "")
            {
                MessageBox.Show("No existen productos en la venta","Mensaje", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
            decimal pagacon;
            decimal total = Convert.ToDecimal(txtTotal.Text);

            if(txtPaga.Text.Trim() == "")
            {
                txtPaga.Text = "0";
            }

            if (decimal.TryParse(txtPaga.Text.Trim(), out pagacon))
            {
                if (pagacon < total)
                {
                    txtCambio.Text = "0.00";
                }
                else
                {
                    decimal cambio = pagacon - total;
                    txtCambio.Text = cambio.ToString("0.00");
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

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if(txtDocumento.Text == "")
            {
                MessageBox.Show("Debe ingresar un documento del cliente", "Mensaje", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
            if(txtNombreC.Text == "")
            {
                MessageBox.Show("Debe ingresar un nombre del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if(dgvData.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar productos a la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

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
                DocumentoCliente = txtDocumento.Text,
                NombreCliente = txtNombreC.Text,
                MontoPago = Convert.ToDecimal(txtPaga.Text),
                MontoCambio = Convert.ToDecimal(txtCambio.Text),
                MontoTotal = Convert.ToDecimal(txtTotal.Text)
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

                txtDocumento.Text = "";
                txtNombreC.Text = "";
                dgvData.Rows.Clear();
                calcularTotal();
                txtPaga.Text = "";
                txtCambio.Text = "";
            }
            else
            {
                MessageBox.Show(Mensaje,"Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }


    }
}
