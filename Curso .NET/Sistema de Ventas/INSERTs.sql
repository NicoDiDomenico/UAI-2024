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
