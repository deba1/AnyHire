USE [master]
GO
/****** Object:  Database [AnyHireDb]    Script Date: 17/12/2019 11:06:36 am ******/
CREATE DATABASE [AnyHireDb]
 CONTAINMENT = NONE
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AnyHireDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AnyHireDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AnyHireDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AnyHireDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AnyHireDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AnyHireDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [AnyHireDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AnyHireDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AnyHireDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AnyHireDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AnyHireDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AnyHireDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AnyHireDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AnyHireDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AnyHireDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AnyHireDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AnyHireDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AnyHireDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AnyHireDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AnyHireDb] SET DB_CHAINING OFF 
GO
USE [AnyHireDb]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 17/12/2019 11:06:36 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](70) NOT NULL,
	[Email] [nvarchar](90) NOT NULL,
	[Mobile] [int] NOT NULL,
	[Gender] [nvarchar](6) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Address] [nvarchar](300) NOT NULL,
	[ProfilePicture] [nvarchar](max) NULL,
	[UserTypeId] [int] NOT NULL,
	[CustomerId] [int] NULL,
	[ServiceProviderId] [int] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Appointments]    Script Date: 17/12/2019 11:06:36 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[PackageId] [int] NOT NULL,
	[Location] [nvarchar](300) NOT NULL,
	[Time] [datetime] NOT NULL,
	[Completed] [bit] NOT NULL,
 CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 17/12/2019 11:06:36 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NID] [int] NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 17/12/2019 11:06:36 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[PackageId] [int] NOT NULL,
	[Stars] [int] NOT NULL,
	[Disliked] [bit] NOT NULL,
 CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notices]    Script Date: 17/12/2019 11:06:36 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AdminId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[Warning] [bit] NOT NULL,
	[IsRead] [bit] NOT NULL,
 CONSTRAINT [PK_Notices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Packages]    Script Date: 17/12/2019 11:06:36 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Packages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[ServiceId] [int] NOT NULL,
	[ServiceProviderId] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[ImagePath] [nvarchar](500) NULL,
	[Price] [numeric](10, 2) NOT NULL,
 CONSTRAINT [PK_Packages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceProviders]    Script Date: 17/12/2019 11:06:36 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceProviders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NID] [int] NOT NULL,
	[Coverage] [nvarchar](max) NOT NULL,
	[JoinDate] [date] NOT NULL,
	[Skills] [nvarchar](max) NOT NULL,
	[Revenue] [numeric](18, 0) NOT NULL,
	[Commission] [numeric](18, 0) NOT NULL,
 CONSTRAINT [PK_SurviceProviders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 17/12/2019 11:06:36 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ImagePath] [varchar](100) NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 17/12/2019 11:06:36 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AppointmentId] [int] NOT NULL,
	[ServiceProviderId] [int] NOT NULL,
	[TotalAmount] [numeric](18, 2) NOT NULL,
	[ServiceProviderRevenue] [numeric](18, 2) NOT NULL,
	[CompanyRevenue] [numeric](18, 2) NOT NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTypes]    Script Date: 17/12/2019 11:06:36 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([Id], [Username], [Password], [Name], [Email], [Mobile], [Gender], [DateOfBirth], [Address], [ProfilePicture], [UserTypeId], [CustomerId], [ServiceProviderId], [Active]) VALUES (1, N'admin', N'admin', N'Admin', N'webmaster@anyhire.com', 1763923789, N'Male', CAST(N'2019-11-19' AS Date), N'Bangladesh', NULL, 1, NULL, NULL, 1)
