USE [master]
GO
/****** Object:  Database [DBSISTEMA_GYM]    Script Date: 31/7/2025 13:19:21 ******/
CREATE DATABASE [DBSISTEMA_GYM]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBSISTEMA_GYM', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\DBSISTEMA_GYM.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DBSISTEMA_GYM_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\DBSISTEMA_GYM_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DBSISTEMA_GYM] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBSISTEMA_GYM].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBSISTEMA_GYM] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET  MULTI_USER 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBSISTEMA_GYM] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DBSISTEMA_GYM] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DBSISTEMA_GYM] SET QUERY_STORE = ON
GO
ALTER DATABASE [DBSISTEMA_GYM] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DBSISTEMA_GYM]
GO
/****** Object:  UserDefinedTableType [dbo].[ETabla_Grupos]    Script Date: 31/7/2025 13:19:21 ******/
CREATE TYPE [dbo].[ETabla_Grupos] AS TABLE(
	[IdGrupo] [int] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[ETabla_Permisos]    Script Date: 31/7/2025 13:19:21 ******/
CREATE TYPE [dbo].[ETabla_Permisos] AS TABLE(
	[TipoPermiso] [varchar](10) NULL,
	[IdGrupo] [int] NULL,
	[IdAccion] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[ETabla_Rutinas]    Script Date: 31/7/2025 13:19:21 ******/
CREATE TYPE [dbo].[ETabla_Rutinas] AS TABLE(
	[FechaModificacion] [date] NOT NULL,
	[Dia] [varchar](20) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[TipoPermiso]    Script Date: 31/7/2025 13:19:21 ******/
CREATE TYPE [dbo].[TipoPermiso] AS TABLE(
	[IdGrupo] [int] NULL,
	[IdAccion] [int] NULL,
	[IdUsuario] [int] NULL
)
GO
/****** Object:  Table [dbo].[Accion]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accion](
	[IdAccion] [int] IDENTITY(1,1) NOT NULL,
	[NombreAccion] [varchar](100) NOT NULL,
	[Descripcion] [varchar](255) NULL,
	[IdGrupo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuditoriaAccesos]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditoriaAccesos](
	[IdAuditoria] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[FechaHora] [datetime] NULL,
	[TipoEvento] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAuditoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuditoriaTurno]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditoriaTurno](
	[IdAuditoria] [int] IDENTITY(1,1) NOT NULL,
	[IdTurno] [int] NULL,
	[IdUsuario] [int] NULL,
	[FechaHora] [datetime] NULL,
	[Accion] [varchar](20) NULL,
	[DatosOriginales] [nvarchar](max) NULL,
	[DatosNuevos] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAuditoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Calentamiento]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Calentamiento](
	[IdCalentamiento] [int] IDENTITY(1,1) NOT NULL,
	[IdMaquina] [int] NULL,
	[DescripcionCalentamiento] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCalentamiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CupoFecha]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CupoFecha](
	[IdCupoFecha] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [date] NOT NULL,
	[IdRangoHorario] [int] NOT NULL,
	[CupoActual] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCupoFecha] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ejercicio]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ejercicio](
	[IdElemento] [int] NOT NULL,
	[Descripcion] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdElemento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ElementoGimnasio]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ElementoGimnasio](
	[IdElemento] [int] IDENTITY(1,1) NOT NULL,
	[NombreElemento] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdElemento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Entrenamiento]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entrenamiento](
	[IdEntrenamiento] [int] IDENTITY(1,1) NOT NULL,
	[IdRutina] [int] NOT NULL,
	[Series] [int] NOT NULL,
	[Repeticiones] [int] NOT NULL,
	[IdElementoGimnasio] [int] NULL,
	[Peso] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEntrenamiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Equipamiento]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipamiento](
	[IdElemento] [int] NOT NULL,
	[Precio] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdElemento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estiramiento]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estiramiento](
	[IdEstiramiento] [int] IDENTITY(1,1) NOT NULL,
	[DescripcionEstiramiento] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEstiramiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gimnasio]    Script Date: 31/7/2025 13:19:21 ******/
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
	[HoraAperturaLaV] [time](7) NULL,
	[HoraCierreLaV] [time](7) NULL,
	[HoraAperturaSabado] [time](7) NULL,
	[HoraCierreSabado] [time](7) NULL,
	[Email] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdGimnasio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Grupo]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grupo](
	[IdGrupo] [int] IDENTITY(1,1) NOT NULL,
	[NombreMenu] [varchar](100) NOT NULL,
	[Descripcion] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdGrupo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Historial_Calentamiento]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Historial_Calentamiento](
	[IdHistorialCalentamiento] [int] IDENTITY(1,1) NOT NULL,
	[IdHistorial] [int] NULL,
	[IdCalentamiento] [int] NOT NULL,
	[Duracion] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdHistorialCalentamiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Historial_Entrenamiento]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Historial_Entrenamiento](
	[IdHistorialEntrenamiento] [int] IDENTITY(1,1) NOT NULL,
	[IdHistorial] [int] NULL,
	[IdElementoGimnasio] [int] NOT NULL,
	[Series] [int] NULL,
	[Repeticiones] [int] NULL,
	[Peso] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdHistorialEntrenamiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Historial_Estiramiento]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Historial_Estiramiento](
	[IdHistorialEstiramiento] [int] IDENTITY(1,1) NOT NULL,
	[IdHistorial] [int] NULL,
	[IdEstiramiento] [int] NOT NULL,
	[Duracion] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdHistorialEstiramiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistorialRutina]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistorialRutina](
	[IdHistorial] [int] IDENTITY(1,1) NOT NULL,
	[IdSocio] [int] NOT NULL,
	[Dia] [nvarchar](20) NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdHistorial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistorialRutinas]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistorialRutinas](
	[IdHistorialRutinas] [int] NOT NULL,
	[UltimaFecha] [date] NOT NULL,
	[UltimaHora] [time](7) NOT NULL,
 CONSTRAINT [PK_HistorialRutinas] PRIMARY KEY CLUSTERED 
(
	[IdHistorialRutinas] ASC,
	[UltimaFecha] ASC,
	[UltimaHora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Maquina]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Maquina](
	[IdElemento] [int] NOT NULL,
	[FechaFabricacion] [date] NOT NULL,
	[FechaCompra] [date] NOT NULL,
	[Precio] [float] NOT NULL,
	[Peso] [int] NOT NULL,
	[EsElectrica] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdElemento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permiso]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permiso](
	[IdPermiso] [int] IDENTITY(1,1) NOT NULL,
	[IdRol] [int] NULL,
	[FechaRegistro] [datetime] NULL,
	[IdGrupo] [int] NULL,
	[IdAccion] [int] NULL,
	[IdUsuario] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPermiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RangoHorario]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RangoHorario](
	[IdRangoHorario] [int] IDENTITY(1,1) NOT NULL,
	[HoraDesde] [time](7) NULL,
	[HoraHasta] [time](7) NULL,
	[Fecha] [date] NULL,
	[CupoActual] [int] NULL,
	[CupoMaximo] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
	[SoloSabado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRangoHorario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RangoHorario_Usuario]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RangoHorario_Usuario](
	[IdRangoHorario] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRangoHorario] ASC,
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 31/7/2025 13:19:21 ******/
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
/****** Object:  Table [dbo].[Rutina]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rutina](
	[IdRutina] [int] IDENTITY(1,1) NOT NULL,
	[IdSocio] [int] NOT NULL,
	[FechaModificacion] [date] NOT NULL,
	[Dia] [varchar](20) NOT NULL,
	[Activa] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRutina] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rutina_Calentamiento]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rutina_Calentamiento](
	[IdRutina] [int] NOT NULL,
	[IdCalentamiento] [int] NOT NULL,
	[Duracion] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRutina] ASC,
	[IdCalentamiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rutina_Estiramiento]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rutina_Estiramiento](
	[IdRutina] [int] NOT NULL,
	[IdEstiramiento] [int] NOT NULL,
	[Duracion] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRutina] ASC,
	[IdEstiramiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Socio]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Socio](
	[IdSocio] [int] IDENTITY(1,1) NOT NULL,
	[NombreYApellido] [varchar](100) NOT NULL,
	[FechaNacimiento] [date] NOT NULL,
	[Genero] [varchar](50) NOT NULL,
	[NroDocumento] [int] NOT NULL,
	[Ciudad] [varchar](50) NOT NULL,
	[Direccion] [varchar](50) NOT NULL,
	[Telefono] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[ObraSocial] [varchar](50) NULL,
	[Plan] [varchar](50) NULL,
	[EstadoSocio] [varchar](50) NOT NULL,
	[FechaInicioActividades] [date] NULL,
	[FechaFinActividades] [date] NULL,
	[FechaNotificacion] [date] NULL,
	[RespuestaNotificacion] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdSocio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Turno]    Script Date: 31/7/2025 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Turno](
	[IdTurno] [int] IDENTITY(1,1) NOT NULL,
	[IdRangoHorario] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[IdSocio] [int] NOT NULL,
	[FechaTurno] [date] NOT NULL,
	[EstadoTurno] [varchar](50) NOT NULL,
	[CodigoIngreso] [varchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTurno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 31/7/2025 13:19:21 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accion] ON 

INSERT [dbo].[Accion] ([IdAccion], [NombreAccion], [Descripcion], [IdGrupo]) VALUES (1, N'menuUsuarios', N'Administrar usuarios del gimnasio, darles de alta o baja, y asignar permisos personalizados.', 1)
INSERT [dbo].[Accion] ([IdAccion], [NombreAccion], [Descripcion], [IdGrupo]) VALUES (2, N'menuRoles', N'Asignar, modificar o consultar los roles y permisos asignados a los usuarios del sistema.', 1)
INSERT [dbo].[Accion] ([IdAccion], [NombreAccion], [Descripcion], [IdGrupo]) VALUES (3, N'menuCalentamiento', N'Gestionar los ejercicios de calentamiento, con o sin uso de máquinas de tipo cardio.', 1)
INSERT [dbo].[Accion] ([IdAccion], [NombreAccion], [Descripcion], [IdGrupo]) VALUES (4, N'menuElementosGym', N'Registrar, editar y eliminar elementos del gimnasio como máquinas, pesas o accesorios utilizados en los entrenamientos.', 1)
INSERT [dbo].[Accion] ([IdAccion], [NombreAccion], [Descripcion], [IdGrupo]) VALUES (5, N'menuEstiramiento', N'Administrar técnicas de estiramiento que pueden formar parte de una rutina para los socios.', 1)
INSERT [dbo].[Accion] ([IdAccion], [NombreAccion], [Descripcion], [IdGrupo]) VALUES (6, N'menuRangosHorarios', N'Configurar los diferentes rangos horarios en los que se puede entrenar y asignarlos a entrenadores y socios.', 1)
INSERT [dbo].[Accion] ([IdAccion], [NombreAccion], [Descripcion], [IdGrupo]) VALUES (7, N'menuNegocio', N'Actualizar los datos de la empresa, como razón social, dirección, nombre del gimnasio y horarios de atención.', 1)
INSERT [dbo].[Accion] ([IdAccion], [NombreAccion], [Descripcion], [IdGrupo]) VALUES (8, N'btnMenuAgregar', N'Dar de alta un nuevo socio en el sistema, registrando todos sus datos personales y de contacto.', 3)
INSERT [dbo].[Accion] ([IdAccion], [NombreAccion], [Descripcion], [IdGrupo]) VALUES (9, N'btnMenuConsultar', N'Consultar información personal del socio, incluyendo rutina activa, historial y estado de cuota.', 3)
INSERT [dbo].[Accion] ([IdAccion], [NombreAccion], [Descripcion], [IdGrupo]) VALUES (10, N'btnMenuEliminar', N'Eliminar un socio del sistema y mover sus datos a la base de datos histórica o de respaldo.', 3)
INSERT [dbo].[Accion] ([IdAccion], [NombreAccion], [Descripcion], [IdGrupo]) VALUES (11, N'btnMenuTurno', N'Visualizar y gestionar los turnos asignados a un socio, incluyendo altas, bajas y reasignaciones.', 3)
INSERT [dbo].[Accion] ([IdAccion], [NombreAccion], [Descripcion], [IdGrupo]) VALUES (12, N'btnEliminar', N'Dar de baja una rutina activa y eliminar los datos de esa jornada para un socio específico.', 2)
INSERT [dbo].[Accion] ([IdAccion], [NombreAccion], [Descripcion], [IdGrupo]) VALUES (13, N'btnHistorial', N'Consultar el historial completo de rutinas realizadas por un socio para su seguimiento y evolución.', 2)
INSERT [dbo].[Accion] ([IdAccion], [NombreAccion], [Descripcion], [IdGrupo]) VALUES (14, N'btnGuardarRutina', N'Guardar la rutina armada para un socio, incluyendo calentamiento, entrenamiento y estiramiento del día.', 2)
INSERT [dbo].[Accion] ([IdAccion], [NombreAccion], [Descripcion], [IdGrupo]) VALUES (15, N'btnRestaurar', N'Recuperar una rutina anterior cargada por el entrenador, restaurándola al estado original en una fecha específica.', 2)
SET IDENTITY_INSERT [dbo].[Accion] OFF
GO
SET IDENTITY_INSERT [dbo].[AuditoriaAccesos] ON 

INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (1, 1, CAST(N'2025-07-20T15:31:47.200' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (2, 1, CAST(N'2025-07-20T15:32:08.523' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (3, 2, CAST(N'2025-07-20T15:32:25.353' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (4, 2, CAST(N'2025-07-20T15:32:30.270' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (5, 1, CAST(N'2025-07-20T15:37:21.257' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (6, 1, CAST(N'2025-07-20T15:40:03.880' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (7, 1, CAST(N'2025-07-20T15:40:10.287' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (8, 1, CAST(N'2025-07-20T15:40:49.870' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (9, 1, CAST(N'2025-07-20T15:51:34.210' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (10, 1, CAST(N'2025-07-20T16:02:38.700' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (11, 1, CAST(N'2025-07-20T16:03:45.217' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (12, 1, CAST(N'2025-07-20T16:04:44.010' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (13, 1, CAST(N'2025-07-20T16:06:19.667' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (14, 1, CAST(N'2025-07-20T16:06:35.070' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (15, 1, CAST(N'2025-07-20T16:07:45.073' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (16, 1, CAST(N'2025-07-20T16:07:52.783' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (17, 1, CAST(N'2025-07-20T16:08:08.243' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (18, 1, CAST(N'2025-07-20T16:16:03.387' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (19, 1, CAST(N'2025-07-20T16:18:01.157' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (20, 1, CAST(N'2025-07-20T16:18:08.327' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (21, 1, CAST(N'2025-07-20T16:19:49.553' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (22, 1, CAST(N'2025-07-20T16:31:43.757' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (23, 1, CAST(N'2025-07-20T16:33:56.743' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (24, 1, CAST(N'2025-07-20T16:34:03.287' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (25, 1, CAST(N'2025-07-20T16:35:04.433' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (26, 1, CAST(N'2025-07-20T16:36:42.863' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (27, 1, CAST(N'2025-07-20T16:36:46.353' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (28, 1, CAST(N'2025-07-20T16:39:55.747' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (29, 1, CAST(N'2025-07-20T16:50:37.870' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (30, 1, CAST(N'2025-07-20T16:51:54.447' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (31, 1, CAST(N'2025-07-20T16:52:18.777' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (32, 1, CAST(N'2025-07-20T16:52:22.273' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (33, 1, CAST(N'2025-07-20T16:52:38.087' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (34, 1, CAST(N'2025-07-20T16:52:56.890' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (35, 1, CAST(N'2025-07-20T16:54:45.017' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (36, 1, CAST(N'2025-07-20T16:56:11.897' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (37, 1, CAST(N'2025-07-20T16:57:17.097' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (38, 1, CAST(N'2025-07-20T17:02:17.517' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (39, 1, CAST(N'2025-07-20T17:03:51.133' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (40, 1, CAST(N'2025-07-20T18:29:03.137' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (41, 1, CAST(N'2025-07-20T18:46:34.457' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (42, 1, CAST(N'2025-07-20T18:46:38.833' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (43, 9, CAST(N'2025-07-20T18:46:42.650' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (44, 9, CAST(N'2025-07-20T18:47:54.760' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (45, 9, CAST(N'2025-07-20T18:48:13.663' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (46, 9, CAST(N'2025-07-20T18:48:34.540' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (47, 1, CAST(N'2025-07-20T18:48:39.690' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (48, 1, CAST(N'2025-07-20T18:50:13.927' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (49, 1, CAST(N'2025-07-20T18:50:20.837' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (50, 1, CAST(N'2025-07-20T18:50:27.433' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (51, 1, CAST(N'2025-07-20T18:50:33.467' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (52, 1, CAST(N'2025-07-20T18:53:37.173' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (53, 15, CAST(N'2025-07-20T18:53:42.237' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (54, 15, CAST(N'2025-07-20T19:00:34.223' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (55, 1, CAST(N'2025-07-20T19:00:37.837' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (56, 1, CAST(N'2025-07-20T19:00:56.020' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (57, 15, CAST(N'2025-07-20T19:01:02.460' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (58, 15, CAST(N'2025-07-20T19:52:06.803' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (59, 1, CAST(N'2025-07-20T19:52:10.943' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (60, 1, CAST(N'2025-07-20T19:52:24.053' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (61, 15, CAST(N'2025-07-20T19:52:28.527' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (62, 15, CAST(N'2025-07-20T19:52:35.467' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (63, 9, CAST(N'2025-07-20T19:52:42.340' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (64, 9, CAST(N'2025-07-20T19:52:47.887' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (65, 1, CAST(N'2025-07-20T19:57:59.530' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (66, 1, CAST(N'2025-07-20T19:58:09.933' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (67, 9, CAST(N'2025-07-20T19:58:14.110' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (68, 9, CAST(N'2025-07-20T20:00:53.073' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (69, 9, CAST(N'2025-07-20T20:04:22.143' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (70, 1, CAST(N'2025-07-20T20:05:15.740' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (71, 1, CAST(N'2025-07-20T20:05:31.457' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (72, 1, CAST(N'2025-07-20T20:05:34.480' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (73, 15, CAST(N'2025-07-20T20:05:40.760' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (74, 15, CAST(N'2025-07-20T20:05:58.630' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (75, 9, CAST(N'2025-07-20T20:06:05.420' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (76, 9, CAST(N'2025-07-20T20:07:44.260' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (77, 1, CAST(N'2025-07-20T20:07:50.537' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (78, 1, CAST(N'2025-07-20T20:07:54.597' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (79, 9, CAST(N'2025-07-20T20:08:00.573' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (80, 9, CAST(N'2025-07-20T20:08:06.053' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (81, 1, CAST(N'2025-07-20T20:16:30.057' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (82, 1, CAST(N'2025-07-21T13:12:17.327' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (83, 1, CAST(N'2025-07-21T13:50:18.617' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (84, 1, CAST(N'2025-07-21T13:50:45.000' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (85, 9, CAST(N'2025-07-21T13:50:48.350' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (86, 9, CAST(N'2025-07-21T13:52:27.547' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (87, 1, CAST(N'2025-07-21T13:52:30.243' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (88, 1, CAST(N'2025-07-21T13:53:12.560' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (89, 15, CAST(N'2025-07-21T13:53:17.973' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (90, 1, CAST(N'2025-07-21T13:54:26.670' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (91, 1, CAST(N'2025-07-21T13:54:29.067' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (92, 1, CAST(N'2025-07-21T14:05:23.640' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (93, 1, CAST(N'2025-07-21T14:22:15.360' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (94, 1, CAST(N'2025-07-21T14:22:23.717' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (95, 1, CAST(N'2025-07-21T14:25:55.153' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (96, 1, CAST(N'2025-07-21T14:26:22.817' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (97, 1, CAST(N'2025-07-21T14:32:08.870' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (98, 1, CAST(N'2025-07-21T15:15:35.283' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (99, 1, CAST(N'2025-07-21T15:46:22.200' AS DateTime), N'Login')
GO
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (100, 1, CAST(N'2025-07-21T16:33:01.437' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (101, 1, CAST(N'2025-07-21T16:33:08.740' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (102, 1, CAST(N'2025-07-21T16:34:53.263' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (103, 1, CAST(N'2025-07-21T16:43:51.893' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (104, 1, CAST(N'2025-07-21T17:03:45.133' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (105, 1, CAST(N'2025-07-21T17:03:50.910' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (106, 1, CAST(N'2025-07-21T17:35:37.780' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (107, 1, CAST(N'2025-07-21T17:36:27.673' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (108, 1, CAST(N'2025-07-21T18:08:36.793' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (109, 1, CAST(N'2025-07-21T18:13:52.790' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (110, 1, CAST(N'2025-07-21T18:13:59.237' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (111, 1, CAST(N'2025-07-21T18:32:29.263' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (112, 1, CAST(N'2025-07-21T18:32:52.247' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (113, 1, CAST(N'2025-07-21T18:34:09.257' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (114, 1, CAST(N'2025-07-21T18:35:00.523' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (115, 1, CAST(N'2025-07-21T18:35:31.173' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (116, 1, CAST(N'2025-07-21T18:39:32.797' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (117, 1, CAST(N'2025-07-21T18:40:15.727' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (118, 1, CAST(N'2025-07-21T18:42:40.807' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (119, 1, CAST(N'2025-07-21T18:47:55.850' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (120, 1, CAST(N'2025-07-21T18:52:10.253' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (121, 1, CAST(N'2025-07-21T18:52:20.993' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (122, 9, CAST(N'2025-07-21T18:52:26.457' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (123, 9, CAST(N'2025-07-21T18:53:02.713' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (124, 16, CAST(N'2025-07-21T18:53:29.183' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (125, 16, CAST(N'2025-07-21T18:55:03.887' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (126, 3, CAST(N'2025-07-21T18:55:08.570' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (127, 3, CAST(N'2025-07-21T18:55:33.063' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (128, 1, CAST(N'2025-07-21T18:55:36.823' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (129, 1, CAST(N'2025-07-21T18:56:04.140' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (130, 9, CAST(N'2025-07-21T18:56:12.940' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (131, 9, CAST(N'2025-07-21T18:56:59.660' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (132, 9, CAST(N'2025-07-21T18:57:07.483' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (133, 9, CAST(N'2025-07-21T18:57:10.007' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (134, 16, CAST(N'2025-07-21T18:57:13.360' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (135, 16, CAST(N'2025-07-21T18:58:10.053' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (136, 1, CAST(N'2025-07-21T18:58:13.107' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (137, 1, CAST(N'2025-07-21T19:11:43.403' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (138, 17, CAST(N'2025-07-21T19:11:46.827' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (139, 17, CAST(N'2025-07-21T19:12:13.247' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (140, 1, CAST(N'2025-07-21T19:12:16.593' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (141, 1, CAST(N'2025-07-21T19:37:25.883' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (142, 1, CAST(N'2025-07-21T19:37:33.613' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (143, 1, CAST(N'2025-07-21T19:38:47.960' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (144, 16, CAST(N'2025-07-21T19:38:51.563' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (145, 16, CAST(N'2025-07-21T19:42:03.530' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (146, 16, CAST(N'2025-07-21T19:42:52.983' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (147, 17, CAST(N'2025-07-21T19:42:55.760' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (148, 17, CAST(N'2025-07-21T19:46:48.720' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (149, 17, CAST(N'2025-07-21T19:48:41.090' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (150, 17, CAST(N'2025-07-21T19:49:20.157' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (151, 16, CAST(N'2025-07-21T19:49:24.360' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (152, 16, CAST(N'2025-07-21T19:51:52.510' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (153, 1, CAST(N'2025-07-21T19:51:55.677' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (154, 1, CAST(N'2025-07-21T19:53:15.477' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (155, 16, CAST(N'2025-07-21T19:53:21.713' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (156, 16, CAST(N'2025-07-21T19:54:32.760' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (157, 1, CAST(N'2025-07-21T19:54:42.990' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (158, 1, CAST(N'2025-07-21T19:56:12.220' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (159, 16, CAST(N'2025-07-21T19:56:15.173' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (160, 16, CAST(N'2025-07-21T19:56:33.823' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (161, 1, CAST(N'2025-07-21T19:56:36.480' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (162, 1, CAST(N'2025-07-21T20:01:27.680' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (163, 1, CAST(N'2025-07-21T20:02:03.523' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (164, 1, CAST(N'2025-07-21T20:07:04.583' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (165, 1, CAST(N'2025-07-21T20:28:36.070' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (166, 1, CAST(N'2025-07-21T20:28:41.923' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (167, 1, CAST(N'2025-07-24T12:35:21.990' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (168, 1, CAST(N'2025-07-24T12:39:03.790' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (169, 9, CAST(N'2025-07-24T12:39:10.020' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (170, 9, CAST(N'2025-07-24T12:39:25.900' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (171, 1, CAST(N'2025-07-27T13:44:12.147' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (172, 1, CAST(N'2025-07-27T13:58:51.263' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (173, 1, CAST(N'2025-07-27T13:59:00.353' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (174, 1, CAST(N'2025-07-27T14:17:26.510' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (175, 1, CAST(N'2025-07-27T14:17:33.013' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (176, 1, CAST(N'2025-07-27T14:19:40.057' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (177, 1, CAST(N'2025-07-27T14:19:47.517' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (178, 1, CAST(N'2025-07-27T14:24:12.473' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (179, 1, CAST(N'2025-07-27T14:24:50.310' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (180, 1, CAST(N'2025-07-27T14:24:55.797' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (181, 1, CAST(N'2025-07-27T14:28:15.980' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (182, 1, CAST(N'2025-07-27T14:29:34.853' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (183, 1, CAST(N'2025-07-27T14:29:43.117' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (184, 1, CAST(N'2025-07-27T14:32:27.043' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (185, 1, CAST(N'2025-07-27T14:33:12.667' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (186, 1, CAST(N'2025-07-27T14:34:53.277' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (187, 1, CAST(N'2025-07-27T14:42:13.490' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (188, 18, CAST(N'2025-07-27T14:42:17.350' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (189, 18, CAST(N'2025-07-27T14:42:39.567' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (190, 1, CAST(N'2025-07-27T14:42:43.203' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (191, 1, CAST(N'2025-07-27T15:10:17.613' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (192, 1, CAST(N'2025-07-27T15:43:43.233' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (193, 1, CAST(N'2025-07-27T15:43:52.550' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (194, 1, CAST(N'2025-07-27T16:04:51.600' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (195, 1, CAST(N'2025-07-27T16:04:58.687' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (196, 1, CAST(N'2025-07-27T16:07:51.757' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (197, 1, CAST(N'2025-07-27T16:07:54.937' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (198, 1, CAST(N'2025-07-27T16:08:39.840' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (199, 1, CAST(N'2025-07-27T16:09:08.433' AS DateTime), N'Login')
GO
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (200, 1, CAST(N'2025-07-27T16:15:37.757' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (201, 1, CAST(N'2025-07-27T16:15:44.723' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (202, 1, CAST(N'2025-07-27T16:19:34.850' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (203, 1, CAST(N'2025-07-27T16:19:37.463' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (204, 1, CAST(N'2025-07-27T16:19:39.667' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (205, 18, CAST(N'2025-07-27T16:19:46.103' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (206, 18, CAST(N'2025-07-27T16:20:21.250' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (207, 18, CAST(N'2025-07-27T16:20:28.870' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (208, 18, CAST(N'2025-07-27T16:21:02.797' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (209, 18, CAST(N'2025-07-27T16:21:14.753' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (210, 18, CAST(N'2025-07-27T16:21:55.350' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (211, 1, CAST(N'2025-07-27T16:21:57.990' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (212, 1, CAST(N'2025-07-27T16:22:00.400' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (213, 18, CAST(N'2025-07-27T16:22:03.540' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (214, 18, CAST(N'2025-07-27T16:22:12.670' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (215, 1, CAST(N'2025-07-27T16:22:14.880' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (216, 1, CAST(N'2025-07-27T16:23:25.770' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (217, 18, CAST(N'2025-07-27T16:23:29.663' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (218, 18, CAST(N'2025-07-27T16:23:35.920' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (219, 1, CAST(N'2025-07-27T16:23:39.117' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (220, 1, CAST(N'2025-07-27T16:24:08.070' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (221, 1, CAST(N'2025-07-27T16:24:11.737' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (222, 1, CAST(N'2025-07-27T16:24:17.137' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (223, 18, CAST(N'2025-07-27T16:24:21.200' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (224, 18, CAST(N'2025-07-27T16:24:56.873' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (225, 1, CAST(N'2025-07-27T17:48:07.097' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (226, 1, CAST(N'2025-07-27T18:01:01.147' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (227, 19, CAST(N'2025-07-27T18:01:04.917' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (228, 19, CAST(N'2025-07-27T18:02:13.773' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (229, 1, CAST(N'2025-07-27T18:02:17.087' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (230, 1, CAST(N'2025-07-27T18:03:12.670' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (231, 19, CAST(N'2025-07-27T18:03:16.860' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (232, 19, CAST(N'2025-07-27T18:03:24.867' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (233, 1, CAST(N'2025-07-27T18:03:28.043' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (234, 1, CAST(N'2025-07-27T18:04:27.157' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (235, 19, CAST(N'2025-07-27T18:04:31.277' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (236, 19, CAST(N'2025-07-27T18:07:25.487' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (237, 1, CAST(N'2025-07-27T18:07:28.397' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (238, 1, CAST(N'2025-07-27T18:16:53.897' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (239, 1, CAST(N'2025-07-27T20:33:33.117' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (240, 1, CAST(N'2025-07-27T20:34:08.270' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (241, 1, CAST(N'2025-07-27T20:34:08.273' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (242, 1, CAST(N'2025-07-27T20:34:15.787' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (243, 1, CAST(N'2025-07-27T20:34:56.617' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (244, 1, CAST(N'2025-07-27T20:34:56.620' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (245, 1, CAST(N'2025-07-27T20:35:00.313' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (246, 1, CAST(N'2025-07-27T20:35:52.523' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (247, 1, CAST(N'2025-07-27T20:35:59.120' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (248, 1, CAST(N'2025-07-27T20:36:08.707' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (249, 1, CAST(N'2025-07-27T20:36:11.583' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (250, 1, CAST(N'2025-07-27T20:36:25.647' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (251, 1, CAST(N'2025-07-27T20:38:34.837' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (252, 1, CAST(N'2025-07-27T20:38:46.630' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (253, 1, CAST(N'2025-07-27T20:38:50.183' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (254, 1, CAST(N'2025-07-27T20:39:01.840' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (255, 1, CAST(N'2025-07-27T20:39:05.000' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (256, 1, CAST(N'2025-07-27T20:42:14.267' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (257, 1, CAST(N'2025-07-28T12:45:31.160' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (258, 1, CAST(N'2025-07-28T12:49:32.740' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (259, 1, CAST(N'2025-07-28T12:51:04.490' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (260, 1, CAST(N'2025-07-28T12:52:00.350' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (261, 1, CAST(N'2025-07-28T18:22:51.780' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (262, 1, CAST(N'2025-07-28T18:53:01.740' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (263, 1, CAST(N'2025-07-28T18:53:21.660' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (264, 1, CAST(N'2025-07-28T18:54:42.080' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (265, 1, CAST(N'2025-07-28T18:55:08.867' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (266, 1, CAST(N'2025-07-28T18:55:26.153' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (267, 1, CAST(N'2025-07-28T19:02:19.980' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (268, 1, CAST(N'2025-07-28T19:02:25.703' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (269, 1, CAST(N'2025-07-28T19:03:43.190' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (270, 1, CAST(N'2025-07-28T19:07:03.267' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (271, 1, CAST(N'2025-07-28T19:07:11.280' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (272, 1, CAST(N'2025-07-28T19:16:26.047' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (273, 1, CAST(N'2025-07-28T19:16:32.560' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (274, 1, CAST(N'2025-07-28T19:27:01.893' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (275, 1, CAST(N'2025-07-28T23:21:11.120' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (276, 1, CAST(N'2025-07-28T23:21:51.320' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (277, 1, CAST(N'2025-07-28T23:32:23.590' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (278, 1, CAST(N'2025-07-28T23:32:29.317' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (279, 1, CAST(N'2025-07-28T23:35:50.353' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (280, 1, CAST(N'2025-07-28T23:35:57.170' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (281, 1, CAST(N'2025-07-28T23:54:26.943' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (282, 1, CAST(N'2025-07-28T23:54:35.250' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (283, 1, CAST(N'2025-07-28T23:59:32.767' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (284, 1, CAST(N'2025-07-28T23:59:39.390' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (285, 1, CAST(N'2025-07-29T00:03:04.123' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (286, 1, CAST(N'2025-07-29T00:04:52.597' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (287, 1, CAST(N'2025-07-29T00:11:54.230' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (288, 1, CAST(N'2025-07-29T00:12:02.653' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (289, 1, CAST(N'2025-07-29T00:18:27.123' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (290, 1, CAST(N'2025-07-29T15:03:45.137' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (291, 1, CAST(N'2025-07-29T15:11:13.240' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (292, 1, CAST(N'2025-07-29T15:11:20.667' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (293, 1, CAST(N'2025-07-29T15:17:13.320' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (294, 1, CAST(N'2025-07-29T15:17:48.513' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (295, 1, CAST(N'2025-07-29T15:35:02.343' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (296, 1, CAST(N'2025-07-29T15:35:17.290' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (297, 1, CAST(N'2025-07-29T15:35:46.610' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (298, 1, CAST(N'2025-07-29T15:42:22.187' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (299, 1, CAST(N'2025-07-29T15:43:38.103' AS DateTime), N'Logout')
GO
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (300, 1, CAST(N'2025-07-29T15:43:44.197' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (301, 1, CAST(N'2025-07-29T17:52:10.967' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (302, 19, CAST(N'2025-07-29T17:52:14.407' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (303, 19, CAST(N'2025-07-29T17:53:37.243' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (304, 13, CAST(N'2025-07-29T17:53:45.580' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (305, 13, CAST(N'2025-07-29T17:54:20.813' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (306, 1, CAST(N'2025-07-29T17:54:24.440' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (307, 1, CAST(N'2025-07-29T17:54:37.903' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (308, 13, CAST(N'2025-07-29T17:54:42.227' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (309, 13, CAST(N'2025-07-29T17:56:22.947' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (310, 18, CAST(N'2025-07-29T17:56:25.997' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (311, 18, CAST(N'2025-07-29T17:59:12.420' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (312, 1, CAST(N'2025-07-29T17:59:16.187' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (313, 1, CAST(N'2025-07-29T17:59:19.357' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (314, 18, CAST(N'2025-07-29T17:59:23.013' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (315, 18, CAST(N'2025-07-29T18:00:45.113' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (316, 19, CAST(N'2025-07-29T18:00:48.040' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (317, 19, CAST(N'2025-07-29T18:04:09.083' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (318, 1, CAST(N'2025-07-29T18:04:12.567' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (319, 1, CAST(N'2025-07-29T18:04:51.143' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (320, 19, CAST(N'2025-07-29T18:04:54.733' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (321, 19, CAST(N'2025-07-29T18:07:20.833' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (322, 1, CAST(N'2025-07-29T18:07:23.620' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (323, 1, CAST(N'2025-07-29T18:08:19.447' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (324, 19, CAST(N'2025-07-29T18:08:22.813' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (325, 19, CAST(N'2025-07-29T18:09:32.240' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (326, 19, CAST(N'2025-07-29T18:09:35.277' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (327, 19, CAST(N'2025-07-29T18:10:29.640' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (328, 18, CAST(N'2025-07-29T18:10:46.883' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (329, 18, CAST(N'2025-07-29T18:12:11.513' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (330, 1, CAST(N'2025-07-29T18:12:15.160' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (331, 1, CAST(N'2025-07-29T18:14:48.183' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (332, 18, CAST(N'2025-07-29T18:14:53.097' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (333, 18, CAST(N'2025-07-29T18:15:09.460' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (334, 1, CAST(N'2025-07-29T18:15:12.670' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (335, 1, CAST(N'2025-07-29T18:15:50.287' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (336, 1, CAST(N'2025-07-29T18:20:56.647' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (337, 1, CAST(N'2025-07-29T18:20:58.790' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (338, 9, CAST(N'2025-07-29T18:21:14.133' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (339, 9, CAST(N'2025-07-29T18:21:25.500' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (340, 18, CAST(N'2025-07-29T18:21:29.173' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (341, 18, CAST(N'2025-07-29T18:21:46.903' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (342, 1, CAST(N'2025-07-29T18:21:49.260' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (343, 1, CAST(N'2025-07-29T18:22:05.500' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (344, 1, CAST(N'2025-07-29T18:23:24.963' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (345, 1, CAST(N'2025-07-29T18:23:33.680' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (346, 1, CAST(N'2025-07-29T19:05:36.097' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (347, 1, CAST(N'2025-07-29T19:12:17.367' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (348, 1, CAST(N'2025-07-29T19:16:52.120' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (349, 1, CAST(N'2025-07-29T19:48:37.447' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (350, 1, CAST(N'2025-07-29T19:56:58.840' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (351, 1, CAST(N'2025-07-29T19:58:07.030' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (352, 19, CAST(N'2025-07-29T19:58:10.727' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (353, 19, CAST(N'2025-07-29T19:58:45.387' AS DateTime), N'Logout')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (354, 1, CAST(N'2025-07-29T19:58:48.603' AS DateTime), N'Login')
INSERT [dbo].[AuditoriaAccesos] ([IdAuditoria], [IdUsuario], [FechaHora], [TipoEvento]) VALUES (355, 1, CAST(N'2025-07-29T20:02:42.160' AS DateTime), N'Logout')
SET IDENTITY_INSERT [dbo].[AuditoriaAccesos] OFF
GO
SET IDENTITY_INSERT [dbo].[AuditoriaTurno] ON 

INSERT [dbo].[AuditoriaTurno] ([IdAuditoria], [IdTurno], [IdUsuario], [FechaHora], [Accion], [DatosOriginales], [DatosNuevos]) VALUES (1, 117, 3, CAST(N'2025-07-20T13:26:28.313' AS DateTime), N'Insertar', NULL, N'IdRangoHorario: 14, IdSocio: 19, Fecha: 2025-07-22, Estado: En Curso, Codigo: 0I6K')
INSERT [dbo].[AuditoriaTurno] ([IdAuditoria], [IdTurno], [IdUsuario], [FechaHora], [Accion], [DatosOriginales], [DatosNuevos]) VALUES (2, 117, 3, CAST(N'2025-07-20T13:27:30.737' AS DateTime), N'Modificar', N'IdRangoHorario: 14, IdSocio: 19, Fecha: 2025-07-22, Estado: En Curso, Codigo: 0I6K', N'IdRangoHorario: 14, IdSocio: 19, Fecha: 2025-07-22, Estado: Cancelado, Codigo: 0I6K')
INSERT [dbo].[AuditoriaTurno] ([IdAuditoria], [IdTurno], [IdUsuario], [FechaHora], [Accion], [DatosOriginales], [DatosNuevos]) VALUES (3, 117, 3, CAST(N'2025-07-20T13:27:59.357' AS DateTime), N'Eliminar', N'IdRangoHorario: 14, IdSocio: 19, Fecha: 2025-07-22, Estado: Cancelado, Codigo: 0I6K', NULL)
INSERT [dbo].[AuditoriaTurno] ([IdAuditoria], [IdTurno], [IdUsuario], [FechaHora], [Accion], [DatosOriginales], [DatosNuevos]) VALUES (4, 118, 10, CAST(N'2025-07-21T17:25:20.403' AS DateTime), N'Insertar', NULL, N'IdRangoHorario: 18, IdSocio: 19, Fecha: 2025-07-21, Estado: En Curso, Codigo: Q34T')
INSERT [dbo].[AuditoriaTurno] ([IdAuditoria], [IdTurno], [IdUsuario], [FechaHora], [Accion], [DatosOriginales], [DatosNuevos]) VALUES (5, 111, 3, CAST(N'2025-07-21T19:38:17.313' AS DateTime), N'Eliminar', N'IdRangoHorario: 21, IdSocio: 11, Fecha: 2025-07-21, Estado: Cancelado, Codigo: GP87', NULL)
INSERT [dbo].[AuditoriaTurno] ([IdAuditoria], [IdTurno], [IdUsuario], [FechaHora], [Accion], [DatosOriginales], [DatosNuevos]) VALUES (6, 119, 3, CAST(N'2025-07-21T19:38:20.680' AS DateTime), N'Insertar', NULL, N'IdRangoHorario: 20, IdSocio: 11, Fecha: 2025-07-21, Estado: En Curso, Codigo: WTVS')
INSERT [dbo].[AuditoriaTurno] ([IdAuditoria], [IdTurno], [IdUsuario], [FechaHora], [Accion], [DatosOriginales], [DatosNuevos]) VALUES (7, 119, 3, CAST(N'2025-07-24T12:35:22.167' AS DateTime), N'Modificar', N'IdRangoHorario: 20, IdSocio: 11, Fecha: 2025-07-21, Estado: En Curso, Codigo: WTVS', N'IdRangoHorario: 20, IdSocio: 11, Fecha: 2025-07-21, Estado: Cancelado, Codigo: WTVS')
INSERT [dbo].[AuditoriaTurno] ([IdAuditoria], [IdTurno], [IdUsuario], [FechaHora], [Accion], [DatosOriginales], [DatosNuevos]) VALUES (8, 118, 10, CAST(N'2025-07-24T12:35:22.170' AS DateTime), N'Modificar', N'IdRangoHorario: 18, IdSocio: 19, Fecha: 2025-07-21, Estado: En Curso, Codigo: Q34T', N'IdRangoHorario: 18, IdSocio: 19, Fecha: 2025-07-21, Estado: Cancelado, Codigo: Q34T')
INSERT [dbo].[AuditoriaTurno] ([IdAuditoria], [IdTurno], [IdUsuario], [FechaHora], [Accion], [DatosOriginales], [DatosNuevos]) VALUES (9, 120, 3, CAST(N'2025-07-24T12:35:50.277' AS DateTime), N'Insertar', NULL, N'IdRangoHorario: 13, IdSocio: 11, Fecha: 2025-07-24, Estado: En Curso, Codigo: 97IL')
INSERT [dbo].[AuditoriaTurno] ([IdAuditoria], [IdTurno], [IdUsuario], [FechaHora], [Accion], [DatosOriginales], [DatosNuevos]) VALUES (10, 120, 3, CAST(N'2025-07-27T13:44:12.283' AS DateTime), N'Modificar', N'IdRangoHorario: 13, IdSocio: 11, Fecha: 2025-07-24, Estado: En Curso, Codigo: 97IL', N'IdRangoHorario: 13, IdSocio: 11, Fecha: 2025-07-24, Estado: Cancelado, Codigo: 97IL')
SET IDENTITY_INSERT [dbo].[AuditoriaTurno] OFF
GO
SET IDENTITY_INSERT [dbo].[Calentamiento] ON 

INSERT [dbo].[Calentamiento] ([IdCalentamiento], [IdMaquina], [DescripcionCalentamiento]) VALUES (1, NULL, N'Saltar la cuerda')
INSERT [dbo].[Calentamiento] ([IdCalentamiento], [IdMaquina], [DescripcionCalentamiento]) VALUES (2, NULL, N'Trotar en el lugar')
INSERT [dbo].[Calentamiento] ([IdCalentamiento], [IdMaquina], [DescripcionCalentamiento]) VALUES (3, NULL, N'Jumping jacks')
INSERT [dbo].[Calentamiento] ([IdCalentamiento], [IdMaquina], [DescripcionCalentamiento]) VALUES (5, NULL, N'Sentadillas con poco peso')
INSERT [dbo].[Calentamiento] ([IdCalentamiento], [IdMaquina], [DescripcionCalentamiento]) VALUES (6, NULL, N'Flexiones de brazos suaves')
INSERT [dbo].[Calentamiento] ([IdCalentamiento], [IdMaquina], [DescripcionCalentamiento]) VALUES (7, NULL, N'Remo con banda elástica')
INSERT [dbo].[Calentamiento] ([IdCalentamiento], [IdMaquina], [DescripcionCalentamiento]) VALUES (8, 17, N'Press de banca con la barra sola')
INSERT [dbo].[Calentamiento] ([IdCalentamiento], [IdMaquina], [DescripcionCalentamiento]) VALUES (9, 14, N'Calentamiento en Bicicleta estática')
INSERT [dbo].[Calentamiento] ([IdCalentamiento], [IdMaquina], [DescripcionCalentamiento]) VALUES (10, 15, N'Calentamiento en Cinta de correr')
INSERT [dbo].[Calentamiento] ([IdCalentamiento], [IdMaquina], [DescripcionCalentamiento]) VALUES (11, 16, N'Calentamiento en Elíptica')
INSERT [dbo].[Calentamiento] ([IdCalentamiento], [IdMaquina], [DescripcionCalentamiento]) VALUES (12, NULL, N'Mover en ciculos las muñecas')
INSERT [dbo].[Calentamiento] ([IdCalentamiento], [IdMaquina], [DescripcionCalentamiento]) VALUES (13, NULL, N'Mover en ciculos los tobillos')
SET IDENTITY_INSERT [dbo].[Calentamiento] OFF
GO
SET IDENTITY_INSERT [dbo].[CupoFecha] ON 

INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (1, CAST(N'2025-03-18' AS Date), 15, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (2, CAST(N'2025-03-16' AS Date), 19, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (3, CAST(N'2025-03-16' AS Date), 20, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (4, CAST(N'2025-03-16' AS Date), 21, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (5, CAST(N'2025-03-17' AS Date), 1, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (6, CAST(N'2025-03-18' AS Date), 1, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (7, CAST(N'2025-03-17' AS Date), 17, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (8, CAST(N'2025-03-18' AS Date), 20, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (9, CAST(N'2025-03-18' AS Date), 19, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (10, CAST(N'2025-03-18' AS Date), 21, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (11, CAST(N'2025-03-19' AS Date), 19, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (12, CAST(N'2025-03-19' AS Date), 20, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (13, CAST(N'2025-03-22' AS Date), 15, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (14, CAST(N'2025-03-25' AS Date), 18, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (15, CAST(N'2025-03-25' AS Date), 20, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (16, CAST(N'2025-03-27' AS Date), 17, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (17, CAST(N'2025-03-27' AS Date), 16, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (18, CAST(N'2025-03-27' AS Date), 18, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (19, CAST(N'2025-03-27' AS Date), 19, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (20, CAST(N'2025-03-27' AS Date), 20, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (21, CAST(N'2025-03-27' AS Date), 21, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (22, CAST(N'2025-03-27' AS Date), 22, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (23, CAST(N'2025-03-27' AS Date), 23, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (24, CAST(N'2025-03-29' AS Date), 21, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (25, CAST(N'2025-03-29' AS Date), 22, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (26, CAST(N'2025-03-29' AS Date), 23, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (27, CAST(N'2025-03-29' AS Date), 24, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (28, CAST(N'2025-03-31' AS Date), 14, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (29, CAST(N'2025-03-31' AS Date), 15, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (30, CAST(N'2025-03-31' AS Date), 16, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (31, CAST(N'2025-03-31' AS Date), 17, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (32, CAST(N'2025-03-31' AS Date), 18, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (33, CAST(N'2025-03-31' AS Date), 19, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (34, CAST(N'2025-03-31' AS Date), 20, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (35, CAST(N'2025-03-31' AS Date), 21, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (36, CAST(N'2025-04-01' AS Date), 20, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (37, CAST(N'2025-04-01' AS Date), 23, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (38, CAST(N'2025-04-02' AS Date), 1, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (39, CAST(N'2025-04-02' AS Date), 11, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (40, CAST(N'2025-04-02' AS Date), 12, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (41, CAST(N'2025-04-02' AS Date), 13, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (42, CAST(N'2025-04-02' AS Date), 15, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (43, CAST(N'2025-04-02' AS Date), 16, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (44, CAST(N'2025-04-02' AS Date), 17, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (45, CAST(N'2025-04-02' AS Date), 18, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (46, CAST(N'2025-04-02' AS Date), 19, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (47, CAST(N'2025-04-02' AS Date), 20, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (48, CAST(N'2025-04-04' AS Date), 18, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (49, CAST(N'2025-04-04' AS Date), 19, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (50, CAST(N'2025-04-05' AS Date), 14, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (51, CAST(N'2025-04-05' AS Date), 12, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (52, CAST(N'2025-04-07' AS Date), 13, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (53, CAST(N'2025-04-07' AS Date), 17, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (54, CAST(N'2025-04-07' AS Date), 14, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (55, CAST(N'2025-04-07' AS Date), 15, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (56, CAST(N'2025-04-07' AS Date), 20, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (57, CAST(N'2025-04-08' AS Date), 12, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (58, CAST(N'2025-07-14' AS Date), 19, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (59, CAST(N'2025-07-16' AS Date), 11, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (60, CAST(N'2025-07-16' AS Date), 16, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (61, CAST(N'2025-07-16' AS Date), 21, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (62, CAST(N'2025-07-16' AS Date), 23, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (63, CAST(N'2025-07-17' AS Date), 13, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (64, CAST(N'2025-07-17' AS Date), 18, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (65, CAST(N'2025-07-17' AS Date), 19, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (66, CAST(N'2025-07-17' AS Date), 20, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (67, CAST(N'2025-07-17' AS Date), 21, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (68, CAST(N'2025-07-18' AS Date), 18, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (69, CAST(N'2025-07-18' AS Date), 19, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (70, CAST(N'2025-07-21' AS Date), 20, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (71, CAST(N'2025-07-21' AS Date), 21, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (72, CAST(N'2025-07-23' AS Date), 21, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (73, CAST(N'2025-07-28' AS Date), 22, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (74, CAST(N'2025-07-22' AS Date), 14, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (75, CAST(N'2025-07-21' AS Date), 18, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (76, CAST(N'2025-07-24' AS Date), 13, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (77, CAST(N'2025-07-28' AS Date), 13, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (78, CAST(N'2025-07-29' AS Date), 16, 1)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (79, CAST(N'2025-08-01' AS Date), 16, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (80, CAST(N'2025-07-29' AS Date), 17, 0)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (81, CAST(N'2025-07-29' AS Date), 19, 1)
INSERT [dbo].[CupoFecha] ([IdCupoFecha], [Fecha], [IdRangoHorario], [CupoActual]) VALUES (82, CAST(N'2025-07-29' AS Date), 20, 2)
SET IDENTITY_INSERT [dbo].[CupoFecha] OFF
GO
INSERT [dbo].[Ejercicio] ([IdElemento], [Descripcion]) VALUES (21, N'Ejercicio de Sentadillas sin bolsa')
INSERT [dbo].[Ejercicio] ([IdElemento], [Descripcion]) VALUES (22, N'Ejercicio de Peso muerto')
INSERT [dbo].[Ejercicio] ([IdElemento], [Descripcion]) VALUES (23, N'Ejercicio de Press de banca')
INSERT [dbo].[Ejercicio] ([IdElemento], [Descripcion]) VALUES (24, N'Ejercicio de Dominadas')
INSERT [dbo].[Ejercicio] ([IdElemento], [Descripcion]) VALUES (25, N'Ejercicio de Fondos en paralelas')
INSERT [dbo].[Ejercicio] ([IdElemento], [Descripcion]) VALUES (26, N'Ejercicio de Plancha abdominal')
INSERT [dbo].[Ejercicio] ([IdElemento], [Descripcion]) VALUES (27, N'Ejercicio de Remo con barra')
INSERT [dbo].[Ejercicio] ([IdElemento], [Descripcion]) VALUES (28, N'Ejercicio de Zancadas')
INSERT [dbo].[Ejercicio] ([IdElemento], [Descripcion]) VALUES (29, N'Ejercicio de Elevaciones laterales')
INSERT [dbo].[Ejercicio] ([IdElemento], [Descripcion]) VALUES (30, N'Ejercicio de Crunch abdominal')
GO
SET IDENTITY_INSERT [dbo].[ElementoGimnasio] ON 

INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (1, N'Mancuernas')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (2, N'Barras olímpicas')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (3, N'Discos de peso')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (4, N'Bandas de resistencia')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (5, N'Cuerdas de batalla')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (6, N'Esterillas de yoga')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (7, N'Balones medicinales')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (8, N'TRX')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (9, N'Step o escalón aeróbico')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (10, N'Kettlebells')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (11, N'Prensa de piernas')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (12, N'Poleas cruzadas')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (13, N'Máquina de remo')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (14, N'Bicicleta estática')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (15, N'Cinta de correr')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (16, N'Elíptica')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (17, N'Press de banca guiado')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (18, N'Máquina de extensión de cuádriceps')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (19, N'Máquina de curl de bíceps')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (20, N'Máquina de aductores y abductores')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (21, N'Sentadillas')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (22, N'Peso muerto')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (23, N'Press de banca')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (24, N'Dominadas')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (25, N'Fondos en paralelas')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (26, N'Plancha abdominal')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (27, N'Remo con barra')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (28, N'Zancadas')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (29, N'Elevaciones laterales')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (30, N'Crunch abdominal')
INSERT [dbo].[ElementoGimnasio] ([IdElemento], [NombreElemento]) VALUES (31, N'Máquina de step')
SET IDENTITY_INSERT [dbo].[ElementoGimnasio] OFF
GO
SET IDENTITY_INSERT [dbo].[Entrenamiento] ON 

INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (4, 21, 5, 10, 21, 10)
INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (23, 12, 6, 16, 9, 20)
INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (24, 20, 5, 15, 8, 0)
INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (25, 8, 4, 10, 10, 100)
INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (31, 15, 5, 5, 22, 60)
INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (32, 15, 4, 6, 3, 50)
INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (33, 15, 5, 4, 30, 10)
INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (37, 16, 3, 1, 8, 1)
INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (38, 16, 3, 3, 7, 2)
INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (39, 25, 5, 25, 21, 60)
INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (40, 25, 2, 15, 8, 21)
INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (41, 25, 5, 0, 28, 5)
INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (42, 25, 3, 4, 30, 0)
INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (43, 25, 5, 4, 21, 60)
INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (44, 23, 5, 10, 24, 10)
INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (49, 27, 5, 10, 8, 0)
INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (50, 27, 5, 12, 22, 100)
INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (51, 27, 2, 2, 29, 50)
INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (53, 53, 3, 2, 4, 3)
INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (55, 17, 4, 3, 8, 0)
INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (56, 17, 2, 3, 28, 0)
INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (58, 74, 2, 1, 18, 1)
INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (59, 74, 1, 1, 9, 1)
INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (61, 77, 3, 2, 6, 1)
INSERT [dbo].[Entrenamiento] ([IdEntrenamiento], [IdRutina], [Series], [Repeticiones], [IdElementoGimnasio], [Peso]) VALUES (62, 77, 1, 1, 7, 1)
SET IDENTITY_INSERT [dbo].[Entrenamiento] OFF
GO
INSERT [dbo].[Equipamiento] ([IdElemento], [Precio]) VALUES (1, 5000)
INSERT [dbo].[Equipamiento] ([IdElemento], [Precio]) VALUES (2, 8000)
INSERT [dbo].[Equipamiento] ([IdElemento], [Precio]) VALUES (3, 3000)
INSERT [dbo].[Equipamiento] ([IdElemento], [Precio]) VALUES (4, 2000)
INSERT [dbo].[Equipamiento] ([IdElemento], [Precio]) VALUES (5, 4000)
INSERT [dbo].[Equipamiento] ([IdElemento], [Precio]) VALUES (6, 2500)
INSERT [dbo].[Equipamiento] ([IdElemento], [Precio]) VALUES (7, 4500)
INSERT [dbo].[Equipamiento] ([IdElemento], [Precio]) VALUES (8, 7000)
INSERT [dbo].[Equipamiento] ([IdElemento], [Precio]) VALUES (9, 3000)
INSERT [dbo].[Equipamiento] ([IdElemento], [Precio]) VALUES (10, 6000)
GO
SET IDENTITY_INSERT [dbo].[Estiramiento] ON 

INSERT [dbo].[Estiramiento] ([IdEstiramiento], [DescripcionEstiramiento]) VALUES (1, N'Estiramiento de isquiotibiales (tocarse la punta de los pies)')
INSERT [dbo].[Estiramiento] ([IdEstiramiento], [DescripcionEstiramiento]) VALUES (2, N'Estiramiento de cuádriceps (llevar el talón al glúteo)')
INSERT [dbo].[Estiramiento] ([IdEstiramiento], [DescripcionEstiramiento]) VALUES (3, N'Estiramiento de hombros (cruzar el brazo sobre el pecho)')
INSERT [dbo].[Estiramiento] ([IdEstiramiento], [DescripcionEstiramiento]) VALUES (4, N'Estiramiento de tríceps (mano detrás de la cabeza)')
INSERT [dbo].[Estiramiento] ([IdEstiramiento], [DescripcionEstiramiento]) VALUES (5, N'Estiramiento de espalda baja (postura del niño en yoga)')
INSERT [dbo].[Estiramiento] ([IdEstiramiento], [DescripcionEstiramiento]) VALUES (6, N'Estiramiento de cadera (posición de mariposa)')
INSERT [dbo].[Estiramiento] ([IdEstiramiento], [DescripcionEstiramiento]) VALUES (7, N'Balanceo de piernas')
INSERT [dbo].[Estiramiento] ([IdEstiramiento], [DescripcionEstiramiento]) VALUES (8, N'Círculos con los brazos')
INSERT [dbo].[Estiramiento] ([IdEstiramiento], [DescripcionEstiramiento]) VALUES (9, N'Giros de torso')
INSERT [dbo].[Estiramiento] ([IdEstiramiento], [DescripcionEstiramiento]) VALUES (10, N'Elevaciones de rodillas al pecho')
INSERT [dbo].[Estiramiento] ([IdEstiramiento], [DescripcionEstiramiento]) VALUES (12, N'Colocás la palma de la mano contra una pared con el brazo extendido hacia atrás, girando lentamente el torso hacia el lado opuesto.')
SET IDENTITY_INSERT [dbo].[Estiramiento] OFF
GO
SET IDENTITY_INSERT [dbo].[Gimnasio] ON 

INSERT [dbo].[Gimnasio] ([IdGimnasio], [NombreGimnasio], [Direccion], [Telefono], [Logo], [HoraAperturaLaV], [HoraCierreLaV], [HoraAperturaSabado], [HoraCierreSabado], [Email]) VALUES (1, N'SariesGym', N'Av. Principal 123', N'3476459549', 0xFFD8FFE000104A46494600010101006000600000FFE102664578696600004D4D002A000000080002876900040000000100000132EA1C00070000010C00000026000000001CEA00000001000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000002920800030000000100000000EA1C00070000010C00000150000000001CEA00000001000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000FFE101DD687474703A2F2F6E732E61646F62652E636F6D2F7861702F312E302F003C3F787061636B657420626567696E3D27EFBBBF272069643D2757354D304D7043656869487A7265537A4E54637A6B633964273F3E0D0A3C783A786D706D65746120786D6C6E733A783D2261646F62653A6E733A6D6574612F223E3C7264663A52444620786D6C6E733A7264663D22687474703A2F2F7777772E77332E6F72672F313939392F30322F32322D7264662D73796E7461782D6E7323222F3E3C2F783A786D706D6574613E0D0A202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020200A202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020200A2020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020203C3F787061636B657420656E643D2777273F3EFFDB0043000302020302020303030304030304050805050404050A070706080C0A0C0C0B0A0B0B0D0E12100D0E110E0B0B1016101113141515150C0F171816141812141514FFDB00430103040405040509050509140D0B0D1414141414141414141414141414141414141414141414141414141414141414141414141414141414141414141414141414FFC00011080219043103012200021101031101FFC4001F0000010501010101010100000000000000000102030405060708090A0BFFC400B5100002010303020403050504040000017D01020300041105122131410613516107227114328191A1082342B1C11552D1F02433627282090A161718191A25262728292A3435363738393A434445464748494A535455565758595A636465666768696A737475767778797A838485868788898A92939495969798999AA2A3A4A5A6A7A8A9AAB2B3B4B5B6B7B8B9BAC2C3C4C5C6C7C8C9CAD2D3D4D5D6D7D8D9DAE1E2E3E4E5E6E7E8E9EAF1F2F3F4F5F6F7F8F9FAFFC4001F0100030101010101010101010000000000000102030405060708090A0BFFC400B51100020102040403040705040400010277000102031104052131061241510761711322328108144291A1B1C109233352F0156272D10A162434E125F11718191A262728292A35363738393A434445464748494A535455565758595A636465666768696A737475767778797A82838485868788898A92939495969798999AA2A3A4A5A6A7A8A9AAB2B3B4B5B6B7B8B9BAC2C3C4C5C6C7C8C9CAD2D3D4D5D6D7D8D9DAE2E3E4E5E6E7E8E9EAF2F3F4F5F6F7F8F9FAFFDA000C03010002110311003F00FA6959D9579C00704629BF688E352A47CD9E4914BB048A0A06273F353A68D5A3788B807AFCC39A398089997084BAFF00B21451F68324659D48553CF151AFCB32C607C9EB8A9194A4BB8072A7E5C76A7A922F98970A080DB47A0A5DCB2798614C363BD3656789501E8C70428A99A109E58CB293CD3158634B2796E24079C01B691316AC4313B71D49CD48D6FE63090EE007070698D10932A36E3EBCD4EE03160812627793DCF348CD2336E8F715FBA73D314F5813CC67505801CD1E72F3B09CE794ED55B0D0A59582A05C1C75A7B46EE8A3CC059874F4A899E4DEC461481C03DE902E240ECFF263B7F2A8650E7936E13E6381C81CD44B18864531A671C939A97CB8D3E70EC383EF51B2AC7B06D257AEE1C66AAC21EB2063E610CA07F77BD285888C3AB303D29D237CA88B8E7F84D35D7CBDA06E52BCFB502B810BE56D4421A9CBB9548660370FCA91597CE39DC58F4E29ABB159D9C9033D68107EF1B681F3267F878A4DCEBB8065C06E14F5A199631B32DC9C839A7C9E5DBE3CB1BA46E79A6046D218D8B2AFCD9F9F77229DE6F9C03303B41FA0A72C8FB59F2A0FA35232BCB192C54A639EC29858199D654658C0627EBC521925DC859548079A58DFCCDA49DAA578DBDA91542C614E646CE48A0A490DDCB7123062CA3191BA9EB3A0D8BB33C70D4B310CAA563CE38EB9348CBB769450A47F7A931D902AC6B1E096248E0532454DA8572CFD3AD3FE68E4572B924629571F249B33D7BF4A56B09916FDC46C1E5B76C0CD39A36775C36F65E4F18A58E468D94380AA4FDE2291942E584809CFE94C91D1B3485B9F2F9E847148631E764B1641E8691595A365562031E3753BCB4B739203B91DA91571A1BCC75DA4A8E9CD2471B46C0210CB93C9A9325A34202E00E7D698D12AECDA4E739DA29D8916394862A63E57A9CD28C47B5BE627FBB9A5922116D60AC5BFDA3C5376AA856D8D9EFCD05681B63DCC5B763AE334EF9368263251470734643B2BF97F2AF1488815D896DBEDD6A434124DAB1AF94871DF351DC2C73302C76BA8C9A9D622ABCBF53DE99E5991995F681EB42111C7E43481803D383EB536F0D20DEA579C74A45548D738C30E07A53965DCAD8058918A60264EEC2FDDF7A430991919F039E314D50E3E56650AC29BE595DADBB9CF005485C56DACDE5ECDCA0F5CD37CE7F2F1B30A0F1CD3FCA0C9C2B3313CD2288D25DA41E3A0F5AB18798AC372E433529CED185F9BBEEA5C87C610840691BF77275603D319A043BCCF3390A43E3F014CC4A392C182F34FDCCD0B6D1C1F5A8D9834614E11FEB5370B8E92466562D1FC9D7149215601A34C9A72B0462AEC58E3A75A46668E2DBB4A93E9542D45DFB72BB46E6E68590EC21F83E83AD02244DACEE49EB406DCAC5703D09EB5202AC8D90E00DBD39A696690B331DA01E41A1984918CA9273DA9CCC88C14827771CD5009248B26DDAFD69ED2332E1790063A535A311BA2A2AFAE694011861BF83D6994908C5A4EA76A81D450CDFBB013E639EDD6858C460027934BB52DDB76F39352D8EC359CE0F96BC8E4D35260CA37E7767B1ED4EDA14860C4EEEC298A764C46CC8C7DEA091C36F9A563E33DEA4830AAD9DA46793DE9238836D2A382392698A3C96217E7C9A0036ACE194E40078A9155578DAC2323D69A079B1B0604153D453FC9F3309F3631C1A241622F30C601D8360E066A18D60562DC965E47356A48FCB50392BDD7351AC615C32479CF506848771216C2BB6D054D3DA367452A4039ED4E0A59768518CF229AE82238072738DA2815C55442AE09C8E94D5F2A2556C9723F868689E3FB8A0E793CD2B463783B0E3DA96A088C1559B72AB74E4F6A5DED1C8C768208ED4E58372B02C40EB8A5470AC372E00E030A760139F277A819CFE34EDCCC464803BAD3782CDF2FD29D26C56040393F7B346C035A416F95099CFA0A8DE4728360C6DF5A99B01B8DC411CB534B08E3C20DF9EA5A9219148CC232015DC3B0EF522CC367EF22CB1EF8A72C6911567192DE948D31DA1B38507A77A7615C06E74C96C0E98229CDBC98D378C7AD1229F2F79623BE29164F315502E09E7345841E58B7DCE194FA536421B6E63CB75183C52AC2096072C3EB482355E7073D8668B0C45916460245C6DE69530BC6DCA6297F88A1400E33416565008298FD68B007C86453B7E6C7DDA4F31958B15CAB71F4A7657CC1C329238346DF9589395A41710E77A36E233D1477A4F930CDE592C39A5DCADB1B0C3B52FC96FF003173863D2980C32B797B847823EF1A7AC8D3118000EF4D5DBB4BEF38CF4A566590954C9FA1C5002B4CCDB955493FDEC52198B47CAE3EB4EF99242AD9DB8ED4C45656CB06DB9E01A400774CFB7770A39A5F2FCB4C263728C9A798FCE563B768F5069AD1B3A92BF2E38EBC9A6801A690AAA3AF2DCFCB448C3E557CAF1D2976828492C58714045083D4F5DC69B288D595184614939CE68F30DBEE20375EFD29F1B0973B4608A747108D5C1CE41CFCDD2900D2CCD18C6300676F7A4F34B286652840C122914282E31927818A5F2442A4BAB1F4C9A2C242EE324602B0248FBDD29B162471824BA8E48A748823446C2841EFCD0AAB2B36D60A31DB8A07A0AA4060C11C851DBD69B14BB831D98E32D4AB1E610A0B920F3834B731855E0FFC04F7A5ADC45710206693CC2015C81D6A4819225DCBB896EC685B74568C923FE026A560B0F2A318E319C9AAB137B08855B74885B207269ADB9A20012E7E98A5605022C6A4134DFDEC8AA49DAF9E99A91EE2C89136E2C846001F8D47E648666603014704F18A7AC21DDDDD98F3FC34C65F95CEE67E7A552042AC8D336CDC9BB3C9F5A3723E1406F97FBBDE9FF2246BB532E0669C10F20E1588C8D948048662CA0A20DBCE73D6A3DACBF3AAFCBD49634AEC2DC280778EE1695906D1CED04F434AE035A6768C1652AB9DDB569559B6A951F3B72777A50A36C840CB9ECD9E29DE41906E76C153D734C0492765084360E785514BE67FB2C5FA9CF4A6C7272A150851DC0A0799248EEE8D81D306900FF30CB1B109B573F2FBD2F9D2336D740368E4FA53554DC47F2E547614E605B2E5B25460A8EF4D210CF396184A862771C67B5394797B8236E723342AA797848C9246727B522AB346CC5324F19ED54161550B124BE70BC8F7A154EE54018AE33863DE8F2F30AE1B1CF3B7B53DA0F330577640E598D26EC508D2F931BABA64E7F84D4723B49F206F2C2AFD6A4DABF3E1779C7273D2A11E5981A30AC09E4D2DC0729469151DD8ED196CF4A5DB13249265B6F4C0E94F8FCB8E10C5555DBFBE7B510DBBBC644877213D17F9D1A85EE0C15A3326FF002C74C2FF005A5558DDF7EE3297EAA7DA98AACCCD1A85442782DDE9D342B0DC025F6E3B25310BF2AC8CD223AA638C9AAF34312C6A23473EA455A8D5BCC72C4943D3CCA8DA16DC2225F939F97A54AD4656DB6DFF003C5BFEFBA2AD7D9DBFE79AFE74550AE3144BB6460C07BD0B23DC6D1B0823A923AD395FCB63F2EEC8C65B9068F311537ED60CDC67B0A5615C2E25DA54A361B18C639CD44AAF2319416F9481CD4CAD1174CBED60383EB4A64656E5D513AEEEE6A8A486BCD25C17001C83E94E666899019325BA639C530C723C7956C7390DEB52AB7CC47983A76A91B1924726DC2176507D7AD3563895BEF02475A58F7301B8950BDFD69CB01594B6E45E3F3A7B13CAC558D3E668F70CFA9A1A31B36EF5DCC7961510C2ED8D989C9CEEA6C9E546CE32597B62A771EA3E354666466DE547AD22F9B0A95640D18F9B7538C6921048DA31F7A9BBBE62B92C9D081541724389B0432AC78E82911838650FC9E05322542A30802A9EA7AD395E36F9549519F4A5716E23AAAC837658814AC9E605752C768CE28554595D77B61BD6980ED5728484079F534C2C3F6B3365DF6151F9D13C8228E3CED233CE3AD2FEE588E58B11FC5447E4AA609F993AFBD0572845B431672C73D030E29A8C530CDB7AE3A528DDB537392B8240A72B799105DC8A739391CD01611731C81D99416EB914C1E632838565073CF4C54AB2234986F9C63D2991B9E540DA338E7D295C341BE6170079636673C714F667593CB1800FA7614E2AD22229015739E2A3F2DA250ECC4F3F7475A2E21822789321CB153F2E29DB8AB0675DC4F51E948A8159892D8ED4F58F6A9F9B78ED9A006C7B9591BEEAE76F3481B7162CFF002E714EDAD82A481B7914DC8DCC59368FEF0A040BB262031240E94D630A4CD9191EE29FB8002355CF39A0346576E4FCC7AE3AD30142858FE54C83C8F6A6F972ACA1811B40C8A55C0DC0BF3D0014BF75997CCC531D86FCBB327EFF00A0A5652AAAA8ADB8FF0011ED42B796A542FCD9E0D0CAEBFBD2EC0F4DA69081524DA8D22EEC1C1C9A46FBAECD19DBE99A5F30090163E603D71DA8C86DC76B30FEEE695806ACA03280BB50F4E6A568515986E50739C8EB4C575F2F0B1F20FF00153BCB5DC5832838E714582C46ACAAD862D264F1530891F04FCA0F18148BB30AC13A1EB4D68E4321218460F3D29EC3E51FB70A141C7A8351CD80DBD5FE55FE1A554327CF2301B4F04526EDCDE585C8DDD7D6A771583E495405F933DCD3931194CB06F53E946D0D85F2F0A3D29ABB2343F293CF39AA00DBE63E15C1C1C9C5202859B08495EF4F11AC80322819E7AE2918FEEF6856CF72295C188CCFC2740DCE69598DBB104965C76E68246DC60938E3342C891EC52F8614C4862618E324EEE99348DB1A41B97007A0A7AB24CEDD8F6A7C61163232777B0A0AB0C5930C1D1793C629EED27CC013BFDA916E3A81C0E838A72EEF9880C0FA9E94058649222EC1D5E91564660DC153D8D2B2333230DBBBBE7B52EE2CDCE405F4E9400D8E491B2814022862FB4060A48A734610B32707B7BD22C87E5CED2DD282958503E52FB707B535644DA46D3B8D0AA637C33669FCAC6580193D3153718C12157C1383D00A3686C890E4F5A6B6E6939181EB4A8CAC70CA783C9A08B846C238F18DA7AD1E712DB71B7239A590249B8ED268460D801707D6A810D8DFCB6211B2A2977F94AACCDCE73C53C31DA46C19F7A8F7842A19033522C798C4C0104927FBB4AC4C19058F4E01A4556656C0D983C60D285DAC432EE27A1A04C48F1248090CA319153B6E3B5B83F4A85958AEDE87D73D298B1B2B2E097F5CF4A6472B24318F30957C63934CDA24400125F3D4548BB55B6E3E634D8E130EE62C55BD050161B1AB8562D927A0342C86250AE721B9E2942995093B87A6293705400292DEF4C761C008C127386E8299B84916D518E78F7A76C2A54B707D29CFBB96185DA7200A42B08CA5DB6BE07A7340254BAAAE7DE98FE5BA82D92D9A7BAE3011F02932921B2337CACC0E57B0A58C6DFDE6CEDDE976B00599BE53DA9DB832A6C6CF19C5084E23564327CC47CA3A52A6E64F9D4633D69A310B01BB764E714AD22365B2D85ED4C2C3B7050D9241EDBBA533CC4DAA7393EA07142B09A40E41DBEF4F3226D6C703B0228191B46635DEA0E49A7331F31731F1DA93689572CF81E946D0876F999CF4A4488D23464909F2D399583236327D293688D5B2C4AF6A44FDDB31F30F4CF4A3510A572CCCCA411D314AAC36271C770DDE91640D27DD2063D697CB5F958B6063A5161D862B248704155CD0D1EF5CB29C03C548591B68575207A0A46B8F330AAA734CAB0926E6C8DA362D204DD31DA8CA31D6A4F3171B5C9C9F6A6B47E7659372F1CEE3C50C06875930195860FDEA4F359B392C11B8C53D8B328550A7D714B866657FBAC38DA692246C71B22B6EC907A0A62ECDA4AB61BB669ECB23312795A554DA58A2F6E322818BB995427438EA699C073B9B3F2FA53D95D89909C76C76A03333150A1B8EA29011850FB486DA33F4A6AA9270C4B0269DE61940420123B52B4C1632A8BF30FEED32436EEDC89BB3D4522C8D1855954938CE69CADF2B176653D8F6A061A360186F1DC9A63B1065E4D9F282B9E0B55868DBCCE76F978E714C2A66C206C15EFDA9577AA307703B0C52B006DDA19A2DC5BEB4E8D4B3AB4A79238C9A6F94D1861B9883D00A153E42641CFF000E4F34C2C48B0ABE495C1E9C534428C85F0C18F03D699F316F91DB38E4376A73C63F76A5C93DC0A42B089E632E5F2DB4FE346D01BCC219558F03BD2AC7B94ABB9FBDC64F143AC6AC7258AFB532AC3542C9B82A328079A5693CB918479031CF14F6DB26E192831F89A6A28F2768976E7A9EF45C2C86AAE64520B4A3A9ED4AB1960C7EEA77C1A779C8ACB1E1B819DC29B1C7E41C960C0F6A0919BD5630A07D18548EA8C46E6CB01914D5912652A5B0B9C6DA7FCA18AA8249E050558646A246259194039CF6A79C37DC5382D93431924298E38C734BE71570A198F638140F946199B6324608E71D29558B821F20E71F2D3B7F939CB36D273C8EF492B18D72A115B39E4D316847201B9BCBDDF29E9EB4E59D2354504A375248A7A4CA304B7DE1CE052960AB98D371518CD22B41891BA9528DC329FAD26D3853F322E3F8B9146DF331956F3169FE5C9B063E5E7001A57168331F7762B37196EC2857691B84DAAC33C9E94E92577C6D5E09DB914AA06ECA8E1473CD1B8AE3709F3B20C9239CF4A72C8F33288D71918E9DA936C922602ECF427A538CAA596324E42E0ECA6490B44B1972EB82DC0EF9A7B798C8854140063AD3E0CA2924719E3B9A66F6F9B09FBB5392CF40C116236E4799870739349F6A645E01604FCA71DA90B6E5629B117773EB5235C22C7B51F241C63140D26C499448DBBCDC160322952385A5C890B15E0337AD1E4E251BD0B71FC34CD8D246C8AA17073F3503E526F2DBFE7AC74554DEFF00F4CE8A2E1CA591BE0DEAB8391D6991ABB28671B813F74D3238FCB5048DCE0FAD4D26571F36F3FDD02815863321721E3D87A0A59B6799FBC5C923036D3E468F3C231E3F8BB545BBCCC20521C8FBD52B5019BD2350543360E3D0549E4B4CDF7829C6452EC663E5E0703AD260EE6561B9B1C6D354098EDCDE588D1B247AD22AC92121957E5E3349F346E4800263F8BAD33CC56CAA9231C9A455C94BB4817684C938A14347B8155249EB5091E4E5A3030BEA7AD4815A4C3938C7381420B8BB4AA3090E4F514E3BD57380A08E699B0B1DE481CF018D3669836E463CE7B7A532058D64F31D428231D4D3A4395081B6B0EBB47149B9B71457C2E300114BFEA0A9C8690F0682EC381457EBBDB1F33522B623E5006278347DC1BB00393FC54BE52CD27CCFBB0723145C36177309230B081819CD33CB32C80B109C67E5A46B831C6D86CAEEC669A731463326EF400763537B8AE3A160B8CA967278A2488C91B4813612D8DC7B522C3B76AAAB07F5278A72FF00CF390E30DDCF5A02E23483C9280EE6CF55A50ACAA1091823393D68658E167D879C63E6A18AC8C0A480B30C7B55210D8F6336C2DCF6A746A9301962581EB43658E43286231B6917CC31A0C043ED4589B31B869249002CC9FED51D631BF298E06DA557F3198073BC7073C0A511B79677C8323AD03E51BB40604C87A74C539591906037AF229AD956F9A5CAAFB50BBB9C3F04F140F945590C84B05040E38E0D337055DCAB839FBA79A7ED0D920160A79C535C9E0AC5819C503B0E68D64CB11B1B19A5673B4ED2A41EFDE91D4C9BB71DBC702A3F3376C8CED42A320E296A1A9261DC05EE3BD122EE6625F27FBB43285703CC3C0E4E319A06CF980215BB531848C5B3F200D8C0E295A4DBB778CE463E5A04823E59F3B7DA95BE560FE60231489634305C27039C8DDDE848CC2DBBE5F9BB52291246094CB8E94EDC540022F994648F4AA1A1D92CA3695E7AFA505DA12307706FC6A2506100347C1F7A58CEEF907CA7352CB14216C9665542714F2ADB7680171FC42991E406509BF1FDEA4F9D23396C0EA45021FBCB1D8AD938E69AB1BFCA0F23D2919D36AF503BB74A4918B301196C2FA50C5A02C624B86455C607738A76CF2D186718F7CD2056571FC4DC9C93DA9515303A2B1EB420114BC6C8B9DC71D4D3954B61D8292294397709C600C0A45638F2FCBC1FEF5310AD27C9E60400838C114C3BD369DC00EA714F659237F9C6E047DDA6AE6560597153B92119DF11C3719E38E68DD24654B06607B669DFC5855DA288D9D4E1D770F5A655C6C7B17E7CFDEE707B52A932A365B83D69B1B8DEC08551D3069DF2B39C6EF7F4A07B8DFF005787CE7B7343299338503B8A37064DAA39CD3B0636E3E62474F4A44884B2B6DE06475A0C4FB41DD823D299B5D9473B79A7FCCADB437E74EC217CC4186E491448ECD80A98C8E7E94796D20DA46D19EB4891794EDF3163F5A63B0BE5F423E51DE9249197053071D29384E0823776A7AAE15806FCEA58EC46EECC1405E7D334876AB067183EA29CB10EA33903939A6AC4DB482463B034210D652D2128FC0F5A9B76DDA13E6FC68EADB58285C72691A37460507C83D2A8131CBC4854AE323BD0EAE15630C39A8F73B2EEE83A52AEE455248CF6CD49A0E5876E10B82739E3B52EF4590EE62C714C5E5412C037B538B1450E42B0CE07AD3258C1B9836D7E7D29595C011B707D4533CEF9999131CF34EE5CF9BBB0076A010F0115B0C49E2923DDB5B0A4FA66937798DB8B0CD2798D229F98AFA60517192AE188C80BC60834C91561E996268DBE74606EDC691A2520EEDCB8E94AEC91177EF5054ECC75CF5A7AAB2B16098E31516D2D183CED5A94658055CE08EF4F60B8ECFCAA7863ED4D20ABB2BBE077A421A375031B7B9A568D1B279C7AD48C3CC0836839F4A46DC5864FCBEF4AACA7715200E9D29BBF6AED00B96AB26E298BCCDA7728A176B37CDF7874A686F3DB611B0A8A51CE3F89075346C5683BCB3B98EF1853D28650CFF336001C629AC8AEC5C0638A3292B29C1F4345C0939F95028A4323EE2BC600F4A26663D36923D3AD22CCE64DA17071CF14891AC557685700F7E29EAE3CC1E5F071D6915B6300DCB1391C50DBC333A800F7A631923B2ED241704F269E5A438C05F987734DF311B8CB7BAE295E419E17073804FA548091F3918E7BEDA72EF752B900B7AF5C529050A9073EC28561213FBBC1FAD500D8E3660555B27D3D2977154F2CBB74E0628F20161B0B03DCD3171CFCF82B40683C2ED60B92C3BD383166200D9D81CD2796EC546EDCA4523C217683B81038C5213177792B956566CE381489BD8BB0186C71C6299180A482496F61C548D37CD86DD851D7A5502427CCCA4B8CF18229A5A3932A070C3938A4C246BB831DCC7BD2F99B4FCAE0AF43F2D4DCAB0DDC8CDB5770DBCF3D295637915B76DE4F14F27CB0437CD9E0014C917E60511B03DF8AA1587A3792CC4C995E947CF21C02BB4739EF4C56130C3A6C07D28F302BEC08C3D314144A24936963B4E78C5363519566397CF14C552B9766E01E052EE40CAC4F3DA900E3BD9577AFCBBB9E686919B788D46DFAD232EE61197CFF00174A6A3468CCA03283D69300309930C5836D1CE0D3DB122966E0EDE14547948D31B1816F4A72C6ACE8E80803BB5161587C61A45CE02FCBB452460C7F33EDF97BD34C836A066C014E58C46C8ADF367AFBD31728D66C45B828724FA60528CAB6F2DB703A53E3675DD88F283A66A2F347CD232107183E94AE011B34927246DC64539C343B4A484927D334CCBB6D40AA323AD3A19248A458C2E40E948771F0485C92DC027F885309334CD22A295E86A46DF26E19C73F2D3B715555C291D0FAE699247E72AF24A85E9803A52EC558B01C824E7AD3711AC4C0C6CA73C8A6EDF2D5405CE7E6CB76A2C213CC3B94348DB98E3A715200CC0A86DC1467AD12655790CEDD471C51E5FCC4B29C1E3E5A760103662F96361ED9A71877B33B3054C718A42C630CAAAC73C734D185C0C3631F30A0AB0E5C0D89E6EE04F34BF3B3B6142E380C3A5246CB24788C0DFF00C2585384A172392E78F6A6160F964DA4B7DDEBB6A3690421C05678CF4269C91B43BC64296E9DE9ED249B36ED5DC3AF34808F76ED8DBA38D7DC734E665F95D4A8E7E638A56F958070A401F7475A6B46228C8284B75A2E2B8AADE739D818E3927349B9DA3DA42A73EBD69B348632AC54A64745EB4D9766D0092587239A9B8F51DF665FEF2D14CFB4FF00B3FA5140AEC9F6990108A300704F7A62C2F088CEFC2F53FE149248ACC9B64DAD8C0E38A5550CA4966651CFE3543243BEDF951F7B9CB52B070A3214311F78D40ACAD857DCF9E40A5DC6363B8678E01341361FE5B342087CB1E38ED41843642B317C738A6AAB344086015BF857B5248A14A88DF0DE9487B0AC25590232EE41D49EB4D59191B1B3087DA9CD228570D21DDDC527DA576A21760A7DB9A120E61A902C2CB232361BA02735246A5D9A4F2C8038C66A390F96E87CC6237700F34E59048C0B16519C63A53B0AE3DA3DCAA51723A1DC680AACC1553E7CF5A6FCBB721492A7BD2F9E19900C271C9C52D806899831F336A8DDD714BB8190B30D8777073DA9B3E59463E739FBC47149B924C339C12703140EEC7EE55240F99D4F5A91C2C80347F31279229B0FCCAFF261B3F78F4A4F9B600AEA195B9028B05C5774DAC047C83C2F6A6026490AB85555EB8A7BCBB37A870AFF004A4DCAC84E373631C534AC211A31F37041C641CD2F968B1863F3B8191490932725370DBB76FBD35FCB380EDB4A9C60530B0E5923DAD804B15F9B776A6C6517188C8751902A43B0798E1978C0DB4E665625D8ED2578DA2A1B1D90C1123246E436E3C91D285C2CB8906D19F9707269BCC863F34FEEF1C522F94CD9DC73D062AB51DC7B0DAA1A34DC49C73DE9A544990498B079EF443186C8259953BFA5036B46423ED19EF4AE02F99FBE641F367F88D3DA3F9C6EDD8FC8547E620217197EE69ACD84237B37AAE6A2E171DE4BEE187D884D064913217E60A69242BE4E3E6E7A0A237DEA4EE391C6DED4F515C732B290C5064F20D04348A410031EF49CEC04BAE7D339A72E1DB7F247D69DC7723558CAE1CEE29CF14AB279920262C0C753479BB32005C77C8E69DE70754F9818FD7BD3B8AE37F7A9BA4D80AF4C0A73AB150E117701C0F4A6A92AEC32C528F31964FE108DEB4C2E3A40CCAAD90A71D3DE91879787327CC465A9DB57CC009E314D591509F93783F7695C00B060AEDF376A6EEDDB98273D30B4F6661205DA0AF7A6C8CED233478DC38DAB4905C62E5901DDE59CF4273536E1148727786E9501914E63906D6F515309225640A485F6192687A00ADF30DA42FAF350B1921DBB0EE04E4E3A54CE14C836FCA4F1F31A8D182E63DDDF1CF4342018B876652D873DAA591576EE04160318A422147271C8E0906988D1C6A5B1924F434EC2256909553E5E78E4AD3B79D88A884F19CD460E636310FC29CAF23638C0C7A51600C7F19DC4F4C6686DACAA013BBD29B95541B771F5A5628BB405C9A0AE516450C368DC873D68C8DAE1DC8FA52C3F32F9843061F9531A449655DC7DE80684924DCABE5818EE7BD3D5D55B0A4FBE697CC55E23DA01A63B093014924F14D889360C63386EB4CF3033158C64F7349E620238C9C62955460F964EE239E2A442951E5AEEC893D2902C921507A77A5E5420392FEB48BBD246F998A77AA00C1DACBBF914AB246B85E59A9597CB7E1460F734C0C815828F9BD695CBB8A77AC6085FD6959CC8809181DE9AAE028C025FDE9AE005C721B34089176893E5390474A3709186E1B05303A3657182B4ACCAAABC13CD31585654FB9B8F26977F96CCB82463AD2E7D17E6A6BDC67036E3D4D436318F0ED3F331008A6C5B377EF1B1E94E931B55B25C534CA933AFCBB403E94EDA05C9D42CC000B9C77A372F2A46003512CAADC2FCAD9A7899590A8FBC39E94857119896DBB4843CFBD33CE48C3A6C2475A9B7966015BE6039CD35A42ABB4A02CC69DC445B84851D46077A725C19A4211997D4629ACFB7E53F2AAFA54AB37964B20C02719C503B8AAFF79517E6FA5376EE006E21B3CD3B7ED5C86CB67B53B3FB90720127F1A6218ECC8FB532C3D28F99901CE0A9E569564556C92C0F6C53558C8F9418C75A2E022A9F955B7609CE7D294320919431CFA7AD2999E59085E83A8A4DD18566236B668B00E6CC7807E51DE9C73B8118C28C834D59908197EB4A54337981B23BD48EC47348D1B064F989EB8A1643B81638CF18A55907CDB783F4A491943AB30DC4D50816429F2316393DA9D248065106D6EBD2924906D60A32D8A5DFF291B943FA6295805937AA0F972DD4B0A56628C1F25988E98A6E4E482D963CF14E56977904E3D3229809BFCC5DC7E50DC0A6903E55DC4B1FE54BB9B2095F973F852BFEF24F997626386A07619E62421B67CCC7B9A159B20150CC39E3D2904910071807EEE4D4C1B6AAE59727A30A6211A3F3654605B1ED44D98D831738EC29B0B36F3B8B15ED8A3E4C6D60C7273F854803B72312139A7BE51B6A95E47E34222796DB015F4350FC8C0FCD96CE01A622464923C36FFA83DE93698541DE793934BB5F63053B88E84D0ACCCC49C103EF50521DB8BC6EC9C0E9EF4DDC76EC237B50AEBF31CB29F4A19BCC939E38C0E295C637728DE182827F4A02AF960B15FA28E69A04655CB03EF9A19924239DB8F4A4324F98C872E00ED4D542A186F2327BD35B64D80AC47A9A6EDDA642CF9CF6C55A15C90B4AB80B860297CCF9B0C76923AD46BB24C05DC08A7B6CDC495276F7A4D85D8CE0C7CBFCAA7F134BE62F0C00655FCE93CE122AED2020EB914AAA3737969BB3CD48AE49246272C42E001D6998768D5432EDF5A79FDE7CC3E5C70454726C6C05CAFA9AA41724F38C6E43957E3D29AA7CD8F258AA63A527983760005BD5A8DE92FC84648EBB696A1761E6232A679C74DDD29F0B1981663B71C0A6ACA1D942606DFEF0A5F3BCCDDB480D9E0638A02E2AC85173F33ED383EF4B93212AC7601CEDF6A50EEB1F4520F5C533F76CCC54B640F9A85B8860E64721F2A074EF4F4318DBFBD62F8C934C597705F2D304FB5485A38A40769624720D5858237333039207A9E288E356C65F0CA6916689954B039CF14F9132CAC23214FA521D86C654B3C8E1BEA29CADB94141BC6724B75C535E60C1902E141EDD6957E794F9637201DE8B89846DFBC60CFB4F6A4556018348413D2A4668D7680ABBBA9CF351B1563BD5F2D9E07A54DC06B4A576EC3BB071CD48B248AF9600EFA6AF98AAFBF0BCE734D59300167C93D06298EEC91958203B8260E78149B8B471E7002F534D53E5B1C1E7AFAD11AEEE5D4F3F36680D47AA0565DED927FBB49246EA84AA0DF9EED9A6F98249193CCC7AFB510E130496619C0228B085F27CC04ED65C0F98D208C48E48320CAEDC53B6C923302CCA5B91CE0523191642A3E68F1C9A2C1B02C92AA95D80F19DDD69ADF7D2468403DC93526F48D4A2BE0F5F5C545B92E146ECC81B818E0D3B05C9FED3FF004CD68A8BECABFDC6A2985C7ABEEE14ABE3D474A454963C02CA8ADCD0F1E085083E66C939A2485F6B3F1B41E84F3482E38BED897E5CBF4EB44792CAC55406183939A8FE599D4AA1CF7E78A930FE618C2201FECD20091F681B4A8E71BA916528CC4FCEA7A3629AA2154F2DFE5C1EA6944BF2C89B805E99C502078BCE53202ABCD2C9206DA100727A535A130E18302A39E3BD3B6ACD8C9EA32B8A60336BC722C879C9FBB4B244D349B5D847CE71DE9ABF2464973D78A4608C8497C36698C7B6E556008F2F3D6A5729E5EE52AC47033C5323644DC108DB8E77531B62C21988663F954EE3B8E570ACA09C32F3F2F43447F7D5DCA84CE738A763710F805B1C6CA3779B0E1BE519C74A6489262E0B88DF3DF278A06C6840CE594F383447185C6C19933B4EEF4A6CAA32B8881DAD8E0D003D5A3F30E14EEFF6E98B184572136FAF34B2B20472437999C7D2850C72ED1E723D78A0A42B4836A2860F8E7E51CD2B47036D38CA9393F5A6026275431EE5C67834711C7B962F989C7E75371EA3A3645DE4A8C74FAD22CBB55CA03D7186ED4924676E563E57A6D3DE932EEA438552DD7DAA440B115DE645DD93802A7DA8195BCB01178EBCD44BCC982DC0E69AB1F0C73919E0E69EAC5A1249B0AB1547007AF19A46C955DA9B40E49A743B958B6EDC318C3734E8F2B193B97E63F771D28B05C6EDDC77EC5271EB48B18642CC83DF9A7ED1E617620E4740293CB4F30B16C03DB14816A40ACE6455DAA1074E6891A50DC281CE4E6A60A195B2AA1FB5396364520AEF2C3AFA55363215CF980EF5518E9419373044F9581EA29586D6CB306C7400537E443B93838E49A49081711E49183D0BD2B3149142B2B28F6A45CB20DF2AEDCF61D69CA4A1621D3938157610D958B86F9791C961D295B6B4630AAC40CF34AB96539652B9C1A4F2CEE2A9B083C934C7608DB6A1E573FDDA5F333190AA7767AE29AD1E14B6515B3D33D295D49D8159B775E3A5400BB4A37CE3706F7C546CBB236655DA7FD96A7C65D58F98A0B31C0DC69563F2B792157D79CD50AC26E5F94B26491D4D024891404E5FD076A6B48EA030656CF038A6F0AC599B6E474028631EEE9BC1D87E5EB9EB4B248BC2AAF3D69BCEC0B9196F5EB4A8A460336093D69002AED94829EF4E55FBDBB6B027EED35B0FBBF7B8F4A36975503041EAD4EE2136B166DAB85A7B2E71F2ED18E0E69A570AC85BDF19A72C677221208C7E540F502BF2800E4639C5363C0DA07D7E7A604084216E33CD24AA15820DC71D2A772912348048473B7D57A52EE8DB0A46063AD44AC230C1C1EBC54B27EF08C018A076123F2A3CA8E368C6EA72B22A1C3673E829BD5DBE5E00E293E5400E72C3B53B93B0B1EC8643D8919E69C5495215F24FF77B531A58E4620AE011D694F971A10327E869DCAB8BE5ED2AC5FE7F4A5CB4818370053558655B61DBEF4EC6E39008CFAF4A4C4464955E4963E94AAC8ACA30C09F6CD3DF24F4C05EA6930990DB9B77B508439642CC7E40314C3C65CF5A0ED1C12C58D2A17605153A7AD02021571B4EE63E8295189DBBF007A530A88E3C670E4D3B6FCDB8B7CC3A0A004DC159B05957FBD8A771B768F981FE2A455765DAC76834881551C33127B52B0D8D0A22C64EE19E94F0DCB640DBD4526D64DA00F97D4D2ED73290CA19714C91CCBBB0576803AD3598C6C762AF231C522ED1951F22E79CD38431AB12ADCF6A006B15902FCB83DF1470AD821895A32D18600027BE295436E05BA1A2C319E50910E17041EA69F365B6A120639C0A6333B30F4F5A7B336F551B4B7AD00353CB8F7153D294C61A3DC49EB9C51E5B2B1276926976969086385F41458761BBB72EF0BCF414E452A4E4E3229BB072A9C60D2EE56902B1E7D69009F74AB063F3706A4DB1E1892ACB4D6593EE1C15CE29ACA91E118E467B5310EDF1C79509F7BA508C1A4C01B05183E661482A3BD2BED390CD8623B5218E765DC7072C7B0A6121426E527EB4B95F900C86C520566C2BBF5A648ABFBBDCFD0535771DCC5571D89A566F2FE556CE29A7845CB8C67914EE31DBB2FC6DDC47E54D00312087071D68658D98B2EE071D6976EE20C791C75CD08684DACCABB37607634B2FCDB7E4C81D79A419503EF3B77C1A6857467654C9F4269163FCB8CAB2A2EEFAD292A23C8032BC0E699C2A1DFB81F6A3CB0C0145E8334C8B0FDC5B82BC0E38A5894C7807E4FF0078F6A4DC65C17E148ED449B2461B577803A13CD1A0F415ADF76D024E54F5CD04950D80A71E94D02358C0E50E69ED1B748CA8F5340683199F249CAC64505917042B74C71DE9CD8653B9FDB1DA861E66C08C00A571E82701728A4E3A93D29DE6B12AC538CD3163745624FCA4F414AD22E001B89FEE9A9B123648DD8960430CFE14E97E6DA40071E9491E3F788F944C64D1B6358C88D883D46452011D882536609E6A400FC9B5467B8A5121E80076C7348CA7CC3CED2DC1C1A7AB10C75D8C408F66EEF9A7472798AAA40418DBBA9EABE5FCC7E63D39A028553C820F38C74A760B91490A79606F5639E45238E9B58003AE0D4AA888FBB0181A6EC8E3DCDC64F6A04B523F2D42929F33FD69FCA2A97887D6855455DCAB8663CFD2919436432B38ECD9C0A57188C5E4604C7F2FA62A4450AAEC02AD234BB5B6176CF7C722A3F91663F36EF6C7154843E305431DAA3D4D2C7F328D8BDF9CD44B115DCCC0943D2A5F2F6CA197705039EF4CAB0DF2C066E70BE99A3255F88F01BAF7A42A19982C65811F29A48F3B4EEC92BC614F340AC4BBB6A31036606053239A3E8F97703A9E94C58D9B6E784F5634F91941DC46E7F4038A455877EED95498D86DEB8EF4E662D202BF2AE3B9A8DB3317E4A0E38A718CBC6095DB83F799A909879C91BC8A233B5BF8A9ACD1348AE3722FAD3DDBCB565098CFF176A4590BED561B57FBA453B087AF97BB7A9E314CFBBB5C15451D88EB4DDC8ACCDE5B15C60714ABB1955429D87B9ED4685D80879598E4118C80295BF7814EE5CA8E45246D12EE504E40E0D0CA4AA2AA2B679233CD3201A47F30701108A5606E111BCCF90771C50B1C8EE08C00BD8F6A6ED73C050A99EBDA80B921E64017CB2BFC44F5A6B48D32B2AAE07AAF4A198C9208D0461C77CD226D55647620E79DA70295CAE61DB55A34C1DC4763DA85DA9305DA7E99E298B19914A85C8539F4A56DD1C6ADB76B93D3AD089DC6F924CC8E30067914E6FF005C558E49FEE7F0D2F0AC7CC5DAD8C6734BB470F1AEE23F8B34C08BC81FDF928A93CE97FB9450171636589892564DC38CD376F0DBD76AB74DA73CD4BF26E2C5540C71BA9A83320E1303D0D20B0ADB53FD5752BEB51AC927988A5151BD579A72E771F901F4A3E6551B76E73D73D298EC076EE65621DBAF2290E767185C1E475CD3B681CB30DCDD31CD3107CAC223961D7340C5CAF98A1958606703BD26645DBB401FDD27B548B248EAB95E179269BF2380CCFF002E680D04DCCCA032AC8FFECF6A70DB2646157070477A6068CC846E2801A1BCB55DC0E581A5A806E0ACDB1700F5DC334A9B3CB63F2E0F66EB4B9DBC87238C90C288D430F3095231E94C9B08AA094651C772A714349BD8804C60739EB48DB6411AABE08EA053D5006DAAAD9EE49E28013B2B249966EBC5336A2286072F9E714B968DD82ED1DF38A40DF730A17D4E39A0BBA1FF0023EE3B18B1391938A49643200C3E53F74FA7D6978DC5946E6C603134ACCEC17214AAFAF14896C4550A700191B18CA9C520631AB220627AE0F34D9364726E255C9E8A0D0B32ED2A06D3FEC50C351CAC4282432FF7B1DE8625F6E1703A73D6919848C036E014718A7865690946E31CE68B750117126131D3A91516A5A85AE936B7377A84AB6B656F199679DB80880649FCAAC47870CC083818C7AD7CEDFB7C78D66F08FECEF776D6AED14DAC5E476448383B39661F92E3F1A8D5EC2B37B1E0BF147FE0A41E296F155DDA7C3FD1F4F4D1E0731C53DE5B19A69B1C6F2010003E98AE461FF828A7C6A8DBE6D3F4993D8E9447F5AF5FFF008272FC3DD3E3F873AD78AAF34FB6B9BBBABEFB2C124F0AB9554505B191D096FD2BEBCFECFB0DC33A669E49FF00A748FF00F89AA51695991CAD2B58FCF283FE0A41F1823387D134793FEE1CC3FAD595FF0082927C57E77785F477FF00B7171FD6BF41574DB11C8D374F56FF00AF48FF00C29FFD9B61C13A6D8F3FF4E91FF8565CAEE38C59F9F07FE0A5BF13E35E7C2BA2E3FDAB5907FECD5ADE11FF00829D789D75AB78FC4DE18D34E9EEC048D668F1C8AB9E48C9209AFBC8E8FA5C8086D1F4D71DF7D9467FF65AFCE0FF00828F5BD85BFC66D16CAC2CADEC845A3C6F22DB44B18259DFB01D70B56A2BA9B4ADCA7E90687E22B0F14E8B63AD693389F4DBF884F6EDC64A9EC7DC7F4AB9BF6EE276EDC7A66BE59FF827678E24F127C1FD4F42B962F71A2DDFEEF71C9F2A41C0FCC1AFAA5F1B42FCAA7DAB4BAE864B623281B68DCBEA38E94BFBB2C85C8F4E3D69773364165017DA9556356DA4640F9AA798631956466D8406F4F5A97EE2A1F90535D4F2C918FAE699B4B2ECD9C8EA68DC0503638DE036EA3F79180DDBBFB537CB1B8B65863FBD4E54DCAC49CAF619AA1A119385DC32DD464D1B5F92ECA3DB34BBC7DE64FA739A734619CB6D5008EF537288D671E5F2BC6703029CC6397967CFB118A6B3332E17903D29368918312381DE9D8570F95957039A56C6D1215E074A63280BBF767FD9A73B0938DBB17190C29DD0B7163612B0655EBC7CDC51E4AA2B33039EDB68602455F9C6DA46DA84153F9D480041C49839F7A910B312A540E3AFAD324C7DE66E28DCBB812DF2E2905C732AEC036E5F3D4533E68E426319F5DC682176A95CF5ED4390919C649CD5080485836E4E3B1A76E2D200540503AD30BEEDBBC951DA944AB9DA392690EE3FCC0AD853F7B8A6AC63E73CB37B52E7002FCB48CA141E79EF8A001B0CCA36648F5A62C8159F29C53D942C7C920D2195635C05C9F7A7601DCCA8ACA3033D29199E5C83F28071934BCB2AED03279A4915C639DDED4AC022B0662A4F3DE97E45008CB734F0BB1C0DBF7875F4A6EE11B1CBE4FA531584E24653B88FC295E4C2BA8273EB4AC5E35278E4F4A1895CED5E3140EC47B936A1FBCFEF4FDB9CB11C8F4A06D65E172D8E73491A86872739ED4C02452EAA46EDD4DF93CB2A32C41A7EDF2C0396DD8E94C4DA636F9583503439BF783E63B47414091599907CBC60522C85A3C18C9C1CE69ED3236309C8A928568D6418C723BE69BE5A6E500E69590AB6108DC4534C7B5467716F5A64D872A891B07E4A66E12308C1C81DE9E54236E1C9239CD279688A1FF889A57286EEDAC4672051B03286070D4E60F1B02A01CF5A6B3BA023807AE6991715B0BF3062C5693861BF9C9ED48A57EF36493D4F6A7B31556C85E4714AE58DDCAAE428EA3BD2ED0CA41187F5A41B762B03F5C0A0B1C87258A9FE1C50406DD990C58B7514E558D81523E6EBCD1E72B3670405F6A469037CCAB934C3985F97710170ADC134EF2573BB19DA29ACAAC5776E5A452BBB6E4EDEE6900AEE5F61C6DA4CACC49CE48A56D9B405C9A308DD3856EA4D300571F28230A78269B90AC06CF949EA69E8C366D0558E78148C08C82C06391486382B2939E10F6A6B6D4DA8A18538AAE4B337CA054631237DEE00A57013CBF298E325B1CF3482E07979D8DBBA668F957B905875A10ED7C16DFE80555891F1AEDE376FEE6839556DC7233D1693862AC781DE95181C8C2E075345C351E0B328FEE63EED471B08D8288F03FBD49C36EC648CD4C764636AF4F7A9D4088796A84E0919EFEB4EDC30A55700F5CD394796CE4B291E948AA8EA4BB7E028D40471925028287AD26EF2B68080823001ED418D76EDDD9C724E688D5154B673E94EC161599D9B8181D8528C198971C7AD0EBB896FB9D85216454DA4B6E14C2C12113648538CD283FBC240C638C1A6C8A4AC7F797D31DE97765D482CDEBC52B14FB0AAE0976C1527B9AF2CFDA1BF680D3BF67DF021D5A7852FB5AB96F234FB1738123752CDDF6815EAEB8F9549E4F4AFCC9FF828378D25F137C76B9D183E6D742B48E154CF01D86E638F5E4566DB3297616EBFE0A11F1BF5677B8D3A3D3EDECF76156DF4A0EABEDB8E726A25FF008281FC75B7C1923D3E5FF7F4815F517FC13CEF20BCFD9DBCAF261792DF559D199A356382A8C3A8AFA365B6B79972F6B6CC3D1A04FF000AE88E8B535F863668FCDEB5FF00828C7C678FE5934DD2643EFA5907F9D5B5FF0082917C5BE92685A3BE3AFF00A038FF00D9ABF44069B63D7FB3EC8FBFD953FC293FB234E61FF20AB1C75CFD923FF0ACE6AEF4328C5DCFCF15FF0082947C500C03F87346607B7D9241FF00B357BAFECDBFB765AFC5EF112F85BC51A4DBE89ABDCF169716EE44523FF70AB13827B735F4B2E83A333076D0F4C248C6E6B18F3FFA0D7E667ED8DE094F833FB4647A8E8B12D8DB5DAC3AADB2C436847DDCE31D395FD6A63F16A6952CB63F50999919A3553F292771E2915892ABC30EED8ACBF0AF88FF00E12EF08E85AD2A291A8D8C372DE8199416FD735ADB4F42DB93FBA3B55F32256AAE359060825DBD97B52AC670A9B9907BF7A1542BB10FB411D08A3990062DB4F6CD4B772878DCB271CA81D69BB42C64ED6C9EEB49F2942B93BA94B24783F7B8C119AA42B8C0DB955086049E7753C23C7B950EFE73CD2C85D986D1800678E6A249556424332FAE698EEC9D64766F990367B1E314C3199173B07CC7A134D0C7CB3BDB8634EDD1A856DFBBB6DA90DC161CE43B7439C678A7C986F9C49C83D3DA98AD1ED27392DD8D0235FBAC471C9DB4EE16639A5DBB86D2476C74A4F39D31BB6A9EC4734C0A8AD91B82E38E69CB85E0EDE7BFA52B00E63B98AE158F76C537CEDA0028376704F7C523315453BC16271F2D49C45976752DD7914582C324DEDB9C0CA678CD2C836C4544832DCE29C233CB3901586473491C61593E50C3AEEAA0E5111237DA6355670396F4A67928EA1586E6CE49CD3DA3DCEB82A0B75C7148D0B6DF2C9503AF5E680B0BE5BF9A00E13D334923055CEEDC0B70ABDA93CB505951B3EA7D2956440591579C5031CFB43A970C5B1D298194FCACA23EFD69C199BEF3807B63AD3E458D58F20B63F8A80D0ABE72FA9A2A5F30FA454521E83F1BA4C9DBB40E8D4D6F2BEF10A1FF00D9A5918C8C76A8F6614E652B164ED6734EE1742AC8709F385EDCD3798D4AE158671F377A63DC3B48A19013DB02952775622455DD9C0DDE9523B8E5049C2003DC74A6AB125938DC3A6D1471CB0900427036D3CB31770A07B30A086C612CA76C7B8E3AEEA478CA94DC9B89E7029DFBC57DE5B8EE33446CB29C96C3AF6CD3B06A2AE19BFD5855EE0F5A6346F1A0289839EA6818C1DFF20CE7839A46999D9800CEBD86698ACC9384DAC5B0EC7BF34DDA064875383C822976B2AF033DCFB5344864EABB47F1714148716DDCA3228F6EB4D313B2872CD96F7A5572C0288C11F4A55CC801FEEF0528B8585FDE42FF007548C7CA31CD30BC8BC36D0CDC8A732F960B3743C0E7914797B9946C2D81C339E28DC3411B31CA0165207F0F6A3681B880ABBBD2850122C6E52E4E00343BFDD023C953D568B0683D576C8A0A022983E5DC9B9118FA52F9CEB230906091C50AA82DCB6E565F4C73400DC398C846DC41E79A7B67872984E9C7734CDC891A6C8F3CF34F8FCB7DCAA194AF2093C0A570448A769DFF002863FC3D2BE2AFF829F6B7E57857C13A583CCD753DC150723E55007F3AFB5A14FBA40CBF40C7BD7E7AFF00C14EB53FB4F8F3C19A6039305849230CF777C0FE5445BE86D0B5CFA57F62FD04687FB33F8476AED7BB335CB1C75DCE403F9015ED3B4331DE5B78E9B4571FF0334AFEC7F82DE08B241B7CBD2A163DB965C9FE75DA7CBCF50FD0914E444DEA35A3660518B01D776297CA70C1548231CE0548B10650AC18B1EEC6811EDDC8A8C0E7EF76A8B9921638D7B12C7A1E2BF2EFF6FABE1A8FED31AB400E45959DAC03F04DC7FF0042AFD4D8210B28010A7AF7CD7E497ED7575FDA7FB4F78D99792B76211FF01451FD29F414AECF63FF008268EB8D6FF113C5BA296C25F699E701FED47203FC89AFD03DA173F28273D07F3AFCC5FD81758FECBFDA574784B63ED50CF6C79EBBA36E3F3C57E9CB2FCC4631EBCE2B3EE382D0696DA4AA7E3B86697CC8DB0B8DC4F054F4A452172564E31D08A5C858D404DC4F3CF6AA1C819BCA5194DA41C0C1A42A5B7323967F61446E8CCF98F8EBD697CC0AC0B1651DB1DEB41A10EFF2C97071DC0A39665223C2F7069DE5BCC3EFFDD3C1A4691D99178FF6AA5B011A455F971803A9C5336ABE0862540C5488C37140DC9EA185218DB690B852BCE4F434B610C587CA239214839148AABF7F0C3B0A995983658AE3EB4C998A2807E5E7D68DC43379DC36E0AF7E29FB9A46E3695FA526D77C8DBFA539ADDA35E8C07A60D20B8C65DF95200A552BB8803247F7AA3DAC171B4EECF52291A3F39491C7BFAD524217736D6DCB95A7472091829002E3BD31554908188E39A7F2555781FED1AA18EDFB1B048F6C0A40C194966F9BD288D82EE5CE4FAD0CC19570A4E0F5A450153364FF00778008A4F2D98A2E02B52B6F6DCC0E028A54613370F82077A6318CA8A80632DEB4E5C0F9704934E1F748003303D6A233ED971B73EA6905C56628A54A1C9EF4F66F2C7CCB926A359236DCA4B16EC69EBB31F33723A668013CBFDE021F1ED4A19F69C0C1CD0F2191948236F7E296461BD76BFE028B8AE2862D9321C7A5336A34792DCE73D29CA9B1848C4819E868F3957073919E8051718ABF758B63DB349B9B76438C63A6297FD66F6DDC7FB551BDC146CEDCA6DF4A7B85C7AE7CCC93B948E314D552243C951E942CE24DA1B28BD7814FC6304BE54D201B1CBB30492C2872D86249CF6A7AA9917683803BD3198E1831623B5048CF3246902F41E94F0E573FBBEF49956C6C7CB7BD2AC9B782B9F5C1A0771ED18E1F3B4F7C5314EDC02589ED4D4C83939C669D22BB3FCBF28EB4AE171AC1A6CE49C8F4A37215087AF734CDCC3219FF0011537CBB46D6C9FA501718A7E62A09029E14C8A4EEC0F7A7B64020105BD0546C195C6DE077CF4A0341B867250F207A52A8FBC4AE42F1F3539C9FEF939FEED35A3F9B01B70EF405C04CADB5146DFC29433B3ED53951C9E286F942E0EDDA71F5A7991B7EDC73FECD30D04F9A46E3E51DF8A67CD8298C7BD1E7797C06E73CE453DA468D55B786E7B530561A8739539665E066869063611B71D6963F9A42E5F1ED49B9DDB3B940A45682860AC027CCBDE807703D028ED44930CA1077FB5246DB815384F6A185C4650EABB70A68076B00CBBBE94E69046BD0360F5C535480E5B6E3FDDA0863B9F2FA61734DDE84A8031CE295812A58B6D3D40A1A65C0FE261ED5003D7731F9B098E0714D1F2162C5491D08A67CFE6372DD3EEE294316603601C734F710ABF74B3AE7BD3B21972AA3AF4A6AC6C7819229E54EE0490A00E286AC3B8BB9B77C83EBC521FBC5BEF678A5DCC5BE63853F9D47CC78DAE18FBD5242B8A1BF79C47C1EF8A76F05812300FF000E29242C9805C01D734E6601F83B8F7F6A652B211557E6D8B9E7241F4A37AB310BC903EEE28F9F71215816E848E28DEAA4A9277639E38A571E8246CFE60DE0ECFA5395959986E24FD28C87DABB4823D3D69218D7952A413FC4D482E0D98E3186DC7F9521668E40A3E65EFB7BD395D137298DB1EB5246CA5B07E507A7AD04A1F0C3FBC5C0E841EBD2BF1B7F681D69BC4DF1D3C7D7FBB72C9A94C80E7B29D83FF41AFD91925167637772C08F261924C9F6526BF13DADA7F13F8ABC473A0323C925CDDB6076DCCD9A71947A9AAE5B6A7DD1FF0004CBD4BCEF86BE2FB0CE4C3A845301E81908FF00D96BEC068F003313B97A6D15F0AFFC12FB5006F7C71A7939DD6F0CC07D1B1FFB357DDAD1ED40C78F4C1AA9496E44A5CDB11F96CC0C649556E4EEA6F96D9DC9B8AA9C1C8A976AC8D86DCCC0673E948212CC036ED839EB8CD4DEE4ADC4D857EF3EEE7006306BE12FF82A0687B6F3C0DACAA7DE49ED1D80F4DAC07F3AFBC13F745F6B724F4EBC7D6BE48FF8295E99F6BF83BA0DF019FB2EABD71D9908A86AFB0495F73D5BF64BD65BC41FB38F83A503CC92085ED9896E9B1CFF00422BD6F688F2CD9271C806BE69FF00827BEA4FA97ECFED6E5C62CF52953AF40541AFA5D507036EE23AB6739A49685D92D10E6CB6D62CB8C74EF4C3B1DD48DDC75CD200BB99BE63D80C53C7CDB3627B9CD3B10C45914E7629DC3BD0CC0A8F30B6ECF14EDAD9C46CA09E79A679BE5C78760CE0FD6B4017E58D88E46EEF4B8E14AAF24F7A6F98767CC067A8A76D693682C0328CF141422C7E5B6D709CF7268C85002AAB73D714A9216425D6327A64F6A24665882A3293D680D052AA48DCE303DA9BB86D6C019E9CD37CF590313228DBD9475A5FB4B75041DDC618548C7B6C5D80C7CF6DA7348B226F3BD402D4ADB214071B9BA8C1A6F98D2219368393C0EE2AAE46A2ADC2AE7E55DBD01C50763302E7248C600A14B48A06D181C9CD3FCE58D86F2003D36D01A8C756280AA700E067BD2EEDDD46C18E307AD21705594B1D83907DE8558F04A1C9519E4D2B85D89F22A8F9B9EA49A71543B583F1F9D313CD2C4315C30EE28DCB195C32A76C8A655C77CE59D51073D7B52963E5820004F07348B24B2292496DDD71C52F9615D762907A92C6A4188482CABE580DEB4D8E365DE4E197A0069FB8EE243A87ECA7A53646D922B798BEEABCD326C33C93FF003C851563ED29FDEFD28A2C3B1122488CC0B045C738A6AC8198719751D7B54AC083BF6E1738209CE697E7F9B08BB7BD2B0AE302BE55E46007FB340DCF26D015875DCD46D5645051828E983DE9E7E6601633951EB55610D8D4E0A15550A734DDC22DF1AE5B34795E7285F9B39C934B1332B1C27DDECD40032488CAAA8B8FAE69245457F99181EE69E77796481B589FAD23678206F3DC52B9498ABE57055772668590AB3945E2A1915DD010A40F45A7EEDA86352D9EE3140EE3D9994FCC701872053564DA555376DEA6956405771566C71B6919D638CAB467AF1814C9B8348AA708C4973DE8556E72FB73D5714D055995131953D5AA5662640779936FF0818A91DC67EE4ABE78C1EF4A6362DF28054FF78D39B0CA4ED8C063FC4698C9B9F08C70BFC3DA9922B46BF33141181D39A6EF3BCB321C28C82829AEC2DDD948DC48FE23D29E4BF960860C71D314C687432798C095DD9E8C6914A2B61F6E07DD02876FB887287191B69182C8155579CF25B8A9EA362A332C7FBB50493E94E939650EB85C734864C3846655F40A290048570CC1CBD0089A38C3AC459882A3E5C0EB5F99DFF0502BCFED3FDA4ADAC40C8B6B3B7888F7624FF5AFD3157C3212CC0F40B8E2BF2CFF006ACBE6F107ED93AB41F7BCBBEB6B51F8220FE66AE2269CB63F4DFC2B602C3C23A25A81B7C8D3E04C7D235ABEBD4285DCDFDE069561F2552204FEED027E400A0282012A073DBAD290DAB21EB199D72E7207BF34BB5B850ADB1BAB13D29A50B282830B9CF5E69FE71E4056624FF176AC848B76B191280414C1F5EB5F8F9F16A51AEFED4BE220C772DC7881E23F4F336D7EC3D905FB422F63DDBAD7E375F4C352FDA76E9BAF99E266FF00D1F4BED2468ADD4D6F80B7DFF085FED61A1C44ED16FAD9B7FC0BE3F957EB6DC4612E26465C80D8F7AFC7BF164C7C33FB525DCD1FC86DF5D4907E2CA7FAD7EC0CD30998481B024C364F7C815D528FBA2D16888E3DF0AE7018374069E62759328FDB919E699F34ABF77EEFA1A55660B929B1BFBD9CD6163390EDC396322EDE981D6976C8CA841571D79ED4B1A82CDCAC848FBB8A458C6DC2ABAAFB5031A1774658958FD5738E69ACBB9543B6DDBE839A6349160A38F997B9E6A511B6D52B2671C9C7A53D804552B1B166620F19DB8A5DC163CA1273FDE14F59B716C6E3B78DAC29431DB90DB187DD5C75A5E64B646CDB80DF1A371C0C56178DBC65A2FC35F0CDDF88BC47791D969B6EA4FCCDF3BB76441D4B1F415278F3C79A27C2FF0009DEF897C4976B69656EB9E47CD2B76441DC9AFCB1FDA13F686D7BF680F153DE5E33596836A4AD969B1B7C91A7663EAE7B9A15DEC67ADEC8ECBE27FEDC5F10FC55E26BB9FC31ABCBE19D103E2DEDA04566DBD8BB11C93D6B8F6FDAEBE326DFF91F350F4FF5717FF135E4CDD3180171C62BDE7F659FD97EFBE396BC2FF521259F84ACDF37138E0CC473E5A7BFA9ED44A096E54972A3D9BF645F177C72F8B1E238F5AD67C4D78DE0CB4622E24B98A30273FDC4C2F27D71D2BED9C06CBE495F5ED553C3FE1DD3FC33A2DA691A3D9A69FA759A6C8ADE1E1401FCCFA9EF9AD011ED527FD5A1F5AB72D2C82298CDBC9DA01E3AD3D829DA5172C07348731EF09B5C6334C6253041EBD76D666A4855A40AC42807F3A8D9FC9723E66403A8A7B49E67DD2060F534D6675DE3868EA856626E4E0AEE604658529F2F236A9008E7DA86C060546DC0CF3D29EAE547CD8CB7A50C63248D3CA0118827BD3644640154AEEF6EB4ADE5C6A707273F2D28C7CC55C7D7BD2401B0EC076FCD4E674DCA597F1A8D6623285FE6F5C539785CB38FF80D3B7710BB97EE86F97E9418D4AAE24C1F5C53973C82300F46C5382A2A8CB679A5A20D48FCB32024BF7C66976AB0019B1F414E2A7938F9739FAD1B8CA00D9B4D17013780DB18823B714A15A5503A27D285914B11DC7AD38068A33B9B83E945D80C76CEC8C104FD29AB889D779CF3E9526C5DA02B658734C5F9812C4F5E3DA81682050D2920955A702F1C646372527AA6EE7D69518AAED638145C2C31F6AB2955E4D355579207CDDF9A9B858F850C334C3005C91C9EB4C2C394968C0238CD35F7890ECC95C52B36E6000C0EF4AB95CED3C9A07A8CF283644639C739A77FAB8C2F1BE97685624F1C76A6B47850D8DC7D6810ADF2924AED63DE90A854C79990DD7DA9FB43305237102A350172361FAD48EC0AA61E23627D38A5656390481B85296F2F68E4FE146D072C5B3CF35416230DE49283E623B62A412A8604A90F8E94EE7717538005315B08198E0E782D408048197053058F714819202D9391F4A91999B3228DDB7F2A6AB99D402B8FC296A21994766666F948A70FEEA292B8E7349959320103DA9EACEA55036E1EBE940C6FCDF2ED4005249B3962791C6053F95623CC1CD46AA8CCCB82DF4E281DC746BF293C6D1CF34992BB972327918A77108DA0673C9069B1E59723E56A603561655058EE39E39A4DC246395DB8EF4E90FCB96FE13CEDA63306C0CEDFAF7A621549F336890AE4FDEC66BE19FDA7BE2E7C72F82BE369923D757FE11ABC72F63731DA214DBFDC6257861EF5F728937E57A63BD61F8D3C0FA2FC4AF0EDDF87FC41662EEC274DB9618643D9D4F63516644A0D9F9A31FED9FF001915B77FC25471EBF648BFF89AF57F82FF00F0500D7ECB598AC3E20F97A8E99290BF6F862092439FE2200C30AF16FDA03F67FD63E01F8B1ECE7592E742B925ACAFC0E1973F75BD180ED5E5CD6EB2F56C2F5CD6918A4EECA71E58EA7ED268FADD9788F4A8354D2AE22BCB1BA40F14D1B655C11EBDBE9574A05DAB95DBDD876AFCD7FD943F69EBAF837AC47A1EBD235CF852EDC0F9BADAB1FE35F6F515FA4FA7DD5B6A9A7DB5FD9DC25DDADD46258A684E55D0F208AB9596C4C657D07642924296EDC8A6AB46ACC32493D2A5DCC73FDDE83351ED0093B379FAD657B9AA1379661BB705CF069FB70C701893CF34DC630C777B0CF4A55FBA58EEF4EBCD35616CC029841EB9EB48D1C92C8BDF3D68DCC54AE0E0F734A0F96BDC0ED83D6A4AB929511C78656E1B9A95635999485C63D6A28D97CB66707DB9AB11E372C982001CF3D687B08E6BE286A9FD87F0B3C5BA82B1060D32E1B71EDF2115F951FB39E8BFF09078BBC46A4642E897921FFBE0FF008D7E977ED4DAA7F66FECE7E3E9D4EDCE9ED18FF81102BE08FD887491A86B7E33B870311E853459F76C0A71859F3333937D0E8FFE098F7E60F8B5AF5A6EC7DA34B6E3D76B035FA34142A020EE3D715F995FF04F9B8FEC9FDA485A671E65ADCC27F007FC2BF4D5B11A70DF3E2B493348C6CAEC5DDBB243019EBC526DDC42025BB9E68D8240BFBC19C741C505559800D86EE6A03A8BE60E9853B7F848AF9D3F6FCD2CEA7FB36EAD29183697704DF4F9B1FD6BE8B1B463F8C1E076AF24FDAD3471AA7ECE1E3C876EF2963E7FE28C0FF4A71D58D9E15FF04CAD516E3C0DE36D399F260BC867DBECCA47F4AFB29BF799080263BF7AF80BFE0983AA05F1278E74F27996C629B1EBB64C7FECD5F7E48CBB064E467A2F514728D3B8BE67DD457DCC783ED47CCACA0F200E686C020B8071D36D2615983839F63D2A599BDC3E4DB26CFBC682C9F75132D8CE47AD23168D7E52AA5BA714FDACCC0A15E3AE29A191B48DE646A63CB629008F7A92705B8229CAD27CE36A9C1FBD48BFC4A63DCE3907D282AC2AA8590EED853FBBDE9777CBBA38D48271F4148AF1065F93E6E9934E58DBE720AAA8E085E6A6C4B7613CBDAC5511413C9A51F2A888B26EEFC73491C3B559D9F776C8EB42AAB30907CC7B29AAB1371FB9558029BB8C6EA40D1B1049F9B3FC3C014ACAA2438077E3A03C53541DAC0AA839EA69EC02888B492625F968485594617CC2BD690C815B6923A72CB48FB23C04DC3750171EDF2B05CA95EBB7D4FA533CA6F319D1157031D7347FAB8B85DE01CE4F5A746EAC17E6D83BAD08639D8C9B4075C74E95118A3590E0FCC475EC29DB57B65761E5BD69308E9B4024E7340840DB23652CE4638C0EB4A237558F232BDF269CDB7730653C0E02D304BE601BE361E983D68B0C7E22DC770C374142B244CC54060063EED39986777DDC7453512CA6652376C6CF618A571EA49F688FFBBFF8ED14DFB39FF9EB451A85C14AC8AC5DB61CF4F4A555CABA99729D697CC85886C0049C63148CBC33AC7819C014C41BD245541BB19EA297CBF2D982BEDC8E49A5675D8155546698B1AF98007DD9EB9348AD051B9A31B5B2D9C60529568F2370518E41A568C236D528A71F8D2471B6D2D2907D6816837CADB20209298FE1EF4A632FB5508519E4F7A74720429B18845E4F19A8FCBF364F312438EA4914AC2B8C686E187320501B8FA54C4BB2AA87007AE3934A55DDB7364A30E4531B7B478886D00F19AAB0AE4AFBF76D575538E4639351466E3CB21CAF3CE714BE5B22AC842B3FA8ED49249B540F339FA522AE34ED8F6940ACD8CE7D29D6E9E665983671C81C53D771002ECCE39C8E6860D33120EC18C75A4D80C90158F20A9E7EEB76A197CFC6CDC7B9DBC522322E32391F78E334E58F7B92246031D7B55009B82A31F9719E8C79A70DAB890B1E46300714D31AC6ABBC162C3AD48244455CB305238C2D272EC088CC89161C93BB38A41FE91B58B6D19FE214EDACC58B0CA9EE0528DFF2ED5CA0F6A43B82C91AF9840CB7A1A554565E17F79D42D2EE64DC5973FED6DA7ED6DA09605CFE94363248D64678FB0CF22BF297C54BFF000947EDC97800DE25F14247F948ABFD2BF57ADD4F9EAEEA576F3F5C57E4EFC1776F157EDB1A7CE7F7826F134D39FA09CB7F21531726EC8B8C53D59FAB33B335DCE718018D3235E853E6E7AD2CDB84F21EC58F3F8D3576C6ABDCFF00B356CCE4C7A96565665F93A106A48F0DF346B8C9E734C4C328CFCC73F74D00FEF06D6000EDDAA4225BB76585B2BC61493CFB57E36F81E33AB7ED376B9391278899FF002989FE95FB1524891DBDC48BC621727F235F8EFF0004CFDAFF00691B093390355965FC998D6B1B2D46EF2D117BF690B21A5FED077F72A36896686E14FE5FE15FAD3A3DC7F68683A3DCAE184D67049F9C6A6BF2A7F6BE8BC9F8A5617038F3AC91F3F4622BF4EBE12DF1D5FE12F82AED7E632E8F6C73FF006CC03FCAA9B6F51C62D2BB3A862572405CF752714FFF0056DB4C6A0673BB7546DF236D3F30619DC474A5CAB2B0930C7AFCB9CFEB58B64B15EDD8B3171B09E432F4A60CC8A546E6D9DF38CD3E17DCCB8766C0E558527CFB72ABD09E3D450B4109BFCC505548653C8C53B0B231724AB0E08C501978DAEC8C47231DEA7590E119995B8E548C1A44B63708558ED2327E53EB59DE29F14E97E02F0DDFF887C4376B67A758A6F9266201C76551DD8FA53F5CD734DF076877DAE6B375158E9967199659A46E1703A01DC9EC2BF2F3F69EFDA6755F8FDE247B6B57934FF0959B7FA2588381273FEB5FD49EDE94D2E6667D6C8CBFDA4BF68BD5FF00686F15F9E4C965E19B272BA7D86E3851D37B7AB11F957912F4500954C600F4A438E768000EBEF5EAFF00B3DFECFF00ACFC78F16C7696B1496FA15BBEEBFD471F24499E80F773D8574691D0E98C6C5DFD9B7F671D53E3D78A550EFB2F0ED9B2B5EEA0578033F714F7635FA97E1BF0EE99E10D06CF44D26D52CB4CB48C470AC6A07038C9F527B9AA1E08F04689F0DFC3369E1FF0E5AAD9E9B6ABB76E3E690FF13B1EE4D6F2E19594B003B5673D4896AC1B390EACCC47F74F5A8F2183B3BE03763D454990BB4B7CBFEE9A647186F30803AFF15623B0D58C23001D4061DE9E182AE368600F5A610CF8276EE07EEE29ECAFC155C6EEAB56801942B30D8006EF4DF2555597825BA734E0E64F97CBDAD8E86A3924081519307A6695EFB0C709182B2EC191C633DA939870D8C8C74A42A0B6021E463752C7BEDD76E0918A76EE489E6C6541F2C64D2B2F96C1F0B8FEE8A7B292AA7CAC8A150B6EF94A9A57EC01190EC4ED0A08CE69A06D6C00A0377A97E59971BA9D243C618607B52D43618631F7B7EE514E21032E7B8A4555DBB40FC4D3E45F254B4EEB0C6BC6F93E55FCCF14F9408554EDC9F947AE6946777DECAFB570DE2DF8F9F0CFC0CB22EB1E32D2D264EB6F6D309E5CFA6D4C915E35E2AFF82857803480E9A1695A97881FB3941027EA73FA51A225BB1F50955552CAA49FA54604842AF520D7C11E20FF00828E78B6EF7AE87E14D2F4D5FE192E9DE661F8020579EEBBFB6A7C5FD78301E208B4B43FC3A7DAA21FFBE8827F5A5740B53F4FFECEF92C1581FA5579EE20B5199EE61887FD349557FAD7E42EB5F18BE20EBCCCD7DE34D6E7DDD57ED8EA3F206B99B8D5B54BD24DD6A779704F5F36776CFEB4F95F619FB1779E35F0CE9EC4DD78874BB76EFE65E460FF003AC1BCF8E1F0F34EDDF68F1968CA7BFF00A621FEB5F90ED187FBFF00313DDB9A16DE2FEE007D80A14642E647EB05C7ED3FF09ED1B1278DF4C0476590B7F21551BF6B6F84099CF8DACBF0573FFB2D7E5679283B6297CB5FF26AB958F9BC8FD515FDADBE0FB71FF09AD9FF00DF0E3FF65A9E1FDA9BE11CFF0077C73A683FED161FCC57E53F963EB41897B8CD03E65D8FD68B7FDA23E17DDF09E38D1CFF00BD7217F9D69DAFC5CF025F63ECFE30D1A4CF617C95F902D0447AA03F85466CE0273E5AE7E945AE3E68F63F676CFC53A0EA1836BAE69D3FFD73BC8CFF005AD58D63993F752A48A475470D9FC8D7E26C71984E62668FFDC623F956AE9FE27D7F4B606CB5DD4ACD8721A1BA75FEB4F90AF759FB422D64E06D381EA0D30C6D0E495C926BF23F48F8FF00F13B43DA6CFC79AE2EDE8B25D175FC9B35DBE93FB6F7C62D27687D7AD7535FFA7EB28DB3F8A806915CABB9FA73B4F2DD07A0A4727E55C657B67B57C05A1FFC1473C656855757F0BE91A828EAD6ACF0B1FC0922BD1FC37FF051CF0B5DB2AEB7E17D4F4D07AC96EC9328FC320D5F2D95CC6CDBB23EB578C3311BF0314926554173951D8578FF0086BF6C3F843E262AA3C596FA74ADD63D4627848F6DC46DFD6BD3B47F187873C4CA24D275DD3F528DBEEB5B5CA383F91A92F92469C922AE46DC647A74A6855F294EF3B7A9C54EB06D0CC0EE278F514D68D9B0A57E4A44A220996073FBBF53D69CD0AA83838F4A7887EEA6DEB4DF27AEE24E6A2E0466431B81B723D682C583670BEDEB4E7CAAED1C11D01A8C8744F9B04FA9AA44846CD809B7834B2C64ED18CE3BE29C4B300BB82BFB50CCCA176E49EF40C6B6E6C9F9569A57746A1C927DA9DB46D3B803FCE9571385DDF2E460D4A34DCE73E207C39D13E2BF84EF3C3BAFC0B25BCEBFBA94FDF85C7DD753DABF2C3E31FC20D67E09F8DAEB40D5A22D0862F6B78A3E49E3ECC0FF004AFD73DA4600F94AF46CD71BF17BE0FE81F1BBC1F2E85ADC4B1DD202D657EABFBCB6931C107D3D47BD3309267E42C90AC8B83D7A8AFA5BF64DFDACAE7E13DF43E16F134D2DCF862E5B6C73124BD9B1FE21FECFA8AF15F8A3F0C35EF841E2EBAF0FEBF6C619A3F9A19B1F24E99E1D0F715C75C0122FBF6A6E0E45AB23F6C2D6E2DB51B482FAD2E12EACE54124734272AEA7A107BD37CC2CC71F77DABF3D7F649FDAC9BE1FCF07843C5B3BC9E1A964020B973B9ACD8F1FF7C7F2AFD080F1C96F15C5A4B1CD6B3AEE8E646C8653C820FA54D9A762DBEC488163209E47A5236D79371F94522E304E770EC68182C3A15EF9AD2C66359FE63FDCA15C2EEC0CA7E78A7850C768C6DEE69E20DDC27FABEE7A669F2E84F3243D15303397CF3C5598B9C1C36DE9CD555F97E50B923BD4F1E7825B8F4CD458B4EE785FEDC9A80D3FF669F140070263145D7D5C57CA5FB0B5B793E1BF1B5E63E694456E0FD4E6BE88FF00828A5FFD97F6779A21FF002F1A8DBA71F527FA57857EC570F95F0CB5A900E65D4635FC8569656B9A46C99C37EC8B27F64FED8D6B6F8C6EBBBB871F50D5FA84A073F2E5BB7BD7E5A7C0E7FECDFDB86C97A635E923FCC91FD6BF541D42C84B6540A5CC9AD0B9BB10F962450C42AB0E76D34108E191777AE69EDCB06DB91FDEA1A421976ED3EC2A0C56E2464AC84B720F02B96F8C1A6FF6C7C23F1AD87DF33E9172A07BF9648FE55D6C78918103E607F8BA556D62D86A1A2EAB6C40226B59A3C0E9CA114A32D4BD3A9F9B1FF04D3D4BECBF1BB51B33FF002F5A45C2E3DC6D6FE95FA4E9B972C46771F4E6BF2E3F601BA6D2FF0069CD2ADC9DA648EE6DC83EF1B0FE62BF52DA2F98618827B56ADA26FD1102B15760C9B8F6C53D7E45C04E18D39982B6D04EE03D29A3F72CA77EE2C7A63A564D92C388E4208E5BA71D29AA3A176D98383F4A7AEEF319A42401D0E286DED1E5DBEF740476A2E04724C5418D5770CE73EB47C914870CC4B0C918A1A6656285FE623A63A52C6B2AA8DA0940739C531DD8B1B6230563C367A52C96EF93D107734BF34CC72C1334D58CC8B82E593F88D2D491ED6ABC846C0C64EEA64A11958F2587002D2FCAA181C963F741F4A4693C952221F7BBF6A68072C69E5AF0EB21A7796CE0B30C95E0669B93205DC4A8038DBD4D3131E5672C587BE69E83B127CCC846C1B88C6314C1E6862AA5490BC29EB4BB98B0906E2DD8638A017995B2B92B40EC0AC598E5B2368C0F7A71FF00485DA595029E4639A3CCD918DBF29E9C8A6B2A3EF193BBB9A4038218D8A20DF9E739A460ED1F51BB3F4C52347B5418C91EA4D289563763FC5EB4C03CB6DA4C7F7BB927AD38C2CD82DBB0A714C2A645DAAC59C73C51B6458F680783CE4D26C7A1336FC864DB81C7BD40CB24C76BC7804F069CB1EC936E59C1E4FB52B37999058A853D68891717FB3D7D4FE74541B17FE7B7EB4550132E5E42A555429CE6858E4924396F9734CDCCBB805126E3C9CF35334832CAC3E7C602D49443944B8200E31819A568FCC5542BB4E69CDF2B27233E98A6317690642673C64D3B08722F9327CDB557B13C9A593E65662DCE790052342563C1605F39C539777CBBF85239C51B0C58DC346A536A0E87752CB2148D767CCC7A8A6B2C52653EEE07534D58D62F2CF9A493D4629123CF58CB336F3FC348AAD197DCD85ECA297E6E031C876E38E9448BE5AB311B9C9C6D26A8AB10EF124641F946781DCD3D9845D11885381914E58D64EA550AF4C75A4696432088B719F4A9D87A0D6658D83EE2DBB82B8E94F570CBB429C9E714A372BB33E0E3A0143E647CC7C7CBC5400A91EC8D986DC3F1D7914D568DC90D90318A6952AAAC570DEE78A918B30F336AE076A648C8D9118E79207A53E360D1B386FC3D29CCA561777F2E34C7DE660A3F335546A163F2AB5F59A903A1B84FF1AA4AE1AD8B31B79919DFF202785A4689923180769F43CD22DF5948C3FD3AC891D85C27F8D4CB736CCDB85E5A9F6FB427F8D333E6447B47CAAC59837E9522A8571FBB2C9D01CD2AC96E5B3F69B5C7AF9E9FE35246D12B0FF48B761FF5DD3FC6A5C4D96D720BDB8FB06937F72E594C36F248013C70A4D7E56FEC4B6EDA87ED59A24E792B35D5C1FF00BE1CD7E9DFC4AD52DF49F863E2EBB92E61061D2EE186655CE7CB6C77AFCDBFF827B5A9B8FDA4ADE5233E5585DCBCFF00B847F5ABF8519CA4DFBA8FD389182C8F905B93C52283192DF28F6CF34F6C31240DDEA2988AA81B3B49CF0476A9F32FA07CD22160429CF4A7A90CC14A803FBDEF4D78D6452CCFF77FBB522E1C2AA8E4F66A435B14B5D90DBE89A9BEEF996D25240F6435F90FFB3328BCF8F16D21ECD3C9FF008EB1AFD6EF1938B7F086BAE7829A7DC1CFFDB335F92DFB24C666F8D1037FD3099BFF001C345EFA1A46E74DFB635B1FF848FC3575D3CCB4653F8367FAD7E86FECCD74750FD9E7C07203C8D3963FFBE5987F4AF823F6C8B5FF0041F075CFA9990FFE3A6BEDAFD8DAF3ED9FB32F8398B67609A2FCA56FF1A9726A5625B6CF6949170519B6E3A8C64D0DF3B9E3CD50323B1A648CBF2F9842FA14EB4E50CD3651B271C60718A6403C67CC53186518E4E69049B87965C75EBD0D288D901472D96E46D1D29461946E5F31579DDDE9FA92DF4427929BD4956FF781E334DD4B50B1D074AB9D5B56BC8EC34FB34324D71370AAA3F9D3EEAEAD349B1B8BFBFB88ECB4FB7432CB3CAE02A281C9E6BF33BF6BAFDAAAEFE33EB72787FC3B7135AF83ACDF0304AFDB581C6F6F6F414AF7D110FB2287ED55FB52EA1F1D75E6D234B67B3F0758CAC208470D70C38F31FD73DABE7E503EE01841CD18E8A0702BBAF831F06F5DF8DFE30B7D0F458CAC21835D5EB83E55BC7DD98FF215BC63CA8DE10B1A1F01BE06EB5F1EBC611E91A6816D610E1EFB50917090479EBEE4F615FAA5F0FBE1DE89F0A7C2367E1AF0FDBFD92D2D572F2701E693BBB9EE6A1F863F0C740F845E11B5F0F787AD634810069EE768125C49DD9CFAFB76AEAE30C995CE76F50C39C5484A5D10C3FC2E1720F190694C8AD9563F2AF3D28F9E3505410B9EC78146373361918375AC9B248D95182E064039E28B86FE103E53FC43AD0ACD1332AAEE5C7F0D2825A13D36F73DE8103AFCAA01395FE2C538A07DAE927229199A18C61B72B74A468DB2AF855C76140C3FD62B32B61F38E69B1A9DEDBCAE40EF4F5666619000EC7B539A31272DB573E94F610D556D873C1EC73D69CAA3F89B9E98A4F2CAAAED1BAA40A0B7CCB83DA96E0355587F1E47A53BCBDCC48249F4A425228E496778E18635CB492304551EA49E057CEFF177F6E3F027C3769EC343CF8BF5A8F20AD9B016B1B7FB529EBCF6145D225C923E8D4B5675DC0602F56E807B93D2BC8FE267ED5DF0D7E16ACB06A3AE8D4B524CAFF67E96A269770EC483B47E26BF3EBE2A7ED49F10FE2EC92C77DAC4BA4692E7034CD358C5163FDA2396FC4E3DABC9D621FC44B9EE58F34BDE96C45E4F63EB8F1F7FC1463C4DAC34B6FE0DD12D744B6E8B757C3ED1707DC0FBA3F5AF9DBC5FF183C77F10A677F11789B50D4118E7C9698AC60FB28C0C7B62B9311FCDC0E2A758C7A56BA25A9A5FA10A5AA0C92BC9E6A458D57B54980290D446376272B0838E9451484D6B18A447BD2170299436714DDC17A9AD115C96DC0F342AD21993FBC29378F5AB2B4449C5271EB5134CA0F5A4FB42FAD269171D49B6D2E2A01709DCE3F1A779CADFC552E371D8936D285CD47E60FEF0A7AB7A1C9A5CBD8C9BB0EF2E90AE3A53B269D50DB44DEE43ED4AB8EF4E65A455A12B9AF3596A05734C6507B73536D14C9176D55D5AC4C65AE840D0AB75A218CDACC2682478651D1E362A47E229C5A9CABBAB3B1B731DCF85FE3D7C49F07EC1A578CB568A35E9149379A9F9366BD63C3DFB7F7C53D1D512FC693AE22F04DD5B1463F8A11CD7CE3B76D1FC5552DAC8C9DDE88FB474BFF8296DC2855D5FC030BF63269D7E57FF001D753FCEBDCBE037ED51A0FC7ED4F50D374CD2350D22F2CE113BADEB2302A5B1C15AFCBD65F9457D2BFF0004F4BB36DF1BF51B70789F489B8FF7594D4463D598ECF73F445C9C92FCF1C50C9E60C93C0F5A91976E768C9A8F71DC448A40F6A4D9B58629650C700E2A4CBB2A91C63B527CC47C8BF2D260CCC30081F5A943B0AAACB96F94E4F7A566DCBB42827BB52FCBC1CE594E714A1DA31BF628DDEA69827611995B1C720F2BE943655F270AC4F14A176A365406EB9069A32CA0B2E1B3EBD2916CF3AF8F3F04746F8F9E0D7D2B5102DB54B505EC2FC0F9A17F43EAA4F515F957E3CF046B1F0D7C537FE1FD76D9EDAFED24DA411C3AF6753DC115FB32CBF38C718EA7D6BC97F687FD9DF47FDA03C2A6231C767E27B552D63A8E30738FB8E7BA9ADA32E8CC9E87E4DC996F5DDD8D7D67FB1C7ED652783EFA0F0578C6732787E7611D9DDC8DCDA39E0027FB9FCABE6BF19782B58F87BE24BBF0FEBF66F63A95AB1578E4EFE8CA7B83D8D60490771C15E86B5D049EA7EDFCD6F1C68A63759A1650CAD19CA9047041EE2A0F242A8DD904F4AF87FF00635FDAF934FF00B1F80BC6B7CDE43308B4FD4A739F2FD23727F87D0F6AFBB25508AAE0655F956EB91EA3DAB3EB61BEE5355F2F1F3E4F7E314F60557EF13E9493121B2DD288E5055B0323A5599B8DC6C08D1EE1B8B6EF7AB71E3B741D6AB7DD2368C9EFCD4F248628C150307DAA2438E9A1F27FFC14B6EBC9F82BA2420F336B0831F48D8D798FEC7B1F97F09653D0C9A9927F002BB3FF00829C5D9FF8417C156BBB3BEFA694AFFBB1E33FAD72BFB26C463F8456A7FBF7EEDFA8A5CCB9523A62ACDB3C87C1930D2FF6E0B693B0F1301F9C9FFD7AFD5CB853E74833CE4F5E95F9277D29B0FDB2164E857C45137E722D7EB75EA9333F619E71F5A8714AED0497BC55C7214B63E94471F96D90460FAD1E66E708BD29363370E385A933EA3A31BB201F9B3CFA54F02AC92940300AB29CFD0D57C6D5DD9EFDAA7B77632444E0127B566D31B3F297F65561A1FED89A4C4C701758B8B723FE04CB5FAB170374CD18520FAE715F92DE04D417C2FF00B6879D23AC51C1E2A98166380079EC3FAD7EB049AC69F2CF2017F667E6C0C5C273FAD5C69CAD764277D121DBFCB6DB8DE4F04D31546E2CA385F5EB4F8E4B665CA5CDB93FECCE9FE35224716EC896263ED2A7F8D3684DDB721DE577C8EADB7A60D23614AB052C31921AAC7D9C312DE646467FE7AAFF008D396D55F3FBC879FF00A6E9FE34EDD4B5B5CAD1EE936CAB924FF7978A5F31E663E8BC1038CD4ED6AD23011BAB7A2A3AB7F23514D0B42E4487C900F009E7F952DC922451858C10D939F9BB5232CABE600C36E78C5058BA804EDC1EB8A6AED462C9213DBEED5013EFD8CA776E908E845340768F05B6F3D4F6A8D42AEE265248E9814E752CC0097391CE45218E92EBCB603EF1C6036282C23236A63B93EB42B79A480E32A31C8A6EC2C7CC67E7EEFCB40EC3B7BBB06F98293D0F028767FBAA78CD2BFDE182CCD8E14D44C1FCB00B1241E540A04C7AC6C8FB81C28FE26E452B798B21C00C08EB4D2C22611A863DF07A5395C1DBE66EDDEA3A50218BFEACE1F7367F0A72E5986762F1F780A6808B9655F941A558CCAC18AA8FD314CA48779ADBBE51D780CB4DDA5242EC46D1C9E7AD3108859B731700F1814FCA17298E08CFCDEB5255872BEE5CAB0EBCAD2AA943B5A452A7F3A6792AAC4F965571EB4E5223DAEFF31F403A551361BBBD87E5454FE741FDEFD28A076215776CA1501BAFCBD68108256460C589A53BA0C95C33E327148236DAB23861DFAD03056CB17C107A7229ACA5C06C2EFCF14E6578E42DB8ED3D0523CBB645053B71F5A09093CD4667E9B474C75A466DAAB230607FBB9CD2A42CD231766191C5244CCBBD029C7F798D0CA246CB05554C96EE699196DBB7661C1EBD714AD0C9B576CB9FF6734BB4C6430903161CFB51B058396505DC900E785A56C49B9F61DC4E314DC92A1A43B777031491E1957748CCAA7938A5706849146F2C83A01F9D3BCD2CC37EEFF808A4DEAC484DEA09A76E10B840E4FA7BD325322DCCAACE1885E9C8A923D9214196000E3DE91D5F96233FEC83C52173232828101FBA69586C906D9B20A93834EBCB882C2D6E2F6E5D22B5B689A5959BF8554649A723F984212381CE2BC7FF006C1F1737837F66EF195E44E639E6852CA36CE0E6460BC7E06A6CD93CAFA1F097C58F8C9E3EFDACBE30BE81E189EE22B033341A7E9F6B298D046A7EFB91D49C6493EB5D45AFFC13C7E2CDCA069FC476303E3255AFDD88F6E2B73FE098FE118EE3C49E2CF134D1877B4B65B685F1CAB39C9FE55FA01B4B381C0CFAD6918F296A3C87E74FFC3BC7E2B2F4F14D8FFE0649FE14E5FF00827DFC5C51F2F8A2CBF0BE7AFD14C8DBB881D70052E0A7C8BB58F5E9D2A168CC1C2E7E72C9FB007C645E57C496ADFF007107A8CFEC0FF1B95709E218187B6A6E2BF4795CB293C0EC3229EADF36DC8C8EB57CC6F16D47951F979F12BF641F8C3F0EFC07AAF88F5FD505C689608AD70B1EA2D2E4138C6DFC6BA9FF00826FD9A5D7C72D46E106D483469CFE6C8A3F9D7D63FB6C5E1B7FD97FC60A1B6890431FCBC67322D7CCBFF04C3B61FF0009C78AEEB1931E98A99FACCBFE15949B6EE528A8AE63F421B0C70BC1F7A6ED20328298A732B48B9C823D298DF326DDBB37700D3321D858C900314F41EB4FDA3EF6704F03D6A38A331A0DDBCFAB0A95146EDE09E3D284547639FF0088CFE57C3BF14B152A174CB939FF00B64D5F951FB1BDBF9BF17D1B1F76CE63FF008E57EA7FC5993CBF853E3194E46349B93CFF00D736AFCC1FD8B63FF8BA53B63EED84C7FF001DA77E5572D4AC8ED7F6C8B70BE09F094C7EF0BA957FF1D1FE15F54FEC1779F6CFD99F465233E4DEDC47D7DF3FD6BE63FDB2A2CFC35F0CB63EEEA0E3F38CD7D13FF04EF984DFB3C3467FE596AD2AFE688692B5DB7B99DCFA586461800547507AD49B4150E18938E406A5551F29CE17B8229CABCEF551B7B95146E2123CCBF75CA1C63E614EB8B982C6CE5BDBD9E3B5B3810B4B2C842AAA8E4924D3A59238619279DBC9B7894C8CEC7014019249EC3DEBF38FF6C8FDAE9FE285D4FE0CF084F243E19B790ADDDEA1C1BD60718047F00FD68DF4443EC8ABFB5F7ED713FC55D42E3C21E1299ADBC216EE565B85255AF9C1EA7FD8F41DFAD7CB8115461463F0EB4E58C2A0551C5749F0EBE1DEB9F15BC5769E1EF0F5AB5CDDDC1F99C8F92251D5D8F602B68C544DA10B13FC2FF85FAE7C60F185A787B41B669A695C196723E4863EEEC7D00AFD54F835F08744F821E0D8740D117CD9586FBDBD703CCB897B9FA7603B554F81BF03F44F815E118B49D3635B8D4A5506FF0052DB869DF1C807B28EC2BD19319DAC4B3F6C7A50D95297443963DB8C9642DC8E78A469BE660E00C8E188A4914A48ACAFB5070334E6FE212B1C9E840E2B2DCCEC3559559630D85619DDDA95A3560E898DC3906964CAED50AAC3A67B0A46CAC84328C11D5682B41D1385608C42B639229A631B72A4A7A861D699B576EDDBF39E849A73C8E8B1939231D3149885D8BB19BEF293803D297CA1F210DC50640A48006D2324D1F2AAAED50476C50486448A406E169DB1766D2D9EFC1A4551C6339EE314E8E3330DA23CFBD1B087AA80C02939AF35F8D9FB42F847E0569267D66E85D6AF22E6DB4B85879D31FF006BFBABEF5E51FB537ED9165F0AA39FC31E10956FFC58CBB65BC521E2B21E9FED3FF2AFCF3D6B58D4FC59ABDCEAFADDF4DA96A374DBE59EE1F7B31F73E9ED53ABD1193937A23D5BE347ED59E39F8DF33DB5D5CAE8DA00398F4BD3C955C76DEF9CB9AF1E8E154E71927AD48ABB69DF4ADA34FB971809F5A5A5A72FCD5AE9146C924B41EABC50580A08DAB5130ACE2AFAB25ABAD0928A6EEE82B43C2FE18D63C71E21B5D0F42B19350D4AE4ED48A31D3DC9EC3DCD56AF44428756663CAB1F2C702B6FC35E0BF11F8DAE161F0FE857FABC8C71FE8B03328F425B181F89AFB77E0AFEC27A0F86122D4BC77247E20D4C807EC09916D11EB83DE4FE55F51E9BA4D8687630DAE9D6505859C23090DB462241F80A7A47734D1688FCF0F06FEC1BF127C49B5F557B0F0FC2DC9FB44BBDC7FC056BD5F42FF8270E8ABB0EB5E32BE9DBF896CADD5067D01627F957D8CDB971CF079F97B53573E705E48EA1A93992D5CF9C34DFD803E165830FB436A9A830EBE7DDEDCFFDF20575765FB1AFC24B1518F0C2CD8FF9ED75237FECD5ECAC383FC2F9FC29D931FCAE7EF0E0D4B9B62704CF2C8BF655F8551E31E0CD3FFE05B9BFAD49FF000CBFF0ADB39F05699FF7CB7F8D7A715DC846ECE0726955B014EDF9718A5CCC71F7763C966FD93FE12DDE54F836C80F58D9D7FAD626A1FB12FC23BC040F0EC96C7FBD05E48BFF00B357B9C6A62CEEC67A8A91BE58C74249CE28E766AA4CF98355FF00827AFC38BF563677DAD69AC7A149D6403F0615C2EB9FF04DB0093A1F8D98F7097F6B81FF007D29FE95F6D460CDB98A9DBFCA90CE368D81B19C1A7CEC992E63F37BC49FB06FC50D077B594561AE463EEFD967C31FC0D79278A7E13F8DFC13BFFB73C2DAA5846A70647B6629FF007D0047EB5FAFDE7796F8C83DB91448C594A39DE3A32B721BEA0F5A5CDDCC1C1AD99F8A5E726EC370476279A716E320D7EB7F8CBE077803C78A46B3E13D36691860CD0C221907BE5715F3FF008E7FE09DFE1DD495E7F0A6BB75A2CAC72B6F7ABE745F4C8E4535322D2EA7C242924E6BDA3E20FEC79F137E1F2C930D2575DB15E7ED3A53F9D803B95FBC3F2AF17B88E5B2B8682EA192DE753868E552AC0FD0D1A36353B106DDCD52A2EDA6EE1B7208A37E6B6B26176C91BA50AB48B9A7E2A1C46A56561A7A57BF7EC177061FDA32C509E26B0BA4FAFEEC9FE95E046BDA3F62FBCFB1FED25E15EDE6B4D11FC617AC25E43843999FA84CC5790702A3DD90C73B8F6A4DE158E7939E94C93770C3E51DEA6C745AC3F2FF00303F740ED481821013238A319C01B88C60D2C9BE3DAA00DBFAD322E361F2D95BBB77A55631C654C7D4FF001546CA7A2103079A977C8AC7A1E3029831250C76FDD208E70694E36AA46C0B7A9A23658F7175CB74149B5A48CE1154E722804C5DCA58866F99476A4F9BEFF21F19E28C286E36EE0724D30B6D90EEE4B743E9414D5CF23FDA3FF673D23F682F0DB3A88F4FF15D9467EC77E00FDE719F29FD41C75ED5F977E29F0DEA5E0DD7AF345D6AD24B0D46CDCC72432AE0E4771EA0FAD7ED02E4E36FDF53C935E33FB4D7ECDBA77C7FF0E1B9B2F26CBC6364B9B5BCDB81381FF2CE4F507B1ED5AC5F4662972B3F29E452B207425255395615F6DFEC7BFB65985EDBC11E3BBC6688010E9FA9CC7EE9ED1C87B8E983DABE3BF12F86353F08EBD7BA2EB5692586A969218E5865182A47F3159725AE30541571CEE1C568ADB32E5247EDECEBB8677028402181C839E78A6236D3B700A9F4AF8B7F635FDAC3FB4A3B2F0178C6EFCB9C623D3B5298FDF51D2273EBD81AFB63C90AA083F21E841ACA4DC58692121555638EF5656205493D7B66A38ADD59B3CE7B5595CED0BD6B3720513E10FF00829D4DB57C0B0670365D498FC50557FD96D4AFC1BD2188C6EB991BFF001E155FFE0A7D73FF00154F81EDFD2C2E1FF374FF000AD3FD9AE2F2BE0BF87063EF49237FE3F4A316F56745B43E79F1EB0B4FDAE9981C11ACDBBFE6C86BF5CB50C25CB127273D2BF20FE2E4BF65FDAA2E64CFFCC42D9FFF0040AFD7ABC93F7C081BB201AA71713057BDD95F6F98376406F41436EDC00CA8EF9EF49B704306CBF61488CDB9C499F6A437B8AB195C13F773EB5223EE990E369DDD09A66300EEE3D29F0E5E64661839E31F853EA1D0FC65F8ED63341F1F3C756F110929D6AE769CE39326473F8D7AEDA7EC19F19AE2D6DEE23BFB3449E359573A890704647F3AF3BFDA6ADFEC3FB4BF8D948FF98A33FE614D7EB2F85E4371E11D0645E8F616E47FDFA5FF001ADE4FA171F755CFCE56FD857E3841FEAF5587FE01A9B53D3F622F8EE9D35503E9AA9AFD26DBF293DF3E94BFC4AA3009F6AE795998CD733B9F9AF27EC4FF001EC7035427E9AB1FF1A89FF624F8F9B4117DE67FDC60FF008D7E96EEDA76679F6A5CE0AAE41626A6DA58D6364AD63F2CFC61FB3EFC7AF83BA2CBE25BA9F505B4B2FDE49359EA4D218C03D4807A57D79FB13FED297BF1D3C2F7FA2789A713F897468D5D6E9F1BEE60E9B8FAB03804FA1AFA1F5ED0E3F10787B55D22E116482F2D64B770470772915F983FB1E6B337C35FDA9EC34991FCB59AEA6D2A65EC724AFF00314E3251E8653924F63F51D643237CADCF4F9A862635F9003CE0E054AEDB647450ACFF00DE614C553223658A81D80AA7218BC07C96507FBBDE8F30F248CA74C81CD1B5B6E6360B8EEDC934A9BA58C6E6DBCF618A498C6EE19214727A134E12155217696EE40E94C6692190E143A81D48A177CD270368C60F6A621EBBF7348EDB7D3E94D5669918F9AABCFA52C6ACAEC406718C7278A634C5F2AA80738E98A650E561F21059BFBCD415F330FE6657A8A5118858921BE51CF3C52A8755421014C636E29884DC1A1CF983DF68EB4C68CC806C2CB81925A9D1A2A32B9C2F380B9A3E699D89F953A706A5B1DC06E68C80EBF2FA8A5DEECE331AB023A8A62C2B2174DE76FF000E69248DE190089B72818340AE3C346DF2B9391EB4A3EE2AAE5547AF7A5DBE5B104AE319E9D29121C052CE7AF1CF1436317C93FDE6FF00BE68AB1B9FFBE28A9D40A4CAC8C1B71662380B4E21D42EF2C413CD11CC91A9FBC79C50AC4F2C8E3072BBBA555890252624A92003EB4F5CCCE48DB85FEF534C7B4290A319C9C53DBEF008BD793CD03B11869559802483D0F5A695F310AED772BD4D2ACCFB98468C33DB3C8A4DB26E1B15883D7B501A8243243B307B77A947EED994042D48576AE08557CF1CE4D35A34F3093CB28E76D4EEC60653185F3307D3BD0D2ED8C29DCCADED8A64332B32C6178EB922A611CACFB893C7AF22AF442BDC0C823558C310A475C537CB0FCFCAA07E752AE59413820715149D4B90A840E31DEA6FA8AC42A195498C170C6A566658F803238DBD69EAB21083006464D363C282BBB1231CE7AE2A9EC3258579254051DC915F2C7FC149B57FECFF0080DA55816C7F686B083EA1119BF9815F56471B32AB965009C15EFF00857C33FF00054CD68A69BF0FF480FF002F997374CBEF8551FCCD65ACB6358EA753FF0004DBD07FB3FE0CEB3A96306F754D9BB1D4220FFE2ABEB2C6F5C77F5C57837EC23A58D37F667D05B66D6BABA9EE0FFDF5B7FF0065AF7F68D9C282013DB3C56EFDDD18A524DE8447E5552C771F614E595812CC3E5E9902963CEE1861C704638A56F31B722E194724E2B2EA64C8F73329DE307A0E2A5DAEAD8651E581F7B1C9A4F336C63CC3B874DAA39A23F91892A467A771499713E7BFDBDAEBECFF00B34EB409DBE6DDDBC63B7F18AF14FF0082605A8FB478EAE76E4AC10460FD5C9FE95EB1FF000512B8F2FF00676753D64D52D873F526BCEFFE0983005F0E78E67CE034D6E99FFBE8D5C4DADA1F6CAF62DC0CF1CD4BB437DFC2F7E0E6A31B7818F3037E94ED803125793C0A4CE66014BA1552C029FCEA452ECC14E0A8F4EB51AABB280E48F7A9514E76F45F5A068E27E3C5C0B3F821E3C9B3F7346B93CFFD7335F9ADFB14AE7E21EA2FFDDD3A4FD78AFD1CFDA49BCAFD9EFE21B03D3469C7E6B8AFCEAFD8A976F8D35B7E9B74F61FF8F0ACDBE85DAE8EF3F6C88F77C2DD088FE1D48FEB1B57B9FF00C1376E3CEF815A9C679D9ABB1FCE35FF000AF13FDB03E6F84FA6FF00B3A92FEA8D5EBBFF0004CC904DF077C4A84FFABD554FE71FFF005A9F2BE6B98BB2763EBA1186C0C0029DFBA82379A5658A041B9D9DB0A17B927B53D542C6F2360222EF624E0003A927B57E7CFED89FB604DE2EB8BDF01F82EE1A2D1558C77DA945C1BA2382898E89FCE9DFA0A52B117ED8DFB614DE349EEFC0DE06BCD9A146C63BFD461241BA6070510FF73F9D7C811A2C4BB500C1EB5247088570073DDBDAB5BC1BE0CD5FE20F896CF40D0ED5EF351BB70A8883A7AB1F403AE7DABA2315157358449FC05E03D6FE26F8AACFC3FE1FB37BBBEB870BF2AFCB1AF7763D940EE6BF537E00FC02D13E00F85458D8AADCEB77480DF6A4C3E791BFBABE8A3D07D6AB7ECF3FB3FE93F00FC2696D024779AF5D286BED476F2C71F717B8507FC6BD59B72A92A0BB7EB52D96DF44267E62B805CFB52B132212AB86CF5CD2A29660303D771A632C91AB9041E6B26C824F283A9DCA370E4F3517C8CAC1C1EB914372EBB97E661D73529CB21DCB86CF02909EA32457450426E0792334166C3AE3007E18A531BB63F85D4F3CF14C936C9B8FCDB8F1B6ABC810E566857E6F9829C8269ED20E4093A8C8150295650B22953DB26A54F9B070BB477A91846DFBBDC5D73F4A7863B54F99F862A2F9966DA082054EAAF82D818E8063AD325899619456DCD9C9E2BE5EFDB23F6A55F85DA6BF83FC2D708FE2ABB8FF00D2AEA339FB0C6DC607FB647E55EA7FB477C69B7F819F0C6EF5B0564D62E0F91A7C27F8E53FC5F45EA7E95F93DA96A97DE22D5AEF57D4E76BAD42F2569A595FA9663926925CCEC65672764447CC9E79279E479A7918BBC921CB1279249EE6A45A4514B5D1A44DA292D07514AAB4E55C5176C1C921A14E6A41C5359B14E53BA87A2D4CDB9481B38A60CD4D4856A14AC357455915B69DA39F6AFD28FD90BE07DBFC2EF87367ABDE5BAFFC247AD442E2799D7E78A261948C7A0C609F526BF3B3458E36D66C44AC044678C313E9B866BF64D218D2D6D562C18BC98CC7FEEED18C7B629FB47B2295DEE22C60AE31961D29C8ACC4AB0F9291A12EB9070FDC52967FDDA310063A8ACEED9761FB76C84A3E401F7683B9FE7C8181DE9163201D9D477A0EE570A7E6A90D854F9D40DB923939A76E56605B2714323F2CA40C9C53770906DCED20F34EE2E605661F285C64F14F6673B40C1038DB4D638D986C9A40C6172776431A2E4EE3B779AE771C28E2910059361E4F5CD076A2B73BB9CE28F2BCC763B88246734C7A0B22C85DBCBE06338A5DC7E58C0C13CF14C5973F21638EC69D1B26502B7CFEF406A26E3B82B2E4AF563472AFB580DCDDE8DC36B29DCCDD734281332330DBD8505A01B955BA32F4A733AAEDED9E06299E5FDE52D84F514B92191146EEF4AC2BDC5563B77E7DB3DEB87F881F057C13F142DFCAF10E816B752B0E2ED13CA9D7DC3AF3F9D76D246D29620AA2E7A52B31CE130C0F06825C533E0EF8B7FF0004FDD534759EFF00C07A89D5A11961A65E612703D15BA37E95F29788340D57C2BA949A6EB7A7DCE977B11C343731946FAE0F6AFD9CDA5589C903FBB5CA7C44F851E15F8B3A5B58789B478AF78C47743E49A2F7571CD6919B467C8D1F904AC7B723D6A552596BE8DF8E9FB10F893E1AC173AC786247F12F87E3CB3A227FA5403D1907DEC7A8AF9CA16DDB971B594F2A7A83E95A4A6AC2693D10BCD7A87ECB975FD9FF00B42F8165CF5D4162E7FDA057FAD79967F315D87C17BEFECDF8C1E0CB9CE045AADB9FFC882B9E5AAD0D23EE9FAE322959997BE69377A9C37607A54975949DBE5FE235098F29C1DC4766E05329BB8F93327CA1BE6EB4DDA170CED9C8A030560DFC58C63B5346655CB2FCABEB496E489848F23B9E94E3188D830058F5EBC538E0B2A84C0A4F2C9DCA5B0BD7356048BB8B6E914118A88B0DC8C18EECD38B066DACE402285FDDC3C0C8ED915202B47B54ED5527AF3D69373B48781803A522FEF1B2C30D8E280D9425CEC3DB140D313E6F2B38C366955B6E195B660F38EF4476E59D8EFDC48E7751B642B8E30A7838A06D5CF11FDA83F669D3BE3B787E4D4F4D892CFC656719F22E781F6951FF002CDFD7DABF31F58D1750F0EEAD75A56AB6B2596A16AE639619970CAC0D7ED4AC9B7E7F98BAF1B470057CF1FB597ECBD6DF19B493AFE8491DAF8B2CE324FF0008BA51CEC6F7F4AD2273C958FCD4859EDEE12784F973C6772329C1C8EF9AFD0FFD8FFF006A283C77A6C1E11F14DE4717882D93CBB59A4603ED6A3A293FDF1FAD7E7BDC59DC69D7D7163790B5B5E5B398E58A4186461C1045496D757163750DD59CCD6F790B064954E0820E41CD4D4575A1716E27ED9AA6D2140271D4D4A176A81D0F535F2D7EC8FF00B5DC3F142383C23E2B916D7C4D0A85B7BB63B56F540E87D1C0EDDEBEAB68F6E411F3FBF159A8BEA5A6A5AA3F393FE0A7575BBE29784A0FF9E7A3B363FDE94FF85765F006310FC19F0AFBA3B7E6E6BCEFFE0A63216F8E1A347D447A2271F5918D7A4FC13FDDFC1BF090C7FCBAE7F3635D12D894DB91F2BFC7F3F65FDA42E64FFA6F6CFF00A257EBC2CDE65B5AB8FE3851BF3515F90FFB4D7EEFE3D4D20E322D9BF45AFD6ED2A412689A5499E5ACE16FCD0543773A651B22665DBF31F98FF7475A94EEDE18F0B8E86A355693B63DE9D2069980242AE3AFAD498C84DCDBBE600E7A0A9A3C165C74CFAD40BBB9CF5EC4D491B0DC9B79E79A3A97D0FC94FDB32D7EC3FB4E78BC818DD3C6FF009C6B5FA8BF0E26FB57C36F09CA39DFA55AB67FED9257E697EDE16FE4FED31E2238C799140FFF008E01FD2BF473E08DC7DAFE0B78166CE4B68D6DFA201FD2B494BA15F64EC7217073C9F5149B4AA8320C8F5A5F99B85C32F7269D1E66EA795ED5919C86A8126EDAA0363029CAA36FCFB7EBDE8961936B1F954F6C53E350B1A9603A77A1EC28934388DD405F973D6BF25FE2746FF0E7F6CED42441E4ADBF8863B85EDF2B386FEB5FACB2678DC31CE40EB8AFCB8FDBF3493A2FED3F7776BF27DAA0B6BA0718E718FE6294527B969C56E7E9F3C81A41230C46C030C77C8CD33CC76E50A609FBBDEA8F86EE06A5E1DD16F376E1716304BBBD77460D5C66DAC0AAAAF3C63A5395AFA137B9337CA1005C1F6A42CD236C76C0CE7A52B2AEE53E67CDFECD053A48572DDB3DE92131AB27960AAC9904D3DA32CC0E36F1DE855DCB92AA3B806A38FCC70E1978CF5CD5885570B90776F1DCF4A5DCFE50C8EBD0E29C15235C6EDD91DCD37A210D274E805400A61712282DD064FA53564753B19F70EA08A58DB0ACAE76FA05E49A546DA9908C36F196A63132AB80ABF331C8CD0D8642A50A90724E69D0ABEE2646C7A546CBBF2C9CB13DE801ACFE746543E003C66A41FBB6D81B7061D8526F3E5A92A3E53E9D69642ECB925A33D46076AA10DF2E546054EEDDD71D293CC1B8A98F8C70695479AA1C0202F72719A5124AA10B20FBD814B7286E0FF71A8A9B327FB5453B13718240AA4491E5FA8C5234864DBFBA63DBDA9FCABB483F78C3F1147CD1AEF656639CED14AE3B882432390B182BD31E951FDA16D828F2C839C70734F959E352563DAAD48844927FAA6E9F7BAD084008CBB468CAE3F8A8F39163246E273820D2EE915718DC09A5CB2A150BC2F24E3AD26FA14324C2E3645B9D7DCD48BB7697D9B589F5A6ACCEB9DD81B7A714D5542C1CB33367A0ED5004BB9FC95D91ED39F4A606DBB80243E7900D481E6666CFCA9EB46D8FCB92792448614CB492B9DAAA07727B0A7B6E4DC63B4730D9CA9A7F9814F97B549DBE99AF30D5BF69EF849E1DBA78EEBC77A5B3A1DAC2DC34BCFD47159A7F6C9F82B10C9F1C4649FEEDAC9FE15695F525CB4B9EC2D9C0F2DC17FE20CBC0A7AAAF231871FC40702BC564FDB53E08C4483E33C8F51672547FF0DC9F03A3183E2C94FF00BB6525538E85C1732D0F758A2C2A994EE7CF1B457E747FC1502F1A6F8A1E19B1ED6FA5EFC7BBB9FF00015F51C7FB787C1076DBFF000945CE578FF8F17E95F0D7EDBDF16BC39F19BE3043ACF852F24BED1E2B186DD66910A92C324F07EB4BE15A9126EF63F40BF657B15D37F674F01C5F73769C243FF02763FD6BD3640D21272368E8735C7FC11B75B0F827E0484AF0BA35B13F5280FF005AEC37601F946CEFCD0F5D5971565A87CD228040033FC348D8859C2313CD0AC186F076ED3D29DB861B685753D4938353D456142A346369DD9E4E68C346A0377FEEF229AAC7728036E3AA9E869CAE5B715000EF50F728F953FE0A457220F809611EECF99AC4233F45635CDFFC131EDCAFC3EF184DD9AF615FC81AD4FF008298B63E0DF86E3078935A04FE113D3FFE09AD6AB0FC20F12C9FDFD4D07E4B5B2A914AC1CCE5A1F5AA82B8DA72DE98A7AA0DC7E72A719E7D693714C053834B85E4B12474E2B2BDD88002194EF2E7E952C6B956DCF91FAD42BFBBF906EDC3F954D1AA72431271C8A067987ED4D38B7FD9B7E21B74FF00896328FC4815F9F1FB17A96F157889C7005963FF001E15F7E7ED78FB7F665F8807A66C957F3916BE0AFD8A509D7BC4E71D2D147FE3C29C21777668ACA373BAFDAE067E13DAF7C6A31FF26AF49FF825FC8DFF000AF7C65113C2DEC0DF9AB579E7ED6D18FF008547093DB5088FFE855DEFFC130A4FF8A57C6F1E7A4F6EDFA3D6AFB239F95CA5A1E9BFB7878D355F077C099069174F62FA95EC7653C91F0C626C92B9F7C62BF31631B631CE77725BDEBF46BFE0A3393F03F4E1DBFB620FE4D5F9C8A311AD2492D4D1535B9B1E15F0BEA9E37F1059687A2D9C97DA95E388A2863EA49EF9EC0753F4AFD41FD9CFF66ED27E03786C3308EEFC4D7718FB6DE05C91DCC687B2FBF7AF8D3FE09FD18FF868FD3DC80C52C6E0827B1D879AFD2E9589C9079C55B668FDD5A11FF167682FD79A58D7CB03B861CF34DE7760852D8FBC4E295A42FB536F18EB5836662A317C7215BA60F7A698DD4314DA5B3C8A1815C111E0F4151B7C9C91823A9149210F2B23060B82075A4757F2C33361BF4A6B30DDFBB3B73C9CF7A5C3C91EEDDF41576043FEF1490A9623923A526E63BCECF973C1F4A1BCE560C7018F5A56DD2315072BDF14C1A626EDCC088F23A64D3794E08CA93C014B1810E10373EF4D5CA800BF25B8E3A52B0126F5DE147F2A995555801B88C75CF1F5A64791270C0FA922B90F8C9E344F87FF0009BC53E2046C4D696520889FEF9185FD6A77259F9D7FB647C5D93E2A7C62BDB3B79B7E87A1B359DAAA9F94B03891BDC93C7E15E22ABDF1C537CE7B99249E43BA4999A463DC92724D4CA3E5E6B44B950A31E51A0D3956855E6A4036D35A8E4F950A062918D2E698C6AF62231BEAC6D3E36C5328A96AE74136EA52698BD2866A7C873B6EF61ACD8E475078AFD26FD91FF68AD33E2778274FF0DEA7771DBF8B74B8560F2E56C7DAA351857527A903AAF5AFCD661B8D59D3750BBD26FA1BDB1B996CEF2160F1CF0B6D7561DC1A2514968545753F68DA3650C1C63DFA1A89559718F9940EF5F08FC27FF82826BBA0C10699E3AD387882C90041A85AE23B903B1607E57FAF06BEACF04FED19F0E3E22470FF0064F89ECE3B8938FB1DE3882507FBBB5B193F4CD62B51C6F23D07C976C90D8E7A54A1B692C082C288D495CA30910F20A9C823EB48B095FE1C8EF4CA63364922EEF7CE2A4E1814230319F7A52BB7246463B547FDE6EB4122FC91C2508F9BDE9EB9320F317A0E2985373827924538FCEAC0924F4FA516015555642C064639CD35B2D82A0E0F029B1A0F28293F366A42B85755277FE94AC31AC0C7E580BF2F56C53C3AB059157D80A17031BB8C8E053377EED4636F3D4D310606CC9F95B3C9ED4BB99D578C853F9D0D192ADB9BE5CE69CDB59D54311B467DA9586220DDBD50719E7348DF2C7C821B38C8A58994EF65248EF4BB488D588C91D45003555A355070771CD1B55571BB0D9FBB48CDE6282A369CF534EDB9607773F4A652623285625B76CF4A4F3079608191E869C4C9F3B30DC01C62903091828F971D452287A395E80383C303E95F367ED1FF00B1BE91F132DEE75FF06C7068FE2851BDADA350905E11EA070AC7D457D219F27249E3D569431539538E338A992B90D1F8CBAE68FA8F86755B9D2F57B3974ED42D9B64B04C30CADFE1EF52F84EFBEC5E2CD1AE41C18AEE271F838AFD35FDA23F66BD0BE3D689E7B05B0F14C087ECD7F1AE37FA2C83BAFF002AFCC7F12F86F54F877E367D0B5BB67B2D4AC6E42BC6C3190186187A83DAAA324B731B4A5A1FB36D279AB1CB9C8650DF98CD459DC7611B87BD55D0EEBED9A06953A9FF00596913FE680D5CFBDD0E71D683A1479771A58ED0A8BF76919B72804127F952EF3F37CD8A6E19133BB82726827A8EF999BE6F97038A66D572E15883D69FE6076383B71DCF4A6484A630EA4F7C532AC3955A4DAA0FCABFC5DE9594FCAA199B0718A4DEBC6D6DA31839A555F2B6E1F3B8E4D016025B775C0CE3E94AC30B8570483DE9A6440D8C364F7A4568E3E541624F39A44B0750C010FF003F7A77CFB48493AFAD0BE5167383BBD285DAB1B155382681A62FCA18FCDF3E2955A4F30EC0BEFEF4BB1768618F4E699E601B805C37F7A806AE7CDBFB577EC9B67F14B4DB9F14785ADA3B4F15C0BBE589576ADE01DBFDEF7EF5F9CF7705C58DE4F6779135BDDDBC8629619176B2B0E0835FB6D6E3F7A0A91CF6C57E457ED2CBB3F686F1E2E36FFC4C9F81D3A0AA57DC6E4B976380B6BE9B4CBB82FAD2E24B5BDB761245344C5595874208EF5FB2BF0835CBAF147C25F06EAFA84866BEBED36196690F5672A324D7E2FCDCAD7EC77ECF2777C07F87E49C9FECA839FC2872E86314EE7C13FF000528937FED030C7FF3CF47857F5635EA7F085767C21F098FFA715FE66BC9FF00E0A3CDBBF68C9BFD9D2EDC7FE855EBBF0A38F84FE1407FE7C13FAD61CCE522E31B6A7CA1FB55FEEFE35EEFEF416EDFA7FF005ABF593C3399BC29E1F909E1B4DB66FF00C86B5F949FB5B438F8C901FEF59DBFF335FAB5E08224F01F855F3F7B49B53FF9096B775236B21F3B91A677B37CA3E5EF48572A0B1FBA78C548C5BE600E07B531971C8E4F538A8DC189F3ED390481D2A40A13663D791512B79C41DC463819A957E55033934A451F985FF050D845BFED1B7CE3FE5A585BB7E8457DE5FB36CC6EBF67FF0000499FF984C4BF9123FA57C2BFF051E80C7F1EA2900FF59A5427F22C2BEDBFD9466371FB36F805C3722C0AFE5230A71835AB346D2563D5D983ED55217DA90EF66E0636F4A561B549C60FAD0377019B03AD518BD43732313805BD29D85F949E49EDDA932158B939ED8A6AEC917009500D4B0896158ED0B918CF26BF37FF00E0A67642DBE34F86EEC0C7DA3495C9F7591857E8F46C0478E06E3D0D7E7DFF00C1512D42F8BFC0774305A4B0B88FFEF9914FFECD53CAE5B0A4FB1F637C0ED4C6B3F047C05741B76FD1E15627D53287F95766D2884055557E7D335F1DFECCFF00B5D7C3CF03FC17D07C3DE2BD6E4D3F56D3BCC88C3F6777F90B96072063F8ABD623FDB73E09B0C8F17BAFA86B393FC2B5E4E5DC6AF6B9EDEAA8CCEE5F695FE10B4CF39D1479BD33F26D1CD78CAFEDB5F04B1FF238B67BFF00A249FE151BFEDBDF03D719F1848C7FEBCA4FF0A8B909F33B1ED8C19A45C32953FC38A73AB2E42E368EAAB5E1BFF0DD1F03E2048F14DCB1FEF7D85EBB2F873FB437C35F8BDA80D3BC2FE2882E354604A5A5C2985DF1CE143753EC28BDF44372E877ACC108648886A4F302C9978B8C75A73B1562986328EFD0535B7484B13B874DB576B00D591A48CE3842796028339859005665FF006BBD0EFE57EE890A7EF002A58CB6373E09C704F4A18C649BA455FE33D78ED409DF682230187047AD2C6D23312718F51C52EE65C85E571490586C80BA9F9826DE8B49FE91FBB20EEE31CD3A3620138CC87AAF5A46F32163B98EC5A07B0ADB42261433679C9C53B6493E064229E78ED50F9718DB2336EE69FB84C5A48F8DBC70698877D9DBFE7A1A2A2FDEFF00CF6145310FC055CB6E8998F6E452AC623440F2315CFDEA5F31782770E31D3229AD089000CC4BE738A5A15611D4CD261033A8F56A790370DA1901E3834D58D63CED5666FAD2EF8E35560ADB97A5000CA91464659D81E39C5337052A4BB71C9039A76FFF00968E9BC1FC2915921C311B7238A9B05C2308CDE6B3E71D8D3D55BCC674DA148A681B95B695D83AE7BD39802010BB074E050F416E491A19A200E5CE7395AF877FE0A2DF1C350D1EEAC7E1B68D72F6A26896EB5331B15326EE12338FCFF115F75DB4692DC280A003C71D2BF267E375F4DF157F6CEBE849332CDAE4560809E36238403E9C5472F30F91C8F67F857FF04D7B1F1278434CD67C57E2AB9B0BBBF856E059D95B07D8AC320331239C11DABB7FF8762F8054F3E2CD648FFAF78C7F5AFB0E68D6DD521897CB8E3508AAA380053321B6A805BD71D8D69B58528E891F20FF00C3B27E1DF39F14EB847B411FF8D3A3FF008265FC395FBDE27D7187A08A315F5D823187073E98A705F9771538A7CD61D3F74F92E1FF008266FC37561B7C45AE31F748BFC2BE21F8D7E05D33E19FC62D6BC2BA44F35C69D61388924B8C6FC9504838FAD7ECA46577614719AFC7DFDA42E86A1FB4C78C48E41D61A31F805150EF50D6315BB3F59BC016A2CFE1EF85A0550047A55AAE3D310A56C3215E554124FE155B4383ECBE1CD1E1231B2CA05FCA35AB414F441B875EB4F6D0C5EA1B7E72A63EF9C8A4318662A2318F5279A4F99F9542A01C75A633797B9551B70FEF1E0D21DC7863B40E1307BF7A72F38545F973CFAD47B89018050D9E86A688332FF779EBDEA58CF8EFFE0A75304F867E0E87A6FD55DBF289BFC6B53FE09C5F2FC12D68F76D57FF006515CE7FC14FA4C7837C091FADF5C37E518FF1AD4FD816F0E93FB39EBF79D0C7A848C1BD30829C60B76426DDCFAEFE6F439A7C6A4291B4E4FD6BE36BBF8ABE2EBA9646FF008482FA342C48589828033D381551BE2178A58F3E23D53FF0248AAD0BE87DAFE5C840CE73EB834F58DBB038EE706BE255F881E283C7FC245AA7FE05354B1FC40F14F20F88B55E7FE9E9BFC697503DCBF6C9CC3FB31F8E83672D0423A7FD354AF877F62941FDA5E296FF00A778C7FE3D5F4EFC6AD5EEF54FD88FC4F2DE5D4D7972678D1A6B872EEC3CE8F1C9AF993F62B522F7C53FF5C621FF008F55395B42141CA4771FB5CAFF00C5A15C7FCFFC27FF0042AEBFFE097AA7FB0BC71E9BEDFF00F67AE4FF006B8FF923CDC74BD80FEA6BAFFF00825E30FEC3F1C8EF9B73FF00A1D3726AC91ACB6563B6FF00828B285F81FA6F3FF317847E8D5F9CB906315FA2FF00F051ECAFC13D23D4EB10FF00E82D5F9CD1A9DA334461AF3315D456A7D1FF00F04FE3FF00190D6E7B8B0B8FFD06BF49B9DA095EDD457E6FFF00C13F63FF008C80438E9A74FF00CABF473E6C838CA63919C5549AB59094DC853B2451B46598739A72EEC22B28031C6DA23560DBD1706915DF76D09F367EF0AC84F708D5B0C361EBFC469E599B2BB54B63A66A3923FF0065DB279C9A63A8463B51B77AD5964CCC625EAB903EED46ABF2EE638523A7BD2ED75656C65B14E2CD2F054002810C5507073951FAD2B0DA490700F414995F2F853807B5018962594FA014842AB2A9539563EF463F89DF693D314DE3708C2E1BD4D3D0617E64C81DE82458D7E62492C3B5782FEDD574F6BFB38EB8A84AEF9E0438F42E2BDE959D6408380704D7857EDC9A7B5EFECE3E22283FD4BC329F6C38CD386E6974B73F312DFE58D6A6F4150C5FEAD7E95320EF572DC9BF51EAB4AD4B48D548E7BF3323A5028340A76D4E856E82E28A5A2A861428F7A29AA6A6E161D49814B453BAB009C8E95198D4FF0008CD4AC38A60ACB46CA4CEBFC23F18BC79E0360342F15EA96510FF009606E19E23EDB1B23F2AF67F09FEDFFF001174128BAC5AE99E2081783E64462908FF00781EBF857CD64679A6D5D95877EE7DEBE1BFF828E7852F1513C41E16D534B93BCB692A5C463DF1C37E95EA7E19FDAFBE12F8AB6A45E2A86C666E91EA113C07F360057E5B6D1E94D923575C11915310959A3F67344F15681E228D64D2F5AD3EF95BA1B7B946FEB5B5F6727257F3AFC4AB569EC640F6B3CB6AE0E4342E50FE95DAF877E38FC46F0A95FECCF1B6B36EABD236BA2E9FF7CB645292B10A0BAB3F5F1E12AC011C2FEB48CBBB181B7757E6A787BF6F2F8B3A1EC5BDBAD375C8D7AFDB2D30E7F1422BD37C3FFF0005259B08BAEF82C13FC5269F73FAE1A9A8DF6264ACF43EDE68C4AC7391B453576B285C1C0ED5F3B7873F6F8F85DAD2AAEA13DFE833375FB5DB1741F8A66BD57C33F1C7E1DF8C157FB23C63A45D48DFF2C85C047FFBE5B06A07C92DCED301CB295C28A43F285CE0A93C9F6A7C5243751EEB7992643FC51B020FE469CD1F407903DA9898C39E918C645317855DB9241F9B9A7CC48F507B527DDC743CF3520273BB0507A8CD355FCC6224E314F65DD97F4A72AE57792003D01A4218ABF29CE5467BF7A6B379726625DC4F5A7346E7215C3291DFB52472347C32E074CFAD01B08AAB1852724B1A5CB16C2E08CD234443A9058F7C51964624A638A0BDC913862E1F18183CF5AF9ABF6D4F8071FC4AF079F13E936824F146923CC2235F9A7847DE5F723A8AFA4420455C0DC69613E59CA8C16EA0D45B52A3EEBB9CDFC33BC37BF0DFC2D33A3C72B69B06E4718604200723B1AE8F1C93F7450BB1F0154051C7CA318A6C6B924B1E07AD6BA09BBB177068F00679EB4876165527F2A5DC558228C0F7A301B62AF5F5A5A103B6E5880BC11D6A367585994804E2841228656209634488ABF2C8001EC690C732AF2CCA1B8E293CC126D50BF2F7A8C46369209663D054ECA7CB50D853ED48637E5899BE6603D288E411AF196CF5CD1F2A39209200EB4BE57F1F2C4D3244660D29DE0A83D3029CD205F910F07D695964639238C719A233B633B932D9E280B09242B1E371E3BD49B906580CD46642CE530314FE879C27B50522E5A61A557F535F911FB4F7CDFB4478F48FFA093FF215FAEF6DBBCD07F873C57E46FED34A3FE1A23C79C7FCC4A4FE9529EA43BEE8F2B93EEE3BD7EC77ECF0A57E02FC3F07FE8150FF002AFC789106D26BF62FF67DE3E04FC3F1FF0050A87F955CA69BB229367E7D7FC145A4FF008C92BE5FEEE9D6E3FF001D35EC9F0A640DF0A3C2BFF5E11FF5AF11FF0082871693F698D638FBB656E3FF001C35ED5F09BE4F853E140783FD9F1FF5A9F67CBAB2A4D2D0F9A7F6BCC2FC5EB13FF4E107FE84D5FA93F0E64FB47C33F0738E7768D687FF00212D7E58FED82E3FE16B589FFA87C3FF00A1357DDFE30F16EAFE19FD9DBE1BDD68D7AF637536996A864400FCBE5F4E41AA8D3B6ACC94B53DEC9CE71F8D210DC6322BE2B5F8C1E377E5BC497C3FDD2807FE834EFF0085B5E3339CF89750FF00BF8BFE149D8BD0FB4994F0714BCEDE4719AF8B53E2D78C4364788F51FF00BFA3FC2BD57E04FC40D7BC4DAC5FDAEAFAADCDF46B0868D666C853BA92D49BBB9F2DFF00C148A107E33692FF00F3D3498FFF00436AFB03F63E937FECD3E0AE3EEC5327E533D7C7BFF051A90BFC5AD08FAE943FF46357D69FB13CC66FD99FC278EA925CAFFE456AD2E354DFC4D9EDEC42A863D3D298C4365981C01914FE4B0CFDEF434815B927EEB540D88AA19B919C8FBB49B7CC5D89F21069481E60D99C91D4D3374919C103713C53144995BE5076EE3D327B57C27FF0544B1636FF000EEF31D1AF223F8F967FA57DD831C231C375E2BE37FF00829CD8897C05E07B8DBF73529A3FCE2CFF004A39D40B495F53C73F653FD91FC33FB44784F58D6F5ED7B50B0B8B2B95B7486D115B23683924FD2BDB1BFE099BF0E428FF008A935E27FDC8C556FF008269DD06F02F8C61EEB790B63EAAC2BEC0F99793CFFB355CFCDAA2A4F4D0F9397FE09A5F0D8000EBFAF1F5388FFC295BFE09A5F0C89CFF006F6BFF00F90FFC2BEB15E78391BBDA976FCC136E475DD52D5F5318AE595D1F25BFFC1347E1AE30BE20D7107A9119FE95F2AFED29FB3FDEFECAFE3FD1AF341D56E67D3AE3F7F657B22EC911D0825491DC57EB02C68C4B6E2154676B75AF977FE0A3DE174D63E02DAEAE2306E349D4233BF1C84932A7F52B517B6A8B9C9B563D8BE047C4C1F17BE107877C4D8FF4B9E230DDF3D264E1CFE279FC6BBBC28DBB4903BD7C8BFF0004D2F12FF697C2BF1468ACE5DB4FBF8EE23527A2C8A41FD5457D79B805191F37E957CD7D4CD26D6A26177B0F9738E58F5A46DD334638C018A7ED119326D0B81E951B31997792CFE9B78A9E62876D50C22F998669E485F957A9E368A8E369021629CE7914C9088D410087AAB957144588CB7CD1367A9E69E250582FCD26E1D4F148D36E5C47CB0EBBBA52C8C5029190C47403814225EA2330F9008C327F7A9A17A8285236E4914EDDB98AABEC18CF4E7342B79CA15642D838C7AD301BFE8DFDE6A2A6FB1AFF0077F5A28B86A37732AB285DA01E00A7966DC7703E9C0E69A8B83CA317EB9CD0ACE19BAB1ED9150319BDD72150E07AF5A554642A3213775C9E69CCABE580CECAD9E946D8F729C977E82AAE922489963DCCCF239DA78E78A93CB04E77071FED1A3E7F995C285CF19A1C6EC060AA3B352BB631AEAFB4B2ED033D2A58E4320C3374FEE8A8DBE4C0DF900F4C75A97FD72E32A1473F28E6A6455BB0F9AEA2D3ADEE2E870228D9C8FA035F937FB399FF0084DBF6C3D1E698799E76B525D1EFD1D9B35FA83F122F8587C3CF13DCA1D8D0E9B70E38E788CF35F99FFF0004FBD3DB51FDA5349B96EB0C13CC4FBEC6FF001A718736ACAE651D59FAA5331562C1BF0A8D193276BE5BAF4A52CAACC40DDEB4870B93BD413ED54DEA67B8E01B8DC80B7A934329DC7E6C8C7383432AB32E5C1DA3F3A4504738C0A962F4258F2EC084D833D2BF1D7E2BC7FDA5FB49788F1CF99E22917FF22015FB1B085691581623B57E3D6BD8BEFDA4B533D77F89A41FF91CD2F69CAAC81DCFD838E1096B6C8DFC30A2FE4A0546C9F38006C1EB9A9A461B82FA01FCAA1671C7980B1CE3E5A1DF761B21B22950DB54BF38E0D02355EAB90DEFD29142AE506707924D3976A1CA8DDC605342114869372A707F4A99158A0DE7BF07BD4580EA0F2A7B85A915B0B8DA48CF5A5B9A58F887FE0A8327FC497E1FC5DFED174DFF008EA5697EC7DFE89FB22F89E5CE0FDAE5FD5547F5AC5FF829E12C9F0F53B66ECFFE8BAD3FD97E716FFB2078857A16BE61F9B256BA244F328E91398DDD29435338FD69735986C48AD5346D971558354D0B7CC2A6E0771F172E4AFEC5BE254EBFE97103FF007F63AF05FD8AE30D278B1F3CEC887FE3C6BDA3E2B5D06FD8FBC5899FBB7900C7D668EBC1FF00649D72DB40B3F19DF5DB88ED6DAD92E246CFF0A924D3A707365AB25A1E8DFB5BAB7FC29F946D6C0BB872D8E986AE93FE0974ACDA4F8E8AE4E3ECE4FE6D5F2A78BBE2078DFE3C6BD2695A7437FA85BC8E64B7D1EC61670141E095032C40EF5D0781FE1AFC7DF00ADC7FC231E1FF001768BF6823CDFB1DBCF16FC74CE0735D5CBCA61769E87D89FF000522CC3F05F42CA91BB5A8C74FF61CD7E742BEE8D6BD73E2B43F1E350F07ADCFC458FC44DE19B3B85DA75589923498E42FDEC126BC86360635C54732B17A33E98FF827DB16F8F9C7FD03A727F2AFD1BE76E3696E2BF3ABFE09EA81BE3C4FCE08D2E6FE95FA30C0B2E070718CF6ACF997434D36437CB29B46FC0FAD2B799E5F41F2F420F5A5DB870A53200C83EB49246BB97736DFF66A4CD8C5DE573862D9E4669FB0AAB9241CFBD0235F33224C0A1A10CE4EFC0EF4C2E372CDD5B6E7A1079A3C96918B06C01FDEA1A347C12C085E8453970B842C1B34086C8C7CB0AB863ED46E31B6577138E734E750AA4060181E0535176B81E6F5EB9140C5453F7CE589ED4DDBBB2A5C2907A0A565607225C28EB4863F940186EE585170448AC8D2286CB1C6322B8FF8D5E136F1BFC1FF001668B1C799AE6C25F2D7A9DC064575FBD197B861D38A922937315C1DA4739E869AD1DC773F11EDC385D8E36BC7F2B0F71C1FD6AD2F6AF58FDAB3E13CFF000A7E326AB1C7018F47D55DAF6C9C7DDC31CB2FE0D9E3E95E4CA0D5B9A90A51BAD097752E734DA33537661B31AEB4DDB537DE14DDB5B459AA6868A52334B48C6A8BB8518149934949A2AE3F14A1B14D5CD3852D04C46A654B914DE3B509233E663D5702A192A45CD3590B54F2EA1CDDC8EA558F34DD9B6A45614ECD6C1CCFA0D65DB4CA91AA3A5AC8D2F61CB46DA722D3CAD17E5D0398AEC99F6A89AD23739233533F0D48BC9A9EA5A91B3E1FF1B789FC2722B689E21D4B4C2A7216DEE5828FC338AF5BF0AFEDB5F16BC30AA926B50EB702F1E5EA502B13FF00025C1AF0FF002E9A78AD1BD3404F5B9F677863FE0A45708AB1F89FC171CA7BCFA4DD153FF7C483FF0066AF61F0A7EDC7F0A3C49B16E753BBD0243C14D52DCA807D372922BF33F6EEA3CB5EE2947CC25A9FB2FE1BF19787FC676E93F87F5AB1D5E361BBFD12E15CFE2339FD2B6A40CBC32E07BD7E2A59DD5CE9770B7163753D94EA72B25BCAD1B03EA0822BD77C0DFB5E7C53F01BA04F1149ADDA2E07D97565F3C11E9BCFCE3FEFAA5CA999B47EA56D5DB8071487A00C73F4AF923C01FF000514F0DEA8D141E2FF000F5D68D29C06BBB03F68881F52BC301F9D7D2DE0BF891E12F88D62977E1AF10D8EAB1B7548661E629F4643F303F5150E2D06E746BCF4249F7A63602ED2BB9B34F915A36E7814C38DB903E6EC6A03618A8773301F8527DE93182076343297C163B48F43465D7D97D6922AE1B595997903FD9A9147CA599491D8D376889890C4B63B51B9D71BB2D9ED4362062FC3F038E334DDD9DBF380DD48C53CB157D9C0E3A3531A345CC9CE7E9493400BF3E00CEFF534797E5B167507F1A7F985BE6CE3DA9A4E548701DBAE2AB984858C3B07236E0F4C52796DE6167E411C0CD2ED665C1650ADEFD29CAA5708080A3A96EF45C4C452C1B01339A45638CEC638F4A3FD560238DC4F4A705DACCA09273D73C54DC430306C16DD8EE334E60FB86C4F97DCD48CA5B680E08EFB4D0ABB5643E6E57A5031B238552A5396F4A7084487A608F53512E047D3773F7B352E470A3E65EC734CA8972D94F98013DEBF237F69CF93F688F1E0FF00A88BFF00215FAE76E4798BC62BF227F69F6DDFB4578F7FEC22FF00C85118B6C527D0F33926001CD7EC57ECF64CBF033E1E800E7FB261CFEA2BF1C1D5BE638DD9EC3AD7B0E8FE1DFDA3B4FD2EC7FB26D3C730E9E62536AB6A6E7CB119E57680718C57472C13D485277B1D07EDFACD27ED31E22DEACBB60B65195FFA675EE3F0B6DDFF00E156F8578E3FB3E3FEB5F29F883E0DFC6AF196A8F79ADF85FC55A95FCB80F71796D3C8ED8E064B0351785FE2278E3C13E28B3D0351D52FADA2D3E75B6974D9DF223C1C14C1E98AC2525CD64CD22A32D4D0FDAF9777C54B338C91A7C3FF00A1357DADF129FF00E31C7E152E3EF69D07E91D7C61FB61285F8B16DB7953A6C241FC5ABEC6F88D2F99FB3EFC2403A369A87F24C55CA57D114E9DDF31E341BB504D1B69DB6B3D09B8D535EB7FB3937FC5597A3D6D7FF6615E4EAB5EA7FB3F4823F184C3FBD6CC3F514732441E17FF000516873F13BC352766D2C8FCA46AFAA7F619264FD99BC3DFECDCDC8FFC884D7CC7FF00051050DE3CF093E3AE9D27FE8CAFA57F613995BF66BD180EAB7B723FF1E147B4E6D11A2D1599EFEEBD001F5348DB84672DC67A0A19976E4E41E942ED5501CFCBEB4B5246BB3F1B23FCE9A53CCDC3E6627938ED52F99B18B2824630334DC6D52C0E1CF5154243ADF1B7918C74F535F26FFC14B22DDF07BC372E398B5A5FC331B8AFAD176ED00AFCC79DDE95F2D7FC1466D4CDF012CE5EBE56B101CFD430A851E67635D2FA9C07FC1336E09D3FC71067F8ADDFF5615F6D0053E6203738E2BE15FF0082675C05D57C690F66B585FF00F2211FD6BEED565539C71EB5AECAC8872E6D842A199498C92391CD3517E4C648727EED3F2438D8F927B531805CC9F31753FC3484B464F1B0562BE5F38EB9AF23FDAFB481AE7ECD3E3C80F2D0D98B90073CC6EAFF00D2BD76362E831F2BF7DDE95C7FC64D3C6ADF087C6F6203159747B907FEFDB56774B71DDAD51F12FF00C12FF552BE2EF186964E12E34F49BAF7471FE26BF40638D957781BB1D98F15F9A1FF0004DED48D8FC7C16B9C2DD69F34657B1E323F957E99B287525F391E955CFCDA0DB6C8FCCF324C89142FA1A729697014631DFA0A5F33CB76081578CF23934D738600960C793C51613D462E2405492083CB134F8E3659182E593FBC69CC1DCFC9B4A0EA698CBE5A36D721BBE064532450CDF3AAA81EE7BD3BCB749321F2A0746A6EC6F2C01B5FD4F434BB55731B3E09A76109E63B31F972DD391436EC3048F9CF3838C50198F31927FDEEA29232B26D182AC4F25BBD22AE45E5C9FED7FDF5455CFB1C7FDF4FCE8A02E4017CC57C395E3F8A9FCA44AA1DB71EE050258D48C2EE6E9834C6492352D920E785A9B0C9148F3393BCAFB51B9B0D85DBCFDEC50A07DF0F8623922A558DA6511A1F9B3F78FAD04DC83CB1263F88FF00B3CD4EB692A85CC4EC00C8E2BE16FDAC3F6DAD6749F144FE07F869732413DAC861BBD5211BE4966CE0A45C7001E335E31027ED51AE462EE29FC6D2248372B2C92A820F4F414E32BBB19391FA9DE44A8E5C41216F4C1A3CB788EE11CA18F53835F96C963FB58DAB7CB2F8DB8F5773FCEAE5B6B7FB59DAFF00178B987FB7116FE95AB515B9D11F795D33F413E3F5DBE9DF037C7572AADB9347B80091D0EC22BE06FF00826FDB87F8ED24A4FF00ABD36661FF007CE2B98F881E34FDA23FE111D4A1F17CBE218BC3F247E5DE1BB84A47B4F182715D87FC139E64B5F8C7A94A42ED8F4B9793F502B272E889F66DBB9FA4CCD95C6E1CFA50A9F2ED2CAE71CFA8AC45F144170C6351838FE0A737892DA29513C96248C16CD5D93072B1B2B229C0DEBB7E94E8D9A4C6E2A0740C6B05FC4D0C8E112DDA4546C06C802963F14232B325BEE00E065AA7947D0E8A3986E24630BE95F8E51DC87FDA0AE27237A9F11C8E40EFFE907FC2BF5735CF1C4DA4C636E9FE734808055C607BD7E4BF83F7DEFC5EB49DC7CD2EB2D211F5958D6908C77664DEBA9FA7B71FB415A4734817479CAAB603195466A2FF008687B45FF981CC7EB32D78DDC481A673EF5599C54C9DF445EE8F6B6FDA32D14FFC8024FF00C081FE14C6FDA46DB1C680FC7FD3C0FF000AF132D4C6FA54215AC7B51FDA4E3C9DBA01FC6E47F8533FE1A4BA8FEC0E3B62E47FF135E28D8A418E78A7B1A5F43CE7F6EAF88CDF10EE3C183EC3F614B55B9E3CDDE58929EC31D3F5AEDBF67ABBF23F656D5A1E85F53C7FE3C95E27FB5137FA5786BFDD9FFF0064AF43F82FAC9B6F821169AA389EFDE52DF4FF00EBD2BEBA8E11B5E4D1AADC1A4A731CD328B92286A9616E6A0DA6A58E934AC4B2F7C4ED62DDBF664F19697238170D736D222F723CE4CD7CA5E1DD7BFB03C0BE24B14389F5410C03FDD0D96FD057D03F13E4FF008B6DAF20EE919FCA45AF99768F287AD5C2A72E88CF95BEA7D41FF04E4B158FE306AF7A146EB7D264C1C7F79D067F9D7E8BC7A8B6492D8E6BF3DFFE09EF225BF8E3C532938FF8972AE7EB20AFB9E3D5119B008233D734733916958F12FF0082856A8CDFB3A98CBFFACD56DD3F99FE95F993036116BF43BFE0A197864F81BA7A06C06D6A1FFD01CD7E7646DFB9401589F500E2AE30FC47CDF651F53FFC13DE455F8E9331EFA64C07E95FA38B83D483C57E367C2DF8C1E21F82FE296D77C3C96ED7BE4BC1FE93133AE1BA9C0EF5EA2DFF00050AF8BDBB2B269871D8580FFE2AAFD9A4B40516B767EA26DF93EF0DD49B07F16D63EB5F97BFF0F08F8C6C33BB4FFF00C001FE3437FC141BE321FE3B1FFC001FE35CDCC65CC7E9F7920295CAF34AB1F97C295C74E4D7E5EFFC3C1BE31FF7EC7FF003FF00AF49FF000F00F8CCDD24B4FC2C3FFAF54D8733EC7EA1792ABB718C0EA33485577678CFAD7E602FEDF1F1A5FEEB5B7FE0BFFF00AF4C6FDBBBE37B31DAD0FE1A756892EE545DDD8FD435503B863EB9A5F246D3C8E7BD7E5E2FEDD5F1D1B18917F0D36A61FB707C76901CCE47A634D1FE1594A491BB8347E9F7921401F291F5A6636B623FBDDEBF2E0FEDDDF19ADAF160BCD56243B977472D8AAB609F715FA69E1DD465D63C33A3DF4E774F756514D26D181B99013FCE9C75572395F534B70120EA4FD38A7AEE55EB8FC2983E5DB9E17DE85DAAC72D9143047917ED59F0353E367C339D2C907FC243A69371632639620731FD1857E5C4D693585C4B69731B43750B949237182AC0E083F4C1AFDA68E7756DCB9C57C91FB60FEC9F378C9AE3C75E0C814EAF1A16D474D887FC7C28E4C8A3FBC076EF8ACF67725CB94F83B8FAD2D332559A37431C8876BAB020E6943574C6CD19357D47514B4516EC4EC2629856A4A4A399A2D488A8A908A6ECABE64573052D18A29E85EA14521A2985C729A764D329771A340E541CD26DC5193452B8EC85EA290A8A28A36172B60AD5203B85474E56C54B570E5E5446CA4D0AB8A93229BD4D5227DE64808C544FF7A9C3348CA4D428EA527CA3569D8A4FBB4E3C8AAB761A95C4A319A36D28A762989B41153E977F77A2DF25EE9B793E9F7719CACD6D218DC1FA8A8BB7149C629333D63B1F4AFC2FFDBCBC73E0D48AC7C47145E2DD2D7E52F3011DD28FFAE8061BFE043F1AFB33E167ED0BE05F8C9670FF0062EAC96BAA919934ABC2239D4FB03F787BAE6BF277752C524967324F6D2B5BCF190E92464AB2B0E8411D0D43D771297467ED44D1B472056183EA4546CDBD46EE99AF81BE027EDD5ABF845A1D1BC7E26D7B49E1135256DD730F6F9BFBEBFAD7DD7E19F11691E36D0ED75AD02FE1D4B4DB85DC92C2DC0F623B1F6359B8DB61A92D8B870ADB95BEA69E5C727247A51B73BC74CF5A4F302ED046454943F866518E7FBC6937337CBD541C5006EF9B70C7614AAC3693C14FD6A6C01B4311F36714D1F78B77AF2FFDA83E216B9F0C7E09EB7AFF0087664B5D4ED9E00924A81C61A4553C7D09AF81ED7F6E2F8E17F23FD87525BB2BF7960B056C7D702AA3BE88CF99DEC7EA3AC60A92483CE69EA7E7DCC4367D4D7E6047FB6D7C7C8492C49F5CE99FFD6A97FE1BABE3BA9C304FC74C35A697B1A38C92BD8FD3C8E31C9F9496A718CED09F28C57E620FDBC3E392F5117FE0B69BFF000DF1F1B97EF083F1D37FFAF53149B31E67D8FD3CF27E5DA085239C8A5DA19836146DEDEB5F987FF0DFDF1ABBC76BFF0082EFFEBD3D7F6FEF8D1DA1B4FF00C177FF005EAACBB94DC9743F4E9A30EE186D18FE1ED42A95DD8C0CFA57E63B7EDF9F1A7B45683FEE1DFF00D7A853F6FEF8D4C4E23B43EDFD9DFF00D7A4E3A1A537CDA1FA8F6DF7C73919AFC8CFDA8B6C7FB4578F369E0EA2FF00C857643F6F9F8D9B4158EDC30FE21A6FFF005EBC3BC49AE788BC75E25D4FC45ADDBCD26A3A84A669DD612AA58F5C0C7038AB4D463B85483337CCDAC84F4DD5FB51E04BC924F00F85C9738FECCB6EFF00F4C96BF14198B3F96E0A367EEB0208E6BF65FC0FA94107807C30AF22A1FECCB6E09FFA64B5375208C545799DDDBDD319141738FAD7E467ED316E347FDA43C6FB782353F307FC082B7F5AFD514D7AD9597F7C9F9D7E5D7ED828927ED15E2D78DB7095E19011FF005CD7FC2B350E6656C73DFB49EA8BE23F1AE937B13AC8A748B70DB79C1F989AFAD3C49E29B7D6BE0F7C31B0824DF259692A2503B13803F957C1E55A403CC6672A001B8E7F0AFAA7C3EDB3C2FA446380B691F1F856CD46289F68DBB22D739A09A5A6F7AC3718F56E95E83F05AF05AF8C63C9C6E898579F2F4AD2D03599744D452EE0DA5D72006E9428EA4B77D8E3BF6F8BC8F50F1B7858238629652A9FFBEC1AF5FF00D8C7E2E68DE14F8236FA5DDC5732DC437F39FDCAE460ED35F36FED45A9CDAE6B1E1CBA9400FE4CC9F28F706BB0FD9D7E4F87A411CFDB24E71ECB5BA8C56C2E577BB3ED26FDA03C3CBFF2E97C7FED98FF001A0FED05A032806CEFBFEFD8FF001AF9F0914D2C2B363DCFA11BF684D031FF001E57DFF7C2FF008D35BF688D0872B617CC7FDD5FF1AF9F323D052161E8292D856B33E80FF8686D1589FF0040BDFC97FC6BC43F6CAF89DA7FC40F81B7DA7D9D8DCC32C37B05C9925236E0360F43EF59B91E95C5FC665F3BE1AEB2A3A6C527F061445F2B2E5A8FFF00826CDC6CF1878B63CE3758467F29857DFEB2EE007079AFCE5FF827EEB3FD8DF1235B8F6075974EE9F4916BEFFB7F11C7303980F03276D09DC4972EA6EEFF002DC02540EFB7AD307EE8927223CF5CF26B2EDF5EB768DCA45217638DAC39A9E1BC56854BC6DB98E02B55582F76682BEE50C5FE43D077ACCF196DB8F05F88632321F4DB85E3AFFAB35657508D5864150BC1E3835575CB8FB4787F555C150D653707AFFAB351C97D024EC7E5E7EC217FF61FDA6BC3B183C49E745D7D51ABF57BC99918E51BE9B6BF0CFC37ABEB7E1EF14437BE1DB9BAB4D6A3B9C5AC969FEB03938007BD7BBC7E3BFDA9239372DDF8C4E7A9F218E7FF001DAD792305A931BDCFD585864DA4189D9BFDDA77973AC6711B839EEB9E2BF2BFFE1617ED53236167F187FDF86FFE2697FE138FDAA9BFE5AF8B8FFDBBB1FF00D96B1E62F9657B58FD496B7755C0DDD72722A2D8913301B998F551D2BF313FE1A13F694F86F6C355D69F59163130DEDA9D9662C7A1F96BEDCFD9A7F686B3FDA23C06DA88896CB5ED3CAC3A85A4678C9FBB22FB1E73E98AA8FBC4B8D9D8F5A66DB9251B731E84F4A6FC866072D91CE3AD49B97861BD881C961D69DCE01551B8D6822377942EF0383C73C53A409B06DFBFD393C53B9DA41456CFA1E951C31A45B8B23104E7AE6A463374FF00DD4A2ACF9F0FF70D1458643B517690C4B03C7BD4AB23FCC4A9F97A134D62EA1146D6CF4A73073966F94F403B1A0408DB99999083D80E95C77C73F1DB7C37F83BE2DF122A84B8B4B26580E7FE5ABFCAA7F36AEC959959837CED8C8C76AF9EFF006FEBC92DBF665D41159945C5FDAC6E071C6FCFF4A956BEA5691F88F94FF60CF87E9E38F899AA78AF568FEDCDA5AF9CA6519066727E639EFD4D7E88FF00695E7189A4007006E35F9AFF00B36788352F0C7866F65D2B519AC5EE25C4BE4903763A678F735ED11FC5CF14C670BADDD7FC0997FC2B5D2DA133717F09F611D4AEFF00E7B49F99A70D4AFD7EECB281ECC6BE3D6F8C1E2D618FEDCB803EABFE14D6F8BDE2CDBFF21CBAFF00BE87F854B8DC50D8F59FDB1AEAF2EFF675F15A49233AF9719224E7FE5A2D7CA9FB01DC7F67FC52D5E60148FECB9000DD3EF0ADBF8B7F1035FF0010FC3FD66C2FF56B8B9B6922CB44CC083820D7977ECEB21B7F106A7E5B3464DA9E549071B871C5128AD8A5396C8FD125F193A49CC76B9231F2E01A7AF8AD9B198ED8FB922BE5A6B96C6031C7FBC4546D3B9E77B7FDF46922248FA97FE124902E152DD7E6C9F9C734F5F133A3023ECA833CFCE3FC6BE52695D8FDE63FF0234AB2B7739FC4D3762E2F43E9CD63C44D25ACE24B9B30B82466419C7B57E697821843F13B4F63C05D5093FF007F1ABE97848691738EBEF5F3468384F8956E0701753238FF00AE86A1CBA217B35267D7D2EAD6FB8FEF9739E99A85B54B7CFF00AD15CFB31DCDF5A29E856C6EB6A96C3FE5A8A4FED4B63FF2D6B019A9CA722A44F636DB52B6FF009E947F695B28FF0059FA56216A7061E9431A3C8BF697996E6EBC36C9CA859F9FC52BAFF84F201F0EAC173FF2D643FAD717FB4472DE1F3FECCDFCD2BADF84ADFF00141D98FF00A6927F3A2D12B591D886CD3B75317A52D2B10C70A72B54745364A39DF89CE7FE15FEB98FF9E4A7FF001F5AF9B3CC2231EB5F48FC4E6DBF0FB5C3FF004C07FE84B5F34AB6E02B5A705BB21DDB3DB7F661F185C784F5DD6A4898A99AD953819CFCC4D7BF43FB405FE9B70C24452A3A135F197863C5177E179A596D638E469536379A0D6FB7C55BF932AD696B83DFE6CD6F656D0B8B3DD7F6B7F89CFE3CF813A1BBA2C72FF6E282AA7AE227FF001AEDBF607B1D36E3E17EACF73A6D95D4BF6DC6EBAB6494E31EE2BE3CF1578DAFFC55A2DAE99388A2B5B79FED08B18232DB4AFF00235EEBFB2EFED01E1EF84BE17BFD2F573389A7B812A18A22C318AC2ED4AE6D647DCAFA1686FCFF006169393DFEC117F8547FF08FE8C8DB5343D2C0F6B18FFC2BC27FE1B3FC1249FF0048B91F580D31BF6CFF0006F55BB9B1EF09AD757D4E794D37A9EF9FD8DA5E71FD8FA681FF005E71FF008537FB0F4C239D274F1FF6E91FF85780FF00C369783FFE7EA63FF6C0FF008D0DFB69783FFE7BDC1FA406B171B0DA491EF5FF0008FE99FF0040AB0CFF00D7AA7F8527F6169C3A699623FEDD93FC2BC00FEDADE101FF002D2E8FD21A6FFC36C783C77BCFFBF557ECC343E845D26C93A69F663E96E9FE14FF00ECFB6E7FD0AD40FF00AE0BFE15F3D27EDAFE0F6EBF6CFF00BF54ADFB6AF83F6F4BC3FF006CBFFAF4F95EC4DD731F411D3EDC7FCBA5B8FA42BFE14BE4443A5AC07EB12FF857CECDFB6B7844FF00CB3BDFFBF63FC6A16FDB67C22A73E45FB8EE046BFE352A9DCE87747CE3FB6846B27ED077C8B1A44A915B8DA8A00CED07FAD7EA07841045E0DD0108FBBA7C009FF802D7E4CFC74F1D5BFC4BF8B179E21B48DE3B6B87895164EA028515FACFE1F5FF008A6F475CF0B670FF00E802A65D91946F27A9A995662DD7B51230DF8CE47A014C55DD920EDDBD8F7A2253131CB800F42D59A469B122E0F1938EF4E493C9E5791D36D317383C82BEB4367CB5DA39FEF629B44B47CDBFB487EC6BA3FC505B8F10F85043A3F8A305A48546D86EFD8E3EEB7BFE75F9FF00E2AF09EB3E05D6A6D1FC41A6DC6957F0B6D68E742BBBDC13C11EE2BF64D5B6B13BB915CA7C46F84FE16F8B5A4B69FE26D262BD5DBFBB9D4EC96127BA3F51F4A517CBB1938BE87E42AE3B53ABE9EF8B7FB05F89BC2667D43C17707C49A62E58DA3E12EA31F4E8FF0051F957CC7A859DDE8D7B2D9EA16B3595DC470F0CE85194FB822B5DF627996CC61A6D2070D8C1CD3CB629868C69EBED4E5A377B5252B7517A0EDA28DB4983EB4B9A9BB2EEFA8C64F4A4DA69FBB9A4FBD5776914A4866D3462A50BF8D34AD2522EE328A5DB4BB6AB990F41B4538AD37155742BF60A28A4A61763B3ED49451405C32697753371A519A351682FE3451453B8EC2734A2978A4A02C3A91A928CD31894E5A4A506B3947A99490D65CE41E6BD07E0BFC7AF137C0BF1025E69370F71A5C8C05DE9B231F2A55EFC763E845703D698CA0F07A54A641FAF9F0ABE2A685F19BC1F6FAFE853AB06189EDDB1E641277461D8FA7AD759B4704F2D8E98AFC9FF00D9FBE35EA7F01FC7316A96ACD3E8F72C22D42C89E248F3D47FB43A835FAB5A5EA965AF68F65ABE9938BCD3EFA159E1954E41561914A56BE86D1698ED8548627EE8A95433124602D338E46319F5A747F798E38EDE958B2CF0AFDB933FF0CD5E22C1FF0096D6A3FF00232D7CF3FF0004E38625D43C5C644491BC98B1B941C7CC7D6BE83FDB91BFE31BF5F1C7FAFB5FFD1EB5F1EFEC89F1634AF84FAF6B32EB171F67B5BBB7540D827E60D9A94DF3684B68FD1D6B6B67E90424FF00D725FF000A8CD9DB7FCFAC1FF7E57FC2BC23FE1B0FC0B1AE3FB4CFFDFB3519FDB2BC083AEA2E7E919AEAE476B8A52EE7BBB58DB1FF00974B723FEB8AFF008542FA5DA1EB656BFF007E57FC2BC29BF6D0F022F4BC95BE911AA72FEDB1E090C713DC1FFB6553183B91169BD0F7C3A4D9F5FB0DAFFDF85FF0A6FF0064DA29E2C2D7FEFC2FF857CF8FFB6D7837B4B7247FD72A6AFEDB3E0D63FEB2E07FDB1AAE465B7A6A7D04FA6DB0E9656BFF007E17FC2A21A7DBA67FD06D7FF01D7FC2BC13FE1B53C15DE6BAFF00BF14D6FDB53C11FF003DAEFF00080D3E576B111924F43E804B789381676FFF007E57FC2A6FDC49C3D95AB0C7468148FE55F3A7FC36C78241EB7CDF482ABCBFB7078293EEDAEAB2FF00BB0A8FE66B351BBB1B34ED73C93F6FFD12CF4BF887E1CBDB3B486D7ED363FBCF2630819831E703EB5EDFA5FC5C69BC3DA3420B224561027E51AD7CC1FB4F7C68D2BE38EA9A1CBA558DDD8269D13C6ED7617326E6078C1AC9B7F8E5AB5AD95BDBC5A7D88486358D59F712428033D7DAB5E54B639799F31F5B7FC2CB0A73F6861CF435F23FC7ED486B1F15352BBCE4BC5173FF0001AAF73F1CBC43202123B28FD36C59FEB5C7EADAB5D7883539751BD757B8900076AED1C74C0AAB591B48AADC57D3DA0B6740D33FEBD63FFD0457CBF274AFA6FC3A4FFC23FA61FF00A758FF00F41158BD55C20ADA9AAAC69C6A3534FAC2D665B619E2954E29291AAAE4A3C8FE3F7CD3F8789EDE77F25AEDBE01DD476DE042AED8FF004B7FE4B5C3FC7F3F3787FEB37F25AE9FE09BFF00C5107FEBE9FF0092D357DD172D91EAE75383FBF4D3A941FDFAC6DD484D2E6275367FB4E0FEFD21D52DFF00BF58ACC45377502EA6D7F6A418FBF5C97C58D423B8F879ADA46771F2B3FF008F0AD4AE67E24B1FF84175B007FCBB9FE629C5733B1A5D230FF627B8F23E2ADD01FC7A7C83F26535FA0161A8318D80E3E82BF33BF67DBCB8D3BC57773DACCF6F38B461BD0E0FDE5AFA17FE130D736E06AF7807B4C4568E2A3D4CDBE63EBB8AEC0DA154A9CE4B5596BD1236E7EAA72BCD7C747C59ADB75D5EF0FF00DBC37F8D44DE20D55F96D46E98FF00B53B7F8D4A68957B9F661BBF3146F2A79CE33516A97C91E93A93BC88BFE8937DE61FDC35F1A1D6AFDBEF5E4EDF599BFC69D1EA571230DF348FC11CC8C7FAD4B7A9AB49A3E56F84517FC5DCF0E63E561AC41FFA3457EC25C6A172D2313348393C03C75AFC77F08DC3E9BF112CA685FCB9A2D4959197B112715F611F8D1E2D91883AD4A4FB6DFF00E26AE1172D5892495CFB0BEDD71FF3D24FCE945EDCAF224907E35F1C49F17BC58DD75BB8C7B63FC2A36F8AFE296C675AB927FDFC7F4AB7108BB9F5CF89B4DB7F19F87754D075555B8B1BDB7689E3979EA0E31EF9C57E75FEC81E24BAF83BFB513786E598C7637D7526937284E031DC421FC0E2BD653E25788649D1A4D62E4ED20FCD2120D7CDD2EAC66FDA96D75385F2CFAFC12EEFF68C8B9FD6A1C5475309DEFA1FAF5344D13326338E0835098F0E4E49FA55DD536FDA5B731009ED553C9FEE0E7D7B543EE6DCBA0D552AAC42020FE7416665C1E1315364AE1B0B9DBF95411B2C8AC0BB024F415371D85F97FBA68A5FB3B7AC94555C058F7050BB548CFDE3D697E5490210C4751EB4D6CB2FDFFBA7818A091955C924F3BAA8511FCC6DB95393EF5E23FB706832788FF667F11AC2A5DEC9E1BBE9CED5719FD335EDEBC1DC5BF2AADAE68969E2BF0FEA3A3EA09E6595FC0D6EEA4750C31593BBD103D373F19BC31E2AD43C3F6423B397644792B8CFE35B87E286BA7A4CA3FE002AB7C56F875A87C20F885AB7863528DA2582526DE461F2CB113F2B03F4AE604B83D40ADA31D353072BEC760DF133C407A5DAAFD10544DF123C407837C7FEFD8AE59A6E73BA9BE612339FD6B5E9746D4F6B1D0DE78CB58D4AD66B6B9BC678655DACBB40CD753F01F6C7E28BD8C7F15AB63F06AF35F33D585779F072E96D7C5CBB982892164C9E3B66B37ABB97A4753DE7776A6E69BBC7665C7FBD4C1202DF797F3A5A985EE487142D3770C72CA3F114DF313A79B1FF00DF42934CA86A4EADF3AE3D457CDBA1A9FF0085951E7A8D51BFF461AFA2FED56D0B0325DC1181D4BCAA3FAD7CE9A3CEADF1056652086D48B023A11E61E69F2A4B51B9B8BB23E986FBC69375237DE34DC9ACEC50ADD2955B0B411C514C9B8528A66EA754C8678F7ED0A72FA07FDB6FFD96BB2F85007FC20763FEFC9FCEB92FDA063063D05FDE6FFD96BA9F84CE5BC0B67FF5D24FE7528D1EDA1D9D14D53D29D566614628A728E940CE63E27AEEF87BADFF00D701FF00A10AF9B6288ED1C57D2FF137E5F87BAE1FFA623FF4315F37C67E51551A8F622C2AA63B530A153D2A5CD35D8E2B68B9322E93212BB8D3953A7A520CD3C9E2A9C522E526F44309029A585779F057E0EEA1F1D3C749E1AB0D46DF4C6F25E76B8B852C005ED815F451FF00826AEA8ABCF8F2C777A2D9C87FAD0B508D3B2BB3E36DE338CD2EEF7AFB10FF00C1357566C63C7561F436927F8D364FF826AEB631B7C73A71FADB4950ED1762B7D0F8ECB536BEC16FF826BEBE18E3C6FA5E3DEDE41FD298DFF04D7F108FF99DB4AFFBF12FF855DD0B94F90D7DA9598E3AD7D7D1FF00C1363C43FC5E34D2C7FDB193FC29EFFF0004D9D7DB18F1A6947FED949FE145D5CC5EE7C7409E714027935F5FBFFC135FC460FCBE31D24FFDB3947F4A824FF826CF8AF9F2FC59A437D4483FA544A56D8EB51E65B9F26AAEF9ED80EBE62FF315FB41A2FEEF43D283FCA05A4591FF000015F0BE9BFF0004E3F175BDF5BCD2F89B4731C72AB301E6124020FA57DEF0C22DEDE28861FCA8D539EF800573750D23A0898762548DBFAD2E15C8F9BE55F5A7796CADBBB0FE11D69FB82B731E3B914D3B1991282D912118F51432A9002C87683CD3D94B4670A369E7A545B76EE2AADCFAD56E5263F08EFCB1C01DA94487680BB9B77AFA5270B8C9ED4E04EE51B8018A0A0DEBBBD4AFAD72BE3AF85BE13F89566D6FE24D06CF532410B33C61654F7120C303F8D753B80E8BB8E7D29403F33751FDDA64B8A7B9F1C7C42FF8277E9D78D25C7833C412594872C2CF521BE3FA091471F88AF9B3C6FF00B31FC4CF87DE63EA1E1AB9BAB38CFF00C7E69E05C447DF2BC8FC40AFD5ACF3B01D84726A55765C1DDDB276FF002A13B19BA7D8FC529965B399A2B989E0957AA48A548FC0D0B20C75ED5FB05E2CF85FE0FF001E46EBAFF86B4DD45987DF7B70AE3E8E066BC47C51FF0004FEF873AE2C8FA4DDEA9E1D95B9558A4171183FEEBF3FA8A776F7172F29F9DDE667A0A4AFAA7C5DFF0004EEF18E965A4F0EEB9A6EB917F0C7296B7971EE0E47EB5E35E2AFD9BFE27F833736A1E0FD45E15E5A6B58FCF4C7D57355CD15A1293933CF40A5A8E78EE6CE630DD5ACD6F283829321461F5069BF685C75C7E3459B29AE52527D2932699E606E94FE29E9123560053B8A4CD2139A94B98AF84764514CA7D572D89E66260518141142D4ABF41B931A5690AD4869B9AAE6B0D48652EDA5A502AB9D16A484A4DB4E2292A94932C4DB494B93494C0281453BA5020C526DA4C9A32695CAB0AB4A4526EA55A971EA8CA51EC205DC083D2BEE6FF827D7C5B9B51B1D57C01A8CCD235B2FDAF4EDE7F83A3C63F9E3EB5F0D9F979AF41FD9EFC6927807E34785F5747D918BC5864E7AA3FCAC3F2358F239326FCBB1FAC8EBF29E3A52AF39193B295D9436EDDC76DBD28507925B8A4CD8F03FDBA00FF866ED70019FF49B5FFD1CB5F98CBC46A3007E15FAEDF1E3E16C9F1A7E186A1E1583518F4A96E658DC5C489BD46C60D8C0F5C57C9127FC136FC463217C67A6301DDA090552E5B5CC1A6DD8F8F8B1DD9269778F6FCABEBBFF00876C78918F3E33D2FF0008A4FF000A5FF876BF88381FF09AE9BEFF00B87AD93E63751EACF90777D290B0F5AFB0BFE1DABADF7F1B69E07FD7BC942FFC135759EFE38D3C0FFAF6928BA071D4F8F33EF467DEBEC61FF04D4D631CF8E74FFF00C057A913FE09A7AB71FF0015DE9F9FFAF5929F322251B23E38539EF4EC719CD7D99FF0ED3D4FBF8F34F071FF003EB253E2FF00826ADFB37CFE3EB10B9FE1B473FD68BA31D4F8BF68F5C1A558C37BD7DADFF0ED39DB83E3FB5E9DAC9BA7FDF55F23F8D3C2DFF084F8CB5BF0FF00DA05E7F66DD3DB7DA02ED0FB4E338CF150E491B464DAB331D5768E29C69AADF353A946F73397722DA6A556F979EB475A6B0C0AA93BE85C6CD6A18DD9AFA7BC3AB8F0FE983FE9D63FFD0457CC3130E7D2BEA1F0FF001A0E9BFF005EB17FE822A1A6272BBB23402D2D3734EAC59623503A518A3A0A4CA47907ED01FF0032FF00D66FE4B5D1FC14FF009129BFEBE9FF0092D733FB4137CDA07FDB63FF00A0D747F04D8FFC216DFF005F4FFC856F1F84B7648F41068CD3334EAC5AD486235228A7514C90AE7BE212E7C0FADFFD7B1FE75D10AE73E2436CF02EB47A7EE31FA8A94DC7540D687997C0E882EBD78D8FF9753FFA10AF68CFB578BFC169D62D72E8B3AA29B6232C703EF0AF6349636E5668C8FF007C569694B7147C89B229DBAA352BFF003D23FF00BE8538B20EB2C7FF007D8A7CAC4F47A8669F1B0561CD5769A15EB3C20FFD741556EF58B0B18DDE6BEB68C2A93CCAB93C7D6928B6CD3647CB737991EA777346C55D6E5D9597A8F98D6B45E2CD69147FC4CAE09F52D592D2091E790746919BF32693CC1EB5D17E88C759B373FE132D6FA8D4EE31F5A6FF00C265AE6EC9D4EE0FE22B19641EB4D660327753BDB708FBB2B2365BC65ACF39D46E31DFE6C56CFC0BD1DFC61FB41783AC0069649753864918F270AC1989FC01AE1E6B848E32CCC09ECB5F64FF00C139FE0ACB7BAC5F7C49D56D9D2DADD5ADB4D6718F3653C3B8F651DFD4D615257B1D318ABDE47DFB78DBA591830EBD0D555864015BEE861D8D2BEDDA5B7F27A71CD46C543E0E5B2381DC56445C73462490150476C134AF8DEBBD88238C2D35BCB675CEE0C3A734D550ACCB2360B1C8A690B71DB47F7DFF003A297EC69FDFFD68AAB0F945DEC8C5130770FC698CB2C0039E17D3AD0BB5D4E5F738F418A50DE5C7FBCDC39C8EF413B31CB20E5C9DA31D853FCC12050091EF8F5A8D641271BBE4C6738A6ACC194A86C1CF1C5496D5CE17E2E7C07F05FC6ED362B6F125833CF00C457D6E424F17B06EE3DABE76D43FE09ABE1B9E43F62F1B6A36A99E166B347C7E3915F631DFB00077678CFBD46C42BAA6C393E9492D6E24B94F8BDBFE099FA6FDD5F883739EFF00F12F5FFE2EA58FFE09A3A66307C7F73FF800BFFC557D978318271C9FE2ED4310AA0E4FE55B73BB0D3D4F82FE2A7EC0763F0E7E1F7887C4F178D27D41B4CB37BA5B56B254DE546719DC6BE44B798B46AEA4AB707E5EDC7AD7EC3FC5ED2CEB9F0A7C5D6217779FA5DC228F7D86BF1CEC33F66507EF0001FC288B495C9A8DC958BBE74F8FF5F2FF00DFC3FE348B24D9E6790FFC0CD0169BCEEA77BB30B3B0E6690F595CFF00C08D47B59B39627EA4D4B46DC553761C5D95888315F7FC6AFE82DB35CD388E317087FF001E155760AB9A4A84D4ACDBA6D954FEA2B39C93358F767D48C3AD20A377CA3FCF6A5CD409B0A0F4A6E68FE1A48428EB4B4DCD2EEF6A96B52CF24FDA01C7D9F43FF7A5FE42BA6F842DBBC0B6BFF5D64FE75CD7C7E5FF0042D149EBE6CA3F415D2FC1F03FE105B7C7FCF693F98AD525141CEBE14766A0E69F4CA703598ACF7169CB4DA55EB4C68E73E2A1FF008B75AE7FD725FF00D0D6BE7AD2B499F53B6BB921F985AC3E7483BED0715F417C5361FF000AE75DFF00AE23FF00425AF3DF80B630EA1AD6A96F2AEF8A5B1646E3D5B14D492D0C9C64F63CD55815CD2EEDD5D3F8FBE1FDD781E44933E769F2BEC8A4E8777F74FE1573E15FC16F17FC666D47FE115D3D2F12C02999A49D63C6ECE3193CF4AD54BB151A4DEE71679E69B5E89F123F67FF001C7C25D16DF56F1369B0D8DA4F38B740B7292316233D149E2BCF17D4F1572F7772FC8FA4FF00E09FAA3FE17A5C1C0C8D326C7E62BF46465941EF8CF35F9CFF00F04FF3FF0017D2E7D3FB325FE62BF4580381B87D2B272B9B3D90BB8EDDE39F6A68661D4809F5A7ED6690EC39A62AF2EAC0F2723EB5998D8697F98B03F28FE1A04996F6ED4F6F95F18C13DE8552A7683B97FBD4C76137306C29DDF4A7348FB772E719C134420C41B2147A53564DF8527863CD326C064639099661D4526FCAB32B70BCE0D3D95958ECDA7D483CD2152BC6D5E7EEB1A9657423DACDF36E0CADD00A972369C8DA3182DEF4D6630B0C4831EC38A7326704B0CF52290F608DB0EBB7963C734FF331C372DD07B546A448E0EDDBEF4F0CAA0AB37CDCF414C9B9E15FB457C7ABAF827E3AF00B5C156F0DEA4F2437CBFC4B8C0DC3E99CD7B7D8DEC1AA58DBDF59CCB73673C6258E48DB2AEA464115F197FC14B2D84DE1CF04B90491773A86FAA8FF000AE7FF00627FDA953C31243E01F185E6CD2E46C69B7D31C2C2C7FE59B13FC24F4F434465D0CD4944FBC7023500B0C3738A59376F55DBC63B54B243E5E1B0086191C547B9BCC254E702ACD13B8D5F959941207614823DAA724A93EB4F2C33C6776298ACCC4190714862860A0632CDDE9176EDDC491CD0247DCC7181EB8A5DBBF05793D4D0310CDB4F963737A1A904C383C91D38F5A689155B04F2DD7DA99BB0A368DC09CE7A51610FF309073907B67D29E2E0F4466C75C29A8CEEDE01231DCD032A4155E3D452D86676B1E17D13C4F198F57D1EC7538FD2F2D524FD48AF30F137EC8DF0ABC50B23BF8692C643FF002D34F95A1E7E9D3F4AF61C7DDC1DA7BE6874623E5F9B1D686D93CA9EACF913C4DFF04E9F0F5E6E7D0BC537DA73768EF605997F3041AF2CF117FC13E3E21E9A5DB4AD4749D6635E8AB2985C8FA30C7EB5FA1DC2E3AE7BD3571E5B738C9A71D0AF43F2A3C45FB30FC53F0AAB35E7842F9E25EB25B28997F35CD79F6A7A26A9A1B15D434DBAB271C1F3E164FE62BF6711D98E63E87D09AA77FA0E9BAD46CB7DA7DADEA771710AB8FD453E6666A1ADE47E2FFDA54F4A7ACC0F7AFD5AF117ECC9F0BFC54CED7FE0ED39246EB2DA29B77CFD548AF32F117FC13D7E1EEA45BFB2B53D574690F214C8264FC88FEB55A751CACB647E7A6E047AD02BEB3F157FC13A7C5161BDFC3FE25D3B5451CAC574A6173F8F4AF1DF167ECBBF14FC16AEF7DE13BCB885793358E275FF00C7493FA52E75B233E5BEACF301CD2D36EADEE74F99A1BBB696DA653868E5428C0FB820546B27E07D0D5A8DC5CABA12E3349D2981A9CAC287126D61734B8CF4A4A2B3D813B0D6A4A93DE9B5B45DCE84EE80534D29A4A1B2828A6834B9F6A490395870E697CB2688EA51C54CA567626E47B0D496ACF05F5A4C8C55A2995C11ECC28CD363CB4F028192CE140FA9ACEEFA19C9ADD9FB2FA05C1BAF0EE9172D96335A4321E3B98D4D5E3FBBE58ED1F4ACDF0CC66DFC2BA1C2C0AB258C087F08D6B404A1D88326003DC5277EA691D41A4DCC4F641D4533CC257AF2790295D82966DD9FA0A69DBBC79637103BD248BD102485B71CED27A5397746370CB67AFA546586DC30F981E2A40FF2EC24A01D455EC48A58AFCC4F6CD22485817CE3D149A164383FC4AA31F3526D3E5F2BF43523B0BE617CFCF8C8C8E680DB9B613818CE73D69ACC927CA0329EFC50660570A483D0362801C66C10A32DBBE53CF4A43F2E549C6D3D453F7A156259801DC77351B6D906724375C0A2E4C49A250D26EEC7D6BF217E38B83F1AFC7381C7F6B4FF00FA11AFD7B871E61FD6BF203E392EDF8D9E3703FE82B3FF00E866AA367AB095FA1C6D1BA9AD2054DDD6BDD3C17FB167C4CF881E10D2BC47A55BE9AFA76A5179D0F997811F6E4804823DAB44EEF42234DCB56786835279664DA07392063EA715B9F13BC03ABFC22F175D786F5F5886A16E89237D9E4F3108619186EF5D67C27F86D73AAC969AFEA31AAE9BC3C3093CC8413C9F618A277E857B2D4E53E22E890F877C50F616EBB112D6DCB0CE4EE29935EFFA0F1A1E983FE9D62FFD04578A7C6E9377C45D4DF3C948471FEE57B5E823FE247A77AFD963FF00D045271B2D4BD1688D114EA6AD3AB064B0A3B53874A2A7A81E35FB41C65A6D007B4BFCD6BA6F82B195F048F7B97FE42B9EF8FDCDD6823FD894FEAB5D37C1DC0F04478FF9F993FA56DED2EAC88D5BD4EDF6D2D377714EACB52D8514525512396B95F8ACDB7C01AB11DD157FF1E15D52F35C77C5E936FC3FD4B1DDA31FF8F0A16F643945BD0F9DDA3CE297CB38E18FE669DCF14EE2BAB644F372AB218A8DFDF6FF00BE8D3B07FBE7F334FC8A6F5A57B99CAEDDD916DCE4139FC4D288C0E700FD6A5F2C50C054B92E86EA5A6A75BF077E189F8C5F12748F08ADF1D345F48435C88F7EC5552C78FC2BEB86FF008265E8B1F0FE3EBE63FECE9F18FF00D9EBC8FF00603D246A1FB40C33EDCAD9D84F2FB03B703F9D7E92B4857976E3D8564A56771E895A27C6F1FF00C134BC3CBCB78E3536FA5A463FAD598FFE09A9E14E0BF8CB587FF76DA3FF001AFAFF0071C3156E9DA923F33E52D2AB0FEE81834E52723351D6E7CC9E15FF008277FC34D035386F2FEEF56D784641FB3DC48B1C67FDEDA327E95F4B5869B65A3E9F6DA6E9D690D8D8DB46121B781422228E800A995D646DFF00323E70035055973B883B0E47D2A2C5A8EA27CC577AE178E949B9FA8DBBCF6F5A5F2D1941CEDC9E169AAA8D26EE415ED4922C7FCD247C840DDEA33331604C407B9E6891230BBC1CEEFE1CD234C4B2A6C18C7E1557216E2FDA3FD8A29FE68F48E8A2EC6359032125B2D9E829ECA1937118DBC6D1CD40B2B36E47508A3BD39612AAAC8FCFD68277246C04562B8DB40601413B547A639A6B2B2AB33313CF4A64CA2668CABE58F4A45C6E2ED52C70CD8CE452A46ECAD83CE7A93CD2B3150038F954F1B7AD298DA6E8984EC73CD5098C60CC1449C063F769A8CD1C84ECDAABC73D0D2BC7B946F0CA73EB4A1833ECF986D1DE931C475DD9A5FD9DD5AB9DC2789E3207FB4A457E31788B486F0FF8A35AD35C6D6B4BD9A1C7D1C8AFDA181847229C649E6BF2B7F6BBF089F04FED0DE27804663B7BF75D420FF69641927FEFA0D59DDDD227D4F24A2A3DD4F5AE98C7AB3172EC2D2AB669BBB14DDC169F2DF60575B926EA74775E44D1BF40A437EB50B3714D5E69F2596A1767D196FF0011FC352DBC45B59B64728B90CC460E07B54C3E20786BFE83769FF7D57CDDE5AF603F2A368FF22B164DD9F48B7C42F0D2FF00CC6AD7F3347FC2C2F0D6DCFF006CDBFE66BE6D651FE453D63F96AA31EE5FD93E8B6F891E185EBACDBFEB4CFF00859DE17CFF00C8621CFE35F3ABC6290C43AD572AB9AAB348F46F8C1E2ED2BC4D67A643A6DDADD490C8ECFB54E002B81DABB5F836FBBC16ABFDD9E4FE95E0D1ED552315EE3F04D8B7846607F86E987E82A25BEA0A2B5677F4B40A1AA192D8BD85396905396931A396F8A71F99F0EF5D1D3F71FF00B30AE1BF6755FF008A9AFF00BFFA19FF00D0ABBAF8A47FE2DFEBB8FF009E1FFB30AE1FF671E7C4B7FF00F5E67FF4214E316DDCDA3B6A745FB447C9E07D3CF7FB70FF00D04D7AF7FC136F3FD91E36C8FE3B753F93D792FED1B19FF841F4D3FF004FFF00FB29AF66FF00826EC20786BC6921EF3DB8FF00C75AB67251B19F35F43A2FDBFA14FF00853F60DB7E65D522C7E4D5F9F4A72A2BF427FE0A0CA57E0DE9F81FF3158BF9357E79EEC20C55AB486D69747D31FF0004FD61FF000BD67F7D326FE62BF46176AAF3939AFCDEFD8073FF000BD9F9FF009874DFD2BF47D70CBC9C1C76A99AB226326C50A15B86604F6A729328E3E5C1E734C591CEDC0E4FAD3955B7027F1AE700552B236F39069A986DC3CC207A53F893E662723818A6B01B8AB1DADD6A877068D54AAB1665C718A91982A8CA8E6A331148F76E6CD091FC8BBB2F93405C568C2EE7C6D19FBBEB4A5559B7B1E0F45A4DA0B1CE463B531551B80485CF268B8EE3A25F9C8F2C91D79A72E793B003DA9ADF7C88D98B52306F941276F53412C93960AE57E65E8A2A4877B0C950A7D5AA2F2FE6C866C9E053A30F20F2C93F29C9CF7A2C558F8DFF00E0A5193E12F051FF00A7F9BFF40AF84DA1DEA0E70C3041AFBCFF00E0A4D1FF00C50FE0A6C631A8CA3FF21D7C26ABD2AF916E66E9F53ED6FD90FF006C42CB67E04F1F5CFCDF2C3A66AD337E022909E9ECC6BEDA953C93B7395C0E7EBDC7B57E26CD007008F9587461DABEB7FD993F6D6B8F05DBD97853C7B2C97DA20222B7D5186F96D57B07EECA33F5156D2B6811B6C7DF06409F73273C7348B9FBE5B81DAAB697A9D8EB5630EA1A55DC57FA74EBBE2B881F7A303DF22AC47F31C1E83938AC8B1EAE1949CE73C629011B429073DA99E58DC594FE1E94AB95EADCFAD201D1AA32FF00B5EF4D65F95555896EBD38A153CC90E71B68FB9B06E207A1A5A88368911CB1E7D0530A158D816C2E322A48C317049001F6A6C3B9D9B201EC49A770B8A36B14193D297CBDA366589EB4C6569178500AF4A7EE656003E5D871C50319CC203649DDC629195C20391C9EF4F20B052EFB4FA535576EE24E4E78140123618AA2363DD698CAD1AEDDD939A70431C61B0339A72AA6D65CED279A6171ACC50A7F11F6A728665670B86CFAD46A8554323166069DE533F3B8A9F4A41B0ABB963CB364E3D38A7C770EB923A7FB3C542CA632EB92C3B0A19404CA923D8501B985E2BF87DE16F1D5B18BC41A0D8EA887FE7BC2A587FC0BAD7807C40FD803C13E2512CDE1BBEBBF0C5DF24464F9D6E4FD0F207D0D7D3FB5D88DDC0C6326A3DA1546F724FA83429323D9A3F2EBE297EC95F10FE15C725DCFA7AEB5A4C7C9BED3099540F565C6E5FCABC6A39771C0C861C10DDBDABF6C16439DB80EBDF3DF3EB5F3C7ED05FB187877E29D9DC6B1E14B7B7D03C54A0BE210120B93FDD7038527FBD5B7B4B2D42DDCFCDE5E69FB455ED7BC3DA9784F5ABCD1B59B592C753B390C735BC830411DFDC77ACF67F4ACAFCDB0722066DB499A61FD68E7BD6F18D87A47442D275A5A4DC28E52C4C53946685E7A548ABB69CA5CA8CC555DB4BBA9370C629A6B0F8993276119AB6FC03A2C9E24F1E78774C8C65EE2FA14C7D58560C8FB7DABDFFF00617F02C9E30F8D70EAB2479B1D0616BB919871BCFCA83EB939FF0080D549D9590460E67E9425BAC31C516788955411D3818A372B2B00C073CE450CA99DE1CD309499BB81E98A8B1B68B41C5BC9F93CC057E9CD2971E60D87B77A48F6F058E1B3C51B8ED3B987CC719228B8AE01A358F3F78E79C53BF76CCAF924F606A2F21638CE1B1DE88D64914371B870290EE4BB924EBBB70EA29AB1852B206C0CFDD347D9E466EB876A6ED2ABF300D83DCD0172663962179DC3EF53158A654A86653C546B1C992C0EC5A9155B66085F9BF8B34862B0218330183DA98CCDBD828C52EDD8C049CA8E983464AA373F293C50244F0B02F80B83DEBF20FE3D647C71F1B8E87FB567FF00D0ABF5F6155F306339EF5F917FB414607C75F1C7FD8526FF00D0A8BA4EC3BAB1E7327FAB615FAF9FB33C617F676F87EB8E9A6467F32C7FAD7E454C07966BF5FBF66C4DBFB3EF8087FD42E2FEB5B7369B0D5EC7E7C7FC140A11FF000D21A9000806CADCFF00E3A6BB1F87F6FE5FC37F0F478FBD6884D72FFF00050CC47FB475E91C6EB0B73FF8E9AEC3C06BFF0016FF00C39DFF00D0E3A8F68E4CCEE8F9FF00E367FC941D4FD7643FFA08AF72D178D1F4F1FF004ED1FF00E822BC37E3683FF0B0F521FECC5FFA08AF77D2536697620FFCFB47FF00A08AD5FC3A836AFA1697AD3A8C77A5C715CC296C1BA969B4BC52B033C6FE3E3EED4B441FDD8643FF008F0ABFF0C7C65A2E8DE174B4BED461B5984CEDB243D8E2B2FE3C7FC8734C5FEEDB13F9B7FF005ABCC5901EA326B78A46714E4EECFA407C42F0D7FD06ED3FEFBA77FC2C6F0C2F5D6AD7FEFAAF9B5611E9FA52B4231D3F4A728AB16E4AF647D26BF123C30DFF0031AB6FCCD31FE24785D7AEB56FF866BE6D48C734E6885118AB12DDA47D167E28785979FED987FEF963FD2B8CF89DF12345D73C37369BA7DDB5D5C4B221F96320000F735E47B16963401B8AB8C797566926ADA132F280F7A63353F701C54279A517764256D4728DCD522AEDA62D3E8946FB09CC5DD4C6F99B9A526A2964DAAC7B8A8D111AC8FB0BFE09B7A37DA3C65E30D59D4E2D6CA38037BBB8FE8A6BEF1D87CB2CA7254E30DD2BE58FF8277F863FB2FE10EAFAF48BB1F57BF291FBC710033F4DCC7F2AFA96595C0D9B77F7C56776CDA30E5DC67985DB6337CC0FDD51C1A7AA99B0248B90786148CBB57F76366D193ED4A5FE6500B303CE690EFD85DCD22B3329001FE1346F4DAB852E0F5DDD697C90031504A671D69143798559400A382690F512455DCCCA3DC6DA77CF26181545618C1EB4C5FDD6F677DA1BA5099DC048E36E3E5A0571ED1C71B0DC309D777634D7D8CD92014E836D091865E5D4A8E029ED48B19557CB727EE8038AB41723FB2A7F768A9713FF007FF4A298AE2058F6B039DBF5A4555F3BEE929F5A7322E42903E5EE0D23664575551D739A07715593CC6CE42F6CD1B449191B48DBDC714D698A6D560A3DC539A32586C2D2123B74A41718B12C9B54EE6C75ED4FDCF10DAA9BB2DC0F4A32F344146376791E947DD51B1C6ECF39A0422A6FDE4862C0F1CD0A1A4C9760BF5A56591A4E18281485B2CBBA318CF52691511D1C726DDA00FC2BE60FDB87F67FBCF89DE1BB6F15E83035D6B7A2C6CB3411AE5E6B7EA71EA41E71F5AFA77CC56DDF363071C77A786F273B5C9C8C9A9D11338DCFC4C4906D21C15753860C3041A55997B1E2BF5BBC45FB3CFC32F18DEC97BAA78374D9EEE53BA49A3530B31F53B08E6B05BF641F839B777FC21500C9ED752FFF00155B45B96E351496A7E579994F39A4F314F7FD0D7EA7A7EC89F07C671E0DB7DA3D6E65FF001AB11FEC97F085463FE10BB3C7BCD2FF008D6A9C621BE87E546F5E7BFE14F8D95949078AFD5D5FD947E1171FF143D8363D6493FC6BE0CFDB23E1FE91F0DFE374DA7E81A647A46913D9453C36B093B01C9048FCA8BF32D06E29A3C6062976D36334FACF9753976644454C9D2A26A556AD6DA16F55A1232E68DB9EB42B0A5AC25A329276D46F93DEBD9FE07CFBB41D422CFDDB80DF9AD78D6E15E93F06B5CB2D3FFB4A2BABA8AD836C7532B6D07E99A56948699EC59A5E2B323F1069327CC355B33FF6DD6A55D774B3FF00314B3FFBFEBFE34B95DEC26D17B75381ACEFEDED2F38FED3B3FF00BFEBFE347FC241A52F5D4ECC7FDB65A7C8CB5B199F1357778075C1FF004EE7F98AE1FF0066F4FF008A97503FF4E87FF4215D7FC45D634F9BE1FEB4B16A16B2C8F06D58D2505892476AE47F671CAF886F89E33687FF0043155B6847BCDD8E97F6906DBE05D3477FB7FF00ECA6BDBBFE09BABFF144F8C24F5BC817F256AF0DFDA49C0F04E9B9FF009FEFFD94D7BC7FC136F0DF0E7C54DDFF00B4631FF8E1A894754CDB974D0D8FF8285131FC1BD2C0E8DAAC5FFA0B57E792A65457E89FFC14363FF8B2FA59EC3558BFF416AFCEF5FB8B4465CAD9934FB9F477EC0985F8EC47AE9D30FD057E8E638E0A8E2BF377F60FB858FE3EDB21EB258CEA3FEF9CD7E92320CE3A1028BB95EE38D90864F3140239E83147CC5B6A92A075EF472CDB80DD81D3142B3C980BF291D7152531DB5F27072BF9535918E704330EB9A55DFF0079BAE78068C3BEE0C00E698866F95C124600EB5216DEBBC380A3A0A5F9F76D2AB83D29B249B7A0048F41417A0A5472C5F1C76A8F7EF4C160BF8548DF26E6D99C8A46F999495F97191C532445DBC282DBFB914AA5914E158313DE9566DBF3AAF078A6ED9235DFBB049CF34818E08CBF2E490C79A72B0390BB89EFC53543B10E492BDF15244DB94951D3AD05F43E42FF0082920CFC3DF071C74D4E41FF00908D7C1CAD5F7AFF00C14853FE2DCF840FFD4564FF00D146BE09DBD2B4567D41BD09339A6BA060411914E55A5FC2AEFA98B5D51E9DF057F690F17FC0DBF034EB8FED0D1243FBFD2EEB2D191DCA7756F715FA23F06BF684F077C73D2E27D1EF16CF5954DD3E9574E16543DF1FDF1EE2BF2836FE54EB1BABAD26FA1BCD3EE66B1BD85C4914F03947461D0823BD36930553A33F69E48CA3608C67DA98D1EE619F98015F0BFC0FFDBFAF743821D1FE22DACDAB5B70A9AB5BE3CF4EC37AF461F4E7EB5F68782BC71E1BF88DA3AEABE18D62DB55B56037085FE64F664EA0FD6B392B1AEFB1AE64DAAB85E0F7A555DE996E71D29EEBB47CE08F4A66DDDC13F2FEB516108CC242A9F31C52AB2B6F50BC9E0D3554EE049208E314A3F79B94AED00F5A2C483158C32B039FAD2EC58D54B3367DA9048A1B919DDC734AACD230561B7D2A6C02B286906EE063826918058DB8CAFF007B3479A6376DC030ED9A0075072A0A934C04660546D393E99A558C97CB903D714EE920658F39148B27CC329CB1C1A4CAB89B58C7F2B0519A71CC842A4A33DE97CB013731FC29A995FBA14AB7B54EA03BCBF9C0DC73EBDA99CC4BF2B6E20FA539711A9DA77907A1A3F79B87213BE314FD42E336F98C33277E451F2AE23C739FBC6A68D54B31C6E239E94F1FBD1BC20073D0D17EC044233B8F3C66A68F2BF32E4114CDDF3B64E3D6A65064F95793DF14B56163E1EFF828F780ED6DAE3C31E32B688477170CD61765463CC206E463EA4722BE2D51CF35F637FC1473C7F6D7DADF86BC176B2EFB8B00D7D7AA0FDC661B6353EF8C9FCABE3A1DAB78A514294D5B4142F34E7A370E94D3CD5EAC98AEAC693483E634F099A785C51CD64311576D0CD8A7532B3D65A92E560A6BB6052B36DA81DB27AF157B1318F33BB229A4CAF1939E303AFF00FAEBF503F643F841FF000AA7E135ABDDC5E5EB7AD6DBDBADEB864523E48CFD060FE35F287EC69FB3CC9F13FC5A9E2BD62DF1E1AD225DCAB22FCB773839083D40EA7F2AFD1C790CECCDC03FDD5E87FCFF004A96ADB9D1CD6564265D5883918E714B9DAC1B2DBBFBA2942491E719F31473DE9CB9DF991CA065A86CCC696F994C99EB91C50C629B706E0F5028F331B43062BFDE34EDC2693E44C363BD46E033C91B18B1247614A3E78C043B4FBD22DBB2A37CC460F3DEA449032E31F30AD2C1717CB3B95589E99DD9E94D9230AC876171DE88D436EDEFF77B50A3CAE416622A18D5C3FE3E262A015C0E9EB4A429454DBC29A7E1EE0865C2E050B89146F42314BD464651BCC1B5570BD8D488BB95BA373D3342FF001009F77BFAD2AA296DCA873DB9A1BEC22CC0AAAC140C0F7AFC86FDA1180F8EDE39C73FF1349BF9D7EBBC2774CA00C82715F903F1F5966F8E7E38646F317FB567E57A7DEC62AA9C6EEEC6DA8AD4E18FCCA6BF617F6758C27C03F018FF00A8543FC8D7E3CB1DB19F5AFD8BFD9ED76FC05F010239FEC984FE95AC9E9A109B93F23F3E3FE0A2B1ECFDA366F46D32DCFE8D5D9780141F87BE1BFF00AF28EB93FF00828FFF00C9C4291C6749B7FE6F5D77C3C39F875E193DBEC71FF5AE78DEE1C8A3B9F3EFC6CFF9291A80CFF0C5FF00A08AF7AB35DB65683D2041FA0AF02F8D673F137511ED10FF00C7457B95AEB5A71B2B62751B507CA51832A83D07BD5A527B909ABE868B506A80D6F4DEFA9598FF00B6EBFE34378834955F9B56B11F59D7FC69F294E48BB4EC62B2A4F156851FDED6AC47FDB75FF1A23F156893C33CD16AF66E90A6F7C4C381EB4B95B764696D0F23F8E936EF165AC7DD2D17F5635E76BF35749F103C516FE2FF00165CDF5AE4DA8458A266182C17F8B1EE735CF37CB5BFC2AC64DB7A20143547BA9402D438B29251DC131CD3986EA6ED34E5AA317AB23298A5C6CE6A4C545349B549EE2B394BA1AC7DE1ACC0B804E0D3CED515F6E7EC77FB36F80BE20FC22FF8493C51A21D5AFEEAF2458D9AE5E30A8B8180148EE6BDD97F649F83E9823C19031FF6AF266FFD9AA55EE695159591F957E6AFAF3F4A5F357FC8AFD5C87F659F8431F4F035837FBD34A7FF0066AB0BFB32FC2255C0F01E9A73EAD27FF1544A56D1184637DCFC987B845E79FCABA1F871F0E35DF8B9E2EB0D07C3F6535C4B3C83CC9954EC857BBB9EC00AFD468FF669F8490FCE3C03A4E73FC4B237E85ABB3F0EF85743F07DAFD97C3FA2D868D6CC7056CADD62CFD48193F8D28F99D4AD1457F87FE0DD3FE1AF81F47F0C69C02DB69F02C45D860C8DD59CFD4E6B78B7CA77BE431C0C529DF80A537367A75FC6946DC3028739C01468886DB068C6FE58B2E3A7AD237F71D1829FBBB691D8AB246E020A5DD1F9A41627038A9B922793E4AA853F293CF3CD3A48D5D9821C0C756351C7B390C8C59BA0A0BB2C4309F2679A631157F77B36E7033B8D3CB48ECBF2280A383D29DB91CA8C30C7231DE918492029B72ABDC9A6483EE6936AA2856EB8A1A37F314AEE00763D285DD221E40C70145013CB8C6E6656EDCD201DE7CFFF003CC514CC3FF7CFE54502B305C2A96117CCBDDA9BFBD690128155BF8B34E69873856C03CD3A4756562A0FB6EED491774275C21D98F5C734DDADB94C6FF203834E8C6D660A431039A456555605496CE714C2E398BC595665407A03D68DB1F96AD905875C508BE64DB9C6571FC547FA9562A15B27A03D290987D9D6490B16E48FBB511755C26DF949FBD9A916479995B6851D0D4C163F2F1B138E9CF4A04995EE644B554541E664F6A7E437CF95566E086142E6DCAE4AB93CD0DBE45C928A09C826958D81860ED047033D28E91AB63E4CF43D68F3195B6860D91C50BBD577B60EDEA0F7AA336C4C44CB8E0B67A0A70059433E102D0376ECE13E7FD29A0B6E72A0B73D0F4A411248760191CB357C05FF000522D27ECBF10BC1FA8803173A7C9096F528E0FF00ECD5F7FB64B7017A727D2BE36FF8293787DAE3C23E0BD654022D6FA4B6665F474C8FD56AA32E5DC6CF85D453F69F4A647DAA61E94E52BBD0C7948D969028CD48D4DC73549BB14ACB418CB8A7EEF9694D31D78AAD1AD497AE831DBB52322B0E573F8534E7352C637553D1685C525B8D5853FB8BF90A78897FBA3F2A7ECC52D677BB339456E42D0AFF00757F2A67929BBEEAFE5561BA5342F7AB72B1707A6A288D707217F2AF54FD9E181F165EA67ADA363FEFA15E567E6E2BD3FF00677CA78DAE07ADA38FD45617EA689A5AB3A5FDA4949F04E999FF009FD27FF1D35F40FF00C13760DBF0AFC46F8FBDAA28FCA3FF00EBD7CFFF00B4937FC517A5AFFD3E31FF00C74D7D1DFF0004E88F67C19D5A4ECFAB31FCA35ABE6BE84272BB3AEFDB8FC19A878C3E045CB69B6ED712E9B751DE491A8C9D8B9DC71EC0D7E62EEF900EE38AFDB8F32396378A545960914ABA30C8607A835F9DDFB5DFEC9B3FC3DD42EFC63E1689A7F0C5CCBE65C5AC6326CDD8E49C0FE027BF6AA8A5733B3E63E70F03F8EB57F86FE2FD3BC45A34C22BEB29448B919561DD587706BF557E04FC70D0BE3B7849352D35D61D4E103EDD60DF7E16F51EAA7D6BF23F683C835D2FC36F895AEFC21F16DAF88340B930DCC271245FC12A77461E94E7DA26BCBA5CFD8D690F24F1D80148AC147C99C9EA3D2BCF7E08FC72D0BE3A78463D5F4C74B7D46140B7BA697F9E07EE7DD4F635E87B5B8CAE4FAF6ACF96DB890C55DDF392703B53F68750C43633422ED380D8E7A50ECDF30FE0CF6A865879C79C28031DE917744BF2F25FF2A72B3172C0285F43422BB3AB6EC03D2A491CA1BCCDA5C631CD28DDB9B71F9077A6B1321DAB8F94F5A325958BEECE7002F4AA1DC4C71CFCEB9ED42EE8F73B05C76DD412CCA170401DE8662772F0462810AA1A46560E36E726A6854B31C6171FAD4224C6D545EA7353C770AEA4904B741B6A597D0F923FE0A480AFC35F087AFF006AC9FF00A28D7C0EB838AFBE3FE0A48DFF0016CBC259E3FE26CFFF00A28D7C0687814E9C6E65292E84A491D2957D685F9A9E16BA345A19DDB1290AE69C78A4A483D46ECF5AD4F0CF8B35BF04EA49A8681AADD6957887224B690AFE60707F1ACD34CC551A28B5B1F60FC2BFF82876ABA7ADB5878F7495D4A25C29D52C06C9F1EAD1F43F8633E95F5DF807E2FF00817E295AC73786FC4967792B0C9B467D93A13D9A33835F909C524464B6B849EDA692DA7439592162ACA7D411CD263BB3F6BA585A339C67F0A82453E98635F97FE01FDB13E29F80618EDA3D706B760838B5D553CD1FF7D70DFAD7BEF837FE0A43A55C0487C61E14B8B27E86EB49944C9F5D8D823F3358F5344AFB33EC3DBD3806918165DDD1BA62BCAFC2BFB58FC25F1834696BE2CB7B299FFE58EA00C0C3EBB862BD4B4DBED3F5CB75B8D36FEDF508187CB25ACAB229FC41AAB344EBD49472A1481C756A8C765C86DC706AC49672291C301D4E41E692450AB90A339A5610C76F270060638E29AA32099300F6C5089D8AE40FE2A568C961B5723BF34B94431577AE55F033DEA65739232005A1A35EA0608E829CCA24C2E36F7C8A4D14866C58C956E4B7A511E15D8EFDDE829CBF23127E6EC29572A728BF5E2A79580798ECA369196EC052B2AEF1F3EDE29FE4B7FAC63B140E4B7CA07E26BCC3E22FED29F0D3E17AC8BACF88E09EFD79FB0D89F3E53ED85E9F8D5280D5D9E9D12B4D9545CE7BF7AF1BFDA2BF6A0F0F7C04D12E6D2D278753F19CC845AE9EA430878FF592E0F18F4EA6BE5EF8B5FF000508F12F8A219B4DF02D87FC23362D9537F39DF76CBD38C709FAD7CA53C971A85F4F797D3C97B7B3B99259E662CCCC792493D6AE30D752277D8D1F10788B51F17EBD7BADEAD72F7BA8DEC86696690E4963D7F0ED8AA47E55A02E393CD233E78AA96E28A4C6EEA72D22AD49B78AABE9644CA56D10E1D290B5368A8E5EE4F331698CD8A46936D412CCA99DCD83D87AD3F245C60DEAC73C9F2E58E057A87ECFBF00B58F8EFE2A8EDE38E4B5F0E5B386BED44A9DAABFDC5FEF31FD3BD74BFB3C7EC97AF7C6A9E2D5F5659343F09A11BA771FBDB9E7256307B7FB47A57E8EF837C23A4F80FC3B6DA2683651D969F6E984441D4F724F727B935A691DCD3C90BE13F0AE97E07F0DD8683A2DA4769A75947E5A4698FC49F539C93F5AD8FB3EE5053181FAD23466E37004A1FEF1A7065DA5188C0E38AE794AE3B036F888DAB8DDEF9A25CAB2050BD3934BB95645014B2E29A855646E3047F0D6621372F9814B314F7A56C336E50C76F18A19CBA852083D69C5996454C718EB5A210C0AC91B3124376A58D9B6FDDCEEEAC690C322A9232DCF6A90A962AB8C37F749A6C10CDDE6646D41EE3AD48AE195B0C36E79CD27FAA76C26DC8F4A57C28FB809EB8A8286C91EE6531FFF005AA41BD4EC3D33C9A632B163B7006338A936EEDB80DBBB0A5BE821598AB615B3ED8A48F73B155E07F794669D1C6EF3140BD7835E07FB557ED5165F013476D0B4468EFBC6D7D1111C7C15B243FF002D1F1D5FD053D8894AC52FDACBF6A8B0F837A3CDE1CF0F5C4579E35BA4DA446DB85821E3731FEF9EC2BF35E6BA9EF2EA6BBBA99A7BB9DCCB2CB21CB3B13924FD4D32FAFAF359D56EB54D46E24BAD46EE469A69E5625998F249A8D59D9D51159A46E02A8C927E95BEC88BB9683BCB69E5586352F2C842AAA8C924F4007AE6BF65BE09E9B75A2FC1DF06E9F7B0BDB5E5BE970472C320C323051C11EB5F327EC83FB218F0AA5AF8E3C656A926B12A8934FD2E55C8B653D24707ABF4C0ED5F6546E42FCDD7359F33D8E8F8158FCCBFF8294C663FDA02CA423FD668F09FC99C5749F0CD849F0DBC33DFFD0D47EA6B33FE0A716FB7E33F8765FF009E9A2AFE92B55CF8467CCF867E1CEBC4057F2635AFBBB99FBD27CCCF9FFE319327C4CD672320141FF8E815C78894E72AA7F0AEC7E2F1FF008B97ADFF00BEA3F415C874A85EFBB92A1D58AB1A8FE15FCA9E235ECABF95229CD480F1576B193DCAED1AEEFBA3F2A5D83232067E838A919699B49AAD11BC5B68915475FD7D69B253BF840A4F2FBD657BB2969A8D55A7D3B6D0466B6E873B77622D382D22AF34FAE76D9AD90D3552E14F23BD5B638AAD70DF28DA39CE05357DC39944FD4CFD8CF477D37F66DF098D9833ACD3FBE1A46C1FC80AF6391766318DE7AA9AE5BE0DE8ADE19F84BE0ED2F1B5ADB4BB70E3FDA2818FEA6BAA924126E0107A12C79A726526E5B8A213B0B70A719E69A1D576AC8A107514DDA7CC08661F779A7B32B6C0A41703AB5663D98D1B99949198CD2EF5DCD84E08E69D249BB0A64FAA8A55CC2480EBB1BBF7A60352456C321C95E38E9448AEACAC64F9FD319A55569176EF5519FCE9159E12F9753B7A3548F6056322B009B5BB67BD2B2ED6460B9F5A4684B3172CAA71C1A6F985117E7CAAF5DA339A3A8AE356E3CC6DAE3680DF7B14BF36E2118328ED4E79BCE004601E7B8C524CF20C797B323A91D6AC771CBBE41BB7282BDA9194BC6CDBB61A3968F24297CE48CE38A6CC64F317EE88FAD2B0B50455914A87248EA7A529061D8036FE3BF38A7311312AA576639F5CD3238D6105FCC279C0E3AD3109BA7FF9E8BF95153799FED2D145C342256D8A408FE534F2AF1B64ED25C679A41B1A4632650FA03C5248AD33288D481FED503B0E9601300C2400F7029BB77150B2039EBB452A67680BB509EB9A4390AA131BBFD9140585643B426FE47AD3A344656C3285E869ABE503B8925870734DF976B3221CE690F947B6559230E36522465A47F950AFB1A372F5957E6C74069CD1B10854AA034C2C26E3F77E556EDDE9ACBB541FBC73F853B69660170AC0E33D690B347B7077F3523B8F5055BA28029047884B33E50B74A6AC999325B70EE28DC814B1C95278F6A40C3724CCBE5A11CE377A52FEF1711F55CF269CD2EC5DB19DB9E4E4546ACAE4B0C923AD32624888177ED05D9BF4AF0AFDB83C2C7C4DFB3A6B12C51992E34C9A2BD181D02B60FE84D7B9C72845014FCD59FE26D0A2F16786756D12E87996FA85BBC0E08ECC3159BBB07A1F8C30B064041A9B3E95A3E32F09DEF807C65AB78735189A1BBB0B8684AB0C6E00F0C3D4118359632335D10491839396C4949EF4CF328F32B45E45283149A1B269A4D1BAA8395EE3769A940DA2915A8DD9A993E843B8FDDC546C4D3BA52362A2364CA4DBD0179E0D38F14D514F65E2895AE3B3B5866E15E9BFB3F4817C7120EE6D5FFA5796E7E635E99F0071FF0009E2FBDBC829D48E85C12EA745FB4C4DB7C39A3A678372E7FF001D35F54FFC13DE110FC0392523FD66A92FE8A82BE4CFDA7A4034BD123EE2490FE8057D8BFB045BFD9BF670D358FF00CB5BFB8939FA81FD2A22B767436923E89DDB80E78F4C543716F0DE5BCB69770C77569329496295432B83D41069CD92CB9E1477069B91F36D505BBD4F5323F3C7F6B4FD93E6F86F793F8ABC2B6CF71E169C979EDE31B9AC98FF00343D8F6AF96F6EE518E98EDCD7ED6DC5AC37D673D9DE449736770863922914323291820835F9D5FB597ECB737C25BE93C47E1B825B9F0ADCBE648D46E364C4F43FEC67A1ED5B45DC89499E17F0CFE246BDF087C5F69E20D02E9E19216FDEC59CA4C9DD587706BF54BE09FC69D0BE3A784E3D5B4690477C985BDB0623CC81B1E9DD4F635F91ADB597D45759F09FE2B6BDF067C5D6FAFE853B23A9C4D6E49D932679561EF54F54289FB0657AE4E31C71481D551B824F63E95C7FC23F8B5A17C6AF085B6B7A3CC8B28502EACF78F32093BA9F6CF435D8331DDC0DC4D60580F9C10CFC37278E9402BB00F33033C1A6E7F784E4631CD092A6155403DC715203F72061F31C8EA7146E11AE325C13E94E593A96009C71473B79C6DCD002379932B0078EC29D9DABB4104E3F1A4660CC36BE0639CD1B42AEEDD83EDE9408154C78F9BF1A55E1828F987AD319959953767DE9C080C5771A45F438AF8BDF087C39F1B3C2E340F124736C8E4F3609EDDF6BC32631919EBD6BE18F8A5FB0A78D7C1324B77E1665F15E96A0B79308DB7483D0A1FBDFF0126BF4746D65247CC7A1E2987722E0373D877A5721415EE7E2CEA16F75A45E496BA85ACD637519DAF15C465194FD0D2C6DBABF5D3E207C23F07FC52B536DE28D06D75338E2665D92AFD245C115F2F7C43FF8276C2DE6DD78175E30752BA7EADC8FA2CCBFFB30ADBDDB0A72B6C8F8B830EF4EE2BB3F1E7C0BF1FF00C3191C6BFE1ABC8AD94F37902F9D011EBBD7803EB5C32DC2B67F87071CF145AFB18F3129C5016901CFBFE3527E14EF62D3634AD47B0F53526EA4DD53AEE573A232A7B535A32DC9E6A5A293B96A443E4A907E5AB5617F7FA34C26D3EFEF2C251C892DAE1E323F23511A377B52B364B91E85A1FED19F14FC3BB45978EB5608A384B8713FEAE0D775A3FEDD3F173492A27D474FD59178DB7764AA4FE298AF0303BD2D2D42E7D4D63FF051BF1BC2145DF85B44B8F529249193FCEB66DFFE0A51AAA01E7F812CD8F7F2EF987F315F2060535947A55F331E8CFB2BFE1E5975DFC050FF00E071FF00E26A09FF00E0A55A99E20F0359A9FF00A697AC7FF65AF8E997DA99B4569177DD094944FABB50FF00828F78DA746167E19D12DB3D199A47C7F2AE1F5DFDB8BE2DEB8A561D66DB4843D3EC368B91F8B66BC20AE2815A586A48EA3C4DF167C77E33DCBAE78BF57D422639314972CB1FFDF2081FA5726B6E371249627A96393530A3773486E498AA9B40C0C53D542F27AD37CCED46EA97733B3931CCD48AB48B4ECD67D472928AB21D46714C66DBD6ABCB791C6B967C7B555FA226306F72D6F1514D28405B3C5749E08F857E35F89D74907863C3F797F1B1C1BAD9B605F7321E07E75F56FC2FFF008276C2CB05F7C40D6D9DB863A669A70A7D9A53FD07E3472BEA6A9289F20783FC1FE21F88DAC47A5786748B8D5AF5CFDC8537051EAC7A28FAD7DC3F01FF00610D33C28F6DADFC409A2D675304489A5C4736F1B7FB47AB91E9D3EB5F4BF837C03E1DF87BA4A69FE18D1EDF4BB55E3F72B866207566C65BF135D06C1BC33124FF0010A3994761DEE323B78A3558618D2086350112350AA00F61D29F1A9CAF00A1FCE9DF7B6B06E7A05238A4671B40DC33FECD62E570B09F246802B1520F1BA95B322954DA48EBC5202B26142E48E79E29A53876FB9EA2A50F62519D8AA76AB53395E7E524F06868C48A640D800D3598E4AEDF94FF0010ABD0487B2EE9010D80053572F2160C4A81C83D691B0B212809C706A452662D81B7EB45CA18C447C02C55852FCAAEB90C46386A7098C4AAAEB9F7A5CF2D86C0EC315248D1BDD4FF0077A0CD384615D72D8DA29A02BAED56C7AD282AC36F2581C0A04387DD2369656F5A7C709276AF38E4B6691230D184F31B93D71D2BC67F69EFDA634EFD9FFC342DEC8C37FE31BC522D2C9B9100C7FAD90761E83D68D8CE52B157F6ABFDA8B4CF80BE1F9349D2A48EF3C6D7B17FA342ADB85983FF002D5C7AFA0AFCC2D5B56BFF00106AD75AAEAF7726A1A9DD39925B8998B3B313CF34FD7FC41A9F8B35CBCD735BBC92FF0054BC7324D2CA724927A7B63D2A86D2C4003716E00F7ED5AC23D58460E5AB1523966915234692466C2AA8C924F6C7D6BEFAFD90FF006448FC376F6BE35F1C592BEAB2012E9FA5DC282201DA571DDBD8F4AAFF00B1FF00EC8E9A14365E3CF1BDA6EBF6025D374B987FAA07A4B229EFE80D7D8CF31DC37162BD8D39EA6E9287A9334C0B658E1BA67D6A78E43B7E61CF6F6AA4B227CC24C965E338E2ACC0A3CBCA9DC33DEB07A107E79FFC14FA1DBF113C193FF7F4A9133FEECBFF00D7A83E0CBEEF865A173D03AFFE3E6B6FFE0A896B8D5BC03778E0DB5D45F93467FAD739F02E4F33E18E97FEC4922FFE3D5A74347B68783FC596F33E23EBC7AFEFF1F957255D37C4B93CCF1F6BADFF004F4E3F5AE697AD69495919C9E848870297348DC5357938AD3CC8495898734B8029AAD4A79AC5A7227E1038A3269286F6AA49216B2177528A88D395AA9A6CA7151D498535A98588A5196A8E54B525B6C435A5E11D15BC47E30D0B49552ED797B0C2157AF2E01ACB938FA57BD7EC3DE036F1B7C6CB5D4668D9AC3438CDEBC98F977F445CFB9FE55326F645C69F36E7E97C36AB6D1C50A6EDB146A83D3006286DCD9C228E79F7A7F326486C64D37CE11B6D74C8FEF77A835D16C3FCA0EA708071C9CD34ED6558B72EEE94981B77E4E09C15A53B1D03AC79238F7A0487089790CBB7DF34C50170770665FBBC71448A15831CA923851CD2993CCE784C0A0A13CC756C88D4B1EA0D498DA87CC4E3B0029AB97656320C81E946F0FB8B49818CE295892331BEE076E1314BB599483F20C7056959832850F90690615CA862303A9AA4842B22B2821FA7A77A6FCE81F6A672691BF7617CB1B8F7A90B3480301B58F5A0637C9C6E2CBBD9BF8735237CCC0155D8A3A1A86E3E5937B1CB0FEED3D9CB7CC8C0961D0D215C11BC96388C007BF6A761FCB270AC0F4E691BE78CA071EE3D693C931CA01E50F653D29D83946796FF00ECD156FC98FF00B8DF9D14587CA5611C72B8058EE5A76E456760C5D871B68F2DC2B0CA963C8A8CDC619808BE71D0E2862B920F2D515CFDE3C0069B1E4A80D950AD8F97BD3BE79086C60019C1149E60955B24AE0E4628D42EC2650A186C3BF3CF149E609158B829B7A0E94AB2347F3090B86F6A6B798CDF302DBFB52B8EEC72C825E563076FBD1E5B48A8599540EA334C6F942809819C1A9FCB014E530A4F1CD0321655B760E4E493D01A4DC26C962CA33C62A48C0918E4210BC5358E14866D8B9ED4D12490B4637E064F6CD224641E5065BA0A6B2897004801C51CB2808D971E9498AE3959958EE0003C0A177263EE8563CD12314D995C9A61648F2A4E4139E680EA2C72279CD8C21038A72C859410C77FB7A51B5655320D83B0CD381F954AB2863E82A4B6AE78B7C78FD963C31F1C9A1D4279A5D175E8C6CFED0B740DBD7B2BAF7C57835D7FC136AEF77EE3C7F0EDF4934F39FD1EBEDF6CF9609E5A98D205F97F898E7BD38E9A845289F0CB7FC1373531FF0033EDA7FE0BDFFF008BA6FF00C3B6F56CE478F6CF1FF60F7FFE2EBEE89395601885ECC69AC1A45511BE48EA0D6FCE4C8F8664FF00826F6B18F97C77627FEDC1FF00F8AA62FF00C13775E6E078E6C3FF00005FFF008AAFBAF7314285B63FB9A5453BE343212C4727B54F3B2A2CF803C51FF04F6D7FC31E1AD4F5A93C67A7DC43A7DBC970D0A5A3AB3855248C96E3A57CA50C9E6A86ED815FB43E2DD3C6A9E0FD7AD5FE759AC278F079EB19AFC5BB6529B97BA923FA56B17CC853F84B2BCD21A0714565D4E78EE3D452B7DD34ABC0A46A96EECD4AE7A9AF42F819308BE215A0CFDE475FD2B80DBD6BB5F836DE4FC44D33DDC8FD2BA25B10B737FF006A0973269518EAAAE4FE62BEEDFD8C6C7EC3FB36F848051FBE59A6FCE56FF0AF80FF006949BCEF10DB419CED833F99AFD1AFD98ACDAD3F67FF000342A3046988F8FF007893FD6B1BF43A1C74B9E9ACE8A460647719A468D892CB855F5A61F2D5B6E312375A36F96ACA5F8CFDDA82506032819249EA0557D434DB4D534F9F4FBF816E6CAE10C72C328CAB83D41AB180CD98836E23AF6A4D84A9DF9CFB51B0A48FCD4FDAA3F663BEF83BAC4BAF68B0B5D783AEA42CA5064D993FC0DFECE7A1AF9F0FCE338CFE19AFDA3D6745B0F1169375A56AF6A979A75D218E5864190C0D7E677ED3BFB33EA3F0375C3A869C925EF842F1BFD1EE00DDF67627FD5BFA7B1ADE32B916382F83BF1835AF827E30835AD1E5CC59DB756AC4F97347DD587F23DABF523E13FC5AD07E3578461D7F429B24E12E2D5B892DE4EEAC3F91F4AFC8075DDD30DFAE2BB9F82FF1A35DF81BE2E8757D265692C9884BBB176FDDCF1E7904763EFDA9F2A655CFD74560A180233DF34AB9570C318C738AE77E1BFC43D0FE2BF846D75FF0FDCACF6F2A8F3A3C8DF0BE39471D8FF3AE8F861BBEEF6E9C5632561A1C8CB825875A147CB961F4E69AD21DF180B42C6776F24104D6632431F98318403DA97276950173F772699B4427E6383DC0A5DBE6286DC383C0A091CD18281410AD9E314D91D954281BBD48A7E1432E0E5BBD479E7E4CE33CD0521599A5462A360F6A63333AED04163FDEEB4F66CE40DC36D35BF7B8C600EC7BD5243B88A4A17F9B773F8E2938DA32C413D568DEBE60D806475DD4E1961BDC616930B0EE0021B263EEBD47D0835E6BE38FD9D3E1D7C443249AAF85ECA3BC9064DD58A7D9A5FA965E09FA8AF4750656DE4FC9D3DE9C59777CA3EB484E0BA9F1DF8C3FE09CDA7DD34937857C592D89C656D753804880FA798B838FC0D78AF8BBF625F8B3E13DCF0E956DAF5B8E8FA5DC076C7AEC6C37E95FA5BFBC44CA67AE79A923936E78076FA7AD5297290E37D8FC70D73C19E25F0C4CD16B1A06A3A73AF5F3ED9D47E645617DA179CF06BF6A2EA38EF11D2EA05B98B1829280E0FE06B87F11FC05F871E2FCFF006B7833479DDBFE5A25BF94FF00F7D2114D4A4C4A9F73F248480F43CD2D7E906B9FB02FC28D5B7B5947ABE8B2374FB35EEF55FA2BA9FE75E7DADFFC1376C5831D1FC77347E897D699FD54D69CCAC4B8BE87C41ED416AFA8F57FF8276F8FAD73FD9BE22D0B511D95A4685BF515C86A5FB0DFC61D398F97A2D9EA2A3BDADFC673F9914277348D27D4F0C5A326BD42FBF65BF8BBA693E6780B54947ADBF9727FE82C6B0EEBE08FC48B1FF5FE04F1021F6D3656FE4B437613A72E871793464D74337C35F1AC24893C1FAEA1F7D3271FFB2D40DE03F16AF07C2BAD0FAE9D37FF001340BD8CCC5A43F4ADC4F87BE3197EE784F5B6FA69B3FF00F1356E0F84BE3FBAFF0053E0AD79FF00EE1B37FF00134296B60F632397DA693F0AEF2DBF67FF008A778A3C9F00EB8D9FEF59B27FE858ADAB1FD92BE326A58D9E09BA833FF3DE6893F9BD54A562E349F53CA029A0AE3B8FCEBE82D27F60CF8BDA83037369A5E9C87BCF7CA48FC1735DAE93FF0004E2F155C91FDA5E30D2ECFD5608DE523F1ACBDA31B8F29F24657FBC334D322A1EA4FE15F77E8DFF0004DBF0EC3B5B58F1AEA379EA9696E9183F89CD7A2F877F61FF00841E1FC3CDA2DDEB522FF16A578EC3FEF94DA29EAC86F4B23F3296E0CCE2382279A43D16352C7F215DF783FE027C49F881E59D17C237EF0BF4B8B98FC98BF166C0AFD49F0CFC31F06F83540D17C29A3E9C40FBF6F668AFFF007D1C9AEA3CD79210070A3A2B1E29E828C7AB3F3FFC13FF0004E9F15EA0D14FE2CF115868F0B7DEB6B1533CBF4C9C283F9D7D11F0FF00F635F861E04659E4D25F5FBD8F9136A7279AB9F64C05FE75EE4AADE6AF99C7AEDED4F58D55B727DDEE0D273B6C6843636F159C2B6F676D1DADBC6BB52386308AA3B00054AABF7864E4F1CD061057780DB8FBD2AAB7DD64C91CE6A5CBA8AC0B1B2B2EE6ED8C0E94EFF56AC43511B0C9255B8E99A45DE76AEDE01CE08ACEE50331E0971F4A17643B0820B0A633B293B5323F950CCAACAA63E48EF4EC171CFB9F00B6D6269540DEC80E78E94CDCDBB7346081D314FC965F90E481934122798C02C7B76EE3F5A63AB7285895CE78A9377EEC165DA453783B76B367BF140082208CE0310EDD3E94E8F76D7561F28E07AD0C4E19DB8038F7A6AB79AC007E7FBB8A063B71655519057A6EA55625D4E382396A6C6EC14A7DE65FE234ABB963500E475DBDE810A540CED5FA53E3439C85C499E714B1E77101810E3807B579B7ED01F1EB44FD9FFC22FA85E4915CF882E14AE9FA66EF9E46FEFB7A28EF4886EC53FDA37F68CD1FF67DF0BB49215BCF12DE237D834F07241ED23FA28EBEF5F961E28F146ABE38F11DF78835EBB92FB53BC95A592590E7AF619E807A54FE3CF1EEB5F13BC5179E23F115DB5EDFDC9CFCC7E58D73C228F415845BFBDC938C003AD6D08756118B7AB1C5BA139DD9C05EA4D7DC9FB1C7EC946DBECBE3CF1BD9AB39FDEE97A4CEB93EA259011C7B0AA1FB1FFEC88B75F61F1EF8EACD826449A669332FDEF496407B7A0AFB8E490F24A80ABC0DA3000EC2A9C8DFE118F2798CCD80A4F514E441BD448083D88E94DC23A8662013D0D2A26DE1E4F94F46AC9B2099668D59D5CEE1D8D49183B0302001FC355A3C4630E37063F7F15244847284327AD66D0CF8ABFE0A836424F0DF80AEC0FBB7373167EA8A7FA5799FECF77425F86B1AFF00CF3BA917F3C1AF66FF008299DB79DF08FC2F74067C9D636E7D3746D5E05FB34DE799E01D4623CB25D6EFC0AD6904DA25B3C6FC79279DE30D65C1FBD7727FE8558B1AD68F88A4FB46BFA8C87F8AE5CFFE3C6A905C574AF7558C5BB8377A8D7A9A90D328E869D87007753FA75A676A6B36E159BF7B61F2EB766C784FC3B75E34F14695A0D94D1C175A85CC76C924992AA5CE3271E95F4FCDFF0004DDF1842CC87C63A112A71C24D9FF00D06BC4BF661B33A87ED09E08B718CFF6823F3FECFCDFD2BF5AA6DC189278F6A56E5D4D2E92D0FCFC5FF826FF008C1DB27C65A205FF00AE537F85594FF826EF8AC2F3E35D17FEFC4BFE15F791F94850C79A79CC5924E076CD4CA6E5A19F5B9F05B7FC1373C5BDBC69A21FAC337F8535FF00E09B9E30DB85F196844FBC730FFD96BEF4919957E56CB7E34A0F04B3004F4A87765248F826DBFE09B3E286987DBBC6DA34710EBE4412BB7E4715F587C0EF829A2FC08F091D174976BA9E6712DE5FCAA15E77EDC7651D87B9AF426DAB20DCFC01DA9C8A870CA308460D35A229C87604C36E4F1CE7D69597E65509961EB4DDAA9B5D5CE68FE2F9C92F9E36D4DC81DB8B1C3A80D9E298F0EF9090C100ED9A718D5836FDCAC3A53114632A3E6ED9A108151881FBCE57AD2B47FBC560DB9C8A5DCBB482C0B75E285327008009EF4C771A77118760A7381C52A463E65C64766ED4EF9D5DB79181DEA3977153B9F9278AA15C77CD1C81155767B52A9C37CCBC9EFDA9AC7C98CFCDF31E46293E79954A9C7D7BD26CB42AAEDDC37609E8281098D5599F27A85A46B9556C18F2C0638A2350EAAEEFB703818A40C72BA8DCE233C8EF4A77B105B007FB239A424C9F2B29008E299B479DC6FD9EB5422658D7995573818C138A491170005197EEADD2A3018925109CF1CD19F2D954460E3AE0D4DC5763FEC9FEF7FDF5451B9BFE799FCE8A61CCC58D8C6C7E604B742DDA918CBE6E78C01C9A56D8C0190E31DA9AC10C6479BB49E76D002AEF770DB588C63DA9CB978C044E3FDAA64726E2AB92063EF1A46748C654973DF9A18D5879076A8465520E7148ACF386C9C01DC1A459103009136EC7028562E9CA7944F4E68188B22AA852A4906936EEC06254E73533111919552D8ED512A9B824BA630314102CB1AAB00A401DC8EF4EC22AEC6E4F5CD46CC2D5F2304631EB4EDC5A30C3041ED400A09F308540474527BD39BF70CCB9C3350A1DB215400BC8CD316331E1D8EE3FDD34156439576E4C9F8734FDA1A4194EDFC54D51BFE6D9838F5A6EE678C93DBB522588E082DB0A2AD1CF057B75342C819557660FD29EBB9D9948D8A691711AD1B34818B633DBB53558EEC91B8A9C6052B29E18B12A0E314051248542EC23A1AA018A92AB060D919E571D2A5F31DA42CA4175E2916490AB05E39EA69CA5958AEC0370EB412C695F9642577934B1BBEDC3A609E001D40A468480C11C85C649A3683F38725FF84D2296C49180F0CB06C6DAE8CA77FB8C57E2E6BD6674AF13EB76478FB3DFDC438FF76461FD2BF6AADD646750ECD827A76AFC7BF8E7A5FF0062FC70F1DD985DA13579D80F666DDFFB356D4F63291C7834EC5363A7D16D4C478E945377629DBB35125666CB61A45755F0AE4F2FC7DA39FF00A6E057295BBE03B8FB2F8CB47909C28B84FE756CCE32B3B9A3F1FAE0DC78F2740788E355FEB5FA95F076CBFB37E147842DB18F2F4AB71FF90C1FEB5F94BF15243AA7C54BA8633BBCCBA8E21FA0AFD77F0F598D3FC3BA55993B443690C7C7B2014E51B2B9D2A5CC8D40A92749781D88A6F1E610572BFDEA67254EE184CF0D527CDB559F079C62B02066E65DFB57E4F5A72285C10D927AE691964F99890ABE949B4CC41760100E08348B4EE3F6E7EF0C8F51599AF683A7F8B345BCD1F58B48EF34DBB431491BAE783DFF000ABFE57EEF01F2DDB9A798D9B68623681EB549D84D1F96FF00B4BFECD7A9FC03F1035D5B17BEF095E484DADE633E5679F2DCF63EF5E2DB4328603295FB39E2AF0B695E3AF0EDE787F5FB44BDD32ED0A346E3A7A303D88F5AFCBEFDA33F673D5FF67FF139F964BDF0BDDB93677BB7803FB8FE8C2B78BB99D999BF017E3A6B5F017C5D1EA5619BAD26E182DF69ECC764C99EDE8DE86BF52BC0FE3DD17E28F846C7C4DE1FB95B9B0BA5FB9FC7138EA8E3B11CFD6BF1B7A8C8F9811DFB57A8FECFDF1F75AF80BE2D8AEE0692EF419D82DF69C58ED917FBC3B061D8D4C92DCD3747EAD0EF9EFDA98BB594673807A564F833C69A2FC4AF0DDAF883C3D7897B6372818E0FCD1B77461D88AD9CE71C65475AC2E02C6A4C8C48F908FC694B16FBB181CD26D019886249E94E5E70AADDB9CD1B887C9F265B682D9E286666C2AAF3DF0299BBCB5DBBC17CF19A915A459146739EAD8ED4B601BFC24ED3B98E29BE5B2B63854FD6A4401597938232D9A5DA1B25F918E3D69DEE2218D836E40A010386228447DA039F949A73319102A83B7A6695BF74C11417007714F62B983CC2D90A0003D45313E5538C7279CD2FA82D8EE734BB4B2A86C2E7A546C2D45FBA549627E878A7E77363EE0EBD3934DDA17E5E0AFF007A9796CEE66F45CD26EE311959FAB6C39FC294E78C10DDB38A3A01185CF3934ADBA36DAB8FA552908465650401C9E841A5F9D405C953D49EB42E159BA904D0BF2EECE4FA532AE46B29652771E78E4522C84E7E5076D2B65994023038C523775DA02FAD315C779D22E7A01DA91AE2658F7076CFD69BB42ED07A8A4D859B27E51DB069365224FB54DF2A8918F726924B89B7001DB1D6A35C963CFCB4AACC88707DA8B8730FF3E6652DBD8FA5219A6C16DC49FAD0B8560C33480B3649E79A1137639A49768EFEB9A4DC5B20E32067A522FC8C4F249ED9A4DA15495FBC4D55C2E39403F31C05ED81D69C4319065B6A81CE0F4A0B2B30DFF75471F5A546FBCC5372E295C9D46F961D43E772F4EBCD09F2B2A28E3BFAD39A30BB46DC0CE460D2A82D3367E5ED45C361BB42EE19273C7E14491C66455E46DF7E2A418605483B41E4D264B6361F93BEEEB4B51DC8FCD4F30820938FBDDA9DF3AC20FCB8FAF34AB1AC60BA8F6DB4796A8DB9BE6F51E94A40333F32ECDC4E3D68C37CEC49FA54817E6053E51D6A393E6CB77CD1B87517EF600DC07BD2B317936E7E6F5CF14D6DCDF31E0638A718C347D3140C6BC9E5A153FA5201B777CBBB70E33D69EBB490A064E293CB1E606CF207414EEC9188CAABB5D48FC6A40E77040A1411C1151ED0CBB88DC49A70257AB05F6A0771C3712A0E090718A04AF9385E49C629981E66E04EC1DF14858AE3631CE72698803750031CF5069C433488538E33CD064F3158C7DBF8A82C7CA195E7D690C4DCD1F60FB8D48ABB6404261F3CF7A64719D851F05B3904715E7BF1D3E3CF87FE01F83DF55D4E44B8D5A646163A606F9E77EC4FA2FA9A443761DF1EBE3C683FB3EF845B53D44FDAF55B95234FD3548DD2BE3EF37A28EF5F955F10BE236BBF15BC5979E23F115E35D5EDC39210FDC893F851476029BF117E23EBFF0016FC5975E21F125DC9737531CC71B37CB0AF6451D80AE74FCB924743DAB6843AB08C6FAB15A4D9F33671D857DA1FB1CFEC8EDAC35A78F7C6D640D8290FA669927FCB63DA493FD91D8565FEC7BFB24378BA6B5F1CF8DAD7CBD0E260F61A6CE841BB6CE43B03FC031F8E2BEFFE16341122A43180AAB1A80A00E9802AE523A3E143988C6D1C201854F4F6A664472038C76C1A52C1B86187EA28F35378F3548F7AC5CAC65B8D214670C1D7B8C50B2453633F3274E074A5F30C7CAA6771A4D8CAACCB845CE40C5420E811A2DBEF3B86D3C0CF6A939590EE23675F96A25DACAC7E5033F78D3E3DD137CB89377F1532A27CD1FF000511D37EDDFB3CB4EA377D9753B793207A923FAD7C95FB32DE04D07C41113F7156403F035F717EDA5A61D53F66BF16291B9A048E7FFBE5C1AFCFCFD9D6ECC72F88E00786D39987E19AE8A6B440E3757383BCB8F3EFAE5FFBD2B1FD698DD2AAAB12CE4F7627F5A9972CB44B7318C7A8ECD2EDE3351F3BAA61DAAA5A21DC02E4535E3A9291AB05268AB9EC9FB18E966FBF691F0A9037792D34A7E822635FA8F216560C795EE2BF383F601B1177F1EC4E46459E9B73267D3202FF00ECD5FA3EEC39D84B1FAD2BB7B94AC4726E6C608553EBD68562CA59F2D838A46512619B09838C6694AFEF1B0F851D6A6C0C5456DE7270A79E69927CCAC30D81DC52950CB912360F7342AEC6D88FD4734C1208F1243C36D19E7239A4568E1DC06641DB8A561E5488CCF9FE54EE7616936827B8A4C0452A1414058E7269CC1AE3247CA31C53242DE622464629C774318DD9214F414241B82E46FDECBD382696457DA1F2001D877A8DA456CB34784ED4BE515933E6E63C722A839446976FCC11727D69DB7736E662A7B0A74CD1CDE580C0FD7BD2280CC4C8841EDB4D310DDABE5B798C4367A53E16DCCC432B0F7A6FC9F3326E9083F74D0CBE73301C6D1D290B40666663F2F7E29841590AE41DA38F6A7C8A3CB085D448299B6357E5BE7C73482E2A49C02082FD08A492478D846578EEC074A07F71B0B9E8475A9140C92D26D1D07A9AA108AA24E70CC07439A5DAF2478D8431EF9A6A89D95816E3A0A6B4C3E55087703B4B0348771FB9F61552370F43D6936985838401987218D24623919B6A73D339A44CE0B6C5DC380734C63BEDCDE894533F79FF003C968A02C492110A26143E7A934E0A00F3767DE18C5302AE0A805988C60D3A4B711ECE5968E8090330662047C11D2988891E7E5C3678E6A450CDBB238FE1F7A47876A85D8431E739A81E886B3798C76390D8E4E38A2450D8F309DD8E3142C2BE490776FEF48A63DBF7981C6178A76240C68583FCCD814EF31D81014723239A459187EEF6EE5C673409235DA707914C61110D1B0D83751B4B3617803BD2950DC2A1F739A248CAA800153EC68B8C4456690EF7C2E39C50D88B2C518AF6A53097250292DD7349E53807049C7F09A41A0A9F31E5F6EEF5A63B0661123FE3524B1BC8AA02AFBD08A70404031D4D090B40CA0D809C91DD6846CAB90707D5A80047F7537E4E4E28932DD4617D280B8D5CA0C677E7903B52BB2EE04EE0DD29AD0AAAFDE2157A9A76D538600B6074A2DD42E0CC036D048045019A1018BE40E8297CC3B4B0427D334898900053E6F4340DEC2ABEDFBF264376C52ED0B854E7B9A458C498565DA169C0ECCB2B0C0E28089240C3CC4019987AE2BF2A7F6C5D2CE93FB4A78C476B9786E471FDE8C7F857EAAC4C1D8047E075CD7E717FC142B453A7FC79B4BC5E16FB4B89FA752A4AD6D4DD84CF9B23E00A901A6AC646334EDB44A5AE873F2BB835229C2D3829A76051CCBA9A6B6B116E26AE68B37D9F58B39B3F72643FA8AAFB47A5319BCBE47506A5CBB0592DCE9B46B53E22F8E7A641F7BED3AB4407FDF407F4AFD8111885F6775F947E02BF28BF665D28EBDFB43783212371FB62CAC5BBEDF9ABF5866F9A47200C9E68E66D6A68A49E8880B0DC04B9DD9E3D29FB630E771CAF5A02A7381B9FD29EBB64C6E18E31B6B262647C3EE6DC7CB1D85298E36C6090BD680B9CA79642FA8A56DACBF2AEDC70734C06F99E5B3385CAFBD290B22AB63E6CF4A18AB2FCA49DA39A5DCCDB55F0981D691A27707DC32BD3278C76AC7F17782F47F885E1BBBF0EF882D56FB4FB842A430C143FDE53D88AD9FBBB1B765BBE6932648D559B69EA69EDB10D1F94DFB427ECFBABFC01F163DACAB25DE8374C5ACAFF001F295CFDD6F4615E59B46DE3953D4D7EC878E3C13A27C4AF0CDDF87FC416C975657230A58728DD994F622BF2F3E3EFC08D67E02F8B64B2BC5373A25C316B2D4147CB22FF0074FA30F4A84DDECC57B6E5AFD9DFF686D5FE01F89BCC4CDE7876F1D56F6C589C119FBEBE8C07435FA7DE17F14E97E3CD02D75CD0AED2F34DBA40F1B21E7A6483E847715F8D0EA187F794F4C57B27ECCFFB4A6A3F017C48B6D7C65BEF0A5E362E6D41C988FF00CF4407B8AD953EA66E5AE87EA46E2B920020FE748CC15863033D73595E1BF11E97E30D0AD75BD16E92FF004FBA40F1CA878E7B7D47715A6B95E410477EF8A2D62C779CABB8315DC7A6054864448C0F98922A3DBD0B6D248ED4E563B7E6C6EF6EA2B318F562CAC19C124600C74AF3FF008B9F1D3C1DF0374A8EEBC4DA988EEA604C1610AEE9E51EA17B0F735E831EF2E0B01DABF283F6DABCD46FBF693F11C5AA49279719892DF772121DA318FD68BBE843BF43E92BBFF829A7846194ADBF85755993B334F1A67DF15027FC14E3C2C5CF99E15D4D01EEB7119AC7F847FB33FECF1E30F0DD849278C5B51D526895A685EF1607572395DBC7435EA327FC13E7E11DD41BE183521191F2BC7765B3EFD08AA517D4D3E18EA8DEF811FB5E681F1DFC572E81A6E8BA8D94E96ED7266B87529B476E2BDDE590464B6490A78AF28F81FF00B377843E015D6A573A07DB27BAD4116332DEB2B322039DAA4763C7E55EB291EF6DB9E09EADE94E76E84A7A1E7FF1B7E34E85F02FC12DE20D6034F2CD2795696311C493BF7C67A01DEB89FD9DBF6BDF0FFED07AB5D68D0E9D2E87ABC4A658A1966122CCA3AED38E08EB8AF8DBF6C9F8973FC72F8E11787346669F4FD3241A6D8C6A4ED9662D877FC5B033E82BCFEDF4DF10FECA7F1EB4F4D41C2DFE973452BB44DF2491B7519EE082454C23CDA99295DEE7EBF9CEEC1CE4753EB5C57C6AF8AD6BF057E1DDE78AAF2C24D4A2B79238FECF0B0566DC719C9E95D5F87F56B6F12687A76B365209ECEFE049E1917A32B0C835E19FB779DBFB39EAC18601BCB61FF8FD292EC6AEED687954BFF053FD0A36DA9E05BD3F5D4101FF00D06A7B5FF829E78698FF00A4782B518D7B98EF91B1FF008ED7807EC73F03FC31F1B3C6FADD8F8996E1ED6CECBCF896DA4D996DE1724FE35F5A37EC17F09258FCB1A7EA2A4FFCB437678F7E94D45BDCA564BDE37FE17FEDB3F0CFE296AD0E9515E5CE87A8CEDB618B5140AB231FE10E0E335EF6AAA3CC67E4052C36F3DBA57E507ED5DFB3EE9DFB3EF8D6C23D06FA6B8B2BC8FCE8D2720C90B03D091D47BD7E8F7ECDBE28BAF1A7C07F086AF7AE64BE7B2F2E576E4B32332673F4515526AF644295DD91E2D67FB7E785EFBC5D1E811F86F5337525E7D8F7B4881436FD99FA57D21E35F16D9780FC33ABF883508DE5B2D3AD5AEA48E11972AA32719E335F91FB7ECBF1F64651C2F88CF1F4B935FA83FB47299BE07F8E805E7FB0E7FF00D06B18CB766AEEA27895C7FC149BE1E2729E1FD69F3ED18FEB51C9FF00052AF002A82BE1DD60F1D37C7FE35F247ECC5F0B743F8BDF14B4ED03C413B5AE95242EEED1C8236C85E3935F6E45FF0004FBF8376B27EF6FAF264EB86BF51FD6AA379330D6CEE7203FE0A5DE082DFF0022BEACDF49A31FD6BEA6F02F8B6D3C7DE0DD2BC496F03D8D95F41F69D970C37469D72C4715E316BFB09FC1059923F2657627863A864FF3AD0FDACAD64F01FECBFAC695E1B592DADED618ED0042772C3B806C9FA56F349474279B4393F891FF00050AF87DE0AD6A7D3B4BB0BCF13340C51AE2D58450961C1DACDD47BD70327FC150347DDFBBF015CE07F7B52507FF0040AF953E0368DF0DF5CF174917C4BD62EB4BD1C4598A4B64277C99FE22012062BED3F04FECD5FB2F78CE151A2EBBFDAB249C049B53F2DF3FEE920D64A2DEACD6F68DE466F84FFE0A45A2F89B5AB2B03E08BD8A4BA9961568EF15F0588038DA2BEC7F33F7719DA5778FBBF85785784FF624F861E09F15587882C34FBE6B9B3944D146F705E2C8E4165239FC2BD47E2778DAD7E1CF8075EF155DFF00A9D3ED9E558CFF001301F22FE24815ACAD6D0952D34394F8D5FB4B7823E05D9C6BAEDE9B8D525198F4DB3C3CE47AB0E8A3DCD7CE575FF0544D1966616BE05BB9573C34BA822E7D380B5F2EF80FC23E23FDAAFE34186E6F0BDDEA32B5C5DDDB6584318E4E07B0E057DC9A7FFC13AFE16416F14530D52EA650034BF6ADBB8F7E00358D9EECB8A77BC8F335FF0082A35AEFE7E1FB05279DBA90CFFE835EC3FB3F7ED7C3F687D5B56D2749F097F675E5859B5E7FA45E6F5900206DCA8E339AA737FC137BE172FDF8B5850DC8C5D13FFB2D777F047F655F097C05D6AFF56F0DFDB9EEAE60F25FED326E017703C71ED5AC6692D8CE5257D0F06D63FE0A19A9783FC6B79E1DF127C3E8F4C96D2E0C329FB5BEE55CFDE008E78E6BEC2F0DF8834DF18786F4ED7F49B95B9D3AFA1596375F71D0FA106BE62FDBABF66D4F889E1597C6BA15B0FEDFD323CDCC683E6B88475181DC738F6AF2DFF827C7ED170786EEA7F873E24B9F26CEEE5F334E9A76C2C52FF121CF407AFD47BD44ADBB2949BD2C7DD7E2AF14699E0BF0EEA1AF6B176967A65944659666E800EC3D49ED5F357C3FFDBA2DFE267C43B4F0B787BC137DA83DE4FE54729B803E5FEFB281F281D6BC57F6EAFDA265F887E225F871E18769F46B19C0BA920248BBB807181EA14FEB5F46FEC4FF00B3937C12F0747AEEB96C8BE2BD5977052017B584F4407FBC7A9FC0567CDAE855ADADCC5F8CBFB6F697F05FC75A8F856FBC2D797D7B62CA2530DCAAC79201E0919EF5EDFF0008FE27693F19FC0363E26D1DFCB8A4CA4D6ACC0BC320EA871EDCD7E77FEDDDA605FDA835A8CAEE33C16E48F56280569FEC3DF14F50F83BF191FC11AF335B69DAC3FD9A48E538586720147FD40CFBD14FDE7A92FDDD6E7E96A019DBD59BD057CF7FB437ED81A1FC03F125A68474C975AD45E2F3678E29447E503D01247535ED3F127C69A77C23F04EB3E25D60ECB7B0859D232DCCB274445F524E2BF1AFC75E29D5BE2878C35AF16EA5E64B25E4E5DE4C92B193F7501F40381F4AB9479476B9FAA5FB37FED0D67FB46693AD5D5A68B368ADA5BA238967126FDC0918C01E86BD65BEE919C81D5ABE39FF008263C5B7C19E3A90704DDDBAFF00E3AD5F6415DD918C0F7A6B44851D86AF50858E0FB52C73084B00BB81EE6942FF001F1C5207F9777CA49FE1A8DCB164D8854FDD1FDDF5A45C6D0E8C4F6DBE94AA597EF6D77078F6AF3DF8DDF1CBC3DF01FC2736ABACBF9D773291656287E79DFD31D87BD17B10DDB513E3A7C72D07E0378364D5B559126D464F96C74F53F3CEFF004FEEFBD7E54FC48F88DAF7C5EF185DF897C477267B9B86252219D90A76551D80152FC4AF899AEFC62F17DCF887C4170CF2C8C7C9B7E76409D9547A5738E403B9BE5E7A56908DF564C7DE77647F7475F97B76AFACFF0063FF00D921BC7B3DBF8D7C696B245E1B85BCCB3B1718378C0F05BBECCFE7557F644FD9265F895790F8BFC5D6E61F0AC0F982D64055AF5875C0FEE0EE4F5AFD108218A0862B68234B5B689447145180AA8A06028C7615A39763ABE101B628A2892358E08C6D8D106151718000EC3DA9CE76A028D8FF00668471BCAB6E247031DFDE9E5D9777DDC562D99EE2796564DC007247DECD248C76ABBE323B537C8CAEE6703BF14FDCA7049EDD2A6D71F90C2DB9958FC801E314071212C1F728E0AD35555E4190766734B232C2B98D50927271D6AD225AD2E22809B9A34F949EE6A58E4DA4070777B74A612CDF78ECC73814F491A4652A38EF432A3B1C0FED09A51D63E05F8DAD48CB3E99330FF80A93FD2BF2DBE05DE0B5D735004E3CCD3E65FF00C7335FAE9E29B21AC784F5CB1DB91716334583EA508AFC66F0BDF49A0EA774FF00C4AB24047D41535A4256761735958AAA3754E176AE6991C657E9529F4A1BBB216BB119639A910E4D34A1A916AE4D589D13B0EA6BD2D3198735CE3D0FAEBFE09BDA3F99E33F19EAA5722DEC23B753E85DF3FC96BEF0690AA918E18E3757C8DFF04E3D1CC1E03F176ABB33F69BE8E107D76293FF00B357D71CCA06F8FF005A6AF6D4D236B682F97B95836C7EF914204650A397FAD0547DE65D8BFCE9648C1CE004E383484F723914A0DAB924FAF4A765971B80C1EAD4CFDE7978072451B46E196653DF3D298F517647F316563E99A4E195495E33EB4E8D5570BB8B37AD2B280CBF2703AF34C611C91B374195A6AAF98CC7E7183C0ED4A17CE525171CF5A31FBB2ADB8BE7A034AE20F3832B2B21C67B537CE01B6942A868755D98CB26D3D73D6A48CA48C32CC5B1F7690EE224A176AAA820F4C8A190B480B8C8E98534A11882095DAB4D58FD4065CF1B4F2698590EF24EEDA99519E687543C819DBE87AD35A12AC5563C0EBCB50D1C7D3247726825A18CC9B77F964BE6A56F9B63EC5DC7A7B537CC48F785C92DEDC50235DDF33E1A98EC39BCC562CC81C2F4C530FEF97A08C6738EF4A9184505E56C6689188C94DB83D73485613CB32293CA953EBC1A76E68CB6E8D483C8A63042BF296C8EB4FCC6CE36FCC71F7BB53103A2300C4053E8A68F91B242142BD314C5D833BF6820F057934EF2D508C392C4F7A96171BF6C3FED7FDF3453F7BFFB3452B3015A4924640AC37F718A1E450A4BC84BFA014E8DDD955B68DDD39A6326DDCDBB0C78DA69DDB1DC6421782F2B602F1914B14823382DE671DBB52893ED0486C7CA31C52AED8CB3AAF38C61A9A5600653F7849B78E947FAC45D8D9DBD78A17E660C00C1EBCD0F1F5C48140E40A63D056930DB4CBC9ED8A66E45C0EA3D4D29997CC51F79B1E948B729B4AE3E65A91E80EBB1B20B1247E14AABB55773E569508392C87E6E94DDABB30C36F340AC1219D54346DBBD853D4855DCE486C7228550589078F434C65DD236E520E38DA6992321FBC72488FAF35232BF063F9B77BF6A7B48635552AB9C70298B2EC2AAC996F6A42101758FFD5E307EF53994942CE086EA294B00583330FF0066915BCEDC149C0A006EDDD1EFDDD7A8A7B48AA7E54DC7148C07CDB073FA53BCC755520292DD4D318C2CBF2821CB1F434EF2CB49F37DC038E79A40AFC171B8F6DA6858FE63E6EEC8EB400E5F336BED195F5A6EE67751E5F1DF349B7048F99508E295648FE55E7DDA9151258FCB573C1C8EC2BE23FF008295E8456EBC11AEAAFCAC935A337BF0C07F3AFB7E3F911B0F927A66BE7EFDBB7C1B278BBE005DDDDB26FB9D16E52F80032767DD7FD0E7F0ACDDDEC2969AB3F341642CA2A4DD50C6DBA356C6334E06B78C7B994A5D87E4D2EEA8CC946ECD53489F796A3F351C9CFD69DBAA0924DB927A2D1A2124E47D0BFB08693FDA9FB42584E5772D8DA4F704FA7CBB47EA6BF4BA68CAFCE793DABE18FF008270F84659750F1778A5E3C42B1258C4C7B9277363F002BEE4DE76E3396EB59DDB474461CA3194423715009EE0D1958D55C64B7BD3951F702F80BEB4BB8BC8363065A962EA33CD7563E68F97DA9DB8270A305BAEEA0B198AAEE190704D31A3DDCE773A9EF4CBD0937A038070D8F4E29BB980071BFD4D3BEEAEF206E23814D32348A81948CF5C5046C2312B28CC7BB3D29150C8CECC36718C53C46B9251D98FA5246A9862E5B777148B5A918215371561E86B0BC71E07D13E25786AEBC3DE23B4175673AF0C47CD1B76653D4115D1A47F2E369F519A475662A5C7BF4A3621C6FA1F98FF00153F635F883E04F134D6DA069177E29D1246CC1776681980F465EA08AE3FFE197FE2DDF2B22780F550ADDE48D53F99AFD67492459015384C7638A679B295DCE4918F5A7CD2EE1CA92B23E29FD917C0FF001BBE0FF88134CD5FC3175FF0885E362E23B9953F704FF1AF39CFA81D6BED9D9838438CF5E28405B1CF6E45019555B2DB003DC75A1C9B1462FA88B1E3E75C9EC08A53DF2ABB8D26ECA82B90B9A5217875DCEDD3EB52696136ED5667DC00E8326BC57F686FD94BC3BFB434315F4F3368DE23B78FCA8AFA25DC1D7B075EE2BDA900DDB72739CFCC6BE0EF8DFF00B697C40F863F1AFC45A2D84BA7DEE8D65388A2B5B8878036827E61CE735AC17515D4753CFF00C4BFF04F1F8A3A1CCD26973586AA89F71A2B9F298FE0F8FE75CBDD68BFB41FECF91ADFB0F10E8B6711F9A68A632C1FF02DA48C7D6BD7EC7FE0A71AA471AA6A3E0DB091C0E5ADAF244CFE054D607C52FF0082875E78EBC17A968161E13B7B05BF85ADE49A6BA32E1586090368E69CA2E5B994A4E7A58F76FD8FBF6B6BEF8E1A9CBE13F14C16E35F8603341796CBB05C851F3061D0301E9D6BD23F6B4F8BDFF0A63E0D6AF790C8B16B57CBF62B2563F3077FBCC07FB2B935F20FFC138FE1CEADE22F8B9278B2381A1D274981FCC95B8566618C7BF19CD72DFB6F7C5F7F8BFF001966D274A9CDDE8FA3C86CADBCB3912C99C3BFBF3C0FA54461CBAC8DE10D35384FD9BFC4DE1BF0D7C5DD33C43E2FBF6B5B2B293ED1E6792652D26720903F3AF4DFDB87E21FC37F8A9A9683ADF833589352D5555A2BC0D0346047C15EBEE4D7AE7C35FF00826CE83AB782748D43C47E23D46D757BB856796DED224291EEE429CE7900D6E7883FE0993E0F6D3EE8E9FE2CD516E963263F3E18CA6EC70081838CD26DED14613493B9B7FF0004F9F8B83C61F0B65F095DDC6FD4B4172D0AB1E4DBB1CE07D189FCEB73F6F6CC9FB386AF8EA2F6D87FE3F5F08FC09F1C5F7ECF3F1E6D65BB9185BD9DE358DFC68701E3DDB5B8FD7F0AFBC7F6E2F22EFF00663D46EEDA559E0B89ED668E4539565DD907F1C8AAA764117296C7E7BFC19F8D7E2AF813AE5E6A7E168AD6E26BA83C9992F20328DB907200231CD7AD49FF00051AF89D259BC0B69A2C33B0204E96AD953EC0B11FA56A7FC13AF478356F8B9AD3DDDA43796B1E8D2168EE230E325D57A107DEBE81FDA23F633F0DFC50D0EF753F0A69D6DA0F89A043222DBA88E2B8C0CED651D09F5F5AABB92B153BEECF8DFC27F0E7E287ED61E2CFED4BAFB66ACAD26C9B56BC388211FDD07A0E3F840AFD49F85FE09B4F871E03D17C2D6659E2D36D4445DFAB31CB31FC4935F97FFB39FC76F10FECDBF10A4D27598EE468AF3795A8E9AC48319071BD47AFF31C57EA87867C41A7789BC3D69AE68F7697DA6DE45E6C33467820FAFA1CF047A8AC63077D4575147E435FA95F8F175918C7889BFF004A0D7EA2FC7C8DA6F82FE385C673A1DC63FEF8AFCC5D51449FB42DEAF1B4F889BE6CF1FEBEBF507E357EF3E15F8C100F95F48B84CFFC00D5A51D89E6934D9F91FE03F087897C73AF5A68FE17B796E75699498D21902370B93C923B0AF545FD92BE3BCCE01D06F476CB5FC63FF6A5723FB3F7C52D3BE107C4CD37C4FAAD9DC5F41688D88ADDC296CA91D4FD6BEAEBCFF829C7876361F67F025E6D3D59EF9467F25A694624DE53D11E5FF0DBF649F8D9A378DB42BFBFD2A54B4B7BC8A5949D410E1430278DE7B57E8D789F4DB5D5ED6F34DD46D23D434FBA1E54F04C32194F15F11CBFF0535D3E497741E049CBFA49A871FA257D11E32F8B97F1FECF53FC41D26DE1B3D4058ADF456B72DBE3E71C67BF5A1DDBB1B46972EE783FC44FF827069DABEA57577E13F11B699033165B2BD8CBAA67F8430E71F5AF1FD7BFE09F3F153C3ACD71A6CB61ABECF997ECB79B1CFD03E39FC6BB7D33FE0A79ADADBAC7A9781749B9940C3CD05DCB113EF8C11562EFFE0A6D74F0BFD93C09670CF8F95DEFDD80FC0253D64ACC8A93FB2798FC24FDA3BE24FECF3E3A8F49F125CDFCDA5C5208AEF47D50B1DAB9C1299E87D08E0D7D91FB675C8F167ECA1A96AFA0969F4FBC5B7BB2CBD7C92E093F877FA57E7BEADE21F197ED4BF163CFFECF4BBD67509163582C63C471A741F80193935FAD3E16F86F63A7FC23D33C09A885BAB48B4D4B2B80DC86F970C467DF3594A3CAEC99505CAAE7C11FF04CDD534CB7F1FF00896D6E648D352B9D382DAEE2016C382C07BE2BEBAFDA7ADFC7773F08EE2DFE1EC375FF0009035CC40B58B059443D5F693FD39AFCF8F8F1F00FC63FB2BFC495D534796E534B12F9D61AADB646D19CEC723A11D31DEBD67C07FF000536D7B4AB38E0F12F86EDB5774500DC5B4C6166C77230467E95BC6378EA372E77A1E65ACF803F69089659EEED7C60C880B3BB4CECA0752786AEFBF60EF895E2AD4BE3C5A695A9EBDA95F5ACD6B7025B7BA999802A848E0F7C8AE9BC59FF0005388753F0FEA167A47825AD6F2E617892E2E2F372C648C6EC05E4FE35E75FF04EDB69350FDA152EE53FF1EF617129CF72571FFB356718C62F566728A5BEE7EA1A2C7F3C72461E3906D74619047A57E5DFEDD3F0063F837F10AD7C41A1A7D9F42D719A58D223B44138396418EDFC43F1AFD403275047E35F197FC14F821F87FE0CDCDFBD3A93ED507F84446A651E6657372A3C77FE09FDF06AD7E237C4ABAF126B118B9D3F41513F95273E64EDF709F500E4D7E97C8E5E40CCCBC1E003C57C7BFF0004CFD32283E1C78AEF378F3E5BD8E3619C6142935F5F96511AE137FCDF7AAD5AD744C537AB3F323FE0A0A8F07ED21733E70DF63B66FC76FF00F5AAFF00ED9FF09E7F0BDBF803E27E94863B7D634CB46B968863CBB8589486FC47F2A87FE0A109E67ED0137FB5616FFA035F71CDE03D2BE297ECE7E1EF0A6AA15A0BBD0AD5239B1931BF92A4383EA0D28D93D0850737A9F9F9FB407ED45ADFED0BE18F04785A08E68CDAC091DDC4A726EEE78556E3A8C73F535DCFED21F04EC7E09FECABE07B558026BFA86A3E76A938EA5FCB2421FF007781F81AF41FD99FF625D63E1AFC4EBDF1078ACD9DD58E9BB8E9BB18379EE785723B6073F5ADDFF82943B37C1BF0C33300DFDA87E51D07EED854C94A4F53697B8B94C9FF008264FF00C93FF1AF1FF2FD073FF006AFB119B7704F1EB5F1FF00FC133D427C39F19B1C64DFC2303AFDC6AFAFF76DE77617DE8B24AC2A69D84DA164519CAFB546103372463391EB52F9798F72B633DF1C546644E41424AFF101548D01B7C70C92C11ACD385252166C066EC09AFCF7F8E7FB2F7C7AF8B3E35BDD7F57B1B5BB52C56DADADEF53645183F2AAAE6BF4276B67FD5EE2DDC1A67964B9DE71FD29595EE1CAAF767E5749FB1D7C63B77119F055D3851F7A396261FF00A157B77ECF7FB06EA736B10EB9F12A0FB0D95B9F322D1D5D5E49987F7F1C05F51D6BEE989A645CF41DF9A56CB7CCDB891D80ABBDF62EEA3B221B7B78AC6DA382D228EDED215091DBC2004451D0014F656E1963FA0CD393BE5C63B0C52479560036F63FA545C8DC540CAC434443FAE685902291B32D48E18333B86FC0D2E2365EA576F3500346E18528369F5A91B9607671D29ADB55725B711D00A63304519C907B7A556C21F133F21805507BD37798FE63B08CD0DB650A3240EF4E48FE6D98040E7268023695943951B813DE9E198C79185627A523166DC3236D29942B0451B811D69B2A2594859982381B586D3B4F5CF15F8D1F12BC3EDE14F8A3E2ED248C0B5D52E107FBBBC91FA115FB2D6ECBBD0EDE95F98BFB737835FC27FB42EA77C176DB6B70C77B19C71B80DAC3F3159EB744BB2DCF085FBB4B8EF518EB4F5AE951EACC5CBA2168A466DB476AAD19366B51C4E6A291B6AE7DF14E6E9935108E4BC9A1B4897CC9A77088A3A924E00FD6A5B515A1518B933F4DFF00619D0DB45FD9EF4D976EC7BEBB9AE391D467683FA57BB965DDF336185735F0BFC247C0BF0D7C33A037DFB3B18924DBFDF232DFA935D2FDA23576063F980EBEB59DDB3751515613F79200BCEDCFF174A93CB3E66D21597B51FBD65500617AF34CDC195896C367802A59235942396236E3D2A48C09F6B07DA31CD2281F32AE1F3FA5318E1D57181ED4C77126D8DC6E627BD3BCB78CA941D7FBC6963C22905493EE299E53ABAB171B719EBFA53024560B263771DF6D349F31F2AF9C76F5A3CEDB2022318E871DE95A628010BB49EA319C52B0B518A8240C369E0E793448A8CC02AB21F5A7B425D49DF83D78A5F94FCB921B1D49AA1D86C9FBB3C29656A6B48A72153EEFA714F5511B00CF92BCD1BBF79918DA7D690046C7E52EA31EE69769C05CA819CE2918964C8453EE686556C1660AD8C8DB4363E6091F7295E55BD00A467F2FE4C0271D69EB8F2CB6FE4D31995B692B91EB496A2B86267E0AA9523007BD48ACCB205744000EB4C662C564FE1E98CD35440CECAC1BF135408779A5BE5CAAF3F81A241B645E87038551C50CB1AA04001F4349B9AD5B24A95038F5A455D08CE891AFCA55873C0A748EAAC19D8BE464803A539645662E640430E9E94D8DC479CC9BB3D0E3A530B20FB743FF003CDBF2A29DE59FF9ED4530B11B3C8C0EE52541E3B53F0EAE0B8C8C64669CCECD1E0AE4F63D2907CDB038271D79A448D58CEEF315307AD09279CA771F9BD28DCE3790405C71CD2ACC9952A3711C1CD4B630663B036C5DCBEF42E7712FB5723BD1BA31972B90C71492224EA01620F6E29081B72E4950476229AAA5BEF285CF7A7B6C58C10C4ECA418BA0A70401576108CC165018EE1D85233F5708719E297F76CB81EB8E686611EE1BF38E8AB4AC00CAF364850028EA4D3B7270AC8CA7B91DE9ACC63C1DA4823914F69432F98CA78EC699686AB09243B1391FDEA77EF1B9DAA39C0A6075900DBF29CE73523624C6D392B4AE3B0B831FCD2F5E951C71A46E0827F2E0D29FDE29DCDB8FB5058B6CDA08C7F09A2E2B0ABBBCC21791DFB51F3093955DA071B683229DDB97069ADB917693856E001D6912C7087E656DDF5A245126E21CED539F7351ED2B20508CAA3BE734EDB1B1620B03DE9D85614EE9245C39DBEF52B0DABCA8C83C547BD64550CACB8A6984AA060DBB9EE690D0F8CFCC5990EE53CD47A9E9B0EB3A6DD58DF4292D95DC6D14B1374642307F4A9240DBB21B2CC79E6957272CFD178A9D872573F33FE33FEC79E39F87FE2BBB7F0F68B71E21F0DCCE5EDA5B15F31E252785751CE457985C7C21F1F43F7BC15AEA9F7B093FF89AFD7E599B62852464F241229E2E195B1938EB9CD545BBEAC2318C773F1D8FC2BF1CF7F06EB80FFD784BFF00C4D4B17C24F1E3FDDF05EB847FD7849FFC4D7EC17DADCE08E431E29DF6E75DD862481F7735ACA5A680FDED0FC7D7F841F10587C9E08D74FF00DC3E5FFE26B53C29FB35FC4FF1C6AD1D85AF84751B20CC03DC5F40D0C518FEF3330E95FADBF6890C79DCD9F4CE28795D54876E48EE6B35E65E91D91C27C15F85763F05FC01A7F86ECDD6E248BF7B7570A31E6CC4727E9E95DBC9F39C8057142B2E411D5BF2A765977E0EE1D286C9D582B2C8D8DC4714D611FCB96C9079C53CAA9C6F3B0E38C533E56528AB96F5C54DC9B08F147233346D8A4DCAC0287E475A5F9FE550A14FA9A0B46378C60FD2A8042A0B310D9F97D69FBDDE31B5D4F18CD3447851B402C7B9A4F331194F2F38EA45215877929BD54360F7A6C8A0176566723B52C6B1C98009DE3A9A5F963C0524B03CD01B0D21E50BB770DBEF52796EDF2BF2DF5A8E52D1316DA7E6F434AF186DCE0B64F6A9EA681F36E6411F1DB9A388F610AA4F719CD0D180CA395F539A48EDC42C3919278C9CD6822439621982A28EB488C926776EC0E94D986E918EF0477CF4A77C8DB76B6D5C73B6A3A8FA0A1B73050B950327B5315FE6240604F4F4A566769142B6548EA6A36936A6CDD939E00A64C5922BBFDE65DB83D6BCBFC65FB2EFC31F1E6AD75AA6B1E1A864D46E8EE9AE16474673EBC1AF4F63BC80036FFD29BB24DBB88E3EED352B6C29453DCF05FF008613F836CDB8E8371F85DBE3F9D6AE93FB197C1AD1E45953C210DD107FE5EAE1DC7E59AF66F2C7C8A99520739A158292BFC7F9D3E677B8ACAC57D1FC33A1E83E1F7D0F49D2E0D2F4768FCA36D643CA054F5195E6BCE342FD95BE13F8675C8B55B1F095A9BE89FCD5799DE5C375CE18E2BD4F6B6D03392BD41EF49F3AABB90BF875A1C8B8B64F24CB2E32BB028E368E29BB8EE0C173DBA9A8D83792B93904E68E1A418739C74A86C9B5CF30D7BF660F85BE25D5AF353D43C23693DE5D48649A6F36452CC4E49C038CF35D6EB3F0D7C2DE20F0147E0CBFD2C5C787230AA9686461B42F41BB39ADFF0030AE1495183CFA50DF272392C7391571F74ADB6381F867F013C17F086FAFAFFC2DA57D82E2F23F2646F3DA51B739006735DFF92EBB42B004F5E29581F9B0E0639A19955F70C9EF44A44D9B3CFBC7DFB3CFC3AF8A574D77E24F0FC3737E4056BC809865FC48E0FE35D1FC37F877E1DF84FE1D1E1FF0F41711E99BDA4F2EE2632104F5C1EDF956EEEDDF7470C78CD0C0F98DB87E23A51D09E5573CAA4FD947E161F103EB3FF08DE750FB47DA8CCD75213E66EDD9C671D6BD3B54D1ECB5CD36EEC6FE1F36CAEE268658B71E51B8233F4A9F700C093CB0C034BBD51509DC71D7D2A6D62FC8F11BAFD8ABE0E5C2B2FF00C233242A0758EEDB27F3AA63F620F8387007872461EF76FF00AD7BCC8A594151F29EA3D69AD12B46AAA403E99E4524B5B87C3B1E250FEC67F076DCFCBE118E4DBCFCD73213FCEBD36F3C07E1ED5BC2BFF08D5D69A8FE1EF205B0B1562ABB074190735D0B61573B40C0DA4F7A6F92235DA0F3D416EF5A730E2D9E212FEC57F07266C7FC22623FFAE77520FEB443FB10FC1A8D839F0C48C73D1AEE4C7F3AF70280704F2C37714ACBB5C16C05DBC1F7A9726C9E457B9CCFC3EF85BE0BF8591CCBE14F0E596912CBF2C93429991FD8B1E715D8F9C7772325B9AABCE02EE196E0F6A717F29D416C91C65453B86E26A96767AD59CB67A8D9DBDF5B4836C90DC4624561EE0D790EBBFB20FC20F105C34B71E0DB5B7958EE6FB2BBC5FA03815EC0DF2292A096619A6B6D524FF111D284D897BACF12B4FD897E0BD8C9E60F09F9FB79C4975211FA1AEFBC0FF07FC0DF0E2EA4BDF0C7866CB45B9923F29E5B753BD973D0935D648BE4C6A1B6E720E734FDA42B3B1CA9381B46702A6DADC4D2BDC9958B0DA4804F415C77C49F843E11F8C16FA7C5E2ED2DB528F4F767B75F359002460E71D6BACE91B3926538E197B52F965E309B43679CC94994A37DCE5FE1DFC31F0BFC25D3EEAD3C31A7FF00675BDD48249630E5C138C03CF4AEAE46F31B68FDD6DE493FC54CDDBB765B69C630BED4F0CDB033370DC7CC7AD5AD0679AFC46FD9BBE1FF00C52F10FF006FF883467BDD499153CDFB4B20C0E9C035E87A759DA697A5DAD85B42B6D656912C10A8E42AA8C01526EF90911201D003C9FAD3729C267083E6F98719A98AEA3D9587292CA814F999FBDDB15C6FC52F84BE1AF8C5A3DA693E27B592EED6D653346B14A53E6C6339AEC14850CFE6152DD3038A095CA3F9790072FD687D89E5BEE713F0AFE0FF00863E0CD85F58F85ED26B6B7BD904928925F30961D2BB665CE55D956936958C70C41391B4631F5A4521A621995CF5C629A43F24386F283E61B33C8EC6970FBC3800276142866563B576FD7A523B2AA98F2D823EF504ADC6FCBCBA02D9EBED4CC46CA64EAC4F2B8ED4B1C6F0C7B179DD48CCFB400A323A9A5B9A0BC1932C988C0E0E79A08DB997E77CF614A250AC7EEB6E1F950234F2F2D236DCFF000D326DDC7E5A6E76E38A6B36E55C91BFA6D5A76C676183B401488AAF900E5BD7A540C48D5977314C9E829482A98DA32DEF400597CB5C923AD3166DAAA1A339CFDEAA440F501576332A1FCCD2C9216DAA3693F4A00665F30E3AF19A679C19C82006CF514143BE72CDF28CF6A02ED6FDE1E4D0CBE6487692CC3DF14657702E7241C628246F97BC129D33CD39BE5E876BF4C629BF3001A3E149A715DCA1DC6496E681C77245F306171EF9AF12FDAABE017FC2F8F05C51D8F950F88B4E264B29E4380D9FBD193D81EDEF5ED3F3B4841F9403C1A55F972E5B0DED53B04A3CDA1F91BAC7C09F891E1BBA6B2D43C17AC79F19C1686D5A446F70CA30456637C2AF1DAF3FF086EB83FEDC24FF000AFD86334AAB8576C13DA95AE6552D976207BD6916FA9518A8A3F1DBFE15678E9BFE64DD70FF00DB849FE14F4F855E3CFF00A1335C3FF6E127F857EC42DC4B80779C7F9F6A16F256C9CB0C7F9F4AB7256B213D4FC75FF8553E3D91F6AF8335C27D3FB3E5FF000AFA53F651FD8F35D93C4F69E31F1C583E976560E25B5D36E9312CD20FBACCA790A3AF3D6BEF6FB44ACBC126A2DC64C16254D66B6D4D39B9760666E581C9F434051FC584A4CE5B707C0E94BB1566C8914E474A57204908F317E538A7284DE422EE3DFDA869046C873B9BA6DC7028DADE616042FAD4EE41198C47B8EFC0F414BE5B6D017073426371DDC9E845377328C2AFCB9F4AD00731793700E41E98029BBA263B5F391D69CDBD1BE5C28F6A5DE171BF69DD40C232594EDC614F14F52CADF3FB9C91519C618807DA82C40566C93FDDA4E4305590072369DC7BD38A11102DB43F6A8C959B796253D450B1AA28676DC076A5A8B51C148019C0DD8FC285665EA01CD0F858D5B7679E00A5591243B80C6D154242C63279F902F6A6160A4F01C13CE295D8328C063CF34F568DB3FC008C9A4511B293212A8703B548B9643D14934C1D3037303DC52ED5560A4B7CBCF4A7B08511AB28C30E0F24D12C65A37C8C91FC6BD2A35710F3B4B6E3DC7152057937804227F7735172B4239081B309C00391DE83BB736557047009E69E19846CAC37761B4D46D18DD196CFB73CD55891E234DDB986DDC3800D2AB09029120001E722964F2D644D8BB8FA0A61FDE2119F2F1DB14013799FF4D53FEF9A2A0F9FFE7A2FE545215C963C48B967C85E3AD1FBB74D9F778C934D54545240E3A9DF4FF97CBF97CB04F5A7A8C64653006320AE2915808F0AA324F7A792772ED8C01EB48C00642CA5F2782B4EC2B00903AE38CAFF0008A50C5998060BC7031492231C8401771EB4364AAB17507A61681D8114C7B81019B1DE91C6D8861B9073C52A85676639271491B1DC4A81B4F5CD210B1912A92570474146CC4780877E6871E715C498C75C53798C1F9CB127AD1701CAAACD976652BD452866DC54FCCA7FBC290B18DB97E5BAF14ACC64623395C52B8EC4124DB640ACA76FFB229F1B47B8280C0B500FEEC2FCCD9E79143487E52A9863C0E298EF61CCD18528060E7AD2AA8770B19607BE69BB8AAB3C89C8E7268670D970D863D31D290AE0E1158A92D9CFA54ACC7CC5400114CDA4E543FCC79A6B2A6E032C59475F5A02E49C42C012C41A63481599910E4528D91EC7DC49EF470ED976F94F4C53B80E0C09525CFF00BB4B8DA5902EFCF7CD431C68ED80586D3D453849B4B2EDDCBFDE3D69120AA12421B2091C53B186003F4EB9A198B3008BD077A62B47E5B654B64E291AAD891B2CC502820F7A8B72A2ED09CAF5C53B707C2A643F70697855FF005803E79AA109BF6B6D55621BA73D29CB186DEC9953DC934AEACB991493E951951B8B025B8E568B8ACC1BE7997E52571CB2D2AB0F330C8EDB79F9A8DCA140446CE7B1A5DD223312B9E3A9A45586B178D0155C293DE8DC0202AAD93D4D48DF3606436E19FA5359BE53133F4F4A8137D87F97B02334BEFD2A36981DE58303D8814D6054AE242C319C53BCF593EF03B318E95491021901507EF9ED9EB4AFBD181E067AD3A32B202A17040CE69A7805771CB0E0D31EA0C9B5836EE31DA958158C6DF9B7738A4451924BE462923DAC71F30C0C502171E5FDD524B75A155595F86DC6850FB939F969196546661CA9A571DC048ACC4E71B78E68591BCF201073D29369F2860283D4D3955415C9C31A63B8AA1831C264E39E69ACC23505D36B1FC69C0155C83BB3C6293722C44F39CFD6900CFDDAAFCC5B2D52195594888630319A8966324837A65474E2A5C7DF0C303191B68B09B02A04272F83DB14AAE8BB541CB63A914C546DCA02109EF4E66DCE36C7ED4C422B063B9CB73C0DB4D595606270C573DE959B6CAA9B7E947CFB4124601E94916F542B3A0DCE776476A6F9837AE14ED3E839A70C80CD8DC7DE9DB8EC0FF73B7029886E13E73E61E7D6A4DBB994327CBFDE06A368E26F940258D0B20DC3765507000A421DF7243B37123F2A323692A99723BD0B33479F9786E8695B218646F2A3B503435581408235CB73CD49B995829D8028A6EE017708F07B0A66D0D1B31439CF7A7701FB447B998AE73417937313B4F1E94CF9A45FB9966E9ED4BB4863E60C6050343CFCDB7242F148B205DE010C714DDAA595B3804639A7284693E41B4FFB554161792C8AEAAC71F4A031693615F9474F4A6AFCC0A850EC39DD42A8DABBCF967EB4856172CB2021940F4A6850BF37CA093C35342AC2A413B883D4F6A72C6AF9E376D1C500D0E597931B0DC4F3BB14DFDDA6199B9F51429E1BCC1B3E94E6C305D89F281CD21218BB58107863DCD28620281B5C7A50F8604AAB1CF1C8A4DAA308ABF3A8CD0534492289180760BE80545B82C7E5A866E7A8A1640CDB80DCDF4A5DD2076555E3A9A09D98E568A2914EE2481D28F343210BC1CFDE6A6B615D7E407DF14AABE61DFD876A06F51CACAE402C378EAC7914E12286721C1F6C71512B0F3181558C7AE29411B76C647B934087F9E53F759C9C7F08A24645914FCC1C8E777205359983151B4391C114AA4FCBB8AB7A923268290F0C2460581418FE1E94CF95718CB73F769BB4C9BB0FBB77183C52AABC67685C803EF6698AD61FFED2AED03EF0269CCA24CEE65518E062A358C229C9605BD2936AC61C16DDDF18A2E26197F977B662CE3A52A9CE5140500F5348C4ED56D9F2E7904D249F32B163E593D290EFA0AF1CC385DAEABEF8A373F4445538FCEA2C94010A976EB9269D93E6648C3FB74A7710E5C48B82A371EA3B52F9A77142AA02F01A8567562B85556E293E664190A0670483CD4DC056F958B6F05718E2A22E906490583509F29500EF19A999490D9030BD315483988D2445C6D4CEEF5A5F2D8C67E550179A4E768DEB939E31408DFCC242FCBD813D6930B8BB76E0BB820F6A4F2C60E5F93D0527CDF3031F2BDE9C772BAB48A083D284171D0EE915B90B8E3229159F6B0015867AB5054EE0A176679A419552242073C62A89170EA403B4AD234AAC9801739ED4817E663B8E074A1642D9070BF85218F2A07CC548CFBD3774423FF681A411B065DF823EB4E6DA3749B08ED4C76170AC37671CF4E948CCBE6E40665C51D63CB329E7814E6C3FDE054015221ABB1C1FDE1FC69321971F86690C71FCA41C0EE29772B630718F6A46884CAAAA83B8B761EB4D924E8814A8C7514ACED2395032D8C838A7AB81B4B80ADEF5421AB0F951A877639E73481999D4618C7FDEA7EE675C37AD356464550CA422F71489D45D8158200C17AEEA5FF57F3862CDD9714A85B6393D3B134809854316CE3BD263116489E360CBF3679CD023FBDCC60E3E5C75A249A3442368DC79CB530AA121C9F9F1D0546A263D999B68CAE0719A698D639002DF78E4F7A19576A7CA73D714B1B85CB6EC1E805688439D793B13A1CE683703E65C90D8F4A6B39DA4B36DE71914D6298C96E474A6039A3F257E66C291CD2B2EE0A100618CF34313329C9F97E94D318562572EBFECD3402297955B23691D29F997001186A6CB11DD85250FBD2FDD6CBB16006322A4ABA0550ECDBFA8A372C8A54000934EC990E171FED6EA5F2DBCB2405523B8A64B13695280A71DA8756556DB85E72714818BED0D20DBEFD69DB576BEEF97E9DE900D278DBBB39ECB4A08793600178EF48236954053B47EB49F3AEF180B8FE234C7615A4DBB620A482392B4D66D8A843B63F8C11529F2D064B65B1C629B34CB26555B031CD4809E6AB46BE5B60839F9A8319666CB82C691957E668D72052B17E5F03E9556B086850BB826ECA9EBEB4E565F389F2F923232696491D07099E3269ACC7890E2318C0145C7A8B1E64242850E0E4E3B0A528A8C3696F9867D79A8C6C5DD83F33FA53862DE518909C8FAE29887626FEF0FFBE68A7FDA47FCF46FFBE68A63B0D65F303064C21C0049A6346A19A30981FDECD38425A1CE76267D734799E4B28DE1F70C735371DC46DED1AB70AA0F6342A796A8AD2EDDCD9149B6487E7655718DA003C53996166DA7E6E3F1CD3B8AE0FBA051B5F78CF7A36A961B416C726A356648D846A7AFF154BB82B3798C41C7F0D22B718EDE670836E7BD3F7952B11DBEFCD3166644188C94078C8EB4E6923EAC9CB5315AC22EE55D839E78E28FDE08D94A640E49A5F38AB1201F2D7818A6AC9E6C8705914D02B8F6C850588C7618A566956404260629BE66C7C36644148A5A56474276FA31A02E3B7853B8365CF6A8E362DF33EE1E9814BC2EDDAB96CF34FDAD363276F3DA80B0DCF9D26DE718E775292F1C641DAC474144AB91C3E1B3F8D3777017E538FBC7BD2B85C527F780BB6C6C76A452CB9F9339E01348EC85461B2697CF4609805B1D79A655C066165042B739343B348CA563C8CF149E61849DE81B3479C7CBDC8BF74E3140C59186E0769504E30A79A5DD232B22818CF5239A552AAD9F28B1EBD685919B722670DC9CD048A4CB26C1B31EFD8D27EF5761014E4FDDA649388D55555B068698AB108994C77A9652689995D58FDCF987DECF4A6FCAB1E242AED9ED51421A143B9146EFEF1A558C37CBE595CF273DE90126E7F30052153D0D2F98FB708075E78A6FCEB36D45C6077E948F194C7CE41279E38A645D8FE55597765BD0547C6017661BBD4D38C63CCE24F72698E4BEE54F9F1D334AD70D41D5376543165A7ACC8AAC4265FA734BBD94E02E0FAD0CA233B495C7534EC2172C91821473C53151F250303CE73479664556DF8EDC50330481549607A9A617125C090A8621BB6DA45918C78D84E0F248A72A08F7B6E3B81E2986E5A35390C4E7A8A0A4C7AC68CCCC0E180FBB4E5F31A3C6D5A6121832B2ECC8E083CD2EE60A9B4EDFAD228568CB4672D9FF0076A35611905CB67A60D3D7628660C49A6B4998F73E5FF0A6910021C7CDBF3CE6A4505B1212062A3F310B0250EDC7414651B380DB4734C2C1B955BE66C83D31DA9FB901D80939E698AEB248A4478E29C32F97DB8EC295806B33BB6150E0719CD3846C5980CE71C9A6ED62A768393D5B34E6C2C672CC4E31C51718854EDC1CB3638A5DCE2201786C669AACCAABB573F53432A46C8096DDDC6680051264060391D73CD22C6CA81981201A7078CC87E46DDE948CC4296C363D29D862AA9924F94E063A353448FE6796E7720F4A733BB6084F97149E6BFC811320521E82EF71B7629EB465F72ED0383CD2F9C2355C2B96CE4FA53B71C1046D53C9F514C5A0D70EEACA08DD9E297E7384DCA1B14D8E4401821E7FDAA5662700A0DF8E3D6A462F9722B2396076F1F5A42927CC77724F4342C6DE58563F3765069557764B1E47402813090ED40412C476142A95932CBF7BD69046114E4B12C7A51B486224242E38E79A64DD8B8F30636FDDE84D11C4D1C6CEFF00373C522838DA9CA93D49A79DAB210C09EFC520D488485A45DAA338ED42AB4CA7760B06C014E90ED3BD4ED3DD71CD47E63C8C0A6D18F5A63B93374E8A14F07D69AC46EE1F6AE3A8A644C92AAEF3DF9C539245552A172BD3340C46D922E19988C7514E52432844665238C52F981711EDE3142CAA176FCC4818E29B109FBD8C659B8CF4A77965A40CCB9DDCE45315A5E01E173D5A9642D16642DED8A915C5DACC1B80BB7A5265D461F863DF348B1A4EAEDBC81486488EC520EEAA42BDC7AA98A4183918EA6A39186D601890BE952042242E4363B2D22C84EE223F94F5CD2D005F91A350324D1B7CEE76636F4A1885DAC186EA6E1F0DB64E9C9A0AB8B18C6D2C8771E00A67968AEACC19707B52820A82CE4C9D8034AB2FCE03C7F28E46EA07715BF7848DA570320AD22A1F2C2B06C31A48E47DC41F907F797A7D293CC95598F2EA3834EC4B63D18C6C004240A9236F977160ADFDDA88BED0BC7B1144810C9939E4631400F751220E42FAE290FDF0A7E603BB522AEF51B88501BB0A475133105F81D2905C74C1E4242FDDC70690F4C0601957240A6F99E5A82ADD78A7F96246CA152D8E493C53D02E195F3115CE48E6987F78D88C0EBCE697E50872C049F4A3CE298C053C727148AD07B3AAB05541BBBF351B432797B90E5B3CD2EE50C59630723A9348B22ED2555B713D33814C2C2C8DB942990023A814C662DB4A86CFD6A4FDDC7BC95C16F519A5599D9B6220200A0434CC1BA0D99FE1A5DAF16CC9069269D635507EF7D297CC53F3A296C0A45682319610082A727BD4AB1891816553C75CF4A89256383B01EE452F96F2E58E02B7F74D30D056DEF1B1C2AD1249950A3E638C9C52347B6400B82BDC523EE66DC8BB73C1A5722E285DECAC5318FE1CD1991890170A3D6964C09371CB607F0D37CC665665191D36B5017622C6EA198B2E295DA69486036AD49E5AC8A57700319A8584B18283057B1CD03B920531E7206286FDD2E0E39E7AD428FB89DE59B1ED4B1B7CA03A1033D4D26512BB48BB582F51DA9B2052CB821D8FE94348ADB963DD81CD34C91C8A7E465DBD3146E4B1642D1B972DD3F84549932201BC63A903AD3166F2D82EDDC3D4D23619564C60F4E2AB601728C369DE33EB4BB515B6963B69B3481705373051C8A45915A2DC50E73CE6A463E4C3302C8A31C64D0D26E9381BBF0A4DC59B6B479514BB9C31555238F4A7626CC6AC817710A770F5A5566DCA76855EE48A42C17E5C7CE7BE29D87DF96E57B530B06E12315CAB0EB9A47E39E085F4A372302C781D36D2798B1A9554DC0503033632FBB6AF4C114A55E48D769C00339F5A56986D080638CE0D1E615D9F28718CE338C52616223249B76BAE59B9E29CD965DACA40EB9A706694AB37C83B114892091997CB2E3B9CD162436893B6DE383EB42C4FE5292E060FAD09700FDE8B0146050D3318D7E41E5E79268341DB12408597601C71CD3C2A2B96070A3BF534DD8F2B80AF94C718A238C60A32724F2C0F343131AEA8CA4866CE78C53B680A401BB3EBE94D4578E47519DAA3926858CB46CC54EE1C67346A40AAA124394F9B1C52C71B1607660918C93C534A3B2873F7F1C52FCD3301B480A39CD03D4465F337210A171D734342158A90CAB8CE696351E5B663CB678C9A5F2E4574CB7CA7AD031CB2154208DC1860546D137DE1B5557A8344D23499403254FF000D3CEF3215D800C73CD091235E45F307EEF79C718E9446C2391804209E471D2A41B9B01502951D8D33749246C48D9CE3777AA1A13ED527F914533CB1FF003F27FEF9A29142C96F23484F11A8382335298FCBC2AAABA83D6A25F2950F98CCCC4F40697748BBCA2FCBDF8A081FC4B2152BC2F6ED48BB83202B9507EF0EB4E1149246B83CF534C5CC32282EC7FD9C50161CCBB9C94761F514D5C9DEC64DCE38E9C5384924796278CE3151CCACAC9B8E01E7681CD003FE676FBF80BDA9649372E58E0E71D299F670F26558841D69D1945DFC9751FA532AE2974932039DAA39C537E69230B19231CE6915D549911095C511CC928739656F4A451246CCCB861918E71D6A2DA5546032A678269E57CC8C95565F520D0DB7846666E3914086A88E375604927A526DFBEE37510C91B65146D23A7B52B848645DD2123F9D4BD41B044675057E563EB49BCE5D4A6E7F5C53E456DACC785CF04534FDD25243C1E4D34891176C2A5B68C1E39A432794A0AC6327A53B6A05277EE50781EB4D6906E0CAAAA075F6A6521C93798CCC14B3743BA9AA116421CF0390050EE3A236E66E79A37ABC7F36C0C3B7AD4831AD30F3311E54B0E3352AAB4014EFFBDC1C5394AF3940A40E01A6AC84ED6F2B2A0D0F6205DFE5ED5F2CB0E809AAFE635BBE1F9463800559E3CCDC1B071D0D357715FDE2EE19C0A10EE34E2E240AA5428ECD4F652C721BEEF1D699332EE0B1272BD6A4F2FCC849DB96EE051615C619046E537B02D44790AC4CA71D0714EDC8922A15DAD8EBD695A4DBB4023603CD502633CB48DB617393CE4D3B60859FCACB13DFB5376AA65998313D01A6452BC8A083B541A9B9648B92AC1DB0DD81A56D918C390CCC280A556460A58FAB5354868C308FE6278A0962BA346AAA30B9EC29576C6E0313BAA378DB7231279EAA2A576F9C128698EC21C18CED397CF348D81D433363A53B6BED2E0819A8DB7280CAC18F7A1098984913732303EC697CE56D8A5385A195B707C6147A7AD3BE7662CB839ED8A00456587855DC18D2B37645E0727752C6CC9172A08CF5EF4CF32305B71233D295C771FE603862A428E303AD37CE118C0CE1BD7B5342AC7925C82DD31522B7CA005DFCF53405C6BEDE00620753C5296591797F957A638A03ED660E3F114D2B1B4606DF941E4D000B2796A3CB048CF39A232577166EFD0D3A57930A2251B68C3347B997736727145843772AAE0FDF1CD0C026D97EFD483123062A1463BD34C7E710300A2FA1A62B06E0CC49214E33C546D208D0804B1CF20D49BA2864C282CC474EB42B3333068F686EE690D1134DCA957EBC6297CF10EF0CD863D2963E59C2272BEB4E6917CC1BA2DC719268B05C647852B2333106A4DC3CC2C01231CFB53DA63F20F2C6DED437F1264056EA7D293020685242047F7872734C4982B379AA4B2F4352B4276A857CB669FB02AB8619DBC926985C62C7BA4F330760E7835233798A40F907AF7A45B85900554D8C69C362EC2F938EF40AE357322A82C40F7EF4C2AB348C4BF4ED52EEF3BEEF08BD09A6B461D0E4043FCEA90209142C6AAAB8F439A6AAA83192496FCA977476E707E63EB9A5FF005CC58AFCB8C8F5A4682FDD66E793EA298CCACAAAA06EA16468D863243762334E1BB616000DA7D28218C6408CB84E54F34F3B594E0305EFB699CCD236F257E94A92796AC10EFE7A5310BE72B329C63B669D2155520FCC7392475A54627036853D79A6AB8DEDBD803EDD2A4770F3126608074E683E5991F7649EB4A194282A016F6A456336E52BB4E7A9AA421921EF1A7CBD48A7BBA6DCE1432F6A55DACC577F038348562F3368E40E68B94832C72CCD8038E29BB86DF2CB11EBC5324994C80153827B54B23A7455DC7352508A628D8838C2FAD359979600FCC69EA448581015BFDAA4561B867E719C6074A640CDC9E6190292BD08A7C8CCCB9FBCA4E00CD37722C8D19529CF2697F771C9BB69D83F5A2C4DC46904EC551781DBB0A7473EE528570C4F38349E62193695233C8DBD294CB1ED0C23C1E98A617155922F307DD23AEEE7144AAD31EAB85E41A6B471B7DF56071F5CD2A2ED5F9D5947AD480348D311182AA3BE29D27EEC00155C772B47962464DA157DE9120E1D7712C7B2D3010E428F2D7A9FE2A3085B2473DD568DAEB8087E51D7753B6ED93720DCFDE8191B32287E37678C8ED446E23C6D5DCC07F1734E56DCAE9B327393C629BF6758D8BAC8C38E475A2C036393748498F0F8E0F6A7B6D6DAC509C75229CADE6A92410A460B018A6068D230402587079CD03B84D700957271838DA69FBD551A44700B52A3C323EE6192474C523C88CA46DF957DA96A1A0C59130ADB4B103918A6856858B632806768A7B5D46ACA421195C74A5D81A40416D98A76005B912280A9B58F14E6B711C4776727B034D930B0901B6B760B4286923208DADD47AD2B3108B294C031707A714E8DB2AC406524F7A564FB8CCFD28690BA8390147534EC21A9198D9F7927D36D3599625202B727934E55054BEFFA537CE458F05989CF34CB42948D5558163ED4E505158800FA03D68F299C860DB5452794642496C01DEA46D0C6CAED6248F518EB4A37346776060F43DE8662C163CFE429AD18130576247AD56E4132B96CB46A08E8690329563B4823DA9BC46ACB1292453A466C203F27738A4171A1DD5BE6E03700E3A52246BF3162CCA7818A7B794EA4993349E62B2A20DC07B521DC560370011802304D46ACB0B6D390BEADEB4F5448D8AB3301D724D21F2FCBC60B9CFD69A15C6A666524C9F2F7E314B24E77050CC3B138A3722236093838DB4D8E64690A8DDBBD31C5055C773B787CE0F7A729499C312C40A8F0236F9B7127B538E1828DACAC0F41DE8B83636491371601860E36E2A45646CB60EDC6318A18A46DB48652C3351ED1B301C85278E298921771506444E33FC54B1C9F333B2A941EF48D205DC9CBA638A7096258976A6548C9A91B13CE79FE555F933C114FDED0E57E5DA4E33DE8DCBE4FCBB941EB8A4DCBB0295DD276C8A640D90349F2A0C053CE688EEA396308CBCE7EE9EF424CBB49F2CEE1C1CF7A1A58FCD388CEFC7141698E937C8C82152ABE8BD2858DF7B1C7EF71D8F14C89A4918282D19CE4FA54B80EE5092A7FBD9EB52171B1CA58AB3E47B01D69A63918BBB36D1DEA4568A3007DE3D0115185D8B92F9E7A559371CB1A48AB20627D074A5DA2460C24DAADC1C9A0C7BA4DC58796A29AC04DE58CA80BE8691771C803BE03ED2BDDBBD3789D55012187DE3DA9CAA90C8029DC5BA6693C9665766214668B0848D0A296019727073DE8F314B3EC25587DEDD4E68C339DAF98C0F5A5768B73AA0326074AA1310008A0E492DFDDA199159536BB3F527B5395A4936C71E30172463F4A5595DCC6AA02F1CB3548B519F6C4FF009E0DF9515279527FCF48FF003145505C62AAF98BB141EE73435C3E319F2F9FBA4678A4F3372B0750A3B0CF269AF330B8036B22B0C00C335372F61030864F92627D6A566DAA0B37CCDD3229591773ED5DC71C35476ED2EF01B6FF00DB4A64DC9159D9391B9579245059B702FB42F6DD4D25FCD7C8017BE296441708B18CA91FDEA4896C6C9BD7956F909CF03AD35962DC029625BAE2A45CF96104C1429C1A748EBB8042AC57820F5FAD1D4132B4914A937CA4ECF4A99153CB77098078CE68F2FCB76DD210A7D3914CCA47B6371D4F0734C60D3C716514B00C3BFAD3A468E1F994E5D85481918E0C790A7866E9512E376767F1718A9B00DC798A54A856FEF0A3CA8E404B065DB527EF04CDC64E39148AADB460E58FE42A8431577A05126DFC291A5F955761273CB548F288183965C631C0A5F311622C096DDEDD2A5B28636370609FBBEFF5A49A607E58D547A8C538AE63077E475DB4EDA376E451F77F8A92110E11984806EDBC114E8E186719206ECE7029C81B66540EBCD2C718552E40DCD56211635F99F072BD334E89A6F2C0DA1509EB4C8D4C8AD96D847F08A734BB95500248EF405817CB567E779A3C991146D390C734EFE2DBB407C726991C321C66419DDEB40243CC86DD9C14DADEB5191F28667FBDF854C59164C13972707349F717E6DAFCD058D1B15880496EDDE917600C369734BB9964C85C1C714ACCCB11200CB75C54B1E842631E62EF383E94FF00336855555DBED4AF1BC87E600EE1C73491FEED55485183C914EC4B091A566620F41D1BA5236FDA194827BEDE94F8F31960CC1C1E79A62EE6E0285563400E9247565DA707BD22C6D1B3B48D807834E646762146DE3F0A859648E2F9C861E9DE91571E230AD9DF84EDCD0A447BB2030ED4A70E1414DA286CAB1F94053D3340B71A15F7023E55FBDC9A7AF9BE613C6D34D685A5241E001C36690FCAA993BBB7140B61772961EB9C7B524EC928E5395A7798982AABEF9A584A9E17393C9069210DF2C488A00DB8EF4A55A3937B3FCABC52642336F3C37A52E54028159EA842B347B401B5B27393434BB95801F28F6A44DA8C3F77F4A5DC1872082C7A2D48F51BF37CAFBF1D851F679172564DC0F2686D91AF3923340223937124AB550018CB4879C8142855DCA0E0F7A5FBD26F4276F4F6A5DCA19B0BB8E3B52D477058D70ACBC6075A6B6C9B0A4B139A73332BA82D85FA5248C233F2AEE20E7750210C5B77ED725FB0A3E686325CE0B539242ACCEEBF4228553212CCBF2F6A60354B6D4C9241F6A5F2D154752B9C934AA66F94AE02E3F8A9164550C1865A90EC37E4C1704AFA52C6AF26E39DDFCA9E8C9B5401CF5FA535A6565640E41EDC5311232B2A8202EE03B53550B6D677F97AE2845552818B1F534AC919553F36698D21BB55B78CFCBD68640DB7E6DCB52E04A3E4C607069AEC102A0F909F4E6A2E5D911796BBDB7293EF8A6EE2B2A940769A5591E467591F68E829FB442A172589EF5445C7AE18EF0DF2E2991FCC30AD939E0546AAB1CC4062476A706F99586540A404849DC632C03772699848D7E6396CF6A56863924077F2C3BD1BD42EC0377BD310BE5977DEB9271F8537CC8B6156187269C245F2F3F329E8052F93116DC41E0678A7610D8C2DB92579E4529DB23373839A3CCDB20047C8DF9D3CED931B00DAC3A9A4C6432489182106E3FCE9E581006DDACDED491B2A075017FE054E1326E001CB8F5E95360233216C28DA48EB81491AB46B94191BBF8BAD48AE3712CB86C7502A35675D85DBE4F7AAB00E494B071B36FF00B543388C02841E79DD4AC638C121F3BBD7A52B3F961772A91DA8015995F3C8F331D8546C4C68BCF998EBB853F6962C72ABC71B69639DC46570A4FBF7A0435A48D64539CE47DD1422AB2EE72300F03B8A5055591DC29627A0A6994F9BFEAF218E29808B2471E5779CE7AD39996560ACE48FE740841420264E7AD2B6187CA02B7414C6319460221C32D24918FBC8ECBDB9A9864765638EDD6A30AEDB55CEE6078148761AB868C29049CD2B3056F91B611C1A7C81E52CA06C3D73423078D958AA806900D91995B2AE1BDA9C7F76839D85BAD1B82C8C368738E285570C0B01861F951710D898E0E5894A45D9B4E36839C93DE9DE6EFC2A82C075A12355DE70A18F38A10842C1199A3E4F43C52FCC233B47CB9E6916E311EC23E6CF3B6959B87E4A2F4C7AD301DB964C2A8257BF14DDDFBCC6EF971F7697F77FBB31B9071C834332ED3DD89E08A063554E5770DA077A56649640036001F7A96260612D202A49A45606160B8273FC54AE00D1A98C0E5D8FE543218CAAEC5618E42D0B1AA861F36E3D31D295632D8C0DA7EB4CA422DB969707000E76E69645588952A0646477A423F76D8C17EE7351FCE8AAE4066E953700F99241BA4E1A9DC4721F9B23D0D35D7ED180CA15C0EA78A584347096255BEB402242CEB1A945C67B51E614DC5B1F4A6F985590B36D4A568557326FC83D8D3B83408DB14379839EB4B1978F76E2ACA4679A4DC9B40601075CD4824573B42EF047069124062877065CB1C64FA548F2160AC13E523A5208D5177FCDC7F0D22C8922847DCB8AA100589954B64127A1E6879B0C3CB4C28E3229C5559546CE3B60D2AAB7978DB900E71DE9011B44B1A921FE73CE2864DCFB93224C64FA52AC8CAA4B01EDEB4ADBE46C336370E3DA9811B3491F96CF83EF4EDC199640A739C0A54DA182BB74E41A46432C870D95EA31400EC166779178ED4DDCD2C78DE14E7814F89B8DA1B2D9A89A1DCBB9B71753F854DCAB92AC9F36C62140E3A75A6C41A4E300463D05270EDB953A75A94B6E1B225EA2842235D8CC15496EC29E9E6ED3F2E4AF7C50B6FE5A8C0DAE4F1934D2F20DCACE146EC6734D8EC3B71961196543E9DE9BBA37F94B618724D3A4C1618018A8E69AAA1F254609EF42102F98D970703A7CD4D4DBC331DCC38C7AD3CB34DF20E028E693223519017DE985858586E0AA987193F35354EC52244527D6A5548DFAB12D8FE2E29A155637DE413DB9A432231BC932909B411D3B539610C8311ED2DC9FA50D3331183B78E73D8539BCB46461233606281730D48E393E719500D35564566083218FF17A53D953702AE5549E94925C6D1957050F1EE2986A3B69DAF1055041E589A43BA3D9E58EBF7A892157E8FB9D941A463B76A2498CA8FCE818E937B4883785C0CF1448C244F2D5B7853CA9A7304DAADBB2C8792BDE9CB21450C09456EB91CD48EE41B63FF9E03F3A293CC4F5FD28A081FB5248CC688AD8FE2CF34F0D8E379DEBC608CE2A2B7D8C198C24283D41A7F990EDF301DEEDC6476A761EA37CF326D8C1C9EA4E318A5669C6D760A47419E828F373182C9C371B88C53A4FDDEC590EF1D4006A86A3DC698F742525EB9C860694C891A850E48718191D298245BAE186C0A71C6734EF30FF00AAF258953C13523E51CD1C6AA438CB374DA6991B2A90C2339E84B0A779C7CC1F22ED5F9738E69EF2395186E3383C550728C57923C174CAE78CD3996392562CA1B1CF5E94EE31F3069157BE6A1DFE739429B33C67BE2905AC2A6F958A04DA879C9A5990AC79DDB5B3DA959D238D9436E238151C7208D9792C5FB1A6161E8DCED666DD8F4EB4461D90940179E775232B4CCCDBCAB74DB8A700C8446E37127A8A96C76132BB59485009FBC39C51F2EC75460A07F150CAB1C7C2166CD234446640074FBB49222C363CB32075057D453B6664181B4638DC78A4DEF242084D873C1A5911D7602C3E99ED543E5055DD2295703D76D248C91FDE05D73D4522942ACB1A9DCA78A9158B2ED29C83C8A07CA37CB6762502A93FCAA4656087951E869B8259CAA81EA0546C4C71908BBB3D77532876E63231560ED8E693E691B710ABB69D13965DCA146383E9432B26E7C06C7BD4B008CEE66CA0271C351877C2964DBFDDA4DC246CB0D99E01ED4BE5C7E59237139C502B06E9049B370CE2A311BA9538DDEC0D49B955B6ED393DE99C2B1C06C74A007FDF70CA327D09A6E0B6448B8E7F8691498595447EF9CE4D3942EE73CEEA081102AEF0E08CF4A72150DD703190299B57192724F5069723CE054F1EF486351DCB15607F034AACCEBF30031C714E7C329F9724FF0010E949B847F261806E7229A10DF31598F0D9ED9A176B293213B87414E693F78360C903F8A9331952EC4B6EE3E945835018DE006C823A9ED426217000DF9EF48157CA0157E63DE9D0B3E705305475A2C3B0E55DAC405C77CD222855DE5B0C7B628552D9639CF6A42CCCAB93F37A1A63E505950AB294E73914EC8119724A93DA97FD5C819C81C7614D5904CE378F973C520E505DF26CC8017B51836EBBB24B76C77A591432ABEEF941FBB48B3090B04F94818E68F40D85DEAD10609C13CD2BE5890546C1D2917F7236B90CA39E29AA8D20670D85EE290B7155563668CB657AEDA5556321C00A31814DD892382064E38A3CE756452B95CE334C690ABBE1DBBB0D9CD3577F98E461B8E734F6564C0E4B6734A2E39208DA0F7A076133BA3C16C903A51B4B30E5BDB14D59155982AF269E0C91C4A3A96A5701B29666D841F634E6C42A0940C7BD3593E74DCC79FD29195B7BEDE78EFD29A247292D26E518047A51F29909381B7EEF149B8ED51C83E8A28654201C9F7E6995615A4336D1903EA29DE63990A6074C0346E0ED8539007422993797B1064AB66A1B0B037DD650016E98CE29AA5E35FBB8E783D697CBDBB98618E3BD2A46FB42907D463A53B1239A23272C9C0E73486560F8D98C0E29BE765B1B8F1D697CC545DB92C5BD2A84346E8F2CFD0F4A7349FC2832AA32693A6415661D850584C42AA6DC8ED49976157648BBB1839A7EE0AC405DA7B1A8BCC74263DBFA53D416C6580C75A1217288C9B9955C1CFD2919B69F94150D4F90B85CA82C4D3B20A10E7031C5363E51772B2AFDD618A8EDD5BE60400A3A2D37CB8D5914648F5A924B8F2F72A80547AD21728C58C7CCEA154F4F5A1648F701B7F79FA527CEAA0AAA80793DE9ECA799180C63BD31581599B2ED8C741492AB48CB18DA78CD26E5955570CA3A1A4CA4782373EDEB40F94120054214F9B3C9CD0C02F0016DA68F2FCEC3A865EE066955E4FBBB4671C9140F6068D998B8DADFECFB53E36336DCA2E1474CD46D1BC8A0823017A8E297E73B42A02B8E5A906E22A244407C649E0D4CA854312EA40E951AC91C985DA3729FCA9AD1ED9F8906D1C9CD170B06F7F2C80778CF6A734622901D8401E9CF34085A44C6EDAB9E314819B0B1FCDD796A64D80C688C5B7152D4F73E5BAEC392076EB4D0ADCEE6DC3A00450ADE5154207D40A9018BBE452E3D7B9A7346245002A8C1C924D2A953BD4292075A636615DAA990DEA6AAC21CE64E170A011F7A9533B57CC1BF03A2D20EF93B768CED3DE9373C9B997E52467A5265A154F564539CE076A158C8C5767CF9C52ACE0A8F980614BBC2CA1F6B396EE298342E3CB8C8298C74348BB997695C719DD51BFF001124F5FBA0D48F978C1076B1E80D04891AA42A09F998FB52230840257E5078A72177524B0DDF4C5233B6CE46FC72714318190C9B4E15867A52BE558809CE38E29048640BB5154FEB4E1218598B3E4F6A5B0588DB31ED39393D476A5C1E1587CC7BE6868C6D2CEDBB9A49366F4DCA42F6A43B0E2BE4F2AA18F704D02531AE4C7B7DE9228DE3DC36E43720E7A534E1C6C391CFF174A1226C398493313F2E31919A6B2FCBF2AA8DBDB34A2466520AA8038A6EE11B11B727FBD4EE3498A4C8C5032295E8714FDDB03E10601E3351852D0E1061C9CE334E85A568CABA6707B54D86C7F1380CCBF2D3554C8C4A9D80775A7EC76563C6DC76E2868C2C6C5171C727355B0B94492558D81DACDB8719A594AC8AA550330EA29130CC083BB8CF3DA9BB5B702A465B96A02C2C56E5240594007D5A9922F96ECF9661DC2D492334F8C0518E849A64722F28C7693D76F4A6090BE60688045C8EFEB4E9246F3026DC1C75A46DA1F08A4023AAD1B414C1625CF1CD20B03C6CD92C000A3E5A42A64C14209031C512298D802E4E463146DDCC0C43680298AC26C1E608CFC8D8EA07534817E575776241E00A7F9932ED202B9EED492664C65F61EFC5248611EFF28055C64F7A747266429B4E4771D298675F242E58106A465C286405988FBA0530B0D5679158051D7A934EF2D1A23BD3073D69B264C7B7CB28719EBDE86DF22A21214F6C9A8DC2E0B30F2F013A1C6691A4719445E9CE2A424C9F22B00579240A6B49B947CFC9E0902AC341136B6EC212DDFD28CEF53F2FCAB4E2A23933B8B71C85A6AC8655C2FCA01C8148AD04F33CC9BE58CEFC639A4F96190829B931C8A72C619BCD5DC7039E695263B7794E0F0452000DE6E1766D8C8CEE3DBDA98D1A1F248E5BB6DA7A0E0338217A0F4A89B6C2012E4E4F18E9564D8976BB48D9452D8E076A458C2298CEC57EBEB4ACCAD2798AC5B68E9EB4C6B9903348231E5A8E87EF520D046F2D0A053BFB1A9190472E044718C8CFAD357E6B7DEB1E327BF5A72B4EACEAC3231F79BA014C5B913DB3ED0E3E52C725453D9F74CA164C81D78CD010A46C58E158F1EB48B20DCED0A923A640A560B327DA9FF3D57FEF9A2ABF973FF78FFDF345160B0BBBCC90C619A2523383D053D6386150DCB15EEA3BD4B7DFEA9FF0A1BFD5C3F5A1B25116E59235632657FBAC314C560594BC441CE473C52DE7FAB5FF007A9C7FD49FA1A572B99919B9DCC1C05DA0FDD229645906D991F049C9506A16FF008F48BFDE3FCE9F65FEB850877251331CB3108CBEDC524733EE62CEAA4F3CD4F3FF00AB9AA837DE6FA0A1893261B9464C98E7815279D20704EDCF41C5563F793EB5726FF9674D1633E68F70C2873CF3479DE5AA8751BBAE40A71FF5C6997BFEB93E949B246B484EDFDEE19B9C01445BE48CFCA41EEDDE86FF005CBF4AB30FFC7C1FA5242B95D643B4A03B9C7F7B8A194C7F75B25BB7A54B71F79BEB4C6FBE2A843648CAC7966E47451DE90B346AAC50331EC4F3520FBA3FDEAA771FF1F15371A6580CF24C1A35DAA78E291A45491CB6EF32A6B2FF00531FD4D31BFD73FD6994208B6C464CE370A55DD0C4A41CFA66961FBADF4A64BFC1524083E6C8DE3D48ED48B19F2FE72064F041A81FEF4B4E8FFD59FAD51489D5764807FAD51CF4A19246662A767FB352C1FEA0FD2A29BEF7E145C183811B65A4E7A5398BAEC5383EF4C6FE1FAD249F78D226E3CAB21DE1875C0A632B36E0C70C4FF0D37FE59AFD6A6B5FF5CDFEED50EE1B3707E41C0E334DF304708DC303A1DA2A33FEB07D6A73FF001EA2A7A888433B1DAA405EA01A58F9C11966CF34E1F797E8696D7F8BEB54521BE618A46F313F11415555CB06EB903B5589BEE8A4B8FF0054290B621DCCB21644F97149F2852FB9B39E4669DFC6BF4A83BB501724F3017190C33D2A48D432024EE2A69CFF00C3F4AAEBF7A4A0A1E76B312495038FAD3D8AF981C270075A8BFE5953FF00863A42B8EDCCFB595015A6FCEECC813682686FB86A55FF0056BF5A10B7236D90B14209C8C66903FEEF010E09C538FF00AE14B277FA8A6CA11A358D87DEC91D7340DF36D5DB8DBD4D34FF00C7C254FF00C6FF004A5726E40D88D9958B6E3D39CD319D523C659C834F6FF582A3FE17A43B9234EB182027CC452797208C127DF1437DDFC2ACC5F747D2985C63711AE13E6619A8C16319F37383D314E93EF47F4A56FF0054B54891B148ACC114B669AEAF129DA839F5A55FF5CDF4A59BA9FA548D31AD2AFCACAC471F3714AAD1F9600CB1C6413445FF001E5F81A487FD5C7F434206C5F3044373A7CE452799E600F82ABDEA693AFE148DFF001EDF8D513713E448C9C16DDCF4A6164465451818CE6A56FF00567E950C3FF2D3FDDA5718E6DB6ED9DE72C3A533CCE836ECE724D36E3FD6252CDF765A9BDCA4C749958F7C6C5893CD0D2195400987EF4965F7454CDFEB3F0A63232CFB979F931CE0F4A68556909763D3A531BFD5C94B1F4FC2811246C1D76A2F7EF4A727085075AAF6BFC552C9FEB1298C796F2772A70D48564555DE32ADD727A5397EEBD175FEAC5226E29472CA232081F95355997726DC39E692D7BFD69D2FFAE6A4D8AE35B88C02C0669640CBFEADB902A1B8FBAB5245F7C7D29A01143792379C06A528DF20462569B79F747D6A79BFE3DD2A8A43388586D037E7B522F1B8C8AA79E3351C7FEB0FD69937F5A928B2AAE5473F213EB48ECC54C71B676B54517FC7BFE35359FDF93E869937076DBB37BEE03DA9B958E32E1B273C534FDE5FAD3FB2FD692218BE63490BB04CE4D1E594E411EC18F343FF00AB1F5A8A6FF591D58D12A877F9DB6E178A62B4AABCEDDA38C8A4FE06FAD33FE58FE348B43C08D994919DBD69DF685DC0AE79E36D247FF1EEFF00EF525BFDE6FC69142A96814B6D5F9BA7AD4855C0DCDB490381D6A16FE1AB30FDF14886C8F12CACA3236E38CF146E66DE0610F4A73F7FA8A8D3FD75048BB56160CCDCB52602AB31CE73F5A4D43F86A63FF1EC3E940D320DEBE66F00B2E3A1A74773E76D5083FC297BC75076FF0081503B920595A420F018D3FF00748A46EC9CF7A917F86AA4BFF2D2A98899D965073855E83147EEE251C6E2077A8BF863A2EFFD62FD4540EE3F6B7CA5484EF4BFBE238C119ED50C9F76A687FD48FAD59371EC85176F97C9E739A8D481BC2E5CFA7A53DFEF8AAF6BFF001F12D49572695A3550AA393C714E8F6C6DB4A9E477A817EF0FAD5AB8FB83E94D14370B1C7B993009A05C6E652B17B5249FEA969F1FFAB1F5A2E04789636182307AD1E495895802EF9E314D6FBBF8D3EDFEF2FD284203BF7AEE62A3D71D2958336FD8D8F7F5A737F0D46BFEA57FDEA0434E5A30A1F0D9E829EAC158EE60CEA3A5241FF1F4D5049FF1F2FF004A3A089E39836F023C9EB485DA3DD2990E3FBA074A6DBF7FA55BB8FF008F5A9632B8C97333138F43D2879036D609CF402A69BFE3D7F1A857B55922B3BA71190598723148EC5A15461B19BF880A9E2FF8F9A3FE59A7FBD5121A2BF96C846D93E6239A48A392650AFF002601E69ABFF1F8DF5AB7FF002D87D69F402B2AA2B2C6CEDB98700505665462BF281C6DEB9A9A4FF8F86FAD2C5F75E84222DC5510CCC76E327029E54B08D04594EC6A4BBFF8F36FA0FE750B7FAB14008BB9A328ABB42B63E5EB4E790DBC9D378618F9A9967F7DBEB4DD43EF0FAD3420684A8DC5F9539033D69CB3F98FB155A42C3E60D49FF2D3F2A963FF008F83545C4646D22B2AB05C630573D29439C2794CA801E4AD10FF00AD6FA9A58BFE3C65FA9A8B8DBD097CC97FBEBF9D15428A083FFFD9, CAST(N'06:00:00' AS Time), CAST(N'22:00:00' AS Time), CAST(N'08:00:00' AS Time), CAST(N'12:00:00' AS Time), N'sariesgymoficial@gmail.com')
SET IDENTITY_INSERT [dbo].[Gimnasio] OFF
GO
SET IDENTITY_INSERT [dbo].[Grupo] ON 

INSERT [dbo].[Grupo] ([IdGrupo], [NombreMenu], [Descripcion]) VALUES (1, N'menuGestionarGimnasio', N'Permite la gestión completa del gimnasio, incluyendo usuarios, máquinas y equipamientos, además de la visualización y asignación de entrenadores a horarios.')
INSERT [dbo].[Grupo] ([IdGrupo], [NombreMenu], [Descripcion]) VALUES (2, N'menuGestionarRutinas', N'Permite gestionar las rutinas de los socios del gimnasio, incluyendo su asignación y modificación.')
INSERT [dbo].[Grupo] ([IdGrupo], [NombreMenu], [Descripcion]) VALUES (3, N'menuSocios', N'Permite gestionar la lista de socios del gimnasio, incluyendo su registro, modificación y eliminación, así como la consulta de sus datos y estado de membresía.')
SET IDENTITY_INSERT [dbo].[Grupo] OFF
GO
SET IDENTITY_INSERT [dbo].[Historial_Calentamiento] ON 

INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (1, 1, 1, 5)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (2, 2, 1, 5)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (3, 2, 3, 5)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (4, 3, 1, 5)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (5, 3, 3, 6)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (6, 4, 1, 5)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (7, 4, 3, 6)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (8, 5, 1, 5)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (9, 5, 3, 6)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (10, 5, 9, 1)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (11, 6, 3, 3)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (12, 7, 3, 3)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (13, 9, 1, 5)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (14, 9, 3, 6)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (15, 9, 9, 1)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (16, 10, 1, 5)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (17, 10, 3, 1)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (18, 10, 9, 5)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (19, 10, 10, 20)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (20, 11, 5, 10)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (21, 12, 5, 10)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (22, 13, 8, 1)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (23, 14, 5, 1)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (24, 15, 1, 5)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (25, 15, 3, 6)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (26, 15, 9, 1)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (27, 16, 10, 10)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (28, 17, 10, 10)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (29, 18, 8, 5)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (30, 19, 8, 5)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (31, 20, 8, 5)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (32, 21, 8, 5)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (33, 22, 8, 5)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (34, 22, 7, 3)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (35, 23, 1, 5)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (36, 23, 3, 6)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (37, 23, 9, 1)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (38, 23, 5, 5)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (39, 23, 11, 4)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (40, 24, 3, 3)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (41, 25, 10, 5)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (42, 26, 10, 5)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (43, 26, 8, 4)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (44, 26, 7, 4)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (45, 27, 7, 4)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (46, 27, 8, 4)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (47, 27, 10, 5)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (48, 28, 7, 3)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (49, 29, 7, 3)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (50, 30, 11, 10)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (51, 30, 12, 5)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (52, 31, 11, 10)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (53, 31, 12, 5)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (54, 32, 7, 5)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (55, 33, 7, 5)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (56, 34, 6, 2)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (57, 34, 9, 3)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (58, 35, 6, 2)
INSERT [dbo].[Historial_Calentamiento] ([IdHistorialCalentamiento], [IdHistorial], [IdCalentamiento], [Duracion]) VALUES (59, 35, 9, 3)
SET IDENTITY_INSERT [dbo].[Historial_Calentamiento] OFF
GO
SET IDENTITY_INSERT [dbo].[Historial_Entrenamiento] ON 

INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (1, 1, 21, 5, 25, 30)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (2, 2, 21, 5, 25, 60)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (3, 2, 8, 2, 15, 20)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (4, 3, 21, 5, 25, 60)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (5, 3, 8, 2, 15, 21)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (6, 4, 21, 5, 25, 60)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (7, 4, 8, 2, 15, 21)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (8, 5, 21, 5, 25, 60)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (9, 5, 8, 2, 15, 21)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (10, 7, 24, 5, 10, 10)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (11, 9, 21, 5, 25, 60)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (12, 9, 8, 2, 15, 21)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (13, 11, 9, 5, 16, 20)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (14, 12, 9, 6, 16, 20)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (15, 13, 8, 5, 15, 0)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (16, 14, 10, 4, 10, 100)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (17, 15, 21, 5, 25, 60)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (18, 15, 8, 2, 15, 21)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (19, 15, 28, 5, 0, 0)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (20, 16, 22, 5, 5, 60)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (21, 16, 3, 4, 6, 50)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (22, 17, 22, 5, 5, 60)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (23, 17, 3, 4, 6, 50)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (24, 17, 30, 5, 4, 10)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (25, 20, 8, 3, 1, 1)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (26, 21, 8, 3, 1, 1)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (27, 21, 7, 3, 3, 2)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (28, 22, 8, 3, 1, 1)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (29, 22, 7, 3, 3, 2)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (30, 23, 21, 5, 25, 60)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (31, 23, 8, 2, 15, 21)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (32, 23, 28, 5, 0, 5)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (33, 23, 30, 3, 4, 0)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (34, 23, 21, 5, 4, 60)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (35, 24, 24, 5, 10, 10)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (36, 25, 8, 5, 10, 0)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (37, 25, 22, 5, 12, 100)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (38, 26, 8, 5, 10, 0)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (39, 26, 22, 5, 12, 100)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (40, 27, 8, 5, 10, 0)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (41, 27, 22, 5, 12, 100)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (42, 27, 29, 2, 2, 50)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (43, 28, 4, 3, 2, 3)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (44, 29, 4, 3, 2, 3)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (45, 30, 8, 4, 3, 0)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (46, 31, 8, 4, 3, 0)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (47, 31, 28, 2, 3, 0)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (48, 32, 18, 2, 1, 1)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (49, 33, 18, 2, 1, 1)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (50, 33, 9, 1, 1, 1)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (51, 34, 6, 3, 2, 1)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (52, 35, 6, 3, 2, 1)
INSERT [dbo].[Historial_Entrenamiento] ([IdHistorialEntrenamiento], [IdHistorial], [IdElementoGimnasio], [Series], [Repeticiones], [Peso]) VALUES (53, 35, 7, 1, 1, 1)
SET IDENTITY_INSERT [dbo].[Historial_Entrenamiento] OFF
GO
SET IDENTITY_INSERT [dbo].[Historial_Estiramiento] ON 

INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (1, 1, 7, 2)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (2, 2, 7, 1)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (3, 2, 9, 1)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (4, 3, 7, 1)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (5, 3, 9, 2)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (6, 3, 8, 1)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (7, 4, 7, 1)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (8, 4, 8, 1)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (9, 4, 9, 2)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (10, 4, 4, 2)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (11, 5, 4, 2)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (12, 5, 7, 1)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (13, 5, 8, 1)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (14, 5, 9, 2)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (15, 8, 4, 2)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (16, 9, 4, 2)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (17, 9, 7, 1)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (18, 9, 8, 1)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (19, 9, 9, 2)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (20, 11, 9, 2)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (21, 12, 9, 2)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (22, 15, 4, 2)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (23, 15, 7, 1)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (24, 15, 8, 1)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (25, 15, 9, 2)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (26, 16, 7, 2)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (27, 17, 7, 2)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (28, 18, 4, 5)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (29, 19, 4, 5)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (30, 20, 4, 5)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (31, 21, 4, 5)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (32, 22, 4, 5)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (33, 23, 4, 2)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (34, 23, 7, 1)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (35, 23, 8, 1)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (36, 23, 9, 2)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (37, 24, 6, 2)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (38, 25, 9, 5)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (39, 25, 7, 3)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (40, 26, 7, 3)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (41, 26, 9, 5)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (42, 27, 7, 3)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (43, 27, 9, 5)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (44, 28, 5, 4)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (45, 29, 5, 4)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (46, 29, 3, 4)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (47, 30, 8, 2)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (48, 30, 10, 2)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (49, 30, 12, 1)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (50, 31, 8, 2)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (51, 31, 10, 2)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (52, 31, 12, 1)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (53, 32, 6, 4)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (54, 33, 6, 4)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (55, 34, 5, 3)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (56, 34, 5, 3)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (57, 35, 5, 3)
INSERT [dbo].[Historial_Estiramiento] ([IdHistorialEstiramiento], [IdHistorial], [IdEstiramiento], [Duracion]) VALUES (58, 35, 4, 3)
SET IDENTITY_INSERT [dbo].[Historial_Estiramiento] OFF
GO
SET IDENTITY_INSERT [dbo].[HistorialRutina] ON 

INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (1, 13, N'Miércoles', CAST(N'2025-04-01T22:29:52.767' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (2, 13, N'Miércoles', CAST(N'2025-04-01T22:35:17.570' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (3, 13, N'Miércoles', CAST(N'2025-04-02T14:33:04.143' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (4, 13, N'Miércoles', CAST(N'2025-04-02T14:40:09.413' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (5, 13, N'Miércoles', CAST(N'2025-04-02T14:40:48.403' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (6, 13, N'Lunes', CAST(N'2025-04-02T15:06:52.653' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (7, 13, N'Lunes', CAST(N'2025-04-02T15:34:33.430' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (8, 13, N'Miércoles', CAST(N'2025-04-02T18:14:19.220' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (9, 13, N'Miércoles', CAST(N'2025-04-02T18:15:04.403' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (10, 12, N'Viernes', CAST(N'2025-04-04T18:32:31.443' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (11, 10, N'Miércoles', CAST(N'2025-04-07T14:38:23.790' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (12, 10, N'Miércoles', CAST(N'2025-04-07T14:39:02.800' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (13, 12, N'Martes', CAST(N'2025-04-07T14:39:35.060' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (14, 9, N'Martes', CAST(N'2025-04-07T14:45:22.733' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (15, 13, N'Miércoles', CAST(N'2025-04-08T11:29:10.387' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (16, 11, N'Lunes', CAST(N'2025-07-16T20:40:25.517' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (17, 11, N'Lunes', CAST(N'2025-07-16T20:43:51.900' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (18, 11, N'Martes', CAST(N'2025-07-16T20:45:33.700' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (19, 11, N'Martes', CAST(N'2025-07-16T20:45:38.890' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (20, 11, N'Martes', CAST(N'2025-07-16T20:45:57.507' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (21, 11, N'Martes', CAST(N'2025-07-16T20:46:24.960' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (22, 11, N'Martes', CAST(N'2025-07-16T20:46:40.350' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (23, 13, N'Miércoles', CAST(N'2025-07-16T22:48:48.273' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (24, 13, N'Lunes', CAST(N'2025-07-17T17:59:20.843' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (25, 13, N'Viernes', CAST(N'2025-07-17T17:59:37.107' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (26, 13, N'Viernes', CAST(N'2025-07-17T17:59:53.410' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (27, 13, N'Viernes', CAST(N'2025-07-17T18:00:16.687' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (28, 16, N'Viernes', CAST(N'2025-07-18T18:22:26.107' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (29, 16, N'Viernes', CAST(N'2025-07-18T18:22:35.067' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (30, 11, N'Miércoles', CAST(N'2025-07-24T12:37:14.623' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (31, 11, N'Miércoles', CAST(N'2025-07-24T12:37:24.577' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (32, 21, N'Martes', CAST(N'2025-07-29T15:04:47.333' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (33, 21, N'Martes', CAST(N'2025-07-29T15:14:30.363' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (34, 21, N'Viernes', CAST(N'2025-07-29T16:43:38.010' AS DateTime))
INSERT [dbo].[HistorialRutina] ([IdHistorial], [IdSocio], [Dia], [FechaRegistro]) VALUES (35, 21, N'Viernes', CAST(N'2025-07-29T16:43:54.780' AS DateTime))
SET IDENTITY_INSERT [dbo].[HistorialRutina] OFF
GO
INSERT [dbo].[Maquina] ([IdElemento], [FechaFabricacion], [FechaCompra], [Precio], [Peso], [EsElectrica]) VALUES (11, CAST(N'2023-01-10' AS Date), CAST(N'2023-02-15' AS Date), 35000, 100, 0)
INSERT [dbo].[Maquina] ([IdElemento], [FechaFabricacion], [FechaCompra], [Precio], [Peso], [EsElectrica]) VALUES (12, CAST(N'2022-12-05' AS Date), CAST(N'2023-03-10' AS Date), 28000, 90, 0)
INSERT [dbo].[Maquina] ([IdElemento], [FechaFabricacion], [FechaCompra], [Precio], [Peso], [EsElectrica]) VALUES (13, CAST(N'2024-01-20' AS Date), CAST(N'2024-02-15' AS Date), 25000, 85, 1)
INSERT [dbo].[Maquina] ([IdElemento], [FechaFabricacion], [FechaCompra], [Precio], [Peso], [EsElectrica]) VALUES (14, CAST(N'2023-10-11' AS Date), CAST(N'2023-11-10' AS Date), 40000, 150, 1)
INSERT [dbo].[Maquina] ([IdElemento], [FechaFabricacion], [FechaCompra], [Precio], [Peso], [EsElectrica]) VALUES (15, CAST(N'2023-07-05' AS Date), CAST(N'2023-08-01' AS Date), 45000, 140, 1)
INSERT [dbo].[Maquina] ([IdElemento], [FechaFabricacion], [FechaCompra], [Precio], [Peso], [EsElectrica]) VALUES (16, CAST(N'2023-09-12' AS Date), CAST(N'2023-10-05' AS Date), 42000, 135, 1)
INSERT [dbo].[Maquina] ([IdElemento], [FechaFabricacion], [FechaCompra], [Precio], [Peso], [EsElectrica]) VALUES (17, CAST(N'2022-06-15' AS Date), CAST(N'2022-07-10' AS Date), 32000, 125, 0)
INSERT [dbo].[Maquina] ([IdElemento], [FechaFabricacion], [FechaCompra], [Precio], [Peso], [EsElectrica]) VALUES (18, CAST(N'2023-02-14' AS Date), CAST(N'2023-03-20' AS Date), 38000, 110, 0)
INSERT [dbo].[Maquina] ([IdElemento], [FechaFabricacion], [FechaCompra], [Precio], [Peso], [EsElectrica]) VALUES (19, CAST(N'2024-03-05' AS Date), CAST(N'2024-03-25' AS Date), 29000, 100, 0)
INSERT [dbo].[Maquina] ([IdElemento], [FechaFabricacion], [FechaCompra], [Precio], [Peso], [EsElectrica]) VALUES (20, CAST(N'2022-11-10' AS Date), CAST(N'2022-12-15' AS Date), 31000, 130, 0)
INSERT [dbo].[Maquina] ([IdElemento], [FechaFabricacion], [FechaCompra], [Precio], [Peso], [EsElectrica]) VALUES (31, CAST(N'2023-08-18' AS Date), CAST(N'2023-09-20' AS Date), 35000, 35000, 0)
GO
SET IDENTITY_INSERT [dbo].[Permiso] ON 

INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (4, 2, CAST(N'2025-03-01T01:55:26.467' AS DateTime), 3, NULL, NULL)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (5, 3, CAST(N'2025-03-01T01:55:26.467' AS DateTime), 2, NULL, NULL)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (201, NULL, CAST(N'2025-07-16T01:33:33.980' AS DateTime), NULL, 8, 9)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (202, NULL, CAST(N'2025-07-16T01:33:33.983' AS DateTime), NULL, 9, 9)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (203, NULL, CAST(N'2025-07-16T01:33:33.987' AS DateTime), NULL, 1, 9)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (204, NULL, CAST(N'2025-07-16T01:33:33.990' AS DateTime), NULL, 6, 9)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (205, NULL, CAST(N'2025-07-16T01:33:33.990' AS DateTime), NULL, 7, 9)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (206, NULL, CAST(N'2025-07-16T01:33:33.993' AS DateTime), NULL, 13, 9)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (232, 6, CAST(N'2025-07-16T04:19:38.320' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (233, 6, CAST(N'2025-07-16T04:19:38.320' AS DateTime), 2, NULL, NULL)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (234, 6, CAST(N'2025-07-16T04:19:38.320' AS DateTime), 3, NULL, NULL)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (273, NULL, CAST(N'2025-07-21T17:24:39.343' AS DateTime), NULL, 12, 10)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (274, NULL, CAST(N'2025-07-21T17:24:39.343' AS DateTime), NULL, 13, 10)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (275, NULL, CAST(N'2025-07-21T17:24:39.343' AS DateTime), NULL, 14, 10)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (276, NULL, CAST(N'2025-07-21T17:24:39.347' AS DateTime), NULL, 15, 10)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (280, 14, CAST(N'2025-07-21T19:56:05.460' AS DateTime), 1, 7, NULL)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (281, 14, CAST(N'2025-07-21T19:56:05.460' AS DateTime), 2, 13, NULL)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (282, 14, CAST(N'2025-07-21T19:56:05.460' AS DateTime), 3, 9, NULL)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (283, 14, CAST(N'2025-07-21T19:56:05.460' AS DateTime), 2, 15, NULL)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (288, 1, CAST(N'2025-07-27T15:22:50.197' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (289, 1, CAST(N'2025-07-27T15:22:50.197' AS DateTime), 2, NULL, NULL)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (290, 1, CAST(N'2025-07-27T15:22:50.197' AS DateTime), 3, NULL, NULL)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (348, NULL, CAST(N'2025-07-29T18:02:48.827' AS DateTime), NULL, 1, 19)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (354, 15, CAST(N'2025-07-29T18:08:55.653' AS DateTime), 1, 1, NULL)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (355, 15, CAST(N'2025-07-29T18:08:55.653' AS DateTime), 1, 2, NULL)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (356, 15, CAST(N'2025-07-29T18:08:55.653' AS DateTime), 2, 14, NULL)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (359, NULL, CAST(N'2025-07-29T18:14:07.890' AS DateTime), NULL, 1, 13)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (360, 19, CAST(N'2025-07-29T19:57:46.250' AS DateTime), NULL, 2, NULL)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (361, 19, CAST(N'2025-07-29T19:57:46.250' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[Permiso] ([IdPermiso], [IdRol], [FechaRegistro], [IdGrupo], [IdAccion], [IdUsuario]) VALUES (362, 19, CAST(N'2025-07-29T19:57:46.250' AS DateTime), NULL, 13, NULL)
SET IDENTITY_INSERT [dbo].[Permiso] OFF
GO
SET IDENTITY_INSERT [dbo].[RangoHorario] ON 

INSERT [dbo].[RangoHorario] ([IdRangoHorario], [HoraDesde], [HoraHasta], [Fecha], [CupoActual], [CupoMaximo], [Activo], [SoloSabado]) VALUES (1, CAST(N'00:00:00' AS Time), CAST(N'01:00:00' AS Time), CAST(N'2025-03-05' AS Date), 0, 27, 0, 0)
INSERT [dbo].[RangoHorario] ([IdRangoHorario], [HoraDesde], [HoraHasta], [Fecha], [CupoActual], [CupoMaximo], [Activo], [SoloSabado]) VALUES (2, CAST(N'01:00:00' AS Time), CAST(N'02:00:00' AS Time), CAST(N'2025-03-05' AS Date), 0, 5, 0, 0)
INSERT [dbo].[RangoHorario] ([IdRangoHorario], [HoraDesde], [HoraHasta], [Fecha], [CupoActual], [CupoMaximo], [Activo], [SoloSabado]) VALUES (3, CAST(N'02:00:00' AS Time), CAST(N'03:00:00' AS Time), CAST(N'2025-03-05' AS Date), 0, 5, 0, 0)
INSERT [dbo].[RangoHorario] ([IdRangoHorario], [HoraDesde], [HoraHasta], [Fecha], [CupoActual], [CupoMaximo], [Activo], [SoloSabado]) VALUES (4, CAST(N'03:00:00' AS Time), CAST(N'04:00:00' AS Time), CAST(N'2025-03-05' AS Date), 0, 5, 0, 0)
INSERT [dbo].[RangoHorario] ([IdRangoHorario], [HoraDesde], [HoraHasta], [Fecha], [CupoActual], [CupoMaximo], [Activo], [SoloSabado]) VALUES (5, CAST(N'04:00:00' AS Time), CAST(N'05:00:00' AS Time), CAST(N'2025-03-05' AS Date), 0, 5, 0, 0)
INSERT [dbo].[RangoHorario] ([IdRangoHorario], [HoraDesde], [HoraHasta], [Fecha], [CupoActual], [CupoMaximo], [Activo], [SoloSabado]) VALUES (6, CAST(N'05:00:00' AS Time), CAST(N'06:00:00' AS Time), CAST(N'2025-03-05' AS Date), 0, 5, 0, 0)
INSERT [dbo].[RangoHorario] ([IdRangoHorario], [HoraDesde], [HoraHasta], [Fecha], [CupoActual], [CupoMaximo], [Activo], [SoloSabado]) VALUES (7, CAST(N'06:00:00' AS Time), CAST(N'07:00:00' AS Time), CAST(N'2025-03-05' AS Date), 0, 5, 1, 0)
INSERT [dbo].[RangoHorario] ([IdRangoHorario], [HoraDesde], [HoraHasta], [Fecha], [CupoActual], [CupoMaximo], [Activo], [SoloSabado]) VALUES (8, CAST(N'07:00:00' AS Time), CAST(N'08:00:00' AS Time), CAST(N'2025-03-05' AS Date), 0, 5, 1, 0)
INSERT [dbo].[RangoHorario] ([IdRangoHorario], [HoraDesde], [HoraHasta], [Fecha], [CupoActual], [CupoMaximo], [Activo], [SoloSabado]) VALUES (9, CAST(N'08:00:00' AS Time), CAST(N'09:00:00' AS Time), CAST(N'2025-03-05' AS Date), 0, 5, 1, 1)
INSERT [dbo].[RangoHorario] ([IdRangoHorario], [HoraDesde], [HoraHasta], [Fecha], [CupoActual], [CupoMaximo], [Activo], [SoloSabado]) VALUES (10, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), CAST(N'2025-03-05' AS Date), 0, 5, 1, 1)
INSERT [dbo].[RangoHorario] ([IdRangoHorario], [HoraDesde], [HoraHasta], [Fecha], [CupoActual], [CupoMaximo], [Activo], [SoloSabado]) VALUES (11, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), CAST(N'2025-03-05' AS Date), 0, 5, 1, 1)
INSERT [dbo].[RangoHorario] ([IdRangoHorario], [HoraDesde], [HoraHasta], [Fecha], [CupoActual], [CupoMaximo], [Activo], [SoloSabado]) VALUES (12, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), CAST(N'2025-03-05' AS Date), 0, 5, 1, 1)
INSERT [dbo].[RangoHorario] ([IdRangoHorario], [HoraDesde], [HoraHasta], [Fecha], [CupoActual], [CupoMaximo], [Activo], [SoloSabado]) VALUES (13, CAST(N'12:00:00' AS Time), CAST(N'13:00:00' AS Time), CAST(N'2025-03-05' AS Date), 0, 5, 1, 0)
INSERT [dbo].[RangoHorario] ([IdRangoHorario], [HoraDesde], [HoraHasta], [Fecha], [CupoActual], [CupoMaximo], [Activo], [SoloSabado]) VALUES (14, CAST(N'13:00:00' AS Time), CAST(N'14:00:00' AS Time), CAST(N'2025-03-05' AS Date), 0, 5, 1, 0)
INSERT [dbo].[RangoHorario] ([IdRangoHorario], [HoraDesde], [HoraHasta], [Fecha], [CupoActual], [CupoMaximo], [Activo], [SoloSabado]) VALUES (15, CAST(N'14:00:00' AS Time), CAST(N'15:00:00' AS Time), CAST(N'2025-03-05' AS Date), 1, 5, 1, 0)
INSERT [dbo].[RangoHorario] ([IdRangoHorario], [HoraDesde], [HoraHasta], [Fecha], [CupoActual], [CupoMaximo], [Activo], [SoloSabado]) VALUES (16, CAST(N'15:00:00' AS Time), CAST(N'16:00:00' AS Time), CAST(N'2025-03-05' AS Date), 0, 5, 1, 0)
INSERT [dbo].[RangoHorario] ([IdRangoHorario], [HoraDesde], [HoraHasta], [Fecha], [CupoActual], [CupoMaximo], [Activo], [SoloSabado]) VALUES (17, CAST(N'16:00:00' AS Time), CAST(N'17:00:00' AS Time), CAST(N'2025-03-05' AS Date), 0, 37, 1, 0)
INSERT [dbo].[RangoHorario] ([IdRangoHorario], [HoraDesde], [HoraHasta], [Fecha], [CupoActual], [CupoMaximo], [Activo], [SoloSabado]) VALUES (18, CAST(N'17:00:00' AS Time), CAST(N'18:00:00' AS Time), CAST(N'2025-03-05' AS Date), 0, 10, 1, 0)
INSERT [dbo].[RangoHorario] ([IdRangoHorario], [HoraDesde], [HoraHasta], [Fecha], [CupoActual], [CupoMaximo], [Activo], [SoloSabado]) VALUES (19, CAST(N'18:00:00' AS Time), CAST(N'19:00:00' AS Time), CAST(N'2025-03-05' AS Date), 0, 7, 1, 0)
INSERT [dbo].[RangoHorario] ([IdRangoHorario], [HoraDesde], [HoraHasta], [Fecha], [CupoActual], [CupoMaximo], [Activo], [SoloSabado]) VALUES (20, CAST(N'19:00:00' AS Time), CAST(N'20:00:00' AS Time), CAST(N'2025-03-05' AS Date), 0, 10, 1, 0)
INSERT [dbo].[RangoHorario] ([IdRangoHorario], [HoraDesde], [HoraHasta], [Fecha], [CupoActual], [CupoMaximo], [Activo], [SoloSabado]) VALUES (21, CAST(N'20:00:00' AS Time), CAST(N'21:00:00' AS Time), CAST(N'2025-03-05' AS Date), 0, 5, 1, 0)
INSERT [dbo].[RangoHorario] ([IdRangoHorario], [HoraDesde], [HoraHasta], [Fecha], [CupoActual], [CupoMaximo], [Activo], [SoloSabado]) VALUES (22, CAST(N'21:00:00' AS Time), CAST(N'22:00:00' AS Time), CAST(N'2025-03-05' AS Date), 0, 5, 1, 0)
INSERT [dbo].[RangoHorario] ([IdRangoHorario], [HoraDesde], [HoraHasta], [Fecha], [CupoActual], [CupoMaximo], [Activo], [SoloSabado]) VALUES (23, CAST(N'22:00:00' AS Time), CAST(N'23:00:00' AS Time), CAST(N'2025-03-05' AS Date), 0, 5, 0, 0)
INSERT [dbo].[RangoHorario] ([IdRangoHorario], [HoraDesde], [HoraHasta], [Fecha], [CupoActual], [CupoMaximo], [Activo], [SoloSabado]) VALUES (24, CAST(N'23:00:00' AS Time), CAST(N'00:00:00' AS Time), CAST(N'2025-03-05' AS Date), 1, 5, 0, 0)
SET IDENTITY_INSERT [dbo].[RangoHorario] OFF
GO
INSERT [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario]) VALUES (1, 3)
INSERT [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario]) VALUES (1, 10)
INSERT [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario]) VALUES (2, 3)
INSERT [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario]) VALUES (11, 10)
INSERT [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario]) VALUES (12, 10)
INSERT [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario]) VALUES (13, 3)
INSERT [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario]) VALUES (14, 3)
INSERT [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario]) VALUES (15, 3)
INSERT [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario]) VALUES (15, 10)
INSERT [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario]) VALUES (16, 3)
INSERT [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario]) VALUES (16, 10)
INSERT [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario]) VALUES (17, 3)
INSERT [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario]) VALUES (17, 10)
INSERT [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario]) VALUES (18, 3)
INSERT [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario]) VALUES (18, 10)
INSERT [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario]) VALUES (19, 3)
INSERT [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario]) VALUES (20, 3)
INSERT [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario]) VALUES (20, 10)
INSERT [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario]) VALUES (21, 3)
INSERT [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario]) VALUES (22, 10)
INSERT [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario]) VALUES (23, 3)
INSERT [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario]) VALUES (23, 10)
INSERT [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario]) VALUES (24, 3)
INSERT [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario]) VALUES (24, 10)
GO
SET IDENTITY_INSERT [dbo].[Rol] ON 

INSERT [dbo].[Rol] ([IdRol], [Descripcion], [FechaRegistro]) VALUES (1, N'ADMIN', CAST(N'2025-02-28T16:38:06.013' AS DateTime))
INSERT [dbo].[Rol] ([IdRol], [Descripcion], [FechaRegistro]) VALUES (2, N'ASISTENTE', CAST(N'2025-02-28T16:38:06.013' AS DateTime))
INSERT [dbo].[Rol] ([IdRol], [Descripcion], [FechaRegistro]) VALUES (3, N'ENTRENADOR', CAST(N'2025-02-28T16:38:06.013' AS DateTime))
INSERT [dbo].[Rol] ([IdRol], [Descripcion], [FechaRegistro]) VALUES (6, N'DUEÑO', CAST(N'2025-03-13T16:39:17.287' AS DateTime))
INSERT [dbo].[Rol] ([IdRol], [Descripcion], [FechaRegistro]) VALUES (14, N'INVITADO', CAST(N'2025-07-16T02:08:30.140' AS DateTime))
INSERT [dbo].[Rol] ([IdRol], [Descripcion], [FechaRegistro]) VALUES (15, N'ESPIA', CAST(N'2025-07-27T14:39:59.587' AS DateTime))
INSERT [dbo].[Rol] ([IdRol], [Descripcion], [FechaRegistro]) VALUES (19, N'DEPORTOLOGO', CAST(N'2025-07-29T19:57:46.250' AS DateTime))
SET IDENTITY_INSERT [dbo].[Rol] OFF
GO
SET IDENTITY_INSERT [dbo].[Rutina] ON 

INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (1, 1, CAST(N'2025-07-29' AS Date), N'Lunes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (2, 1, CAST(N'2025-07-29' AS Date), N'Miércoles', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (3, 1, CAST(N'2025-07-29' AS Date), N'Viernes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (4, 1, CAST(N'2025-07-29' AS Date), N'Martes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (5, 8, CAST(N'2025-07-16' AS Date), N'Lunes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (6, 8, CAST(N'2025-07-16' AS Date), N'Miércoles', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (7, 8, CAST(N'2025-07-16' AS Date), N'Sábado', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (8, 9, CAST(N'2025-04-07' AS Date), N'Martes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (9, 9, CAST(N'2025-03-19' AS Date), N'Miércoles', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (10, 9, CAST(N'2025-03-19' AS Date), N'Viernes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (11, 9, CAST(N'2025-03-19' AS Date), N'Sábado', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (12, 10, CAST(N'2025-07-15' AS Date), N'Miércoles', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (13, 10, CAST(N'2025-07-15' AS Date), N'Viernes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (14, 10, CAST(N'2025-07-15' AS Date), N'Sábado', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (15, 11, CAST(N'2025-07-16' AS Date), N'Lunes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (16, 11, CAST(N'2025-07-16' AS Date), N'Martes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (17, 11, CAST(N'2025-07-24' AS Date), N'Miércoles', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (18, 11, CAST(N'2025-07-16' AS Date), N'Jueves', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (19, 11, CAST(N'2025-07-16' AS Date), N'Viernes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (20, 12, CAST(N'2025-07-15' AS Date), N'Martes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (21, 12, CAST(N'2025-07-15' AS Date), N'Miércoles', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (22, 12, CAST(N'2025-07-15' AS Date), N'Viernes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (23, 13, CAST(N'2025-07-19' AS Date), N'Lunes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (24, 13, CAST(N'2025-07-19' AS Date), N'Martes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (25, 13, CAST(N'2025-07-19' AS Date), N'Miércoles', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (26, 13, CAST(N'2025-03-31' AS Date), N'Jueves', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (27, 13, CAST(N'2025-07-19' AS Date), N'Viernes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (28, 13, CAST(N'2025-07-19' AS Date), N'Sábado', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (35, 1, CAST(N'2025-07-29' AS Date), N'Jueves', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (36, 1, CAST(N'2025-03-31' AS Date), N'Sábado', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (37, 8, CAST(N'2025-03-31' AS Date), N'Martes', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (38, 8, CAST(N'2025-03-31' AS Date), N'Jueves', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (39, 8, CAST(N'2025-03-31' AS Date), N'Viernes', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (40, 9, CAST(N'2025-03-31' AS Date), N'Lunes', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (41, 9, CAST(N'2025-03-31' AS Date), N'Jueves', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (42, 10, CAST(N'2025-07-15' AS Date), N'Lunes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (43, 10, CAST(N'2025-03-31' AS Date), N'Martes', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (44, 10, CAST(N'2025-03-31' AS Date), N'Jueves', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (45, 11, CAST(N'2025-03-31' AS Date), N'Sábado', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (46, 12, CAST(N'2025-03-31' AS Date), N'Lunes', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (47, 12, CAST(N'2025-03-31' AS Date), N'Jueves', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (48, 12, CAST(N'2025-03-31' AS Date), N'Sábado', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (49, 16, CAST(N'2025-07-28' AS Date), N'Lunes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (50, 16, CAST(N'2025-07-28' AS Date), N'Martes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (51, 16, CAST(N'2025-07-16' AS Date), N'Miércoles', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (52, 16, CAST(N'2025-07-28' AS Date), N'Jueves', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (53, 16, CAST(N'2025-07-28' AS Date), N'Viernes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (54, 16, CAST(N'2025-07-28' AS Date), N'Sábado', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (55, 20, CAST(N'2025-07-21' AS Date), N'Lunes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (56, 20, CAST(N'2025-07-21' AS Date), N'Martes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (57, 20, CAST(N'2025-07-19' AS Date), N'Miércoles', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (58, 20, CAST(N'2025-07-19' AS Date), N'Jueves', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (59, 20, CAST(N'2025-07-21' AS Date), N'Viernes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (60, 20, CAST(N'2025-07-19' AS Date), N'Sábado', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (61, 14, CAST(N'2025-07-19' AS Date), N'Lunes', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (62, 14, CAST(N'2025-07-29' AS Date), N'Martes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (63, 14, CAST(N'2025-07-19' AS Date), N'Miércoles', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (64, 14, CAST(N'2025-07-29' AS Date), N'Jueves', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (65, 14, CAST(N'2025-07-19' AS Date), N'Viernes', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (66, 14, CAST(N'2025-07-19' AS Date), N'Sábado', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (67, 19, CAST(N'2025-07-19' AS Date), N'Lunes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (68, 19, CAST(N'2025-07-19' AS Date), N'Martes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (69, 19, CAST(N'2025-07-19' AS Date), N'Miércoles', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (70, 19, CAST(N'2025-07-19' AS Date), N'Jueves', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (71, 19, CAST(N'2025-07-19' AS Date), N'Viernes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (72, 19, CAST(N'2025-07-19' AS Date), N'Sábado', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (73, 21, CAST(N'2025-07-29' AS Date), N'Lunes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (74, 21, CAST(N'2025-07-29' AS Date), N'Martes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (75, 21, CAST(N'2025-07-29' AS Date), N'Miércoles', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (76, 21, CAST(N'2025-07-29' AS Date), N'Jueves', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (77, 21, CAST(N'2025-07-29' AS Date), N'Viernes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (78, 21, CAST(N'2025-07-28' AS Date), N'Sábado', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (79, 22, CAST(N'2025-07-29' AS Date), N'Lunes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (80, 22, CAST(N'2025-07-29' AS Date), N'Martes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (81, 22, CAST(N'2025-07-29' AS Date), N'Miércoles', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (82, 22, CAST(N'2025-07-29' AS Date), N'Sábado', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (87, 24, CAST(N'2025-07-29' AS Date), N'Lunes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (88, 24, CAST(N'2025-07-29' AS Date), N'Martes', 1)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (89, 24, CAST(N'2025-07-29' AS Date), N'Miércoles', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (90, 24, CAST(N'2025-07-29' AS Date), N'Sábado', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (91, 22, CAST(N'2025-07-29' AS Date), N'Jueves', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (92, 22, CAST(N'2025-07-29' AS Date), N'Viernes', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (93, 24, CAST(N'2025-07-29' AS Date), N'Jueves', 0)
INSERT [dbo].[Rutina] ([IdRutina], [IdSocio], [FechaModificacion], [Dia], [Activa]) VALUES (94, 24, CAST(N'2025-07-29' AS Date), N'Viernes', 1)
SET IDENTITY_INSERT [dbo].[Rutina] OFF
GO
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (8, 5, 1)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (12, 5, 10)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (13, 1, 10)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (13, 5, 7)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (14, 6, 10)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (15, 10, 10)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (16, 7, 3)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (16, 8, 5)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (17, 11, 10)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (17, 12, 5)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (19, 9, 1)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (19, 10, 10)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (20, 8, 1)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (21, 9, 20)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (22, 1, 5)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (22, 3, 1)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (22, 9, 5)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (22, 10, 20)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (23, 3, 3)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (25, 1, 5)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (25, 3, 6)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (25, 5, 5)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (25, 9, 1)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (25, 11, 4)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (27, 7, 4)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (27, 8, 4)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (27, 10, 5)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (53, 7, 3)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (74, 7, 5)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (77, 6, 2)
INSERT [dbo].[Rutina_Calentamiento] ([IdRutina], [IdCalentamiento], [Duracion]) VALUES (77, 9, 3)
GO
INSERT [dbo].[Rutina_Estiramiento] ([IdRutina], [IdEstiramiento], [Duracion]) VALUES (12, 9, 2)
INSERT [dbo].[Rutina_Estiramiento] ([IdRutina], [IdEstiramiento], [Duracion]) VALUES (13, 3, 7)
INSERT [dbo].[Rutina_Estiramiento] ([IdRutina], [IdEstiramiento], [Duracion]) VALUES (14, 2, 10)
INSERT [dbo].[Rutina_Estiramiento] ([IdRutina], [IdEstiramiento], [Duracion]) VALUES (15, 7, 2)
INSERT [dbo].[Rutina_Estiramiento] ([IdRutina], [IdEstiramiento], [Duracion]) VALUES (16, 4, 5)
INSERT [dbo].[Rutina_Estiramiento] ([IdRutina], [IdEstiramiento], [Duracion]) VALUES (17, 8, 2)
INSERT [dbo].[Rutina_Estiramiento] ([IdRutina], [IdEstiramiento], [Duracion]) VALUES (17, 10, 2)
INSERT [dbo].[Rutina_Estiramiento] ([IdRutina], [IdEstiramiento], [Duracion]) VALUES (17, 12, 1)
INSERT [dbo].[Rutina_Estiramiento] ([IdRutina], [IdEstiramiento], [Duracion]) VALUES (19, 8, 10)
INSERT [dbo].[Rutina_Estiramiento] ([IdRutina], [IdEstiramiento], [Duracion]) VALUES (21, 4, 1)
INSERT [dbo].[Rutina_Estiramiento] ([IdRutina], [IdEstiramiento], [Duracion]) VALUES (23, 6, 2)
INSERT [dbo].[Rutina_Estiramiento] ([IdRutina], [IdEstiramiento], [Duracion]) VALUES (25, 4, 2)
INSERT [dbo].[Rutina_Estiramiento] ([IdRutina], [IdEstiramiento], [Duracion]) VALUES (25, 7, 1)
INSERT [dbo].[Rutina_Estiramiento] ([IdRutina], [IdEstiramiento], [Duracion]) VALUES (25, 8, 1)
INSERT [dbo].[Rutina_Estiramiento] ([IdRutina], [IdEstiramiento], [Duracion]) VALUES (25, 9, 2)
INSERT [dbo].[Rutina_Estiramiento] ([IdRutina], [IdEstiramiento], [Duracion]) VALUES (27, 7, 3)
INSERT [dbo].[Rutina_Estiramiento] ([IdRutina], [IdEstiramiento], [Duracion]) VALUES (27, 9, 5)
INSERT [dbo].[Rutina_Estiramiento] ([IdRutina], [IdEstiramiento], [Duracion]) VALUES (53, 3, 4)
INSERT [dbo].[Rutina_Estiramiento] ([IdRutina], [IdEstiramiento], [Duracion]) VALUES (53, 5, 4)
INSERT [dbo].[Rutina_Estiramiento] ([IdRutina], [IdEstiramiento], [Duracion]) VALUES (74, 6, 4)
INSERT [dbo].[Rutina_Estiramiento] ([IdRutina], [IdEstiramiento], [Duracion]) VALUES (77, 4, 3)
INSERT [dbo].[Rutina_Estiramiento] ([IdRutina], [IdEstiramiento], [Duracion]) VALUES (77, 5, 3)
GO
SET IDENTITY_INSERT [dbo].[Socio] ON 

INSERT [dbo].[Socio] ([IdSocio], [NombreYApellido], [FechaNacimiento], [Genero], [NroDocumento], [Ciudad], [Direccion], [Telefono], [Email], [ObraSocial], [Plan], [EstadoSocio], [FechaInicioActividades], [FechaFinActividades], [FechaNotificacion], [RespuestaNotificacion]) VALUES (1, N'Camila Socia', CAST(N'2006-07-01' AS Date), N'Femenino', 70958602, N'Rosario', N'Mitre 386', N'341586842', N'CamilaSocia@gmail.com', N'OSDE', N'Mensual', N'Eliminado', CAST(N'2025-03-06' AS Date), CAST(N'2025-06-06' AS Date), CAST(N'2025-07-05' AS Date), NULL)
INSERT [dbo].[Socio] ([IdSocio], [NombreYApellido], [FechaNacimiento], [Genero], [NroDocumento], [Ciudad], [Direccion], [Telefono], [Email], [ObraSocial], [Plan], [EstadoSocio], [FechaInicioActividades], [FechaFinActividades], [FechaNotificacion], [RespuestaNotificacion]) VALUES (8, N'Roro Pirrorro', CAST(N'1988-12-27' AS Date), N'Masculino', 25968503, N'San Invencion', N'Fantasia 131', N'0245145764', N'Roro_Pirrorro@gmail.com', N'OSDE', N'Mensual', N'Eliminado', CAST(N'2025-03-19' AS Date), CAST(N'2025-04-18' AS Date), CAST(N'2025-04-17' AS Date), NULL)
INSERT [dbo].[Socio] ([IdSocio], [NombreYApellido], [FechaNacimiento], [Genero], [NroDocumento], [Ciudad], [Direccion], [Telefono], [Email], [ObraSocial], [Plan], [EstadoSocio], [FechaInicioActividades], [FechaFinActividades], [FechaNotificacion], [RespuestaNotificacion]) VALUES (9, N'Liliana Aire', CAST(N'1988-12-27' AS Date), N'Femenino', 15696103, N'San Ventilador', N'Aires 565', N'0425485966', N'Liliana88@hotmail.com', N'Galeno', N'Anual', N'Actualizado', CAST(N'2025-03-19' AS Date), CAST(N'2026-03-19' AS Date), CAST(N'2026-03-18' AS Date), NULL)
INSERT [dbo].[Socio] ([IdSocio], [NombreYApellido], [FechaNacimiento], [Genero], [NroDocumento], [Ciudad], [Direccion], [Telefono], [Email], [ObraSocial], [Plan], [EstadoSocio], [FechaInicioActividades], [FechaFinActividades], [FechaNotificacion], [RespuestaNotificacion]) VALUES (10, N'Nacho Harinas', CAST(N'2000-03-03' AS Date), N'Masculino', 55686103, N'Moreno', N'Juan Domingo Perón 666', N'0254869542', N'NachoHarinasRC@gmail.com', N'Medifé', N'Mensual', N'Actualizado', CAST(N'2025-03-19' AS Date), CAST(N'2025-08-14' AS Date), CAST(N'2025-09-12' AS Date), NULL)
INSERT [dbo].[Socio] ([IdSocio], [NombreYApellido], [FechaNacimiento], [Genero], [NroDocumento], [Ciudad], [Direccion], [Telefono], [Email], [ObraSocial], [Plan], [EstadoSocio], [FechaInicioActividades], [FechaFinActividades], [FechaNotificacion], [RespuestaNotificacion]) VALUES (11, N'Myriam Izquierdoz', CAST(N'2008-12-30' AS Date), N'Femenino', 39565103, N'Rosario', N'Mendoza 501', N'03411568425', N'MyriamIzquierdoz96@gmail.com', N'MEDICUS', N'Anual', N'Actualizado', CAST(N'2025-03-19' AS Date), CAST(N'2026-03-19' AS Date), CAST(N'2026-03-18' AS Date), NULL)
INSERT [dbo].[Socio] ([IdSocio], [NombreYApellido], [FechaNacimiento], [Genero], [NroDocumento], [Ciudad], [Direccion], [Telefono], [Email], [ObraSocial], [Plan], [EstadoSocio], [FechaInicioActividades], [FechaFinActividades], [FechaNotificacion], [RespuestaNotificacion]) VALUES (12, N'Gabriel Hostel', CAST(N'1998-12-29' AS Date), N'Masculino', 26953102, N'Rosario', N'Sarmiento 1243', N'0341648695', N'GabrielHostel@gmail.com', N'Swiss Medical', N'Mensual', N'Eliminado', CAST(N'2025-03-19' AS Date), CAST(N'2025-04-18' AS Date), CAST(N'2025-04-17' AS Date), NULL)
INSERT [dbo].[Socio] ([IdSocio], [NombreYApellido], [FechaNacimiento], [Genero], [NroDocumento], [Ciudad], [Direccion], [Telefono], [Email], [ObraSocial], [Plan], [EstadoSocio], [FechaInicioActividades], [FechaFinActividades], [FechaNotificacion], [RespuestaNotificacion]) VALUES (13, N'Alejandro Fantino', CAST(N'1961-07-12' AS Date), N'Masculino', 10021560, N'Rosario', N'Neura 321', N'0341695241', N'Alejandro_Fantino@gmail.com', N'OSDE', N'Mensual', N'Actualizado', CAST(N'2025-03-31' AS Date), CAST(N'2025-08-15' AS Date), CAST(N'2025-09-13' AS Date), NULL)
INSERT [dbo].[Socio] ([IdSocio], [NombreYApellido], [FechaNacimiento], [Genero], [NroDocumento], [Ciudad], [Direccion], [Telefono], [Email], [ObraSocial], [Plan], [EstadoSocio], [FechaInicioActividades], [FechaFinActividades], [FechaNotificacion], [RespuestaNotificacion]) VALUES (14, N'Alejandro Angelini', CAST(N'1996-12-18' AS Date), N'Masculino', 30987654, N'Rosario', N'Laprida 111', N'3415550011', N'nico.a.didomenico@gmail.com', N'OSDE', N'Mensual', N'Actualizado', CAST(N'2025-05-16' AS Date), CAST(N'2025-10-16' AS Date), CAST(N'2025-11-14' AS Date), NULL)
INSERT [dbo].[Socio] ([IdSocio], [NombreYApellido], [FechaNacimiento], [Genero], [NroDocumento], [Ciudad], [Direccion], [Telefono], [Email], [ObraSocial], [Plan], [EstadoSocio], [FechaInicioActividades], [FechaFinActividades], [FechaNotificacion], [RespuestaNotificacion]) VALUES (16, N'Don Brito', CAST(N'1988-12-26' AS Date), N'Masculino', 32656402, N'Rosario', N'Sarmiento 1600', N'341265320', N'DonBrito@gmail.com', N'Galeno', N'Anual', N'Actualizado', CAST(N'2025-07-16' AS Date), CAST(N'2026-07-16' AS Date), CAST(N'2026-07-15' AS Date), NULL)
INSERT [dbo].[Socio] ([IdSocio], [NombreYApellido], [FechaNacimiento], [Genero], [NroDocumento], [Ciudad], [Direccion], [Telefono], [Email], [ObraSocial], [Plan], [EstadoSocio], [FechaInicioActividades], [FechaFinActividades], [FechaNotificacion], [RespuestaNotificacion]) VALUES (19, N'Lucas Biagini', CAST(N'1990-05-20' AS Date), N'Masculino', 32987654, N'Rosario', N'Laprida 111', N'3415550011', N'lucasbiagini@gmail.com', N'OSDE', N'Mensual', N'Actualizado', CAST(N'2025-05-16' AS Date), CAST(N'2025-08-18' AS Date), CAST(N'2025-08-17' AS Date), NULL)
INSERT [dbo].[Socio] ([IdSocio], [NombreYApellido], [FechaNacimiento], [Genero], [NroDocumento], [Ciudad], [Direccion], [Telefono], [Email], [ObraSocial], [Plan], [EstadoSocio], [FechaInicioActividades], [FechaFinActividades], [FechaNotificacion], [RespuestaNotificacion]) VALUES (20, N'Daniel Dutch', CAST(N'1990-07-19' AS Date), N'Masculino', 256965035, N'Algunlugarlandia', N'Queti 215', N'3456423156', N'sasractorsas@gmail.com', N'OSDE', N'Mensual', N'Nuevo', CAST(N'2025-07-19' AS Date), CAST(N'2025-08-18' AS Date), CAST(N'2025-08-17' AS Date), NULL)
INSERT [dbo].[Socio] ([IdSocio], [NombreYApellido], [FechaNacimiento], [Genero], [NroDocumento], [Ciudad], [Direccion], [Telefono], [Email], [ObraSocial], [Plan], [EstadoSocio], [FechaInicioActividades], [FechaFinActividades], [FechaNotificacion], [RespuestaNotificacion]) VALUES (21, N'Daniela Gold', CAST(N'2001-07-28' AS Date), N'Femenino', 51636032, N'Rafaela', N'Saavedra 472', N'341458402', N'Daniela_Gold@gmail.com', N'OSDE', N'Mensual', N'Actualizado', CAST(N'2025-07-28' AS Date), CAST(N'2025-08-27' AS Date), CAST(N'2025-08-26' AS Date), NULL)
INSERT [dbo].[Socio] ([IdSocio], [NombreYApellido], [FechaNacimiento], [Genero], [NroDocumento], [Ciudad], [Direccion], [Telefono], [Email], [ObraSocial], [Plan], [EstadoSocio], [FechaInicioActividades], [FechaFinActividades], [FechaNotificacion], [RespuestaNotificacion]) VALUES (22, N'Juan Pérez', CAST(N'1990-05-10' AS Date), N'Masculino', 12345678, N'Rosario', N'Calle Falsa 123', N'3415551234', N'juan.perez@email.com', N'OSDE', N'Mensual', N'Suspendido', CAST(N'2025-06-26' AS Date), CAST(N'2025-07-26' AS Date), NULL, 0)
INSERT [dbo].[Socio] ([IdSocio], [NombreYApellido], [FechaNacimiento], [Genero], [NroDocumento], [Ciudad], [Direccion], [Telefono], [Email], [ObraSocial], [Plan], [EstadoSocio], [FechaInicioActividades], [FechaFinActividades], [FechaNotificacion], [RespuestaNotificacion]) VALUES (24, N'María González', CAST(N'1992-07-22' AS Date), N'Femenino', 66666666, N'Rosario', N'San Martín 350', N'3415559876', N'maria.gonzalez@email.com', N'OSDE', N'Mensual', N'Eliminado', CAST(N'2025-05-19' AS Date), CAST(N'2025-06-19' AS Date), NULL, 0)
SET IDENTITY_INSERT [dbo].[Socio] OFF
GO
SET IDENTITY_INSERT [dbo].[Turno] ON 

INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (1, 15, 3, 1, CAST(N'2025-03-12' AS Date), N'Cancelado', N'2LSE')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (2, 1, 3, 1, CAST(N'2025-03-11' AS Date), N'Finalizado', N'7A1K')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (10, 15, 3, 1, CAST(N'2025-03-18' AS Date), N'Cancelado', N'JU7X')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (14, 20, 3, 1, CAST(N'2025-03-16' AS Date), N'Cancelado', N'IV9K')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (27, 19, 3, 1, CAST(N'2025-03-19' AS Date), N'Cancelado', N'KCO9')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (28, 20, 3, 10, CAST(N'2025-03-19' AS Date), N'Cancelado', N'5YHI')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (29, 15, 10, 1, CAST(N'2025-03-22' AS Date), N'Cancelado', N'G4DU')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (30, 18, 3, 8, CAST(N'2025-03-25' AS Date), N'Cancelado', N'C042')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (31, 20, 10, 1, CAST(N'2025-03-25' AS Date), N'Cancelado', N'00NS')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (32, 17, 10, 1, CAST(N'2025-03-27' AS Date), N'Cancelado', N'HUXO')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (33, 16, 3, 8, CAST(N'2025-03-27' AS Date), N'Cancelado', N'DE0R')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (34, 18, 10, 9, CAST(N'2025-03-27' AS Date), N'Cancelado', N'TQHL')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (35, 19, 3, 10, CAST(N'2025-03-27' AS Date), N'Cancelado', N'ZGZL')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (38, 22, 10, 12, CAST(N'2025-03-27' AS Date), N'Cancelado', N'XHW5')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (39, 23, 10, 11, CAST(N'2025-03-27' AS Date), N'Cancelado', N'IMOV')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (41, 22, 10, 9, CAST(N'2025-03-29' AS Date), N'Cancelado', N'JJ06')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (42, 22, 10, 12, CAST(N'2025-03-29' AS Date), N'Cancelado', N'JWO6')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (43, 23, 10, 11, CAST(N'2025-03-29' AS Date), N'Cancelado', N'2QP4')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (44, 24, 10, 10, CAST(N'2025-03-29' AS Date), N'Cancelado', N'TA44')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (45, 14, 3, 1, CAST(N'2025-03-31' AS Date), N'Cancelado', N'ZB3H')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (46, 15, 10, 8, CAST(N'2025-03-31' AS Date), N'Cancelado', N'PAAS')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (52, 20, 3, 13, CAST(N'2025-03-31' AS Date), N'Cancelado', N'1RGM')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (53, 20, 10, 12, CAST(N'2025-03-31' AS Date), N'Cancelado', N'55AM')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (54, 20, 3, 10, CAST(N'2025-03-31' AS Date), N'Cancelado', N'H3QI')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (55, 20, 3, 9, CAST(N'2025-03-31' AS Date), N'Cancelado', N'WG8T')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (56, 21, 3, 11, CAST(N'2025-03-31' AS Date), N'Cancelado', N'SRTN')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (58, 23, 10, 13, CAST(N'2025-04-01' AS Date), N'Cancelado', N'KQG3')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (68, 20, 10, 13, CAST(N'2025-04-02' AS Date), N'Cancelado', N'HGAS')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (69, 18, 3, 13, CAST(N'2025-04-04' AS Date), N'Cancelado', N'LR0U')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (70, 19, 3, 12, CAST(N'2025-04-04' AS Date), N'Cancelado', N'M6DK')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (72, 12, 10, 13, CAST(N'2025-04-05' AS Date), N'Cancelado', N'EXA3')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (73, 13, 3, 13, CAST(N'2025-04-07' AS Date), N'Finalizado', N'RXT1')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (77, 17, 10, 8, CAST(N'2025-04-07' AS Date), N'Cancelado', N'RKA9')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (86, 15, 3, 1, CAST(N'2025-04-07' AS Date), N'Cancelado', N'U4T7')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (87, 15, 3, 9, CAST(N'2025-04-07' AS Date), N'Cancelado', N'X30C')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (88, 15, 3, 10, CAST(N'2025-04-07' AS Date), N'Cancelado', N'G63X')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (89, 15, 3, 11, CAST(N'2025-04-07' AS Date), N'Cancelado', N'Q2ZN')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (91, 20, 10, 12, CAST(N'2025-04-07' AS Date), N'Cancelado', N'IL0V')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (92, 12, 10, 13, CAST(N'2025-04-08' AS Date), N'Finalizado', N'4R7E')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (93, 12, 10, 12, CAST(N'2025-04-08' AS Date), N'Cancelado', N'BWEJ')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (94, 19, 3, 9, CAST(N'2025-07-14' AS Date), N'Cancelado', N'9U8I')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (95, 11, 10, 9, CAST(N'2025-07-16' AS Date), N'Cancelado', N'A67J')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (97, 16, 3, 10, CAST(N'2025-07-16' AS Date), N'Cancelado', N'W6JY')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (99, 21, 3, 11, CAST(N'2025-07-16' AS Date), N'Cancelado', N'7L5E')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (109, 19, 3, 16, CAST(N'2025-07-18' AS Date), N'Cancelado', N'KNKS')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (112, 21, 3, 13, CAST(N'2025-07-21' AS Date), N'Cancelado', N'YZZI')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (114, 21, 3, 13, CAST(N'2025-07-23' AS Date), N'Cancelado', N'UY6H')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (115, 22, 10, 13, CAST(N'2025-07-28' AS Date), N'Cancelado', N'E8GL')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (118, 18, 10, 19, CAST(N'2025-07-21' AS Date), N'Cancelado', N'Q34T')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (119, 20, 3, 11, CAST(N'2025-07-21' AS Date), N'Cancelado', N'WTVS')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (120, 13, 3, 11, CAST(N'2025-07-24' AS Date), N'Cancelado', N'97IL')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (121, 13, 3, 10, CAST(N'2025-07-28' AS Date), N'Cancelado', N'81N8')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (123, 22, 10, 16, CAST(N'2025-07-28' AS Date), N'Cancelado', N'F2S1')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (126, 16, 3, 19, CAST(N'2025-07-29' AS Date), N'En Curso', N'TUSU')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (131, 19, 3, 21, CAST(N'2025-07-29' AS Date), N'En Curso', N'0FLG')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (132, 20, 10, 16, CAST(N'2025-07-29' AS Date), N'En Curso', N'AN1I')
INSERT [dbo].[Turno] ([IdTurno], [IdRangoHorario], [IdUsuario], [IdSocio], [FechaTurno], [EstadoTurno], [CodigoIngreso]) VALUES (133, 20, 3, 13, CAST(N'2025-07-29' AS Date), N'En Curso', N'R1YX')
SET IDENTITY_INSERT [dbo].[Turno] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([IdUsuario], [NombreYApellido], [Email], [Telefono], [Direccion], [Ciudad], [NroDocumento], [Genero], [FechaNacimiento], [NombreUsuario], [Clave], [IdRol], [Estado], [FechaRegistro]) VALUES (1, N'Nico Di Domenico', N'nicolasdidomenico1996@gmail.com', N'03476649542', N'Av. Principal 1234', N'Ciudad Ejemplo', 39686403, N'Masculino', CAST(N'1980-05-16T08:30:00.000' AS DateTime), N'adm', N'123', 1, 1, CAST(N'2025-02-28T16:40:42.207' AS DateTime))
INSERT [dbo].[Usuario] ([IdUsuario], [NombreYApellido], [Email], [Telefono], [Direccion], [Ciudad], [NroDocumento], [Genero], [FechaNacimiento], [NombreUsuario], [Clave], [IdRol], [Estado], [FechaRegistro]) VALUES (2, N'Juan Pérez', N'juan.perez@example.com', N'987654321', N'Calle Secundaria 456', N'Ciudad Ejemplo', 87654321, N'Masculino', CAST(N'1990-08-20T14:45:00.000' AS DateTime), N'asi', N'123', 2, 1, CAST(N'2025-02-28T16:40:42.207' AS DateTime))
INSERT [dbo].[Usuario] ([IdUsuario], [NombreYApellido], [Email], [Telefono], [Direccion], [Ciudad], [NroDocumento], [Genero], [FechaNacimiento], [NombreUsuario], [Clave], [IdRol], [Estado], [FechaRegistro]) VALUES (3, N'María López Spinning', N'maria.lopez@example.com', N'555111222', N'Avenida Fitness 789', N'Ciudad Deportiva', 13579246, N'Femenino', CAST(N'1985-12-10T19:15:00.000' AS DateTime), N'ent', N'123', 3, 1, CAST(N'2025-02-28T16:40:42.207' AS DateTime))
INSERT [dbo].[Usuario] ([IdUsuario], [NombreYApellido], [Email], [Telefono], [Direccion], [Ciudad], [NroDocumento], [Genero], [FechaNacimiento], [NombreUsuario], [Clave], [IdRol], [Estado], [FechaRegistro]) VALUES (9, N'Roy Action', N'RoyAction@gmail.com', N'123456789', N'asd 123', N'asd', 123456, N'Masculino', CAST(N'2044-06-07T00:00:00.000' AS DateTime), N'acc', N'123', NULL, 1, CAST(N'2025-03-14T21:02:40.327' AS DateTime))
INSERT [dbo].[Usuario] ([IdUsuario], [NombreYApellido], [Email], [Telefono], [Direccion], [Ciudad], [NroDocumento], [Genero], [FechaNacimiento], [NombreUsuario], [Clave], [IdRol], [Estado], [FechaRegistro]) VALUES (10, N'Emilio Mancuerna', N'gym@gmail.com', N'15615152', N'efwef 123', N'we errewrep', 421565132, N'Masculino', CAST(N'1993-05-03T19:44:53.000' AS DateTime), N'ent2', N'123', 3, 1, CAST(N'2025-03-15T19:46:15.553' AS DateTime))
INSERT [dbo].[Usuario] ([IdUsuario], [NombreYApellido], [Email], [Telefono], [Direccion], [Ciudad], [NroDocumento], [Genero], [FechaNacimiento], [NombreUsuario], [Clave], [IdRol], [Estado], [FechaRegistro]) VALUES (13, N'Juan Action', N'JuanAction@gmail.com', N'123478910', N'asd 123', N'asd', 1561461, N'Masculino', CAST(N'2044-06-07T00:00:00.000' AS DateTime), N'acc2', N'123', NULL, 1, CAST(N'2025-04-04T20:15:48.310' AS DateTime))
INSERT [dbo].[Usuario] ([IdUsuario], [NombreYApellido], [Email], [Telefono], [Direccion], [Ciudad], [NroDocumento], [Genero], [FechaNacimiento], [NombreUsuario], [Clave], [IdRol], [Estado], [FechaRegistro]) VALUES (14, N'Jesus Rolaction', N'Jesus_Rolaction@gmail.com', N'3456465436', N'asdasd 213', N'San Inventon', 25696402, N'Masculino', CAST(N'1990-01-31T21:49:47.000' AS DateTime), N'due', N'123', 2, 1, CAST(N'2025-07-15T21:51:35.710' AS DateTime))
INSERT [dbo].[Usuario] ([IdUsuario], [NombreYApellido], [Email], [Telefono], [Direccion], [Ciudad], [NroDocumento], [Genero], [FechaNacimiento], [NombreUsuario], [Clave], [IdRol], [Estado], [FechaRegistro]) VALUES (16, N'Bruno Inv', N'Bruno_Inv@gmail.com', N'2341548623', N'sfdsdf 123', N'San Lorenzo', 26965302, N'Masculino', CAST(N'1988-12-26T17:03:55.000' AS DateTime), N'inv', N'123', 14, 1, CAST(N'2025-07-21T17:17:17.717' AS DateTime))
INSERT [dbo].[Usuario] ([IdUsuario], [NombreYApellido], [Email], [Telefono], [Direccion], [Ciudad], [NroDocumento], [Genero], [FechaNacimiento], [NombreUsuario], [Clave], [IdRol], [Estado], [FechaRegistro]) VALUES (18, N'Coco Spy', N'saasdadasdasd@gmail.com', N'213132136', N'asdsad 123', N'asdasdasd', 25696503, N'Masculino', CAST(N'2000-07-27T14:40:04.000' AS DateTime), N'esp', N'123', 14, 1, CAST(N'2025-07-27T14:41:48.190' AS DateTime))
INSERT [dbo].[Usuario] ([IdUsuario], [NombreYApellido], [Email], [Telefono], [Direccion], [Ciudad], [NroDocumento], [Genero], [FechaNacimiento], [NombreUsuario], [Clave], [IdRol], [Estado], [FechaRegistro]) VALUES (19, N'Pepe Multirol', N'Pepe_Multirol@gmail.com', N'5146546542', N'Asdsdas', N'asdasdasd', 25965203, N'Masculino', CAST(N'2025-07-27T17:59:37.000' AS DateTime), N'all', N'123', 19, 1, CAST(N'2025-07-27T18:00:56.423' AS DateTime))
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Grupo__BC363B3452E62629]    Script Date: 31/7/2025 13:19:22 ******/
ALTER TABLE [dbo].[Grupo] ADD UNIQUE NONCLUSTERED 
(
	[NombreMenu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__Socio__CC62C91D3E90B324]    Script Date: 31/7/2025 13:19:22 ******/
ALTER TABLE [dbo].[Socio] ADD UNIQUE NONCLUSTERED 
(
	[NroDocumento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_CodigoIngreso]    Script Date: 31/7/2025 13:19:22 ******/
ALTER TABLE [dbo].[Turno] ADD  CONSTRAINT [UQ_CodigoIngreso] UNIQUE NONCLUSTERED 
(
	[CodigoIngreso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Usuario__A9D105345030614A]    Script Date: 31/7/2025 13:19:22 ******/
ALTER TABLE [dbo].[Usuario] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__Usuario__CC62C91DF54D358D]    Script Date: 31/7/2025 13:19:22 ******/
ALTER TABLE [dbo].[Usuario] ADD UNIQUE NONCLUSTERED 
(
	[NroDocumento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AuditoriaAccesos] ADD  DEFAULT (getdate()) FOR [FechaHora]
GO
ALTER TABLE [dbo].[AuditoriaTurno] ADD  DEFAULT (getdate()) FOR [FechaHora]
GO
ALTER TABLE [dbo].[CupoFecha] ADD  DEFAULT ((0)) FOR [CupoActual]
GO
ALTER TABLE [dbo].[Entrenamiento] ADD  DEFAULT ((0)) FOR [Peso]
GO
ALTER TABLE [dbo].[Gimnasio] ADD  DEFAULT ('') FOR [Email]
GO
ALTER TABLE [dbo].[HistorialRutina] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[Permiso] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[RangoHorario] ADD  DEFAULT ((0)) FOR [CupoActual]
GO
ALTER TABLE [dbo].[RangoHorario] ADD  DEFAULT ((0)) FOR [CupoMaximo]
GO
ALTER TABLE [dbo].[RangoHorario] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[RangoHorario] ADD  DEFAULT ((0)) FOR [SoloSabado]
GO
ALTER TABLE [dbo].[Rol] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[Rutina] ADD  DEFAULT ((0)) FOR [Activa]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[Accion]  WITH CHECK ADD  CONSTRAINT [FK_Accion_Grupo] FOREIGN KEY([IdGrupo])
REFERENCES [dbo].[Grupo] ([IdGrupo])
GO
ALTER TABLE [dbo].[Accion] CHECK CONSTRAINT [FK_Accion_Grupo]
GO
ALTER TABLE [dbo].[Calentamiento]  WITH CHECK ADD FOREIGN KEY([IdMaquina])
REFERENCES [dbo].[Maquina] ([IdElemento])
GO
ALTER TABLE [dbo].[CupoFecha]  WITH CHECK ADD FOREIGN KEY([IdRangoHorario])
REFERENCES [dbo].[RangoHorario] ([IdRangoHorario])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Ejercicio]  WITH CHECK ADD FOREIGN KEY([IdElemento])
REFERENCES [dbo].[ElementoGimnasio] ([IdElemento])
GO
ALTER TABLE [dbo].[Entrenamiento]  WITH CHECK ADD FOREIGN KEY([IdElementoGimnasio])
REFERENCES [dbo].[ElementoGimnasio] ([IdElemento])
GO
ALTER TABLE [dbo].[Entrenamiento]  WITH CHECK ADD FOREIGN KEY([IdRutina])
REFERENCES [dbo].[Rutina] ([IdRutina])
GO
ALTER TABLE [dbo].[Equipamiento]  WITH CHECK ADD FOREIGN KEY([IdElemento])
REFERENCES [dbo].[ElementoGimnasio] ([IdElemento])
GO
ALTER TABLE [dbo].[Historial_Calentamiento]  WITH CHECK ADD FOREIGN KEY([IdHistorial])
REFERENCES [dbo].[HistorialRutina] ([IdHistorial])
GO
ALTER TABLE [dbo].[Historial_Entrenamiento]  WITH CHECK ADD FOREIGN KEY([IdHistorial])
REFERENCES [dbo].[HistorialRutina] ([IdHistorial])
GO
ALTER TABLE [dbo].[Historial_Estiramiento]  WITH CHECK ADD FOREIGN KEY([IdHistorial])
REFERENCES [dbo].[HistorialRutina] ([IdHistorial])
GO
ALTER TABLE [dbo].[Maquina]  WITH CHECK ADD FOREIGN KEY([IdElemento])
REFERENCES [dbo].[ElementoGimnasio] ([IdElemento])
GO
ALTER TABLE [dbo].[Permiso]  WITH CHECK ADD FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
GO
ALTER TABLE [dbo].[Permiso]  WITH CHECK ADD  CONSTRAINT [FK_Permiso_Accion] FOREIGN KEY([IdAccion])
REFERENCES [dbo].[Accion] ([IdAccion])
GO
ALTER TABLE [dbo].[Permiso] CHECK CONSTRAINT [FK_Permiso_Accion]
GO
ALTER TABLE [dbo].[Permiso]  WITH CHECK ADD  CONSTRAINT [FK_Permiso_Grupo] FOREIGN KEY([IdGrupo])
REFERENCES [dbo].[Grupo] ([IdGrupo])
GO
ALTER TABLE [dbo].[Permiso] CHECK CONSTRAINT [FK_Permiso_Grupo]
GO
ALTER TABLE [dbo].[Permiso]  WITH CHECK ADD  CONSTRAINT [FK_Permiso_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Permiso] CHECK CONSTRAINT [FK_Permiso_Usuario]
GO
ALTER TABLE [dbo].[RangoHorario_Usuario]  WITH CHECK ADD FOREIGN KEY([IdRangoHorario])
REFERENCES [dbo].[RangoHorario] ([IdRangoHorario])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RangoHorario_Usuario]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Rutina]  WITH CHECK ADD  CONSTRAINT [FK_Rutina_Socio] FOREIGN KEY([IdSocio])
REFERENCES [dbo].[Socio] ([IdSocio])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Rutina] CHECK CONSTRAINT [FK_Rutina_Socio]
GO
ALTER TABLE [dbo].[Rutina_Calentamiento]  WITH CHECK ADD FOREIGN KEY([IdCalentamiento])
REFERENCES [dbo].[Calentamiento] ([IdCalentamiento])
GO
ALTER TABLE [dbo].[Rutina_Calentamiento]  WITH CHECK ADD FOREIGN KEY([IdRutina])
REFERENCES [dbo].[Rutina] ([IdRutina])
GO
ALTER TABLE [dbo].[Rutina_Estiramiento]  WITH CHECK ADD FOREIGN KEY([IdEstiramiento])
REFERENCES [dbo].[Estiramiento] ([IdEstiramiento])
GO
ALTER TABLE [dbo].[Rutina_Estiramiento]  WITH CHECK ADD FOREIGN KEY([IdRutina])
REFERENCES [dbo].[Rutina] ([IdRutina])
GO
ALTER TABLE [dbo].[Turno]  WITH CHECK ADD  CONSTRAINT [FK_Turno_RangoHorarioUsuario] FOREIGN KEY([IdRangoHorario], [IdUsuario])
REFERENCES [dbo].[RangoHorario_Usuario] ([IdRangoHorario], [IdUsuario])
GO
ALTER TABLE [dbo].[Turno] CHECK CONSTRAINT [FK_Turno_RangoHorarioUsuario]
GO
ALTER TABLE [dbo].[Turno]  WITH CHECK ADD  CONSTRAINT [FK_Turno_Socio] FOREIGN KEY([IdSocio])
REFERENCES [dbo].[Socio] ([IdSocio])
GO
ALTER TABLE [dbo].[Turno] CHECK CONSTRAINT [FK_Turno_Socio]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
GO
/****** Object:  StoredProcedure [dbo].[SP_ACTUALIZARROL]    Script Date: 31/7/2025 13:19:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ACTUALIZARROL]
(
    @IdRol INT,
    @Descripcion VARCHAR(50),
    @IdGrupo INT,
    @DescripcionGrupo VARCHAR(255),
    @Permisos TipoPermiso READONLY,
    @Mensaje VARCHAR(500) OUTPUT,
    @Resultado BIT OUTPUT
)
AS
BEGIN
    BEGIN TRY
        SET @Mensaje = ''
        SET @Resultado = 0

        BEGIN TRANSACTION

        IF EXISTS (SELECT 1 FROM Rol WHERE IdRol = @IdRol)
        BEGIN
            -- Actualizar descripción del rol
            UPDATE Rol 
            SET Descripcion = @Descripcion 
            WHERE IdRol = @IdRol;

            -- Solo actualizar grupo si se indica uno válido
            IF @IdGrupo <> -1
            BEGIN
                IF EXISTS (SELECT 1 FROM Grupo WHERE IdGrupo = @IdGrupo)
                BEGIN
                    UPDATE Grupo 
                    SET Descripcion = @DescripcionGrupo 
                    WHERE IdGrupo = @IdGrupo;
                END
                ELSE
                BEGIN
                    SET @Mensaje = 'El grupo especificado no existe.'
                    ROLLBACK TRANSACTION
                    RETURN;
                END
            END

            -- Comparar permisos antes de actualizar
            IF EXISTS (
                SELECT 1
                FROM Permiso pr
                FULL OUTER JOIN @Permisos tmp
                    ON pr.IdGrupo = tmp.IdGrupo AND pr.IdAccion = tmp.IdAccion AND pr.IdUsuario = tmp.IdUsuario
                WHERE pr.IdRol = @IdRol
                  AND (pr.IdGrupo IS NULL OR tmp.IdGrupo IS NULL
                       OR pr.IdAccion IS NULL OR tmp.IdAccion IS NULL
                       OR pr.IdUsuario IS NULL OR tmp.IdUsuario IS NULL)
            )
            BEGIN
                -- Eliminar permisos antiguos
                DELETE FROM Permiso WHERE IdRol = @IdRol;

                -- Insertar nuevos permisos
                INSERT INTO Permiso(IdRol, IdGrupo, IdAccion, IdUsuario, FechaRegistro)
                SELECT @IdRol, p.IdGrupo, p.IdAccion, p.IdUsuario, GETDATE()
                FROM @Permisos p;
            END

            SET @Resultado = 1
            SET @Mensaje = 'Rol, grupo y permisos actualizados correctamente'
            COMMIT TRANSACTION
        END
        ELSE
        BEGIN
            SET @Mensaje = 'El rol especificado no existe.'
            ROLLBACK TRANSACTION
        END
    END TRY
    BEGIN CATCH
        SET @Mensaje = ERROR_MESSAGE()
        SET @Resultado = 0
        ROLLBACK TRANSACTION
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ActualizarSocio]    Script Date: 31/7/2025 13:19:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_ActualizarSocio]
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

        -- Actualizar datos del socio
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

        -- Desactivar rutinas no seleccionadas
        UPDATE Rutina
        SET Activa = 0
        WHERE IdSocio = @IdSocio AND Dia NOT IN (SELECT Dia FROM @Rutinas);

        -- Activar rutinas seleccionadas
        UPDATE Rutina
        SET Activa = 1, FechaModificacion = GETDATE()
        WHERE IdSocio = @IdSocio AND Dia IN (SELECT Dia FROM @Rutinas);

        COMMIT TRANSACTION;
        SET @Mensaje = 'Socio actualizado correctamente con sus nuevos días de asistencia.';
    END TRY
    BEGIN CATCH
        SET @Mensaje = ERROR_MESSAGE();
        ROLLBACK TRANSACTION;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_EDITARUSUARIO]    Script Date: 31/7/2025 13:19:22 ******/
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
		SET @Mensaje = 'Usuario actualizado correctamente' 
	end
	else
		set @Mensaje = 'No se puede repetir el documento para más de un usuario'
end
GO
/****** Object:  StoredProcedure [dbo].[SP_ELIMINARROL]    Script Date: 31/7/2025 13:19:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_ELIMINARROL] (
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
            SET @Mensaje = 'No se puede eliminar este rol porque está asignado a un Usuario'
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
/****** Object:  StoredProcedure [dbo].[SP_ELIMINARSOCIO]    Script Date: 31/7/2025 13:19:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ELIMINARSOCIO]
    @IdSocio INT,
    @Respuesta BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET @Respuesta = 0;
    SET @Mensaje = '';

    DECLARE @FechaFinActividades DATE;

    -- Obtener la FechaFinActividades del socio
    SELECT @FechaFinActividades = FechaFinActividades 
    FROM Socio 
    WHERE IdSocio = @IdSocio;

    -- Verificar si la fecha de fin de actividades aún no ha vencido
    IF @FechaFinActividades > GETDATE()
    BEGIN
        SET @Mensaje = 'No se puede eliminar el socio porque su cuota aún está vigente.';
        RETURN;
    END

    -- Verificar si el socio tiene turnos en curso
    IF EXISTS (SELECT 1 FROM Turno WHERE IdSocio = @IdSocio AND EstadoTurno = 'En Curso')
    BEGIN
        SET @Mensaje = 'No se puede eliminar el socio porque tiene turnos en curso.';
        RETURN;
    END

    -- Iniciar la transacción
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Eliminar relaciones hijas manualmente
        DELETE FROM Rutina_Calentamiento WHERE IdRutina IN (SELECT IdRutina FROM Rutina WHERE IdSocio = @IdSocio);
        DELETE FROM Rutina_Estiramiento  WHERE IdRutina IN (SELECT IdRutina FROM Rutina WHERE IdSocio = @IdSocio);
        DELETE FROM Entrenamiento        WHERE IdRutina IN (SELECT IdRutina FROM Rutina WHERE IdSocio = @IdSocio);

        -- Eliminar rutinas
        DELETE FROM Rutina WHERE IdSocio = @IdSocio;

        -- Eliminar los turnos asociados al socio
        DELETE FROM Turno WHERE IdSocio = @IdSocio;

        -- Eliminar el socio
        DELETE FROM Socio WHERE IdSocio = @IdSocio;

        -- Confirmar la transacción si todo se ejecutó sin errores
        COMMIT TRANSACTION;

        SET @Respuesta = 1;
        SET @Mensaje = 'Socio eliminado correctamente junto con sus rutinas y turnos.';
    END TRY
    BEGIN CATCH
        -- Si hay un error, revertir la transacción
        ROLLBACK TRANSACTION;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_ELIMINARTURNO]    Script Date: 31/7/2025 13:19:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_ELIMINARTURNO]
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
GO
/****** Object:  StoredProcedure [dbo].[SP_ELIMINARUSUARIO]    Script Date: 31/7/2025 13:19:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_ELIMINARUSUARIO](
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
GO
/****** Object:  StoredProcedure [dbo].[SP_ListarEntrenadoresDisponibles]    Script Date: 31/7/2025 13:19:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_ListarEntrenadoresDisponibles]
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
/****** Object:  StoredProcedure [dbo].[SP_MODIFICARACCION]    Script Date: 31/7/2025 13:19:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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
/****** Object:  StoredProcedure [dbo].[SP_OBTENER_PERMISOS_POR_ROL]    Script Date: 31/7/2025 13:19:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_OBTENER_PERMISOS_POR_ROL]
(
    @IdRol INT
)
AS
BEGIN
    -- Permisos por GRUPO
    SELECT 
        'Grupo' AS TipoPermiso,
        g.IdGrupo,
        g.NombreMenu,
        g.Descripcion,
        NULL AS IdAccion,
        NULL AS NombreAccion,
        NULL AS DescAccion
    FROM Permiso p
    INNER JOIN Grupo g ON p.IdGrupo = g.IdGrupo
    WHERE p.IdRol = @IdRol AND p.IdGrupo IS NOT NULL

    UNION

    -- Permisos por ACCIÓN
    SELECT 
        'Accion' AS TipoPermiso,
        a.IdGrupo,
        g.NombreMenu,
        g.Descripcion,
        a.IdAccion,
        a.NombreAccion,
        a.Descripcion
    FROM Permiso p
    INNER JOIN Accion a ON p.IdAccion = a.IdAccion
    INNER JOIN Grupo g ON a.IdGrupo = g.IdGrupo
    WHERE p.IdRol = @IdRol AND p.IdAccion IS NOT NULL
END
GO
/****** Object:  StoredProcedure [dbo].[SP_REGISTRAR_RANGOHORARIO]    Script Date: 31/7/2025 13:19:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_REGISTRAR_RANGOHORARIO]
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
/****** Object:  StoredProcedure [dbo].[SP_REGISTRAR_RANGOHORARIO_USUARIO]    Script Date: 31/7/2025 13:19:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_REGISTRAR_RANGOHORARIO_USUARIO]
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
/****** Object:  StoredProcedure [dbo].[SP_REGISTRARROL]    Script Date: 31/7/2025 13:19:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_REGISTRARROL](
    @Descripcion VARCHAR(50),
    @Permisos ETabla_Permisos READONLY,
    @Mensaje VARCHAR(500) OUTPUT,
    @Resultado BIT OUTPUT
)
AS
BEGIN
    BEGIN TRY
        DECLARE @IdRol INT
        SET @Mensaje = ''
        SET @Resultado = 0

        BEGIN TRANSACTION

        IF NOT EXISTS (SELECT 1 FROM Rol WHERE Descripcion = @Descripcion)
        BEGIN
            INSERT INTO Rol (Descripcion) VALUES (@Descripcion);
            SET @IdRol = SCOPE_IDENTITY();

            -- Insertar permisos por grupo
            INSERT INTO Permiso (IdRol, IdGrupo)
            SELECT @IdRol, IdGrupo
            FROM @Permisos
            WHERE TipoPermiso = 'Grupo';

            -- Insertar permisos por acción
            INSERT INTO Permiso (IdRol, IdAccion)
            SELECT @IdRol, IdAccion
            FROM @Permisos
            WHERE TipoPermiso = 'Accion';

            SET @Resultado = 1
            SET @Mensaje = 'Rol registrado correctamente'
            COMMIT TRANSACTION
        END
        ELSE
        BEGIN
            SET @Mensaje = 'No se puede tener más de un rol con la misma descripción'
            ROLLBACK TRANSACTION
        END
    END TRY
    BEGIN CATCH
        SET @Mensaje = ERROR_MESSAGE()
        SET @Resultado = 0
        ROLLBACK TRANSACTION
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_RegistrarSocio]    Script Date: 31/7/2025 13:19:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_RegistrarSocio]
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
    @Rutinas ETabla_Rutinas READONLY, -- Lista de rutinas elegidas
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

        SET @IdSocio = SCOPE_IDENTITY()

        -- Insertar TODAS las rutinas de lunes a sábado, activando solo las seleccionadas
        INSERT INTO Rutina (IdSocio, FechaModificacion, Dia, Activa)
        SELECT @IdSocio, GETDATE(), Dia,
               CASE WHEN Dia IN (SELECT Dia FROM @Rutinas) THEN 1 ELSE 0 END
        FROM (
            SELECT 'Lunes' AS Dia UNION
            SELECT 'Martes' UNION
            SELECT 'Miércoles' UNION
            SELECT 'Jueves' UNION
            SELECT 'Viernes' UNION
            SELECT 'Sábado'
        ) AS DiasSemana

        COMMIT TRANSACTION
        SET @Mensaje = 'Socio registrado exitosamente con sus rutinas.'
    END TRY
    BEGIN CATCH
        SET @Mensaje = ERROR_MESSAGE()
        ROLLBACK TRANSACTION
    END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[SP_REGISTRARTURNO]    Script Date: 31/7/2025 13:19:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_REGISTRARTURNO]
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
GO
/****** Object:  StoredProcedure [dbo].[SP_REGISTRARUSUARIO]    Script Date: 31/7/2025 13:19:22 ******/
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
		SET @Mensaje = 'Usuario registrado correctamente'
		
	end
	else
		set @Mensaje = 'No se puede repetir el documento para más de un usuario'

end
GO
USE [master]
GO
ALTER DATABASE [DBSISTEMA_GYM] SET  READ_WRITE 
GO
