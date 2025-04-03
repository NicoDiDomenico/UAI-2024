use DBSISTEMA_VENTA

select * from usuario

/*
insert into rol (Descripcion)
values('ADMINISTRADOR')

insert into USUARIO (Documento, NombreCompleto, Correo, Clave, IdRol, Estado)
values 
('101010', 'ADMIN', '@GMAIL.COM', '123', 1, 1)

insert into USUARIO (Documento, NombreCompleto, Correo, Clave, IdRol, Estado)
values 
('20', 'EMPLEADO', '@GMAIL.COM', '456', 2, 1);
*/
select * from rol

select IdPermiso, idRol, NombreMenu from PERMISO
/*
insert into PERMISO (IdRol, NombreMenu) values
(1, 'menuUsuarios'),
(1, 'menuMantenedor'),
(1, 'menuVentas'),
(1, 'menuCompras'),
(1, 'menuClientes'),
(1, 'menuProveedores'),
(1, 'menuReportes'),
(1, 'menuAcercaDe')

insert into rol (Descripcion)
values('EMPLEADO')

insert into PERMISO (IdRol, NombreMenu) values
/*(2, 'menuUsuarios'),
(2, 'menuMantenedor'),*/ /*No quiero que tenga estos permisos */
(2, 'menuVentas'),
(2, 'menuCompras'),
(2, 'menuClientes'),
(2, 'menuProveedores'),
/*(2, 'menuReportes'),*/
(2, 'menuAcercaDe')
*/

select * from rol

select p.IdRol, p.NombreMenu 
from PERMISO p
inner join ROL r on r.IdRol = p.IdRol
inner join USUARIO u on u.IdRol = r.IdRol
where u.IdUsuario = 1

select IdUsuario, Documento, NombreCompleto, Correo, Clave, Estado from usuario

select u.IdUsuario, u.Documento, u.NombreCompleto, u.Correo, u.Clave, u.Estado, r.IdRol, r.Descripcion
from usuario u
inner join ROL r
on r.IdRol = u.IdRol

update usuario set estado = 0 where idusuario = 2

/* Alta Usuario */
create PROC SP_REGISTRARUSUARIO(
    @Documento varchar(50),
    @NombreCompleto varchar(100),
    @Correo varchar(100),
    @Clave varchar(100),
    @IdRol int,
    @Estado bit,
    @IdUsuarioResultado int output,
    @Mensaje varchar(500) output
)
as
begin
    set @IdUsuarioResultado = 0;
    set @Mensaje = '';

    if not exists(select * from USUARIO where Documento = @Documento)
    begin
        insert into usuario(Documento, NombreCompleto, Correo, Clave, IdRol, Estado) 
        values (@Documento, @NombreCompleto, @Correo, @Clave, @IdRol, @Estado)

        set @IdUsuarioResultado = SCOPE_IDENTITY();
    end
    else
        set @Mensaje = 'No se puede repetir el documento para más de un usuario'
end

go

/* Probando el alta */
declare @idusuariogenerado int
declare @mensaje varchar(500)

exec SP_REGISTRARUSUARIO '123','pruebas','test@gmail.com','456',2,1,@idusuariogenerado output,@mensaje output

select @idusuariogenerado
select @mensaje

/* Modificacion Usuario */
create PROC SP_EDITARUSUARIO(
	@IdUsuario int,
    @Documento varchar(50),
    @NombreCompleto varchar(100),
    @Correo varchar(100),
    @Clave varchar(100),
    @IdRol int,
    @Estado bit,
    @Respuesta bit output,
    @Mensaje varchar(500) output
)
as
begin
    set @Respuesta = 0;
    set @Mensaje = '';

    if not exists(select * from USUARIO where Documento = @Documento and IdUsuario != @IdUsuario )
    begin
        update usuario set 
		Documento = @Documento,
		NombreCompleto = @NombreCompleto,
		Correo = @Correo,
		Clave = @Clave,
		IdRol = @IdRol,
		Estado = @Estado
		where IdUsuario = @IdUsuario

        set @Respuesta = 1;
    end
    else
        set @Mensaje = 'No se puede repetir el documento para más de un usuario'
