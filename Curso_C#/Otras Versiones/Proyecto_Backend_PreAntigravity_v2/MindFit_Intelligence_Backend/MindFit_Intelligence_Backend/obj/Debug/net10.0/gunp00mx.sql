IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Usuario] (
    [IdUsuario] int NOT NULL IDENTITY,
    [FechaRegistro] date NOT NULL,
    [Username] nvarchar(max) NOT NULL,
    [PasswordHash] nvarchar(max) NOT NULL,
    [Rol] nvarchar(max) NOT NULL,
    [RefreshToken] nvarchar(max) NULL,
    [RefreshTokenExpiryTime] datetime2 NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY ([IdUsuario])
);

CREATE TABLE [PersonaResponsable] (
    [IdUsuario] int NOT NULL,
    [Nombre] varchar(50) NOT NULL,
    [Apellido] varchar(50) NOT NULL,
    [Email] varchar(50) NOT NULL,
    [Telefono] varchar(20) NULL,
    [Direccion] varchar(50) NULL,
    [Ciudad] varchar(50) NULL,
    [TipoDocumento] varchar(50) NOT NULL,
    [NroDocumento] varchar(20) NOT NULL,
    [Genero] int NULL,
    [FechaNacimiento] date NULL,
    CONSTRAINT [PK_PersonaResponsable] PRIMARY KEY ([IdUsuario]),
    CONSTRAINT [FK_PersonaResponsable_Usuario_IdUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuario] ([IdUsuario]) ON DELETE CASCADE
);

CREATE TABLE [PersonaSocio] (
    [IdUsuario] int NOT NULL,
    [Nombre] varchar(50) NOT NULL,
    [Apellido] varchar(50) NOT NULL,
    [Email] varchar(50) NOT NULL,
    [Telefono] varchar(20) NULL,
    [Direccion] varchar(50) NULL,
    [Ciudad] varchar(50) NULL,
    [TipoDocumento] varchar(50) NOT NULL,
    [NroDocumento] varchar(20) NOT NULL,
    [Genero] int NULL,
    [FechaNacimiento] date NULL,
    [ObraSocial] varchar(50) NULL,
    [Plan] varchar(50) NULL,
    [EstadoSocio] varchar(50) NULL,
    [FechaInicioActividades] date NULL,
    [FechaFinActividades] date NULL,
    [FechaNotificacion] date NULL,
    [RespuestaNotificacion] bit NULL,
    [Pregunta] varchar(100) NULL,
    [Respuesta] varchar(100) NULL,
    CONSTRAINT [PK_PersonaSocio] PRIMARY KEY ([IdUsuario]),
    CONSTRAINT [FK_PersonaSocio_Usuario_IdUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuario] ([IdUsuario]) ON DELETE CASCADE
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260210171414_InitDB', N'10.0.3');

COMMIT;
GO

BEGIN TRANSACTION;
DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Usuario]') AND [c].[name] = N'Username');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Usuario] DROP CONSTRAINT ' + @var + ';');
ALTER TABLE [Usuario] ALTER COLUMN [Username] varchar(50) NOT NULL;

DECLARE @var1 nvarchar(max);
SELECT @var1 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Usuario]') AND [c].[name] = N'Rol');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Usuario] DROP CONSTRAINT ' + @var1 + ';');
ALTER TABLE [Usuario] ALTER COLUMN [Rol] varchar(30) NOT NULL;

DECLARE @var2 nvarchar(max);
SELECT @var2 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Usuario]') AND [c].[name] = N'RefreshToken');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Usuario] DROP CONSTRAINT ' + @var2 + ';');
ALTER TABLE [Usuario] ALTER COLUMN [RefreshToken] varchar(512) NULL;

DECLARE @var3 nvarchar(max);
SELECT @var3 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Usuario]') AND [c].[name] = N'PasswordHash');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Usuario] DROP CONSTRAINT ' + @var3 + ';');
ALTER TABLE [Usuario] ALTER COLUMN [PasswordHash] varchar(255) NOT NULL;

