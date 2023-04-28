﻿USE [master]
GO
/****** Object:  Database [NewsPlatform]    Script Date: 28/04/2023 12:11:15 ******/
CREATE DATABASE [NewsPlatform]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NewsPlatform', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\NewsPlatform.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NewsPlatform_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\NewsPlatform_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [NewsPlatform] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NewsPlatform].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NewsPlatform] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NewsPlatform] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NewsPlatform] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NewsPlatform] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NewsPlatform] SET ARITHABORT OFF 
GO
ALTER DATABASE [NewsPlatform] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NewsPlatform] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NewsPlatform] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NewsPlatform] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NewsPlatform] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NewsPlatform] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NewsPlatform] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NewsPlatform] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NewsPlatform] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NewsPlatform] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NewsPlatform] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NewsPlatform] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NewsPlatform] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NewsPlatform] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NewsPlatform] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NewsPlatform] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NewsPlatform] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NewsPlatform] SET RECOVERY FULL 
GO
ALTER DATABASE [NewsPlatform] SET  MULTI_USER 
GO
ALTER DATABASE [NewsPlatform] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NewsPlatform] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NewsPlatform] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NewsPlatform] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NewsPlatform] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NewsPlatform] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'NewsPlatform', N'ON'
GO
ALTER DATABASE [NewsPlatform] SET QUERY_STORE = ON
GO
ALTER DATABASE [NewsPlatform] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [NewsPlatform]
GO
/****** Object:  Table [dbo].[Article]    Script Date: 28/04/2023 12:11:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Article](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Content] [nvarchar](max) NULL,
	[CategroryId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 28/04/2023 12:11:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Article] ADD  CONSTRAINT [DF_Article_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Article]  WITH CHECK ADD  CONSTRAINT [FK_Article_Category] FOREIGN KEY([CategroryId])
REFERENCES [dbo].[Category] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Article] CHECK CONSTRAINT [FK_Article_Category]
GO
USE [master]
GO
ALTER DATABASE [NewsPlatform] SET  READ_WRITE 
GO
