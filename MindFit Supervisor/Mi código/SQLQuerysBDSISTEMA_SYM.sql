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

Select * from Rutina

UPDATE Rutina 
SET Dia = 'Miercoles' 
WHERE IdRutina = 2

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
Listo --> reiniciar los CuposACtuales despues de las 00:00 hs,
Listo --> cuando el Socio no asiste o asiste se tiene que disminuir el cupo actual en 1 y cambiar el estado del turno a Finalizado,
Listo --> Cuadno se cancela el turno se tiene que disminuir el cupo actual en 1 y cambiar el estado del turno a Cancelado,
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
 /*
 -- ESTE NO PORQUE NO QUIERO RESTAR EL CUPO CUANDO SE ELIMINA EL TURNO, SOLO QUIERO QUE PASE ESTO CUANDO SE HACE EL INGRESO DEL SOCIO AL GYM (FINALZIADO) O CUANDO NO SE PRESNETA EN EL DIA DE LA FECHA (CANCELADO O VENCIDO)
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
*/
CREATE PROCEDURE SP_ELIMINARTURNO
    @IdTurno INT,
    @IdRangoHorario INT,
    @Respuesta INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @EstadoTurno VARCHAR(50);

    -- Verificar si el turno existe y obtener su estado
    IF NOT EXISTS (SELECT 1 FROM Turno WHERE IdTurno = @IdTurno)
    BEGIN
        SET @Respuesta = 0;
        SET @Mensaje = 'El turno no existe.';
        RETURN;
    END

    -- Obtener el EstadoTurno del turno
    SELECT @EstadoTurno = EstadoTurno
    FROM Turno WHERE IdTurno = @IdTurno;

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Eliminar el turno
        DELETE FROM Turno WHERE IdTurno = @IdTurno;

        -- Si el estado del turno era "En Curso", restar 1 al cupo del rango horario
        IF @EstadoTurno = 'En Curso'
        BEGIN
            UPDATE RangoHorario
            SET CupoActual = CASE 
                                WHEN CupoActual > 0 THEN CupoActual - 1 
                                ELSE 0 
                             END
            WHERE IdRangoHorario = @IdRangoHorario;
        END

        COMMIT TRANSACTION;

        SET @Respuesta = 1;
        SET @Mensaje = 'Turno eliminado correctamente.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Respuesta = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;

ALTER TABLE Turno
ADD CONSTRAINT UQ_CodigoIngreso UNIQUE (CodigoIngreso);

/* NUEVO --> Listo */
ALTER PROCEDURE SP_REGISTRARTURNO
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
    
    DECLARE @CupoActual INT, @CupoMaximo INT, @HoraDesde TIME, @HoraHasta TIME, @EstadoSocio VARCHAR(50);

    -- Obtener el estado del socio
    SELECT @EstadoSocio = EstadoSocio 
    FROM Socio 
    WHERE IdSocio = @IdSocio;

    -- Validar si el socio está Suspendido o Eliminado
    IF @EstadoSocio IN ('Suspendido', 'Eliminado')
    BEGIN
        SET @IdTurnoResultado = 0;
        SET @Mensaje = 'Error: No puede reservar un turno porque el socio está suspendido o eliminado.';
        RETURN;
    END

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

SELECT t.IdTurno, t.FechaTurno, rh.IdRangoHorario, rh.HoraDesde, rh.HoraHasta, 
                       t.EstadoTurno, t.CodigoIngreso, 
                       u.IdUsuario, u.NombreYApellido AS NombreEntrenador, 
                       s.IdSocio, s.NombreYApellido AS NombreSocio, rh.CupoActual, rh.CupoMaximo
                FROM Turno t
                INNER JOIN Usuario u ON t.IdUsuario = u.IdUsuario
                INNER JOIN Socio s ON t.IdSocio = s.IdSocio
                INNER JOIN RangoHorario rh ON t.IdRangoHorario = rh.IdRangoHorario
                WHERE t.FechaTurno = CAST(GETDATE() AS DATE) -- Solo turnos de hoy
                AND rh.HoraDesde <= CAST(GETDATE() AS TIME)  -- Horario actual o anterior
                AND rh.HoraHasta >= CAST(GETDATE() AS TIME)  -- Todavía dentro del horario

select * from RangoHorario

select IdRangoHorario, HoraDesde, HoraHasta, CupoMaximo 
from RangoHorario

---- CORRECCION MODULO SEGURIDAD ----
-- Grupo, cada accion tiene un menu que tambien tiene que ser accedido si elije determianada accion, por lo tanto sseran 3 filas nomas (los 3 menus).
CREATE TABLE Grupo (
    IdGrupo INT PRIMARY KEY IDENTITY,
    NombreMenu VARCHAR(100) NOT NULL UNIQUE,
    Descripcion VARCHAR(255) NULL
);

INSERT INTO Grupo (NombreMenu, Descripcion)
SELECT DISTINCT NombreMenu, Descripcion FROM Permiso;

ALTER TABLE Permiso ADD IdGrupo INT NULL;

UPDATE Permiso
SET IdGrupo = (SELECT IdGrupo FROM Grupo WHERE Grupo.NombreMenu = Permiso.NombreMenu);

ALTER TABLE Permiso 
ADD CONSTRAINT FK_Permiso_Grupo FOREIGN KEY (IdGrupo) REFERENCES Grupo(IdGrupo);