end

/* Probando la Modificacion */
declare @respuesta bit
declare @mensaje varchar(500)

exec SP_EDITARUSUARIO 2, '123','pruebas 2','test@gmail.com','456',2,1,@respuesta output,@mensaje output

select @respuesta
select @mensaje

select * from USUARIO

/* Eliminando el Usuario */
create PROC SP_ELIMINARUSUARIO(
    @IdUsuario int,
    @Respuesta bit output,
    @Mensaje varchar(500) output
)
as
begin
    set @Respuesta = 0
    set @Mensaje = ''
	declare @pasoreglas bit = 1

    IF EXISTS (SELECT * FROM COMPRA C
    INNER JOIN USUARIO U ON U.IdUsuario = C.IdUsuario
    WHERE U.IDUSUARIO = @IdUsuario
    )
    BEGIN
		set @pasoreglas = 0
        set @Respuesta = 0
        set @Mensaje = @Mensaje + 'No se puede eliminar porque el usuario se encuentra relacionado a una COMPRA\n'
    END

    IF EXISTS (SELECT * FROM VENTA V
    INNER JOIN USUARIO U ON U.IdUsuario = V.IdUsuario
    WHERE U.IDUSUARIO = @IdUsuario
    )
    BEGIN
		set @pasoreglas = 0
        set @Respuesta = 0
        set @Mensaje = @Mensaje + 'No se puede eliminar porque el usuario se encuentra relacionado a una VENTA\n'
    END

	if (@pasoreglas = 1)
	BEGIN
		delete from USUARIO where IdUsuario = @IdUsuario
		set @Respuesta = 1 /* La eliminacion del usuario fue correcta*/
	END
end

/* ---------- PROCEDIMIENTOS PARA CATEGORIA ------------------ */

-- PROCEDIMIENTO PARA GUARDAR CATEGORIA
CREATE PROC SP_RegistrarCategoria(
    @Descripcion varchar(50),
	@Estado bit,
    @Resultado int output,
    @Mensaje varchar(500) output
)
AS
begin
    SET @Resultado = 0
    IF NOT EXISTS (SELECT * FROM CATEGORIA WHERE Descripcion = @Descripcion)
    begin
        insert into CATEGORIA(Descripcion, Estado) values (@Descripcion, @Estado)
        set @Resultado = SCOPE_IDENTITY()
    end
    ELSE
        set @Mensaje = 'No se puede repetir la descripción de una categoría'
end
go

-- PROCEDIMIENTO PARA MODIFICAR CATEGORIA
create procedure sp_EditarCategoria(
    @IdCategoria int,
    @Descripcion varchar(50),
	@Estado bit,
    @Resultado bit output,
    @Mensaje varchar(500) output
)
as
begin
    SET @Resultado = 1
    IF NOT EXISTS (SELECT * FROM CATEGORIA WHERE Descripcion = @Descripcion AND IdCategoria != @IdCategoria)
        update CATEGORIA set
            Descripcion = @Descripcion,
			Estado = @Estado
        where IdCategoria = @IdCategoria
    ELSE
    begin
        SET @Resultado = 0
        set @Mensaje = 'No se puede repetir la descripción de una categoría'
    end
end
go

-- PROCEDIMIENTO PARA ELIMINAR CATEGORIA
create procedure sp_EliminarCategoria(
    @IdCategoria int,
    @Resultado bit output,
    @Mensaje varchar(500) output
)
as
begin
    SET @Resultado = 1
    IF NOT EXISTS (
        select * from CATEGORIA c
        inner join PRODUCTO p on p.IdCategoria = c.IdCategoria
        where c.IdCategoria = @IdCategoria
    )
    begin
        delete top(1) from CATEGORIA where IdCategoria = @IdCategoria
    end
    ELSE
    begin
        SET @Resultado = 0
        set @Mensaje = 'La categoría se encuentra relacionada a un producto'
    end