CREATE UNIQUE INDEX [IX_Usuario_Username] ON [Usuario] ([Username]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260211182024_JWT', N'10.0.3');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Usuario] ADD [PasswordResetTokenExpiryTime] datetime2 NULL;

ALTER TABLE [Usuario] ADD [PasswordResetTokenHash] varchar(64) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260213161344_AddPasswordResetFields', N'10.0.3');

COMMIT;
GO

BEGIN TRANSACTION;
CREATE TABLE [Grupo] (
    [IdGrupo] int NOT NULL IDENTITY,
    [Nombre] varchar(50) NOT NULL,
    [Descripcion] varchar(200) NULL,
    CONSTRAINT [PK_Grupo] PRIMARY KEY ([IdGrupo])
);

CREATE TABLE [Permiso] (
    [IdPermiso] int NOT NULL IDENTITY,
    [Codigo] varchar(80) NOT NULL,
    [Descripcion] varchar(200) NULL,
    CONSTRAINT [PK_Permiso] PRIMARY KEY ([IdPermiso])
);

CREATE TABLE [UsuarioGrupo] (
    [IdUsuario] int NOT NULL,
    [IdGrupo] int NOT NULL,
    CONSTRAINT [PK_UsuarioGrupo] PRIMARY KEY ([IdUsuario], [IdGrupo]),
    CONSTRAINT [FK_UsuarioGrupo_Grupo_IdGrupo] FOREIGN KEY ([IdGrupo]) REFERENCES [Grupo] ([IdGrupo]) ON DELETE CASCADE,
    CONSTRAINT [FK_UsuarioGrupo_Usuario_IdUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuario] ([IdUsuario]) ON DELETE CASCADE
);

CREATE TABLE [GrupoPermiso] (
    [IdGrupo] int NOT NULL,
    [IdPermiso] int NOT NULL,
    CONSTRAINT [PK_GrupoPermiso] PRIMARY KEY ([IdGrupo], [IdPermiso]),
    CONSTRAINT [FK_GrupoPermiso_Grupo_IdGrupo] FOREIGN KEY ([IdGrupo]) REFERENCES [Grupo] ([IdGrupo]) ON DELETE CASCADE,
    CONSTRAINT [FK_GrupoPermiso_Permiso_IdPermiso] FOREIGN KEY ([IdPermiso]) REFERENCES [Permiso] ([IdPermiso]) ON DELETE CASCADE
);

CREATE UNIQUE INDEX [IX_Grupo_Nombre] ON [Grupo] ([Nombre]);

CREATE INDEX [IX_GrupoPermiso_IdPermiso] ON [GrupoPermiso] ([IdPermiso]);

CREATE UNIQUE INDEX [IX_Permiso_Codigo] ON [Permiso] ([Codigo]);

CREATE INDEX [IX_UsuarioGrupo_IdGrupo] ON [UsuarioGrupo] ([IdGrupo]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260216214734_AddSecurityModule', N'10.0.3');

COMMIT;
GO

BEGIN TRANSACTION;
DECLARE @var4 nvarchar(max);
SELECT @var4 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Usuario]') AND [c].[name] = N'Rol');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Usuario] DROP CONSTRAINT ' + @var4 + ';');
ALTER TABLE [Usuario] ALTER COLUMN [Rol] varchar(30) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260218131758_rolNull', N'10.0.3');

COMMIT;
GO

BEGIN TRANSACTION;

                INSERT INTO Grupo (Nombre, Descripcion)
                SELECT DISTINCT u.Rol, 'Migrado desde Usuario.Rol'
                FROM Usuario u
                WHERE u.Rol IS NOT NULL AND LTRIM(RTRIM(u.Rol)) <> ''
                  AND NOT EXISTS (
                      SELECT 1 FROM Grupo g WHERE g.Nombre = u.Rol
                  );
            


                INSERT INTO UsuarioGrupo (IdUsuario, IdGrupo)
                SELECT u.IdUsuario, g.IdGrupo
                FROM Usuario u
                JOIN Grupo g ON g.Nombre = u.Rol
                WHERE u.Rol IS NOT NULL AND LTRIM(RTRIM(u.Rol)) <> ''
                  AND NOT EXISTS (
                      SELECT 1 FROM UsuarioGrupo ug
                      WHERE ug.IdUsuario = u.IdUsuario AND ug.IdGrupo = g.IdGrupo
                  );
            

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260219224310_MoveUsuarioRolToGrupo', N'10.0.3');

