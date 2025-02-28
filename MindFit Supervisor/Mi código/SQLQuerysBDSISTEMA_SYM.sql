/* Proyecto Gym */
CREATE database DBSISTEMA_GYM

USE DBSISTEMA_GYM

GO

/* Rol */
CREATE TABLE Rol (
    IdRol INT PRIMARY KEY IDENTITY,
    Descripcion VARCHAR(50),
    FechaRegistro DATETIME DEFAULT GETDATE()
);

INSERT INTO Rol (Descripcion) VALUES ('ADMINISTRADOR');
INSERT INTO Rol (Descripcion) VALUES ('ASISTENTE');
INSERT INTO Rol (Descripcion) VALUES ('ENTRENADOR');

SELECT * FROM Rol;

SELECT IdRol, Descripcion FROM Rol

SELECT r.IdRol, r.Descripcion, p.NombreMenu from Rol r
inner join Permiso p
on p.IdRol = r.IdRol

SELECT p.NombreMenu from Permiso p
inner join Rol r
on p.IdRol = r.IdRol
Where p.IdRol = 1

GO
---
CREATE TYPE [dbo].[ETabla_Permisos] AS TABLE(
    [NombreMenu] VARCHAR(100)
);
GO

CREATE PROCEDURE SP_REGISTRARROL(
    @Descripcion VARCHAR(50),
    @Permisos ETabla_Permisos READONLY, -- Tipo tabla
    @Mensaje VARCHAR(500) OUTPUT,
    @Resultado BIT OUTPUT -- Indica éxito o error
)
AS
BEGIN
    BEGIN TRY
        DECLARE @IdRol INT
        SET @Mensaje = ''
        SET @Resultado = 0  -- Por defecto, fallido

        BEGIN TRANSACTION  -- Iniciar transacción para evitar inconsistencias

        -- Verifica si el rol ya existe
        IF NOT EXISTS (SELECT * FROM Rol WHERE Descripcion = @Descripcion)
        BEGIN
            -- Insertar el nuevo rol
            INSERT INTO Rol (Descripcion) VALUES (@Descripcion);
            SET @IdRol = SCOPE_IDENTITY();

            -- Insertar los permisos para el rol
            INSERT INTO Permiso (IdRol, NombreMenu)
            SELECT @IdRol, NombreMenu FROM @Permisos

            -- Si todo está bien, marcar como éxito
            SET @Resultado = 1
            SET @Mensaje = 'Rol registrado correctamente'
            COMMIT TRANSACTION  -- Confirmar los cambios
        END
        ELSE
        BEGIN
            SET @Mensaje = 'No se puede tener más de un rol con la misma descripción'
            ROLLBACK TRANSACTION  -- Revertir cambios en caso de error
        END
    END TRY
    BEGIN CATCH
        SET @Mensaje = ERROR_MESSAGE()
        SET @Resultado = 0
        ROLLBACK TRANSACTION -- Revertir cambios si hay error
    END CATCH
END
GO

---

create PROC SP_EDITARUSUARIO(
	@IdUsuario int,
	@NombreUsuario VARCHAR(50),
    @NombreYApellido VARCHAR(100),
    @Email VARCHAR(100),
    @Telefono VARCHAR(50),
    @Direccion VARCHAR(100),
    @Ciudad VARCHAR(50),
    @NroDocumento INT,
    @Genero VARCHAR(50),
    @FechaNacimiento DATETIME,
    @Clave VARCHAR(100),
    @IdRol INT,
    @Estado BIT,
	@Respuesta bit output,
	@Mensaje varchar(500) output
)
as
begin
	set @Respuesta = 0
	set @Mensaje = ''


	if not exists(select * from Usuario where NroDocumento = @NroDocumento and idusuario != @IdUsuario)
	begin
		update  Usuario set
		NombreYApellido = @NombreYApellido,
		Email = @Email,
		Telefono = @Telefono,
		Direccion = @Direccion,
		Ciudad = @Ciudad,
		NroDocumento = @NroDocumento,
		Genero = @Genero,
		FechaNacimiento = @FechaNacimiento,
		NombreUsuario = @NombreUsuario,
		Clave = @Clave,
		IdRol = @IdRol,
		Estado = @Estado
		where IdUsuario = @IdUsuario

		set @Respuesta = 1
		
	end
	else
		set @Mensaje = 'No se puede repetir el documento para más de un usuario'
