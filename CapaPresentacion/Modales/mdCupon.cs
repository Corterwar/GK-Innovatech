using CapaEntidad;
using CapaNegocio;
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

namespace CapaPresentacion.Modales
{
    public partial class mdCupon : Form
    {
        public Cupon cupon { set; get; }
        public mdCupon()
        {
            InitializeComponent();
            // Configura la opacidad del formulario (50% de transparencia)
            this.Opacity = 1;

            // Establece un color clave de transparencia (el color de fondo del formulario será transparente)
            this.BackColor = Color.FromArgb(36, 35, 58); // El color que quieres transparente
            this.TransparencyKey = this.BackColor;
        }

        private void mdCupon_Load(object sender, EventArgs e)
        {
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

            //Lista todos los productos
            List<Cupon> listaCupones = new CN_Cupon().obtenerCupones();

            //Se le aplica un filtro para no mostrar los no activos
            List<Cupon> lista = listaCupones.Where(p => p.Estado == true).ToList();

            foreach (Cupon item in lista)
            {
                dgvData.Rows.Add(new object[] {
                    item.IdCupon,
                    item.Codigo,
                    item.Descuento,
                });
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

        private void btnBuscar_Click(object sender, EventArgs e)
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int iColumn = e.ColumnIndex;

            if (iRow >= 0 && iColumn > 0)
            {
                this.cupon = new Cupon()
                {
                    IdCupon = Convert.ToInt32(dgvData.Rows[iRow].Cells["IdCupon"].Value.ToString()),
                    Codigo = dgvData.Rows[iRow].Cells["Codigo"].Value.ToString(),
                    Descuento = Convert.ToInt32(dgvData.Rows[iRow].Cells["Descuento"].Value)
                   
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
