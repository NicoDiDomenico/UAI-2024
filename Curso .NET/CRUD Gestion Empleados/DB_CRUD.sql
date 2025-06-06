USE [master]
GO
/****** Object:  Database [bd_gestion_empleados]    Script Date: 25/1/2025 23:00:32 ******/
CREATE DATABASE [bd_gestion_empleados]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'bd_gestion_empleados', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\bd_gestion_empleados.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'bd_gestion_empleados_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\bd_gestion_empleados_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [bd_gestion_empleados] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [bd_gestion_empleados].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [bd_gestion_empleados] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [bd_gestion_empleados] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [bd_gestion_empleados] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [bd_gestion_empleados] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [bd_gestion_empleados] SET ARITHABORT OFF 
GO
ALTER DATABASE [bd_gestion_empleados] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [bd_gestion_empleados] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [bd_gestion_empleados] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [bd_gestion_empleados] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [bd_gestion_empleados] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [bd_gestion_empleados] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [bd_gestion_empleados] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [bd_gestion_empleados] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [bd_gestion_empleados] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [bd_gestion_empleados] SET  DISABLE_BROKER 
GO
ALTER DATABASE [bd_gestion_empleados] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [bd_gestion_empleados] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [bd_gestion_empleados] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [bd_gestion_empleados] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [bd_gestion_empleados] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [bd_gestion_empleados] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [bd_gestion_empleados] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [bd_gestion_empleados] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [bd_gestion_empleados] SET  MULTI_USER 
GO
ALTER DATABASE [bd_gestion_empleados] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [bd_gestion_empleados] SET DB_CHAINING OFF 
GO
ALTER DATABASE [bd_gestion_empleados] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [bd_gestion_empleados] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [bd_gestion_empleados] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [bd_gestion_empleados] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [bd_gestion_empleados] SET QUERY_STORE = ON
GO
ALTER DATABASE [bd_gestion_empleados] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [bd_gestion_empleados]
GO
/****** Object:  Table [dbo].[tb_cargos]    Script Date: 25/1/2025 23:00:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_cargos](
	[id_cargo] [int] IDENTITY(1,1) NOT NULL,
	[nombre_cargo] [varchar](50) NULL,
	[activo_cargo] [bit] NULL,
 CONSTRAINT [PK_tb_cargos] PRIMARY KEY CLUSTERED 
(
	[id_cargo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_departamentos]    Script Date: 25/1/2025 23:00:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_departamentos](
	[id_departamento] [int] IDENTITY(1,1) NOT NULL,
	[nombre_departamento] [varchar](50) NULL,
	[activo_departamento] [bit] NULL,
 CONSTRAINT [PK_tb_departamentos] PRIMARY KEY CLUSTERED 
(
	[id_departamento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_empleados]    Script Date: 25/1/2025 23:00:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_empleados](
	[id_empleado] [int] IDENTITY(1,1) NOT NULL,
	[nombre_empleado] [varchar](100) NULL,
	[direccion_empleado] [varchar](150) NULL,
	[fecha_nacimiento_empleado] [date] NULL,
	[telefono_empleado] [varchar](80) NULL,
	[salario_emplado] [money] NULL,
	[id_departamento] [int] NULL,
	[id_cargo] [int] NULL,
	[activo_empleado] [bit] NULL,
 CONSTRAINT [PK_tb_empleados] PRIMARY KEY CLUSTERED 
(
	[id_empleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tb_empleados]  WITH CHECK ADD  CONSTRAINT [FK_tb_empleados_tb_cargos] FOREIGN KEY([id_cargo])
REFERENCES [dbo].[tb_cargos] ([id_cargo])
GO
ALTER TABLE [dbo].[tb_empleados] CHECK CONSTRAINT [FK_tb_empleados_tb_cargos]
GO
ALTER TABLE [dbo].[tb_empleados]  WITH CHECK ADD  CONSTRAINT [FK_tb_empleados_tb_departamentos] FOREIGN KEY([id_departamento])
REFERENCES [dbo].[tb_departamentos] ([id_departamento])
GO
ALTER TABLE [dbo].[tb_empleados] CHECK CONSTRAINT [FK_tb_empleados_tb_departamentos]
GO
USE [master]
GO
ALTER DATABASE [bd_gestion_empleados] SET  READ_WRITE 
GO
