CREATE TABLE [dbo].[RidePassangers]
(
	[Id]			[int] IDENTITY(1,1) NOT NULL,
	[UserId]		[nvarchar](128) NOT NULL,
	[RideId]		[int]			NOT NULL,
	[Rate]			[int]			NULL,
	[RateComment]	[nvarchar](max)	NULL,

	CONSTRAINT [PK_dbo.RidePassengers] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.RidePassengers_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
	CONSTRAINT [FK_dbo.RidePassengers_dbo.Driving_Id] FOREIGN KEY([RideId]) REFERENCES [dbo].[Ride] ([Id])
)
