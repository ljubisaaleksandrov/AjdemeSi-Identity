CREATE TABLE [dbo].[Driver]
(
	[UserId]					[nvarchar](128) UNIQUE NOT NULL,
	[LicenceRegistrationDate]	[nvarchar](128) not NULL,
	[SiteRegistrationDate]		[Datetime] not null,
	
	CONSTRAINT [PK_dbo.Driver] PRIMARY KEY CLUSTERED ([UserId] ASC),
	CONSTRAINT [FK_dbo.Driver_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
)