end

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
	-- Yo no tengo compras y ventas pero si debo tener turnos o rangos hortarios asignados a ese responsable, asi que luego debere hace esas validaciones para este sp --
	/*
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
	*/

	if(@pasoreglas = 1)
	begin
		delete from USUARIO where IdUsuario = @IdUsuario
		set @Respuesta = 1 
	end

end


/* Permiso */
CREATE TABLE Permiso (
    IdPermiso INT PRIMARY KEY IDENTITY,
    IdRol INT REFERENCES rol(idRol),
    NombreMenu VARCHAR(100),
    FechaRegistro DATETIME DEFAULT GETDATE()
);

INSERT INTO Permiso (idRol, nombreMenu) VALUES
(1, 'menuGestionarRutinas'),
(1, 'menuSocios'),
(1, 'menuGestionarGimnasio'),
(2, 'menuSocios'),
(3, 'menuGestionarRutinas');

SELECT * FROM Permiso;

SELECT r.IdRol, r.Descripcion, p.IdPermiso, p.NombreMenu  
FROM Permiso p
INNER JOIN Rol r ON p.IdRol = r.IdRol;
GO

select NombreMenu 
from Permiso
group by NombreMenu

/* Usuario */
CREATE TABLE Usuario (
    IdUsuario INT PRIMARY KEY IDENTITY,
--  idGimnasio INT NOT NULL,  -- Clave foránea (suponiendo que hay una tabla Gimnasio)
    NombreYApellido VARCHAR(100) NOT NULL,
    Email VARCHAR(50) NOT NULL UNIQUE,
    Telefono VARCHAR(50),
    Direccion VARCHAR(50),
    Ciudad VARCHAR(50),
    NroDocumento INT UNIQUE NOT NULL,
    Genero VARCHAR(50),
    FechaNacimiento DATETIME,
--  tipoResponsable VARCHAR(50),
	NombreUsuario VARCHAR(50),
    Clave VARCHAR(50),
    IdRol INT REFERENCES rol(idRol),
    Estado BIT,
    FechaRegistro DATETIME DEFAULT GETDATE()
);
GO

