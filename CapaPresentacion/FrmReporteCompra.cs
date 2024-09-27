using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmReporteCompra : Form
    {
        public FrmReporteCompra()
        {
            InitializeComponent();
        }

        private void FrmReporteCompra_Load(object sender, EventArgs e)
        {
            //Mostrar Proveedors en el dataGridView
            List<Proveedor> listaProveedores = new CN_Proveedor().Listar();

            comboProveedor.Items.Add(new OpcionesCombo() { Valor = 0, Texto = "Todos" });
            foreach (Proveedor item in listaProveedores)
            {
                comboProveedor.Items.Add(new OpcionesCombo() { Valor = item.IdProveedor, Texto = item.RazonSocial });

            }
            comboProveedor.DisplayMember = "Texto";
            comboProveedor.ValueMember = "Valor";
            comboProveedor.SelectedIndex = 0;


            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible == true)
                {
                    comboBusqueda.Items.Add(new OpcionesCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            comboBusqueda.DisplayMember = "Texto";
            comboBusqueda.ValueMember = "Valor";
            comboBusqueda.SelectedIndex = 0;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int idproveedor = Convert.ToInt32(((OpcionesCombo)comboProveedor.SelectedItem).Valor.ToString());

            List<ReporteCompra> lista = new List<ReporteCompra>();

            lista = new CN_Reporte().Compra(
                dtFechaInicio.Value,
                dtFechaFin.Value,
                idproveedor
            );

            dgvData.Rows.Clear();

            foreach (ReporteCompra rc in lista)
            {
                dgvData.Rows.Add(new object[]
                {
                    rc.FechaRegistro,
                    rc.TipoDocumento,
                    rc.NumeroDocumento,
                    rc.MontoTotal,
                    rc.UsuarioRegistro,
                    rc.DocumentoProveedor,
                    rc.RazonSocial,
                    rc.CodigoProducto,
                    rc.NombreProducto,
                    rc.Categoria,
                    rc.PrecioCompra,
                    rc.PrecioVenta,
                    rc.Cantidad,
                    rc.SubTotal
                });
            }

        }

        private void btnBuscar2_Click(object sender, EventArgs e)
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

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
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
                    dt.Columns.Add(colum.HeaderText, typeof(string));
                }

                foreach (DataGridViewRow rows in dgvData.Rows)
                {
                    if (rows.Visible)
                    {
                        dt.Rows.Add(new object[]
                        {
                            rows.Cells[0].Value.ToString(),
                            rows.Cells[1].Value.ToString(),
                            rows.Cells[2].Value.ToString(),
                            rows.Cells[3].Value.ToString(),
                            rows.Cells[4].Value.ToString(),
                            rows.Cells[5].Value.ToString(),
                            rows.Cells[6].Value.ToString(),
                            rows.Cells[7].Value.ToString(),
                            rows.Cells[8].Value.ToString(),
                            rows.Cells[9].Value.ToString(),
                            rows.Cells[10].Value.ToString(),
                            rows.Cells[11].Value.ToString(),
                            rows.Cells[12].Value.ToString(),
                            rows.Cells[13].Value.ToString(),


                        });

                    }
                }
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("ReporteCompras_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
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


        private void BtnGuardar2_Click(object sender, EventArgs e)
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
                    dt.Columns.Add(colum.HeaderText, typeof(string));
                }

                foreach (DataGridViewRow rows in dgvData.Rows)
                {
                    if (rows.Visible)
                    {
                        dt.Rows.Add(new object[]
                        {
                            rows.Cells[0].Value.ToString(),
                            rows.Cells[1].Value.ToString(),
                            rows.Cells[2].Value.ToString(),
                            rows.Cells[3].Value.ToString(),
                            rows.Cells[4].Value.ToString(),
                            rows.Cells[5].Value.ToString(),
                            rows.Cells[6].Value.ToString(),
                            rows.Cells[7].Value.ToString(),
                            rows.Cells[8].Value.ToString(),
                            rows.Cells[9].Value.ToString(),
                            rows.Cells[10].Value.ToString(),
                            rows.Cells[11].Value.ToString(),
                            rows.Cells[12].Value.ToString(),
                            rows.Cells[13].Value.ToString(),


                        });

                    }
                }
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("ReporteCompras_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
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

        private void dtFechaInicio_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtFechaFin_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtBusqueda__TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
