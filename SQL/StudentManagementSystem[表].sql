USE [StudentManagementSystem]
GO
/****** Object:  Table [dbo].[book_details]    Script Date: 2022/4/13 11:57:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[book_details](
	[book_id] [int] IDENTITY(1,1) NOT NULL,
	[book_name] [varchar](50) NOT NULL,
	[book_author] [varchar](30) NULL,
	[book_image] [image] NULL,
	[book_price] [money] NULL,
	[book_publisher] [varchar](80) NULL,
	[book_issuing] [datetime] NULL,
	[book_type] [varchar](20) NULL,
	[book_words] [int] NULL,
	[book_pagination] [int] NULL,
	[book_introduction] [varchar](200) NULL,
	[book_url] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[course_teacher_relation]    Script Date: 2022/4/13 11:57:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[course_teacher_relation](
	[course_id] [varchar](20) NOT NULL,
	[teacher_id] [varchar](20) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[courses]    Script Date: 2022/4/13 11:57:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[courses](
	[course_id] [varchar](20) NOT NULL,
	[course_name] [varchar](150) NOT NULL,
	[description] [varchar](500) NULL,
	[is_publish] [int] NOT NULL,
	[course_cover] [varchar](200) NOT NULL,
	[course_url] [varchar](200) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[nations]    Script Date: 2022/4/13 11:57:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[nations](
	[id] [int] NOT NULL,
	[nations_name] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[platforms]    Script Date: 2022/4/13 11:57:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[platforms](
	[platform_id] [varchar](20) NOT NULL,
	[platform_name] [varchar](50) NOT NULL,
	[is_validation] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[play_record]    Script Date: 2022/4/13 11:57:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[play_record](
	[course_id] [varchar](20) NOT NULL,
	[platform_id] [varchar](20) NOT NULL,
	[play_count] [decimal](18, 0) NOT NULL,
	[is_growing] [int] NOT NULL,
	[growing_rate] [decimal](18, 0) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[politics_status]    Script Date: 2022/4/13 11:57:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[politics_status](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[politics_status] [nchar](20) NULL,
 CONSTRAINT [PK_politics_status] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[students]    Script Date: 2022/4/13 11:57:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[students](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[number] [nvarchar](50) NULL,
	[name] [nvarchar](50) NULL,
	[sex] [int] NULL,
	[birthday] [datetime] NULL,
	[nation_id] [int] NULL,
	[politics_status_id] [int] NULL,
	[grade] [nvarchar](50) NULL,
	[site] [nvarchar](200) NULL,
	[phone] [nvarchar](50) NULL,
	[is_delete] [int] NULL,
	[gmt_create] [datetime] NULL,
	[gmt_modified] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 2022/4/13 11:57:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[user_id] [varchar](20) NOT NULL,
	[user_name] [varchar](20) NOT NULL,
	[real_name] [varchar](20) NOT NULL,
	[password] [varchar](40) NULL,
	[is_validation] [int] NOT NULL,
	[is_can_login] [int] NOT NULL,
	[is_teacher] [int] NOT NULL,
	[avatar] [varchar](200) NULL,
	[gender] [int] NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[book_details] ON 

INSERT [dbo].[book_details] ([book_id], [book_name], [book_author], [book_image], [book_price], [book_publisher], [book_issuing], [book_type], [book_words], [book_pagination], [book_introduction], [book_url]) VALUES (1, N'C#开发案例教程', N'李**', NULL, 80.0000, N'清华大学出版社', CAST(N'2018-01-01T00:00:00.000' AS DateTime), N'IT信息技术', 7700000, 8631, N'"程序开发案例课堂”系列图书是专门为软件开发和数据库初学者量身定做的一套学习用书,整套书涵盖软件开发、数据库设计等方面.', NULL)
INSERT [dbo].[book_details] ([book_id], [book_name], [book_author], [book_image], [book_price], [book_publisher], [book_issuing], [book_type], [book_words], [book_pagination], [book_introduction], [book_url]) VALUES (2, N'ASPNET MVC 5高级编程(第版)', N'刘远帅', NULL, 25.0000, N'清华大学出版社', CAST(N'2015-01-01T00:00:00.000' AS DateTime), NULL, NULL, NULL, N'本书包括了MVC 5.1和IMVC 5.21的一些新特性,通过采用分步骤讲解的方法,指导读者如充分利用MVC.', NULL)
SET IDENTITY_INSERT [dbo].[book_details] OFF
GO
INSERT [dbo].[course_teacher_relation] ([course_id], [teacher_id]) VALUES (N'C0001', N'1003')
INSERT [dbo].[course_teacher_relation] ([course_id], [teacher_id]) VALUES (N'C0001', N'1005')
INSERT [dbo].[course_teacher_relation] ([course_id], [teacher_id]) VALUES (N'C0002', N'1004')
INSERT [dbo].[course_teacher_relation] ([course_id], [teacher_id]) VALUES (N'C0002', N'1005')
INSERT [dbo].[course_teacher_relation] ([course_id], [teacher_id]) VALUES (N'C0003', N'1006')
INSERT [dbo].[course_teacher_relation] ([course_id], [teacher_id]) VALUES (N'C0004', N'1007')
INSERT [dbo].[course_teacher_relation] ([course_id], [teacher_id]) VALUES (N'C0005', N'1008')
INSERT [dbo].[course_teacher_relation] ([course_id], [teacher_id]) VALUES (N'C0006', N'1003')
INSERT [dbo].[course_teacher_relation] ([course_id], [teacher_id]) VALUES (N'C0007', N'1003')
INSERT [dbo].[course_teacher_relation] ([course_id], [teacher_id]) VALUES (N'C0007', N'1005')
INSERT [dbo].[course_teacher_relation] ([course_id], [teacher_id]) VALUES (N'C0008', N'1003')
INSERT [dbo].[course_teacher_relation] ([course_id], [teacher_id]) VALUES (N'C0008', N'1005')
INSERT [dbo].[course_teacher_relation] ([course_id], [teacher_id]) VALUES (N'C0009', N'1007')
INSERT [dbo].[course_teacher_relation] ([course_id], [teacher_id]) VALUES (N'C0010', N'1008')
GO
INSERT [dbo].[courses] ([course_id], [course_name], [description], [is_publish], [course_cover], [course_url]) VALUES (N'C0001', N'C#/.Net架构师蜕变营', N'.Net终极课程，培养优秀的.Net架构师！深入分布式、跨平台、微服务最核心技术，输出核心架构师思维，实战架构落地，成就架构师!', 1, N'http://cdn2.ziti7.com//qrcode/uploaded2/11/60/0ss/b5/6eede52f86b55a87eae52e2381cf1f.jpg!larger2
', N'http://www.ziti3.com/qrcode/q/15/97124791
')
INSERT [dbo].[courses] ([course_id], [course_name], [description], [is_publish], [course_cover], [course_url]) VALUES (N'C0002', N'C#/.Net高级进阶VIP班', N'.Net高级进阶课程，深入C#，程序设计定向培养，深入常用框架应用并扩展定制，学习跨平台微服务等最新技术，快速成长为团队的优秀carry！', 1, N'http://cdn2.ziti7.com//qrcode/uploaded2/11/60/0ss/b5/6eede52f86b55a87eae52e2381cf1f.jpg!larger2
', N'http://www.ziti3.com/qrcode/q/15/97124791
')
INSERT [dbo].[courses] ([course_id], [course_name], [description], [is_publish], [course_cover], [course_url]) VALUES (N'C0003', N'Java高级实战VIP班', N'旨在培养满足一线互联网需求的JAVA架构师人才，VIP课内容涵盖酒店系统，朝夕培训平台，易淘电商三大项目，项目取自企业中真实项目需求，是您提升项目开发和进阶的首选课程', 1, N'http://cdn2.ziti7.com//qrcode/uploaded2/11/60/0ss/b5/6eede52f86b55a87eae52e2381cf1f.jpg!larger2
', N'http://www.ziti3.com/qrcode/q/15/97124791
')
INSERT [dbo].[courses] ([course_id], [course_name], [description], [is_publish], [course_cover], [course_url]) VALUES (N'C0004', N'C#/.Net全栈VIP班', N'全栈信仰，从C#到Sql到Ado.Net到O/RM，从Html到CSS到JS到vue等前端框架，从Webform到MVC到Asp.NetCore，多个完整项目实战锤炼你的动手能力！', 1, N'http://cdn2.ziti7.com//qrcode/uploaded2/11/60/0ss/b5/6eede52f86b55a87eae52e2381cf1f.jpg!larger2
', N'http://www.ziti3.com/qrcode/q/15/97124791
')
INSERT [dbo].[courses] ([course_id], [course_name], [description], [is_publish], [course_cover], [course_url]) VALUES (N'C0005', N'Winfom高级就业班', N'零基础学习SqlServer和Winform客户端开发，上百视频+三月直播，轻松入门解决就业，毕业交付可实用产品项目！', 1, N'http://cdn2.ziti7.com//qrcode/uploaded2/11/60/0ss/b5/6eede52f86b55a87eae52e2381cf1f.jpg!larger2
', N'http://www.ziti3.com/qrcode/q/15/97124791
')
INSERT [dbo].[courses] ([course_id], [course_name], [description], [is_publish], [course_cover], [course_url]) VALUES (N'C0006', N'AutoCAD/C#零基础到项目实战', N'覆盖.Net开发过程中遇到的框架组件解读，语法深入，跨平台开发，大数据高并发等专题体验课合集，持续更新', 1, N'http://cdn2.ziti7.com//qrcode/uploaded2/11/60/0ss/b5/6eede52f86b55a87eae52e2381cf1f.jpg!larger2
', N'http://www.ziti3.com/qrcode/q/15/97124791
')
INSERT [dbo].[courses] ([course_id], [course_name], [description], [is_publish], [course_cover], [course_url]) VALUES (N'C0007', N'C#/.Net架构师修养', N'覆盖.Net开发过程中遇到的框架组件解读，语法深入，跨平台开发，大数据高并发等专题体验课合集，持续更新！', 1, N'http://cdn2.ziti7.com//qrcode/uploaded2/11/60/0ss/b5/6eede52f86b55a87eae52e2381cf1f.jpg!larger2
', N'http://www.ziti3.com/qrcode/q/15/97124791
')
INSERT [dbo].[courses] ([course_id], [course_name], [description], [is_publish], [course_cover], [course_url]) VALUES (N'C0008', N'构师必备框架组件', N'专题录制开发组件，含Autofac/log4net/Nginx/读写分离/GIT/Core-Linux等全套视频课件配套环境等！', 1, N'http://cdn2.ziti7.com//qrcode/uploaded2/11/60/0ss/b5/6eede52f86b55a87eae52e2381cf1f.jpg!larger2
', N'http://www.ziti3.com/qrcode/q/15/97124791
')
INSERT [dbo].[courses] ([course_id], [course_name], [description], [is_publish], [course_cover], [course_url]) VALUES (N'C0009', N'Web前端零基础教程', N'2020年最新Web前端零基础教程，含Html/CSS/JavaScript系统教程，被称之为“地球最好”的零基础教程！', 1, N'http://cdn2.ziti7.com//qrcode/uploaded2/11/60/0ss/b5/6eede52f86b55a87eae52e2381cf1f.jpg!larger2
', N'http://www.ziti3.com/qrcode/q/15/97124791
')
INSERT [dbo].[courses] ([course_id], [course_name], [description], [is_publish], [course_cover], [course_url]) VALUES (N'C0010', N'零基础Winform实战学生管理系统', N'基于C#完成AutoCAD二次开发，支持C#和AutoCAD双零基础快速入门并独立完成项目实战，成为稀缺领域人才，一站式搞定高薪就业！', 1, N'http://cdn2.ziti7.com//qrcode/uploaded2/11/60/0ss/b5/6eede52f86b55a87eae52e2381cf1f.jpg!larger2
', N'http://www.ziti3.com/qrcode/q/15/97124791
')
GO
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (1, N'汉族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (2, N'蒙古族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (3, N'回族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (4, N'藏族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (5, N'维吾尔族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (6, N'苗族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (7, N'彝族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (8, N'壮族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (9, N'布依族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (10, N'朝鲜族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (11, N'满族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (12, N'侗族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (13, N'瑶族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (14, N'白族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (15, N'土家族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (16, N'哈尼族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (17, N'哈萨克族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (18, N'傣族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (19, N'黎族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (20, N'傈僳族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (21, N'佤族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (22, N'畲族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (23, N'高山族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (24, N'拉祜族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (25, N'水族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (26, N'东乡族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (27, N'纳西族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (28, N'景颇族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (29, N'柯尔克孜族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (30, N'土族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (31, N'达斡尔族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (32, N'仫佬族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (33, N'羌族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (34, N'布朗族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (35, N'撒拉族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (36, N'毛难族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (37, N'仡佬族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (38, N'锡伯族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (39, N'阿昌族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (40, N'普米族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (41, N'塔吉克族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (42, N'怒族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (43, N'乌孜别克族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (44, N'俄罗斯族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (45, N'鄂温克族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (46, N'崩龙族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (47, N'保安族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (48, N'裕固族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (49, N'京族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (50, N'塔塔尔族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (51, N'独龙族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (52, N'鄂伦春族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (53, N'赫哲族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (54, N'门巴族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (55, N'珞巴族')
INSERT [dbo].[nations] ([id], [nations_name]) VALUES (56, N'基诺族')
GO
INSERT [dbo].[platforms] ([platform_id], [platform_name], [is_validation]) VALUES (N'PF001', N'云课堂', 1)
INSERT [dbo].[platforms] ([platform_id], [platform_name], [is_validation]) VALUES (N'PF002', N'B站', 1)
INSERT [dbo].[platforms] ([platform_id], [platform_name], [is_validation]) VALUES (N'PF003', N'知乎', 1)
INSERT [dbo].[platforms] ([platform_id], [platform_name], [is_validation]) VALUES (N'PF004', N'抖音', 1)
INSERT [dbo].[platforms] ([platform_id], [platform_name], [is_validation]) VALUES (N'PF005', N'博客', 1)
GO
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0001', N'PF001', CAST(161 AS Decimal(18, 0)), 0, CAST(-75 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0001', N'PF002', CAST(283 AS Decimal(18, 0)), 1, CAST(4 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0001', N'PF003', CAST(568 AS Decimal(18, 0)), 1, CAST(22 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0001', N'PF004', CAST(41 AS Decimal(18, 0)), 1, CAST(77 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0001', N'PF005', CAST(678 AS Decimal(18, 0)), 1, CAST(91 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0002', N'PF001', CAST(207 AS Decimal(18, 0)), 0, CAST(-43 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0002', N'PF002', CAST(930 AS Decimal(18, 0)), 0, CAST(-84 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0002', N'PF003', CAST(218 AS Decimal(18, 0)), 1, CAST(6 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0002', N'PF004', CAST(107 AS Decimal(18, 0)), 1, CAST(4 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0002', N'PF005', CAST(420 AS Decimal(18, 0)), 1, CAST(31 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0003', N'PF001', CAST(512 AS Decimal(18, 0)), 1, CAST(25 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0003', N'PF002', CAST(921 AS Decimal(18, 0)), 1, CAST(86 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0003', N'PF003', CAST(161 AS Decimal(18, 0)), 0, CAST(-29 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0003', N'PF004', CAST(918 AS Decimal(18, 0)), 0, CAST(-87 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0003', N'PF005', CAST(590 AS Decimal(18, 0)), 0, CAST(-98 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0004', N'PF001', CAST(454 AS Decimal(18, 0)), 1, CAST(38 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0004', N'PF002', CAST(443 AS Decimal(18, 0)), 0, CAST(-75 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0004', N'PF003', CAST(68 AS Decimal(18, 0)), 0, CAST(-72 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0004', N'PF004', CAST(274 AS Decimal(18, 0)), 0, CAST(-40 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0004', N'PF005', CAST(678 AS Decimal(18, 0)), 0, CAST(-42 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0005', N'PF001', CAST(264 AS Decimal(18, 0)), 1, CAST(64 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0005', N'PF002', CAST(53 AS Decimal(18, 0)), 0, CAST(-64 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0005', N'PF003', CAST(638 AS Decimal(18, 0)), 0, CAST(-18 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0005', N'PF004', CAST(404 AS Decimal(18, 0)), 1, CAST(49 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0005', N'PF005', CAST(862 AS Decimal(18, 0)), 1, CAST(78 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0006', N'PF001', CAST(741 AS Decimal(18, 0)), 1, CAST(57 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0006', N'PF002', CAST(430 AS Decimal(18, 0)), 1, CAST(96 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0006', N'PF003', CAST(490 AS Decimal(18, 0)), 1, CAST(13 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0006', N'PF004', CAST(82 AS Decimal(18, 0)), 0, CAST(-38 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0006', N'PF005', CAST(221 AS Decimal(18, 0)), 1, CAST(66 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0007', N'PF001', CAST(389 AS Decimal(18, 0)), 1, CAST(54 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0007', N'PF002', CAST(378 AS Decimal(18, 0)), 0, CAST(-21 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0007', N'PF003', CAST(902 AS Decimal(18, 0)), 0, CAST(-66 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0007', N'PF004', CAST(548 AS Decimal(18, 0)), 1, CAST(18 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0007', N'PF005', CAST(497 AS Decimal(18, 0)), 0, CAST(-61 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0008', N'PF001', CAST(876 AS Decimal(18, 0)), 0, CAST(-64 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0008', N'PF002', CAST(50 AS Decimal(18, 0)), 0, CAST(-9 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0008', N'PF003', CAST(918 AS Decimal(18, 0)), 1, CAST(92 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0008', N'PF004', CAST(867 AS Decimal(18, 0)), 1, CAST(53 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0008', N'PF005', CAST(198 AS Decimal(18, 0)), 0, CAST(-47 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0009', N'PF001', CAST(451 AS Decimal(18, 0)), 1, CAST(39 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0009', N'PF002', CAST(155 AS Decimal(18, 0)), 1, CAST(88 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0009', N'PF003', CAST(689 AS Decimal(18, 0)), 1, CAST(54 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0009', N'PF004', CAST(822 AS Decimal(18, 0)), 1, CAST(44 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0009', N'PF005', CAST(915 AS Decimal(18, 0)), 0, CAST(-56 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0010', N'PF001', CAST(299 AS Decimal(18, 0)), 0, CAST(-64 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0010', N'PF002', CAST(339 AS Decimal(18, 0)), 0, CAST(-44 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0010', N'PF003', CAST(481 AS Decimal(18, 0)), 1, CAST(37 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0010', N'PF004', CAST(902 AS Decimal(18, 0)), 0, CAST(-92 AS Decimal(18, 0)))
INSERT [dbo].[play_record] ([course_id], [platform_id], [play_count], [is_growing], [growing_rate]) VALUES (N'C0010', N'PF005', CAST(99 AS Decimal(18, 0)), 1, CAST(11 AS Decimal(18, 0)))
GO
SET IDENTITY_INSERT [dbo].[politics_status] ON 

INSERT [dbo].[politics_status] ([id], [politics_status]) VALUES (1, N'群众                  ')
INSERT [dbo].[politics_status] ([id], [politics_status]) VALUES (2, N'共青团员                ')
INSERT [dbo].[politics_status] ([id], [politics_status]) VALUES (3, N'共产党员                ')
INSERT [dbo].[politics_status] ([id], [politics_status]) VALUES (4, N'民主党派人士              ')
INSERT [dbo].[politics_status] ([id], [politics_status]) VALUES (5, N'中共预备党员              ')
SET IDENTITY_INSERT [dbo].[politics_status] OFF
GO
SET IDENTITY_INSERT [dbo].[students] ON 

INSERT [dbo].[students] ([id], [number], [name], [sex], [birthday], [nation_id], [politics_status_id], [grade], [site], [phone], [is_delete], [gmt_create], [gmt_modified]) VALUES (1, N'191203027', N'XXX', 1, CAST(N'2020-03-12T00:00:00.000' AS DateTime), 1, 2, N'19级计算机应用班', N'广西贺州市八步区XXXX', N'18078097982', 0, CAST(N'2022-04-09T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[students] ([id], [number], [name], [sex], [birthday], [nation_id], [politics_status_id], [grade], [site], [phone], [is_delete], [gmt_create], [gmt_modified]) VALUES (2, N'191304004', N'XXX', 2, CAST(N'2020-03-12T00:00:00.000' AS DateTime), 1, 2, N'19级旅游管理', N'广西贺州市平桂区XXX', N'18376413933', 0, CAST(N'2022-04-09T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[students] ([id], [number], [name], [sex], [birthday], [nation_id], [politics_status_id], [grade], [site], [phone], [is_delete], [gmt_create], [gmt_modified]) VALUES (3, N'202202002', N'瞿雨州', 2, CAST(N'2011-01-01T00:00:00.000' AS DateTime), 1, 2, N'19级旅游管理', N'广西贺州市XXXXXX', N'12343234234', 0, CAST(N'2022-04-09T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[students] ([id], [number], [name], [sex], [birthday], [nation_id], [politics_status_id], [grade], [site], [phone], [is_delete], [gmt_create], [gmt_modified]) VALUES (4, N'202222022', N'吴庄岑', 2, CAST(N'2015-02-24T00:00:00.000' AS DateTime), 1, 4, N'19级计算机应用班', N'广西贺州市八步区XXXX', N'17342053145', 0, CAST(N'2022-04-09T23:10:54.000' AS DateTime), NULL)
INSERT [dbo].[students] ([id], [number], [name], [sex], [birthday], [nation_id], [politics_status_id], [grade], [site], [phone], [is_delete], [gmt_create], [gmt_modified]) VALUES (6, N'253196986', N'江童焰', 2, CAST(N'2022-03-28T00:00:00.000' AS DateTime), 1, 1, N'11级医疗班', N'上海市宝山区', N'13641487803', 1, CAST(N'2022-04-10T00:00:00.000' AS DateTime), CAST(N'2022-04-13T00:12:17.567' AS DateTime))
INSERT [dbo].[students] ([id], [number], [name], [sex], [birthday], [nation_id], [politics_status_id], [grade], [site], [phone], [is_delete], [gmt_create], [gmt_modified]) VALUES (7, N'447947339', N'庞晴奇', 1, CAST(N'2010-01-27T00:00:00.000' AS DateTime), 1, 1, N'12级艺术综合班', N'浙江省杭州市滨江区', N'17344072251', 0, CAST(N'2022-04-10T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[students] ([id], [number], [name], [sex], [birthday], [nation_id], [politics_status_id], [grade], [site], [phone], [is_delete], [gmt_create], [gmt_modified]) VALUES (8, N'341601929', N'韩宝煦', 1, CAST(N'2013-02-27T00:00:00.000' AS DateTime), 1, 1, N'13级商务英语', N'上海浦东新区', N'13358815807', 1, CAST(N'2022-04-10T00:00:00.000' AS DateTime), CAST(N'2022-04-12T23:18:37.230' AS DateTime))
INSERT [dbo].[students] ([id], [number], [name], [sex], [birthday], [nation_id], [politics_status_id], [grade], [site], [phone], [is_delete], [gmt_create], [gmt_modified]) VALUES (9, N'721805028', N'瞿令奕', 1, CAST(N'1981-02-27T00:00:00.000' AS DateTime), 1, 1, N'19级网络营销', N'杭州市上城区', N'13775023953', 0, CAST(N'2022-04-10T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[students] ([id], [number], [name], [sex], [birthday], [nation_id], [politics_status_id], [grade], [site], [phone], [is_delete], [gmt_create], [gmt_modified]) VALUES (10, N'626520706', N'方郁余', 1, CAST(N'2022-04-10T00:00:00.000' AS DateTime), 1, 1, N'22级计算机网络', N'上海市浦东小区', N'17363564895', 0, CAST(N'2022-04-10T23:10:20.940' AS DateTime), NULL)
INSERT [dbo].[students] ([id], [number], [name], [sex], [birthday], [nation_id], [politics_status_id], [grade], [site], [phone], [is_delete], [gmt_create], [gmt_modified]) VALUES (11, N'测试', N'测试', 0, CAST(N'2012-02-01T00:00:00.000' AS DateTime), 1, 1, N'测试', N'测试', N'测试', 1, CAST(N'2022-04-12T18:36:06.067' AS DateTime), CAST(N'2022-04-13T00:35:12.333' AS DateTime))
INSERT [dbo].[students] ([id], [number], [name], [sex], [birthday], [nation_id], [politics_status_id], [grade], [site], [phone], [is_delete], [gmt_create], [gmt_modified]) VALUES (5, N'218688728', N'蒋舸冰', 2, CAST(N'2022-03-28T00:00:00.000' AS DateTime), 1, 1, N'18级艺术', N'上海市徐汇区', N'15764938939', 0, CAST(N'2022-04-09T23:23:50.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[students] OFF
GO
INSERT [dbo].[users] ([user_id], [user_name], [real_name], [password], [is_validation], [is_can_login], [is_teacher], [avatar], [gender]) VALUES (N'1', N'1', N'1', N'1', 1, 1, 1, N'../Assets/Image/12.jpg', 1)
INSERT [dbo].[users] ([user_id], [user_name], [real_name], [password], [is_validation], [is_can_login], [is_teacher], [avatar], [gender]) VALUES (N'1001', N'admin', N'Administrator', N'123456', 1, 1, 0, N'../Assets/Image/12.jpg', 1)
INSERT [dbo].[users] ([user_id], [user_name], [real_name], [password], [is_validation], [is_can_login], [is_teacher], [avatar], [gender]) VALUES (N'1002', N'guest', N'Guest', N'123456', 1, 0, 0, N'../Assets/Image/12.jpg', 1)
INSERT [dbo].[users] ([user_id], [user_name], [real_name], [password], [is_validation], [is_can_login], [is_teacher], [avatar], [gender]) VALUES (N'1003', N'eleven', N'Eleven', N'123456', 1, 1, 1, N'../Assets/Image/12.jpg', 1)
INSERT [dbo].[users] ([user_id], [user_name], [real_name], [password], [is_validation], [is_can_login], [is_teacher], [avatar], [gender]) VALUES (N'1004', N'richard', N'Richard', N'123456', 1, 1, 1, N'../Assets/Image/12.jpg', 1)
INSERT [dbo].[users] ([user_id], [user_name], [real_name], [password], [is_validation], [is_can_login], [is_teacher], [avatar], [gender]) VALUES (N'1005', N'clay', N'Clay', N'123456', 1, 1, 1, N'../Assets/Image/12.jpg', 1)
INSERT [dbo].[users] ([user_id], [user_name], [real_name], [password], [is_validation], [is_can_login], [is_teacher], [avatar], [gender]) VALUES (N'1006', N'garry', N'Garry', N'123456', 1, 1, 1, N'../Assets/Image/12.jpg', 1)
INSERT [dbo].[users] ([user_id], [user_name], [real_name], [password], [is_validation], [is_can_login], [is_teacher], [avatar], [gender]) VALUES (N'1007', N'ace', N'Ace', N'123456', 1, 1, 1, N'../Assets/Image/12.jpg', 1)
INSERT [dbo].[users] ([user_id], [user_name], [real_name], [password], [is_validation], [is_can_login], [is_teacher], [avatar], [gender]) VALUES (N'1008', N'leah', N'Leah', N'123456', 1, 1, 1, N'../Assets/Image/12.jpg', 2)
INSERT [dbo].[users] ([user_id], [user_name], [real_name], [password], [is_validation], [is_can_login], [is_teacher], [avatar], [gender]) VALUES (N'1009', N'jovan', N'Jovan', N'123456', 1, 1, 1, N'../Assets/Image/12.jpg', 1)
GO
ALTER TABLE [dbo].[courses] ADD  CONSTRAINT [DF_courses_course_cover]  DEFAULT ('') FOR [course_cover]
GO
ALTER TABLE [dbo].[courses] ADD  CONSTRAINT [DF_courses_course_url]  DEFAULT ('') FOR [course_url]
GO
ALTER TABLE [dbo].[students] ADD  DEFAULT ((1)) FOR [nation_id]
GO
ALTER TABLE [dbo].[students] ADD  DEFAULT ((1)) FOR [politics_status_id]
GO
ALTER TABLE [dbo].[students] ADD  CONSTRAINT [DF_students_is_delete]  DEFAULT ((0)) FOR [is_delete]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图书编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'book_details', @level2type=N'COLUMN',@level2name=N'book_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图书名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'book_details', @level2type=N'COLUMN',@level2name=N'book_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'book_details', @level2type=N'COLUMN',@level2name=N'book_author'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'封面' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'book_details', @level2type=N'COLUMN',@level2name=N'book_image'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图书价格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'book_details', @level2type=N'COLUMN',@level2name=N'book_price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出版社' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'book_details', @level2type=N'COLUMN',@level2name=N'book_publisher'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出版日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'book_details', @level2type=N'COLUMN',@level2name=N'book_issuing'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'book_details', @level2type=N'COLUMN',@level2name=N'book_words'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'页数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'book_details', @level2type=N'COLUMN',@level2name=N'book_pagination'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'介绍' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'book_details', @level2type=N'COLUMN',@level2name=N'book_introduction'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'链接' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'book_details', @level2type=N'COLUMN',@level2name=N'book_url'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'students', @level2type=N'COLUMN',@level2name=N'number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'students', @level2type=N'COLUMN',@level2name=N'name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性别：0：未知 1：男 2：女' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'students', @level2type=N'COLUMN',@level2name=N'sex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生日' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'students', @level2type=N'COLUMN',@level2name=N'birthday'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'民族' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'students', @level2type=N'COLUMN',@level2name=N'nation_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'政治面貌' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'students', @level2type=N'COLUMN',@level2name=N'politics_status_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'班级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'students', @level2type=N'COLUMN',@level2name=N'grade'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'students', @level2type=N'COLUMN',@level2name=N'site'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'students', @level2type=N'COLUMN',@level2name=N'phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除：0：删除  1：未删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'students', @level2type=N'COLUMN',@level2name=N'is_delete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'students', @level2type=N'COLUMN',@level2name=N'gmt_create'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'students', @level2type=N'COLUMN',@level2name=N'gmt_modified'
GO