end

-- Insertando Categorias --
SELECT * FROM CATEGORIA

INSERT INTO CATEGORIA(Descripcion, Estado) VALUES ('Lacteos', 1)
INSERT INTO CATEGORIA(Descripcion, Estado) VALUES ('Embutidos', 1)
INSERT INTO CATEGORIA(Descripcion, Estado) VALUES ('Enlatados', 1)
INSERT INTO CATEGORIA(Descripcion, Estado) VALUES ('Carnes', 1)
INSERT INTO CATEGORIA(Descripcion, Estado) VALUES ('Bebidas', 1)

-- TODOS en 1 el estado --
update CATEGORIA set Estado = 1

select * from PRODUCTO

SELECT IdProducto, Codigo, Nombre, p.Descripcion, c.IdCategoria, 
       c.Descripcion [DescripcionCategoria], Stock, PrecioCompra, PrecioVenta, p.Estado 
FROM PRODUCTO p
INNER JOIN CATEGORIA c ON c.IdCategoria = p.IdCategoria

/* ---------- PROCEDIMIENTOS PARA PRODUCTO ----------------*/

create PROC sp_RegistrarProducto(
    @Codigo varchar(20),
    @Nombre varchar(30),
    @Descripcion varchar(30),
    @IdCategoria int,
    @Estado bit,
    @Resultado bit output,
    @Mensaje varchar(500) output
) as
begin
    SET @Resultado = 0
    IF NOT EXISTS (SELECT * FROM producto WHERE Codigo = @Codigo)
    begin
        insert into producto(Codigo, Nombre, Descripcion, IdCategoria, Estado) 
        values (@Codigo, @Nombre, @Descripcion, @IdCategoria, @Estado)
        set @Resultado = SCOPE_IDENTITY()
    end
    ELSE
        SET @Mensaje = 'Ya existe un producto con el mismo codigo'
end
GO

create procedure sp_ModificarProducto(
    @IdProducto int,
    @Codigo varchar(20),
    @Nombre varchar(30),
    @Descripcion varchar(30),
    @IdCategoria int,
    @Estado bit,
    @Resultado bit output,
    @Mensaje varchar(500) output
) 
as
begin
    SET @Resultado = 1
    IF NOT EXISTS (SELECT * FROM PRODUCTO WHERE codigo = @Codigo and IdProducto != @IdProducto)
    begin
        update PRODUCTO set
            codigo = @Codigo,
            Nombre = @Nombre,
            Descripcion = @Descripcion,
            IdCategoria = @IdCategoria,
			Estado = @Estado
        where IdProducto = @IdProducto
    end
    ELSE
    begin
        SET @Resultado = 0
        SET @Mensaje = 'Ya existe un producto con el mismo codigo'
    end
end
go

create PROCEDURE SP_EliminarProducto(
    @IdProducto int,
    @Respuesta bit output,
    @Mensaje varchar(500) output
)
AS
BEGIN
    SET @Respuesta = 0
    SET @Mensaje = ''
    DECLARE @pasoreglas bit = 1

    -- Verificar si el producto está relacionado con una compra
    IF EXISTS (SELECT * FROM DETALLE_COMPRA dc
               INNER JOIN PRODUCTO p ON p.IdProducto = dc.IdProducto
               WHERE p.IdProducto = @IdProducto)
    BEGIN
        SET @pasoreglas = 0
        SET @Respuesta = 0
        SET @Mensaje = @Mensaje + 'No se puede eliminar porque se encuentra relacionado a una COMPRA\n'
    END

    -- Verificar si el producto está relacionado con una venta
    IF EXISTS (SELECT * FROM DETALLE_VENTA dv
               INNER JOIN PRODUCTO p ON p.IdProducto = dv.IdProducto
               WHERE p.IdProducto = @IdProducto)
    BEGIN
        SET @pasoreglas = 0
        SET @Respuesta = 0
        SET @Mensaje = @Mensaje + 'No se puede eliminar porque se encuentra relacionado a una VENTA\n'
    END

    -- Si no está relacionado, proceder con la eliminación
    IF @pasoreglas = 1
    BEGIN
        DELETE FROM PRODUCTO WHERE IdProducto = @IdProducto
        SET @Respuesta = 1
    END