INSERT [dbo].[Accounts] ([Id], [Username], [Password], [Name], [Email], [Mobile], [Gender], [DateOfBirth], [Address], [ProfilePicture], [UserTypeId], [CustomerId], [ServiceProviderId], [Active]) VALUES (3, N'hori', N'1234', N'Hobbur', N'hori@femail.com', 1234567890, N'Male', CAST(N'1994-02-07' AS Date), N'Kuril, Dhaka', NULL, 2, 4, NULL, 1)
INSERT [dbo].[Accounts] ([Id], [Username], [Password], [Name], [Email], [Mobile], [Gender], [DateOfBirth], [Address], [ProfilePicture], [UserTypeId], [CustomerId], [ServiceProviderId], [Active]) VALUES (4, N'horibur', N'81dc9bdb52d04dc20036dbd8313ed055', N'horibur', N'hori@gmail.com', 1763222333, N'Male', CAST(N'2019-11-07' AS Date), N'Kuril, Dhaka', N'', 3, NULL, 2, 1)
INSERT [dbo].[Accounts] ([Id], [Username], [Password], [Name], [Email], [Mobile], [Gender], [DateOfBirth], [Address], [ProfilePicture], [UserTypeId], [CustomerId], [ServiceProviderId], [Active]) VALUES (5, N'sariful', N'81dc9bdb52d04dc20036dbd8313ed055', N'Sariful Islam', N'shorif@gmail.com', 17777778, N'Male', CAST(N'1996-02-07' AS Date), N'Kuril, Dhaka', NULL, 2, 5, NULL, 1)
INSERT [dbo].[Accounts] ([Id], [Username], [Password], [Name], [Email], [Mobile], [Gender], [DateOfBirth], [Address], [ProfilePicture], [UserTypeId], [CustomerId], [ServiceProviderId], [Active]) VALUES (8, N'debashish', N'81dc9bdb52d04dc20036dbd8313ed055', N'Debashish', N'dsarker@hotmail.com', 1763923789, N'Male', CAST(N'2019-11-07' AS Date), N'Kuril, Dhaka', NULL, 3, NULL, 3, 1)
SET IDENTITY_INSERT [dbo].[Accounts] OFF
SET IDENTITY_INSERT [dbo].[Appointments] ON 