ALTER TABLE Permiso DROP COLUMN NombreMenu, Descripcion;

/*
-- Este yano va:
SELECT p.IdRol, p.NombreMenu 
FROM PERMISO p
INNER JOIN ROL r ON r.IdRol = p.IdRol
INNER JOIN USUARIO u ON u.IdRol = r.IdRol
WHERE u.IdUsuario = 1
*/

-- Este si:
SELECT p.IdRol, g.NombreMenu 
FROM PERMISO p
INNER JOIN ROL r ON r.IdRol = p.IdRol
INNER JOIN USUARIO u ON u.IdRol = r.IdRol
INNER JOIN GRUPO g on p.IdGrupo = g.IdGrupo
WHERE u.IdUsuario = 1 /*Reemplazar 1 por @idusuario en el query del backend --> Listo*/

-- Accion, en vez de asignarle un rol, le asigno cada accion directamente. --
CREATE TABLE Accion (
	IdAccion INT PRIMARY KEY IDENTITY,
 -- IdGrupo INT NOT NULL REFERENCES Grupo(IdGrupo),
	NombreAccion VARCHAR(100) NOT NULL,
	Descripcion VARCHAR(255) NULL
);

ALTER TABLE Permiso ADD IdAccion INT NULL;
ALTER TABLE Permiso ADD CONSTRAINT FK_Permiso_Accion FOREIGN KEY (IdAccion) REFERENCES Accion(IdAccion);

/*
Un permiso puede estar asociado a un grupo (IdGrupo).
Un permiso también puede estar asociado a una acción específica (IdAccion).
Se puede dejar IdGrupo o IdAccion en NULL, permitiendo que el permiso sea flexible.
*/

SELECT p.IdRol, g.NombreMenu, a.NombreAccion
                FROM PERMISO p
                INNER JOIN ROL r ON r.IdRol = p.IdRol
                INNER JOIN USUARIO u ON u.IdRol = r.IdRol
                LEFT JOIN GRUPO g ON p.IdGrupo = g.IdGrupo -- Puede ser NULL
                LEFT JOIN ACCION a ON p.IdAccion = a.IdAccion -- Puede ser NULL
                WHERE u.IdUsuario = 1 -- @idusuario

select * from Permiso

select * from Grupo

-- Cambio esto:
select NombreMenu, Descripcion
from Permiso
group by NombreMenu, Descripcion
-- Por esto:
select IdGrupo, NombreMenu, Descripcion
from Grupo

-- Cambio esto:
SELECT p.NombreMenu, p.Descripcion from Permiso p
inner join Rol r
on p.IdRol = r.IdRol
Where p.IdRol = @IdRol
-- Por esto:
SELECT g.NombreMenu, g.Descripcion, g.IdGrupo 
from Permiso p
inner join Rol r
on p.IdRol = r.IdRol
inner join Grupo g
on p.IdGrupo = g.IdGrupo
Where p.IdRol = 5 -- @IdRol

DROP PROCEDURE IF EXISTS SP_REGISTRARROL;
DROP PROCEDURE IF EXISTS SP_ACTUALIZARROL;
DROP PROCEDURE IF EXISTS SP_ELIMINARROL;

DROP TYPE IF EXISTS [dbo].[ETabla_Permisos];
GO

CREATE TYPE [dbo].[ETabla_Permisos] AS TABLE (
    [IdGrupo] INT NOT NULL -- Se usará solo para almacenar grupos
);
GO

