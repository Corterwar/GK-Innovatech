using CapaEntidad;
using CapaNegocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmDetalleVenta : Form
    {
        public FrmDetalleVenta()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            Venta oVenta = new CN_Venta().obtenerVenta(txtBusqueda.Texts);

            if (oVenta.IdVenta != 0)
            {
                Cliente oCliente = new CN_Cliente().Listar().Where(d => d.Documento == oVenta.DocumentoCliente).First();

                txtDocumento.Texts = oVenta.NumeroDocumento;
              
                txtFecha.Texts = oVenta.FechaRegistro;
                txtTipoDocumento.Texts = oVenta.TipoDocumento;


                txtUsuario.Texts = oVenta.oUsuario.NombreCompleto;
                txtDocV.Texts = oVenta.oUsuario.Documento;

                txtDocumentoC.Texts = oVenta.DocumentoCliente;
                txtNombre.Texts = oVenta.NombreCliente;
                txtCorreoC.Texts = oCliente.Correo;



                dgvData.Rows.Clear();

                foreach (DetalleVenta dc in oVenta.oDetalleVenta)
                {
                    dgvData.Rows.Add(new object[]
                    {
                        dc.oProducto.Nombre,
                        dc.oProducto.Descripcion,
                        dc.PrecioVenta,
                        dc.Cantidad,
                        dc.SubTotal
                    });
                }
                txtPago.Texts = oVenta.MontoPago.ToString("0.00");
                txtCambio.Texts = oVenta.MontoCambio.ToString("0.00");
                txtTotal.Texts = oVenta.MontoTotal.ToString("0.00");

            }
            else
            {
                MessageBox.Show("Debe colocar un numero de venta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFecha.Texts = "";
            txtTipoDocumento.Texts = "";
            txtUsuario.Texts = "";
            txtDocumentoC.Texts = "";
            txtNombre.Texts = "";
            txtDocV.Texts = "";
            txtCorreoC.Texts = "";

            dgvData.Rows.Clear();
            txtPago.Texts = "0.00";
            txtCambio.Texts = "0.00";
            txtTotal.Texts = "0.00";
        }

        private void FrmDetalleVenta_Load(object sender, EventArgs e)
        {
            txtBusqueda.Select();
        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            if (txtDocumento.Texts == "")
            {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            string Texto_Html = Properties.Resources.PlantillaVenta.ToString();
            Negocio oDatos = new CN_Negocio().obtenerDatos();

            Texto_Html = Texto_Html.Replace("@nombrenegocio", oDatos.Nombre.ToUpper());
            Texto_Html = Texto_Html.Replace("@docnegocio", oDatos.RUC);
            Texto_Html = Texto_Html.Replace("@direcnegocio", oDatos.Direccion);

            Texto_Html = Texto_Html.Replace("@tipodocumento", txtTipoDocumento.Texts.ToUpper());
            Texto_Html = Texto_Html.Replace("@numerodocumento", txtDocumento.Texts);

            Texto_Html = Texto_Html.Replace("@doccliente", txtDocumentoC.Texts);
            Texto_Html = Texto_Html.Replace("@nombrecliente", txtNombre.Texts);
            Texto_Html = Texto_Html.Replace("@fecharegistro", txtFecha.Texts);
            Texto_Html = Texto_Html.Replace("@usuarioregistro", txtUsuario.Texts);


            string filas = string.Empty;

            foreach (DataGridViewRow row in dgvData.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Producto"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Precio"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                filas += "</tr>";
            }
            Texto_Html = Texto_Html.Replace("@filas", filas);
            Texto_Html = Texto_Html.Replace("@montototal", txtTotal.Texts);
            Texto_Html = Texto_Html.Replace("@pagocon", txtPago.Texts);
            Texto_Html = Texto_Html.Replace("@cambio", txtCambio.Texts);

            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("Venta_{0}.pdf", txtDocumento.Texts);
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


                Venta oVenta = new CN_Venta().obtenerVenta(txtBusqueda.Texts);

                if (oVenta.IdVenta != 0)
                {

                    
                    Cliente oCliente = new CN_Cliente().Listar().Where(d => d.Documento == oVenta.DocumentoCliente).First();
                  

                    txtDocumento.Texts = oVenta.NumeroDocumento;

                    txtFecha.Texts = oVenta.FechaRegistro;
                    txtTipoDocumento.Texts = oVenta.TipoDocumento;


                    txtUsuario.Texts = oVenta.oUsuario.NombreCompleto;
                    txtDocV.Texts = oVenta.oUsuario.Documento;

                    txtDocumentoC.Texts = oVenta.DocumentoCliente;
                    txtNombre.Texts = oVenta.NombreCliente;
                    txtCorreoC.Texts = oCliente.Correo;

                    dgvData.Rows.Clear();

                    foreach (DetalleVenta dc in oVenta.oDetalleVenta)
                    {
                        dgvData.Rows.Add(new object[]
                        {
                        dc.oProducto.Nombre,
                        dc.oProducto.Descripcion,
                        dc.PrecioVenta,
                        dc.Cantidad,
                        dc.SubTotal
                        });
                    }
                    txtPago.Texts = oVenta.MontoPago.ToString("0.00");
                    txtCambio.Texts = oVenta.MontoCambio.ToString("0.00");
                    txtTotal.Texts = oVenta.MontoTotal.ToString("0.00");

                }
                else
                {
                    txtBusqueda.ForeColor = Color.MistyRose;
                    btnLimpiar_Click(sender, e);
                }

            }


        }

        private void txtFecha__TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDocumentoC__TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombre__TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTipoDocumento__TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsuario__TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtBusqueda__TextChanged(object sender, EventArgs e)
        {

        }
    }
}


