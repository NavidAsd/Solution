USE [DbTest]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 7/26/2022 11:36:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[InsertTime] [date] NOT NULL,
	[IsRemoved] [bit] NOT NULL,
	[LastUpdate] [date] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GetUsers]    Script Date: 7/26/2022 11:36:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUsers] 
	
AS
SELECT Id,FullName,UserName,Email,InsertTime,LastUpdate,IsRemoved FROM Users Order by Id
GO
/****** Object:  StoredProcedure [dbo].[SP_GetUsers]    Script Date: 7/26/2022 11:36:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetUsers]

AS
SELECT Id,FullName,UserName,Email,InsertTime,LastUpdate,IsRemoved FROM Users ORDER By Id
GO
/****** Object:  StoredProcedure [dbo].[SP_UserLogin]    Script Date: 7/26/2022 11:36:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UserLogin]
    @username VARCHAR(max),
    @password varchar(max)
AS
BEGIN

SET NOCOUNT ON

IF EXISTS(SELECT * FROM Users WHERE UserName = @username AND Password = @password AND IsRemoved='false')
    SELECT 'true' AS UserExists
ELSE
    SELECT 'false' AS UserExists

END
GO
