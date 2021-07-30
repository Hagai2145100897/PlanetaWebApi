USE [WebApi]
GO

CREATE TABLE [dbo].[Client](
	[Id] [int] IDENTITY(1,1),
	[FullName] [nvarchar](256) NULL,
	[Age] [int] NULL,
	[Gender] [nvarchar](128) NULL
)
GO

ALTER TABLE [dbo].[Client]
	ADD CONSTRAINT PK_Client PRIMARY KEY CLUSTERED ([Id])
GO

CREATE TABLE [dbo].[Subnet](
	[Id] [int] IDENTITY(1,1),
	[ClientId] [int] NULL,
	[NetworkPrefix] [nchar](18) NOT NULL
)
GO

ALTER TABLE [dbo].[Subnet]
	ADD CONSTRAINT PK_Subnet PRIMARY KEY CLUSTERED ([Id])
GO

ALTER TABLE [dbo].[Subnet]
	ADD CONSTRAINT UQ_Subnet_NetworkPrefix UNIQUE ([NetworkPrefix])
GO

ALTER TABLE [dbo].[Subnet]
	ADD CONSTRAINT FK_Subnet_Client FOREIGN KEY ([ClientId])
		REFERENCES [dbo].[Client]([Id])
		ON UPDATE CASCADE
		ON DELETE NO ACTION
GO

INSERT INTO [dbo].[Client] ([FullName], [Age], [Gender])
	VALUES
		('Arckady', 10, 'Malchik');
GO

INSERT INTO [dbo].[Subnet] ([ClientId], [NetworkPrefix])
	VALUES
		(1, '127.0.0.0/24'),
		(1, '128.0.0.0/24'),
		(1, '129.0.0.0/24')
GO
