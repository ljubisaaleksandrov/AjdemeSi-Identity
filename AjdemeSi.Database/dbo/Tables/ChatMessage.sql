CREATE TABLE [dbo].[ChatMessage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UsersGroupId] int NOT NULL,
	[SenderId] [nvarchar](128) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[IsEdited] [bit] NOT NULL,
	[IsRemoved] [bit] NOT NULL,
	[DateCreated] Datetime NOT NULL,
	[SeenBy] [nvarchar](max) NOT NULL,
	
	CONSTRAINT [PK_dbo.ChatMessage] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.ChatMessage_dbo.UsersGroup_UsersGroupId] FOREIGN KEY([UsersGroupId]) REFERENCES [dbo].[UsersGroup] ([Id]),
	CONSTRAINT [FK_dbo.ChatMessage_dbo.AspNetUsers_UserId] FOREIGN KEY([SenderId]) REFERENCES [dbo].[AspNetUsers] ([Id])
) ON [PRIMARY]
GO