CREATE PROCEDURE SP_REGISTRARROL(
    @Descripcion VARCHAR(50),
    @Permisos ETabla_Permisos READONLY, -- Ahora usa el nuevo tipo de tabla
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
        IF NOT EXISTS (SELECT 1 FROM Rol WHERE Descripcion = @Descripcion)
        BEGIN
            -- Insertar el nuevo rol
            INSERT INTO Rol (Descripcion) VALUES (@Descripcion);
            SET @IdRol = SCOPE_IDENTITY();

            -- Insertar los grupos asociados al rol
            INSERT INTO Permiso (IdRol, IdGrupo)
            SELECT @IdRol, IdGrupo FROM @Permisos;

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
SELECT * FROM Permiso WHERE IdRol = 3;

CREATE PROCEDURE SP_ACTUALIZARROL(
    @IdRol INT, -- Identifica qué rol actualizar
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
        IF EXISTS (SELECT 1 FROM Rol WHERE IdRol = @IdRol)
        BEGIN
            -- Actualizar la descripción del rol
            UPDATE Rol 
            SET Descripcion = @Descripcion 
            WHERE IdRol = @IdRol;

            -- Eliminar los grupos actuales asociados a este rol
            DELETE FROM Permiso WHERE IdRol = @IdRol;

            -- Insertar los nuevos grupos
            INSERT INTO Permiso (IdRol, IdGrupo)
            SELECT @IdRol, IdGrupo FROM @Permisos;

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
Go

ALTER TABLE Permiso
ADD IdUsuario INT NULL;

ALTER TABLE Permiso
ADD CONSTRAINT FK_Permiso_Usuario
FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario);

-- Traigo los permisos de un usuario (1) que pueden ser por Grupo (tiene rol) o por accion (tiene usuario)
SELECT 
    p.IdPermiso, p.IdRol, p.IdUsuario, 
    g.NombreMenu AS Grupo, a.NombreAccion AS Accion
FROM Permiso p
LEFT JOIN Grupo g ON p.IdGrupo = g.IdGrupo
LEFT JOIN Accion a ON p.IdAccion = a.IdAccion
WHERE p.IdUsuario = 1 OR p.IdRol = (SELECT IdRol FROM Usuario WHERE IdUsuario = 1);

ALTER TABLE Accion
ADD IdGrupo INT NOT NULL;

ALTER TABLE Accion
ADD CONSTRAINT FK_Accion_Grupo
FOREIGN KEY (IdGrupo) REFERENCES Grupo(IdGrupo);
-- Cambio esto:
SELECT p.IdPermiso, p.IdRol, g.NombreMenu, a.NombreAccion
FROM PERMISO p
INNER JOIN ROL r ON r.IdRol = p.IdRol
INNER JOIN USUARIO u ON u.IdRol = r.IdRol
LEFT JOIN GRUPO g ON p.IdGrupo = g.IdGrupo -- Puede ser NULL
LEFT JOIN ACCION a ON p.IdAccion = a.IdAccion -- Puede ser NULL
WHERE u.IdUsuario = 1 -- @idusuario
-- Por esto - COALESCE() devolverá el primer valor no nulo entre NombreAccionGrupo y NombreAccion::
SELECT p.IdPermiso, p.IdRol, g.NombreMenu, 
       COALESCE(ac.NombreAccion, a.NombreAccion) AS NombreAccion
FROM PERMISO p
INNER JOIN ROL r ON r.IdRol = p.IdRol
INNER JOIN USUARIO u ON u.IdRol = r.IdRol
LEFT JOIN GRUPO g ON p.IdGrupo = g.IdGrupo -- Puede ser NULL
LEFT JOIN ACCION ac ON g.IdGrupo = ac.IdGrupo -- Puede ser NULL
LEFT JOIN ACCION a ON p.IdAccion = a.IdAccion -- Puede ser NULL
WHERE u.IdUsuario = 1 -- @idusuario

INSERT INTO Accion (NombreAccion, Descripcion, IdGrupo) VALUES
('menuUsuarios', 'Gestionar usuarios', 1),
('menuRoles', 'Gestionar roles', 1),
('menuMaquinas', 'Gestionar maquinas', 1),
('menuEjercicios', 'Gestionar ejercicios', 1),
('menuEquipamiento', 'Gestionar equipamiento', 1),
('menuRangosHorarios', 'Gestionar rangos horarios', 1),
('menuNegocio', 'Consultar los datos del gimnasio y su logo', 1),
('btnMenuAgregar', 'Agregar un nuevo socio al sistema', 3),
('btnMenuConsultar', 'Ver y modificar un Socio actual', 3),
('btnMenuEliminar', 'Eliminar un socio actual', 3),
('btnMenuTurno', 'Consultar los turnos actuales y anteriores de los socios registrados y agregar uno nuevo', 3);

SELECT name 
FROM sys.foreign_keys 
WHERE parent_object_id = OBJECT_ID('Permiso');

ALTER TABLE Permiso DROP CONSTRAINT FK_Permiso_Usuario;

ALTER TABLE Permiso DROP COLUMN IdUsuario;
/*
// no hacer
CREATE TABLE Usuario_Accion (
    IdUsuario INT NOT NULL,
    IdAccion INT NOT NULL,
    FechaRegistro DATETIME DEFAULT GETDATE(),
    PRIMARY KEY (IdUsuario, IdAccion),
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (IdAccion) REFERENCES Accion(IdAccion) ON DELETE CASCADE ON UPDATE CASCADE
);
*/

ALTER TABLE Permiso
ADD IdUsuario INT NULL;

ALTER TABLE Permiso
ADD CONSTRAINT FK_Permiso_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario);

select u.IdUsuario, NombreYApellido, u.Email, u.Telefono, u.Direccion, u.Ciudad, u.NroDocumento, u.Genero, FechaNacimiento, u.NombreUsuario, u.Clave, r.IdRol, r.Descripcion, u.Estado, u.FechaRegistro
from Usuario u
inner join Rol r
on r.IdRol = u.IdRol

SELECT u.IdUsuario, u.NombreYApellido, u.Email, u.Telefono, 
                       u.Direccion, u.Ciudad, u.NroDocumento, u.Genero, 
                       u.FechaNacimiento, u.NombreUsuario, u.Clave, 
                       u.IdRol, r.Descripcion AS RolDescripcion, 
                       u.Estado, u.FechaRegistro
                FROM Usuario u
                LEFT JOIN Rol r ON r.IdRol = u.IdRol

SELECT p.IdPermiso, p.IdRol, g.NombreMenu, 
       COALESCE(ac.NombreAccion, a.NombreAccion) AS NombreAccion
FROM PERMISO p
INNER JOIN ROL r ON r.IdRol = p.IdRol
INNER JOIN USUARIO u ON u.IdRol = r.IdRol
LEFT JOIN GRUPO g ON p.IdGrupo = g.IdGrupo -- Puede ser NULL
LEFT JOIN ACCION a ON p.IdAccion = a.IdAccion -- Puede ser NULL
LEFT JOIN ACCION ac ON g.IdGrupo = ac.IdGrupo -- Puede ser NULL
WHERE u.IdUsuario = 1 --@idusuario

