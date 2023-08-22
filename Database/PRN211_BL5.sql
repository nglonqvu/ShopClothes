
USE [PRN211_BL5]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 18/08/2023 12:41:15 AM ******/
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
/****** Object:  Table [dbo].[Category]    Script Date: 18/08/2023 12:41:15 AM ******/
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
/****** Object:  Table [dbo].[Color]    Script Date: 18/08/2023 12:41:15 AM ******/
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
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 18/08/2023 12:41:15 AM ******/
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
/****** Object:  Table [dbo].[Orders]    Script Date: 18/08/2023 12:41:15 AM ******/
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
/****** Object:  Table [dbo].[ProductDetail]    Script Date: 18/08/2023 12:41:15 AM ******/
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
/****** Object:  Table [dbo].[Products]    Script Date: 18/08/2023 12:41:15 AM ******/
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
/****** Object:  Table [dbo].[Roles]    Script Date: 18/08/2023 12:41:15 AM ******/
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
/****** Object:  Table [dbo].[Size]    Script Date: 18/08/2023 12:41:15 AM ******/
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
/****** Object:  Table [dbo].[Thumbnail]    Script Date: 18/08/2023 12:41:15 AM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 18/08/2023 12:41:15 AM ******/
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
INSERT [dbo].[Category] ([Category_Id], [Name], [Image]) VALUES (4, N'Áo Jeans', N'ao-khoac-jean-ava.jpg')
INSERT [dbo].[Category] ([Category_Id], [Name], [Image]) VALUES (5, N'Quần Shorts', N'quan-short.jpg')
INSERT [dbo].[Category] ([Category_Id], [Name], [Image]) VALUES (6, N'Quần dài', N'quan-dai2.jpg')
INSERT [dbo].[Category] ([Category_Id], [Name], [Image]) VALUES (7, N'Quần Thể Thao', N'quan-sport.jpg')
INSERT [dbo].[Category] ([Category_Id], [Name], [Image]) VALUES (8, N'Quần Jeans', N'quan-jean.jpg')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Color] ON 

INSERT [dbo].[Color] ([Id], [Name], [Product_Id]) VALUES (1, N'Gray                                              ', 5)
INSERT [dbo].[Color] ([Id], [Name], [Product_Id]) VALUES (2, N'Black                                             ', 10)
SET IDENTITY_INSERT [dbo].[Color] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductDetail] ON 

INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (1, 5, 1, 1, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/July2023/ATCB.BA-3.jpg', 1, 5)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (2, 5, 1, 2, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/July2023/ATCB.BA-3.jpg', 1, 5)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (3, 5, 1, 3, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/July2023/ATCB.BA-3.jpg', 1, 2)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (4, 5, 2, 2, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/July2023/ATCB.BA-1_92.jpg', 2, 3)
INSERT [dbo].[ProductDetail] ([ProductDetail_Id], [Product_Id], [Color_Id], [Size_Id], [Image], [Thumbnail_Id], [Quantity]) VALUES (5, 5, 2, 3, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/July2023/ATCB.BA-1_92.jpg', 2, 5)
SET IDENTITY_INSERT [dbo].[ProductDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Product_Id], [Name], [Cate_Id], [Price], [Image], [Description], [Status], [Quantity]) VALUES (5, N'Quần Dài', 6, 100000.0000, N'quan-dai-2.jpg', N'Quần Dài Nike', 1, 100)
INSERT [dbo].[Products] ([Product_Id], [Name], [Cate_Id], [Price], [Image], [Description], [Status], [Quantity]) VALUES (6, N'Quần Ngắn Adidas', 5, 200000.0000, N'quan-short.jpg', N'Quần Ngắn Adidas', 1, 100)
INSERT [dbo].[Products] ([Product_Id], [Name], [Cate_Id], [Price], [Image], [Description], [Status], [Quantity]) VALUES (7, N'Áo Jeans', 4, 250000.0000, N'ao-jean.jpg', N'Áo Jeans', 1, 100)
INSERT [dbo].[Products] ([Product_Id], [Name], [Cate_Id], [Price], [Image], [Description], [Status], [Quantity]) VALUES (8, N'Áo Polo', 2, 200000.0000, N'ao-polo.jpg', N'Áo Polo', 1, 100)
INSERT [dbo].[Products] ([Product_Id], [Name], [Cate_Id], [Price], [Image], [Description], [Status], [Quantity]) VALUES (9, N'Áo Sport', 3, 150000.0000, N'ao-sport.jpg', N'Áo Sport', 1, 100)
INSERT [dbo].[Products] ([Product_Id], [Name], [Cate_Id], [Price], [Image], [Description], [Status], [Quantity]) VALUES (10, N'Áo T-shirt', 1, 200000.0000, N't-shirt1.jpg', N'Áo T-shirt', 1, 100)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Role_Id], [Role_Name]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([Role_Id], [Role_Name]) VALUES (2, N'Customer')
INSERT [dbo].[Roles] ([Role_Id], [Role_Name]) VALUES (3, N'Sale')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Size] ON 

INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (1, N'S         ', 5)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (2, N'M         ', 5)
INSERT [dbo].[Size] ([Size_Id], [Name], [Product_Id]) VALUES (3, N'L         ', 5)
SET IDENTITY_INSERT [dbo].[Size] OFF
GO
SET IDENTITY_INSERT [dbo].[Thumbnail] ON 

INSERT [dbo].[Thumbnail] ([Id], [Thumbnail]) VALUES (1, N'https://media.coolmate.me/cdn-cgi/image/quality=80,format=auto/uploads/July2023/ATCBBS-MODEL-15.jpg')
INSERT [dbo].[Thumbnail] ([Id], [Thumbnail]) VALUES (2, N'https://media.coolmate.me/cdn-cgi/image/quality=100/uploads/July2023/ATCBBS-MODEL-1_8.jpg')
SET IDENTITY_INSERT [dbo].[Thumbnail] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([User_Id], [Email], [Password], [FullName], [Phone], [Gender], [Dob], [Avatar], [Address], [Role], [Status], [CodeVerify]) VALUES (2, N'test@gmail.com', N'fcea920f7412b5da7be0cf42b8c93759', N'test', NULL, NULL, NULL, N'UserID _1660f525d-d8f8-4557-9a6f-f053926892bf_Sample_User_Icon.png', NULL, 2, 1, NULL)
INSERT [dbo].[Users] ([User_Id], [Email], [Password], [FullName], [Phone], [Gender], [Dob], [Avatar], [Address], [Role], [Status], [CodeVerify]) VALUES (3, N'test2@gmail.com', N'fcea920f7412b5da7be0cf42b8c93759', N'test2', NULL, NULL, NULL, N'UserID _1660f525d-d8f8-4557-9a6f-f053926892bf_Sample_User_Icon.png', NULL, 1, 1, NULL)
INSERT [dbo].[Users] ([User_Id], [Email], [Password], [FullName], [Phone], [Gender], [Dob], [Avatar], [Address], [Role], [Status], [CodeVerify]) VALUES (4, N'test3@gmail.com', N'fcea920f7412b5da7be0cf42b8c93759', N'test3', NULL, NULL, NULL, N'UserID _1660f525d-d8f8-4557-9a6f-f053926892bf_Sample_User_Icon.png', NULL, 3, 1, N'3534')
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
