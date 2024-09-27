using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CapaPresentacion
{
    public partial class FrmGraficos : Form
    {
        public FrmGraficos()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FrmGraficos_Load(object sender, EventArgs e)
        {

            ArrayList Categorias = new ArrayList();
            ArrayList Productos = new ArrayList();
        
            Categorias = new CN_Grafico().Categorias();
            Productos = new CN_Grafico().Productos();

            ArrayList Vendidos = new ArrayList();
            ArrayList Cantidad = new ArrayList();

            Vendidos = new CN_Grafico().Vendidos();
            Cantidad = new CN_Grafico().Cantidad();

            int compra = new CN_Compra().obtenerCorrelativo()-1;
           

            for (int i=0; i< Categorias.Count; i++)
            {
                Series series1 = new Series(Categorias[i].ToString());
                series1.ChartType = SeriesChartType.Column;


                series1.Points.AddXY(Categorias[i], Productos[i]);
                chVentas.Series.Add(series1);
            }



            //chVentas.Series[0].Points.DataBindXY(Categorias, Productos);

           

            chCompras.Series[0].Points.DataBindXY(Vendidos, Cantidad);

            Grafico obj = new CN_Grafico().Datos();

            Ventas.Text = Ventas.Text + "  $" + obj.totalVentas.ToString();
          
            Compras.Text = Compras.Text + "  $" + obj.Compras.ToString();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void rjTextBox2__TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lblTituloV_Click(object sender, EventArgs e)
        {

        }
    }
}