SELECT p.IdPermiso, p.IdRol, g.NombreMenu, ac.NombreAccion, a.NombreAccion 
FROM PERMISO p
INNER JOIN ROL r ON r.IdRol = p.IdRol
INNER JOIN USUARIO u ON u.IdRol = r.IdRol
LEFT JOIN GRUPO g ON p.IdGrupo = g.IdGrupo -- Puede ser NULL
LEFT JOIN ACCION a ON p.IdAccion = a.IdAccion -- Puede ser NULL
LEFT JOIN ACCION ac ON g.IdGrupo = ac.IdGrupo -- Puede ser NULL
WHERE u.IdUsuario = 1

select p.IdPermiso, g.NombreMenu, a.NombreAccion
from Permiso p
INNER JOIN USUARIO u ON p.IdUsuario = u.IdUsuario
LEFT JOIN ACCION a ON p.IdAccion = a.IdAccion -- Puede ser NULL
LEFT JOIN GRUPO g ON a.IdGrupo = g.IdGrupo -- Puede ser NULL
WHERE u.IdUsuario = 6

SELECT p.IdPermiso, p.IdRol, g.NombreMenu, 
       COALESCE(ac.NombreAccion, a.NombreAccion) AS NombreAccion
FROM PERMISO p
LEFT JOIN ROL r ON r.IdRol = p.IdRol -- Cambié de INNER JOIN a LEFT JOIN
LEFT JOIN USUARIO u ON u.IdRol = r.IdRol OR u.IdUsuario = p.IdUsuario -- Incluir usuarios sin rol, pero con permisos directos
LEFT JOIN GRUPO g ON p.IdGrupo = g.IdGrupo -- Puede ser NULL
LEFT JOIN ACCION a ON p.IdAccion = a.IdAccion -- Puede ser NULL
LEFT JOIN ACCION ac ON g.IdGrupo = ac.IdGrupo -- Puede ser NULL
WHERE u.IdUsuario = 6 -- @idusuario

-- CLAVE LE SALIO
SELECT 
    p.IdPermiso, 
    p.IdRol, 
    COALESCE(g.NombreMenu, am.NombreMenu) AS NombreMenu, 
    COALESCE(ac.NombreAccion, a.NombreAccion) AS NombreAccion
FROM PERMISO p
LEFT JOIN ROL r ON r.IdRol = p.IdRol 
LEFT JOIN USUARIO u ON u.IdRol = r.IdRol OR u.IdUsuario = p.IdUsuario 
LEFT JOIN GRUPO g ON p.IdGrupo = g.IdGrupo -- Puede ser NULL si es acción directa
LEFT JOIN ACCION a ON p.IdAccion = a.IdAccion -- Acciones individuales
LEFT JOIN ACCION ac ON g.IdGrupo = ac.IdGrupo -- Acciones que provienen de grupos
LEFT JOIN ( -- Subconsulta para asignar el NombreMenu a acciones individuales
    SELECT a.IdAccion, g.NombreMenu 
    FROM ACCION a
    LEFT JOIN GRUPO g ON a.IdGrupo = g.IdGrupo
) am ON a.IdAccion = am.IdAccion
WHERE u.IdUsuario = 6 -- @idusuario

/* NUEVO - DE AHORA EN MAS EXPORTAR LA BD CON ESQUEMA Y DATOS */

select a.NombreAccion
from Accion a
inner join Permiso p
on a.IdAccion = p.IdAccion
inner join Usuario u
on a.IdAccion = p.IdAccion 
where u.IdUsuario = 6

SELECT u.IdUsuario, u.NombreYApellido, u.Email, u.Telefono, 
       u.Direccion, u.Ciudad, u.NroDocumento, u.Genero, 
       u.FechaNacimiento, u.NombreUsuario, u.Clave, 
       u.IdRol, r.Descripcion AS RolDescripcion, 
       u.Estado, u.FechaRegistro
FROM Usuario u
LEFT JOIN Rol r ON r.IdRol = u.IdRol

--Cambio esto:
select a.NombreAccion
from Accion a
inner join Permiso p
on a.IdAccion = p.IdAccion
inner join Usuario u
on a.IdAccion = p.IdAccion 
where u.IdUsuario = 2

--Por esto:
SELECT 
    COALESCE(ac.NombreAccion, a.NombreAccion) AS NombreAccion
FROM PERMISO p
LEFT JOIN ROL r ON r.IdRol = p.IdRol 
LEFT JOIN USUARIO u ON u.IdRol = r.IdRol OR u.IdUsuario = p.IdUsuario 
LEFT JOIN GRUPO g ON p.IdGrupo = g.IdGrupo -- Puede ser NULL si es acción directa
LEFT JOIN ACCION a ON p.IdAccion = a.IdAccion -- Acciones individuales
LEFT JOIN ACCION ac ON g.IdGrupo = ac.IdGrupo -- Acciones que provienen de grupos
LEFT JOIN ( -- Subconsulta para asignar el NombreMenu a acciones individuales
    SELECT a.IdAccion, g.NombreMenu 
    FROM ACCION a
    LEFT JOIN GRUPO g ON a.IdGrupo = g.IdGrupo
) am ON a.IdAccion = am.IdAccion
WHERE u.IdUsuario = 6 -- @idusuario

