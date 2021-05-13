# BlogWebSite

Please change the Connection String in AppSettings.json file .

Below is the databse script , Run it in SSMS and then RUN Application

USE [master]
GO
/****** Object:  Database [BlogManagement] ******/
CREATE DATABASE [BlogManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BlogManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER01\MSSQL\DATA\BlogManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BlogManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER01\MSSQL\DATA\BlogManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [BlogManagement] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BlogManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BlogManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BlogManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BlogManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BlogManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BlogManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [BlogManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BlogManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BlogManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BlogManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BlogManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BlogManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BlogManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BlogManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BlogManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BlogManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BlogManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BlogManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BlogManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BlogManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BlogManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BlogManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BlogManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BlogManagement] SET RECOVERY FULL 
GO
ALTER DATABASE [BlogManagement] SET  MULTI_USER 
GO
ALTER DATABASE [BlogManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BlogManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BlogManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BlogManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BlogManagement] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'BlogManagement', N'ON'
GO
ALTER DATABASE [BlogManagement] SET QUERY_STORE = OFF
GO
USE [BlogManagement]
GO
/****** Object:  Table [dbo].[Author]    Script Date: 5/13/2021 6:14:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Author](
	[AuthorId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED 
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogDetail]    Script Date: 5/13/2021 6:14:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogDetail](
	[BlogId] [int] IDENTITY(1,1) NOT NULL,
	[Tilte] [varchar](50) NOT NULL,
	[Category] [varchar](50) NULL,
	[BlogContent] [varchar](max) NOT NULL,
	[AuthorId] [int] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_BlogDetail] PRIMARY KEY CLUSTERED 
(
	[BlogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Author] ON 
GO
INSERT [dbo].[Author] ([AuthorId], [UserName], [Password], [CreatedAt], [CreatedBy]) VALUES (1, N'guptasrishti1995@gmail.com', N'gupta@33', CAST(N'2021-05-13T15:45:11.453' AS DateTime), N'guptasrishti1995@gmail.com')
GO
INSERT [dbo].[Author] ([AuthorId], [UserName], [Password], [CreatedAt], [CreatedBy]) VALUES (2, N'generalUser@gmail.com', N'grapecity', CAST(N'2021-05-13T18:11:40.057' AS DateTime), N'generalUser@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[Author] OFF
GO
SET IDENTITY_INSERT [dbo].[BlogDetail] ON 
GO
INSERT [dbo].[BlogDetail] ([BlogId], [Tilte], [Category], [BlogContent], [AuthorId], [CreatedAt], [CreatedBy]) VALUES (3, N'My Updated Blog', N'General Info', N'Successfull pushed first blog', 1, CAST(N'2021-05-13T17:59:36.057' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[BlogDetail] OFF
GO
ALTER TABLE [dbo].[Author] ADD  CONSTRAINT [DF_Author_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[BlogDetail] ADD  CONSTRAINT [DF_BlogDetail_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[BlogDetail]  WITH CHECK ADD  CONSTRAINT [FK_BlogDetail_Author] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Author] ([AuthorId])
GO
ALTER TABLE [dbo].[BlogDetail] CHECK CONSTRAINT [FK_BlogDetail_Author]
GO
USE [master]
GO
ALTER DATABASE [BlogManagement] SET  READ_WRITE 
GO
