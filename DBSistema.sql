
--Creacion de la base de datos
CREATE DATABASE DBSistemaVentas
GO
--Conectarse a la base de datos
USE DBSistemaVentas
GO


--Creacion de las tablas
CREATE TABLE Rol(
IdRol int primary key identity,
Descripcion varchar(50),
FechaRegistro datetime default getdate()
)
GO

CREATE TABLE Permiso(
IdPermiso int primary key identity,
IdRol int references Rol(IdRol),
NombreMenu varchar(100),
FechaRegistro datetime default getDate()
)
GO


create TABLE Proveedor(
IdProveedor int primary key identity,
Documento varchar(50) unique,
RazonSocial varchar(80),
Direccion varchar(100),
Correo varchar(100) unique,
Telefono varchar(50),
Estado bit,
FechaRegistro datetime default getDate()
)
GO



CREATE TABLE Cliente(
IdCliente int primary key identity,
Documento varchar(50) unique,
NombreCompleto varchar(100),
Direccion varchar(100),
Correo varchar(100) unique,
Telefono varchar(50),
Estado bit,
FechaRegistro datetime default getDate()
)
GO

CREATE TABLE Usuario(
IdUsuario int primary key identity,
Documento varchar(50) unique,
NombreCompleto varchar(100),
Correo varchar(100) unique,
Clave varchar(80),
IdRol int references Rol(IdRol),
Estado bit,
FechaRegistro datetime default getDate()
)
GO



CREATE TABLE Categorias(
IdCategoria int primary key identity,
Descripcion varchar(80) unique,
Estado bit,
FechaRegistro datetime default getDate()
)
GO

CREATE TABLE Productos(
IdProducto int primary key identity,
Codigo int unique,
Nombre varchar(80),
Descripcion varchar(100),
Marca varchar(80), --Se agrega el campo marca
IdCategoria int references Categorias(IdCategoria),
Stock int not null default 0,
PrecioCompra decimal(10,2) default 0,
PrecioVenta decimal(10,2) default 0,
Estado bit,
FechaRegistro datetime default getDate()
)
GO

CREATE TABLE Compra(
IdCompra int primary key identity,
IdUsuario int references Usuario(IdUsuario),
IdProveedor int references Proveedor(IdProveedor),
TipoDocumento varchar(50),
NumeroDocumento varchar(50),
MontoTotal decimal(30,2),
FechaRegistro datetime default getDate(),
Estado bit default 0
)
GO



CREATE TABLE Detalle_Compra(
IdDetalleCompra int primary key identity,
IdCompra int references Compra(IdCompra),
IdProducto int references Productos(IdProducto),
PrecioCompra decimal(10,2) default 0,
PrecioVenta decimal(10,2) default 0,
--Descuento int default 0, Proxima entrega
Cantidad int,
Total decimal(30,2),
FechaRegistro datetime default getDate()
)
GO

CREATE TABLE Venta(
IdVenta int primary key identity,
IdUsuario int references Usuario(IdUsuario),
TipoDocumento varchar(50),
NumeroDocumento varchar(50),
DocumentoCliente varchar(50),
NombreCliente varchar(80),
MontoPago decimal(30,2),
MontoCambio decimal(30,2),
MontoTotal decimal(30,2),
--CodCupon int references Cupon(CodCupon), Para segunda entrega implementar sistema de cupones
FechaRegistro datetime default getDate()
)
GO

CREATE TABLE Detalle_Venta(
IdDetalleVenta int primary key identity,
IdVenta int references Venta(IdVenta),
IdProducto int references Productos(IdProducto),
PrecioVenta decimal(10,2) default 0,
Cantidad int,
SubTotal decimal(30,2),
FechaRegistro datetime default getDate()
)
GO



CREATE TABLE Negocio(
IdNegocio int primary key identity,
Nombre varchar(60),
RUC varchar(60),
Direccion varchar(60),
Logo varbinary(Max) NULL
)
go



--Creacion de los procedimientos



create procedure SP_REGISTRARUSUARIO(
@Documento varchar(50),
@NombreCompleto varchar(100),
@Correo varchar(100),
@Clave varchar(80),
@IdRol int,
@Estado bit,
@IdUsuarioResultado int output,
@Mensaje varchar(500) output
)
as
begin
	set @IdUsuarioResultado = 0
	set @Mensaje = ''
	if not exists(select * from Usuario where Documento = @Documento) 
	begin
		insert into Usuario(Documento,NombreCompleto,Correo,Clave,IdRol,Estado) values 
		(@Documento,@NombreCompleto,@Correo,@Clave,@IdRol,@Estado)

		set @IdUsuarioResultado = SCOPE_IDENTITY()
	
	end
	else
		set @Mensaje = 'No se puede repetir el documento para m�s de un usuario'
