USE [LocalSearch]
GO

/****** Object:  Table [dbo].[HKPublishedData]    Script Date: 11/14/2013 1:53:02 PM ******/
DROP TABLE [dbo].[CNPublishedData]
GO




/****** Object:  Table [dbo].[HKPublishedData]    Script Date: 11/14/2013 1:52:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CNPublishedData](
	[Identifiers] [nvarchar](1024) NULL,
	[MasterId] [nvarchar](50) NULL,
	[ExternalId] [nvarchar](1024) NULL,
	[EntityName] [nvarchar](500) NULL,
	[Odpdescription] [nvarchar](1024) NULL,
	[FeedsMulti8] [nvarchar](1024) NULL,
	[FeedsMulti9] [nvarchar](1024) NULL
) ON [PRIMARY]

GO

/*
USE [LocalSearch]
GO


CREATE NONCLUSTERED INDEX [NonClusteredIndex-20131114-135127] ON [dbo].[HKPublishedData]
(
	[EntityName] ASC,
	[FeedsMulti8] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

*/


  BULK INSERT [LocalSearch].[dbo].[CNPublishedData]
FROM '\\lmax-vm9\d$\users\likel\tools\DBFile\CNDB.txt.unicode.txt'
WITH (
FIELDTERMINATOR = '\t',
ROWTERMINATOR = '\n',
datafiletype='widechar',
ERRORFILE = 'D:\users\likel\DBFile\mypubbishDatacn7.log' 
);