COMMIT;
GO

BEGIN TRANSACTION;
DECLARE @var5 nvarchar(max);
SELECT @var5 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Usuario]') AND [c].[name] = N'Rol');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Usuario] DROP CONSTRAINT ' + @var5 + ';');
ALTER TABLE [Usuario] DROP COLUMN [Rol];

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260219224654_DropUsuarioRol', N'10.0.3');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [UsuarioGrupo] DROP CONSTRAINT [FK_UsuarioGrupo_Grupo_IdGrupo];

UPDATE [PersonaSocio] SET [Plan] = 'Mensual' WHERE [Plan] IS NULL;

DECLARE @var6 nvarchar(max);
SELECT @var6 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PersonaSocio]') AND [c].[name] = N'Plan');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [PersonaSocio] DROP CONSTRAINT ' + @var6 + ';');
UPDATE [PersonaSocio] SET [Plan] = '' WHERE [Plan] IS NULL;
ALTER TABLE [PersonaSocio] ALTER COLUMN [Plan] varchar(50) NOT NULL;
ALTER TABLE [PersonaSocio] ADD DEFAULT '' FOR [Plan];

DECLARE @var7 nvarchar(max);
SELECT @var7 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PersonaSocio]') AND [c].[name] = N'Genero');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [PersonaSocio] DROP CONSTRAINT ' + @var7 + ';');
ALTER TABLE [PersonaSocio] ALTER COLUMN [Genero] varchar(20) NULL;

UPDATE [PersonaSocio] SET [Genero] = 'Masculino' WHERE [Genero] = '1';

UPDATE [PersonaSocio] SET [Genero] = 'Femenino' WHERE [Genero] = '2';

UPDATE [PersonaSocio] SET [Genero] = 'Otro' WHERE [Genero] = '3';

UPDATE [PersonaSocio] SET [Genero] = 'NoEspecifica' WHERE [Genero] = '4';

UPDATE [PersonaSocio] SET [EstadoSocio] = 'Nuevo' WHERE [EstadoSocio] IS NULL;

DECLARE @var8 nvarchar(max);
SELECT @var8 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PersonaSocio]') AND [c].[name] = N'EstadoSocio');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [PersonaSocio] DROP CONSTRAINT ' + @var8 + ';');
UPDATE [PersonaSocio] SET [EstadoSocio] = '' WHERE [EstadoSocio] IS NULL;
ALTER TABLE [PersonaSocio] ALTER COLUMN [EstadoSocio] varchar(50) NOT NULL;
ALTER TABLE [PersonaSocio] ADD DEFAULT '' FOR [EstadoSocio];

DECLARE @var9 nvarchar(max);
SELECT @var9 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PersonaResponsable]') AND [c].[name] = N'Genero');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [PersonaResponsable] DROP CONSTRAINT ' + @var9 + ';');
ALTER TABLE [PersonaResponsable] ALTER COLUMN [Genero] varchar(20) NULL;

UPDATE [PersonaResponsable] SET [Genero] = 'Masculino' WHERE [Genero] = '1';

UPDATE [PersonaResponsable] SET [Genero] = 'Femenino' WHERE [Genero] = '2';

UPDATE [PersonaResponsable] SET [Genero] = 'Otro' WHERE [Genero] = '3';

UPDATE [PersonaResponsable] SET [Genero] = 'NoEspecifica' WHERE [Genero] = '4';

CREATE TABLE [Dia] (
    [IdDia] int NOT NULL IDENTITY,
    [NombreDia] varchar(25) NOT NULL,
    CONSTRAINT [PK_Dia] PRIMARY KEY ([IdDia])
);

