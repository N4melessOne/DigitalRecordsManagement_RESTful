USE [RecordsManagementDb]
GO

/****** Object: Table [dbo].[Admins] Script Date: 2022. 12. 19. 11:20:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--CREATE
CREATE TABLE [dbo].[Admins] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [AdminName] NVARCHAR (50)  NOT NULL,
    [AdminPass] NVARCHAR (MAX) NOT NULL
);
--INSERT
SET IDENTITY_INSERT [dbo].[Admins] ON
INSERT INTO [dbo].[Admins] ([Id], [AdminName], [AdminPass]) VALUES (2, N'root', N'Root_0')
INSERT INTO [dbo].[Admins] ([Id], [AdminName], [AdminPass]) VALUES (3, N'l4cos', N'l4cos-0')
SET IDENTITY_INSERT [dbo].[Admins] OFF



--CREATE
CREATE TABLE [dbo].[Record] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Performer]  NVARCHAR (MAX) NOT NULL,
    [Title]      NVARCHAR (MAX) NOT NULL,
    [Price]      FLOAT (53)     NOT NULL,
    [StockCount] INT            NOT NULL
);
--INSERT
SET IDENTITY_INSERT [dbo].[Record] ON
INSERT INTO [dbo].[Record] ([Id], [Performer], [Title], [Price], [StockCount]) VALUES (1, N'Architects', N'the symptoms of a broken spirit', 12.99, 10)
INSERT INTO [dbo].[Record] ([Id], [Performer], [Title], [Price], [StockCount]) VALUES (2, N'Bury Tomorrow', N'Black Flame', 9.99, 12)
INSERT INTO [dbo].[Record] ([Id], [Performer], [Title], [Price], [StockCount]) VALUES (3, N'Architects', N'Holy Hell', 11.99, 18)
INSERT INTO [dbo].[Record] ([Id], [Performer], [Title], [Price], [StockCount]) VALUES (4, N'We Came As Romans', N'Darkbloom', 8.99, 10)
INSERT INTO [dbo].[Record] ([Id], [Performer], [Title], [Price], [StockCount]) VALUES (5, N'Lorna Shore', N'Pain Remains', 12.99, 14)
INSERT INTO [dbo].[Record] ([Id], [Performer], [Title], [Price], [StockCount]) VALUES (7, N'Machine Head', N'OF KINGDOM AND CROWN', 13.99, 8)
INSERT INTO [dbo].[Record] ([Id], [Performer], [Title], [Price], [StockCount]) VALUES (8, N'Lindemann', N'F & M (Deluxe)', 14.89, 6)
INSERT INTO [dbo].[Record] ([Id], [Performer], [Title], [Price], [StockCount]) VALUES (10, N'Motionless in White', N'Scoring the End of the World', 11.99, 16)
INSERT INTO [dbo].[Record] ([Id], [Performer], [Title], [Price], [StockCount]) VALUES (11, N'Ice Nine Kills', N'Welcome to Horrorwood: The Silver Scream 2', 13.13, 13)
INSERT INTO [dbo].[Record] ([Id], [Performer], [Title], [Price], [StockCount]) VALUES (13, N'Bad Omens', N'THE DEATH OF PEACE OF MIND', 10.99, 8)
INSERT INTO [dbo].[Record] ([Id], [Performer], [Title], [Price], [StockCount]) VALUES (8009, N'Architects', N'For Those That Wish to Exist', 14.99, 12)
SET IDENTITY_INSERT [dbo].[Record] OFF


--CREATE
CREATE TABLE [dbo].[RecordCover] (
    [id]         INT   NOT NULL,
    [recordId]   INT   NOT NULL,
    [coverImage] IMAGE NOT NULL
);
--INSERT