ALTER PROC [dbo].[SP_ELIMINARUSUARIO](
    @IdUsuario int,
    @Respuesta bit output,
    @Mensaje varchar(500) output
)
AS
BEGIN
    SET @Respuesta = 0
    SET @Mensaje = ''
    DECLARE @pasoreglas bit = 1

    -- Validaciones (agregar aquí las que necesites, por ejemplo, turnos asignados, rangos horarios, etc.)

    IF (@pasoreglas = 1)
    BEGIN
        -- Eliminar los permisos del usuario en la tabla Permiso
        DELETE FROM PERMISO WHERE IdUsuario = @IdUsuario

        -- Eliminar el usuario de la tabla Usuario
        DELETE FROM USUARIO WHERE IdUsuario = @IdUsuario

        SET @Respuesta = 1
        SET @Mensaje = 'Usuario eliminado correctamente junto con sus permisos'
    END
END

ALTER PROCEDURE [dbo].[SP_ACTUALIZARROL](
    @IdRol INT,  -- Identifica qué rol actualizar
    @Descripcion VARCHAR(50),  -- Nueva descripción del rol
    @IdGrupo INT,  -- Grupo asociado al rol
    @DescripcionGrupo VARCHAR(255),  -- Nueva descripción del grupo
    @Mensaje VARCHAR(500) OUTPUT,
    @Resultado BIT OUTPUT  -- Indica éxito o error
)
AS
BEGIN
    BEGIN TRY
        SET @Mensaje = ''
        SET @Resultado = 0  -- Por defecto, fallido

        BEGIN TRANSACTION  -- Iniciar transacción para evitar inconsistencias

        -- Verifica si el rol existe
        IF EXISTS (SELECT 1 FROM Rol WHERE IdRol = @IdRol)
        BEGIN
            -- Actualizar la descripción del rol
            UPDATE Rol 
            SET Descripcion = @Descripcion 
            WHERE IdRol = @IdRol;

            -- Verifica si el grupo existe
            IF EXISTS (SELECT 1 FROM Grupo WHERE IdGrupo = @IdGrupo)
            BEGIN
                -- Actualizar la descripción del grupo
                UPDATE Grupo 
                SET Descripcion = @DescripcionGrupo 
                WHERE IdGrupo = @IdGrupo;
            END
            ELSE
            BEGIN
                SET @Mensaje = 'El grupo especificado no existe.'
                ROLLBACK TRANSACTION  -- Revertir cambios en caso de error
                RETURN;
            END

            -- Si todo está bien, marcar como éxito
            SET @Resultado = 1
            SET @Mensaje = 'Rol y grupo actualizados correctamente'
            COMMIT TRANSACTION  -- Confirmar los cambios
        END
        ELSE
        BEGIN
            SET @Mensaje = 'El rol especificado no existe.'
            ROLLBACK TRANSACTION  -- Revertir cambios en caso de error
        END
    END TRY
    BEGIN CATCH
        SET @Mensaje = ERROR_MESSAGE()
        SET @Resultado = 0
        ROLLBACK TRANSACTION -- Revertir cambios si hay error
    END CATCH
END

select IdAccion, NombreAccion, Descripcion, IdGrupo
from Accion

CREATE PROCEDURE [dbo].[SP_MODIFICARACCION](
    @IdAccion INT,               -- Identificador de la acción a modificar
    @NombreAccion VARCHAR(100),  -- Nuevo nombre de la acción
    @Descripcion VARCHAR(255),   -- Nueva descripción
    @IdGrupo INT,                -- Nuevo ID de grupo asociado
    @Resultado BIT OUTPUT,       -- Indica éxito (1) o fallo (0)
    @Mensaje VARCHAR(500) OUTPUT -- Mensaje de confirmación o error
)
AS
BEGIN
    BEGIN TRY
        -- Inicialización de variables de salida
        SET @Mensaje = ''
        SET @Resultado = 0

        -- Verificar si la acción existe
        IF NOT EXISTS (SELECT 1 FROM Accion WHERE IdAccion = @IdAccion)
        BEGIN
            SET @Mensaje = 'La acción especificada no existe.'
            RETURN
        END

        -- Actualizar la acción en la tabla Accion
        UPDATE Accion
        SET NombreAccion = @NombreAccion,
            Descripcion = @Descripcion,
            IdGrupo = @IdGrupo
        WHERE IdAccion = @IdAccion;

        -- Si se actualiza correctamente
        SET @Resultado = 1
        SET @Mensaje = 'Acción actualizada correctamente.'
    END TRY
    BEGIN CATCH
        -- Captura de errores y rollback en caso de falla
        SET @Mensaje = ERROR_MESSAGE()
        SET @Resultado = 0
    END CATCH
END
GO

select IdAccion, NombreAccion, Descripcion, IdGrupo
from Accion

SELECT u.IdUsuario, u.NombreYApellido, u.Email, u.Telefono, 
       u.Direccion, u.Ciudad, u.NroDocumento, u.Genero, 
       u.FechaNacimiento, u.NombreUsuario, u.Clave, 
       u.IdRol, r.Descripcion AS RolDescripcion, 
       u.Estado, u.FechaRegistro
FROM Usuario u
LEFT JOIN Rol r ON r.IdRol = u.IdRol

/* Relacion Completa */
select u.IdUsuario, u.NombreYApellido, rh.IdRangoHorario, rh.HoraDesde, rh.HoraHasta, t.IdTurno, t.FechaTurno, s.IdSocio, s.NombreYApellido, r.IdRutina, r.Dia
from RangoHorario rh
inner join RangoHorario_Usuario rh_u
on rh.IdRangoHorario = rh_u.IdRangoHorario
inner join Usuario u 
on rh_u.IdUsuario = u.IdRol
inner join Turno t on u.IdUsuario = t.IdUsuario
inner join Socio s on t.IdSocio = s.IdSocio
inner join Rutina r on s.IdSocio = r.IdSocio
where t.EstadoTurno = 'En Curso'

