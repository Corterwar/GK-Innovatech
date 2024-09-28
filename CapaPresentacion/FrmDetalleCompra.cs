using CapaEntidad;
using CapaNegocio;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace CapaPresentacion
{
    public partial class FrmDetalleCompra : Form
    {
        public FrmDetalleCompra(Usuario obj)
        {
            InitializeComponent();
            if (obj.oRol.IdRol != 1) {

                btnValidar.Visible = false;
            }
        }



        private void btnBuscar_Click(object sender, EventArgs e)
        {

            Compra oCompra = new CN_Compra().obtenerCompra(txtBusqueda.Texts);

            if (oCompra.IdCompra != 0)
            {
                txtDocumento.Texts = oCompra.NumeroDocumento;
                txtFecha.Texts = oCompra.FechaRegistro;
                txtTipoDocumento.Texts = oCompra.TipoDocumento;

                txtUsuario.Texts = oCompra.oUsuario.NombreCompleto;
                txtDocV.Texts = oCompra.oUsuario.Documento;

                txtDocumentoProv.Texts = oCompra.oProveedor.Documento;
                txtRazon.Texts = oCompra.oProveedor.RazonSocial;
                txtCorreoP.Texts = oCompra.oProveedor.Correo;

                if (oCompra.Estado == false){
                    txtEstado.Texts = "En Camino";
                }
                else
                {
                    txtEstado.Texts = "Recibida";
                }


                dgvData.Rows.Clear();

                foreach (DetalleCompra dc in oCompra.oDetalleCompra)
                {
                    dgvData.Rows.Add(new object[]
                    {
                        dc.oProducto.Nombre,
                        dc.oProducto.Descripcion,
                        dc.PrecioCompra,
                        dc.Cantidad,
                        dc.MontoTotal
                    });
                }

                txtTotal.Texts = oCompra.MontoTotal.ToString("0.00");

            }
            else{
                MessageBox.Show("Debe colocar un numero de compra","Alerta",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFecha.Texts = "";
            txtTipoDocumento.Texts = "";
            txtUsuario.Texts = "";
            txtDocV.Texts = "";
            txtDocumentoProv.Texts = "";
            txtRazon.Texts = "";
            txtCorreoP.Texts = "";
            txtEstado.Texts = "";
            txtTotal.Texts = "0.00";

            dgvData.Rows.Clear();
        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            if (txtDocumento.Texts == "")
            {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            string Texto_Html = Properties.Resources.PlantillaCompra.ToString();
            Negocio oDatos = new CN_Negocio().obtenerDatos();

            Texto_Html = Texto_Html.Replace("@nombrenegocio", oDatos.Nombre.ToUpper());
            Texto_Html = Texto_Html.Replace("@docnegocio", oDatos.RUC);
            Texto_Html = Texto_Html.Replace("@direcnegocio", oDatos.Direccion);

            Texto_Html = Texto_Html.Replace("@tipodocumento", txtTipoDocumento.Texts.ToUpper());
            Texto_Html = Texto_Html.Replace("@numerodocumento", txtDocumento.Texts);

            Texto_Html = Texto_Html.Replace("@docproveedor", txtDocumentoProv.Texts);
            Texto_Html = Texto_Html.Replace("@nombreproveedor", txtRazon.Texts);
            Texto_Html = Texto_Html.Replace("@fecharegistro", txtFecha.Texts);
            Texto_Html = Texto_Html.Replace("@usuarioregistro", txtUsuario.Texts);


            string filas = string.Empty;

            foreach (DataGridViewRow row in dgvData.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Producto"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["PrecioCompra"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                filas += "</tr>";
            }
            Texto_Html = Texto_Html.Replace("@filas", filas);
            Texto_Html = Texto_Html.Replace("@montototal", txtTotal.Texts);

            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("Compra_{0}.pdf", txtDocumento.Texts);
            savefile.Filter = "Pdf Files | *.pdf";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    bool obtener = true;
                    byte[] byteImage = new CN_Negocio().obtenerLogo(out obtener);

                    if (obtener)
                    {
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(byteImage);
                        img.ScaleToFit(60, 60);
                        img.Alignment = iTextSharp.text.Image.UNDERLYING;
                        img.SetAbsolutePosition(pdfDoc.Left, pdfDoc.GetTop(51));
                        pdfDoc.Add(img);
                    }

                    using (StringReader sr = new StringReader(Texto_Html))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                    MessageBox.Show("Pdf Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void FrmDetalleCompra_Load(object sender, EventArgs e)
        {
            txtBusqueda.Select();
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar si el carácter es un dígito
            bool esDigito = Char.IsDigit(e.KeyChar);

            // Verificar la longitud actual del texto y permitir solo hasta 8 dígitos
            bool longitudPermitida = txtBusqueda.Texts.Length < 8;

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



        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                Compra oCompra = new CN_Compra().obtenerCompra(txtBusqueda.Texts);

                if (oCompra.IdCompra != 0)
                {
                    txtBusqueda.ForeColor = Color.Honeydew;
                    txtDocumento.Texts = oCompra.NumeroDocumento;
                    txtFecha.Texts = oCompra.FechaRegistro;
                    txtTipoDocumento.Texts = oCompra.TipoDocumento;

                    txtUsuario.Texts = oCompra.oUsuario.NombreCompleto;
                    txtDocV.Texts = oCompra.oUsuario.Documento;

                    txtDocumentoProv.Texts = oCompra.oProveedor.Documento;
                    txtRazon.Texts = oCompra.oProveedor.RazonSocial;
                    txtCorreoP.Texts = oCompra.oProveedor.Correo;
                    if (oCompra.Estado == false)
                    {
                        txtEstado.Texts = "En Camino";
                    }
                    else
                    {
                        txtEstado.Texts = "Recibida";
                    }

                    dgvData.Rows.Clear();

                    foreach (DetalleCompra dc in oCompra.oDetalleCompra)
                    {
                        dgvData.Rows.Add(new object[]
                        {
                            dc.oProducto.Nombre,
                            dc.oProducto.Descripcion,
                            dc.PrecioCompra,
                            dc.Cantidad,
                            dc.MontoTotal
                        });
                    }

                    txtTotal.Texts = oCompra.MontoTotal.ToString("0.00");

                }
                else
                {
                    txtBusqueda.ForeColor = Color.MistyRose;
                    btnLimpiar_Click(sender, e);
                }
            }

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void txtDocumentoProv__TextChanged(object sender, EventArgs e)
        {

        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            String Mensaje = string.Empty;
            if(txtBusqueda.Texts != "")
            {
                bool Resultado = new CN_Compra().ValidarCompra(txtBusqueda.Texts, out Mensaje);
                MessageBox.Show(Mensaje,"Alerta",MessageBoxButtons.OK,MessageBoxIcon.Information);
                txtEstado.Texts = "Recibida";
            }
            else
            {
                MessageBox.Show("No hay ninguna compra para validar","Alerta",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtFecha__TextChanged(object sender, EventArgs e)
        {

        }
    }
}
