CREATE TABLE [dbo].[UserSettings](
	[Id]					[int] IDENTITY(1,1) NOT NULL,
	[UserId]				[nvarchar](128) UNIQUE NOT NULL,
	[City]					[nvarchar](128) NULL,
	[Country]				[nvarchar](128) NULL,
	[BirthDate]				[Datetime] null,
	[Gender]				[bit] NULL,
	
	CONSTRAINT [PK_dbo.UserSettings] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.UserSettings_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
)