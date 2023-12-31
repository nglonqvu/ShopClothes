USE [master]
GO
/****** Object:  Database [PRN211_BL5]    Script Date: 8/23/2023 7:55:11 PM ******/
CREATE DATABASE [PRN211_BL5]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PRN211_BL5', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PRN211_BL5.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PRN211_BL5_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PRN211_BL5_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PRN211_BL5] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PRN211_BL5].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PRN211_BL5] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PRN211_BL5] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PRN211_BL5] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PRN211_BL5] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PRN211_BL5] SET ARITHABORT OFF 
GO
ALTER DATABASE [PRN211_BL5] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [PRN211_BL5] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PRN211_BL5] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PRN211_BL5] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PRN211_BL5] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PRN211_BL5] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PRN211_BL5] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PRN211_BL5] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PRN211_BL5] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PRN211_BL5] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PRN211_BL5] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PRN211_BL5] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PRN211_BL5] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PRN211_BL5] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PRN211_BL5] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PRN211_BL5] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PRN211_BL5] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PRN211_BL5] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PRN211_BL5] SET  MULTI_USER 
GO
ALTER DATABASE [PRN211_BL5] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PRN211_BL5] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PRN211_BL5] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PRN211_BL5] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PRN211_BL5] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PRN211_BL5] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PRN211_BL5] SET QUERY_STORE = OFF
GO
USE [PRN211_BL5]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 8/23/2023 7:55:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[Cart_Id] [int] IDENTITY(1,1) NOT NULL,
	[User_Id] [int] NOT NULL,
	[ProductDetail_Id] [int] NOT NULL,
	[Quantity] [int] NULL,
	[Total] [int] NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[Cart_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 8/23/2023 7:55:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Category_Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Image] [nvarchar](max) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Category_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Color]    Script Date: 8/23/2023 7:55:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Color](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](50) NULL,
	[Product_Id] [int] NULL,
 CONSTRAINT [PK_Color] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 8/23/2023 7:55:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderDetail_Id] [int] IDENTITY(1,1) NOT NULL,
	[Order_Id] [int] NULL,
	[ProductDetail_Id] [int] NULL,
	[Quantity] [int] NULL,
	[Total] [int] NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderDetail_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 8/23/2023 7:55:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Order_Id] [int] IDENTITY(1,1) NOT NULL,
	[User_Id] [int] NULL,
	[OrderDate] [date] NULL,
	[Status] [int] NULL,
	[Total] [int] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Order_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductDetail]    Script Date: 8/23/2023 7:55:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductDetail](
	[ProductDetail_Id] [int] IDENTITY(1,1) NOT NULL,
	[Product_Id] [int] NULL,
	[Color_Id] [int] NULL,
	[Size_Id] [int] NULL,
	[Image] [nvarchar](max) NULL,
	[Thumbnail_Id] [int] NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK_ProductDetail] PRIMARY KEY CLUSTERED 
(
	[ProductDetail_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 8/23/2023 7:55:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Product_Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Cate_Id] [int] NOT NULL,
	[Price] [money] NULL,
	[Image] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Product_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 8/23/2023 7:55:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Role_Id] [int] IDENTITY(1,1) NOT NULL,
	[Role_Name] [nvarchar](20) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Role_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Size]    Script Date: 8/23/2023 7:55:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Size](
	[Size_Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](10) NULL,
	[Product_Id] [int] NULL,
 CONSTRAINT [PK_Size] PRIMARY KEY CLUSTERED 
(
	[Size_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Thumbnail]    Script Date: 8/23/2023 7:55:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Thumbnail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Thumbnail] [nvarchar](max) NULL,
 CONSTRAINT [PK_Thumbnail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 8/23/2023 7:55:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[User_Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[FullName] [nvarchar](100) NULL,
	[Phone] [varchar](20) NULL,
	[Gender] [bit] NULL,
	[Dob] [datetime] NULL,
	[Avatar] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Role] [int] NULL,
	[Status] [bit] NULL,
	[CodeVerify] [varchar](10) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[User_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Category_Id], [Name], [Image]) VALUES (1, N'Áo T-Shirt', N't-shirt.jpg')
INSERT [dbo].[Category] ([Category_Id], [Name], [Image]) VALUES (2, N'Áo Polo', N'polo1.jpg')
INSERT [dbo].[Category] ([Category_Id], [Name], [Image]) VALUES (3, N'Áo Thể Thao', N'sport.jpg')
INSERT [dbo].[Category] ([Category_Id], [Name], [Image]) VALUES (6, N'Quần dài', N'quan-dai2.jpg')
INSERT [dbo].[Category] ([Category_Id], [Name], [Image]) VALUES (7, N'Quần Thể Thao', N'quan-sport.jpg')
INSERT [dbo].[Category] ([Category_Id], [Name], [Image]) VALUES (8, N'Quần Jeans', N'quan-jean.jpg')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Color] ON 

