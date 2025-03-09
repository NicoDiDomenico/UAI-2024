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

SELECT r.IdRol, r.Descripcion, p.NombreMenu from Rol r
inner join Permiso p
on p.IdRol = r.IdRol

SELECT r.IdRol, r.Descripcion, p.NombreMenu from Rol r
inner join Permiso p
on p.IdRol = r.IdRol

GO
---
--- NUEVO, MODIFIQUE [ETabla_Permisos] Y SP_REGISTRARROL, POR LO TANTO EJECUTAR: --> NO TOQUE NADA TAMPOCO PORQUE ANDA BIEN --
/*
DROP PROCEDURE IF EXISTS SP_REGISTRARROL;
GO

DROP TYPE IF EXISTS ETabla_Permisos;
GO
*/
CREATE TYPE [dbo].[ETabla_Permisos] AS TABLE(
    [NombreMenu] VARCHAR(100),
    [Descripcion] VARCHAR(255) -- Nueva columna agregada
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
            INSERT INTO Permiso (IdRol, NombreMenu, Descripcion)
            SELECT @IdRol, NombreMenu, Descripcion FROM @Permisos

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
CREATE PROCEDURE SP_ACTUALIZARROL(
    @IdRol INT, -- Ahora recibimos el IdRol para identificar qué rol actualizar
    @Descripcion VARCHAR(50),
    @Permisos ETabla_Permisos READONLY, -- Tipo tabla con los permisos actualizados
    @Mensaje VARCHAR(500) OUTPUT,
    @Resultado BIT OUTPUT -- Indica éxito o error
)
AS
BEGIN
    BEGIN TRY
        SET @Mensaje = ''
        SET @Resultado = 0  -- Por defecto, fallido

        BEGIN TRANSACTION  -- Iniciar transacción para evitar inconsistencias

        -- Verifica si el rol existe
        IF EXISTS (SELECT * FROM Rol WHERE IdRol = @IdRol)
        BEGIN
            -- Actualizar la descripción del rol
            UPDATE Rol 
            SET Descripcion = @Descripcion 
            WHERE IdRol = @IdRol;

            -- Eliminar los permisos actuales asociados a este rol
            DELETE FROM Permiso WHERE IdRol = @IdRol;

			DBCC CHECKIDENT ('Permiso', RESEED, 0);

            -- Insertar los permisos actualizados
            INSERT INTO Permiso (IdRol, NombreMenu, Descripcion)
            SELECT @IdRol, NombreMenu, Descripcion FROM @Permisos;
            -- Si todo está bien, marcar como éxito
            SET @Resultado = 1
            SET @Mensaje = 'Rol actualizado correctamente'
            COMMIT TRANSACTION  -- Confirmar los cambios
        END
        ELSE
        BEGIN
            SET @Mensaje = 'El rol no existe'
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

CREATE PROCEDURE SP_ELIMINARROL (
    @IdRol INT,
    @Respuesta BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    BEGIN TRY
        SET @Respuesta = 0
        SET @Mensaje = ''
        DECLARE @pasoreglas BIT = 1

        -- Evita eliminar roles protegidos
        IF @IdRol IN (1, 2, 3)
        BEGIN
            SET @pasoreglas = 0
            SET @Mensaje = 'No se puede eliminar este rol'
        END

        -- Verifica si el rol está en uso en otras tablas (ejemplo: Usuario)
        IF EXISTS (SELECT * FROM Usuario WHERE IdRol = @IdRol)
        BEGIN
            SET @pasoreglas = 0
            SET @Mensaje = 'No se puede eliminar este rol porque está en uso en la tabla Usuario'
        END

        IF @pasoreglas = 1
        BEGIN
            BEGIN TRANSACTION

            -- Eliminar los permisos asociados al rol
            DELETE FROM Permiso WHERE IdRol = @IdRol

            -- Eliminar el rol
            DELETE FROM Rol WHERE IdRol = @IdRol

            SET @Respuesta = 1
            SET @Mensaje = 'Rol eliminado correctamente'

            COMMIT TRANSACTION
        END
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SET @Respuesta = 0
        SET @Mensaje = ERROR_MESSAGE()
    END CATCH
END
GO


/* Permiso */
CREATE TABLE Permiso (
    IdPermiso INT PRIMARY KEY IDENTITY,
    IdRol INT REFERENCES rol(idRol),
    NombreMenu VARCHAR(100),
    FechaRegistro DATETIME DEFAULT GETDATE()
);

ALTER TABLE Permiso
ADD Descripcion VARCHAR(255) NULL;

INSERT INTO Permiso (idRol, nombreMenu) VALUES
(1, 'menuGestionarRutinas'),
(1, 'menuSocios'),
(1, 'menuGestionarGimnasio'),
(2, 'menuSocios'),
(3, 'menuGestionarRutinas');

UPDATE Permiso
SET Descripcion = 'Permite gestionar las rutinas de los socios del gimnasio, incluyendo su asignación y modificación.'
WHERE NombreMenu = 'menuGestionarRutinas';
UPDATE Permiso
SET Descripcion = 'Permite registrar, editar y eliminar socios, así como gestionar sus turnos para asistir al gimnasio en la fecha y hora de su preferencia.'
WHERE NombreMenu = 'menuSocios';
UPDATE Permiso
SET Descripcion = 'Permite la gestión completa del gimnasio, incluyendo usuarios, máquinas y equipamientos, además de la visualización y administración de turnos.'
WHERE NombreMenu = 'menuGestionarGimnasio';

SELECT * FROM Permiso;

SELECT r.IdRol, r.Descripcion, p.IdPermiso, p.NombreMenu  
FROM Permiso p
INNER JOIN Rol r ON p.IdRol = r.IdRol;
GO

SELECT p.NombreMenu, p.Descripcion from Permiso p
inner join Rol r
on p.IdRol = r.IdRol
Where p.IdRol = 1

select NombreMenu, Descripcion
from Permiso
group by NombreMenu, Descripcion

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
-- NUEVO --> Eliminar los SP viejos y agregar estos. --> ANDA BIEN EN LA BOOK 4 POR LO TANTO LO DEJE COMO ESTABA--
----
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
		SET @Mensaje = 'Usuario registrado correctamente'
		
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
		set @Mensaje = 'Usuario actualizado correctamente' 
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
		SET @Mensaje = 'Usuario eliminado correctamente' 
	end

end
----

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

-- No hacer --
/*
-- NUEVO, normalizando permisos.... --
CREATE TABLE Rol_Permiso (
    IdRol INT NOT NULL,
    IdPermiso INT NOT NULL,
    PRIMARY KEY (IdRol, IdPermiso),
    FOREIGN KEY (IdRol) REFERENCES Rol(IdRol) ON DELETE CASCADE,
    FOREIGN KEY (IdPermiso) REFERENCES Permiso(IdPermiso) ON DELETE CASCADE
);

SELECT name
FROM sys.foreign_keys
WHERE parent_object_id = OBJECT_ID('Permiso'); --> Devuelve: FK__Permiso__IdRol__5535A963

ALTER TABLE Permiso DROP CONSTRAINT FK__Permiso__IdRol__5535A963;

ALTER TABLE Permiso DROP COLUMN IdRol;

SELECT * FROM Permiso;

SELECT NombreMenu, COUNT(*)
FROM Permiso
GROUP BY NombreMenu
HAVING COUNT(*) > 1;

WITH CTE AS (
    SELECT 
        IdPermiso, 
        NombreMenu, 
        FechaRegistro, 
        Descripcion,
        ROW_NUMBER() OVER (PARTITION BY NombreMenu ORDER BY IdPermiso) AS fila
    FROM Permiso
)
DELETE FROM Permiso WHERE IdPermiso IN (SELECT IdPermiso FROM CTE WHERE fila > 1);

ALTER TABLE Permiso ADD CONSTRAINT UQ_Permiso_NombreMenu UNIQUE (NombreMenu);



SELECT * FROM Usuario
UPDATE Usuario
SET IdRol = 1
WHERE IdRol = 7;

SELECT * FROM Rol;
/*
SET IDENTITY_INSERT Rol ON;

DELETE FROM Rol WHERE IdRol = 7;

SELECT MAX(IdRol) AS UltimoID FROM Rol;
*/
SELECT * FROM Permiso;


DELETE FROM Permiso;
DBCC CHECKIDENT ('Permiso', RESEED, 0);
INSERT INTO Permiso (IdRol, NombreMenu, FechaRegistro, Descripcion) VALUES
(1, 'menuGestionarRutinas', GETDATE(), 'Permite gestionar las rutinas de los socios del gimnasio, incluyendo su asignación y modificación.'),
(1, 'menuSocios', GETDATE(), 'Permite registrar, editar y eliminar socios, así como gestionar sus turnos para asistir al gimnasio en la fecha y hora de su preferencia.'),
(1, 'menuGestionarGimnasio', GETDATE(), 'Permite la gestión completa del gimnasio, incluyendo usuarios, máquinas y equipamientos, además de la visualización y administración de turnos.'),
(2, 'menuSocios', GETDATE(), 'Permite registrar, editar y eliminar socios, así como gestionar sus turnos para asistir al gimnasio en la fecha y hora de su preferencia.'),
(3, 'menuGestionarRutinas', GETDATE(), 'Permite gestionar las rutinas de los socios del gimnasio, incluyendo su asignación y modificación.');

SELECT * FROM Permiso;

SELECT * FROM Rol;

DBCC CHECKIDENT ('Rol', NORESEED);
DBCC CHECKIDENT ('Rol', RESEED, 3);

DBCC CHECKIDENT ('Permiso', NORESEED);
DBCC CHECKIDENT ('Permiso', RESEED, 5);

---- CORRECCION MODULO SEGURIDAD ----

-- Accion, en vez de asignarle un rol, le asigno cada accion directamente. --
CREATE TABLE Accion (
	IdAccion INT PRIMARY KEY IDENTITY,
	IdUsuario INT REFERENCES Usuario(IdUsuario),
	IdGrupo INT NOT NULL REFERENCES Grupo(IdGrupo),
	NombreSubMenu VARCHAR(100)
);

-- Grupo, cada accion tiene un menu que tambien tiene que ser accedido si elije determianada accion, por lo tanto sseran 3 filas nomas (los 3 menus).
CREATE TABLE Grupo (
	IdGrupo INT PRIMARY KEY IDENTITY,
	NombreMenu VARCHAR(100)
);

-- Permiso, queda igual. --
CREATE TABLE Permiso (
    IdPermiso INT PRIMARY KEY IDENTITY,
    IdRol INT REFERENCES rol(idRol),
    NombreMenu VARCHAR(100),
    FechaRegistro DATETIME DEFAULT GETDATE(),
	Descripcion VARCHAR(255) NULL
);
*/


-- Rangos Horarios --
CREATE TABLE RangoHorario (
    IdRangoHorario INT PRIMARY KEY IDENTITY,
	HoraDesde Time,
	HoraHasta Time,
	Fecha Date,
	CupoActual INT NULL DEFAULT 0,
	CupoMaximo INT NOT NULL DEFAULT 0
);

CREATE TABLE RangoHorario_Usuario (
    IdRangoHorario INT NOT NULL,
    IdUsuario INT NOT NULL,
    PRIMARY KEY (IdRangoHorario, IdUsuario),
    FOREIGN KEY (IdRangoHorario) REFERENCES RangoHorario(IdRangoHorario) ON DELETE CASCADE,
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario) ON DELETE CASCADE
);
GO

CREATE PROCEDURE SP_REGISTRAR_RANGOHORARIO
    @HoraDesde TIME,
    @HoraHasta TIME,
    @Fecha DATE,
    @CupoMaximo INT,
    @IdUsuario INT,
    @Mensaje VARCHAR(500) OUTPUT,
    @Resultado BIT OUTPUT
AS
BEGIN
    BEGIN TRY
        DECLARE @IdRangoHorario INT;
        SET @Mensaje = '';
        SET @Resultado = 0;

        BEGIN TRANSACTION;

        -- Insertar el nuevo RangoHorario
        INSERT INTO RangoHorario (HoraDesde, HoraHasta, Fecha, CupoMaximo)
        VALUES (@HoraDesde, @HoraHasta, @Fecha, @CupoMaximo);

        -- Obtener el ID generado
        SET @IdRangoHorario = SCOPE_IDENTITY();

        -- Insertar en la tabla relacional
        INSERT INTO RangoHorario_Usuario (IdRangoHorario, IdUsuario)
        VALUES (@IdRangoHorario, @IdUsuario);

        SET @Resultado = 1;
        SET @Mensaje = 'Rango horario registrado correctamente.';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        SET @Mensaje = ERROR_MESSAGE();
        SET @Resultado = 0;
        ROLLBACK TRANSACTION;
    END CATCH
END;
GO

select rh.IdRangoHorario, rh.HoraDesde, rh.HoraHasta, rh.CupoMaximo, u.NombreYApellido, u.IdUsuario from RangoHorario rh
inner join RangoHorario_Usuario rh_u
on rh.IdRangoHorario = rh_u.IdRangoHorario
inner join Usuario u 
on rh_u.IdUsuario = u.IdRol

CREATE PROCEDURE SP_REGISTRAR_RANGOHORARIO_USUARIO
    @IdRangoHorario INT,
    @IdUsuario INT,
    @Mensaje VARCHAR(500) OUTPUT,
    @Resultado BIT OUTPUT
AS
BEGIN
    BEGIN TRY
        SET @Mensaje = '';
        SET @Resultado = 0;

        BEGIN TRANSACTION;

        -- Insertar el nuevo RangoHorario
        INSERT INTO RangoHorario_Usuario(IdRangoHorario, IdUsuario)
        VALUES (@IdRangoHorario, @IdUsuario);

        SET @Resultado = 1;
        SET @Mensaje = 'Rango horario registrado correctamente.';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        SET @Mensaje = ERROR_MESSAGE();
        SET @Resultado = 0;
        ROLLBACK TRANSACTION;
    END CATCH
END;
GO

-- Insertar los rangos horarios de 1 hora cada uno
INSERT INTO RangoHorario (HoraDesde, HoraHasta, Fecha, CupoActual, CupoMaximo)
SELECT 
    DATEADD(HOUR, v.number, '00:00:00') AS HoraDesde,
    DATEADD(HOUR, 1, DATEADD(HOUR, v.number, '00:00:00')) AS HoraHasta,
    GETDATE() AS Fecha,
    0 AS CupoActual,
    5 AS CupoMaximo
FROM master.dbo.spt_values v
WHERE v.type = 'P' AND v.number BETWEEN 0 AND 23;

select * from RangoHorario

select rh.IdRangoHorario, rh.HoraDesde, rh.HoraHasta, rh.CupoMaximo, u.NombreYApellido, u.IdUsuario from RangoHorario rh
inner join RangoHorario_Usuario rh_u
on rh.IdRangoHorario = rh_u.IdRangoHorario
inner join Usuario u 
on rh_u.IdUsuario = u.IdRol

select IdRangoHorario, HoraDesde, HoraHasta, CupoMaximo from RangoHorario

select * from RangoHorario_Usuario


/* Socio */
CREATE TABLE Socio (
    IdSocio INT PRIMARY KEY IDENTITY,
    NombreYApellido VARCHAR(100) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    Genero VARCHAR(50) NOT NULL,
    NroDocumento INT UNIQUE NOT NULL,
    Ciudad VARCHAR(50) NOT NULL,
    Direccion VARCHAR(50) NOT NULL,
    Telefono VARCHAR(50) NOT NULL,
    Email VARCHAR(50) NOT NULL,
    ObraSocial VARCHAR(50) NULL,
    [Plan] VARCHAR(50) NULL,  -- Corchetes para evitar conflictos con la palabra reservada
    EstadoSocio VARCHAR(50) NOT NULL,
    FechaInicioActividades DATE NULL,
    FechaFinActividades DATE NULL,
    FechaNotificacion DATE NULL,
    RespuestaNotificacion BIT NULL
);
/*
// No lo voy a hacer por ahora
CREATE TABLE HistorialRutinas (
    IdHistorialRutinas INT NOT NULL,
    UltimaFecha DATE NOT NULL,
    UltimaHora TIME NOT NULL,
    CONSTRAINT PK_HistorialRutinas PRIMARY KEY (IdHistorialRutinas, UltimaFecha, UltimaHora)
);
*/

select * from Socio s
left join Rutina r
on s.IdSocio = r.IdSocio

delete Socio where IdSocio = 1

DBCC CHECKIDENT ('Socio', RESEED, 0);
DBCC CHECKIDENT ('Rutina', RESEED, 0);

INSERT INTO Socio (NombreYApellido, FechaNacimiento, Genero, NroDocumento, Ciudad, Direccion, Telefono, Email, EstadoSocio)
VALUES ('Test Usuario', '1990-01-01', 'Masculino', 12345678, 'Ciudad Prueba', 'Calle 123', '1234567890', 'test@email.com', 'Nuevo');

DBCC CHECKIDENT ('Socio', NORESEED);

CREATE TYPE [dbo].[ETabla_Rutinas] AS TABLE(
    FechaModificacion DATE NOT NULL,    -- Fecha de modificación de la rutina
    Dia VARCHAR(20) NOT NULL            -- Día de la rutina ("Lunes", "Martes", etc.)
);
GO

CREATE PROCEDURE SP_RegistrarSocio
(
    @NombreYApellido VARCHAR(100),
    @FechaNacimiento DATE,
    @Genero VARCHAR(50),
    @NroDocumento INT,
    @Ciudad VARCHAR(50),
    @Direccion VARCHAR(50),
    @Telefono VARCHAR(50),
    @Email VARCHAR(50),
    @ObraSocial VARCHAR(50),
    @Plan VARCHAR(50),
    @EstadoSocio VARCHAR(50),
    @FechaInicioActividades DATE,
    @FechaFinActividades DATE,
    @FechaNotificacion DATE,
    @RespuestaNotificacion BIT,
    @Rutinas ETabla_Rutinas READONLY, -- Lista de rutinas
    @IdSocio INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    BEGIN TRY
        SET @Mensaje = ''
        SET @IdSocio = 0

        BEGIN TRANSACTION

        -- Verificar si ya existe un socio con el mismo documento
        IF EXISTS (SELECT 1 FROM Socio WHERE NroDocumento = @NroDocumento)
        BEGIN
            SET @Mensaje = 'El número de documento ya está registrado.'
            ROLLBACK TRANSACTION
            RETURN
        END

        -- Insertar el socio
        INSERT INTO Socio 
        (NombreYApellido, FechaNacimiento, Genero, NroDocumento, Ciudad, Direccion, Telefono, Email, ObraSocial, [Plan], EstadoSocio, FechaInicioActividades, FechaFinActividades, FechaNotificacion, RespuestaNotificacion)
        VALUES 
        (@NombreYApellido, @FechaNacimiento, @Genero, @NroDocumento, @Ciudad, @Direccion, @Telefono, @Email, @ObraSocial, @Plan, @EstadoSocio, @FechaInicioActividades, @FechaFinActividades, @FechaNotificacion, @RespuestaNotificacion)

        -- Obtener el ID generado del socio
        SET @IdSocio = SCOPE_IDENTITY()

        -- Insertar las rutinas asociadas al socio
        INSERT INTO Rutina (IdSocio, FechaModificacion, Dia)
        SELECT @IdSocio, FechaModificacion, Dia FROM @Rutinas

        -- Confirmar transacción
        COMMIT TRANSACTION
        SET @Mensaje = 'Socio registrado exitosamente con sus rutinas.'

    END TRY
    BEGIN CATCH
        SET @Mensaje = ERROR_MESSAGE()
        ROLLBACK TRANSACTION
    END CATCH
END
GO

/* Rutina */
CREATE TABLE Rutina (
	IdRutina INT PRIMARY KEY IDENTITY,
	IdSocio INT NOT NULL,
	FechaModificacion DATE NOT NULL,
	Dia VARCHAR(20) NOT NULL,
	CONSTRAINT FK_Rutina_Socio FOREIGN KEY (IdSocio) REFERENCES Socio(IdSocio) ON DELETE CASCADE
);

select * from Rutina

/* NUEVO */
SELECT s.IdSocio, s.NombreYApellido, s.Email, s.Telefono, s.Direccion, s.Ciudad, 
       s.NroDocumento, s.Genero, s.FechaNacimiento, s.ObraSocial, s.[Plan], 
       s.EstadoSocio, s.FechaInicioActividades, s.FechaFinActividades, 
       s.FechaNotificacion, s.RespuestaNotificacion, r.IdRutina, r.Dia
FROM Socio s
left join Rutina r
on s.IdSocio = r.IdSocio

UPDATE Rutina 
SET Dia = 'Miercoles' 
WHERE IdRutina = 2 AND IdSocio = 2;

CREATE PROCEDURE SP_ActualizarSocio
(
    @IdSocio INT,
    @NombreYApellido VARCHAR(100),
    @FechaNacimiento DATE,
    @Genero VARCHAR(50),
    @NroDocumento INT,
    @Ciudad VARCHAR(50),
    @Direccion VARCHAR(50),
    @Telefono VARCHAR(50),
    @Email VARCHAR(50),
    @ObraSocial VARCHAR(50),
    @Plan VARCHAR(50),
    @EstadoSocio VARCHAR(50),
    @FechaInicioActividades DATE NULL,
    @FechaFinActividades DATE NULL,
    @FechaNotificacion DATE NULL,
    @RespuestaNotificacion BIT NULL,
    @Rutinas ETabla_Rutinas READONLY,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    BEGIN TRY
        SET @Mensaje = '';

        BEGIN TRANSACTION;

        -- Actualizar los datos del socio (sin tocar rutinas)
        UPDATE Socio
        SET NombreYApellido = @NombreYApellido,
            FechaNacimiento = @FechaNacimiento,
            Genero = @Genero,
            NroDocumento = @NroDocumento,
            Ciudad = @Ciudad,
            Direccion = @Direccion,
            Telefono = @Telefono,
            Email = @Email,
            ObraSocial = @ObraSocial,
            [Plan] = @Plan,
            EstadoSocio = @EstadoSocio,
            FechaInicioActividades = @FechaInicioActividades,
            FechaFinActividades = @FechaFinActividades,
            FechaNotificacion = @FechaNotificacion,
            RespuestaNotificacion = @RespuestaNotificacion
        WHERE IdSocio = @IdSocio;

        -- Eliminar solo las rutinas que ya no están en la nueva lista
        DELETE FROM Rutina
        WHERE IdSocio = @IdSocio
        AND Dia NOT IN (SELECT Dia FROM @Rutinas);

        -- Insertar solo las rutinas nuevas (que no existían antes)
        INSERT INTO Rutina (IdSocio, FechaModificacion, Dia)
        SELECT @IdSocio, GETDATE(), Dia 
        FROM @Rutinas
        WHERE Dia NOT IN (SELECT Dia FROM Rutina WHERE IdSocio = @IdSocio);

        COMMIT TRANSACTION;
        SET @Mensaje = 'Socio actualizado correctamente con sus nuevos días de asistencia.';
    END TRY
    BEGIN CATCH
        SET @Mensaje = ERROR_MESSAGE();
        ROLLBACK TRANSACTION;
    END CATCH
END;

EXEC sp_helptext 'SP_ActualizarSocio'
EXEC sp_help 'ETabla_Rutinas';

INSERT INTO dbo.Socio (
    NombreYApellido, FechaNacimiento, Genero, NroDocumento, Ciudad, 
    Direccion, Telefono, Email, ObraSocial, [Plan], EstadoSocio, 
    FechaInicioActividades, FechaFinActividades, FechaNotificacion, RespuestaNotificacion
) 
VALUES (
    'Carlos Pérez', '1985-06-15', 'Masculino', 40123456, 'Buenos Aires', 
    'Av. Corrientes 1234', '1134567890', 'carlos.perez@email.com', 'OSDE', 'Mensual', 'Suspendido', 
    '2024-01-01', '2024-02-01', '2024-01-30', 0
);

INSERT INTO dbo.Socio (
    NombreYApellido, FechaNacimiento, Genero, NroDocumento, Ciudad, 
    Direccion, Telefono, Email, ObraSocial, [Plan], EstadoSocio, 
    FechaInicioActividades, FechaFinActividades, FechaNotificacion, RespuestaNotificacion
) 
VALUES (
    'Mariana Gómez', '1992-09-10', 'Femenino', 40234567, 'Rosario', 
    'Calle San Martín 567', '3416789123', 'mariana.gomez@email.com', 'Swiss Medical', 'Anual', 'Suspendido', 
    '2023-03-15', '2024-03-15', '2024-03-14', 0
);

INSERT INTO dbo.Socio (
    NombreYApellido, FechaNacimiento, Genero, NroDocumento, Ciudad, 
    Direccion, Telefono, Email, ObraSocial, [Plan], EstadoSocio, 
    FechaInicioActividades, FechaFinActividades, FechaNotificacion, RespuestaNotificacion
) 
VALUES (
    'Angie Gómez', '1992-09-10', 'Femenino', 39786454, 'Rosario', 
    'Calle San Martín 567', '3416789123', 'Angie.gomez@email.com', 'Swiss Medical', 'Anual', 'Suspendido', 
    '2023-03-15', '2024-03-15', '2024-03-14', 0
);

INSERT INTO dbo.Socio (
    NombreYApellido, FechaNacimiento, Genero, NroDocumento, Ciudad, 
    Direccion, Telefono, Email, ObraSocial, [Plan], EstadoSocio, 
    FechaInicioActividades, FechaFinActividades, FechaNotificacion, RespuestaNotificacion
) 
VALUES (
    'Angela Gómez', '1992-09-10', 'Femenino', 49786454, 'Rosario', 
    'Calle San Martín 567', '3416789123', 'Angie.gomez@email.com', 'Swiss Medical', 'Anual', 'Suspendido', 
    '2023-03-15', '2024-03-15', '2024-03-14', 0
);

INSERT INTO dbo.Socio (
    NombreYApellido, FechaNacimiento, Genero, NroDocumento, Ciudad, 
    Direccion, Telefono, Email, ObraSocial, [Plan], EstadoSocio, 
    FechaInicioActividades, FechaFinActividades, FechaNotificacion, RespuestaNotificacion
) 
VALUES (
    'Angelarda Gómez', '1992-09-10', 'Femenino', 59786454, 'Rosario', 
    'Calle San Martín 567', '3416789123', 'Angie.gomez@email.com', 'Swiss Medical', 'Mensual', 'Suspendido', 
    '2023-03-15', '2024-03-15', '2024-03-14', 0
);

CREATE PROCEDURE SP_ELIMINARSOCIO
    @IdSocio INT,
    @Respuesta BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET @Respuesta = 0;
    SET @Mensaje = '';

    -- Verificar si el socio tiene rutinas asignadas
    IF EXISTS (SELECT 1 FROM Rutina WHERE IdSocio = @IdSocio)
    BEGIN
        SET @Mensaje = 'No se puede eliminar el socio porque tiene rutinas asignadas.';
        RETURN;
    END

    -- Intentar eliminar el socio
    BEGIN TRY
        DELETE FROM Socio WHERE IdSocio = @IdSocio;
        SET @Respuesta = 1;
        SET @Mensaje = 'Socio eliminado correctamente.';
    END TRY
    BEGIN CATCH
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END

/* Turno */
CREATE TABLE Turno (
    IdTurno INT IDENTITY(1,1) PRIMARY KEY,  -- Clave primaria autoincremental
    IdRangoHorario INT NOT NULL,            -- Relación con RangoHorario_Usuario
    IdUsuario INT NOT NULL,                 -- Relación con RangoHorario_Usuario
    IdSocio INT NOT NULL,                   -- Relación con Socio
    FechaTurno DATE NOT NULL,               -- Fecha del turno
    EstadoTurno VARCHAR(50) NOT NULL,       -- Estado del turno
    CodigoIngreso VARCHAR(10) NOT NULL,     -- Código único para el ingreso

    -- Clave foránea compuesta con RangoHorario_Usuario
    CONSTRAINT FK_Turno_RangoHorarioUsuario 
    FOREIGN KEY (IdRangoHorario, IdUsuario) 
    REFERENCES RangoHorario_Usuario(IdRangoHorario, IdUsuario),

    -- Clave foránea con Socio
    CONSTRAINT FK_Turno_Socio 
    FOREIGN KEY (IdSocio) 
    REFERENCES Socio(IdSocio)
);
 
Select rh_u.IdRangoHorario, rh_u.IdUsuario, rh.HoraDesde, rh.HoraHasta, rh.CupoActual, rh.CupoMaximo, u.NombreYApellido
from RangoHorario rh
inner join RangoHorario_Usuario rh_u
on rh.IdRangoHorario = rh_u.IdRangoHorario
inner join Usuario u
on rh_u.IdUsuario = u.IdUsuario

CREATE PROCEDURE SP_REGISTRARTURNO
    @IdRangoHorario INT,
    @IdUsuario INT,
    @IdSocio INT,
    @FechaTurno DATE,
    @EstadoTurno VARCHAR(50),
    @CodigoIngreso VARCHAR(4),
    @IdTurnoResultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @CupoActual INT, @CupoMaximo INT, @HoraDesde TIME, @HoraHasta TIME;

    -- Validar que la fecha del turno sea hoy o futura
    IF @FechaTurno < CAST(GETDATE() AS DATE)
    BEGIN
        SET @IdTurnoResultado = 0;
        SET @Mensaje = 'Error: No puede reservar un turno para una fecha pasada.';
        RETURN;
    END

    -- Obtener la información del Rango Horario (hora de inicio y fin, cupo)
    SELECT @CupoActual = CupoActual, @CupoMaximo = CupoMaximo, 
           @HoraDesde = HoraDesde, @HoraHasta = HoraHasta
    FROM RangoHorario WHERE IdRangoHorario = @IdRangoHorario;

    -- Si el turno es para hoy, validar que el horario no haya pasado
    IF @FechaTurno = CAST(GETDATE() AS DATE) AND @HoraDesde <= CAST(GETDATE() AS TIME)
    BEGIN
        SET @IdTurnoResultado = 0;
        SET @Mensaje = 'Error: No puede reservar un turno en un horario que ya pasó.';
        RETURN;
    END

    -- Validar que el RangoHorario y el Usuario existan en RangoHorario_Usuario
    IF NOT EXISTS (
        SELECT 1 FROM RangoHorario_Usuario 
        WHERE IdRangoHorario = @IdRangoHorario AND IdUsuario = @IdUsuario
    )
    BEGIN
        SET @IdTurnoResultado = 0;
        SET @Mensaje = 'Error: El Rango Horario y Usuario especificados no existen.';
        RETURN;
    END

    -- Validar que el Socio existe
    IF NOT EXISTS (SELECT 1 FROM Socio WHERE IdSocio = @IdSocio)
    BEGIN
        SET @IdTurnoResultado = 0;
        SET @Mensaje = 'Error: El Socio especificado no existe.';
        RETURN;
    END

    -- Validar que el Socio no tenga otro turno en la misma fecha
    IF EXISTS (
        SELECT 1 FROM Turno 
        WHERE IdSocio = @IdSocio AND FechaTurno = @FechaTurno
    )
    BEGIN
        SET @IdTurnoResultado = 0;
        SET @Mensaje = 'Error: Un socio no puede reservar más de un turno para la misma fecha.';
        RETURN;
    END

    -- Validar si hay cupos disponibles
    IF @CupoActual >= @CupoMaximo
    BEGIN
        SET @IdTurnoResultado = 0;
        SET @Mensaje = 'Error: No hay cupos disponibles para este rango horario.';
        RETURN;
    END

    -- Insertar el Turno
    BEGIN TRY
        INSERT INTO Turno (IdRangoHorario, IdUsuario, IdSocio, FechaTurno, EstadoTurno, CodigoIngreso)
        VALUES (@IdRangoHorario, @IdUsuario, @IdSocio, @FechaTurno, @EstadoTurno, @CodigoIngreso);

        -- Obtener el ID del Turno insertado
        SET @IdTurnoResultado = SCOPE_IDENTITY();

        -- Aumentar el CupoActual en RangoHorario (+1)
        UPDATE RangoHorario 
        SET CupoActual = CupoActual + 1 
        WHERE IdRangoHorario = @IdRangoHorario;

        -- Mensaje de éxito con el Código de Ingreso generado
        SET @Mensaje = CONCAT('Turno registrado exitosamente. Código de Ingreso: ', @CodigoIngreso);
    END TRY
    BEGIN CATCH
        SET @IdTurnoResultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH;
END;


select * from Turno
DBCC CHECKIDENT ('Turno', NORESEED);
DBCC CHECKIDENT ('Turno', RESEED, 0);

select * from RangoHorario
/*
FALTA:

Listo --> validar que el turno que se esta sacando es para una fecha actual o superior,
Listo --> mostrar los intervalos horarios menores a la hora actual para que no registre un turno en una hora que sea imposible asister porque ya paso,
reiniciar los CuposACtuales despues de las 00:00 hs,
cuando el Socio no asiste o asiste se tiene que disminuir el cupo actual en 1 y cambiar el estado del turno a Finalizado,
Cuadno se cancela el turno se tiene que disminuir el cupo actual en 1 y cambiar el estado del turno a Cancelado,
Listo --> Un Socio no puede sacar mas de un turnopara una misma fecha (con esta validacion ya no haria falta validar que no saque mas de un turno en el mismo rango horario)
Listo --> Reiniciar manualmente la tabla turno y los cupos actuales de los rangos horarios. No olvidarse rde reiniciar los id.

*/

 SELECT t.IdTurno, t.FechaTurno, rh.IdRangoHorario, rh.HoraDesde, rh.HoraHasta, t.EstadoTurno, t.CodigoIngreso, 
        u.IdUsuario, u.NombreYApellido AS NombreEntrenador, 
        s.IdSocio, s.NombreYApellido AS NombreSocio
 FROM Turno t
 INNER JOIN Usuario u ON t.IdUsuario = u.IdUsuario
 INNER JOIN Socio s ON t.IdSocio = s.IdSocio
 INNER JOIN RangoHorario rh ON t.IdRangoHorario = rh.IdRangoHorario

CREATE PROCEDURE SP_ELIMINARTURNO
    @IdTurno INT,
    @IdRangoHorario INT,
    @Respuesta INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Verificar si el turno existe
    IF NOT EXISTS (SELECT 1 FROM Turno WHERE IdTurno = @IdTurno)
    BEGIN
        SET @Respuesta = 0;
        SET @Mensaje = 'El turno no existe.';
        RETURN;
    END

    -- Verificar si el rango horario existe
    IF NOT EXISTS (SELECT 1 FROM RangoHorario WHERE IdRangoHorario = @IdRangoHorario)
    BEGIN
        SET @Respuesta = 0;
        SET @Mensaje = 'El rango horario no existe.';
        RETURN;
    END

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Eliminar el turno
        DELETE FROM Turno WHERE IdTurno = @IdTurno;

        -- Actualizar el CupoActual del RangoHorario (restando 1)
        UPDATE RangoHorario
        SET CupoActual = CASE 
                            WHEN CupoActual > 0 THEN CupoActual - 1 
                            ELSE 0 
                         END
        WHERE IdRangoHorario = @IdRangoHorario;

        COMMIT TRANSACTION;

        SET @Respuesta = 1;
        SET @Mensaje = 'Turno eliminado correctamente y cupo actualizado.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Respuesta = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
