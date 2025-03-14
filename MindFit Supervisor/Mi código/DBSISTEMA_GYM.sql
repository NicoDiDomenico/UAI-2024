USE [DBSISTEMA_GYM]
GO
/****** Object:  UserDefinedTableType [dbo].[ETabla_Permisos]    Script Date: 28/2/2025 13:26:37 ******/
CREATE TYPE [dbo].[ETabla_Permisos] AS TABLE(
	[NombreMenu] [varchar](100) NULL
)
GO
/****** Object:  Table [dbo].[Gimnasio]    Script Date: 28/2/2025 13:26:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gimnasio](
	[IdGimnasio] [int] IDENTITY(1,1) NOT NULL,
	[NombreGimnasio] [varchar](50) NULL,
	[Direccion] [varchar](50) NULL,
	[Telefono] [varchar](50) NULL,
	[Logo] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdGimnasio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permiso]    Script Date: 28/2/2025 13:26:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permiso](
	[IdPermiso] [int] IDENTITY(1,1) NOT NULL,
	[IdRol] [int] NULL,
	[NombreMenu] [varchar](100) NULL,
	[FechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPermiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 28/2/2025 13:26:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NULL,
	[FechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 28/2/2025 13:26:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[NombreYApellido] [varchar](100) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Telefono] [varchar](50) NULL,
	[Direccion] [varchar](50) NULL,
	[Ciudad] [varchar](50) NULL,
	[NroDocumento] [int] NOT NULL,
	[Genero] [varchar](50) NULL,
	[FechaNacimiento] [datetime] NULL,
	[NombreUsuario] [varchar](50) NULL,
	[Clave] [varchar](50) NULL,
	[IdRol] [int] NULL,
	[Estado] [bit] NULL,
	[FechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[NroDocumento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Permiso] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[Rol] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[Permiso]  WITH CHECK ADD FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
GO
/****** Object:  StoredProcedure [dbo].[SP_EDITARUSUARIO]    Script Date: 28/2/2025 13:26:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[SP_EDITARUSUARIO](
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
GO
/****** Object:  StoredProcedure [dbo].[SP_ELIMINARUSUARIO]    Script Date: 28/2/2025 13:26:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[SP_ELIMINARUSUARIO](
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
GO
/****** Object:  StoredProcedure [dbo].[SP_REGISTRARROL]    Script Date: 28/2/2025 13:26:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_REGISTRARROL](
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
/****** Object:  StoredProcedure [dbo].[SP_REGISTRARUSUARIO]    Script Date: 28/2/2025 13:26:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[SP_REGISTRARUSUARIO](
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
GO
