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
/****** Object:  Table [dbo].[Accounts]    Script Date: 12/9/2019 7:52:59 PM ******/
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
	[PhoneNumber] [nvarchar](100) NOT NULL,
	[Carrier] [nvarchar](100) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Activated] [bit] NULL,
	[Location] [int] NOT NULL,
	[NotificationType] [int] NOT NULL,
	[Code] [nvarchar](max) NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 12/9/2019 7:52:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[NotificationId] [int] IDENTITY(1,1) NOT NULL,
	[Location] [int] NULL,
	[Subject] [nvarchar](500) NULL,
	[Message] [nvarchar](4000) NOT NULL,
	[TemplateId] [int] NULL,
	[SentAccountId] [int] NULL,
	[SentDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED
(
	[NotificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubscriberNotification]    Script Date: 12/9/2019 7:52:59 PM ******/
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
/****** Object:  Table [dbo].[Tags]    Script Date: 12/9/2019 7:52:59 PM ******/
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
/****** Object:  Table [dbo].[Templates]    Script Date: 12/9/2019 7:52:59 PM ******/
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
INSERT [dbo].[Accounts] ([AccountId], [Role], [Username], [PasswordHash], [PasswordSalt], [Name], [Email], [PhoneNumber], [Carrier], [CreatedDate], [Activated], [Location], [NotificationType], [Code]) VALUES (1, 1, N'employee', N'VGgb523w+1++WPlqFfnb8NxiB6jUlmeY', N'KJmdMdY/3fPklOAldA2kQpzIeZFoWh/P', N'Solid Snake', N'hanngry.emp@pcc.edu', N'1111111111', N'T-Mobile', CAST(N'2019-11-05T18:46:39.673' AS DateTime), 1, 1, 0, NULL)
GO
INSERT [dbo].[Accounts] ([AccountId], [Role], [Username], [PasswordHash], [PasswordSalt], [Name], [Email], [PhoneNumber], [Carrier], [CreatedDate], [Activated], [Location], [NotificationType], [Code]) VALUES (2, 2, N'manager', N'Rj8nuHKXvab2hwsoc1SZbQWCLcvszIvM', N'Dd2A/1rAY3zYOs99fbN1Fgpbl7oAmJek', N'Big Boss', N'hanngry.mgr@pcc.edu', N'1111111111', N'T-Mobile', CAST(N'2019-11-05T23:49:00.120' AS DateTime), 1, 1, 0, NULL)
GO
INSERT [dbo].[Accounts] ([AccountId], [Role], [Username], [PasswordHash], [PasswordSalt], [Name], [Email], [PhoneNumber], [Carrier], [CreatedDate], [Activated], [Location], [NotificationType], [Code]) VALUES (3, 0, N'gordon', N'7Ur3GapYfsfPiQ8dUnWVlZd6nKyQKaaZ', N'9yZ5Y/xwuFOEi6GMO/WEVe08mh8FmR/i', N'Gordon', N'weig@my.lanecc.edu', N'1111111111', N'T-Mobile', CAST(N'2019-11-06T02:34:41.913' AS DateTime), 1, 1, 1, NULL)
GO
INSERT [dbo].[Accounts] ([AccountId], [Role], [Username], [PasswordHash], [PasswordSalt], [Name], [Email], [PhoneNumber], [Carrier], [CreatedDate], [Activated], [Location], [NotificationType], [Code]) VALUES (4, 0, N'gonghao', N'If94MqZxVRMJtMOzHorYhsePUBvKODMr', N'tK/xjP06GT4tMhDwBStJX8jGa6FCWm+q', N'Gong-Hao', N'gonghao.wei@pcc.edu', N'2062653975', N'AT&T', CAST(N'2019-12-06T02:37:00.420' AS DateTime), 1, 8, 3, NULL)
GO
INSERT [dbo].[Accounts] ([AccountId], [Role], [Username], [PasswordHash], [PasswordSalt], [Name], [Email], [PhoneNumber], [Carrier], [CreatedDate], [Activated], [Location], [NotificationType], [Code]) VALUES (5, 0, N'mickeyMouse', N'1s0c5qSe7yuFFHlKhkDo1PhCUpA/rRm4', N'EbdWTWjiN+0vkIIX8r7EF7juL/+DZ0+0', N'Mickey Mouse', N'tuyetnhung100@me.com', N'5038888207', N'T-Mobile', CAST(N'2019-12-06T19:11:22.143' AS DateTime), 1, 8, 3, NULL)
GO
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[Notifications] ON
GO
INSERT [dbo].[Notifications] ([NotificationId], [Location], [Subject], [Message], [TemplateId], [SentAccountId], [SentDate]) VALUES (1, 15, N'Durian Party', N'Hi there,

We will have a durian party tonight!', NULL, 2, CAST(N'2019-12-01T01:12:18.037' AS DateTime))
GO
INSERT [dbo].[Notifications] ([NotificationId], [Location], [Subject], [Message], [TemplateId], [SentAccountId], [SentDate]) VALUES (2, 1, N'Event With Summary', N'Dear {$student name},

We will have Yellow Jello Contest at the Sylvania campus in TCP 311 on 12/25/2019 from 10 am to 5 pm.

{$student name}, feel free to come and join us. We are looking forward to seeing you. :)

Sincerely,

{$employee name}

Event Information
Event: Yellow Jello Contest
Location: Sylvania TCP 311
Date: 12/25/2019 10 am - 5 pm', 2, 2, CAST(N'2019-12-02T01:16:41.100' AS DateTime))
GO
INSERT [dbo].[Notifications] ([NotificationId], [Location], [Subject], [Message], [TemplateId], [SentAccountId], [SentDate]) VALUES (3, 15, N'Happy Event', N'Hi {$student name},

Do you love surströmming?
Let''s have a surströmming party tonight!
Join our delicious supper at PCC.

Sincerely,

{$employee name},
PCC Food Pantry', 1, 2, CAST(N'2019-12-03T01:18:52.193' AS DateTime))
GO
INSERT [dbo].[Notifications] ([NotificationId], [Location], [Subject], [Message], [TemplateId], [SentAccountId], [SentDate]) VALUES (4, 15, N'Happy Event', N'Hi {$student name},

Do you love bananas?
Let''s have a bananas party tonight!
Join our Happy Bananas at PCC.

Sincerely,

{$employee name},
PCC Food Pantry', 1, 2, CAST(N'2019-12-03T16:41:04.530' AS DateTime))
GO
INSERT [dbo].[Notifications] ([NotificationId], [Location], [Subject], [Message], [TemplateId], [SentAccountId], [SentDate]) VALUES (5, 15, N'Happy Event', N'Hi {$student name},

Do you love chicken feet?
Let''s have a chicken feet party tonight!
Join our Chicken Noddle Soup at PCC.

Sincerely,

{$employee name},
PCC Food Pantry', 1, 2, CAST(N'2019-12-05T17:15:59.373' AS DateTime))
GO
INSERT [dbo].[Notifications] ([NotificationId], [Location], [Subject], [Message], [TemplateId], [SentAccountId], [SentDate]) VALUES (6, 15, N'Nice Event', N'Dear {$student name},

We will have Nice Event at the PCC campus in TCP 311 on 12/25/2019 from 10 am to 5 pm.

{$student name}, feel free to come and join us. We are looking forward to seeing you. :)

Sincerely,

{$employee name}

Event Information
Event: Nice Event
Location: PCC TCP 311
Date: 12/25/2019 10 am - 5 pm', 2, 2, CAST(N'2019-12-09T19:45:43.077' AS DateTime))
GO
INSERT [dbo].[Notifications] ([NotificationId], [Location], [Subject], [Message], [TemplateId], [SentAccountId], [SentDate]) VALUES (7, 8, N'How about more vegetable?', N'We have a lot of vegetable?

Want some?

PCC Food Pantry', NULL, 2, CAST(N'2019-12-09T19:49:25.013' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Notifications] OFF
GO
INSERT [dbo].[SubscriberNotification] ([Subscribers_AccountId], [ReceivedNotifications_NotificationId], [Succeeded], [Cancelled], [Failed], [ErrorMessage]) VALUES (3, 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SubscriberNotification] ([Subscribers_AccountId], [ReceivedNotifications_NotificationId], [Succeeded], [Cancelled], [Failed], [ErrorMessage]) VALUES (3, 2, 1, 0, 0, NULL)
GO
INSERT [dbo].[SubscriberNotification] ([Subscribers_AccountId], [ReceivedNotifications_NotificationId], [Succeeded], [Cancelled], [Failed], [ErrorMessage]) VALUES (3, 3, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SubscriberNotification] ([Subscribers_AccountId], [ReceivedNotifications_NotificationId], [Succeeded], [Cancelled], [Failed], [ErrorMessage]) VALUES (3, 4, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SubscriberNotification] ([Subscribers_AccountId], [ReceivedNotifications_NotificationId], [Succeeded], [Cancelled], [Failed], [ErrorMessage]) VALUES (3, 5, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SubscriberNotification] ([Subscribers_AccountId], [ReceivedNotifications_NotificationId], [Succeeded], [Cancelled], [Failed], [ErrorMessage]) VALUES (3, 6, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SubscriberNotification] ([Subscribers_AccountId], [ReceivedNotifications_NotificationId], [Succeeded], [Cancelled], [Failed], [ErrorMessage]) VALUES (4, 1, 1, 0, 0, NULL)
GO
INSERT [dbo].[SubscriberNotification] ([Subscribers_AccountId], [ReceivedNotifications_NotificationId], [Succeeded], [Cancelled], [Failed], [ErrorMessage]) VALUES (4, 3, 1, 0, 0, NULL)
GO
INSERT [dbo].[SubscriberNotification] ([Subscribers_AccountId], [ReceivedNotifications_NotificationId], [Succeeded], [Cancelled], [Failed], [ErrorMessage]) VALUES (4, 4, 1, 0, 0, NULL)
GO
INSERT [dbo].[SubscriberNotification] ([Subscribers_AccountId], [ReceivedNotifications_NotificationId], [Succeeded], [Cancelled], [Failed], [ErrorMessage]) VALUES (4, 5, 1, 0, 0, NULL)
GO
INSERT [dbo].[SubscriberNotification] ([Subscribers_AccountId], [ReceivedNotifications_NotificationId], [Succeeded], [Cancelled], [Failed], [ErrorMessage]) VALUES (4, 6, 1, 0, 0, NULL)
GO
INSERT [dbo].[SubscriberNotification] ([Subscribers_AccountId], [ReceivedNotifications_NotificationId], [Succeeded], [Cancelled], [Failed], [ErrorMessage]) VALUES (4, 7, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SubscriberNotification] ([Subscribers_AccountId], [ReceivedNotifications_NotificationId], [Succeeded], [Cancelled], [Failed], [ErrorMessage]) VALUES (5, 1, 1, 0, 0, NULL)
GO
INSERT [dbo].[SubscriberNotification] ([Subscribers_AccountId], [ReceivedNotifications_NotificationId], [Succeeded], [Cancelled], [Failed], [ErrorMessage]) VALUES (5, 3, 1, 0, 0, NULL)
GO
INSERT [dbo].[SubscriberNotification] ([Subscribers_AccountId], [ReceivedNotifications_NotificationId], [Succeeded], [Cancelled], [Failed], [ErrorMessage]) VALUES (5, 4, 1, 0, 0, NULL)
GO
INSERT [dbo].[SubscriberNotification] ([Subscribers_AccountId], [ReceivedNotifications_NotificationId], [Succeeded], [Cancelled], [Failed], [ErrorMessage]) VALUES (5, 5, 1, 0, 0, NULL)
GO
INSERT [dbo].[SubscriberNotification] ([Subscribers_AccountId], [ReceivedNotifications_NotificationId], [Succeeded], [Cancelled], [Failed], [ErrorMessage]) VALUES (5, 6, 1, 0, 0, NULL)
GO
INSERT [dbo].[SubscriberNotification] ([Subscribers_AccountId], [ReceivedNotifications_NotificationId], [Succeeded], [Cancelled], [Failed], [ErrorMessage]) VALUES (5, 7, 1, 0, 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[Tags] ON
GO
INSERT [dbo].[Tags] ([TagId], [Type], [Name]) VALUES (1, 0, N'student name')
GO
INSERT [dbo].[Tags] ([TagId], [Type], [Name]) VALUES (2, 0, N'employee name')
GO
INSERT [dbo].[Tags] ([TagId], [Type], [Name]) VALUES (3, 1, N'food')
GO
INSERT [dbo].[Tags] ([TagId], [Type], [Name]) VALUES (4, 1, N'event name')
GO
INSERT [dbo].[Tags] ([TagId], [Type], [Name]) VALUES (5, 1, N'campus name')
GO
INSERT [dbo].[Tags] ([TagId], [Type], [Name]) VALUES (6, 1, N'room')
GO
INSERT [dbo].[Tags] ([TagId], [Type], [Name]) VALUES (7, 1, N'date')
GO
INSERT [dbo].[Tags] ([TagId], [Type], [Name]) VALUES (8, 1, N'start time')
GO
INSERT [dbo].[Tags] ([TagId], [Type], [Name]) VALUES (9, 1, N'end time')
GO
SET IDENTITY_INSERT [dbo].[Tags] OFF
GO
SET IDENTITY_INSERT [dbo].[Templates] ON
GO
INSERT [dbo].[Templates] ([TemplateId], [Subject], [Message], [CreatedAccountId], [CreatedDate]) VALUES (1, N'Happy Event', N'Hi {$student name},

Do you love {$food}?
Let''s have a {$food} party tonight!
Join our {$event name} at PCC.

Sincerely,

{$employee name},
PCC Food Pantry', 2, CAST(N'2019-11-06T00:23:10.507' AS DateTime))
GO
INSERT [dbo].[Templates] ([TemplateId], [Subject], [Message], [CreatedAccountId], [CreatedDate]) VALUES (2, N'Event With Summary', N'Dear {$student name},

We will have {$event name} at the {$campus name} campus in {$room} on {$date} from {$start time} to {$end time}.

{$student name}, feel free to come and join us. We are looking forward to seeing you. :)

Sincerely,

{$employee name}

Event Information
Event: {$event name}
Location: {$campus name} {$room}
Date: {$date} {$start time} - {$end time}', 2, CAST(N'2019-11-06T00:23:10.507' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Templates] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UNIQUE_Email]    Script Date: 12/9/2019 7:53:00 PM ******/
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [UNIQUE_Email] UNIQUE NONCLUSTERED
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UNIQUE_Username]    Script Date: 12/9/2019 7:53:00 PM ******/
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [UNIQUE_Username] UNIQUE NONCLUSTERED
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
