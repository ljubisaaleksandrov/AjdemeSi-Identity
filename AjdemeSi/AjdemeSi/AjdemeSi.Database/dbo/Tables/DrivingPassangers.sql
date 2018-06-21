CREATE TABLE [dbo].[DrivingPassangers]
(
	[Id]			[int] IDENTITY(1,1) NOT NULL,
	[UserId]		[nvarchar](128) NOT NULL,
	[DrivingId]		[int]			NOT NULL,
	[Rate]			[int]			NULL,
	[RateComment]	[nvarchar](max)	NULL,

	CONSTRAINT [PK_dbo.DrivingPassengers] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.DrivingPassengers_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
	CONSTRAINT [FK_dbo.DrivingPassengers_dbo.Driving_Id] FOREIGN KEY([DrivingId]) REFERENCES [dbo].[Driving] ([Id])
)