END

Select * from PRODUCTO

-- Insertando Producto --
insert into PRODUCTO(Codigo, Nombre, Descripcion, IdCategoria, Estado) 
values ('101010', 'gaseosa', '1litro', 5, 1)

SELECT IdProducto, Codigo, Nombre, p.Descripcion, c.IdCategoria, 
       c.Descripcion [DescripcionCategoria], Stock, PrecioCompra, PrecioVenta, p.Estado  
FROM PRODUCTO p
INNER JOIN CATEGORIA c ON c.IdCategoria = p.IdCategoria

update PRODUCTO set Estado = 1

/* ---------- PROCEDIMIENTOS PARA CLIENTE ----------------*/

create PROC sp_RegistrarCliente(
    @Documento varchar(50),
    @NombreCompleto varchar(50),
    @Correo varchar(50),
    @Telefono varchar(50),
    @Estado bit,
    @Resultado int output,
    @Mensaje varchar(500) output
)
as
begin
    SET @Resultado = 0
    DECLARE @IDPERSONA INT

    IF NOT EXISTS (SELECT * FROM CLIENTE WHERE Documento = @Documento)
    begin
        insert into CLIENTE(Documento,NombreCompleto,Correo,Telefono,Estado) values (
        @Documento,@NombreCompleto,@Correo,@Telefono,@Estado)

        set @Resultado = SCOPE_IDENTITY()
    end
    else
        set @Mensaje = 'El numero de documento ya existe'
end

go

create PROC sp_ModificarCliente(
    @IdCliente int,
    @Documento varchar(50),
    @NombreCompleto varchar(50),
    @Correo varchar(50),
    @Telefono varchar(50),
    @Estado bit,
    @Resultado bit output,
    @Mensaje varchar(500) output
)
as
begin
    SET @Resultado = 1
    DECLARE @IDPERSONA INT

    IF NOT EXISTS (SELECT * FROM CLIENTE WHERE Documento = @Documento and IdCliente != @IdCliente)
    begin
        update CLIENTE set
            Documento = @Documento,
            NombreCompleto = @NombreCompleto,
            Correo = @Correo,
            Telefono = @Telefono,
            Estado = @Estado
        where IdCliente = @IdCliente
    end
    else
    begin
        SET @Resultado = 0
        set @Mensaje = 'El numero de documento ya existe'
    end
end

-- No hace falta eliminar un cliente con un SP ya que no existen dependencias --> Lo haremos directamente en el codigo --

SELECT IdCliente, Documento, NombreCompleto, Correo, Telefono, Estado FROM CLIENTE

/* ---------- PROCEDIMIENTOS PARA PROVEEDOR ---------------*/

create PROC sp_RegistrarProveedor(
    @Documento varchar(50),
    @RazonSocial varchar(50),
    @Correo varchar(50),
    @Telefono varchar(50),
    @Estado bit,
    @Resultado int output,
    @Mensaje varchar(500) output
)
as
begin
    SET @Resultado = 0
    DECLARE @IDPERSONA INT

    IF NOT EXISTS (SELECT * FROM PROVEEDOR WHERE Documento = @Documento)
    begin
        insert into PROVEEDOR(Documento, RazonSocial, Correo, Telefono, Estado) 
        values (@Documento, @RazonSocial, @Correo, @Telefono, @Estado)

        set @Resultado = SCOPE_IDENTITY()
    end
    else
        set @Mensaje = 'El numero de documento ya existe'
