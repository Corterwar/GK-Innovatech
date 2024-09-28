using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmProducto : Form
    {
        public FrmProducto()
        {
            InitializeComponent();

        }

        private void FrmProducto_Load(object sender, EventArgs e)
        {
            comboEstado.Items.Add(new OpcionesCombo() { Valor = 1, Texto = "Activo" });
            comboEstado.Items.Add(new OpcionesCombo() { Valor = 0, Texto = "No Activo" });

            comboEstado.DisplayMember = "Texto";
            comboEstado.ValueMember = "Valor";
            comboEstado.SelectedIndex = 0;

            List<Categoria> listaCategoria = new CN_Categoria().Listar();

            if (listaCategoria != null && listaCategoria.Count > 0)
            {
                foreach (Categoria item in listaCategoria)
                {
                    comboCategoria.Items.Add(new OpcionesCombo() { Valor = item.IdCategoria, Texto = item.Descripcion });
                }
            }
            else
            {
                comboCategoria.Items.Add(new OpcionesCombo() { Valor = 0, Texto = "Todos" });
            }
            comboCategoria.DisplayMember = "Texto";
            comboCategoria.ValueMember = "Valor";
            comboCategoria.SelectedIndex = 0; // Solo establece el índice si hay elementos


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

            //Mostrar Productos en el dataGridView
            List<Producto> listaProductos = new CN_Producto().Listar();

            foreach (Producto item in listaProductos)
            {
                dgvData.Rows.Add(new object[] {
                    "",
                    item.IdProducto,
                    item.Codigo,
                    item.Nombre,
                    item.Descripcion,
                    item.Marca,
                    item.oCategoria.IdCategoria,
                    item.oCategoria.Descripcion,
                    item.Stock,
                    item.PrecioCompra,
                    item.PrecioVenta,
                    item.Estado == true ? 1 : 0,
                    item.Estado == true ? "Activo": "No Activo"
                });
            }
            comboCategoria.DisplayMember = "Texto";
            comboCategoria.ValueMember = "Valor";
            comboCategoria.SelectedIndex = 0;
        }

        private bool Validaciones()
        {
            bool validaciones = true;

            if (txtCodigo.Texts == "")
            {
                validaciones = false;
            }
            if (txtNombre.Texts == "")
            {
                validaciones = false;
            }
            if (txtDescripcion.Texts == "")
            {
                validaciones = false;
            }
            if(txtMarca.Texts == "") 
            { 
                validaciones = false; 
            }

            return validaciones;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Validaciones())
            {
                string mensaje = string.Empty;
                DialogResult confirmacion;

                if (Convert.ToInt32(txtid.Text) == 0)
                {
                    confirmacion = MessageBox.Show("¿Seguro desea agregar el producto?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                else
                {
                    confirmacion = MessageBox.Show("¿Seguro desea editar el producto?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
           
                if(confirmacion == DialogResult.Yes)
                {
                    Producto objProducto = new Producto()
                    {
                        IdProducto = Convert.ToInt32(txtid.Text),
                        Codigo = txtCodigo.Texts,
                        Nombre = txtNombre.Texts,
                        Descripcion = txtDescripcion.Texts,
                        Marca = txtMarca.Texts,
                        oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(((OpcionesCombo)comboCategoria.SelectedItem).Valor) },
                        Estado = Convert.ToInt32(((OpcionesCombo)comboEstado.SelectedItem).Valor) == 1 ? true : false
                    };

                    if (objProducto.IdProducto == 0)
                    {
                        int idProductoGenerado = new CN_Producto().Registrar(objProducto, out mensaje);
                        if (idProductoGenerado != 0)
                        {
                            dgvData.Rows.Add(new object[] {
                                "",
                                idProductoGenerado,
                                txtCodigo.Texts,
                                txtNombre.Texts,
                                txtDescripcion.Texts,
                                txtMarca.Texts,
                                ((OpcionesCombo)comboCategoria.SelectedItem).Valor.ToString(),
                                ((OpcionesCombo)comboCategoria.SelectedItem).Texto.ToString(),
                                "0",
                                "0.00",
                                "0.00",
                                ((OpcionesCombo)comboEstado.SelectedItem).Valor.ToString(),
                                ((OpcionesCombo)comboEstado.SelectedItem).Texto.ToString()
                            });

                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show(mensaje);
                        }
                    }
                    else
                    {
                        bool resultado = new CN_Producto().Editar(objProducto, out mensaje);
                        if (resultado == true)
                        {
                            DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtindice.Text)];
                            row.Cells["IdProducto"].Value = txtid.Text;
                            row.Cells["Codigo"].Value = txtCodigo.Texts;
                            row.Cells["Nombre"].Value = txtNombre.Texts;
                            row.Cells["Descripcion"].Value = txtDescripcion.Texts;
                            row.Cells["Marca"].Value = txtMarca.Texts;
                            row.Cells["IdCategoria"].Value = ((OpcionesCombo)comboCategoria.SelectedItem).Valor.ToString();
                            row.Cells["Categoria"].Value = ((OpcionesCombo)comboCategoria.SelectedItem).Texto.ToString();
                            row.Cells["EstadoValor"].Value = ((OpcionesCombo)comboEstado.SelectedItem).Valor.ToString();
                            row.Cells["Estado"].Value = ((OpcionesCombo)comboEstado.SelectedItem).Texto.ToString();

                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show(mensaje);
                        }
                    }
                }

            }
            else
            {

                MessageBox.Show("Debe Completar todos los campos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el Producto?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Producto objProducto = new Producto()
                    {
                        IdProducto = Convert.ToInt32(txtid.Text),
                    };

                    bool respuesta = new CN_Producto().Eliminar(objProducto, out mensaje); ;
                    if (respuesta)
                    {
                        dgvData.Rows[Convert.ToInt32(txtindice.Text)].Cells["EstadoValor"].Value = 0;
                        dgvData.Rows[Convert.ToInt32(txtindice.Text)].Cells["Estado"].Value = "No Activo";
                     
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }
 

        private void LimpiarCampos()
        {
            txtindice.Text = "-1";
            txtid.Text = "0";
            txtCodigo.Texts = "";
            txtNombre.Texts = "";
            txtDescripcion.Texts = "";
            txtMarca.Texts = "";
            comboCategoria.SelectedIndex = 0;
            comboEstado.SelectedIndex = 0;
            txtCodigo.Select();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtindice.Text = indice.ToString();
                    txtid.Text = dgvData.Rows[indice].Cells["IdProducto"].Value.ToString();
                    txtCodigo.Texts = dgvData.Rows[indice].Cells["Codigo"].Value.ToString();
                    txtNombre.Texts = dgvData.Rows[indice].Cells["Nombre"].Value.ToString();
                    txtDescripcion.Texts = dgvData.Rows[indice].Cells["Descripcion"].Value.ToString();



                    foreach (OpcionesCombo oc in comboCategoria.Items)
                    {

                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["IdCategoria"].Value))
                        {
                            int indiceCombo = comboCategoria.Items.IndexOf(oc);
                            comboCategoria.SelectedIndex = indiceCombo;
                            break;
                        }

                    }

                    foreach (OpcionesCombo oc in comboEstado.Items)
                    {

                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indiceCombo = comboEstado.Items.IndexOf(oc);
                            comboEstado.SelectedIndex = indiceCombo;
                            break;
                        }

                    }

                }

            }
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var w = Properties.Resources.comprobado.Width - 15;
                var h = Properties.Resources.comprobado.Height - 15;

                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.comprobado, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionesCombo)comboBusqueda.SelectedItem).Valor.ToString();

            if (dgvData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Texts.Trim().ToUpper()))
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }

                }
            }
        }

        private void btnLimpiarBusqueda_Click_1(object sender, EventArgs e)
        {
            txtBusqueda.Texts = "";
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable();
                foreach (DataGridViewColumn colum in dgvData.Columns)
                {
                    if (colum.HeaderText != "" && colum.Visible)
                    {
                        dt.Columns.Add(colum.HeaderText, typeof(string));
                    }
                }

                foreach (DataGridViewRow rows in dgvData.Rows)
                {
                    if (rows.Visible)
                    {
                        dt.Rows.Add(new object[]
                        {
                            rows.Cells[2].Value.ToString(),
                            rows.Cells[3].Value.ToString(),
                            rows.Cells[4].Value.ToString(),
                            rows.Cells[5].Value.ToString(),
                            rows.Cells[7].Value.ToString(),
                            rows.Cells[8].Value.ToString(),
                            rows.Cells[9].Value.ToString(),
                            rows.Cells[10].Value.ToString(),
                            rows.Cells[12].Value.ToString(),
                        });

                    }
                }
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("ReporteProducto_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
                savefile.Filter = "Excel Files | *.xlsx";

                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XLWorkbook wb = new XLWorkbook();
                        var hoja = wb.Worksheets.Add(dt, "Informe");
                        hoja.ColumnsUsed().AdjustToContents();
                        wb.SaveAs(savefile.FileName);
                        MessageBox.Show("Reporte Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Error al generar el informe", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                }
            }
        }

        private void txtCodigo__TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombre__TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDescripcion__TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBusqueda__TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboEstado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar si el carácter es un dígito
            bool esDigito = Char.IsDigit(e.KeyChar);

            // Verificar la longitud actual del texto y permitir solo hasta 8 dígitos
            bool longitudPermitida = txtNombre.Texts.Length < 80;

            // Permitir el carácter solo si es una tecla de control o un dígito y la longitud permitida no se ha alcanzado
            if (esControl || (!esDigito && longitudPermitida))
            {
                e.Handled = false; // Permitir el carácter
            }
            else
            {
                e.Handled = true; // Bloquear el carácter
            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar si el carácter es un dígito
            bool esDigito = Char.IsDigit(e.KeyChar);

            // Verificar la longitud actual del texto y permitir solo hasta 8 dígitos
            bool longitudPermitida = txtDescripcion.Texts.Length < 80;

            // Permitir el carácter solo si es una tecla de control o un dígito y la longitud permitida no se ha alcanzado
            if (esControl ||  longitudPermitida)
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
