CREATE TABLE [dbo].[Vehicles]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Model] nvarchar(30) NOT NULL,
	[Make] nvarchar(30) NOT NULL,
	[Year] INT NOT NULL
)
