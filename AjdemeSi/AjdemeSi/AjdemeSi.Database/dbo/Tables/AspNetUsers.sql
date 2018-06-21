﻿CREATE TABLE [dbo].[AspNetUsers](
	[Id]					[nvarchar](128) NOT NULL,
	[Email]					[nvarchar](256) NOT NULL,
	[UserName]				[nvarchar](256) NOT NULL,
	[EmailConfirmed]		[bit] NOT NULL,
	[PasswordHash]			[nvarchar](max) NULL,
	[SecurityStamp]			[nvarchar](max) NULL,
	[PhoneNumber]			[nvarchar](max) NULL,
	[PhoneNumberConfirmed]	[bit] NOT NULL,
	[TwoFactorEnabled]		[bit] NOT NULL,
	[LockoutEndDateUtc]		[datetime] NULL,
	[LockoutEnabled]		[bit] NOT NULL,
	[AccessFailedCount]		[int] NOT NULL,
	[DateCreated]			DATETIME NULL		DEFAULT(getdate()),
	
	CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED (	[Id] ASC )
)