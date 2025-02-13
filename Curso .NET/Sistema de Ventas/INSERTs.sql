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
