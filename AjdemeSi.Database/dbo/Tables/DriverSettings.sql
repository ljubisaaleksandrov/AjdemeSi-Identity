CREATE TABLE [dbo].[DriverSettings]
(
	[Id]					[int] IDENTITY(1,1) NOT NULL,
	[UserId]				[nvarchar](128) NOT NULL,
	[City]					[nvarchar](128) NULL,
	[Country]				[nvarchar](128) NULL,
	[BirthDate]				[Datetime] null,
	
	CONSTRAINT [PK_dbo.DriverSettings] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.DriverSettings_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
)
