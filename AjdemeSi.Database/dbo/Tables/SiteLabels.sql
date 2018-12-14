CREATE TABLE [dbo].[SiteLabels]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[LanguageName] nvarchar(max),
	[RideLocationFrom] nvarchar(max),
	[RideLocationTo] nvarchar(max),
	[RideStartDate] nvarchar(max),
	[RideStartTime] nvarchar(max),
	[RideTravelTIme] nvarchar(max),
	[RidePricePerPerson] nvarchar(max),
	[RidePricePerRide] nvarchar(max),
	[RideDelay] nvarchar(max),
	[RideBreakTime] nvarchar(max),
	[RidePassengers] nvarchar(max)
)