CREATE TABLE [Rutina] (
    [IdRutina] int NOT NULL IDENTITY,
    [FechaModificacion] datetime2 NOT NULL,
    [IdPersonaSocio] int NOT NULL,
    [IdDia] int NOT NULL,
    CONSTRAINT [PK_Rutina] PRIMARY KEY ([IdRutina]),
    CONSTRAINT [FK_Rutina_Dia_IdDia] FOREIGN KEY ([IdDia]) REFERENCES [Dia] ([IdDia]) ON DELETE CASCADE,
    CONSTRAINT [FK_Rutina_PersonaSocio_IdPersonaSocio] FOREIGN KEY ([IdPersonaSocio]) REFERENCES [PersonaSocio] ([IdUsuario]) ON DELETE CASCADE
);

CREATE UNIQUE INDEX [IX_Dia_NombreDia] ON [Dia] ([NombreDia]);

CREATE INDEX [IX_Rutina_IdDia] ON [Rutina] ([IdDia]);

CREATE INDEX [IX_Rutina_IdPersonaSocio] ON [Rutina] ([IdPersonaSocio]);

ALTER TABLE [UsuarioGrupo] ADD CONSTRAINT [FK_UsuarioGrupo_Grupo_IdGrupo] FOREIGN KEY ([IdGrupo]) REFERENCES [Grupo] ([IdGrupo]) ON DELETE NO ACTION;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260226142528_NuevasTablasVFinal', N'10.0.3');

COMMIT;
GO

BEGIN TRANSACTION;
DROP INDEX [IX_Rutina_IdPersonaSocio] ON [Rutina];

CREATE UNIQUE INDEX [IX_Rutina_IdPersonaSocio_IdDia] ON [Rutina] ([IdPersonaSocio], [IdDia]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260226190011_RestriccionSocioDiaUnico', N'10.0.3');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Rutina] ADD [Activo] bit NOT NULL DEFAULT CAST(0 AS bit);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260227012809_AtributoActivoRutina', N'10.0.3');

COMMIT;
GO

