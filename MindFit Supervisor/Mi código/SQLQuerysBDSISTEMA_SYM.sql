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
--- NUEVO, MODIFIQUE [ETabla_Permisos] Y SP_REGISTRARROL, POR LO TANTO EJECUTAR: --
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

-- NUEVO --> Listo  --
ALTER TABLE Permiso
ADD Descripcion VARCHAR(255) NULL;

INSERT INTO Permiso (idRol, nombreMenu) VALUES
(1, 'menuGestionarRutinas'),
(1, 'menuSocios'),
(1, 'menuGestionarGimnasio'),
(2, 'menuSocios'),
(3, 'menuGestionarRutinas');

-- NUEVO --> Listo --
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
-- NUEVO --> Eliminar los SP viejos y agregar estos. --
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

/* NUEVO */
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