end
GO

create PROC sp_ModificarProveedor(
    @IdProveedor int,
    @Documento varchar(50),
    @RazonSocial varchar(50),
    @Correo varchar(50),
    @Telefono varchar(50),
    @Estado bit,
    @Resultado bit output,
    @Mensaje varchar(500) output
)
as
begin
    SET @Resultado = 1
    DECLARE @IDPERSONA INT

    IF NOT EXISTS (SELECT * FROM PROVEEDOR WHERE Documento = @Documento and IdProveedor != @IdProveedor)
    begin
        update PROVEEDOR set
            Documento = @Documento,
            RazonSocial = @RazonSocial,
            Correo = @Correo,
            Telefono = @Telefono,
            Estado = @Estado
        where IdProveedor = @IdProveedor
    end
    else
    begin
        SET @Resultado = 0
        set @Mensaje = 'El numero de documento ya existe'
    end
end
GO

create procedure sp_EliminarProveedor(
    @IdProveedor int,
    @Resultado bit output,
    @Mensaje varchar(500) output
)
as
begin
    SET @Resultado = 1
    IF NOT EXISTS (
        select * from PROVEEDOR P
        inner join COMPRA C on P.IdProveedor = C.IdProveedor
        where P.IdProveedor = @IdProveedor
    )
    begin
        delete top(1) from PROVEEDOR where IdProveedor = @IdProveedor
    end
    ELSE
    begin
        SET @Resultado = 0
        set @Mensaje = 'El proveedor se encuentra relacionado a una compra'
    end
end

create table NEGOCIO(
    IdNegocio int primary key,
    Nombre varchar(60),
    RUC varchar(60),
    Direccion varchar(60),
    Logo varbinary(max) NULL -- Para el logo --
);

select * from NEGOCIO

insert into NEGOCIO (IdNegocio, Nombre, RUC, Direccion) 
values (1, 'Codigo Estudiante', '101010', 'av. codigo 123');

/* PROCESOS PARA REGISTRAR UNA COMPRA */
-- Esta estructura será utilizada como tipo de parametro usado en mi codigo C# --
CREATE TYPE [dbo].[EDetalle_Compra] AS TABLE(
    [IdProducto] int NULL,
    [PrecioCompra] decimal(18,2) NULL,
    [PrecioVenta] decimal(18,2) NULL,
    [Cantidad] int NULL,
    [MontoTotal] decimal(18,2) NULL
)

GO
/* PROCESO PARA REGISTRAR UNA COMPRA */

