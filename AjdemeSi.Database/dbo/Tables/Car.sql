CREATE TABLE [dbo].[Car]
(
	[Id]						[int] IDENTITY(1,1) NOT NULL,
	[UserId]					[nvarchar](128) NOT NULL,
	[Make]						[nvarchar](128) NOT NULL,
	[Model]						[nvarchar](128) NOT NULL,
	[NumberOfSits]				[int] NOT NULL,
	[YearOfManufacture]			[int] NOT NULL,
	[IsDefault]					[bit] NOT NULL
	
	CONSTRAINT [PK_dbo.Car] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.Car_dbo.Driver_UserId] FOREIGN KEY([UserId]) REFERENCES [dbo].[Driver] ([UserId]) ON DELETE CASCADE,
)