create PROC SP_REGISTRARUSUARIO(
	@NombreUsuario VARCHAR(50),
    @NombreYApellido VARCHAR(100),
    @Email VARCHAR(100),
    @Telefono VARCHAR(50),
    @Direccion VARCHAR(100),
    @Ciudad VARCHAR(50),
    @NroDocumento INT,
    @Genero VARCHAR(50),
    @FechaNacimiento DATETIME,
    @Clave VARCHAR(100),
    @IdRol INT,
    @Estado BIT,
    @IdUsuarioResultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
as
begin
	set @IdUsuarioResultado = 0
	set @Mensaje = ''


	if not exists(select * from Usuario where NroDocumento = @NroDocumento)
	begin
		INSERT INTO Usuario (NombreUsuario, NombreYApellido, Email, Telefono, Direccion, Ciudad, NroDocumento, Genero, FechaNacimiento, Clave, IdRol, Estado)  
		VALUES (@NombreUsuario, @NombreYApellido, @Email, @Telefono, @Direccion, @Ciudad, @NroDocumento, @Genero, @FechaNacimiento, @Clave, @IdRol, @Estado);

		set @IdUsuarioResultado = SCOPE_IDENTITY()
		
	end
	else
		set @Mensaje = 'No se puede repetir el documento para más de un usuario'

end

create PROC SP_EDITARUSUARIO(
	@IdUsuario int,
	@NombreUsuario VARCHAR(50),
    @NombreYApellido VARCHAR(100),
    @Email VARCHAR(100),
    @Telefono VARCHAR(50),
    @Direccion VARCHAR(100),
    @Ciudad VARCHAR(50),
    @NroDocumento INT,
    @Genero VARCHAR(50),
    @FechaNacimiento DATETIME,
    @Clave VARCHAR(100),
    @IdRol INT,
    @Estado BIT,
	@Respuesta bit output,
	@Mensaje varchar(500) output
)
as
begin
	set @Respuesta = 0
	set @Mensaje = ''


	if not exists(select * from Usuario where NroDocumento = @NroDocumento and idusuario != @IdUsuario)
	begin
		update  Usuario set
		NombreYApellido = @NombreYApellido,
		Email = @Email,
		Telefono = @Telefono,
		Direccion = @Direccion,
		Ciudad = @Ciudad,
		NroDocumento = @NroDocumento,
		Genero = @Genero,
		FechaNacimiento = @FechaNacimiento,
		NombreUsuario = @NombreUsuario,
		Clave = @Clave,
		IdRol = @IdRol,
		Estado = @Estado
		where IdUsuario = @IdUsuario

		set @Respuesta = 1
		
	end
	else
		set @Mensaje = 'No se puede repetir el documento para más de un usuario'
end

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
	-- Yo no tengo compras y ventas pero si debo tener turnos o rangos hortarios asignados a ese responsable, asi que luego debere hace esas validaciones para este sp --
	/*
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
	*/

	if(@pasoreglas = 1)
	begin
		delete from USUARIO where IdUsuario = @IdUsuario
		set @Respuesta = 1 
	end

end

INSERT INTO Usuario 
    (nombreUsuario, nombreYApellido, email, telefono, direccion, ciudad, nroDocumento, genero, fechaNacimiento, clave, idRol, estado) 
VALUES
    ('adm', 'Administrador General', 'admin@example.com', '123456789', 'Av. Principal 123', 'Ciudad Ejemplo', 12345678, 'Masculino', '1980-05-15 08:30:00', 'admin123', 1, 1),
    ('asistente1', 'Juan Pérez', 'juan.perez@example.com', '987654321', 'Calle Secundaria 456', 'Ciudad Ejemplo', 87654321, 'Masculino', '1990-08-20 14:45:00', 'asistente2024', 2, 1),
    ('entrenador1', 'María López', 'maria.lopez@example.com', '555111222', 'Avenida Fitness 789', 'Ciudad Deportiva', 13579246, 'Femenino', '1985-12-10 19:15:00', 'trainerfit', 3, 1);

select * from Usuario

select u.IdUsuario, NombreYApellido, u.Email, u.Telefono, u.Direccion, u.Ciudad, u.NroDocumento, u.Genero, FechaNacimiento, u.NombreUsuario, u.Clave, r.IdRol, r.Descripcion, u.Estado, u.FechaRegistro
from Usuario u
inner join Rol r
on r.IdRol = u.IdRol

SELECT p.IdRol, p.NombreMenu 
FROM PERMISO p
INNER JOIN ROL r ON r.IdRol = p.IdRol
INNER JOIN USUARIO u ON u.IdRol = r.IdRol
WHERE u.IdUsuario = 2

update Usuario set Clave = 123

go

/* Gimnasio */
CREATE TABLE Gimnasio (
    IdGimnasio INT PRIMARY KEY IDENTITY,
	NombreGimnasio VARCHAR(50),
	Direccion VARCHAR(50),
	Telefono VARCHAR(50),
	Logo varbinary(max) NULL
);
GO

INSERT INTO Gimnasio 
    (NombreGimnasio, Direccion, Telefono, Logo) 
VALUES
    ('SariesGym', 'Av. Principal 123', '123456789', null)

select * from Gimnasio

go



