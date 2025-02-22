USE [master]
GO
/****** Object:  Database [pizzeria_POS]    Script Date: 30/01/2025 03:43:29 a. m. ******/
CREATE DATABASE [pizzeria_POS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'pizzeria_POS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\pizzeria_POS.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'pizzeria_POS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\pizzeria_POS_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [pizzeria_POS] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [pizzeria_POS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [pizzeria_POS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [pizzeria_POS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [pizzeria_POS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [pizzeria_POS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [pizzeria_POS] SET ARITHABORT OFF 
GO
ALTER DATABASE [pizzeria_POS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [pizzeria_POS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [pizzeria_POS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [pizzeria_POS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [pizzeria_POS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [pizzeria_POS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [pizzeria_POS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [pizzeria_POS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [pizzeria_POS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [pizzeria_POS] SET  ENABLE_BROKER 
GO
ALTER DATABASE [pizzeria_POS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [pizzeria_POS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [pizzeria_POS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [pizzeria_POS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [pizzeria_POS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [pizzeria_POS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [pizzeria_POS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [pizzeria_POS] SET RECOVERY FULL 
GO
ALTER DATABASE [pizzeria_POS] SET  MULTI_USER 
GO
ALTER DATABASE [pizzeria_POS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [pizzeria_POS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [pizzeria_POS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [pizzeria_POS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [pizzeria_POS] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [pizzeria_POS] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'pizzeria_POS', N'ON'
GO
ALTER DATABASE [pizzeria_POS] SET QUERY_STORE = ON
GO
ALTER DATABASE [pizzeria_POS] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [pizzeria_POS]
GO
/****** Object:  User [NT AUTHORITY\SYSTEM]    Script Date: 30/01/2025 03:43:30 a. m. ******/
CREATE USER [NT AUTHORITY\SYSTEM] FOR LOGIN [NT AUTHORITY\SYSTEM] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  DatabaseRole [SQLArcExtensionUserRole]    Script Date: 30/01/2025 03:43:30 a. m. ******/
CREATE ROLE [SQLArcExtensionUserRole]
GO
ALTER ROLE [SQLArcExtensionUserRole] ADD MEMBER [NT AUTHORITY\SYSTEM]
GO
/****** Object:  Table [dbo].[categoriaProducto]    Script Date: 30/01/2025 03:43:30 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categoriaProducto](
	[idCategoria] [int] IDENTITY(1,1) NOT NULL,
	[nombreCategoria] [varchar](255) NULL,
	[descripcion] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[idCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[clientes]    Script Date: 30/01/2025 03:43:30 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[clientes](
	[id_cliente] [int] IDENTITY(1,1) NOT NULL,
	[nombreCliente] [varchar](255) NULL,
	[telefono] [varchar](20) NULL,
	[email] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detallePedido]    Script Date: 30/01/2025 03:43:30 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detallePedido](
	[idDetalle] [int] IDENTITY(1,1) NOT NULL,
	[idPedido] [int] NOT NULL,
	[idProducto] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[subtotal] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[idDetalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[direccionCliente]    Script Date: 30/01/2025 03:43:30 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[direccionCliente](
	[idDireccion] [int] IDENTITY(1,1) NOT NULL,
	[idCliente] [int] NULL,
	[direccion] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[idDireccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pedidos]    Script Date: 30/01/2025 03:43:30 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pedidos](
	[idPedido] [int] IDENTITY(1,1) NOT NULL,
	[idEmpleado] [int] NULL,
	[idDireccionCliente] [int] NULL,
	[Total] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[idPedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[producto]    Script Date: 30/01/2025 03:43:30 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[producto](
	[idProducto] [int] IDENTITY(1,1) NOT NULL,
	[nombreProducto] [varchar](255) NULL,
	[precioProducto] [decimal](10, 2) NULL,
	[impuesto] [decimal](10, 2) NULL,
	[stock] [int] NULL,
	[idCategoria] [int] NULL,
	[idProveedor] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[proveedores]    Script Date: 30/01/2025 03:43:30 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[proveedores](
	[idProveedor] [int] IDENTITY(1,1) NOT NULL,
	[nombreProveedor] [varchar](255) NULL,
	[direccion] [text] NULL,
	[telefono] [varchar](20) NULL,
	[email] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[idProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[roles]    Script Date: 30/01/2025 03:43:30 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[roles](
	[idRol] [int] IDENTITY(1,1) NOT NULL,
	[nombreRol] [varchar](100) NULL,
	[descripcion] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[idRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 30/01/2025 03:43:30 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuarios](
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[nombreUsuario] [varchar](255) NULL,
	[idRol] [int] NULL,
	[telefono] [varchar](20) NULL,
	[email] [varchar](100) NULL,
	[password] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[detallePedido]  WITH CHECK ADD FOREIGN KEY([idPedido])
REFERENCES [dbo].[pedidos] ([idPedido])
GO
ALTER TABLE [dbo].[detallePedido]  WITH CHECK ADD FOREIGN KEY([idProducto])
REFERENCES [dbo].[producto] ([idProducto])
GO
ALTER TABLE [dbo].[direccionCliente]  WITH CHECK ADD FOREIGN KEY([idCliente])
REFERENCES [dbo].[clientes] ([id_cliente])
GO
ALTER TABLE [dbo].[pedidos]  WITH CHECK ADD FOREIGN KEY([idDireccionCliente])
REFERENCES [dbo].[direccionCliente] ([idDireccion])
GO
ALTER TABLE [dbo].[pedidos]  WITH CHECK ADD FOREIGN KEY([idEmpleado])
REFERENCES [dbo].[usuarios] ([idUsuario])
GO
ALTER TABLE [dbo].[producto]  WITH CHECK ADD FOREIGN KEY([idCategoria])
REFERENCES [dbo].[categoriaProducto] ([idCategoria])
GO
ALTER TABLE [dbo].[producto]  WITH CHECK ADD FOREIGN KEY([idProveedor])
REFERENCES [dbo].[proveedores] ([idProveedor])
GO
ALTER TABLE [dbo].[usuarios]  WITH CHECK ADD FOREIGN KEY([idRol])
REFERENCES [dbo].[roles] ([idRol])
GO
USE [master]
GO
ALTER DATABASE [pizzeria_POS] SET  READ_WRITE 
GO
