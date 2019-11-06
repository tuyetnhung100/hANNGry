USE [234a_hANNGry]
GO
IF OBJECT_ID('dbo.SubscriberNotification', 'U') IS NOT NULL 
	DROP TABLE SubscriberNotification;
IF OBJECT_ID('dbo.Tags', 'U') IS NOT NULL 
	DROP TABLE Tags;
IF OBJECT_ID('dbo.Notifications', 'U') IS NOT NULL 
	DROP TABLE Notifications;
IF OBJECT_ID('dbo.Templates', 'U') IS NOT NULL 
	DROP TABLE Templates;
IF OBJECT_ID('dbo.Accounts', 'U') IS NOT NULL 
	DROP TABLE Accounts;
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 11/6/2019 3:19:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[Role] [int] NOT NULL,
	[Username] [nvarchar](300) NOT NULL,
	[PasswordHash] [nvarchar](500) NOT NULL,
	[PasswordSalt] [nvarchar](500) NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[Email] [nvarchar](254) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 11/6/2019 3:19:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[NotificationId] [int] IDENTITY(1,1) NOT NULL,
	[Subject] [nvarchar](500) NULL,
	[Message] [nvarchar](4000) NOT NULL,
	[TemplateId] [int] NULL,
	[SentAccountId] [int] NOT NULL,
	[SentDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED 
(
	[NotificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubscriberNotification]    Script Date: 11/6/2019 3:19:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubscriberNotification](
	[Subscribers_AccountId] [int] NOT NULL,
	[ReceivedNotifications_NotificationId] [int] NOT NULL,
	[Succeeded] [bit] NULL,
	[Cancelled] [bit] NULL,
	[Failed] [bit] NULL,
	[ErrorMessage] [nvarchar](500) NULL,
 CONSTRAINT [PK_SubscriberNotification] PRIMARY KEY CLUSTERED 
(
	[Subscribers_AccountId] ASC,
	[ReceivedNotifications_NotificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 11/6/2019 3:19:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tags](
	[TagId] [int] IDENTITY(1,1) NOT NULL,
	[Type] [int] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[TagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Templates]    Script Date: 11/6/2019 3:19:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Templates](
	[TemplateId] [int] IDENTITY(1,1) NOT NULL,
	[Subject] [nvarchar](200) NOT NULL,
	[Message] [nvarchar](4000) NOT NULL,
	[CreatedAccountId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Templates] PRIMARY KEY CLUSTERED 
(
	[TemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 
GO
INSERT [dbo].[Accounts] ([AccountId], [Role], [Username], [PasswordHash], [PasswordSalt], [Name], [Email], [CreatedDate]) VALUES (1, 1, N'employee', N'VGgb523w+1++WPlqFfnb8NxiB6jUlmeY', N'KJmdMdY/3fPklOAldA2kQpzIeZFoWh/P', N'Solid Snake', N'hanngry.emp@pcc.edu', CAST(N'2019-11-05T18:46:39.673' AS DateTime))
GO
INSERT [dbo].[Accounts] ([AccountId], [Role], [Username], [PasswordHash], [PasswordSalt], [Name], [Email], [CreatedDate]) VALUES (2, 2, N'manager', N'Rj8nuHKXvab2hwsoc1SZbQWCLcvszIvM', N'Dd2A/1rAY3zYOs99fbN1Fgpbl7oAmJek', N'Big Boss', N'hanngry.mgr@pcc.edu', CAST(N'2019-11-05T23:49:00.120' AS DateTime))
GO
INSERT [dbo].[Accounts] ([AccountId], [Role], [Username], [PasswordHash], [PasswordSalt], [Name], [Email], [CreatedDate]) VALUES (3, 0, N'gonghao', N'm09Z5sph4N7yAXIdG0f3HVkFAgaTMZmS', N'LF3Yqt8ssMjgejRocyTvgZ5iCBXZ2rWg', N'Gong-Hao', N'gonghao.wei@pcc.edu', CAST(N'2019-11-05T23:50:02.483' AS DateTime))
GO
INSERT [dbo].[Accounts] ([AccountId], [Role], [Username], [PasswordHash], [PasswordSalt], [Name], [Email], [CreatedDate]) VALUES (4, 0, N'gordon', N'7Ur3GapYfsfPiQ8dUnWVlZd6nKyQKaaZ', N'9yZ5Y/xwuFOEi6GMO/WEVe08mh8FmR/i', N'Gordon', N'weig@my.lanecc.edu', CAST(N'2019-11-06T02:34:41.913' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[Notifications] ON 
GO
INSERT [dbo].[Notifications] ([NotificationId], [Subject], [Message], [TemplateId], [SentAccountId], [SentDate]) VALUES (1, N'Event With Start and End Time', N'Dear {$student name},

We will have Nice event at the PCC campus in TCP 123 on 12/2 from 10 am to 5 pm.

Feel free to come and join us.

{$employee name}', 1, 1, CAST(N'2019-11-06T01:20:18.387' AS DateTime))
GO
INSERT [dbo].[Notifications] ([NotificationId], [Subject], [Message], [TemplateId], [SentAccountId], [SentDate]) VALUES (2, N'Event With Start and End Time', N'Dear {$student name},

We will have Nice event at the PCC campus in TCP 311 on 12/25 from 10 am to 5pm.

Feel free to come and join us.

{$employee name}', 1, 1, CAST(N'2019-11-06T02:36:19.860' AS DateTime))
GO
INSERT [dbo].[Notifications] ([NotificationId], [Subject], [Message], [TemplateId], [SentAccountId], [SentDate]) VALUES (3, N'Good Day', N'Today is a good day. : )', NULL, 1, CAST(N'2019-11-06T03:15:38.340' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Notifications] OFF
GO
INSERT [dbo].[SubscriberNotification] ([Subscribers_AccountId], [ReceivedNotifications_NotificationId], [Succeeded], [Cancelled], [Failed], [ErrorMessage]) VALUES (3, 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SubscriberNotification] ([Subscribers_AccountId], [ReceivedNotifications_NotificationId], [Succeeded], [Cancelled], [Failed], [ErrorMessage]) VALUES (3, 2, 1, 0, 0, NULL)
GO
INSERT [dbo].[SubscriberNotification] ([Subscribers_AccountId], [ReceivedNotifications_NotificationId], [Succeeded], [Cancelled], [Failed], [ErrorMessage]) VALUES (3, 3, 1, 0, 0, NULL)
GO
INSERT [dbo].[SubscriberNotification] ([Subscribers_AccountId], [ReceivedNotifications_NotificationId], [Succeeded], [Cancelled], [Failed], [ErrorMessage]) VALUES (4, 2, 1, 0, 0, NULL)
GO
INSERT [dbo].[SubscriberNotification] ([Subscribers_AccountId], [ReceivedNotifications_NotificationId], [Succeeded], [Cancelled], [Failed], [ErrorMessage]) VALUES (4, 3, 1, 0, 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[Tags] ON 
GO
INSERT [dbo].[Tags] ([TagId], [Type], [Name]) VALUES (1, 0, N'student name')
GO
INSERT [dbo].[Tags] ([TagId], [Type], [Name]) VALUES (2, 0, N'employee name')
GO
INSERT [dbo].[Tags] ([TagId], [Type], [Name]) VALUES (3, 1, N'food')
GO
INSERT [dbo].[Tags] ([TagId], [Type], [Name]) VALUES (4, 1, N'campus name')
GO
INSERT [dbo].[Tags] ([TagId], [Type], [Name]) VALUES (5, 1, N'room')
GO
INSERT [dbo].[Tags] ([TagId], [Type], [Name]) VALUES (6, 1, N'date')
GO
INSERT [dbo].[Tags] ([TagId], [Type], [Name]) VALUES (7, 1, N'start time')
GO
INSERT [dbo].[Tags] ([TagId], [Type], [Name]) VALUES (8, 1, N'end time')
GO
INSERT [dbo].[Tags] ([TagId], [Type], [Name]) VALUES (9, 1, N'event name')
GO
INSERT [dbo].[Tags] ([TagId], [Type], [Name]) VALUES (10, 1, N'description')
GO
SET IDENTITY_INSERT [dbo].[Tags] OFF
GO
SET IDENTITY_INSERT [dbo].[Templates] ON 
GO
INSERT [dbo].[Templates] ([TemplateId], [Subject], [Message], [CreatedAccountId], [CreatedDate]) VALUES (1, N'Event With Start and End Time', N'Dear {$student name},

We will have {$event name} at the {$campus name} campus in {$room} on {$date} from {$start time} to {$end time}.

Feel free to come and join us.

{$employee name}', 2, CAST(N'2019-11-06T00:18:00.000' AS DateTime))
GO
INSERT [dbo].[Templates] ([TemplateId], [Subject], [Message], [CreatedAccountId], [CreatedDate]) VALUES (2, N'All Day Event', N'Dear {$student name},

There will be {$food} at the {$campus name} campus in {$room} on {$date} All day.

Thanks,

{$employee name}', 0, CAST(N'2019-11-06T00:23:10.507' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Templates] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UNIQUE_Email]    Script Date: 11/6/2019 3:19:55 AM ******/
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [UNIQUE_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UNIQUE_Username]    Script Date: 11/6/2019 3:19:55 AM ******/
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [UNIQUE_Username] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_NotificationTemplate] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[Templates] ([TemplateId])
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_NotificationTemplate]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_UserNotification] FOREIGN KEY([SentAccountId])
REFERENCES [dbo].[Accounts] ([AccountId])
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_UserNotification]
GO
ALTER TABLE [dbo].[SubscriberNotification]  WITH CHECK ADD  CONSTRAINT [FK_SubscriberNotification_Notification] FOREIGN KEY([ReceivedNotifications_NotificationId])
REFERENCES [dbo].[Notifications] ([NotificationId])
GO
ALTER TABLE [dbo].[SubscriberNotification] CHECK CONSTRAINT [FK_SubscriberNotification_Notification]
GO
ALTER TABLE [dbo].[SubscriberNotification]  WITH CHECK ADD  CONSTRAINT [FK_SubscriberNotification_User] FOREIGN KEY([Subscribers_AccountId])
REFERENCES [dbo].[Accounts] ([AccountId])
GO
ALTER TABLE [dbo].[SubscriberNotification] CHECK CONSTRAINT [FK_SubscriberNotification_User]
GO
