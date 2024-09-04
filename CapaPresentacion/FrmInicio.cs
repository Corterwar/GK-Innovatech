using CapaEntidad;
using CapaNegocio;
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
    public partial class FrmInicio : Form
    {
        public FrmInicio()
        {
            InitializeComponent();
        }


        private void FrmInicio_Load(object sender, EventArgs e)
        {
            int ventas = new CN_Venta().obtenerCorrelativo() - 1;
            Btn1.Text = "Cantidad de Ventas: " + ventas;

            int prod = new CN_Producto().Listar().Where(p => p.Estado == true).Count();
            Btn2.Text = "Cantidad de Productos: " + prod;

            int user = new CN_Usuario().Listar().Where(u=> u.Estado == true).Count();
            Btn3.Text = "Cantidad de Usuarios: " + user;

            int clientes = new CN_Cliente().Listar().Where(c => c.Estado == true).Count();
            Btn4.Text = "Cantidad de Clientes: " + clientes;


            int compras = new CN_Compra().obtenerCorrelativo() - 1;
            Btn5.Text = "Cantidad de Compras: " + compras;


            int proveedores = new CN_Proveedor().Listar().Where(c => c.Estado == true).Count();
            Btn6.Text = "Cantidad de Proveedores: " + proveedores;
        }

        private void btnIngresar2_Click(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

        }

        private void Btn1_Click(object sender, EventArgs e)
        {

        }
    }
}