INSERT [dbo].[Appointments] ([Id], [CustomerId], [PackageId], [Location], [Time], [Completed]) VALUES (1, 3, 2, N'Kuril', CAST(N'2019-11-29T14:22:00.000' AS DateTime), 1)
INSERT [dbo].[Appointments] ([Id], [CustomerId], [PackageId], [Location], [Time], [Completed]) VALUES (2, 3, 2, N'Kuril', CAST(N'2019-11-29T17:55:00.000' AS DateTime), 0)
INSERT [dbo].[Appointments] ([Id], [CustomerId], [PackageId], [Location], [Time], [Completed]) VALUES (3, 3, 2, N'Kuril', CAST(N'2019-11-29T17:55:00.000' AS DateTime), 0)
INSERT [dbo].[Appointments] ([Id], [CustomerId], [PackageId], [Location], [Time], [Completed]) VALUES (4, 3, 2, N'Kuril', CAST(N'2019-11-29T13:33:00.000' AS DateTime), 0)
INSERT [dbo].[Appointments] ([Id], [CustomerId], [PackageId], [Location], [Time], [Completed]) VALUES (5, 3, 2, N'Kuril', CAST(N'2019-11-29T01:00:00.000' AS DateTime), 0)
INSERT [dbo].[Appointments] ([Id], [CustomerId], [PackageId], [Location], [Time], [Completed]) VALUES (6, 3, 2, N'Kuril', CAST(N'2019-11-29T01:00:00.000' AS DateTime), 1)
INSERT [dbo].[Appointments] ([Id], [CustomerId], [PackageId], [Location], [Time], [Completed]) VALUES (7, 3, 2, N'Kuril', CAST(N'2019-11-29T01:00:00.000' AS DateTime), 0)
INSERT [dbo].[Appointments] ([Id], [CustomerId], [PackageId], [Location], [Time], [Completed]) VALUES (8, 3, 3, N'Bashundhara', CAST(N'2019-11-29T12:00:00.000' AS DateTime), 0)
INSERT [dbo].[Appointments] ([Id], [CustomerId], [PackageId], [Location], [Time], [Completed]) VALUES (9, 3, 3, N'Bashundhara', CAST(N'2019-11-29T12:00:00.000' AS DateTime), 0)
INSERT [dbo].[Appointments] ([Id], [CustomerId], [PackageId], [Location], [Time], [Completed]) VALUES (10, 3, 2, N'Kuril', CAST(N'2019-11-29T12:00:00.000' AS DateTime), 0)
INSERT [dbo].[Appointments] ([Id], [CustomerId], [PackageId], [Location], [Time], [Completed]) VALUES (11, 3, 3, N'Kuril', CAST(N'2019-11-30T10:00:00.000' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Appointments] OFF
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([Id], [NID]) VALUES (4, 1234567891)
INSERT [dbo].[Customers] ([Id], [NID]) VALUES (5, 1234567892)
SET IDENTITY_INSERT [dbo].[Customers] OFF
SET IDENTITY_INSERT [dbo].[Packages] ON 

INSERT [dbo].[Packages] ([Id], [Title], [ServiceId], [ServiceProviderId], [Description], [ImagePath], [Price]) VALUES (2, N'Appliance Installation 2', 1, 4, N'Weekday: Mon-Wed
Time: 10:00AM-8:00PM
Installation of appliance in 1 room', N'horibur_191127210138.jpg', CAST(800.00 AS Numeric(10, 2)))
INSERT [dbo].[Packages] ([Id], [Title], [ServiceId], [ServiceProviderId], [Description], [ImagePath], [Price]) VALUES (3, N'Painting 1 Room', 2, 4, N'Weekday: Sun-Thu
Time: 10:00AM-8:00PM', N'horibur_191127204521.jpg', CAST(1200.00 AS Numeric(10, 2)))
INSERT [dbo].[Packages] ([Id], [Title], [ServiceId], [ServiceProviderId], [Description], [ImagePath], [Price]) VALUES (4, N'Appliance Installation', 1, 8, N'Weekday: Mon-Wed
Time: 10:00AM-8:00PM
Installation of appliance in 1 room each', NULL, CAST(1200.00 AS Numeric(10, 2)))
SET IDENTITY_INSERT [dbo].[Packages] OFF
SET IDENTITY_INSERT [dbo].[ServiceProviders] ON 

INSERT [dbo].[ServiceProviders] ([Id], [NID], [Coverage], [JoinDate], [Skills], [Revenue], [Commission]) VALUES (2, 1112312313, N'Kuril,Bashundhara', CAST(N'2019-11-25' AS Date), N'Mechanic', CAST(0 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)))
INSERT [dbo].[ServiceProviders] ([Id], [NID], [Coverage], [JoinDate], [Skills], [Revenue], [Commission]) VALUES (3, 1112312314, N'Kuril,Bashundhara', CAST(N'2019-12-10' AS Date), N'Mechanic', CAST(0 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)))
SET IDENTITY_INSERT [dbo].[ServiceProviders] OFF
SET IDENTITY_INSERT [dbo].[Services] ON 

INSERT [dbo].[Services] ([Id], [Name], [ImagePath]) VALUES (1, N'Electrician', N'service_191127224634.jpg')
INSERT [dbo].[Services] ([Id], [Name], [ImagePath]) VALUES (2, N'Labourer', N'service_191127224824.jpg')
INSERT [dbo].[Services] ([Id], [Name], [ImagePath]) VALUES (3, N'Cleaning', N'service_191127224824.jpg')
INSERT [dbo].[Services] ([Id], [Name], [ImagePath]) VALUES (10, N'Labourer', N'service_191127224824.jpg')
SET IDENTITY_INSERT [dbo].[Services] OFF
SET IDENTITY_INSERT [dbo].[Transactions] ON 

INSERT [dbo].[Transactions] ([Id], [AppointmentId], [ServiceProviderId], [TotalAmount], [ServiceProviderRevenue], [CompanyRevenue]) VALUES (1, 1, 4, CAST(850.00 AS Numeric(18, 2)), CAST(800.00 AS Numeric(18, 2)), CAST(50.00 AS Numeric(18, 2)))
INSERT [dbo].[Transactions] ([Id], [AppointmentId], [ServiceProviderId], [TotalAmount], [ServiceProviderRevenue], [CompanyRevenue]) VALUES (2, 6, 4, CAST(850.00 AS Numeric(18, 2)), CAST(800.00 AS Numeric(18, 2)), CAST(50.00 AS Numeric(18, 2)))
SET IDENTITY_INSERT [dbo].[Transactions] OFF
SET IDENTITY_INSERT [dbo].[UserTypes] ON 

INSERT [dbo].[UserTypes] ([Id], [Title]) VALUES (1, N'Admin')
INSERT [dbo].[UserTypes] ([Id], [Title]) VALUES (2, N'Customer')
INSERT [dbo].[UserTypes] ([Id], [Title]) VALUES (3, N'Service Provider')
SET IDENTITY_INSERT [dbo].[UserTypes] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Accounts]    Script Date: 17/12/2019 11:06:36 am ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Accounts] ON [dbo].[Accounts]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [DF_Accounts_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Appointments] ADD  CONSTRAINT [DF_Appointments_Completed]  DEFAULT ((0)) FOR [Completed]
GO
ALTER TABLE [dbo].[Feedback] ADD  CONSTRAINT [DF_Feedback_Stars]  DEFAULT ((0)) FOR [Stars]
GO
ALTER TABLE [dbo].[Feedback] ADD  CONSTRAINT [DF_Feedback_Disliked]  DEFAULT ((0)) FOR [Disliked]
GO
ALTER TABLE [dbo].[Notices] ADD  CONSTRAINT [DF_Notices_Warning]  DEFAULT ((0)) FOR [Warning]
GO
ALTER TABLE [dbo].[Notices] ADD  CONSTRAINT [DF_Notices_IsRead]  DEFAULT ((0)) FOR [IsRead]
GO
ALTER TABLE [dbo].[ServiceProviders] ADD  CONSTRAINT [DF_ServiceProviders_JoinDate]  DEFAULT (getdate()) FOR [JoinDate]
GO
ALTER TABLE [dbo].[ServiceProviders] ADD  CONSTRAINT [DF_ServiceProviders_Revenue]  DEFAULT ((0)) FOR [Revenue]
GO
ALTER TABLE [dbo].[ServiceProviders] ADD  CONSTRAINT [DF_ServiceProviders_Commission]  DEFAULT ((0)) FOR [Commission]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Customers]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_SurviceProviders] FOREIGN KEY([ServiceProviderId])
REFERENCES [dbo].[ServiceProviders] ([Id])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_SurviceProviders]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_UserTypes] FOREIGN KEY([UserTypeId])
REFERENCES [dbo].[UserTypes] ([Id])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_UserTypes]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Accounts] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Accounts] ([Id])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Accounts]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Packages] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Packages] ([Id])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Packages]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Accounts] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Accounts] ([Id])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_Accounts]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Packages] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Packages] ([Id])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_Packages]
GO
ALTER TABLE [dbo].[Notices]  WITH CHECK ADD  CONSTRAINT [FK_NoticesFrom_Accounts] FOREIGN KEY([AdminId])
REFERENCES [dbo].[Accounts] ([Id])
GO
ALTER TABLE [dbo].[Notices] CHECK CONSTRAINT [FK_NoticesFrom_Accounts]
GO
ALTER TABLE [dbo].[Notices]  WITH CHECK ADD  CONSTRAINT [FK_NoticesTo_AccountsUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[Accounts] ([Id])
GO
ALTER TABLE [dbo].[Notices] CHECK CONSTRAINT [FK_NoticesTo_AccountsUsers]
GO
ALTER TABLE [dbo].[Packages]  WITH CHECK ADD  CONSTRAINT [FK_Packages_Accounts] FOREIGN KEY([ServiceProviderId])
REFERENCES [dbo].[Accounts] ([Id])
GO
ALTER TABLE [dbo].[Packages] CHECK CONSTRAINT [FK_Packages_Accounts]
GO
ALTER TABLE [dbo].[Packages]  WITH CHECK ADD  CONSTRAINT [FK_Packages_Services] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([Id])
GO
ALTER TABLE [dbo].[Packages] CHECK CONSTRAINT [FK_Packages_Services]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Accounts] FOREIGN KEY([ServiceProviderId])
REFERENCES [dbo].[Accounts] ([Id])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Accounts]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Appointments] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointments] ([Id])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Appointments]
GO