INSERT [dbo].[Color] ([Id], [Name], [Product_Id]) VALUES (1, N'Gray                                              ', 10)
INSERT [dbo].[Color] ([Id], [Name], [Product_Id]) VALUES (2, N'Black                                             ', 10)
INSERT [dbo].[Color] ([Id], [Name], [Product_Id]) VALUES (3, N'Green                                             ', 12)
INSERT [dbo].[Color] ([Id], [Name], [Product_Id]) VALUES (4, N'White                                             ', 12)
INSERT [dbo].[Color] ([Id], [Name], [Product_Id]) VALUES (5, N'Black                                             ', 5)
INSERT [dbo].[Color] ([Id], [Name], [Product_Id]) VALUES (6, N'Gray                                              ', 5)
INSERT [dbo].[Color] ([Id], [Name], [Product_Id]) VALUES (7, N'White                                             ', 8)
INSERT [dbo].[Color] ([Id], [Name], [Product_Id]) VALUES (8, N'Black                                             ', 8)
INSERT [dbo].[Color] ([Id], [Name], [Product_Id]) VALUES (9, N'Brown                                             ', 8)
INSERT [dbo].[Color] ([Id], [Name], [Product_Id]) VALUES (10, N'White                                             ', 9)
INSERT [dbo].[Color] ([Id], [Name], [Product_Id]) VALUES (11, N'Black                                             ', 9)
INSERT [dbo].[Color] ([Id], [Name], [Product_Id]) VALUES (12, N'Black                                             ', 13)
INSERT [dbo].[Color] ([Id], [Name], [Product_Id]) VALUES (13, N'Black                                             ', 14)
INSERT [dbo].[Color] ([Id], [Name], [Product_Id]) VALUES (14, N'Blue                                              ', 14)
INSERT [dbo].[Color] ([Id], [Name], [Product_Id]) VALUES (15, N'Blue                                              ', 15)
INSERT [dbo].[Color] ([Id], [Name], [Product_Id]) VALUES (16, N'Black                                             ', 15)
INSERT [dbo].[Color] ([Id], [Name], [Product_Id]) VALUES (17, N'Gray                                              ', 16)
INSERT [dbo].[Color] ([Id], [Name], [Product_Id]) VALUES (18, N'Gray                                              ', 17)
INSERT [dbo].[Color] ([Id], [Name], [Product_Id]) VALUES (19, N'Black                                             ', 17)
SET IDENTITY_INSERT [dbo].[Color] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductDetail] ON 

INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (1, 10, 1, 1, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/July2023/ATCB.BA-3.jpg', 1, 5)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (2, 10, 1, 2, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/July2023/ATCB.BA-3.jpg', 1, 5)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (3, 10, 1, 3, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/July2023/ATCB.BA-3.jpg', 1, 2)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (4, 10, 2, 2, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/July2023/ATCB.BA-1_92.jpg', 2, 3)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (5, 10, 2, 3, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/July2023/ATCB.BA-1_92.jpg', 2, 5)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (6, 12, 3, 4, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/AT220-thumb-4.jpg', 3, 3)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (7, 12, 3, 5, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/AT220-thumb-4.jpg', 3, 3)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (8, 12, 3, 6, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/AT220-thumb-4.jpg', 3, 3)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (9, 12, 4, 4, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/AT220-thumb-2.jpg', 4, 2)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (10, 12, 4, 5, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/AT220-thumb-2.jpg', 4, 2)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (11, 12, 4, 6, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/AT220-thumb-2.jpg', 4, 3)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (12, 12, 4, 7, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/AT220-thumb-2.jpg', 4, 3)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (13, 12, 4, 8, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/AT220-thumb-2.jpg', 4, 2)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (14, 5, 5, 9, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/_CMM0460_29.jpg', 5, 3)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (15, 5, 5, 10, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/_CMM0460_29.jpg', 5, 3)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (16, 5, 5, 11, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/_CMM0460_29.jpg', 5, 4)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (17, 5, 6, 10, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/_CMM0646.jpg', 6, 10)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (18, 8, 7, 12, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/APL100-EDIT-30.jpg', 7, 3)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (19, 8, 7, 13, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/APL100-EDIT-30.jpg', 7, 3)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (20, 8, 7, 14, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/APL100-EDIT-30.jpg', 7, 3)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (21, 8, 8, 12, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/APL100-thumb-4.jpg', 8, 3)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (22, 8, 8, 13, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/APL100-thumb-4.jpg', 8, 2)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (23, 8, 9, 14, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/APL100-thumb-6.jpg', 9, 5)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (24, 8, 9, 15, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/APL100-thumb-6.jpg', 9, 1)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (25, 9, 10, 16, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/May2023/1426x2100_(4).jpg', 10, 5)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (26, 9, 10, 17, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/May2023/1426x2100_(4).jpg', 10, 5)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (27, 9, 11, 17, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/May2023/17-0_64-copy1.jpg', 11, 10)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (28, 13, 12, 18, N'https://media.coolmate.me/cdn-cgi/image/quality=80,format=auto/uploads/February2023/advance6.913.jpg', 12, 10)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (29, 13, 12, 19, N'https://media.coolmate.me/cdn-cgi/image/quality=80,format=auto/uploads/February2023/advance6.913.jpg', 12, 5)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (30, 13, 12, 20, N'https://media.coolmate.me/cdn-cgi/image/quality=80,format=auto/uploads/February2023/advance6.913.jpg', 12, 5)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (31, 14, 13, 21, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/straight-048-1.jpg', 13, 5)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (32, 14, 13, 22, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/straight-048-1.jpg', 13, 5)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (35, 14, 14, 21, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/March2023/Straight-Garment-frontlight1.jpg', 14, 5)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (36, 14, 14, 22, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/March2023/Straight-Garment-frontlight1.jpg', 14, 5)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (37, 15, 15, 23, N'https://media.coolmate.me/cdn-cgi/image/quality=80,format=auto/uploads/November2022/quan-short-promax-s1-xanh-navy-4.jpg', 15, 4)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (38, 15, 15, 24, N'https://media.coolmate.me/cdn-cgi/image/quality=80,format=auto/uploads/November2022/quan-short-promax-s1-xanh-navy-4.jpg', 15, 6)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (39, 15, 16, 23, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/May2022/thumb_quan_promax_den.jpg', 16, 3)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (40, 15, 16, 25, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/May2022/thumb_quan_promax_den.jpg', 16, 7)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (41, 16, 17, 26, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/June2023/Drop_Arm_Gym_Powerfit_Xam_2.jpg', 17, 5)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (42, 16, 17, 27, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/June2023/Drop_Arm_Gym_Powerfit_Xam_2.jpg', 17, 5)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (43, 17, 18, 28, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/_CMM6466.jpg', 18, 3)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (44, 17, 18, 29, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/_CMM6466.jpg', 18, 2)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (45, 17, 19, 28, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/June2023/kaki_excool_-_mau_den-5.jpg', 19, 3)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (46, 17, 19, 29, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/June2023/kaki_excool_-_mau_den-5.jpg', 19, 2)
SET IDENTITY_INSERT [dbo].[ProductDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Product_Id], [Name], [Cate_Id], [Price], [Image], [Description], [Status], [Quantity]) VALUES (5, N'Quần dài UT Pants', 6, 100000.0000, N'https://media.coolmate.me/cdn-cgi/image/quality=80,format=auto/uploads/August2023/_CMM0646.jpg', N'Quần dài UT Pants', 1, 20)
INSERT [dbo].[Products] ([Product_Id], [Name], [Cate_Id], [Price], [Image], [Description], [Status], [Quantity]) VALUES (8, N'Polo Pique Basic Cotton 100%', 2, 300000.0000, N'https://media.coolmate.me/cdn-cgi/image/quality=80,format=auto/uploads/August2023/APL100-EDIT-28.jpg', N'Polo Pique Basic Cotton 100%', 1, 20)
INSERT [dbo].[Products] ([Product_Id], [Name], [Cate_Id], [Price], [Image], [Description], [Status], [Quantity]) VALUES (9, N'Áo sát nách thể thao nam Dri-Breathe thoáng mát', 3, 150000.0000, N'https://media.coolmate.me/cdn-cgi/image/quality=80/image/May2023/thumb_sat_nach_trang.jpg', N'Áo sát nách thể thao nam Dri-Breathe thoáng mát', 1, 20)
INSERT [dbo].[Products] ([Product_Id], [Name], [Cate_Id], [Price], [Image], [Description], [Status], [Quantity]) VALUES (10, N'Áo thun chạy bộ Basics', 1, 200000.0000, N'https://media.coolmate.me/cdn-cgi/image/quality=80,format=auto/uploads/July2023/ATCB.BA-3.jpg', N'Áo thun chạy bộ Basics', 1, 20)
INSERT [dbo].[Products] ([Product_Id], [Name], [Cate_Id], [Price], [Image], [Description], [Status], [Quantity]) VALUES (12, N'T-Shirt Basic Cotton 100% 220gsm', 1, 180000.0000, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/AT220-thumb-4.jpg', N'T-Shirt Basic Cotton 100% 220gsm', 1, 20)
INSERT [dbo].[Products] ([Product_Id], [Name], [Cate_Id], [Price], [Image], [Description], [Status], [Quantity]) VALUES (13, N'Quần shorts chạy bộ Advanced Fast & Free Run', 7, 350000.0000, N'https://media.coolmate.me/cdn-cgi/image/quality=80,format=auto/uploads/February2023/advance6.913.jpg', N'Quần shorts chạy bộ Advanced Fast & Free Run', 1, 20)
INSERT [dbo].[Products] ([Product_Id], [Name], [Cate_Id], [Price], [Image], [Description], [Status], [Quantity]) VALUES (14, N'Quần Jeans dáng Straight', 8, 570000.0000, N'https://media.coolmate.me/cdn-cgi/image/quality=80,format=auto/uploads/March2023/Straight-Garment-frontzz.jpg', N'Quần Jeans dáng Straight', 1, 20)
INSERT [dbo].[Products] ([Product_Id], [Name], [Cate_Id], [Price], [Image], [Description], [Status], [Quantity]) VALUES (15, N'Quần short nam thể thao ProMax-S1 thoáng khí', 7, 180000.0000, N'https://media.coolmate.me/cdn-cgi/image/quality=80,format=auto/uploads/November2022/quan-short-promax-s1-xanh-navy-4.jpg', N'Quần short nam thể thao ProMax-S1 thoáng khí', 1, 20)
INSERT [dbo].[Products] ([Product_Id], [Name], [Cate_Id], [Price], [Image], [Description], [Status], [Quantity]) VALUES (16, N'Áo Drop Arm Gym Powerfit', 3, 190000.0000, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/June2023/Drop_Arm_Gym_Powerfit_Xam_1.jpg', N'Áo Drop Arm Gym Powerfit', 1, 10)
INSERT [dbo].[Products] ([Product_Id], [Name], [Cate_Id], [Price], [Image], [Description], [Status], [Quantity]) VALUES (17, N'Quần dài Kaki Excool co giãn', 6, 460000.0000, N'https://media.coolmate.me/cdn-cgi/image/quality=80,format=auto/uploads/August2023/_CMM6466.jpg', N'Quần dài Kaki Excool co giãn', 1, 10)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Role_Id], [Role_Name]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([Role_Id], [Role_Name]) VALUES (2, N'Customer')
INSERT [dbo].[Roles] ([Role_Id], [Role_Name]) VALUES (3, N'Sale')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Size] ON 

INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (1, N'S         ', 10)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (2, N'M         ', 10)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (3, N'L         ', 10)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (4, N'S         ', 12)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (5, N'M         ', 12)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (6, N'L         ', 12)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (7, N'XL        ', 12)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (8, N'2XL       ', 12)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (9, N'M         ', 5)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (10, N'L         ', 5)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (11, N'XL        ', 5)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (12, N'S         ', 8)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (13, N'M         ', 8)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (14, N'L         ', 8)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (15, N'XL        ', 8)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (16, N'S         ', 9)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (17, N'M         ', 9)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (18, N'M         ', 13)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (19, N'L         ', 13)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (20, N'XL        ', 13)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (21, N'M         ', 14)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (22, N'L         ', 14)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (23, N'M         ', 15)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (24, N'L         ', 15)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (25, N'XL        ', 15)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (26, N'L         ', 16)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (27, N'XL        ', 16)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (28, N'L         ', 17)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (29, N'XL        ', 17)
SET IDENTITY_INSERT [dbo].[Size] OFF
GO
SET IDENTITY_INSERT [dbo].[Thumbnail] ON 

INSERT [dbo].[Thumbnail] ([Id], [Thumbnail]) VALUES (1, N'https://media.coolmate.me/cdn-cgi/image/quality=80,format=auto/uploads/July2023/ATCBBS-MODEL-15.jpg')
INSERT [dbo].[Thumbnail] ([Id], [Thumbnail]) VALUES (2, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/July2023/ATCBBS-MODEL-1_8.jpg')
INSERT [dbo].[Thumbnail] ([Id], [Thumbnail]) VALUES (3, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/AT220-27.jpg')
INSERT [dbo].[Thumbnail] ([Id], [Thumbnail]) VALUES (4, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/AT220-21.jpg')
INSERT [dbo].[Thumbnail] ([Id], [Thumbnail]) VALUES (5, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/_CMM0458.jpg')
INSERT [dbo].[Thumbnail] ([Id], [Thumbnail]) VALUES (6, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/thumb_CMM0608_28.jpg')
INSERT [dbo].[Thumbnail] ([Id], [Thumbnail]) VALUES (7, N'https://media.coolmate.me/cdn-cgi/image/quality=80,format=auto/uploads/August2023/APL100-EDIT-28.jpg')
INSERT [dbo].[Thumbnail] ([Id], [Thumbnail]) VALUES (8, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/APL100-EDIT-44.jpg')
INSERT [dbo].[Thumbnail] ([Id], [Thumbnail]) VALUES (9, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/APL100-EDIT-2.jpg')
INSERT [dbo].[Thumbnail] ([Id], [Thumbnail]) VALUES (10, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/May2023/thumb_sat_nach_trang2.jpg')
INSERT [dbo].[Thumbnail] ([Id], [Thumbnail]) VALUES (11, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/May2023/thumb_sat_nach_den2.jpg')
INSERT [dbo].[Thumbnail] ([Id], [Thumbnail]) VALUES (12, N'https://media.coolmate.me/cdn-cgi/image/quality=80,format=auto/uploads/February2023/advanced_fast-1.jpg')
INSERT [dbo].[Thumbnail] ([Id], [Thumbnail]) VALUES (13, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/May2023/Quan_Jeans_dang_Straight-thumb-1.jpg')
INSERT [dbo].[Thumbnail] ([Id], [Thumbnail]) VALUES (14, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/May2023/jenas-copper-straight-s1.jpg')
INSERT [dbo].[Thumbnail] ([Id], [Thumbnail]) VALUES (15, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/May2022/DSC05731-copy.jpg')
INSERT [dbo].[Thumbnail] ([Id], [Thumbnail]) VALUES (16, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/May2022/DSC05891-copy.jpg')
INSERT [dbo].[Thumbnail] ([Id], [Thumbnail]) VALUES (17, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/June2023/Drop_Arm_Gym_Powerfit_Xam_3.jpg')
INSERT [dbo].[Thumbnail] ([Id], [Thumbnail]) VALUES (18, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/August2023/_CMM6480.jpg')
INSERT [dbo].[Thumbnail] ([Id], [Thumbnail]) VALUES (19, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/June2023/1kaki_excool_-_mau_den-4.jpg')
SET IDENTITY_INSERT [dbo].[Thumbnail] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([User_Id], [Email], [Password], [FullName], [Phone], [Gender], [Dob], [Avatar], [Address], [Role], [Status], [CodeVerify]) VALUES (2, N'test@gmail.com', N'fcea920f7412b5da7be0cf42b8c93759', N'test', NULL, NULL, NULL, N'UserID _1660f525d-d8f8-4557-9a6f-f053926892bf_Sample_User_Icon.png', NULL, 2, 1, NULL)
INSERT [dbo].[Users] ([User_Id], [Email], [Password], [FullName], [Phone], [Gender], [Dob], [Avatar], [Address], [Role], [Status], [CodeVerify]) VALUES (3, N'test2@gmail.com', N'fcea920f7412b5da7be0cf42b8c93759', N'test2', NULL, NULL, NULL, N'UserID _1660f525d-d8f8-4557-9a6f-f053926892bf_Sample_User_Icon.png', NULL, 1, 1, NULL)
INSERT [dbo].[Users] ([User_Id], [Email], [Password], [FullName], [Phone], [Gender], [Dob], [Avatar], [Address], [Role], [Status], [CodeVerify]) VALUES (4, N'test3@gmail.com', N'fcea920f7412b5da7be0cf42b8c93759', N'test3', NULL, NULL, NULL, N'UserID _1660f525d-d8f8-4557-9a6f-f053926892bf_Sample_User_Icon.png', NULL, 3, 1, N'3534')
INSERT [dbo].[Users] ([User_Id], [Email], [Password], [FullName], [Phone], [Gender], [Dob], [Avatar], [Address], [Role], [Status], [CodeVerify]) VALUES (5, N'longvu131102@gmail.com', N'f788a33019bf120c31b06c8a5da2dcc5', N'vu', NULL, NULL, NULL, N'UserID _1660f525d-d8f8-4557-9a6f-f053926892bf_Sample_User_Icon.png', NULL, 2, 1, NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_ProductDetail] FOREIGN KEY([ProductDetail_Id])
REFERENCES [dbo].[ProductDetail] ([ProductDetail_Id])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_ProductDetail]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_Users] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Users] ([User_Id])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_Users]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Orders] FOREIGN KEY([Order_Id])
REFERENCES [dbo].[Orders] ([Order_Id])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Orders]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_ProductDetail] FOREIGN KEY([ProductDetail_Id])
REFERENCES [dbo].[ProductDetail] ([ProductDetail_Id])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_ProductDetail]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Users] ([User_Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users]
GO
ALTER TABLE [dbo].[ProductDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProductDetail_Color] FOREIGN KEY([Color_Id])
REFERENCES [dbo].[Color] ([Id])
GO
ALTER TABLE [dbo].[ProductDetail] CHECK CONSTRAINT [FK_ProductDetail_Color]
GO
ALTER TABLE [dbo].[ProductDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProductDetail_Products] FOREIGN KEY([Product_Id])
REFERENCES [dbo].[Products] ([Product_Id])
GO
ALTER TABLE [dbo].[ProductDetail] CHECK CONSTRAINT [FK_ProductDetail_Products]
GO
ALTER TABLE [dbo].[ProductDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProductDetail_Size] FOREIGN KEY([Size_Id])
REFERENCES [dbo].[Size] ([Size_Id])
GO
ALTER TABLE [dbo].[ProductDetail] CHECK CONSTRAINT [FK_ProductDetail_Size]
GO
ALTER TABLE [dbo].[ProductDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProductDetail_Thumbnail] FOREIGN KEY([Thumbnail_Id])
REFERENCES [dbo].[Thumbnail] ([Id])
GO
ALTER TABLE [dbo].[ProductDetail] CHECK CONSTRAINT [FK_ProductDetail_Thumbnail]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Category] FOREIGN KEY([Cate_Id])
REFERENCES [dbo].[Category] ([Category_Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Category]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles1] FOREIGN KEY([Role])
REFERENCES [dbo].[Roles] ([Role_Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles1]
GO
USE [master]
GO
ALTER DATABASE [PRN211_BL5] SET  READ_WRITE 
GO