-- Cambio esto
select rh.IdRangoHorario, rh.HoraDesde, rh.HoraHasta, u.NombreYApellido, u.IdUsuario from RangoHorario rh
inner join RangoHorario_Usuario rh_u
on rh.IdRangoHorario = rh_u.IdRangoHorario
inner join Usuario u 
on rh_u.IdUsuario = u.IdRol

-- Por esto:
select rh.IdRangoHorario, rh.HoraDesde, rh.HoraHasta, u.NombreYApellido, u.IdUsuario
from RangoHorario rh
inner join RangoHorario_Usuario rh_u 
on rh.IdRangoHorario = rh_u.IdRangoHorario 
inner join Usuario u on u.IdUsuario = rh_u.IdUsuario
--
select * 
from RangoHorario rh
inner join Turno t 
on t.IdRangoHorario = rh.IdRangoHorario
inner join RangoHorario_Usuario rh_u 
on rh.IdRangoHorario = rh_u.IdRangoHorario 
inner join Usuario u on u.IdUsuario = rh_u.IdUsuario

select u.IdUsuario, u.NombreYApellido
from Usuario u
inner join RangoHorario_Usuario rh_u 
on u.IdUsuario = rh_u.IdUsuario  
inner join Turno t on t.IdRangoHorario = rh_u.IdRangoHorario
where rh_u.IdRangoHorario = 1 -- @IdRangoHorario

select IdRangoHorario, HoraDesde, HoraHasta, CupoMaximo 
from RangoHorario

-- ListarTodo()
select rh.IdRangoHorario, rh.HoraDesde, rh.HoraHasta, u.NombreYApellido, u.IdUsuario
from RangoHorario rh
inner join RangoHorario_Usuario rh_u 
on rh.IdRangoHorario = rh_u.IdRangoHorario 
inner join Usuario u on u.IdUsuario = rh_u.IdUsuario

-- ListarParaTurno
Select rh_u.IdRangoHorario, rh_u.IdUsuario, rh.HoraDesde, rh.HoraHasta, rh.CupoActual, rh.CupoMaximo, u.NombreYApellido
from RangoHorario rh
inner join RangoHorario_Usuario rh_u
on rh.IdRangoHorario = rh_u.IdRangoHorario
inner join Usuario u
on rh_u.IdUsuario = u.IdUsuario

-- ListarEntrenadoresDisponibles
SELECT DISTINCT rh_u.IdUsuario, u.NombreYApellido 
                FROM RangoHorario rh
                INNER JOIN RangoHorario_Usuario rh_u ON rh.IdRangoHorario = rh_u.IdRangoHorario
                INNER JOIN Usuario u ON rh_u.IdUsuario = u.IdUsuario
				where rh.CupoActual < rh.CupoMaximo and rh.IdRangoHorario = 1 -- @IdRangoHorario

-- CORREGIR CUPOS SEGUN FECHA, NO SE COMO SERÁ LA LÓGICA 
CREATE TABLE CupoFecha (
    IdCupoFecha INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATE NOT NULL,
    IdRangoHorario INT NOT NULL,
    CupoActual INT NOT NULL DEFAULT 0,
    FOREIGN KEY (IdRangoHorario) REFERENCES RangoHorario(IdRangoHorario) ON DELETE CASCADE
);

USE [DBSISTEMA_GYM]
GO

ALTER PROCEDURE [dbo].[SP_REGISTRARTURNO]
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

    DECLARE @CupoActual INT, @CupoMaximo INT, @EstadoSocio VARCHAR(50);

    -- Obtener el estado del socio
    SELECT @EstadoSocio = EstadoSocio 
    FROM Socio 
    WHERE IdSocio = @IdSocio;

    -- Validar si el socio está Suspendido o Eliminado
    IF @EstadoSocio IN ('Suspendido', 'Eliminado')
    BEGIN
        SET @IdTurnoResultado = 0;
        SET @Mensaje = 'Error: No puede reservar un turno porque el socio está suspendido o eliminado.';
        RETURN;
    END

    -- Validar que la fecha del turno sea hoy o futura
    IF @FechaTurno < CAST(GETDATE() AS DATE)
    BEGIN
        SET @IdTurnoResultado = 0;
        SET @Mensaje = 'Error: No puede reservar un turno para una fecha pasada.';
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

    -- Obtener CupoMaximo desde RangoHorario
    SELECT @CupoMaximo = CupoMaximo 
    FROM RangoHorario 
    WHERE IdRangoHorario = @IdRangoHorario;

    -- Verificar si existe un registro en CupoFecha para la fecha dada
    IF NOT EXISTS (
        SELECT 1 FROM CupoFecha 
        WHERE IdRangoHorario = @IdRangoHorario 
          AND Fecha = @FechaTurno
    )
    BEGIN
        -- Si no existe, lo creamos con CupoActual = 0
        INSERT INTO CupoFecha (Fecha, IdRangoHorario, CupoActual)
        VALUES (@FechaTurno, @IdRangoHorario, 0);
    END

    -- Obtener el CupoActual desde CupoFecha
    SELECT @CupoActual = CupoActual 
    FROM CupoFecha 
    WHERE IdRangoHorario = @IdRangoHorario 
      AND Fecha = @FechaTurno;

    -- Validar si hay cupos disponibles
    IF @CupoActual >= @CupoMaximo
    BEGIN
        SET @IdTurnoResultado = 0;
        SET @Mensaje = 'Error: No hay cupos disponibles para este rango horario en esta fecha.';
        RETURN;
    END

    -- Insertar el Turno
    BEGIN TRY
        INSERT INTO Turno (IdRangoHorario, IdUsuario, IdSocio, FechaTurno, EstadoTurno, CodigoIngreso)
        VALUES (@IdRangoHorario, @IdUsuario, @IdSocio, @FechaTurno, @EstadoTurno, @CodigoIngreso);

        -- Obtener el ID del Turno insertado
        SET @IdTurnoResultado = SCOPE_IDENTITY();

        -- Aumentar el CupoActual en CupoFecha (+1)
        UPDATE CupoFecha 
        SET CupoActual = CupoActual + 1 
        WHERE IdRangoHorario = @IdRangoHorario AND Fecha = @FechaTurno;

        -- Mensaje de éxito con el Código de Ingreso generado
        SET @Mensaje = CONCAT('Turno registrado exitosamente. Código de Ingreso: ', @CodigoIngreso);
    END TRY
    BEGIN CATCH
        SET @IdTurnoResultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH;