CREATE PROCEDURE sp_RegistrarCompra(
    @IdUsuario INT,
    @IdProveedor INT,
    @TipoDocumento VARCHAR(500),
    @NumeroDocumento VARCHAR(500),
    @MontoTotal DECIMAL(18,2),
    @DetalleCompra [EDetalle_Compra] READONLY,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    BEGIN TRY
        -- Inicialización de variables
        DECLARE @IdCompra INT = 0;
        SET @Resultado = 1;
        SET @Mensaje = '';

        -- Iniciar transacción
        BEGIN TRANSACTION registro;

        -- Insertar en la tabla COMPRA
        INSERT INTO COMPRA (IdUsuario, IdProveedor, TipoDocumento, NumeroDocumento, MontoTotal)
        VALUES (@IdUsuario, @IdProveedor, @TipoDocumento, @NumeroDocumento, @MontoTotal);

        -- Obtener el ID de la compra recién insertada
        SET @IdCompra = SCOPE_IDENTITY();

        -- Insertar los detalles de la compra en DETALLE_COMPRA
        INSERT INTO DETALLE_COMPRA (IdCompra, IdProducto, PrecioCompra, PrecioVenta, Cantidad, MontoTotal)
        SELECT @IdCompra, IdProducto, PrecioCompra, PrecioVenta, Cantidad, MontoTotal 
        FROM @DetalleCompra;

        -- Actualizar el stock y precios de los productos en la tabla PRODUCTO
        UPDATE p
        SET 
            p.Stock = p.Stock + dc.Cantidad,
            p.PrecioCompra = dc.PrecioCompra,
            p.PrecioVenta = dc.PrecioVenta
        FROM PRODUCTO p
        INNER JOIN @DetalleCompra dc ON dc.IdProducto = p.IdProducto;

        -- Confirmar la transacción
        COMMIT TRANSACTION registro;
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
        
        -- Revertir la transacción en caso de error
        ROLLBACK TRANSACTION registro;
    END CATCH
END;

/* Para asginar un mumero de documento a la nuyeva compra que se hace en la capa de presentacion frmCompras */
select count(*) + 1 from COMPRA

/* Chequeando si se registro bien la compra */
select * from COMPRA c
where c.NumeroDocumento = '000001'; 

select * from DETALLE_COMPRA
where IdCompra = 1; 

select c.IdCompra,
       u.NombreCompleto,
       pr.Documento, pr.RazonSocial,
       c.TipoDocumento, c.NumeroDocumento, c.MontoTotal,
       convert(char(10), c.FechaRegistro, 103) [FechaRegistro]
from COMPRA c
inner join USUARIO u on u.IdUsuario = c.IdUsuario
inner join PROVEEDOR pr on pr.IdProveedor = c.IdProveedor
where c.NumeroDocumento = '000001';

select p.Nombre, dc.PrecioCompra, dc.Cantidad, dc.MontoTotal
from DETALLE_COMPRA dc
inner join PRODUCTO p on p.IdProducto = dc.IdProducto
where dc.IdCompra = 1;

select * from CLIENTE
update CLIENTE set Estado = 1

/* PROCESOS PARA REGISTRAR UNA VENTA */
CREATE TYPE [dbo].[EDetalle_Venta] AS TABLE(
    [IdProducto] int NULL,
    [PrecioVenta] decimal(18,2) NULL,
    [Cantidad] int NULL,
    [SubTotal] decimal(18,2) NULL
)

GO

CREATE PROCEDURE usp_RegistrarVenta(
    @IdUsuario int,
    @TipoDocumento varchar(500),
    @NumeroDocumento varchar(500),
    @DocumentoCliente varchar(500),
    @NombreCliente varchar(500),
    @MontoPago decimal(18,2),
    @MontoCambio decimal(18,2),
    @MontoTotal decimal(18,2),
    @DetalleVenta [EDetalle_Venta] READONLY,
    @Resultado bit OUTPUT,
    @Mensaje varchar(500) OUTPUT
)
AS
BEGIN
    BEGIN TRY
        DECLARE @idVenta int = 0
        SET @Resultado = 1
        SET @Mensaje = ''

        BEGIN TRANSACTION registro

        INSERT INTO VENTA(IdUsuario, TipoDocumento, NumeroDocumento, DocumentoCliente, NombreCliente, MontoPago, MontoCambio, MontoTotal)
        VALUES(@IdUsuario, @TipoDocumento, @NumeroDocumento, @DocumentoCliente, @NombreCliente, @MontoPago, @MontoCambio, @MontoTotal)

        SET @idVenta = SCOPE_IDENTITY()

        INSERT INTO DETALLE_VENTA(IdVenta, IdProducto, PrecioVenta, Cantidad, SubTotal)
        SELECT @idVenta, IdProducto, PrecioVenta, Cantidad, SubTotal FROM @DetalleVenta

        COMMIT TRANSACTION registro
    END TRY
    BEGIN CATCH
        SET @Resultado = 0
        SET @Mensaje = ERROR_MESSAGE()
        ROLLBACK TRANSACTION registro
    END CATCH
END

/* Reducir el stock cuando se hace una venta */
update producto set stock = stock - @cantidad where idproducto = @idproducto

select * from Venta 

select * from DETALLE_VENTA

/* Consultas para Ver detalles de la venta */
select v.IdVenta, u.NombreCompleto,
       v.DocumentoCliente, v.NombreCliente,
       v.TipoDocumento, v.NumeroDocumento,
       v.MontoPago, v.MontoCambio, v.MontoTotal,
       convert(char(10), v.FechaRegistro, 103) [FechaRegistro]
from VENTA v
inner join USUARIO u on u.IdUsuario = v.IdUsuario
where v.NumeroDocumento = '00001'

select p.Nombre, dv.PrecioVenta, dv.Cantidad, dv.SubTotal
from DETALLE_VENTA dv
inner join PRODUCTO p on p.IdProducto = dv.IdProducto
where dv.IdVenta = 1

/* Reportes --> Se van a crear 2 entidades nuevas para cada sp */
create PROC sp_ReporteCompras(
    @fechainicio varchar(10),
    @fechafin varchar(10),
    @idproveedor int
)
as
begin
    SET DATEFORMAT dmy;
    select
        convert(char(10), c.FechaRegistro, 103) [FechaRegistro], 
        c.TipoDocumento, 
        c.NumeroDocumento, 
        c.MontoTotal,
        u.NombreCompleto [UsuarioRegistro], 
        pr.Documento [DocumentoProveedor], 
        pr.RazonSocial,
        p.Codigo [CodigoProducto], 
        p.Nombre [NombreProducto], 
        ca.Descripcion [Categoria], 
        dc.PrecioCompra, 
        dc.PrecioVenta, 
        dc.Cantidad, 
        dc.MontoTotal [SubTotal]
    from COMPRA c
    inner join USUARIO u on u.IdUsuario = c.IdUsuario
    inner join PROVEEDOR pr on pr.IdProveedor = c.IdProveedor
    inner join DETALLE_COMPRA dc on dc.IdCompra = c.IdCompra
    inner join PRODUCTO p on p.IdProducto = dc.IdProducto
    inner join CATEGORIA ca on ca.IdCategoria = p.IdCategoria
    where CONVERT(date, c.FechaRegistro) between @fechainicio and @fechafin
    and pr.IdProveedor = iif(@idproveedor=0, pr.IdProveedor, @idproveedor)
end

select * from PROVEEDOR

EXEC sp_ReporteCompras @fechainicio = '2024-01-01', @fechafin = '2025-02-23', @idproveedor = 0;

go

CREATE PROC sp_ReporteVentas(
    @fechainicio varchar(10),
    @fechafin varchar(10)
)
as
begin
    SET DATEFORMAT dmy;
    select
        convert(char(10), v.FechaRegistro, 103) [FechaRegistro], 
        v.TipoDocumento, 
        v.NumeroDocumento, 
        v.MontoTotal,
        u.NombreCompleto [UsuarioRegistro], 
        v.DocumentoCliente, 
        v.NombreCliente,
        p.Codigo [CodigoProducto], 
        p.Nombre [NombreProducto], 
        ca.Descripcion [Categoria], 
        dv.PrecioVenta, 
        dv.Cantidad, 
        dv.SubTotal
    from VENTA v
    inner join USUARIO u on u.IdUsuario = v.IdUsuario
    inner join DETALLE_VENTA dv on dv.IdVenta = v.IdVenta
    inner join PRODUCTO p on p.IdProducto = dv.IdProducto
    inner join CATEGORIA ca on ca.IdCategoria = p.IdCategoria
    where CONVERT(date, v.FechaRegistro) between @fechainicio and @fechafin
end

EXEC sp_ReporteVentas @fechainicio = '2024-01-01', @fechafin = '2025-02-23';
