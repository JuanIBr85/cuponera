USE [master]
GO
/****** Object:  Database [Cupones]    Script Date: 10/11/2024 19:24:29 ******/
CREATE DATABASE [Cupones]
GO
ALTER DATABASE [Cupones] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Cupones].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Cupones] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Cupones] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Cupones] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Cupones] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Cupones] SET ARITHABORT OFF 
GO
ALTER DATABASE [Cupones] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Cupones] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Cupones] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Cupones] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Cupones] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Cupones] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Cupones] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Cupones] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Cupones] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Cupones] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Cupones] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Cupones] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Cupones] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Cupones] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Cupones] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Cupones] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Cupones] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Cupones] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Cupones] SET  MULTI_USER 
GO
ALTER DATABASE [Cupones] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Cupones] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Cupones] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Cupones] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Cupones] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Cupones] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Cupones] SET QUERY_STORE = OFF
GO
USE [Cupones]
GO
/****** Object:  Table [dbo].[Articulos]    Script Date: 10/11/2024 19:24:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articulos](
	[Id_Articulo] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Articulo] [varchar](100) NOT NULL,
	[Descripcion_Articulo] [varchar](100) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Articulos] PRIMARY KEY CLUSTERED 
(
	[Id_Articulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 10/11/2024 19:24:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[Id_Categoria] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Categorias] PRIMARY KEY CLUSTERED 
(
	[Id_Categoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 10/11/2024 19:24:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[CodCliente] [varchar](12) NOT NULL,
	[Nombre_Cliente] [varchar](100) NOT NULL,
	[Apellido_Cliente] [varchar](100) NOT NULL,
	[Direccion] [varchar](100) NOT NULL,
	[Email] [varchar](100) NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[CodCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cupones]    Script Date: 10/11/2024 19:24:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cupones](
	[id_Cupon] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](300) NOT NULL,
	[Descripcion] [varchar](300) NOT NULL,
	[PorcentajeDto] [numeric](18, 2) NULL,
	[ImportePromo] [numeric](18, 2) NULL,
	[FechaInicio] [date] NOT NULL,
	[FechaFin] [date] NOT NULL,
	[Id_Tipo_Cupon] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Cupones_1] PRIMARY KEY CLUSTERED 
(
	[id_Cupon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cupones_Categorias]    Script Date: 10/11/2024 19:24:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cupones_Categorias](
	[Id_Cupones_Categorias] [int] IDENTITY(1,1) NOT NULL,
	[Id_Cupon] [int] NOT NULL,
	[Id_Categoria] [int] NOT NULL,
 CONSTRAINT [PK_Cupones_Categorias] PRIMARY KEY CLUSTERED 
(
	[Id_Cupones_Categorias] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cupones_Clientes]    Script Date: 10/11/2024 19:24:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cupones_Clientes](
	[id_Cupon] [int] NOT NULL,
	[NroCupon] [varchar](12) NOT NULL,
	[FechaAsignado] [datetime] NOT NULL,
	[CodCliente] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Cupones_Clientes] PRIMARY KEY CLUSTERED 
(
	[NroCupon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cupones_Detalle]    Script Date: 10/11/2024 19:24:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cupones_Detalle](
	[id_Cupon] [int] NOT NULL,
	[id_Articulo] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
 CONSTRAINT [PK_Cupones_Detalle] PRIMARY KEY CLUSTERED 
(
	[id_Cupon] ASC,
	[id_Articulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cupones_Historial]    Script Date: 10/11/2024 19:24:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cupones_Historial](
	[id_Cupon] [int] NOT NULL,
	[NroCupon] [varchar](12) NOT NULL,
	[FechaUso] [date] NOT NULL,
	[CodCliente] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Cupones_Historial] PRIMARY KEY CLUSTERED 
(
	[id_Cupon] ASC,
	[NroCupon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Precios]    Script Date: 10/11/2024 19:24:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Precios](
	[Id_Precio] [int] IDENTITY(1,1) NOT NULL,
	[Id_Articulo] [int] NOT NULL,
	[Precio] [numeric](18, 2) NOT NULL,
 CONSTRAINT [PK_Precios] PRIMARY KEY CLUSTERED 
(
	[Id_Precio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipo_Cupon]    Script Date: 10/11/2024 19:24:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipo_Cupon](
	[Id_Tipo_Cupon] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Tipo_Cupon] PRIMARY KEY CLUSTERED 
(
	[Id_Tipo_Cupon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Articulos] ON 

INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (1, N'Refrigerador', N'Refrigerador de alta capacidad con eficiencia energética', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (2, N'Lavadora', N'Lavadora automática de carga frontal con múltiples ciclos', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (3, N'Secadora', N'Secadora de ropa con sistema de secado rápido', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (4, N'Microondas', N'Microondas con descongelado rápido y múltiples configuraciones', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (5, N'Horno Eléctrico', N'Horno eléctrico compacto con control de temperatura', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (6, N'Horno de Convección', N'Horno de convección con ajuste de temperatura', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (7, N'Licuadora', N'Licuadora de alta potencia para smoothies y batidos', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (8, N'Batidora', N'Batidora de mano con múltiples velocidades', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (9, N'Cafetera', N'Cafetera de goteo con temporizador programable', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (10, N'Extractor de Jugos', N'Extractor de jugos con sistema de extracción lenta', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (11, N'Plancha de Vapor', N'Plancha de vapor con ajuste de temperatura', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (12, N'Aspiradora', N'Aspiradora de alta succión con filtro HEPA', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (13, N'Ventilador de Piso', N'Ventilador de piso con ajuste de velocidad', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (14, N'Aire Acondicionado', N'Aire acondicionado de pared con control remoto', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (15, N'Calefactor', N'Calefactor eléctrico portátil con termostato ajustable', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (16, N'Televisor LED', N'Televisor LED 4K de 55 pulgadas', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (17, N'Televisor OLED', N'Televisor OLED con resolución UHD', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (18, N'Equipo de Sonido', N'Sistema de sonido envolvente con subwoofer', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (19, N'Barras de Sonido', N'Barra de sonido Bluetooth con subwoofer integrado', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (20, N'Roku', N'Dispositivo de streaming Roku con control remoto', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (21, N'Apple TV', N'Dispositivo de streaming Apple TV con 4K', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (22, N'Chromecast', N'Dispositivo de streaming Chromecast', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (23, N'Router Wi-Fi', N'Router Wi-Fi de alta velocidad con dual-band', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (24, N'Extensor de Red', N'Extensor de red para ampliar la cobertura Wi-Fi', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (25, N'Teléfono Inalámbrico', N'Teléfono inalámbrico con contestador automático', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (26, N'Teléfono IP', N'Teléfono IP compatible con servicios VoIP', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (27, N'Monitor de PC', N'Monitor LED Full HD de 24 pulgadas', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (28, N'Teclado Inalámbrico', N'Teclado inalámbrico con batería recargable', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (29, N'Mouse Inalámbrico', N'Mouse ergonómico inalámbrico', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (30, N'Computadora Portátil', N'Laptop con procesador Intel Core i5', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (31, N'Tablet', N'Tablet de 10 pulgadas con almacenamiento expandible', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (32, N'Smartphone', N'Teléfono inteligente de última generación', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (33, N'Smartwatch', N'Reloj inteligente con monitor de frecuencia cardíaca', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (34, N'Cámara Digital', N'Cámara digital compacta con zoom óptico', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (35, N'Videocámara', N'Videocámara con grabación en 4K', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (36, N'Parlante Bluetooth', N'Parlante portátil Bluetooth resistente al agua', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (37, N'Subwoofer', N'Subwoofer para sistema de audio', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (38, N'Disco Duro Externo', N'Disco duro externo de 1TB', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (39, N'Memoria USB', N'Memoria USB de 64GB', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (40, N'Impresora', N'Impresora multifuncional inalámbrica', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (41, N'Escáner', N'Escáner de documentos de alta resolución', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (42, N'Deshumidificador', N'Deshumidificador de aire portátil', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (43, N'Humidificador', N'Humidificador ultrasónico de aire', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (44, N'Despertador Digital', N'Reloj despertador digital con proyector de hora', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (45, N'Báscula de Baño', N'Báscula digital de baño con análisis de composición corporal', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (46, N'Freidora de Aire', N'Freidora de aire sin aceite', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (47, N'Olla de Cocción Lenta', N'Olla de cocción lenta con temporizador programable', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (48, N'Parrilla Eléctrica', N'Parrilla eléctrica antiadherente', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (49, N'Tostadora', N'Tostadora de 4 rebanadas con control de dorado', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (50, N'Hervidor Eléctrico', N'Hervidor eléctrico de acero inoxidable', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (51, N'Purificador de Agua', N'Purificador de agua con filtro de carbón activado', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (52, N'Cocina de Inducción', N'Cocina de inducción portátil de 2 quemadores', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (53, N'Termo para Agua', N'Termo eléctrico para agua con control de temperatura', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (54, N'Ventilador de Techo', N'Ventilador de techo con control remoto', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (55, N'Extractor de Aire', N'Extractor de aire para cocina o baño', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (56, N'Aspiradora Robot', N'Aspiradora robot con mapeo inteligente', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (57, N'Cortadora de Cabello', N'Cortadora de cabello inalámbrica', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (58, N'Afeitadora Eléctrica', N'Afeitadora eléctrica con cabezal rotativo', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (59, N'Depiladora', N'Depiladora de luz pulsada para uso en casa', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (60, N'Secador de Pelo', N'Secador de pelo de alta potencia', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (61, N'Plancha para Cabello', N'Plancha alisadora con placas de cerámica', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (62, N'Rizador de Cabello', N'Rizador automático para cabello', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (63, N'Balanza de Cocina', N'Balanza digital para cocina', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (64, N'Picadora de Alimentos', N'Picadora de alimentos eléctrica', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (65, N'Máquina para Hacer Pan', N'Máquina de pan con varios programas de cocción', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (66, N'Fábrica de Helados', N'Máquina para hacer helado en casa', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (67, N'Molinillo de Café', N'Molinillo eléctrico para café', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (68, N'Deshidratador de Alimentos', N'Deshidratador de alimentos portátil', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (69, N'Máquina de Palomitas', N'Máquina para hacer palomitas de maíz sin aceite', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (70, N'Cafetera Espresso', N'Cafetera espresso con vaporizador de leche', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (71, N'Placa de Inducción', N'Placa de inducción con superficie antiadherente', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (72, N'Extractor de Granos de Café', N'Extractor manual de granos de café', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (73, N'Tetera', N'Tetera eléctrica de acero inoxidable', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (74, N'Purificador de Aire', N'Purificador de aire con filtro HEPA', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (75, N'Ventilador de Escritorio', N'Ventilador de escritorio silencioso', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (76, N'Secador de Ropa', N'Secador de ropa portátil', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (77, N'Calentador de Agua', N'Calentador de agua instantáneo', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (78, N'Radiador Eléctrico', N'Radiador eléctrico de bajo consumo', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (79, N'Congelador Horizontal', N'Congelador horizontal con capacidad de 100 litros', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (80, N'Triturador de Alimentos', N'Triturador de alimentos para el fregadero', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (81, N'Lavavajillas', N'Lavavajillas automático con varias funciones de lavado', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (82, N'Plancha de Vapor Vertical', N'Plancha de vapor vertical para prendas delicadas', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (83, N'Secador de Manos', N'Secador de manos eléctrico para baño', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (84, N'Filtro de Agua', N'Filtro de agua para grifo de cocina', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (85, N'Cerradura Digital', N'Cerradura digital para puerta con huella dactilar', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (86, N'Sensor de Movimiento', N'Sensor de movimiento para seguridad en el hogar', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (87, N'Intercomunicador', N'Intercomunicador de video para puerta', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (88, N'Controlador de Iluminación', N'Controlador inteligente para iluminación del hogar', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (89, N'Panel Solar', N'Panel solar portátil para cargar dispositivos', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (90, N'Bomba de Agua', N'Bomba de agua portátil para uso doméstico', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (91, N'Ventilador de Torre', N'Ventilador de torre con control remoto', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (92, N'Máquina de Coser', N'Máquina de coser portátil para hogar', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (93, N'Repelente de Insectos', N'Repelente de insectos ultrasónico', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (94, N'Detector de Humedad', N'Detector de humedad para interiores', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (95, N'Medidor de Calidad del Aire', N'Medidor de calidad del aire portátil', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (96, N'Cámara de Seguridad', N'Cámara de seguridad con visión nocturna', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (97, N'Caja Fuerte', N'Caja fuerte digital para hogar', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (98, N'Mini Nevera', N'Mini nevera portátil para bebidas', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (99, N'Sistema de Alarma', N'Sistema de alarma para el hogar', 1)
GO
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (100, N'Lámpara LED Recargable', N'Lámpara LED recargable para emergencias', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (101, N'Reproductor de DVD', N'Reproductor de DVD portátil', 1)
INSERT [dbo].[Articulos] ([Id_Articulo], [Nombre_Articulo], [Descripcion_Articulo], [Activo]) VALUES (102, N'Centro de Entretenimiento', N'Centro de entretenimiento multimedia', 1)
SET IDENTITY_INSERT [dbo].[Articulos] OFF
GO
SET IDENTITY_INSERT [dbo].[Categorias] ON 

INSERT [dbo].[Categorias] ([Id_Categoria], [Nombre]) VALUES (1, N'Descuentos')
INSERT [dbo].[Categorias] ([Id_Categoria], [Nombre]) VALUES (2, N'Promociones')
SET IDENTITY_INSERT [dbo].[Categorias] OFF
GO
SET IDENTITY_INSERT [dbo].[Precios] ON 

INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (1, 1, CAST(75000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (2, 2, CAST(45000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (3, 3, CAST(40000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (4, 4, CAST(25000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (5, 5, CAST(22000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (6, 6, CAST(30000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (7, 7, CAST(12000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (8, 8, CAST(8000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (9, 9, CAST(10000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (10, 10, CAST(15000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (11, 11, CAST(7000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (12, 12, CAST(18000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (13, 13, CAST(6000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (14, 14, CAST(55000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (15, 15, CAST(22000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (16, 16, CAST(80000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (17, 17, CAST(120000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (18, 18, CAST(25000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (19, 19, CAST(18000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (20, 20, CAST(7000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (21, 21, CAST(25000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (22, 22, CAST(15000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (23, 23, CAST(12000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (24, 24, CAST(6000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (25, 25, CAST(5000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (26, 26, CAST(30000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (27, 27, CAST(25000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (28, 28, CAST(8000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (29, 29, CAST(6000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (30, 30, CAST(65000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (31, 31, CAST(35000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (32, 32, CAST(90000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (33, 33, CAST(25000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (34, 34, CAST(15000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (35, 35, CAST(30000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (36, 36, CAST(12000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (37, 37, CAST(18000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (38, 38, CAST(20000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (39, 39, CAST(2000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (40, 40, CAST(18000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (41, 41, CAST(12000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (42, 42, CAST(15000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (43, 43, CAST(8000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (44, 44, CAST(3500.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (45, 45, CAST(7000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (46, 46, CAST(12000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (47, 47, CAST(12000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (48, 48, CAST(10000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (49, 49, CAST(4500.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (50, 50, CAST(3000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (51, 51, CAST(12000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (52, 52, CAST(18000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (53, 53, CAST(4000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (54, 54, CAST(6000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (55, 55, CAST(8000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (56, 56, CAST(20000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (57, 57, CAST(5000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (58, 58, CAST(4500.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (59, 59, CAST(8000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (60, 60, CAST(10000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (61, 61, CAST(6000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (62, 62, CAST(5000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (63, 63, CAST(2500.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (64, 64, CAST(8000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (65, 65, CAST(15000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (66, 66, CAST(12000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (67, 67, CAST(5000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (68, 68, CAST(15000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (69, 69, CAST(6000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (70, 70, CAST(15000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (71, 71, CAST(8000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (72, 72, CAST(4000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (73, 73, CAST(6000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (74, 74, CAST(10000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (75, 75, CAST(4000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (76, 76, CAST(15000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (77, 77, CAST(10000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (78, 78, CAST(15000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (79, 79, CAST(22000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (80, 80, CAST(10000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (81, 81, CAST(25000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (82, 82, CAST(8000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (83, 83, CAST(8000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (84, 84, CAST(5000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (85, 85, CAST(25000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (86, 86, CAST(10000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (87, 87, CAST(12000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (88, 88, CAST(15000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (89, 89, CAST(35000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (90, 90, CAST(8000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (91, 91, CAST(10000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (92, 92, CAST(20000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (93, 93, CAST(5000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (94, 94, CAST(6000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (95, 95, CAST(15000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (96, 96, CAST(20000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (97, 97, CAST(12000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (98, 98, CAST(6000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (99, 99, CAST(25000.00 AS Numeric(18, 2)))
GO
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (100, 100, CAST(3000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (101, 101, CAST(5000.00 AS Numeric(18, 2)))
INSERT [dbo].[Precios] ([Id_Precio], [Id_Articulo], [Precio]) VALUES (102, 102, CAST(40000.00 AS Numeric(18, 2)))
SET IDENTITY_INSERT [dbo].[Precios] OFF
GO
SET IDENTITY_INSERT [dbo].[Tipo_Cupon] ON 

INSERT [dbo].[Tipo_Cupon] ([Id_Tipo_Cupon], [Nombre]) VALUES (1, N'Promoción')
INSERT [dbo].[Tipo_Cupon] ([Id_Tipo_Cupon], [Nombre]) VALUES (2, N'Descuento')
INSERT [dbo].[Tipo_Cupon] ([Id_Tipo_Cupon], [Nombre]) VALUES (3, N'Promoción')
INSERT [dbo].[Tipo_Cupon] ([Id_Tipo_Cupon], [Nombre]) VALUES (4, N'Descuento')
SET IDENTITY_INSERT [dbo].[Tipo_Cupon] OFF
GO
ALTER TABLE [dbo].[Cupones] ADD  CONSTRAINT [DF__Cupones__Activo__73BA3083]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Cupones]  WITH CHECK ADD  CONSTRAINT [FK_Cupones_Tipo_Cupon] FOREIGN KEY([Id_Tipo_Cupon])
REFERENCES [dbo].[Tipo_Cupon] ([Id_Tipo_Cupon])
GO
ALTER TABLE [dbo].[Cupones] CHECK CONSTRAINT [FK_Cupones_Tipo_Cupon]
GO
ALTER TABLE [dbo].[Cupones_Categorias]  WITH CHECK ADD  CONSTRAINT [FK_Cupones_Categorias_Categorias1] FOREIGN KEY([Id_Categoria])
REFERENCES [dbo].[Categorias] ([Id_Categoria])
GO
ALTER TABLE [dbo].[Cupones_Categorias] CHECK CONSTRAINT [FK_Cupones_Categorias_Categorias1]
GO
ALTER TABLE [dbo].[Cupones_Categorias]  WITH CHECK ADD  CONSTRAINT [FK_Cupones_Categorias_Cupones1] FOREIGN KEY([Id_Cupon])
REFERENCES [dbo].[Cupones] ([id_Cupon])
GO
ALTER TABLE [dbo].[Cupones_Categorias] CHECK CONSTRAINT [FK_Cupones_Categorias_Cupones1]
GO
ALTER TABLE [dbo].[Cupones_Clientes]  WITH CHECK ADD  CONSTRAINT [FK_Cupones_Clientes_Cupones] FOREIGN KEY([id_Cupon])
REFERENCES [dbo].[Cupones] ([id_Cupon])
GO
ALTER TABLE [dbo].[Cupones_Clientes] CHECK CONSTRAINT [FK_Cupones_Clientes_Cupones]
GO
ALTER TABLE [dbo].[Cupones_Detalle]  WITH CHECK ADD  CONSTRAINT [FK_Cupones_Detalle_Articulos] FOREIGN KEY([id_Articulo])
REFERENCES [dbo].[Articulos] ([Id_Articulo])
GO
ALTER TABLE [dbo].[Cupones_Detalle] CHECK CONSTRAINT [FK_Cupones_Detalle_Articulos]
GO
ALTER TABLE [dbo].[Cupones_Detalle]  WITH CHECK ADD  CONSTRAINT [FK_Cupones_Detalle_Cupones] FOREIGN KEY([id_Cupon])
REFERENCES [dbo].[Cupones] ([id_Cupon])
GO
ALTER TABLE [dbo].[Cupones_Detalle] CHECK CONSTRAINT [FK_Cupones_Detalle_Cupones]
GO
ALTER TABLE [dbo].[Precios]  WITH CHECK ADD  CONSTRAINT [FK_Precios_Articulos] FOREIGN KEY([Id_Articulo])
REFERENCES [dbo].[Articulos] ([Id_Articulo])
GO
ALTER TABLE [dbo].[Precios] CHECK CONSTRAINT [FK_Precios_Articulos]
GO
USE [master]
GO
ALTER DATABASE [Cupones] SET  READ_WRITE 
GO
