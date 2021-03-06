USE [master]
GO
/****** Object:  Database [MVCEntityCRUD_db]    Script Date: 13-08-2020 16:31:28 ******/
CREATE DATABASE [MVCEntityCRUD_db]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MVCEntityCRUD_db', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MVCEntityCRUD_db.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MVCEntityCRUD_db_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MVCEntityCRUD_db_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [MVCEntityCRUD_db] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MVCEntityCRUD_db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MVCEntityCRUD_db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET ARITHABORT OFF 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET  MULTI_USER 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MVCEntityCRUD_db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MVCEntityCRUD_db] SET QUERY_STORE = OFF
GO
USE [MVCEntityCRUD_db]
GO
/****** Object:  Table [dbo].[City]    Script Date: 13-08-2020 16:31:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[CityId] [int] IDENTITY(1,1) NOT NULL,
	[StateId] [int] NULL,
	[CityName] [nvarchar](100) NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 13-08-2020 16:31:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[CountryId] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](100) NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hobby]    Script Date: 13-08-2020 16:31:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hobby](
	[HobbyId] [int] IDENTITY(1,1) NOT NULL,
	[HobbyName] [nvarchar](100) NULL,
 CONSTRAINT [PK_Hobby] PRIMARY KEY CLUSTERED 
(
	[HobbyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 13-08-2020 16:31:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](100) NULL,
	[Price] [decimal](18, 0) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[State]    Script Date: 13-08-2020 16:31:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[StateId] [int] IDENTITY(1,1) NOT NULL,
	[CountryId] [int] NULL,
	[StateName] [nvarchar](100) NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 13-08-2020 16:31:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Firstname] [nvarchar](100) NULL,
	[Lastname] [nvarchar](100) NULL,
	[Dateofbirth] [datetime] NULL,
	[Profilepicture] [nvarchar](max) NULL,
	[Age] [numeric](18, 0) NULL,
	[Gender] [numeric](18, 0) NULL,
	[CityId] [int] NULL,
	[Address] [nvarchar](max) NULL,
	[Phoneno] [numeric](18, 0) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserHobbies]    Script Date: 13-08-2020 16:31:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserHobbies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[HobbyId] [int] NULL,
 CONSTRAINT [PK_UserHobbies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProducts]    Script Date: 13-08-2020 16:31:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProducts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[ProductId] [int] NULL,
	[Recievername] [nvarchar](100) NULL,
 CONSTRAINT [PK_UserProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[City]  WITH CHECK ADD  CONSTRAINT [FK_City_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([StateId])
GO
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_City_State]
GO
ALTER TABLE [dbo].[State]  WITH CHECK ADD  CONSTRAINT [FK_State_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([CountryId])
GO
ALTER TABLE [dbo].[State] CHECK CONSTRAINT [FK_State_Country]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_City] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([CityId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_City]
GO
ALTER TABLE [dbo].[UserHobbies]  WITH CHECK ADD  CONSTRAINT [FK_UserHobbies_Hobby] FOREIGN KEY([HobbyId])
REFERENCES [dbo].[Hobby] ([HobbyId])
GO
ALTER TABLE [dbo].[UserHobbies] CHECK CONSTRAINT [FK_UserHobbies_Hobby]
GO
ALTER TABLE [dbo].[UserHobbies]  WITH CHECK ADD  CONSTRAINT [FK_UserHobbies_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserHobbies] CHECK CONSTRAINT [FK_UserHobbies_User]
GO
ALTER TABLE [dbo].[UserProducts]  WITH CHECK ADD  CONSTRAINT [FK_UserProducts_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[UserProducts] CHECK CONSTRAINT [FK_UserProducts_Product]
GO
ALTER TABLE [dbo].[UserProducts]  WITH CHECK ADD  CONSTRAINT [FK_UserProducts_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserProducts] CHECK CONSTRAINT [FK_UserProducts_User]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteUser]    Script Date: 13-08-2020 16:31:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[SP_DeleteUser]
(
@UserId int) As
Begin
	  DELETE FROM UserHobbies WHERE [UserId] =@UserId;
	  DELETE FROM UserProducts WHERE [UserId] =@UserId;
	  DELETE FROM [User] WHERE [UserId] =@UserId;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetUserList]    Script Date: 13-08-2020 16:31:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[SP_GetUserList] as
Begin
 
SELECT * from [dbo].[User]

End
GO
/****** Object:  StoredProcedure [dbo].[SP_InsertHobbies]    Script Date: 13-08-2020 16:31:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[SP_InsertHobbies]
(
@UserId int,
@HobbyId int) As
Begin
    INSERT INTO UserHobbies
	(UserId,HobbyId)
	VALUES
	(@UserId,@HobbyId)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_InsertProducts]    Script Date: 13-08-2020 16:31:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[SP_InsertProducts]
(
@UserId int,
@ProductId int,
@Recievername nvarchar(100)) As
Begin
    INSERT INTO UserProducts
	(UserId,ProductId,Recievername)
	VALUES
	(@UserId,@ProductId,@Recievername)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_InsertUpdateUser]    Script Date: 13-08-2020 16:31:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[SP_InsertUpdateUser]
(
@UserId int,
@Firstname nvarchar(100),
@Lastname nvarchar(100),
@Dateofbirth datetime,
@Profilepicture nvarchar(max),
@Age numeric(18,0),
@Gender numeric(18,0),
@CityId int,
@Address nvarchar(max),
@Phoneno numeric(18,0),
@UserScalarId int out) As
Begin
    if(@UserId > 0)
	BEGIN
      
	  UPDATE [User] SET Firstname = ISNULL(@Firstname,Firstname),Lastname = IsNull(@Lastname,Lastname),
	  Dateofbirth =IsNull(@Dateofbirth,Dateofbirth),Profilepicture = IsNull(@Profilepicture,Profilepicture),Age = IsNull(@Age,Age),
	  Gender = ISNULL (@Gender,Gender),CityId = ISNULL(@CityId,CityId),[Address] = ISNULL(@Address,[Address]),
	  Phoneno = ISNULL(@Phoneno,Phoneno)
	  WHERE UserId = @UserId

	  DELETE FROM UserHobbies WHERE [UserId] =@UserId;
	  DELETE FROM UserProducts WHERE [UserId] =@UserId;

	  SET @UserScalarId =@UserId 
	  select @UserScalarId 
	END
	ELSE
	BEGIN
	  INSERT INTO [User] 
	  (Firstname,Lastname,Dateofbirth,Profilepicture,Age,Gender,CityId,[Address],Phoneno)
	  VALUES
	  (@Firstname,@Lastname,@Dateofbirth,@Profilepicture,@Age,@Gender,@CityId,@Address,@Phoneno)
	  SET @UserScalarId = (SELECT SCOPE_IDENTITY()) 
	  select @UserScalarId 
	END
END
GO
USE [master]
GO
ALTER DATABASE [MVCEntityCRUD_db] SET  READ_WRITE 
GO
