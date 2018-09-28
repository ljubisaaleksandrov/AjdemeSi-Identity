CREATE TABLE [dbo].[Ride]
(
	[Id]					[int] IDENTITY(1,1) NOT NULL,
	[DriverUserId]			[nvarchar](128) NOT NULL,
	[FromCounty]			[nvarchar](128) NULL,
	[FromCity]				[nvarchar](128) NULL,
	[ToCounty]				[nvarchar](128) NULL,
	[ToCity]				[nvarchar](128) NULL,
	[StartTime]				[datetime]		NOT NULL,
	[EndTime]				[dateTime]		NOT NULL,
	[TravelTime]			[int]			NOT NULL,
	[DelayedStartTime]		[int]			NULL,
	[BreakTime]				[int]			NULL,
	[MaxPassengers]			[int]			NOT NULL,
	[TotalPrice]			[real]			NOT NULL,
	[MinTotalPrice]			[real]			NOT NULL,
	[PricePerPassenger]		[real]			NOT NULL,
	[ReturnDrivingId]		[int]			NULL,
	[DateCreated]			DATETIME NULL		DEFAULT(getdate()),
	
	CONSTRAINT [PK_dbo.Ride] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.Ride_dbo.AspNetUsers_UserId] FOREIGN KEY([DriverUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
	CONSTRAINT [FK_dbo.Ride_dbo.Ride_Id] FOREIGN KEY([ReturnDrivingId]) REFERENCES [dbo].[Ride] ([Id])
)
