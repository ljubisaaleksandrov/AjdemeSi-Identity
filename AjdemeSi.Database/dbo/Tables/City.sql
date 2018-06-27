CREATE TABLE [dbo].[City]
(
	[Id]						[int] IDENTITY(1,1) NOT NULL,
	[Name]						[nvarchar](50) NOT NULL,
	[Country]					[nvarchar](50) NOT NULL,
	[NameAlternative]			[nvarchar](50),
	[CountryAlternative]		[nvarchar](50),
	
	CONSTRAINT [PK_dbo.CityB] PRIMARY KEY CLUSTERED ([Id] ASC)
)
