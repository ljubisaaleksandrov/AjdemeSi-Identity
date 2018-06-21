CREATE TABLE [dbo].[Friends](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Friend1Id] [nvarchar](128) NOT NULL,
	[Friend2Id] [nvarchar](128) NOT NULL,
	[DateCreated] Datetime NOT NULL,
	
	CONSTRAINT [PK_dbo.Friends] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.Friends1_dbo.AspNetUsers_UserId] FOREIGN KEY([Friend1Id]) REFERENCES [dbo].[AspNetUsers] ([Id]),
	CONSTRAINT [FK_dbo.Friends2_dbo.AspNetUsers_UserId] FOREIGN KEY([Friend2Id]) REFERENCES [dbo].[AspNetUsers] ([Id])
) ON [PRIMARY]
GO