END;

-- Si funca
select cf.*, rh.HoraDesde, rh.HoraHasta, t.FechaTurno from CupoFecha cf
inner join RangoHorario rh
on rh.IdRangoHorario = cf.IdRangoHorario
inner join Turno t on t.IdRangoHorario = rh.IdRangoHorario

select * from turno t
inner join RangoHorario rh
on t.IdRangoHorario = rh.IdRangoHorario
inner join CupoFecha cf
on rh.IdRangoHorario = cf.IdRangoHorario
where t.FechaTurno = cf.Fecha

select * from CupoFecha

select * from RangoHorario

ALTER PROCEDURE [dbo].[SP_ELIMINARTURNO]
    @IdTurno INT,
    @IdRangoHorario INT,
    @FechaTurno DATE,
    @Respuesta INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @EstadoTurno VARCHAR(50);

    -- Verificar si el turno existe y obtener su estado
    IF NOT EXISTS (SELECT 1 FROM Turno WHERE IdTurno = @IdTurno)
    BEGIN
        SET @Respuesta = 0;
        SET @Mensaje = 'Error: El turno no existe.';
        RETURN;
    END

    -- Obtener el EstadoTurno del turno
    SELECT @EstadoTurno = EstadoTurno
    FROM Turno 
    WHERE IdTurno = @IdTurno;

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Eliminar el turno
        DELETE FROM Turno WHERE IdTurno = @IdTurno;

        -- Verificar si existe un registro en CupoFecha para la fecha y el rango horario
        IF EXISTS (
            SELECT 1 FROM CupoFecha 
            WHERE IdRangoHorario = @IdRangoHorario AND Fecha = @FechaTurno
        )
        BEGIN
            -- Restar 1 al CupoActual en CupoFecha si hay cupos registrados
            UPDATE CupoFecha
            SET CupoActual = CASE 
                                WHEN CupoActual > 0 THEN CupoActual - 1 
                                ELSE 0 
                             END
            WHERE IdRangoHorario = @IdRangoHorario 
              AND Fecha = @FechaTurno;
        END

        COMMIT TRANSACTION;

        SET @Respuesta = 1;
        SET @Mensaje = 'Turno eliminado correctamente.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Respuesta = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;

-- Cambio esto:
SELECT t.IdTurno, t.FechaTurno, rh.IdRangoHorario, rh.HoraDesde, rh.HoraHasta, 
   t.EstadoTurno, t.CodigoIngreso, 
   u.IdUsuario, u.NombreYApellido AS NombreEntrenador, 
   s.IdSocio, s.NombreYApellido AS NombreSocio, 
   rh.CupoActual, rh.CupoMaximo
FROM Turno t
INNER JOIN Usuario u ON t.IdUsuario = u.IdUsuario
INNER JOIN Socio s ON t.IdSocio = s.IdSocio
INNER JOIN RangoHorario rh ON t.IdRangoHorario = rh.IdRangoHorario
WHERE t.FechaTurno = CAST(GETDATE() AS DATE) -- Solo turnos de hoy
AND rh.HoraDesde <= CAST(GETDATE() AS TIME)  -- Horario actual o anterior
AND rh.HoraHasta >= CAST(GETDATE() AS TIME)  -- Todavía dentro del horario
-- Por esto:
SELECT t.IdTurno, t.FechaTurno, rh.IdRangoHorario, rh.HoraDesde, rh.HoraHasta, 
       t.EstadoTurno, t.CodigoIngreso, 
       u.IdUsuario, u.NombreYApellido AS NombreEntrenador, 
       s.IdSocio, s.NombreYApellido AS NombreSocio, 
       cf.CupoActual, rh.CupoMaximo -- CupoActual desde CupoFecha, CupoMaximo desde RangoHorario
FROM Turno t
INNER JOIN Usuario u ON t.IdUsuario = u.IdUsuario
INNER JOIN Socio s ON t.IdSocio = s.IdSocio
INNER JOIN RangoHorario rh ON t.IdRangoHorario = rh.IdRangoHorario
INNER JOIN CupoFecha cf ON cf.IdRangoHorario = rh.IdRangoHorario 
    AND cf.Fecha = t.FechaTurno -- Se obtiene el cupo exacto de esa fecha
