using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
using CapaPresentacion.Utilidades;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Color = System.Drawing.Color;

namespace CapaPresentacion
{
    public partial class FrmCompras : Form
    {
        private Usuario usuarioActual;

        public FrmCompras(Usuario oUsuario = null)
        {
            usuarioActual = oUsuario;
            InitializeComponent();
        }

        private void FrmCompras_Load(object sender, EventArgs e)
        {
            comboDocumento.Items.Add(new OpcionesCombo() { Valor = "Recibo", Texto = "Recibo" });
            comboDocumento.Items.Add(new OpcionesCombo() { Valor = "Factura", Texto = "Factura" });

            comboDocumento.DisplayMember = "Texto";
            comboDocumento.ValueMember = "Valor";
            comboDocumento.SelectedIndex = 0;


            txtFecha.Texts = DateTime.Now.ToString("dd/MM/yyyy");
            txtIdProd.Text = "0";
            txtIdProveedor.Text = "0";

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProducto())
            {
                var result = modal.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtIdProd.Text = modal._Producto.IdProducto.ToString();
                    txtCod.Texts = modal._Producto.Codigo.ToString();
                    txtProducto.Texts = modal._Producto.Nombre.ToString();
                    txtPrecioC.Select();
                }
                else
                {
                    txtCod.Select();
                }
            }
        }

       

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
            decimal precioCompra = 0;
            decimal precioVenta = 0;
            bool prodExist = false;

            if (int.Parse(txtIdProd.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un producto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrecioC.Texts, out precioCompra))
            {
                MessageBox.Show("Precio de compra formato de moneda incorrecto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecioC.Select();
                return;
            }
            if (!decimal.TryParse(txtPrecioV.Texts, out precioVenta))
            {
                MessageBox.Show("Precio de venta formato de moneda incorrecto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecioV.Select();
                return;
            }

            Producto oProducto = new CN_Producto().Listar().Where(p => p.Codigo == txtCod.Texts && p.Estado == true).FirstOrDefault();

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
                dgvData.Rows.Add(new object[]
                {
                    txtIdProd.Text,
                    txtProducto.Texts,
                    oProducto.Descripcion,
                    precioCompra.ToString("0.00"),
                    precioVenta.ToString("0.00"),
                    txtCantidad.Value.ToString(),
                    (txtCantidad.Value * precioCompra).ToString("0.00")
                });

                calcularTotal();
                limpiarCampos();
                txtCod.Select();
            }


        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProveedor())
            {
                var result = modal.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtIdProveedor.Text = modal._Proveedor.IdProveedor.ToString();
                    txtDocumento.Texts = modal._Proveedor.Documento.ToString();
                    txtRazon.Texts = modal._Proveedor.RazonSocial.ToString();
                }
                else
                {
                    txtDocumento.Select();
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
                    txtPrecioC.Select();
                }
                else
                {
                    txtCod.ForeColor = Color.MistyRose;
                    txtIdProd.Text = "0";
                    txtProducto.Texts = "";
                }

            }
        }

        private void limpiarCampos()
        {
            txtIdProd.Text = "0";
            txtCod.Texts = "";
            txtProducto.Texts = "";
            txtPrecioC.Texts = "";
            txtPrecioV.Texts = "";
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
                
            }
            txtTotal.Texts = total.ToString("0.00");
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 7)
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
                    dgvData.Rows.RemoveAt(indice);
                    calcularTotal();
                }

            }
        }

        private void txtPrecioC_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo dígitos, control (como Backspace), o una coma decimal
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == ',' && txtPrecioC.Texts.Trim().Length > 0)
            {
                // Permitir una coma decimal solo si no existe ya una en el texto
                if (txtPrecioC.Texts.Contains(","))
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

        private void txtPrecioV_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo dígitos, control (como Backspace), o una coma decimal
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == ',' && txtPrecioV.Texts.Trim().Length > 0)
            {
                // Permitir una coma decimal solo si no existe ya una en el texto
                if (txtPrecioV.Texts.Contains(","))
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


        private bool Validaciones()
        {
            bool bandera = true;

            if (txtDocumento.Texts == "")
            {
                bandera = false;
            }
            if (txtRazon.Texts == "")
            {
                bandera = false;
            }
            if (dgvData.Rows.Count < 1)
            {
                bandera = false;
            }
            if (Convert.ToInt32(txtIdProveedor.Text) == 0)
            {
                bandera = false;
            }

            return bandera;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {

            if (Validaciones()){

                DialogResult confirmacion = MessageBox.Show("¿Desea registrar la compra?","Confirmacion",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                if(confirmacion == DialogResult.Yes)
                {
                    DataTable detalle_compra = new DataTable();
                    detalle_compra.Columns.Add("IdProducto", typeof(int));
                    detalle_compra.Columns.Add("PrecioCompra", typeof(decimal));
                    detalle_compra.Columns.Add("PrecioVenta", typeof(decimal));
                    detalle_compra.Columns.Add("Cantidad", typeof(int));
                    detalle_compra.Columns.Add("MontoTotal", typeof(decimal));

                    foreach (DataGridViewRow row in dgvData.Rows)
                    {
                        detalle_compra.Rows.Add(
                            new Object[]
                            {
                                Convert.ToInt32(row.Cells["IdProducto"].Value.ToString()),
                                Convert.ToDecimal(row.Cells["PrecioCompra"].Value.ToString()),
                                Convert.ToDecimal(row.Cells["PrecioVenta"].Value.ToString()),
                                Convert.ToInt32(row.Cells["Cantidad"].Value.ToString()),
                                Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString())
                            }
                            );
                    }


                    int idCorrelativo = new CN_Compra().obtenerCorrelativo();
                    string numeroDocumento = string.Format("{0:00000}", idCorrelativo);

                    Compra oCompra = new Compra()
                    {
                        oUsuario = new Usuario() { IdUsuario = usuarioActual.IdUsuario },
                        oProveedor = new Proveedor() { IdProveedor = Convert.ToInt32(txtIdProveedor.Text) },
                        TipoDocumento = ((OpcionesCombo)comboDocumento.SelectedItem).Texto,
                        NumeroDocumento = numeroDocumento,
                        MontoTotal = Convert.ToDecimal(txtTotal.Texts)

                    };

                    string Mensaje = string.Empty;
                    bool respuesta = new CN_Compra().Registrar(oCompra, detalle_compra, out Mensaje);

                    if (respuesta)
                    {
                        var result = MessageBox.Show("Numero de compra generada:\n" + numeroDocumento + "\n\n¿Desea copiar al portapapeles?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            Clipboard.SetText(numeroDocumento);
                        }

                        txtIdProveedor.Text = "0";
                        txtDocumento.Texts = "";
                        txtRazon.Texts = "";
                        dgvData.Rows.Clear();
                        calcularTotal();
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



        private void txtDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Proveedor oProveedor = new CN_Proveedor().Listar().Where(p => p.Documento == txtDocumento.Texts && p.Estado == true).FirstOrDefault();

                if (oProveedor != null)
                {
                    txtDocumento.BackColor = Color.Honeydew;
                    txtIdProveedor.Text = oProveedor.IdProveedor.ToString();
                    txtRazon.Texts = oProveedor.RazonSocial;

                }
                else
                {
                    txtDocumento.BackColor = Color.MistyRose;
                    txtIdProveedor.Text = "0";
                    txtRazon.Texts = "";

                }

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

        private void txtCantidad_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void txtPrecioC__TextChanged(object sender, EventArgs e)
        {

        }
    }
}
