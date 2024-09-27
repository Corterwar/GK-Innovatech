using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmUsuario : Form
    {
        public FrmUsuario()
        {
            InitializeComponent();

        }



        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            Usuario objUsuario = new Usuario()
            {
                IdUsuario = Convert.ToInt32(txtid.Text),
                Documento = txtDocumento.Texts,
                NombreCompleto = txtNombre.Texts,
                Correo = txtCorreo.Texts,
                Clave = txtClave.Texts,
                oRol = new Rol() { IdRol = Convert.ToInt32(((OpcionesCombo)comboRol.SelectedItem).Valor) },
                Estado = Convert.ToInt32(((OpcionesCombo)comboEstado.SelectedItem).Valor) == 1 ? true : false
            };

            if (objUsuario.IdUsuario == 0)
            {
                int idUsuarioGenerado = new CN_Usuario().Registrar(objUsuario, out mensaje);
                if (idUsuarioGenerado != 0)
                {
                    dgvData.Rows.Add(new object[] {"",idUsuarioGenerado,txtDocumento.Texts,txtNombre.Texts,txtCorreo.Texts,txtClave.Texts,
                ((OpcionesCombo)comboRol.SelectedItem).Valor.ToString(),
                ((OpcionesCombo)comboRol.SelectedItem).Texto.ToString(),
                ((OpcionesCombo)comboEstado.SelectedItem).Valor.ToString(),
                ((OpcionesCombo)comboEstado.SelectedItem).Texto.ToString()});
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            else
            {
                bool resultado = new CN_Usuario().Editar(objUsuario, out mensaje);
                if (resultado == true)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtindice.Text)];
                    row.Cells["IdUsuario"].Value = txtid.Text;
                    row.Cells["Documento"].Value = txtDocumento.Texts;
                    row.Cells["NombreCompleto"].Value = txtNombre.Texts;
                    row.Cells["Correo"].Value = txtCorreo.Texts;
                    row.Cells["Clave"].Value = txtClave.Texts;
                    row.Cells["IdRol"].Value = ((OpcionesCombo)comboRol.SelectedItem).Valor.ToString();
                    row.Cells["Rol"].Value = ((OpcionesCombo)comboRol.SelectedItem).Texto.ToString();
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

        private void FrmUsuario_Load(object sender, EventArgs e)
        {

            comboEstado.Items.Add(new OpcionesCombo() { Valor = 1, Texto = "Activo" });
            comboEstado.Items.Add(new OpcionesCombo() { Valor = 0, Texto = "No Activo" });

            comboEstado.DisplayMember = "Texto";
            comboEstado.ValueMember = "Valor";
            comboEstado.SelectedIndex = 0;

            List<Rol> listaRol = new CN_Rol().Listar();

            foreach (Rol item in listaRol)
            {
                comboRol.Items.Add(new OpcionesCombo() { Valor = item.IdRol, Texto = item.Descripcion });
            }
            comboRol.DisplayMember = "Texto";
            comboRol.ValueMember = "Valor";
            comboRol.SelectedIndex = 0;

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

            //Mostrar Usuarios en el dataGridView
            List<Usuario> listaUsuarios = new CN_Usuario().Listar();

            foreach (Usuario item in listaUsuarios)
            {
                dgvData.Rows.Add(new object[] {"",item.IdUsuario,item.Documento,item.NombreCompleto,item.Correo,item.Clave,
                    item.oRol.IdRol,
                    item.oRol.Descripcion,
                    item.Estado == true ? 1 : 0,
                    item.Estado == true ? "Activo": "No Activo"
                });
            }
            comboRol.DisplayMember = "Texto";
            comboRol.ValueMember = "Valor";
            comboRol.SelectedIndex = 0;
        }


        private void LimpiarCampos()
        {
            txtindice.Text = "-1";
            txtid.Text = "0";
            txtDocumento.Texts = "";
            txtNombre.Texts = "";
            txtCorreo.Texts = "";
            txtClave.Texts = "";

            comboRol.SelectedIndex = 0;
            comboEstado.SelectedIndex = 0;
            txtDocumento.Select();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtindice.Text = indice.ToString();
                    txtid.Text = dgvData.Rows[indice].Cells["IdUsuario"].Value.ToString();
                    txtDocumento.Texts = dgvData.Rows[indice].Cells["Documento"].Value.ToString();
                    txtNombre.Texts = dgvData.Rows[indice].Cells["NombreCompleto"].Value.ToString();
                    txtCorreo.Texts = dgvData.Rows[indice].Cells["Correo"].Value.ToString();
                    txtClave.Texts = dgvData.Rows[indice].Cells["Clave"].Value.ToString();



                    foreach (OpcionesCombo oc in comboRol.Items)
                    {

                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["IdRol"].Value))
                        {
                            int indiceCombo = comboRol.Items.IndexOf(oc);
                            comboRol.SelectedIndex = indiceCombo;
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el usuario?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Usuario objUsuario = new Usuario()
                    {
                        IdUsuario = Convert.ToInt32(txtid.Text),
                    };

                    bool respuesta = new CN_Usuario().Eliminar(objUsuario, out mensaje); ;
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
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
        private bool Validaciones()
        {
            bool validaciones = true;

            if (txtNombre.Texts == "")
            {
                validaciones = false;
            }

            if (txtDocumento.Texts == "")
            {
                validaciones = false;
            }

            if (txtCorreo.Texts == "" || !(txtCorreo.Texts.Contains("@")))
            {
                validaciones = false;
            }

            if (txtClave.Texts == "")
            {
                validaciones = false;
            }


            return validaciones;

        }
       



        private void BtnGuardar2_Click(object sender, EventArgs e)
        {
        
            if (Validaciones())
            {
                string mensaje = string.Empty;
                DialogResult confirmacion;

                if (Convert.ToInt32(txtid.Text) == 0)
                {
                    confirmacion = MessageBox.Show("¿Seguro desea agregar el usuario?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                else
                {
                   confirmacion = MessageBox.Show("¿Seguro desea editar el usuario?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }


                if(confirmacion == DialogResult.Yes){
                    Usuario objUsuario = new Usuario()
                    {
                        IdUsuario = Convert.ToInt32(txtid.Text),
                        Documento = txtDocumento.Texts,
                        NombreCompleto = txtNombre.Texts,
                        Correo = txtCorreo.Texts,
                        Clave = txtClave.Texts,
                        oRol = new Rol() { IdRol = Convert.ToInt32(((OpcionesCombo)comboRol.SelectedItem).Valor) },
                        Estado = Convert.ToInt32(((OpcionesCombo)comboEstado.SelectedItem).Valor) == 1 ? true : false
                    };

                    if (objUsuario.IdUsuario == 0)
                    {
                        int idUsuarioGenerado = new CN_Usuario().Registrar(objUsuario, out mensaje);
                        if (idUsuarioGenerado != 0)
                        {
                            dgvData.Rows.Add(new object[] {"",idUsuarioGenerado,txtDocumento.Texts,txtNombre.Texts,txtCorreo.Texts,txtClave.Texts,
                            ((OpcionesCombo)comboRol.SelectedItem).Valor.ToString(),
                            ((OpcionesCombo)comboRol.SelectedItem).Texto.ToString(),
                            ((OpcionesCombo)comboEstado.SelectedItem).Valor.ToString(),
                            ((OpcionesCombo)comboEstado.SelectedItem).Texto.ToString()});
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show(mensaje,"Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        bool resultado = new CN_Usuario().Editar(objUsuario, out mensaje);
                        if (resultado == true)
                        {
                            DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtindice.Text)];
                            row.Cells["IdUsuario"].Value = txtid.Text;
                            row.Cells["Documento"].Value = txtDocumento.Texts;
                            row.Cells["NombreCompleto"].Value = txtNombre.Texts;
                            row.Cells["Correo"].Value = txtCorreo.Texts;
                            row.Cells["Clave"].Value = txtClave.Texts;
                            row.Cells["IdRol"].Value = ((OpcionesCombo)comboRol.SelectedItem).Valor.ToString();
                            row.Cells["Rol"].Value = ((OpcionesCombo)comboRol.SelectedItem).Texto.ToString();
                            row.Cells["EstadoValor"].Value = ((OpcionesCombo)comboEstado.SelectedItem).Valor.ToString();
                            row.Cells["Estado"].Value = ((OpcionesCombo)comboEstado.SelectedItem).Texto.ToString();
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }



            }
            else{

                MessageBox.Show("Debe Completar todos los campos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


           
        }

        private void BtnEliminar2_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el usuario?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Usuario objUsuario = new Usuario()
                    {
                        IdUsuario = Convert.ToInt32(txtid.Text),
                    };

                    bool respuesta = new CN_Usuario().Eliminar(objUsuario, out mensaje); ;
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

        private void rjButton1_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
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

        private void dgvData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dgvData.Columns[e.ColumnIndex].Name == "Estado")
            {
                if (e.Value.ToString() == "No Activo")
                {
                    e.CellStyle.BackColor = Color.Red;
                    e.CellStyle.ForeColor = Color.Black;
                }

            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void txtDocumento__TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCorreo__TextChanged(object sender, EventArgs e)
        {

        }

        private void txtClave__TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombre__TextChanged(object sender, EventArgs e)
        {

        }

        private void comboEstado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboRol_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtBusqueda__TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
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

            // Permitir el carácter solo si es una tecla de control o un caracter y la longitud permitida no se ha alcanzado
            if (esControl || (!esDigito && longitudPermitida))
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