WHERE t.FechaTurno = CAST(GETDATE() AS DATE) -- Solo turnos de hoy
AND rh.HoraDesde <= CAST(GETDATE() AS TIME)  -- Horario actual o anterior
AND rh.HoraHasta >= CAST(GETDATE() AS TIME)  -- Todavía dentro del horario

select * from RangoHorario rh
INNER JOIN RangoHorario_Usuario rh_u ON rh.IdRangoHorario = rh_u.IdRangoHorario
INNER JOIN Usuario u ON rh_u.IdUsuario = u.IdUsuario
WHERE rh.CupoActual < rh.CupoMaximo 
AND rh.IdRangoHorario = 1

-- Cambio esto:
SELECT DISTINCT rh_u.IdUsuario, u.NombreYApellido 
FROM RangoHorario rh
INNER JOIN RangoHorario_Usuario rh_u ON rh.IdRangoHorario = rh_u.IdRangoHorario
INNER JOIN Usuario u ON rh_u.IdUsuario = u.IdUsuario
WHERE rh.CupoActual < rh.CupoMaximo 
AND rh.IdRangoHorario = 1
-- Por esto:
SELECT DISTINCT rh_u.IdUsuario, u.NombreYApellido, rh.CupoMaximo, cf.CupoActual
FROM RangoHorario rh
INNER JOIN RangoHorario_Usuario rh_u ON rh.IdRangoHorario = rh_u.IdRangoHorario
INNER JOIN Usuario u ON rh_u.IdUsuario = u.IdUsuario
left JOIN CupoFecha cf ON cf.IdRangoHorario = rh.IdRangoHorario 
WHERE (cf.CupoActual < rh.CupoMaximo OR cf.CupoActual IS NULL) -- cf.CupoActual IS NULL quiere decir que no se registraron turnos para esa fecha, entonces es como que cupo actual es 0
AND rh.IdRangoHorario = 1
AND cf.Fecha = '2025-03-17' -- @FechaTurno
-- Mejor esto:
SELECT DISTINCT rh_u.IdUsuario, u.NombreYApellido, rh.CupoMaximo, 
       COALESCE(cf.CupoActual, 0) AS CupoActual -- Si no hay registros en CupoFecha, se considera como 0
FROM RangoHorario rh
INNER JOIN RangoHorario_Usuario rh_u ON rh.IdRangoHorario = rh_u.IdRangoHorario
INNER JOIN Usuario u ON rh_u.IdUsuario = u.IdUsuario
LEFT JOIN CupoFecha cf ON cf.IdRangoHorario = rh.IdRangoHorario 
    AND cf.Fecha = '2025-03-16' -- @FechaTurno -- Se mueve aquí para permitir NULLs en el WHERE
WHERE (COALESCE(cf.CupoActual, 0) < rh.CupoMaximo) -- Si cf.CupoActual es NULL, se trata como 0
AND rh.IdRangoHorario = 20;
-- pero no anda con query, asi que probmaos con SP --> SI anda.
CREATE PROCEDURE SP_ListarEntrenadoresDisponibles
    @IdRangoHorario INT,
    @FechaTurno DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT 
        rh_u.IdUsuario, 
        u.NombreYApellido, 
        rh.CupoMaximo, 
        COALESCE(cf.CupoActual, 0) AS CupoActual -- Si no hay registros en CupoFecha, se considera como 0
    FROM RangoHorario rh
    INNER JOIN RangoHorario_Usuario rh_u 
        ON rh.IdRangoHorario = rh_u.IdRangoHorario
    INNER JOIN Usuario u 
        ON rh_u.IdUsuario = u.IdUsuario
    LEFT JOIN CupoFecha cf 
        ON cf.IdRangoHorario = rh.IdRangoHorario 
        AND cf.Fecha = @FechaTurno -- Se filtra por fecha específica
    WHERE COALESCE(cf.CupoActual, 0) < rh.CupoMaximo
    AND rh.IdRangoHorario = @IdRangoHorario;
END;
GO

select * from Turno
select * from RangoHorario
select * from CupoFecha

-- Chequeando Turnos --> Mal asignados lo entrenadores, LISTO y los cupos actuales ahora respetan la fecha del turno
select rh.IdRangoHorario, u.NombreYApellido, rh.HoraDesde, rh.HoraHasta, t.FechaTurno, t.EstadoTurno from Turno t
inner join Usuario u
on t.IdUsuario = u.IdUsuario
inner join RangoHorario rh
on t.IdRangoHorario = rh.IdRangoHorario
where t.EstadoTurno = 'En Curso'

select rh_u.IdRangoHorario, u.NombreYApellido, rh.HoraDesde, rh.HoraHasta from RangoHorario rh
inner join RangoHorario_Usuario rh_u
on rh.IdRangoHorario = rh_u.IdRangoHorario
inner join Usuario u
on rh_u.IdUsuario = u.IdUsuario

select * from Turno

/*
ELIMINAR TURNOS:
24	María López	23:00:00.0000000	00:00:00.0000000	2025-03-17	En Curso
15	María López	14:00:00.0000000	15:00:00.0000000	2025-03-17	En Curso
19	María López	18:00:00.0000000	19:00:00.0000000	2025-03-17	En Curso
*/
DELETE FROM Turno
WHERE IdTurno IN (9, 8, 7);