end

go

create procedure SP_EDITARUSUARIO(
@IdUsuario int,
@Documento varchar(50),
@NombreCompleto varchar(100),
@Correo varchar(100),
@Clave varchar(80),
@IdRol int,
@Estado bit,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Respuesta = 0
	set @Mensaje = ''
	if not exists(select * from Usuario where Documento = @Documento and IdUsuario != @IdUsuario) 
	begin
		update Usuario set
		Documento = @Documento,
		NombreCompleto =@NombreCompleto,
		Correo =@Correo,
		Clave =@Clave,
		IdRol =@IdRol,
		Estado =@Estado 
		where IdUsuario = @IdUsuario

		set @Respuesta = 1
	
	end
	else
		set @Mensaje = 'No se puede repetir el documento para m�s de un usuario'
end
go



create PROC SP_ELIMINARUSUARIO(
@IdUsuario int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Respuesta = 0
	set @Mensaje = ''
	declare @bandera bit = 1

	if exists (select * from Compra c 
	inner join Usuario u on u.IdUsuario = c.IdUsuario
	where u.IdUsuario = @IdUsuario)
	begin
		set @bandera = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar el usuario porque se encuentra relacionado a una compra\n'
	end


		if exists (select * from Venta v 
	inner join Usuario u on u.IdUsuario = v.IdUsuario
	where u.IdUsuario = @IdUsuario)
	begin
		set @bandera = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar el usuario porque se encuentra relacionado a una venta\n'
	end


	if(@bandera = 1)
	begin
		Update Usuario set
		Estado = 0
		where IdUsuario = @IdUsuario
		--delete from Usuario where IdUsuario = @IdUsuario
		set @Respuesta = 1
	end


end
go

/*Procedimiento para registrar una categoria*/

Create procedure SP_REGISTRARCATEGORIA(
@Descripcion varchar(80),
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	set @Mensaje = ''
	set @Resultado = 0
	if not exists (select * from Categorias where Descripcion = @Descripcion)
	begin
		insert into Categorias(Descripcion,Estado) values (@Descripcion,@Estado)
		set @Resultado = SCOPE_IDENTITY()
	end
	else
	set @Mensaje = 'No se puede repetir la descripcion de una categoria'
end

go


/*Procedimiento modificar Categorias*/

Create procedure SP_EDITARCATEGORIA(
@IdCategoria int,
@Descripcion varchar(80),
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Resultado=1
	set @Mensaje = ''


	if not exists(select * from Categorias WHERE Descripcion = @Descripcion and IdCategoria != @IdCategoria)
	begin
UPDATE Categorias
SET 
    Descripcion = @Descripcion, -- Nueva descripci�n para la categor�a
    Estado = @Estado            -- Nuevo estado para la categor�a
WHERE 
    IdCategoria = @IdCategoria  -- ID de la categor�a a actualizar
	end
	else
		begin
			set @Resultado = 0
			set @Mensaje = 'No se puede editar la descripcion de una categoria'
		end
end
go

create PROC SP_ELIMINARCATEGORIA(
@IdCategoria int,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Resultado=1
	set @Mensaje = ''

	if not exists(select * from Categorias c 
	inner join Productos p on p.IdCategoria = c.IdCategoria
	where c.IdCategoria = @IdCategoria
	)
	begin
		update Categorias set
		Estado = 0
		where IdCategoria = @IdCategoria
		--delete top(1) from Categorias where IdCategoria = @IdCategoria
	end
	else
		begin
			set @Resultado = 0
			set @Mensaje = 'No se puede eliminar la categoria'
		end

end
go


Create procedure SP_REGISTRARPRODUCTO(
@Codigo int,
@Nombre varchar(80),
@Descripcion varchar(100),
@Marca varchar(80),
@IdCategoria int,
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	Set @Resultado = 0
	if not exists(select * from Productos where Codigo = @Codigo)
	begin
		insert into Productos(Codigo,Nombre,Descripcion,Marca,IdCategoria,Estado) values (@Codigo,@Nombre,@Descripcion,@Marca,@IdCategoria,@Estado)
		set @Resultado = SCOPE_IDENTITY()
	end
	else
		set @Mensaje = 'Ya existe un producto con el mismo codigo'
end
GO

create procedure SP_EDITARPRODUCTO(
@IdProducto int,
@Codigo int,
@Nombre varchar(80),
@Descripcion varchar(100),
@Marca varchar(80),
@IdCategoria int,
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin
	Set @Resultado = 1
	if not exists(select * from Productos where Codigo = @Codigo and IdProducto != @IdProducto )
	
		update Productos set
		Codigo = @Codigo,
		Nombre = @Nombre,
		Descripcion = @Descripcion,
		Marca = @Marca,
		IdCategoria = @IdCategoria,
		Estado = @Estado
		where IdProducto = @IdProducto
	
	else
	begin
		set @Mensaje = 'Ya existe un producto con el mismo codigo'
		set @Resultado = 0
	end
	
end
GO

create PROC SP_ELIMINARPRODUCTO(
@IdProducto int,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin
	set @Resultado = 1
	set @Mensaje = ''
	
	declare @bandera bit = 1

	if exists(select * from Detalle_Compra dc 
	inner join Productos p on p.IdProducto = dc.IdProducto
	where p.IdProducto = @IdProducto
	)
	begin
		set @bandera = 0
		set @Resultado =0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque esta relacionado a una compra\n'
	end
		if exists(select * from Detalle_Venta dv 
	inner join Productos p on p.IdProducto = dv.IdProducto
	where p.IdProducto = @IdProducto
	)
	begin
		set @bandera = 0
		set @Resultado =0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque esta relacionado a una venta\n'
	end
	if(@bandera = 1)
		begin
			update Productos set
			Estado = 0
			where IdProducto = @IdProducto
			set @Resultado = 1
		
		end
	
end
go

create procedure SP_REGISTRARCLIENTE(
@Documento varchar(50),
@NombreCompleto varchar(100),
@Direccion varchar(100),
@Correo varchar(100),
@Telefono varchar(50),
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	Set @Resultado = 0
	set @Mensaje = ''



	if not exists(Select * from Cliente where Documento = @Documento)
	begin
		insert into Cliente (Documento,NombreCompleto,Direccion,Correo,Telefono,Estado)
		values (@Documento,@NombreCompleto,@Direccion,@Correo,@Telefono,@Estado)

		set @Resultado = SCOPE_IDENTITY()
	end
	else
		set @Mensaje = 'El numero de documento ya existe'
end
go

create procedure SP_EDITARCLIENTE(
@IdCliente int,
@Documento varchar(50),
@NombreCompleto varchar(100),
@Direccion varchar(100),
@Correo varchar(100),
@Telefono varchar(50),
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin
	Set @Resultado = 1
	set @Mensaje = ''

	

	if not exists(Select * from Cliente where Documento = @Documento and IdCliente != @IdCliente)
	begin
		update Cliente set
		Documento = @Documento,
		NombreCompleto = @NombreCompleto,
		Direccion = @Direccion,
		Correo = @Correo,
		Telefono = @Telefono,
		Estado = @Estado
		where IdCliente = @IdCliente

	end
	else
		set @Resultado = 0
		set @Mensaje = 'No se puede editar el cliente'
end
go

create PROC SP_ELIMINARCLIENTE(
@IdCliente int,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Resultado=1
	set @Mensaje = ''

		if exists(select * from Cliente where IdCliente = @IdCliente)
			begin
				update Cliente set
				Estado = 0
				where IdCliente = @IdCliente
				--delete top(1) from Cliente where IdCliente = @IdCliente
			end
		else
			begin
				set @Resultado = 0
				set @Mensaje = 'No se puede eliminar el cliente'
			end
end
go

create procedure SP_REGISTRARPROVEEDOR(
@Documento varchar(50),
@RazonSocial varchar(80),
@Direccion varchar(100),
@Correo varchar(100),
@Telefono varchar(50),
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	Set @Resultado = 0
	set @Mensaje = ''

	

	if not exists(Select * from Proveedor where Documento = @Documento)
	begin
		insert into Proveedor(Documento,RazonSocial,Direccion,Correo,Telefono,Estado)
		values (@Documento,@RazonSocial,@Direccion,@Correo,@Telefono,@Estado)

		set @Resultado = SCOPE_IDENTITY()
	end
	else
		set @Mensaje = 'El numero de documento ya existe'
end
go

create procedure SP_EDITARPROVEEDOR(
@IdProveedor int,
@Documento varchar(50),
@RazonSocial varchar(80),
@Direccion varchar(100),
@Correo varchar(100),
@Telefono varchar(50),
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin
	Set @Resultado = 1
	set @Mensaje = ''



	if not exists(Select * from Proveedor where Documento = @Documento and IdProveedor != @IdProveedor)
	begin
		update Proveedor set
		Documento = @Documento,
		RazonSocial = @RazonSocial,
		Direccion = @Direccion,
		Correo = @Correo,
		Telefono = @Telefono,
		Estado = @Estado
		where IdProveedor = @IdProveedor

	end
	else
		set @Resultado = 0
		set @Mensaje = 'No se puede editar el proveedor'
end
go

Create procedure SP_ELIMINARPROVEEDOR(
@IdProveedor int,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Resultado=1
	set @Mensaje = ''

		if not exists(select * from Proveedor p
		inner join Compra c on p.IdProveedor = c.IdProveedor
		where p.IdProveedor = @IdProveedor)
			begin
				update Proveedor set
				Estado = 0
				where IdProveedor = @IdProveedor
				--delete top(1) from Proveedor where IdProveedor = @IdProveedor
			end
		else
			begin
				set @Resultado = 0
				set @Mensaje = 'No se puede eliminar el proveedor, se encuentra relacionado con una compra'
			end
end
go


/*PROCEDIMIENTO PARA REGISTRAR UNA COMPRA*/

Create TYPE [dbo].[EDetalle_Compra] as table(
	[IdProducto] int NULL,
	[PrecioCompra]	decimal(18,2) NULL,
	[PrecioVenta]	decimal(18,2) NULL,
	[Cantidad] int NULL,
	[Total] decimal(18,2) NULL
)
go




create procedure SP_REGISTRARCOMPRA(
@IdUsuario int,
@IdProveedor int,
@TipoDocumento varchar(50),
@NumeroDocumento varchar(50),
@MontoTotal decimal(10,2),
@DetalleCompra [EDetalle_Compra] READONLY,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin

	begin try
		declare @idcompra int = 0
		set @Resultado = 1
		set @Mensaje = ''

		begin transaction registro

		insert into Compra(IdUsuario,IdProveedor,TipoDocumento,NumeroDocumento,MontoTotal,Estado)
		values(@IdUsuario,@IdProveedor,@TipoDocumento,@NumeroDocumento,@MontoTotal,0)
		set @idcompra = SCOPE_IDENTITY()


		insert into Detalle_Compra(IdCompra,IdProducto,PrecioCompra,PrecioVenta,Cantidad,Total)
		select @idcompra,IdProducto,PrecioCompra,PrecioVenta,Cantidad,Total from @DetalleCompra

		update p set 
		p.PrecioCompra = dc.PrecioCompra,
		P.PrecioVenta = dc.PrecioVenta
		from Productos p
		inner join @DetalleCompra dc on dc.IdProducto = p.IdProducto


		commit transaction registro

	end try
	begin catch
		set @Resultado = 0
		set @Mensaje = ERROR_MESSAGE()
		rollback transaction registro
		
	end catch
end

go



--Procedimiento para validar una compra y que actualice el stock
CREATE PROCEDURE SP_VALIDARCOMPRA(
    @IdCompra INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    -- Verificamos si la compra existe y su estado es '0' (no validada)
    IF EXISTS(SELECT * FROM Compra WHERE IdCompra = @IdCompra AND Estado = 0)
    BEGIN
        -- Verificamos si existen detalles para la compra especificada
        IF EXISTS(SELECT * FROM Detalle_Compra WHERE IdCompra = @IdCompra)
        BEGIN
            -- Actualizamos los stocks de los productos
            UPDATE p
            SET p.Stock = p.Stock + dc.Cantidad
            FROM Productos p
            INNER JOIN Detalle_Compra dc ON dc.IdProducto = p.IdProducto
            WHERE dc.IdCompra = @IdCompra;

            -- Actualizamos el estado de la compra a '1' (validada)
            UPDATE Compra
            SET Estado = 1
            WHERE IdCompra = @IdCompra;

            -- Devolvemos que la operaci�n fue exitosa
            SET @Resultado = 1;
            SET @Mensaje = 'Compra validada y stock actualizado correctamente.';
        END
        ELSE
        BEGIN
            -- Si no se encuentran detalles de la compra
            SET @Resultado = 0;
            SET @Mensaje = 'No se encontr� ning�n detalle para la compra especificada.';
        END
    END
    ELSE
    BEGIN
        -- Si la compra ya fue validada o no existe
        SET @Resultado = 0;
        SET @Mensaje = 'La compra no existe o ya ha sido validada.';
    END
END
GO


/*PROCEDIMIENTO PARA REGISTRAR UNA VENTA*/

CREATE TYPE [dbo].[EDetalle_Venta] as table(
	[IdProducto] int NULL,
	[PrecioVenta]	decimal(18,2) NULL,
	[Cantidad] int NULL,
	[SubTotal] decimal(18,2) NULL
)
go


Create procedure SP_REGISTRARVENTA(
@IdUsuario int,
@TipoDocumento varchar(50),
@NumeroDocumento varchar(50),
@DocumentoCliente varchar(50),
@NombreCliente varchar(80),
@MontoPago decimal(10,2),
@MontoCambio decimal(10,2),
@MontoTotal decimal(10,2),
@DetalleVenta [EDetalle_Venta] READONLY,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin

	begin try
		declare @idventa int = 0
		set @Resultado = 1
		set @Mensaje = ''

		begin transaction registro

		insert into Venta(IdUsuario,TipoDocumento,NumeroDocumento,DocumentoCliente,NombreCliente,MontoPago,MontoCambio,MontoTotal)
		values(@IdUsuario,@TipoDocumento,@NumeroDocumento,@DocumentoCliente,@NombreCliente,@MontoPago,@MontoCambio,@MontoTotal)
		set @idventa = SCOPE_IDENTITY()

		
	
		insert into Detalle_Venta(idventa,IdProducto,PrecioVenta,Cantidad,SubTotal)
		select @idventa,IdProducto,PrecioVenta,Cantidad,SubTotal from @DetalleVenta


		commit transaction registro

	end try
	begin catch
		set @Resultado = 0
		set @Mensaje = ERROR_MESSAGE()
		rollback transaction registro
		
	end catch
end

go



create procedure SP_REPORTECOMPRAS(
    @fechainicio date, -- Cambiar a tipo de dato date
    @fechafin date,    -- Cambiar a tipo de dato date
    @idproveedor int
)
AS
BEGIN
    -- El formato de la fecha solo es relevante para la salida, no para las comparaciones
    SELECT 
        CONVERT(char(10), c.FechaRegistro, 103) AS [FechaRegistro], 
        c.TipoDocumento,
        c.NumeroDocumento,
        c.MontoTotal,
        u.NombreCompleto AS [UsuarioRegistro],
        pr.Documento AS [DocumentoProveedor], 
        pr.RazonSocial,
        p.Codigo AS [CodigoProducto],
        p.Nombre AS [NombreProducto],
        ca.Descripcion AS [Categoria],
        dc.PrecioCompra,
        dc.PrecioVenta,
        dc.Cantidad,
        dc.Total AS [SubTotal]
    FROM Compra c
    INNER JOIN Usuario u ON u.IdUsuario = c.IdUsuario
    INNER JOIN Proveedor pr ON pr.IdProveedor = c.IdProveedor
    INNER JOIN Detalle_Compra dc ON dc.IdCompra = c.IdCompra
    INNER JOIN Productos p ON p.IdProducto = dc.IdProducto
    INNER JOIN Categorias ca ON ca.IdCategoria = p.IdCategoria
    WHERE c.FechaRegistro BETWEEN @fechainicio AND @fechafin
      AND pr.IdProveedor = IIF(@idproveedor = 0, pr.IdProveedor, @idproveedor)
END
go


create procedure SP_REPORTEVENTAS(
    @fechainicio date, -- Cambiar a tipo de dato date
    @fechafin date     -- Cambiar a tipo de dato date
)
AS
BEGIN
    -- El formato de la fecha solo es relevante para la salida, no para las comparaciones
    SELECT 
        CONVERT(char(10), v.FechaRegistro, 103) AS [FechaRegistro], 
        v.TipoDocumento,
        v.NumeroDocumento,
        v.MontoTotal,
        u.NombreCompleto AS [UsuarioRegistro],
        v.DocumentoCliente,
        v.NombreCliente,
        p.Codigo AS [CodigoProducto],
        p.Nombre AS [NombreProducto],
        ca.Descripcion AS [Categoria],
        dv.PrecioVenta,
        dv.Cantidad,
        dv.SubTotal
    FROM Venta v
    INNER JOIN Usuario u ON u.IdUsuario = v.IdUsuario
    INNER JOIN Detalle_Venta dv ON dv.IdVenta = v.IdVenta
    INNER JOIN Productos p ON p.IdProducto = dv.IdProducto
    INNER JOIN Categorias ca ON ca.IdCategoria = p.IdCategoria
    WHERE v.FechaRegistro BETWEEN @fechainicio AND @fechafin
END
go


Create procedure SP_DATOSGRAFICOS
@totVentas int out,
@totCompras int out,
@totProveedores int out,
@totProd int out,
@totCat int out,
@totUser int out,
@totClientes int out
as
set @totVentas = (select sum(MontoTotal) as TotVentas from Venta)
set @totCompras = (select sum(c.MontoTotal) as TotCompras from Compra as c)
set @totProveedores = (select sum(IdProveedor) as TotProveedores from Proveedor)
set @totProd = (select sum(IdProducto) as TotProd from Productos)
set @totCat = (select sum(IdCategoria) as TotCat from Categorias)
set @totUser = (select sum(IdUsuario) as TotUser from Usuario)
set @totClientes = (select sum(IdCliente) as TotClientes from Cliente)
go

create procedure SP_PRODPREFERIDOS
as
select top 5 c.Descripcion+' ' +p.Descripcion as Producto, count(dv.IdProducto) as cantSalidas
from Detalle_Venta as dv
inner join Productos as p on p.IdProducto = dv.IdProducto
inner join Categorias as c on c.IdCategoria = p.IdCategoria
group by dv.IdProducto,c.Descripcion,p.Descripcion
order by count(2) desc
go

create PROCEDURE SP_GRAFICOSVFECHAS
    @fechaDesde DATE,
    @fechaHasta DATE,
    @totVentas INT OUT,
    @totCompras INT OUT
AS
BEGIN
    -- Total de ventas en el rango de fechas
    SET @totVentas = (
        SELECT SUM(MontoTotal) as TotVentas
        FROM Venta v 
        WHERE v.FechaRegistro BETWEEN @fechaDesde AND @fechaHasta 
    );

    -- Total de compras en el mismo rango de fechas
    SET @totCompras = (
        SELECT SUM(MontoTotal) as TotCompras
        FROM Compra c 
        WHERE c.FechaRegistro BETWEEN @fechaDesde AND @fechaHasta
    );
END
GO




create procedure SP_PRODCATEGORIAS
as
select c.Descripcion, count(c.IdCategoria) as Cant 
from  Productos as p
inner join Categorias c on c.IdCategoria=p.IdCategoria
group by c.IdCategoria, c.Descripcion
order by count(2)
go



--Inserts a la base de datos
Insert into Rol (Descripcion)
values('Gerente')
go

Insert into Rol (Descripcion)
values('Vendedor')
go

Insert into Rol (Descripcion)
values('Manager')
go

--Inserta los permisos
insert into PERMISO(IdRol,NombreMenu) values
  (1,'menuUsuarios'),
  (1,'menuMantenimiento'),
  (1,'menuVentas'),
  (1,'menuCompras'),
  (1,'menuClientes'),
  (1,'menuProveedores'),
  (1,'menuReportes'),
  (1,'menuInicio'),
  (1,'menuGraficos')
  go

  insert into PERMISO(IdRol,NombreMenu) values
  (2,'menuMantenimiento'),
  (2,'menuVentas'),
  (2,'menuCompras'),
  (2,'menuClientes'),
  (2,'menuProveedores'),
  (2,'menuInicio')
  go

insert into PERMISO(IdRol,NombreMenu) values
  (3,'menuMantenimiento'),
  (3,'menuUsuarios'),
  (3,'menuClientes'),
  (3,'menuProveedores'),
  (3,'menuInicio')
  go


--Inserta un usuario (Administrador) con estas credenciales
Insert into Usuario(Documento,NombreCompleto,Correo,Clave,IdRol,Estado)
VALUES('1234','Tester','test@gmail.com','1234',1,1)
go

insert into Negocio(Nombre,RUC,Direccion) 
values('GK Innovatech',10000,'Av.Armenia 3400')
go
