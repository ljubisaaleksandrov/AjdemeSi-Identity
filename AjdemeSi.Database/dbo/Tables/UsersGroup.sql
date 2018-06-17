CREATE TABLE [dbo].[UsersGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserCreatorId] [nvarchar](128) NOT NULL,
	[DateCreated] Datetime NOT NULL,
	[RideId] int,
	
	CONSTRAINT [PK_dbo.UsersGroup] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.UsersGroup_dbo.AspNetUsers_UserId] FOREIGN KEY([UserCreatorId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
	CONSTRAINT [FK_dbo.UsersGroup_dbo.Ride_RideId] FOREIGN KEY([RideId]) REFERENCES [dbo].[Ride] ([Id])
) ON [PRIMARY]
GO
