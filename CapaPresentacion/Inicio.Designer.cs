namespace CapaPresentacion
{
    partial class Inicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.nombreUser = new System.Windows.Forms.Label();
            this.panelLateral = new System.Windows.Forms.Panel();
            this.menuLateral = new System.Windows.Forms.MenuStrip();
            this.menuInicio = new FontAwesome.Sharp.IconMenuItem();
            this.menuVentas = new FontAwesome.Sharp.IconMenuItem();
            this.menuRegistrarVenta = new FontAwesome.Sharp.IconMenuItem();
            this.menuVerDetalle = new FontAwesome.Sharp.IconMenuItem();
            this.menuCompras = new FontAwesome.Sharp.IconMenuItem();
            this.menuRegistrarCompra = new FontAwesome.Sharp.IconMenuItem();
            this.menuVerDetalleCompra = new FontAwesome.Sharp.IconMenuItem();
            this.menuClientes = new FontAwesome.Sharp.IconMenuItem();
            this.menuMantenimiento = new FontAwesome.Sharp.IconMenuItem();
            this.Productos = new FontAwesome.Sharp.IconMenuItem();
            this.Categorias = new FontAwesome.Sharp.IconMenuItem();
            this.Negocio = new FontAwesome.Sharp.IconMenuItem();
            this.menuReportes = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuRVentas = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuRCompras = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProveedores = new FontAwesome.Sharp.IconMenuItem();
            this.menuUsuarios = new FontAwesome.Sharp.IconMenuItem();
            this.BarraTop = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.menuTitulo = new System.Windows.Forms.MenuStrip();
            this.iconMenuItem9 = new FontAwesome.Sharp.IconMenuItem();
            this.Contenedor = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelLateral.SuspendLayout();
            this.menuLateral.SuspendLayout();
            this.BarraTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.menuTitulo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CapaPresentacion.Properties.Resources.Logo;
            this.pictureBox1.Location = new System.Drawing.Point(-43, -32);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(246, 160);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // nombreUser
            // 
            this.nombreUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nombreUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.nombreUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nombreUser.ForeColor = System.Drawing.Color.White;
            this.nombreUser.Image = global::CapaPresentacion.Properties.Resources.image_Photoroom;
            this.nombreUser.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.nombreUser.Location = new System.Drawing.Point(602, 12);
            this.nombreUser.Name = "nombreUser";
            this.nombreUser.Size = new System.Drawing.Size(169, 31);
            this.nombreUser.TabIndex = 3;
            this.nombreUser.Text = "Usuario";
            this.nombreUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelLateral
            // 
            this.panelLateral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.panelLateral.Controls.Add(this.menuLateral);
            this.panelLateral.Location = new System.Drawing.Point(0, 0);
            this.panelLateral.Name = "panelLateral";
            this.panelLateral.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panelLateral.Size = new System.Drawing.Size(163, 517);
            this.panelLateral.TabIndex = 5;
            // 
            // menuLateral
            // 
            this.menuLateral.AutoSize = false;
            this.menuLateral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuLateral.Dock = System.Windows.Forms.DockStyle.None;
            this.menuLateral.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuInicio,
            this.menuVentas,
            this.menuCompras,
            this.menuClientes,
            this.menuMantenimiento,
            this.menuReportes,
            this.menuProveedores,
            this.menuUsuarios});
            this.menuLateral.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.menuLateral.Location = new System.Drawing.Point(0, 62);
            this.menuLateral.Name = "menuLateral";
            this.menuLateral.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuLateral.Size = new System.Drawing.Size(163, 455);
            this.menuLateral.TabIndex = 0;
            this.menuLateral.Text = "menuLateral";
            this.menuLateral.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuLateral_ItemClicked);
            // 
            // menuInicio
            // 
            this.menuInicio.AutoSize = false;
            this.menuInicio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.menuInicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuInicio.IconChar = FontAwesome.Sharp.IconChar.HomeUser;
            this.menuInicio.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuInicio.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuInicio.IconSize = 30;
            this.menuInicio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuInicio.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuInicio.Name = "menuInicio";
            this.menuInicio.Size = new System.Drawing.Size(161, 45);
            this.menuInicio.Text = "Inicio";
            this.menuInicio.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.menuInicio.Click += new System.EventHandler(this.menuInicio_Click);
            // 
            // menuVentas
            // 
            this.menuVentas.AutoSize = false;
            this.menuVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuVentas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRegistrarVenta,
            this.menuVerDetalle});
            this.menuVentas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.menuVentas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuVentas.IconChar = FontAwesome.Sharp.IconChar.Tags;
            this.menuVentas.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuVentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuVentas.IconSize = 30;
            this.menuVentas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuVentas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuVentas.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.menuVentas.Name = "menuVentas";
            this.menuVentas.Size = new System.Drawing.Size(161, 45);
            this.menuVentas.Text = "Ventas";
            this.menuVentas.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // menuRegistrarVenta
            // 
            this.menuRegistrarVenta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuRegistrarVenta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuRegistrarVenta.IconChar = FontAwesome.Sharp.IconChar.None;
            this.menuRegistrarVenta.IconColor = System.Drawing.Color.Black;
            this.menuRegistrarVenta.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuRegistrarVenta.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuRegistrarVenta.Name = "menuRegistrarVenta";
            this.menuRegistrarVenta.Size = new System.Drawing.Size(158, 22);
            this.menuRegistrarVenta.Text = "Registrar Venta";
            this.menuRegistrarVenta.Click += new System.EventHandler(this.menuRegistrarVenta_Click);
            // 
            // menuVerDetalle
            // 
            this.menuVerDetalle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuVerDetalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuVerDetalle.IconChar = FontAwesome.Sharp.IconChar.None;
            this.menuVerDetalle.IconColor = System.Drawing.Color.Black;
            this.menuVerDetalle.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuVerDetalle.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuVerDetalle.Name = "menuVerDetalle";
            this.menuVerDetalle.Size = new System.Drawing.Size(158, 22);
            this.menuVerDetalle.Text = "Ver Detalle";
            this.menuVerDetalle.Click += new System.EventHandler(this.menuVerDetalle_Click);
            // 
            // menuCompras
            // 
            this.menuCompras.AutoSize = false;
            this.menuCompras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuCompras.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRegistrarCompra,
            this.menuVerDetalleCompra});
            this.menuCompras.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.menuCompras.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuCompras.IconChar = FontAwesome.Sharp.IconChar.Shopify;
            this.menuCompras.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuCompras.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuCompras.IconSize = 30;
            this.menuCompras.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuCompras.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuCompras.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.menuCompras.Name = "menuCompras";
            this.menuCompras.Size = new System.Drawing.Size(161, 45);
            this.menuCompras.Text = "Compras";
            this.menuCompras.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // menuRegistrarCompra
            // 
            this.menuRegistrarCompra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuRegistrarCompra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuRegistrarCompra.IconChar = FontAwesome.Sharp.IconChar.None;
            this.menuRegistrarCompra.IconColor = System.Drawing.Color.Black;
            this.menuRegistrarCompra.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuRegistrarCompra.Name = "menuRegistrarCompra";
            this.menuRegistrarCompra.Size = new System.Drawing.Size(181, 22);
            this.menuRegistrarCompra.Text = "Registrar Compra";
            this.menuRegistrarCompra.Click += new System.EventHandler(this.menuRegistrarCompra_Click);
            // 
            // menuVerDetalleCompra
            // 
            this.menuVerDetalleCompra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuVerDetalleCompra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuVerDetalleCompra.IconChar = FontAwesome.Sharp.IconChar.None;
            this.menuVerDetalleCompra.IconColor = System.Drawing.Color.Black;
            this.menuVerDetalleCompra.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuVerDetalleCompra.Name = "menuVerDetalleCompra";
            this.menuVerDetalleCompra.Size = new System.Drawing.Size(181, 22);
            this.menuVerDetalleCompra.Text = "Ver Detalle Compra";
            this.menuVerDetalleCompra.Click += new System.EventHandler(this.menuVerDetalleCompra_Click);
            // 
            // menuClientes
            // 
            this.menuClientes.AutoSize = false;
            this.menuClientes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuClientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.menuClientes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuClientes.IconChar = FontAwesome.Sharp.IconChar.Shopware;
            this.menuClientes.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuClientes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuClientes.IconSize = 30;
            this.menuClientes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuClientes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuClientes.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.menuClientes.Name = "menuClientes";
            this.menuClientes.Size = new System.Drawing.Size(161, 45);
            this.menuClientes.Text = "Clientes";
            this.menuClientes.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.menuClientes.Click += new System.EventHandler(this.menuClientes_Click);
            // 
            // menuMantenimiento
            // 
            this.menuMantenimiento.AutoSize = false;
            this.menuMantenimiento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuMantenimiento.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Productos,
            this.Categorias,
            this.Negocio});
            this.menuMantenimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.menuMantenimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuMantenimiento.IconChar = FontAwesome.Sharp.IconChar.Wrench;
            this.menuMantenimiento.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuMantenimiento.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuMantenimiento.IconSize = 30;
            this.menuMantenimiento.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuMantenimiento.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuMantenimiento.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.menuMantenimiento.Name = "menuMantenimiento";
            this.menuMantenimiento.Size = new System.Drawing.Size(161, 45);
            this.menuMantenimiento.Text = "Mantenimiento";
            this.menuMantenimiento.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // Productos
            // 
            this.Productos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.Productos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.Productos.IconChar = FontAwesome.Sharp.IconChar.None;
            this.Productos.IconColor = System.Drawing.Color.Black;
            this.Productos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Productos.Name = "Productos";
            this.Productos.Size = new System.Drawing.Size(133, 22);
            this.Productos.Text = "Productos";
            this.Productos.Click += new System.EventHandler(this.Productos_Click);
            // 
            // Categorias
            // 
            this.Categorias.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.Categorias.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.Categorias.IconChar = FontAwesome.Sharp.IconChar.None;
            this.Categorias.IconColor = System.Drawing.Color.Black;
            this.Categorias.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Categorias.Name = "Categorias";
            this.Categorias.Size = new System.Drawing.Size(133, 22);
            this.Categorias.Text = "Categorias";
            this.Categorias.Click += new System.EventHandler(this.Categorias_Click);
            // 
            // Negocio
            // 
            this.Negocio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.Negocio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.Negocio.IconChar = FontAwesome.Sharp.IconChar.None;
            this.Negocio.IconColor = System.Drawing.Color.Black;
            this.Negocio.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Negocio.Name = "Negocio";
            this.Negocio.Size = new System.Drawing.Size(133, 22);
            this.Negocio.Text = "Negocio";
            this.Negocio.Click += new System.EventHandler(this.Negocio_Click);
            // 
            // menuReportes
            // 
            this.menuReportes.AutoSize = false;
            this.menuReportes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuReportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuRVentas,
            this.subMenuRCompras});
            this.menuReportes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.menuReportes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuReportes.IconChar = FontAwesome.Sharp.IconChar.Receipt;
            this.menuReportes.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuReportes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuReportes.IconSize = 30;
            this.menuReportes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuReportes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuReportes.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.menuReportes.Name = "menuReportes";
            this.menuReportes.Size = new System.Drawing.Size(161, 45);
            this.menuReportes.Text = "Reportes";
            this.menuReportes.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // subMenuRVentas
            // 
            this.subMenuRVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.subMenuRVentas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.subMenuRVentas.Name = "subMenuRVentas";
            this.subMenuRVentas.Size = new System.Drawing.Size(171, 22);
            this.subMenuRVentas.Text = "Reporte Ventas";
            this.subMenuRVentas.Click += new System.EventHandler(this.subMenuRVentas_Click);
            // 
            // subMenuRCompras
            // 
            this.subMenuRCompras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.subMenuRCompras.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.subMenuRCompras.Name = "subMenuRCompras";
            this.subMenuRCompras.Size = new System.Drawing.Size(171, 22);
            this.subMenuRCompras.Text = "Reporte Compras";
            this.subMenuRCompras.Click += new System.EventHandler(this.subMenuRCompras_Click);
            // 
            // menuProveedores
            // 
            this.menuProveedores.AutoSize = false;
            this.menuProveedores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuProveedores.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.menuProveedores.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuProveedores.IconChar = FontAwesome.Sharp.IconChar.TruckFast;
            this.menuProveedores.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuProveedores.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuProveedores.IconSize = 30;
            this.menuProveedores.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuProveedores.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuProveedores.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.menuProveedores.Name = "menuProveedores";
            this.menuProveedores.Size = new System.Drawing.Size(161, 45);
            this.menuProveedores.Text = "Proveedores";
            this.menuProveedores.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.menuProveedores.Click += new System.EventHandler(this.menuProveedores_Click);
            // 
            // menuUsuarios
            // 
            this.menuUsuarios.AutoSize = false;
            this.menuUsuarios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuUsuarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.menuUsuarios.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuUsuarios.IconChar = FontAwesome.Sharp.IconChar.UserGroup;
            this.menuUsuarios.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuUsuarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuUsuarios.IconSize = 30;
            this.menuUsuarios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuUsuarios.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuUsuarios.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.menuUsuarios.Name = "menuUsuarios";
            this.menuUsuarios.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuUsuarios.Size = new System.Drawing.Size(161, 45);
            this.menuUsuarios.Text = "Usuarios";
            this.menuUsuarios.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.menuUsuarios.Click += new System.EventHandler(this.menuUsuarios_Click);
            // 
            // BarraTop
            // 
            this.BarraTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.BarraTop.Controls.Add(this.pictureBox2);
            this.BarraTop.Controls.Add(this.pictureBox1);
            this.BarraTop.Controls.Add(this.nombreUser);
            this.BarraTop.Controls.Add(this.menuTitulo);
            this.BarraTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.BarraTop.Location = new System.Drawing.Point(0, 0);
            this.BarraTop.Name = "BarraTop";
            this.BarraTop.Size = new System.Drawing.Size(823, 59);
            this.BarraTop.TabIndex = 7;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::CapaPresentacion.Properties.Resources.image_Photoroom1;
            this.pictureBox2.Location = new System.Drawing.Point(535, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(49, 46);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // menuTitulo
            // 
            this.menuTitulo.AutoSize = false;
            this.menuTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.menuTitulo.Dock = System.Windows.Forms.DockStyle.None;
            this.menuTitulo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iconMenuItem9});
            this.menuTitulo.Location = new System.Drawing.Point(414, 7);
            this.menuTitulo.Name = "menuTitulo";
            this.menuTitulo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuTitulo.Size = new System.Drawing.Size(408, 40);
            this.menuTitulo.TabIndex = 0;
            this.menuTitulo.Text = "Titulo";
            // 
            // iconMenuItem9
            // 
            this.iconMenuItem9.IconChar = FontAwesome.Sharp.IconChar.RightFromBracket;
            this.iconMenuItem9.IconColor = System.Drawing.Color.White;
            this.iconMenuItem9.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItem9.IconSize = 40;
            this.iconMenuItem9.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.iconMenuItem9.Name = "iconMenuItem9";
            this.iconMenuItem9.Size = new System.Drawing.Size(52, 36);
            this.iconMenuItem9.Click += new System.EventHandler(this.iconMenuItem9_Click);
            // 
            // Contenedor
            // 
            this.Contenedor.BackColor = System.Drawing.Color.White;
            this.Contenedor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Contenedor.Location = new System.Drawing.Point(163, 49);
            this.Contenedor.Name = "Contenedor";
            this.Contenedor.Size = new System.Drawing.Size(659, 510);
            this.Contenedor.TabIndex = 6;
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(823, 517);
            this.Controls.Add(this.BarraTop);
            this.Controls.Add(this.panelLateral);
            this.Controls.Add(this.Contenedor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Inicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Inicio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelLateral.ResumeLayout(false);
            this.menuLateral.ResumeLayout(false);
            this.menuLateral.PerformLayout();
            this.BarraTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.menuTitulo.ResumeLayout(false);
            this.menuTitulo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label nombreUser;
        private System.Windows.Forms.Panel panelLateral;
        private System.Windows.Forms.MenuStrip menuLateral;
        private FontAwesome.Sharp.IconMenuItem menuInicio;
        private FontAwesome.Sharp.IconMenuItem menuVentas;
        private FontAwesome.Sharp.IconMenuItem menuRegistrarVenta;
        private FontAwesome.Sharp.IconMenuItem menuVerDetalle;
        private FontAwesome.Sharp.IconMenuItem menuCompras;
        private FontAwesome.Sharp.IconMenuItem menuRegistrarCompra;
        private FontAwesome.Sharp.IconMenuItem menuVerDetalleCompra;
        private FontAwesome.Sharp.IconMenuItem menuClientes;
        private FontAwesome.Sharp.IconMenuItem menuMantenimiento;
        private FontAwesome.Sharp.IconMenuItem Productos;
        private FontAwesome.Sharp.IconMenuItem Categorias;
        private FontAwesome.Sharp.IconMenuItem Negocio;
        private FontAwesome.Sharp.IconMenuItem menuReportes;
        private System.Windows.Forms.ToolStripMenuItem subMenuRVentas;
        private System.Windows.Forms.ToolStripMenuItem subMenuRCompras;
        private FontAwesome.Sharp.IconMenuItem menuProveedores;
        private FontAwesome.Sharp.IconMenuItem menuUsuarios;
        private System.Windows.Forms.Panel BarraTop;
        private System.Windows.Forms.MenuStrip menuTitulo;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem9;
        private System.Windows.Forms.Panel Contenedor;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