BEGIN TRANSACTION;
CREATE TABLE [PerfilIA] (
    [IdUsuario] int NOT NULL,
    [ObjetivoPrincipal] varchar(500) NULL,
    [NivelExperiencia] varchar(500) NULL,
    [EjerciciosPreferidos] varchar(500) NULL,
    [EjerciciosAEvitar] varchar(500) NULL,
    [DisponibilidadHoraria] varchar(500) NULL,
    [MotivacionPersonal] varchar(500) NULL,
    CONSTRAINT [PK_PerfilIA] PRIMARY KEY ([IdUsuario]),
    CONSTRAINT [FK_PerfilIA_PersonaSocio_IdUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [PersonaSocio] ([IdUsuario]) ON DELETE CASCADE
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260227231629_PerfilIA', N'10.0.3');

COMMIT;
GO

BEGIN TRANSACTION;
CREATE TABLE [Cuota] (
    [IdCuota] int NOT NULL IDENTITY,
    [IdUsuario] int NOT NULL,
    [Plan] varchar(50) NOT NULL,
    [FechaInicioPeriodo] date NOT NULL,
    [FechaFinPeriodo] date NOT NULL,
    [Monto] decimal(10,2) NOT NULL,
    [EstadoCuota] varchar(50) NOT NULL,
    [FechaPago] date NULL,
    CONSTRAINT [PK_Cuota] PRIMARY KEY ([IdCuota]),
    CONSTRAINT [FK_Cuota_PersonaSocio_IdUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [PersonaSocio] ([IdUsuario]) ON DELETE CASCADE
);

CREATE TABLE [RangoHorario] (
    [IdRangoHorario] int NOT NULL IDENTITY,
    [HoraDesde] time NOT NULL,
    [HoraHasta] time NOT NULL,
    [CupoMaximo] int NOT NULL,
    [Activo] bit NOT NULL,
    [IdDia] int NOT NULL,
    CONSTRAINT [PK_RangoHorario] PRIMARY KEY ([IdRangoHorario]),
    CONSTRAINT [FK_RangoHorario_Dia_IdDia] FOREIGN KEY ([IdDia]) REFERENCES [Dia] ([IdDia]) ON DELETE NO ACTION
);

CREATE TABLE [CupoFecha] (
    [IdCupoFecha] int NOT NULL IDENTITY,
    [IdRangoHorario] int NOT NULL,
    [Fecha] date NOT NULL,
    [CupoActual] int NOT NULL,
    CONSTRAINT [PK_CupoFecha] PRIMARY KEY ([IdCupoFecha]),
    CONSTRAINT [FK_CupoFecha_RangoHorario_IdRangoHorario] FOREIGN KEY ([IdRangoHorario]) REFERENCES [RangoHorario] ([IdRangoHorario]) ON DELETE NO ACTION
);

CREATE TABLE [RangoHorarioResponsable] (
    [IdRangoHorario] int NOT NULL,
    [IdUsuarioResponsable] int NOT NULL,
    CONSTRAINT [PK_RangoHorarioResponsable] PRIMARY KEY ([IdRangoHorario], [IdUsuarioResponsable]),
    CONSTRAINT [FK_RangoHorarioResponsable_PersonaResponsable_IdUsuarioResponsable] FOREIGN KEY ([IdUsuarioResponsable]) REFERENCES [PersonaResponsable] ([IdUsuario]) ON DELETE CASCADE,
    CONSTRAINT [FK_RangoHorarioResponsable_RangoHorario_IdRangoHorario] FOREIGN KEY ([IdRangoHorario]) REFERENCES [RangoHorario] ([IdRangoHorario]) ON DELETE CASCADE
);

CREATE TABLE [Turno] (
    [IdTurno] int NOT NULL IDENTITY,
    [IdUsuarioResponsable] int NOT NULL,
    [IdUsuarioSocio] int NOT NULL,
    [IdCupoFecha] int NOT NULL,
    [FechaAlta] date NOT NULL,
    [EstadoTurno] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Turno] PRIMARY KEY ([IdTurno]),
    CONSTRAINT [FK_Turno_CupoFecha_IdCupoFecha] FOREIGN KEY ([IdCupoFecha]) REFERENCES [CupoFecha] ([IdCupoFecha]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Turno_PersonaResponsable_IdUsuarioResponsable] FOREIGN KEY ([IdUsuarioResponsable]) REFERENCES [PersonaResponsable] ([IdUsuario]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Turno_PersonaSocio_IdUsuarioSocio] FOREIGN KEY ([IdUsuarioSocio]) REFERENCES [PersonaSocio] ([IdUsuario]) ON DELETE CASCADE
);

CREATE INDEX [IX_Cuota_IdUsuario] ON [Cuota] ([IdUsuario]);

CREATE INDEX [IX_CupoFecha_IdRangoHorario] ON [CupoFecha] ([IdRangoHorario]);

CREATE INDEX [IX_RangoHorario_IdDia] ON [RangoHorario] ([IdDia]);

CREATE INDEX [IX_RangoHorarioResponsable_IdUsuarioResponsable] ON [RangoHorarioResponsable] ([IdUsuarioResponsable]);

CREATE INDEX [IX_Turno_IdCupoFecha] ON [Turno] ([IdCupoFecha]);

CREATE INDEX [IX_Turno_IdUsuarioResponsable] ON [Turno] ([IdUsuarioResponsable]);

CREATE INDEX [IX_Turno_IdUsuarioSocio] ON [Turno] ([IdUsuarioSocio]);


                INSERT INTO Cuota (IdUsuario, Plan, FechaInicioPeriodo, FechaFinPeriodo, Monto, EstadoCuota)
                SELECT IdUsuario, Plan, GETDATE(), ISNULL(FechaFinActividades, GETDATE()), 0, 'Vencida'
                FROM PersonaSocio
            

DECLARE @var10 nvarchar(max);
SELECT @var10 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PersonaSocio]') AND [c].[name] = N'FechaFinActividades');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [PersonaSocio] DROP CONSTRAINT ' + @var10 + ';');
ALTER TABLE [PersonaSocio] DROP COLUMN [FechaFinActividades];

DECLARE @var11 nvarchar(max);
SELECT @var11 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PersonaSocio]') AND [c].[name] = N'Plan');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [PersonaSocio] DROP CONSTRAINT ' + @var11 + ';');
ALTER TABLE [PersonaSocio] DROP COLUMN [Plan];

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260309123827_CuotaYTurno', N'10.0.3');

COMMIT;
GO

