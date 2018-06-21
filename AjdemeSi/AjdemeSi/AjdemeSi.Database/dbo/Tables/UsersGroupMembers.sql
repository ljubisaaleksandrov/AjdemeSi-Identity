CREATE TABLE [dbo].[GroupMembers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UsersGroupId] int NOT NULL,
	[MemberId] [nvarchar](128) NOT NULL,
	
	CONSTRAINT [PK_dbo.GroupMembers] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.GroupMembers_dbo.UsersGroup_UsersGroupId] FOREIGN KEY([UsersGroupId]) REFERENCES [dbo].[UsersGroup] ([Id]),
	CONSTRAINT [FK_dbo.GroupMembers_dbo.AspNetUsers_UserId] FOREIGN KEY([MemberId]) REFERENCES [dbo].[AspNetUsers] ([Id])
) ON [PRIMARY]
GO
