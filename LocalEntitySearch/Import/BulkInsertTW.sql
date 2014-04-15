USE [LocalSearch]
GO

/****** Object:  Table [dbo].[HKPublishedData]    Script Date: 11/14/2013 1:53:02 PM ******/
DROP TABLE [dbo].[TWPublishedData]
GO




/****** Object:  Table [dbo].[HKPublishedData]    Script Date: 11/14/2013 1:52:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TWPublishedData](
	[Identifiers] [nvarchar](1024) NULL,
	[MasterId] [nvarchar](50) NULL,
	[ExternalId] [nvarchar](1024) NULL,
	[EntityName] [nvarchar](500) NULL,
	[Odpdescription] [nvarchar](1024) NULL,
	[FeedsMulti8] [nvarchar](1024) NULL,
	[FeedsMulti9] [nvarchar](1024) NULL
) ON [PRIMARY]

GO

  BULK INSERT [LocalSearch].[dbo].[TWPublishedData]
FROM '\\lmax-vm9\d$\users\likel\tools\DBFile\TWDB.txt.unicode.txt'
WITH (
FIELDTERMINATOR = '\t',
ROWTERMINATOR = '\n',
datafiletype='widechar',
ERRORFILE = 'D:\users\likel\DBFile\mypubbishDatatw15.log' 
);