USE [master]
GO
/****** Object:  Database [JJMdb]    Script Date: 2020/12/10 下午 04:09:13 ******/
CREATE DATABASE [JJMdb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'JJMdb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\JJMdb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'JJMdb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\JJMdb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [JJMdb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [JJMdb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [JJMdb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [JJMdb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [JJMdb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [JJMdb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [JJMdb] SET ARITHABORT OFF 
GO
ALTER DATABASE [JJMdb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [JJMdb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [JJMdb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [JJMdb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [JJMdb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [JJMdb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [JJMdb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [JJMdb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [JJMdb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [JJMdb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [JJMdb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [JJMdb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [JJMdb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [JJMdb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [JJMdb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [JJMdb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [JJMdb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [JJMdb] SET RECOVERY FULL 
GO
ALTER DATABASE [JJMdb] SET  MULTI_USER 
GO
ALTER DATABASE [JJMdb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [JJMdb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [JJMdb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [JJMdb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [JJMdb] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'JJMdb', N'ON'
GO
ALTER DATABASE [JJMdb] SET QUERY_STORE = OFF
GO
USE [JJMdb]
GO
/****** Object:  Table [dbo].[tBill]    Script Date: 2020/12/10 下午 04:09:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tBill](
	[billID] [int] IDENTITY(1,1) NOT NULL,
	[b_name] [nvarchar](50) NULL,
	[b_cardnumber] [int] NULL,
	[b_expireMonth] [int] NULL,
	[b_expireYear] [int] NULL,
	[b_code] [int] NULL,
	[b_billing] [nvarchar](255) NULL,
	[memberID] [int] NULL,
 CONSTRAINT [PK_Bill] PRIMARY KEY CLUSTERED 
(
	[billID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tClass]    Script Date: 2020/12/10 下午 04:09:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tClass](
	[classID] [int] IDENTITY(1,1) NOT NULL,
	[c_name] [nvarchar](50) NULL,
	[c_nameText] [nvarchar](100) NULL,
	[c_intro] [nvarchar](500) NULL,
	[c_startTime] [date] NULL,
	[c_endTime] [date] NULL,
	[c_hourRate] [int] NULL,
	[c_registerStart] [date] NULL,
	[c_registerEnd] [date] NULL,
	[c_maxStudent] [int] NULL,
	[c_minStudent] [int] NULL,
	[c_student] [int] NULL,
	[c_location] [nvarchar](100) NULL,
	[c_price] [int] NULL,
	[c_onsaleStart] [date] NULL,
	[c_onsaleEnd] [date] NULL,
	[c_discount] [int] NULL,
	[c_level] [nvarchar](10) NULL,
	[c_requirement] [nvarchar](500) NULL,
	[c_rate] [float] NULL,
	[c_rateTotal] [int] NULL,
	[c_imgPath1] [nvarchar](max) NULL,
	[c_imgPath2] [nvarchar](max) NULL,
	[c_imgPath3] [nvarchar](max) NULL,
	[c_lable1] [nvarchar](20) NULL,
	[c_lable2] [nvarchar](20) NULL,
	[c_lable3] [nvarchar](20) NULL,
	[c_lable4] [nvarchar](20) NULL,
	[c_lable5] [nvarchar](20) NULL,
	[teacherID] [int] NULL,
	[c_getNow] [datetime] NULL,
 CONSTRAINT [PK_Class] PRIMARY KEY CLUSTERED 
(
	[classID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tDeposit]    Script Date: 2020/12/10 下午 04:09:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tDeposit](
	[depositID] [int] IDENTITY(1,1) NOT NULL,
	[d_point] [int] NULL,
	[d_method] [nvarchar](50) NULL,
	[d_memo] [nvarchar](50) NULL,
	[d_getNow] [datetime] NULL,
	[memberID] [int] NULL,
 CONSTRAINT [PK_tDeposit] PRIMARY KEY CLUSTERED 
(
	[depositID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tMember]    Script Date: 2020/12/10 下午 04:09:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tMember](
	[memberID] [int] IDENTITY(1,1) NOT NULL,
	[m_firstName] [nvarchar](50) NULL,
	[m_lastName] [nvarchar](50) NULL,
	[m_birth] [date] NULL,
	[m_gender] [nvarchar](10) NULL,
	[m_email] [nvarchar](50) NULL,
	[m_password] [nvarchar](50) NULL,
	[m_phone] [nvarchar](50) NULL,
	[m_district] [nvarchar](255) NULL,
	[m_address] [nvarchar](255) NULL,
	[m_role] [int] NULL,
	[m_hobby] [nvarchar](255) NULL,
	[m_imgPath] [nvarchar](max) NULL,
	[m_Jpoint] [int] NULL,
	[m_getNow] [datetime] NULL,
	[m_emailConfirm] [nchar](15) NULL,
 CONSTRAINT [PK_member] PRIMARY KEY CLUSTERED 
(
	[memberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tMessage]    Script Date: 2020/12/10 下午 04:09:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tMessage](
	[messageID] [int] IDENTITY(1,1) NOT NULL,
	[m_send] [nvarchar](50) NULL,
	[m_sendText] [nvarchar](500) NULL,
	[m_sendTime] [datetime] NULL,
	[m_reply] [nvarchar](50) NULL,
	[m_replyText] [nvarchar](500) NULL,
	[m_replyTime] [datetime] NULL,
	[m_status] [nvarchar](10) NULL,
	[memberID] [int] NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[messageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tOrder]    Script Date: 2020/12/10 下午 04:09:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tOrder](
	[orderID] [int] IDENTITY(1,1) NOT NULL,
	[memberID] [int] NULL,
	[o_orderdate] [datetime] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[orderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tOrder_Detail]    Script Date: 2020/12/10 下午 04:09:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tOrder_Detail](
	[od_itemID] [int] IDENTITY(1,1) NOT NULL,
	[orderID] [int] NULL,
	[classID] [int] NULL,
	[c_name] [nvarchar](50) NULL,
	[c_price] [int] NULL,
	[c_discount] [int] NULL,
	[od_profit] [int] NULL,
 CONSTRAINT [PK_Order_Detail] PRIMARY KEY CLUSTERED 
(
	[od_itemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tPay]    Script Date: 2020/12/10 下午 04:09:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tPay](
	[payID] [int] IDENTITY(1,1) NOT NULL,
	[p_money] [int] NULL,
	[p_getNow] [datetime] NULL,
	[p_status] [nvarchar](20) NULL,
	[p_method] [nvarchar](50) NULL,
	[p_getMoneyTime] [datetime] NULL,
	[p_memo] [nvarchar](50) NULL,
	[teacherID] [int] NULL,
 CONSTRAINT [PK_Pay] PRIMARY KEY CLUSTERED 
(
	[payID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tRating]    Script Date: 2020/12/10 下午 04:09:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tRating](
	[ratingID] [int] IDENTITY(1,1) NOT NULL,
	[memberID] [int] NULL,
	[classID] [int] NULL,
	[r_send] [nvarchar](20) NULL,
	[r_sendText] [nvarchar](1000) NULL,
	[r_star] [int] NULL,
	[r_sendTime] [datetime] NULL,
 CONSTRAINT [PK_Rating] PRIMARY KEY CLUSTERED 
(
	[ratingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tShop]    Script Date: 2020/12/10 下午 04:09:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tShop](
	[shopID] [int] IDENTITY(1,1) NOT NULL,
	[classID] [int] NULL,
	[memberID] [int] NULL,
	[s_getNow] [datetime] NULL,
 CONSTRAINT [PK_Shop] PRIMARY KEY CLUSTERED 
(
	[shopID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tTeacher]    Script Date: 2020/12/10 下午 04:09:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tTeacher](
	[teacherID] [int] IDENTITY(1,1) NOT NULL,
	[t_certificateImg1] [nvarchar](max) NULL,
	[t_certificateImg2] [nvarchar](max) NULL,
	[t_certificateImg3] [nvarchar](max) NULL,
	[t_certificateTxt] [nvarchar](max) NULL,
	[t_title] [nvarchar](255) NULL,
	[t_intro] [nvarchar](500) NULL,
	[t_experience] [nvarchar](500) NULL,
	[t_expertise] [nvarchar](500) NULL,
	[t_messageTotal] [int] NULL,
	[t_moneyTotal] [int] NULL,
	[t_money] [int] NULL,
	[t_studentTotal] [int] NULL,
	[t_classTotal] [int] NULL,
	[t_rateAvg] [float] NULL,
	[t_socialMedia1] [nvarchar](500) NULL,
	[t_socialMedia2] [nvarchar](500) NULL,
	[t_socialMedia3] [nvarchar](500) NULL,
	[t_socialMedia4] [nvarchar](500) NULL,
	[memberID] [int] NULL,
	[t_getNow] [datetime] NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[teacherID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tWish]    Script Date: 2020/12/10 下午 04:09:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tWish](
	[WishID] [int] IDENTITY(1,1) NOT NULL,
	[classID] [int] NULL,
	[memberID] [int] NULL,
	[s_getNow] [datetime] NULL,
 CONSTRAINT [PK_Wish] PRIMARY KEY CLUSTERED 
(
	[WishID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tClass] ON 

INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (28, N'流瑜伽', N'適合一般大眾或想學習阿斯坦加瑜珈的學員', N'什麼是流瑜珈？ 
流瑜珈是建立在阿斯坦加瑜珈之上的一種瑜珈類型，又稱作Vinyasa Yoga、Vinyasa Flow。 Flow 的意思是流動、流暢，而Vinyasa 的意思是串聯。 
1.注重動作與呼吸的搭配 
2.強度介於哈達瑜珈和阿斯湯伽瑜伽之間 
3.過程猶如跳舞般行雲流水，能消耗不少的熱量，有助於減肥塑身  
流瑜珈的好處 
生理上： 
1. 促進心血管功能 
2. 增加柔軟度 
3. 放鬆僵硬的關節 
4. 緊實肌肉 
心理上： 
1. 幫助舒緩壓力與焦慮感 
2. 提升能量 
3. 改善整體狀態 
4. 有助於內在的專注力', CAST(N'2020-12-07' AS Date), CAST(N'2020-12-30' AS Date), 5, CAST(N'2020-11-01' AS Date), CAST(N'2020-12-06' AS Date), 8, 2, 0, N'高雄前金區圈圈路999號', 5000, CAST(N'2020-11-01' AS Date), CAST(N'2020-11-15' AS Date), 4500, N'中級', N'*上課穿著 
1.緊身褲或短褲：緊身褲、七分褲或短褲都適合練習流瑜珈。最好找一件高腰又透氣的衣物，才能在練習中保護你不走光喔。 
2.合適的上衣：選擇透氣且較合身的上衣，避免在運動過程中因服飾而分心
3.瑜珈墊與其他配件和輔具由課程教室提供 
4.請自備水壺及毛巾', 4, 0, N'/Content/imgClass/b5c65852-47f7-4f9d-ad7c-27db9bdd47fb.jpg', N'/Content/imgClass/fb23c801-f2c2-4e06-b5b5-17e77fdf334f.jpg', N'/Content/imgClass/96dcd3aa-30a2-41e1-aa94-d54e34c76ee2.jpg', N'室內健身', N'瑜珈', N'團體課程', N'前金區', N'瑜珈', 3, CAST(N'2020-12-08T11:24:30.720' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (29, N'熱瑜珈', N'熱瑜珈可以讓我們的身體快速暖和以達更深層的伸展', N'什麼是熱瑜珈？
在高溫 (36-40°C) 與高濕度環境，跟著呼吸的節奏，進行一套約26到42個體位的肢體伸展動作。對於增加體內新陳代謝、身體排毒、增加身體柔軟度和肌力有很大的幫助。熱瑜珈的好處
生理上：
1. 改善柔軟度
2. 加強肌力與平衡感
3. 促進身體代謝
4. 幫助體內排毒
心理上：
1. 減輕壓力與焦慮感
2. 放鬆緊繃的感覺
3. 使內心平靜
4. 提高專注力', CAST(N'2020-12-07' AS Date), CAST(N'2020-12-30' AS Date), 8, CAST(N'2020-11-01' AS Date), CAST(N'2020-12-06' AS Date), 8, 2, 0, N'高雄前金區圈圈路999號', 8000, CAST(N'2020-11-11' AS Date), CAST(N'2020-11-15' AS Date), 7500, N'大師級', N'*上課穿著
1. 補充水分：熱瑜珈整堂課都是在 40 度左右的高溫下做瑜珈，必定汗如雨下，通常熱瑜珈老師也會提醒在練習時該在何時喝水。  2. 課前兩小時空腹：大部分的瑜珈都建議課前空腹，因為瑜珈有許多前彎、後彎、扭轉等等，都會影響到胃與腹部，尤其熱瑜珈教室高溫悶熱，如果課前進食容易在過程中感到不舒服。  3. 量力而為：表現出謙遜以及對自己的尊重，不與他人比較，必須在練習中傾聽你的身體，避免超出自己的極限。  4. 保持呼吸：在過程中調整呼吸，避免過喘或呼吸不順暢，一有不適就應該停下休息。  熱瑜珈是一門需要很多耐心的課程，許多瑜珈練習者都認為需要很多的練習才能適應，也才能更專注。因此，建議可以先參加較為和緩的熱瑜珈課程，再決定這種瑜珈類型是否適合你。', 3, 0, N'/Content/imgClass/0108ab35-600d-458d-9cca-01b98d4d4bff.jpg', N'/Content/imgClass/9d9e65e0-2b20-45ae-8573-89b8e9148464.jpg', N'/Content/imgClass/9bf3a5df-3c86-4e73-9eed-f987072d20eb.jpg', N'室內健身', N'瑜珈', N'團體課程', N'前金區', N'瑜珈', 3, CAST(N'2020-12-08T11:29:54.143' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (30, N'修復瑜珈', N'適合曾有運動傷害／舊疾不易痊癒者，熱愛運動者、家庭主婦、辦公室族群，有效改善上半身(後背部) 下半身 (膝蓋、前後側大腿)等不適', N'舒緩身體因舊疾復發而產生的不適感，並開始學習在運動當中 (瑜珈或健身) 清楚了解身體的肌肉作動，進一步正確的控制身體並預防再次受傷', CAST(N'2020-12-14' AS Date), CAST(N'2020-12-30' AS Date), 20, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-13' AS Date), 1, 1, 0, N'高雄前金區圈圈路999號', 12000, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-11' AS Date), 11000, N'初級', N'*上課穿著 1.緊身褲或短褲：緊身褲、七分褲或短褲都適合。最好找一件高腰又透氣的衣物，才能在練習中保護你不走光喔。 2.合適的上衣：選擇透氣且較合身的上衣，避免在運動過程中因服飾而分心3.瑜珈墊與其他配件和輔具由課程教室提供 4.請自備水壺及毛巾', 4, 0, N'/Content/imgClass/10524299-23ab-49cf-acde-860ceb20b3cc.jpg', N'/Content/imgClass/47ff4b16-c5cd-4187-aa51-3573131472ce.jpg', N'/Content/imgClass/9bcdaa95-4c2b-4655-bdc3-b053b60c2b4a.jpg', N'室內健身', N'瑜珈', N'一對一課程', N'前金區', N'瑜珈', 3, CAST(N'2020-12-08T11:39:00.583' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (31, N'空中瑜伽', N'練習過程中會比其他瑜伽多練習到核心肌群和平常較少使用的肌肉群，此外，練習空中瑜伽，也可訓練身體的平衡感與協調感。', N'現代人因為長期躺坐，姿勢不正確而導致出現脊柱側彎、頸椎疲勞、肩背酸痛等問題，空中瑜珈利用懸吊等物理治療方式，利用自身重力藉以拉伸脊椎，糾正姿勢；同時間因為空中瑜珈招式大多以拉伸為主，也能有效地改善體態。', CAST(N'2020-12-14' AS Date), CAST(N'2020-12-30' AS Date), 20, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-13' AS Date), 10, 2, 0, N'高雄前金區圈圈路999號', 5000, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-11' AS Date), 4500, N'中級', N'空中瑜伽課程建議穿著不過於寬鬆的上衣，以避免倒掛衣服翻落; 儘量穿著緊身長褲，避免摩擦吊床，產生不適。 空中瑜珈課程禁止配帶任何飾品、耳環、手錶等尖銳物品，以避免刮破吊床，造成危險，如強行配戴尖銳物品而損毀吊床，將收取$3000 元吊床費用。 空中瑜珈上課前一小時不吃過飽，也不能太久沒進食，造成血糖過低，易產生暈眩感。', 4, 0, N'/Content/imgClass/e5991031-54e9-4e4c-8f75-c232bfe2d17e.jpg', N'/Content/imgClass/33f0c835-c48f-493c-9c24-4ad878bc499c.jpg', N'/Content/imgClass/fa31a104-2ea9-476c-a3ba-0fc4eab2b93c.jpg', N'室內健身', N'瑜珈', N'團體課程', N'前金區', N'瑜珈', 3, CAST(N'2020-12-08T11:46:00.330' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (32, N'孕婦瑜珈', N'在懷孕滿三個月之後，只要沒有不適，不論妳是完全沒練過瑜珈或是有經驗的練習者，都可以安全的參加孕婦瑜珈課程。 至於可以練到何時則因人而異，只要寶寶和媽媽的條件許可，做孕婦瑜伽的時間可以到產前。', N'練習孕婦瑜珈好處多多
1. 緩解緊張情緒：透過呼吸練習，能帶給媽媽與寶寶活力與生命能量，同時可以穩定情緒、抒發壓力。
2. 改善孕期不適：孕婦瑜伽能增進體內血液循環及新陳代謝，因此孕期不適症狀諸如手腳水腫、腰酸背痛、抽筋都能夠有效舒緩。
3. 增強骨盆韌性與肌耐力：孕婦瑜珈可以幫助加強骨盆、臀大肌、手臂以及肩膀的力量，擁有更多肌耐力來乘載寶寶不斷成長的重量。
4. 為順產做準備：分娩時更容易聽從身體發出的信息及指令，有助於縮短產程。
5. 有助產後體態恢復：鍛鍊肌肉的彈性，有助於產後形體的恢復，預防子宮脫垂、尿失禁等問題。
6. 與寶寶建立更親密聯繫：我們在練習中充分的放鬆身心，媽媽愉快的情緒會直接影響寶寶，能夠與寶寶建立更親密的聯結。', CAST(N'2020-11-06' AS Date), CAST(N'2020-12-06' AS Date), 6, CAST(N'2020-10-01' AS Date), CAST(N'2020-11-05' AS Date), 6, 2, 0, N'高雄前金區圈圈路999號', 3000, CAST(N'2020-10-01' AS Date), CAST(N'2020-10-31' AS Date), 2500, N'初級', N'有不規則宮縮、腹痛、先兆性流產、產前出血或曾經發生流產者，皆不適宜進行練習。
應在用餐後1小時再開始練習，並且穿著寬鬆舒適的衣服。
運動時應該保持穩定的呼吸，需要特別注意關節的角度和肌肉的延展，緩慢地進行動作練習。
學習聆聽且尊重自己的身體，依據身體狀況來決定練習的時間與強度，並且練習中有任何不適，應該立即休息。', 4, 0, N'/Content/imgClass/4f0a83c4-d9f7-4ef6-9d06-947db8e57855.jpg', N'/Content/imgClass/35e6d44a-c35c-4d8e-a722-d6d97f2276ba.jpg', N'/Content/imgClass/e6b27ca0-bf37-484d-8a5c-6e06a277da6d.jpg', N'室內健身', N'瑜珈', N'團體課程', N'前金區', N'瑜珈', 3, CAST(N'2020-12-08T11:50:08.477' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (33, N'親子瑜珈', N'親子瑜伽在歐美各地流行多年，適合3歲以上的小朋友，講求溝通與互動。', N'除了單人、雙人的動作，還包含遊戲、音樂與靜觀等元素。 透過身體接觸，令雙方關係更密切。 既可增進父母與孩子間的感情，同時培養彼此的信賴與親密度。
練習瑜伽的好處有很多。在體能方面，小朋友可以強化大小肌肉、增加身體柔軟度及專注力等，幫助骨骼成長。至於心理方面，有助小朋友控制情緒、建立自信心和改善睡眠質素。而家長亦可以減壓、放鬆精神。', CAST(N'2020-11-06' AS Date), CAST(N'2020-12-06' AS Date), 10, CAST(N'2020-10-01' AS Date), CAST(N'2020-11-05' AS Date), 20, 4, 0, N'高雄前金區圈圈路999號', 10000, CAST(N'2020-10-01' AS Date), CAST(N'2020-10-31' AS Date), 9000, N'初級', N'1.親子瑜伽運動通常會建議由媽媽或爸爸和孩子兩人親自互動，會有較好的效果。有時候也可加入第三人在旁邊提供協助。
2.最好採取「漸進式」的力道，一開始逐漸加重力道，結束動作時逐漸減弱力道，才不會造成孩子身體負擔。
3.建議餐後的1～2小時後才練習，練習完後至少半小時後才可進食。
4.練習之後，可緩慢地飲用不冰的溫開水
5.服裝舒適、透氣即可，勿選擇複雜性高的服裝，另外請父母先取下所有飾品。', 4, 5, N'/Content/imgClass/edd28b02-2740-4554-94b1-4882f9a9cc76.jpg', N'/Content/imgClass/e416c211-2a53-47ed-8817-1228f2bc4602.jpg', N'/Content/imgClass/6039869f-2e09-48fa-813a-cb7723b4309c.jpg', N'室內健身', N'瑜珈', N'團體課程', N'左營區', N'瑜珈', 3, CAST(N'2020-12-08T11:58:02.813' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (35, N'羽球基本動作', N'發短球, 米字步, 高遠球球點', N'基本技術理論教學，包括握拍、執球、熟悉球性、步法、發球、接發球、抽球、高正手擊長球(高遠球)、高正手擊短球(切球)、網前球(挑球、輕彈球)、米字步不法
介紹新舊規則、傷害及預防及羽球禮儀。
上課時間為禮拜二和禮拜五早上6:00 ~ 7:00 共16堂課
', CAST(N'2020-11-14' AS Date), CAST(N'2020-12-10' AS Date), 10, CAST(N'2020-10-01' AS Date), CAST(N'2020-10-30' AS Date), 10, 2, 0, N'高雄市新興區開心國小室內羽球場', 3000, CAST(N'2020-10-01' AS Date), CAST(N'2020-10-25' AS Date), 2800, N'初級', N'當日入場門票需自費。
請自備羽球及球拍。
請穿著運動衣褲。
請自備水壺。請準備乾淨室內羽球運動鞋，攜帶至綜合多功能球場入口處更換後，始可進場。', 3, 0, N'/Content/imgClass/64b2d367-2664-4def-b495-16ca027917a3.jpg', N'/Content/imgClass/1b566107-dae6-4892-8cc0-c1860c554e36.jpg', N'/Content/imgClass/5d1d439a-5951-4b82-a094-cf27491f79fc.jpg', N'球類運動', N'其它', N'團體課程', N'新興區', N'羽球', 6, CAST(N'2020-12-09T11:28:52.860' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (36, N'籃球訓練課程', N'旅美訓練師教你頂級進攻技巧', N'這堂課你能真正學習到
1.自主訓練的技巧
2.運球持球的控制
3.腳步移動的反應
4.上籃終結的技術

誰適合這堂課？
熱愛籃球運動的你
想陪伴孩子一起練球、一起更厲害的家長
想學習專業籃球技術的系籃成員或業餘球友
希望強化自主訓練、以籃球員為目標的學生球員

課程內容
一、運球控球
二、進攻腳步
三、上籃終結
四、投籃腳步
', CAST(N'2020-12-16' AS Date), CAST(N'2020-12-09' AS Date), 3, CAST(N'2020-12-15' AS Date), CAST(N'2020-12-22' AS Date), 8, 2, 0, N'高雄三民區叉叉路999號', 3600, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-10' AS Date), 3400, N'高級', N'*上課穿著
1.合適的運動穿著、球鞋
2.以及簡單的籃球基礎及一顆籃球加上想讓自己更強的心態就可以囉！
3.請自備水壺及毛巾
', 4, 0, N'/Content/imgClass/edd913d2-848d-4eb0-b45d-481470c1d438.jpg', N'/Content/imgClass/985f8376-1a83-4504-8911-e6ccf414f568.jpg', N'/Content/imgClass/a237f323-1777-412b-b011-a415ff2c5e14.jpg', N'球類運動', N'籃球', N'團體課程', N'三民區', N'籃球', 12, CAST(N'2020-12-09T11:33:04.970' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (37, N'柔道防身班', N'終身受用的防身技術：柔道', N'課程為每週五晚上18:00-20:00，共12週 (遇國定假日即順延一週)
 
透過循序漸進的課程規劃，我們將培養您學習：
 
(一)   禮節與武德：柔道運動是非常講究「禮」字的競技運動，不論是訓練、比賽皆是「始於禮而止於禮」，且講究「尊重場地、尊重對手、尊重師長」。
 
(二)   提升體能、身體協調性：柔道不同於其他單項武術著重在單一部位上（例如跆拳道重腳、搏擊重手），柔道講究全身的協調性及各部位的爆發力，需要全面發展訓練，故非常適合從小開始訓練。
 
(三)   終身受用的防身技術：柔道是最適合用於防身的技術，其原因是它包含了站立時的摔技、被拉進地板後的寢技、適合力量較小者所使用的勒技、適合狹小空間的關節技，全方面發展的技術讓施術者可依對方體型與現場環境地形施展適合的柔道技術，保護自我。
', CAST(N'2020-12-22' AS Date), CAST(N'2020-12-08' AS Date), 16, CAST(N'2020-12-23' AS Date), CAST(N'2020-12-22' AS Date), 20, 8, 0, N'高雄市三民區十全一路100號', 3000, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-22' AS Date), 1200, N'初級', N'柔道衣請自行購買
', 5, 0, N'/Content/imgClass/2cd5e2ff-81a2-4c88-a8f1-2869ab5bdb1c.jpg', N'/Content/imgClass/cd0534a7-7e96-4515-8cda-376fc99542fa.jpg', N'/Content/imgClass/c3969747-0cff-4a14-a8f0-3e061a78ee87.jpg', N'格鬥競技', N'武術', N'團體課程', N'三民區', N'柔道', 5, CAST(N'2020-12-09T11:39:42.960' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (38, N'初級網球課程', N'針對每位學員規劃專屬網球課程，使用因材施教的應變教學，分享更多樣、有效的教學服務。', N'網球是項令人上癮的娛樂。在球場上積極奔跑、直線、斜線、正手、反手、上網截擊，所有的一切都能給你帶來無窮的樂趣！在專業教練的教導下，透過網球運動有氧及無氧綜合鍛鍊，可提高人的耐力、速度、力量、柔韌、靈敏等身體素質；通過滿場的奔跑、有力的擊球、大聲的吼叫或歡快的笑聲，可以宣洩或緩解您的壓力和緊張，並能給你身心帶來放鬆和愉悅。
', CAST(N'2020-12-02' AS Date), CAST(N'2020-12-30' AS Date), 8, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-30' AS Date), 6, 2, 0, N'高雄市橋頭竹林網球場', 3000, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 2500, N'初級', N'請穿著適合運動的服裝', 3, 0, N'/Content/imgClass/cde39fda-224b-4a70-aeb4-8a945dbc5b01.jpg', N'/Content/imgClass/12f7635a-d5d4-4bd3-be77-1cfe35dad74a.jpg', N'/Content/imgClass/09d0a524-cd43-447f-a48f-8be22d388e6c.jpg', N'球類運動', N'網球', N'團體課程', N'其它區', N'網球', 18, CAST(N'2020-12-09T13:46:33.153' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (39, N'棒球進階', N'適合原本會打棒球的學員，進階學習戰術合作及技巧', N'想學棒球找不到好課程?  千萬別錯過這次特別開設  棒球綜合訓練課程  一次滿足你對棒球的渴望', CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 24, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 12, 5, 0, N'高雄市鳥松區澄清湖棒球場', 17500, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 14800, N'中級', N'請穿著適當服裝', 4, 0, N'/Content/imgClass/59fa931a-991f-444f-9ac6-6cbf685ed347.jpg', N'/Content/imgClass/3d4cc657-3071-431b-8827-6a0fa272d7cb.jpg', N'/Content/imgClass/aabb9119-721c-421c-8609-2dc7018a97c9.jpg', N'其它類別', N'其它', N'團體課程', N'其它區', N'棒球', 18, CAST(N'2020-12-09T14:02:16.210' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (40, N'桌球指導', N'明星教練教您打桌球', N'一、學會初級桌球比賽必備的技術。二、了解（學會）桌球必備的一般認知及技術認知。三、用桌球運動刺激並發展和桌球有關的運動能力。四、認識與桌球有關的運動概念。五、在桌球運動中培養運動精神與運動道德。
', CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 8, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 8, 4, 0, N'高雄市苓雅區活動中心', 2600, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 3000, N'大師級', N'凡8歲以上均可報名參加
請穿著跑步用運動衣褲、跑步鞋。
請自備水壺、毛巾。
課程相關說明：
除颱風警報、打雷、超豪大雨等，足以影響學員安全情況下停課，一般正常雨勢天候狀況，本訓練課程仍將正常進行。
每堂課程都將進行動態熱身、主課表長跑訓練、靜態收操、教練分享等程序。
主課表內容將由課表開立教練，於當日課程現場宣佈講解。', 5, 0, N'/Content/imgClass/0a800a65-6d96-49d6-a8f1-e66a64ae534c.jpg', N'/Content/imgClass/06521c7e-f62a-42ea-8dbd-ae66b3267a2e.jpg', N'/Content/imgClass/6ad96215-eba5-4e87-b647-1f2a2097b218.jpg', N'球類運動', N'其它', N'團體課程', N'苓雅區', N'桌球', 10, CAST(N'2020-12-09T14:13:04.673' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (41, N'高爾夫入門訓練', N'羅開學院首創的學習模式，「實體教學，數位學習。」保障球友高爾夫課程學習成效，也提昇高爾夫訓練界思維，讓學習成為一種樂趣，沒有任何負擔。加入羅開學院，是你學習高爾夫技巧最佳的選擇。', N'想要輕輕鬆鬆與客戶、朋友，進行商務球敘或是休閒聯誼嗎？羅開高爾夫學院的高爾夫基礎入門課程，從室內理論課開始，循序漸進的動作練習與球場實地教學，讓你在輕鬆學習的環境下，學會全套揮桿技巧與高球禮儀，輕鬆與好友一起去球場揮揮桿。
', CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 8, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 1, 1, 0, N'高雄市鳥松區高球練習場', 10000, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 7500, N'初級', N'課程相關說明：
每周一、三、五上課，每次兩小時，「實體教學，數位學習。」羅開高爾夫學院創院經典訓練課程-基礎入門課程，是你高爾夫訓練課程的最佳決定。
', 4, 0, N'/Content/imgClass/70eaf515-6a94-48b7-84f9-96ead99d0995.jpg', N'/Content/imgClass/2e5809c0-86ad-4569-82a7-4f2105ee68e2.jpg', N'/Content/imgClass/cfcb34ba-7932-42ba-9921-41d0e3a7ab6a.jpg', N'球類運動', N'其它', N'一對一課程', N'其它區', N'高爾夫', 7, CAST(N'2020-12-09T14:32:11.243' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (42, N'幼兒足球', N'小班制教學，讓學童學習效果更好更快。', N'全世界瘋足球，每四年一次世界盃足球賽全球瘋足球，台灣也不例外樂在其中，教學中以深入淺出方式帶動趣味足球，並以遊戲輔助訓練基本體能，從規則中介紹足球，親自體會參予比賽對抗足球樂趣，有機會對外友誼賽增加運動視野及交流，讓足球也能踢動台灣。上課時間為禮拜六下午7:00 ~ 8:00
8:00 後為自由組隊團體練習時間
', CAST(N'2020-12-01' AS Date), CAST(N'2020-12-30' AS Date), 8, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 9, 3, 0, N'高雄市左營區博愛二路86號', 4500, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 4000, N'初級', N'課程相關說明：
除颱風警報、打雷、超豪大雨等，足以影響學員安全情況下停課，一般正常雨勢天候狀況，本訓練課程仍將正常進行。
每堂課程都將進行動態熱身、主課表訓練、靜態收操、教練分享等程序。
主課表內容將由課表開立教練，於當日課程現場宣佈講解。
', 5, 0, N'/Content/imgClass/438d8063-305e-47d1-93f7-730ded2e72f8.jpg', N'/Content/imgClass/5c6c45f7-a75d-433c-bff4-f2b079d01bc9.jpg', N'/Content/imgClass/57d340c8-e090-4c1f-a380-681d4e98ac39.jpg', N'球類運動', N'足球', N'團體課程', N'左營區', N'足球', 13, CAST(N'2020-12-09T14:41:25.063' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (43, N'魚式游泳', N'完全沉浸式游泳訓練法', N'介紹「完全沉浸」(Total Immersion, TI)游泳訓練法，打破一般人對傳統游泳訓練的既有印象，重拾學習游泳的樂趣。想學習蛙式、自由式卻不知該從何下手；已經到泳訓班受過訓練，也花費苦工不斷練習，為何泳技卻毫無進步？游泳高手們在水中悠游如魚，為何我們在水中卻游得礙手礙腳？游泳不難入門，但想游得輕鬆，游得漂亮卻非易事。
　　教練以長達40年的親身體驗，研究傑出泳者的體態動作，將豐富的觀察結果化為簡單易學的教材，並以這種訓練方式嘉惠無數學子，這種全新的游泳技巧，稱之為「完全沉浸」(Total Immersion, TI)訓練法。TI訓練法實施後，得到卓越的成果；不論泳者年紀多寡，都能很快掌握輕靈悠遊的訣竅，進而獲得學習游泳的樂趣。
上課時間為禮拜六下午5:00 ~ 6:00
', CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 8, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 4, 1, 0, N'高雄市三民區大地游泳池', 3000, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 2800, N'高級', N'當日泳池門票需自費。
請自備泳衣、泳褲、泳帽、蛙鏡、耳塞及換洗衣物。
有更衣室及盥洗室
請自備水壺。
請勿穿著沙灘褲

補課須知：
魚式游泳全系列團體班適用
因為現代人工作、事業、家庭 繁忙
難免會有請假的需求
所以我們在學生請假後，開放補課，請和教練聯繫，在同場地開的同一課程，可以來補課，但需依該泳池收費標準，另收門票費用哦!! 
', 3, 0, N'/Content/imgClass/c6afa7a0-38d7-4844-9b45-f880e0634996.jpg', N'/Content/imgClass/4879d5c9-5401-4a25-ab78-2d42e80d0377.jpg', N'/Content/imgClass/b76453d1-5579-4a64-903c-2c516aa8344e.jpg', N'水上運動', N'游泳', N'團體課程', N'三民區', N'游泳', 14, CAST(N'2020-12-09T14:49:21.073' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (44, N'長板衝浪', N'學習長板衝浪技巧', N'全台唯一合法衝浪區有救生員戒護，規劃，最安全的衝浪環境
有3位以上救生員，巡邏戒護，安全第一!
需認識海域''辨識海流''浪況''危險事項!
認識裝備器材
裝備使用''解釋淺在的風險和降低風險的技巧!
水域安全
緊急招回信號和緊急協助手勢!
海上定位!
安全須知
針對當天的狀況和危險加以解說!
衝浪教學
滑水、等浪、追浪、起乘、衝浪安全、衝浪禮儀!
', CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 6, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 6, 2, 0, N'墾丁白沙灣', 3400, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 3000, N'高級', N'凡18歲以上均可報名參加
請穿著泳衣泳褲。
有更衣室及盥洗室請自備換洗衣物。
請自備水壺、毛巾。
課程相關說明：
除颱風警報、打雷、超豪大雨等，足以影響學員安全情況下停課，一般正常雨勢天候狀況，本訓練課程仍將正常進行。
每堂課程都將進行動態熱身、主課表訓練、靜態收操、教練分享等程序。
主課表內容將由課表開立教練，於當日課程現場宣佈講解。
', 4, 0, N'/Content/imgClass/a7e791a1-a8ac-483d-b552-86f593855eb2.jpg', N'/Content/imgClass/2840a832-14d8-46ae-9b0a-008e9cc25799.jpg', N'/Content/imgClass/d4de68e7-add8-4eb3-858b-af4164650cec.jpg', N'水上運動', N'衝浪', N'團體課程', N'其它區', N'衝浪', 14, CAST(N'2020-12-09T14:53:05.017' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (45, N'風帆入門訓練', N'禮帽及碧高風帆是初級訓練所採用的標準艇種，透過這入門課程，你可掌握駕駛風帆的基本技巧、航行理論及安全守則，是進入風帆基本技巧訓練班的準備課程。', N'初次乘風帆航行感覺固然有趣，在浪花中遨遊或飛馳更是妙不可言。康樂及文化事務署轄下五個水上活動中心均設於理想及安全的水域，是舉辦多元化訓練課程的理想地點。所有訓練課程由合資格及經驗豐富的教練教授，不論你是希望一嘗揚帆出海的滋味，或是提升個人技術，又或希望成為出色的帆手，我們都可以為你一一安排。完成有關課程及考獲相關資歷後，你可租用中心內的各種風帆，例如禮帽、碧高、Wanderer、激光XD、420、Magno、激光2000、Dart16及RS500，出海暢遊，享受逐浪樂趣。
', CAST(N'2020-12-02' AS Date), CAST(N'2020-12-31' AS Date), 8, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 6, 3, 0, N'高雄市鹽埕區風帆訓練基地', 2000, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 1500, N'初級', N'適合年齡
8 歲或以上，能穿著衣服游泳最少 50 米/諳練泳術

課程相關說明：
除颱風警報、打雷、超豪大雨等，足以影響學員安全情況下停課，一般正常雨勢天候狀況，本訓練課程仍將正常進行。
每堂課程都將進行動態熱身、主課表訓練、靜態收操、教練分享等程序。
主課表內容將由課表開立教練，於當日課程現場宣佈講解。
', 4, 0, N'/Content/imgClass/2f30ccd7-3d9b-46ad-8887-d50357808e14.jpg', N'/Content/imgClass/ff402d7e-50fb-436a-8403-efae96f132c2.jpg', N'/Content/imgClass/1b4fbd69-dde0-4d37-b634-7d9a98d2337d.jpg', N'水上運動', N'其它', N'團體課程', N'鹽埕區', N'風帆', 14, CAST(N'2020-12-09T14:57:26.803' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (47, N'長跑技巧', N'擁有正確跑姿, 才能跑得又快又久', N'指導學員掌握良好跑姿、分拆細動作訓練跑長跑訓練的基礎知識，將要練的原理解釋按個人的能力給予目標時間。除課堂外，有任何問題亦會有專人站內訊息跟進。使用健身帶進行肌力訓練，一套訓練下肢主要肌肉的動作，幫助改善跑姿。斜坡交替訓練，教授如何安全利用斜坡訓練加強速度及肌力。提升速度訓練，按學員的能力派出練習課程。十公里比賽計劃
上課時間為禮拜三上午5:00 ~ 6:00
', CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 8, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 1, 1, 0, N'高雄苓雅區中正技擊館', 1500, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 1300, N'高級', N'凡16歲以上，對戶外跑步訓練運動有興趣之民眾，均可報名參加
當日入場門票需自費。
請穿著跑步用運動衣褲、跑步鞋。
有更衣室及盥洗室請自備換洗衣物。
請自備水壺、毛巾。
課程相關說明：
除颱風警報、打雷、超豪大雨等，足以影響學員安全情況下停課，一般正常雨勢天候狀況，本訓練課程仍將正常進行。
每堂課程都將進行動態熱身、主課表長跑訓練、靜態收操、教練分享等程序。
主課表內容將由課表開立教練，於當日課程現場宣佈講解。
', 4, 0, N'/Content/imgClass/172a3e5d-7a9a-4f13-b95d-f11def201940.jpg', N'/Content/imgClass/449c2e90-8d64-498a-b675-9907a24199e7.jpg', N'/Content/imgClass/9579d665-ccb9-4a97-9de6-b7e52dc5de00.jpg', N'其它類別', N'其它', N'一對一課程', N'苓雅區', N'長跑', 8, CAST(N'2020-12-09T15:31:50.460' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (48, N'長跑訓練', N'擁有正確跑姿, 才能跑得又快又久', N'指導學員掌握良好跑姿', CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 8, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 1, 1, 0, N'高雄市鳳山區中正技擊館', 2555, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 2500, N'初級', N'請穿著跑步用運動衣褲、跑步鞋', 3, 0, N'/Content/imgClass/3732fa3d-9f8f-433e-a9be-e3287b8c47ad.jpg', N'/Content/imgClass/ab14e279-7b00-45c2-89ef-4c0e7ccd0eef.jpg', N'/Content/imgClass/72cc63fc-a109-446d-85e8-3b68515ad663.jpg', N'其它類別', N'其它', N'一對一課程', N'其它區', N'長跑', 8, CAST(N'2020-12-09T15:49:17.750' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (49, N'新型態高效能-重訓團班', N'團體訓練發揮潛能與耐力，提升運動效果', N'想開始運動，又無法下定決心的朋友，每次進健身房都不清楚怎麼開始，安全友善的健身團班採用小班制，根據您的個人情況，教授基本的健身動作和知識，搭配營養師的諮詢建議，幫助您踏出健身的第一步，邁向健康美麗之路！ 2h這次用心規劃了全新的健身團課“每週一千多元即可擁有：精緻重訓團班3小時＋每日營養師一對一指導！” 還有超級好康的全勤獎鼓勵你完成目標，完整的師資陣容，精緻小班，健康運動最佳的選擇～早鳥優惠至06/30，超容易滿班～趕快來報名卡位唷！
', CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 24, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 1, 1, 0, N'高雄前金區圈圈路555號', 17500, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 14800, N'中級', N'1. 心血管疾病者，或有其他傷病(醫生不建議參與劇烈鍛煉者)，糖尿病患者以及未成年不建議參加。
2. 孕婦(包括順產產後6個月內，剖腹產1年內)及有身體健康問題者不建議參加。
3. 人數滿四人才開班，如開班人數不足會統一通知將予以退款。
4. 禁止空腹參加訓練，建議運動前半小時內不宜進食過量，以免身體不適。
5. 必須穿著運動服裝和乾淨運動鞋。
', 4, 0, N'/Content/imgClass/7c6a4074-57a3-4134-8baf-c6f0ff03e40d.jpg', N'/Content/imgClass/34911429-9b5a-4d33-8541-d2e8dd138e14.jpg', N'/Content/imgClass/fc3e1ec0-87a2-4738-8772-e6532d324916.jpg', N'室內健身', N'重訓', N'一對一課程', N'前金區', N'重訓', 9, CAST(N'2020-12-09T16:04:32.170' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (50, N'TRX懸掛式訓練', N'源自於美國海豹突擊隊訓練且風靡全球的運動器材', N'課程源自於美國海豹突擊隊訓練使用，現今已是風靡全球的運動器材之一，藉由自身體重以懸吊方式來進行訓練，因為繩索的不穩定性，可促使更多沈睡的肌群（如腹部）參與動作訓練，加強平衡感，柔軟度，核心，來雕塑曲線。課程內容會分別編排以下肢、上肢及核心三大部位訓練為主軸，搭配間歇跳躍動作提高心肺效率，是全身性肌力訓練課程。
', CAST(N'2020-12-10' AS Date), CAST(N'2020-12-30' AS Date), 16, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 6, 1, 0, N'高雄前金區圈圈路555號', 3750, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 3000, N'初級', N'上課請穿著運動服裝及乾淨運動鞋
', 4, 0, N'/Content/imgClass/37e15c92-d7fa-4b50-885c-e73fe8a5d0bb.jpg', N'/Content/imgClass/59965024-7bae-487c-9a45-fa668dd8cd81.jpg', N'/Content/imgClass/83295837-43b4-4688-94dd-682d58198df9.jpg', N'室內健身', N'TRX', N'團體課程', N'前金區', N'TRX', 9, CAST(N'2020-12-09T16:07:49.193' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (51, N'小民飛輪減脂套課', N'跟著老師這樣做 燃脂JO甘單', N'一周訓練總時數最少須達150分鐘
搭配個人喜好運動(如跑步、騎腳踏車等)來達到150分鐘的運動時數也可以搭配小件器材做無氧訓練，以強化核心及全身肌力、肌耐力。
減脂最重要除了運動外還是需要跟飲食作搭配,才能有效的達到減脂及減重的成效。(運動占減脂的30%，飲食則占了70%)

十堂課強度為階梯式，慢慢拉高運動強度
當課堂強度高於燃脂區間會用到最大攝氧量，達到後燃效應，即使運動結束還是會持續維持在最大耗氧量一直燃燒總熱量
', CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 10, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 10, 5, 0, N'高雄前金區圈圈路555號', 8000, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 7600, N'中級', N'*上課穿著
1.合適的運動穿著、球鞋
2.請自備水壺及毛巾
', 4, 0, N'/Content/imgClass/47cef64a-f374-4311-92c7-bbd2e8343b3d.jpg', N'/Content/imgClass/acb01bda-88d7-491d-8bf0-4108eb056271.jpg', N'/Content/imgClass/5456c8c5-694e-4d3e-a688-5eced9ed1a45.jpg', N'室內健身', N'其它', N'團體課程', N'前金區', N'飛輪', 9, CAST(N'2020-12-09T16:11:02.373' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (52, N'硬舉初級班', N'深蹲為完整動作角度訓練身體後側鏈肌群的發力，以建立力量與爆發力。', N'一對一私人教練課程，教練講解、示範正確動作外，更必須協助每位學員找出最佳臀、腿、背的個人感受度，每個人工作、生活形態大不相同，也造就出不同身體狀況，一個硬舉動作，對於不同學員，就『一定』會有不同指導方針。
', CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 8, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 1, 1, 0, N'高雄前金區圈圈路555號', 5000, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 4600, N'初級', N'。初學者或健身愛好者。你在訓練上遇到瓶頸，想要突破現狀。你想更進一步了解動作細節與正確性

', 4, 0, N'/Content/imgClass/a7652791-fae3-4aaf-865c-7e39cd5835df.jpg', N'/Content/imgClass/4662e922-75cd-403c-8494-f383b2970ca0.jpg', N'/Content/imgClass/dff1e0f6-7db1-4fdb-a3c7-63a046c501ff.jpg', N'室內健身', N'其它', N'一對一課程', N'前金區', N'硬舉', 9, CAST(N'2020-12-09T16:14:23.427' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (53, N'有氧拳擊', N'提升心肺功能', N'90 年代興起於美國，有氧拳擊以踢拳、英式拳和泰拳的動作為基礎，能消耗熱量和燃燒卡路里。剛好符合你練習有氧拳擊所預期達到的效果。    事實上，學習有氧拳擊並不是為了精進格鬥或對打技能，其目的在於透過練習拳擊動作，增強心肺功能與消耗熱量。 ', CAST(N'2020-12-01' AS Date), CAST(N'2020-12-30' AS Date), 24, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 12, 5, 0, N'高雄鼓山區圈圈路555號', 17500, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 14800, N'中級', N'請自備拳套', 4, 0, N'/Content/imgClass/0f79159b-a190-46bc-bb8e-74ee2788bda6.jpg', N'/Content/imgClass/904c1ff0-4598-480b-aab7-930138b12081.jpg', N'/Content/imgClass/27433e42-6c94-4112-a7db-3f5364cdd263.jpg', N'格鬥競技', N'拳擊', N'團體課程', N'鼓山區', N'拳擊', 11, CAST(N'2020-12-09T16:23:48.543' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (54, N'競賽飛盤', N'基本規則、動作介紹、團體練習', N'課程介紹、運動安全須知飛盤的發展史與基本介紹飛盤基本持盤與投擲飛盤直飛平盤投擲教學飛盤外彎盤投擲教學飛盤內彎盤投擲教學飛盤顛倒盤投擲教學綜合擲盤練習
上課時間為禮拜六下午7:00 ~ 8:00
8:00 後為自由組隊團體練習時間
', CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 8, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 25, 12, 0, N'高雄市苓雅區中正技擊館', 2400, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 2200, N'初級', N'凡16歲以上，對戶外競技飛盤訓練運動有興趣之民眾，均可報名參加
當日入場門票需自費。
請穿著跑步用運動衣褲、跑步鞋。
有更衣室及盥洗室請自備換洗衣物。
請自備水壺、毛巾。
課程相關說明：
除颱風警報、打雷、超豪大雨等，足以影響學員安全情況下停課，一般正常雨勢天候狀況，本訓練課程仍將正常進行。
每堂課程都將進行動態熱身、主課表訓練、靜態收操、教練分享等程序。
主課表內容將由課表開立教練，於當日課程現場宣佈講解。
', 5, 0, N'/Content/imgClass/c543224f-0df7-4301-96aa-a9f46f7edf45.jpg', N'/Content/imgClass/a4aacca6-78bf-4905-878e-88ffbbbfa41e.jpg', N'/Content/imgClass/ae0a4623-3b56-4699-8799-90f3e6f3a56f.jpg', N'其它類別', N'其它', N'團體課程', N'苓雅區', N'飛盤', 15, CAST(N'2020-12-09T21:32:07.157' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (55, N'花式飛盤', N'認識飛盤花式的器材應用、變化、玩法與創意發展；花式飛盤為有如韻律體操的另一手應用。', N'懸盤如有偏離中心時，應以雙眼釘住盤面中心點，而以手指修正，進行手眼協調。依各種時機的旋盤運作可分利手旋、非利手旋、翻手旋、肘旋、趾旋、正旋、反旋、外邊旋、內邊旋、齒旋、頭旋等，均屬Ｄ的動作。旋盤須較長時間練習，在教學時，以旋盤製練習盤，加以練習為佳，以免難以旋盤之苦，亦可免除初學花式的挫折感，藉以提高興趣，但如想使旋盤更上一層樓，應多下功夫於旋盤自我練習了！', CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 20, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 10, 2, 0, N'高雄市苓雅區中正技擊館', 3000, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 2700, N'高級', N'凡16歲以上，對戶外競技飛盤訓練運動有興趣之民眾，均可報名參加
當日入場門票需自費。
請穿著跑步用運動衣褲、跑步鞋。
有更衣室及盥洗室請自備換洗衣物。
請自備水壺、毛巾。
', 4, 0, N'/Content/imgClass/fb037f05-7b18-4e1c-9478-0d86c3453e20.jpg', N'/Content/imgClass/d63e0fd2-efab-442d-bd7e-d0a045770ab3.jpg', N'/Content/imgClass/569c13e0-12bc-4c46-80be-183827035844.jpg', N'其它類別', N'其它', N'團體課程', N'苓雅區', N'飛盤', 15, CAST(N'2020-12-09T21:42:19.010' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (56, N'裸弓教學課程', N'為提升社會大眾的體能及體育知識，以現下國際賽常見現代反曲弓項目中的裸弓（barebow）為指導方向，教導學員在簡單的器材前提下學習射箭，享受命中黃心的樂趣與成就感。', N'Archer Space 擁有數個具備正式證照的教練們，分別指導自己最拿手的弓種項目，不論是簡單好上手的裸弓項目、滿是機械零件的複合弓項目、及奧運現行的競技反曲弓項目等等。各式各樣的課程，只要你想學，就不怕沒人教。', CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 8, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 1, 1, 0, N'高雄市三民區射箭場', 4000, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 2700, N'高級', N'凡對射箭有興趣的民眾皆能報名，無射箭經驗可。無須自備器材，若自備亦可。

課程相關說明：
1、彈性時段（平日晚上或假日晚上也可）2、採預約制，上課時間可與教練約定3、該課程依照個人學習進度量身打造，而非按表操課，可重複登記修習此課程。
', 4, 0, N'/Content/imgClass/d24b4a99-1a7e-417a-9bd6-c52eea077a6b.jpg', N'/Content/imgClass/28b925ea-f484-4b67-86d6-004a0fc1c606.jpg', N'/Content/imgClass/3b55a774-3917-43a0-b2df-bd3d9d2f3f80.jpg', N'其它類別', N'其它', N'一對一課程', N'三民區', N'射箭', 16, CAST(N'2020-12-09T22:01:13.260' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (57, N'成人現代舞', N'透過肢體更認識自己', N'藝術一直都存在於每個人的身體裡，只是需要被適時的開發及引導出來！為了將芭蕾舞／現代舞的美與創意的種子播入群眾生活裡，本舞團集合專業師資，加入歷年來專業演出和社區學校之藝文推廣經驗，從成人舞蹈課程到兒童青少年專修班，以及專業班培訓課程，推出一系列舞蹈工作坊，希望能讓大家在專業的舞蹈創作者及表演者的帶領下進入此一藝術的真善美世界裡，體驗舞蹈藝術如何與生活連結，並開發各自的想像力來創造出屬於自己的生活美學與藝術欣賞觀，進而發展出潛藏在身體裡的藝術細胞，讓自我人生因此充滿藝術創造性，讓生命變得更優質良好。
', CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 12, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 10, 5, 0, N'高雄市新興區雅室舞蹈教室', 4000, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 3440, N'中級', N'無任何舞蹈基礎可
上課著輕便服裝即可
', 4, 0, N'/Content/imgClass/6aa83020-f760-418c-8b2a-3d43b0d6e70d.jpg', N'/Content/imgClass/542d928a-b836-4802-aaec-0bcfd9963134.jpg', N'/Content/imgClass/c683cd6e-47bb-46b1-b64d-05e37063018c.jpg', N'其它類別', N'其它', N'團體課程', N'新興區', N'現代舞', 21, CAST(N'2020-12-09T22:18:01.903' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (58, N'長板基礎教學課程', N'不敢站在滑板上的你是因為你還沒上過 LONGBOARD DAY - 長板基礎教學課程，三小時的課程讓你馬上在長板上自由自在的滑行。', N'想要學習長板卻又不知從何開始？清樹聽到這些心聲囉！非常歡迎從來沒有接觸過長板但想要嘗試在長板上滑行的朋友們！一切從零開始學習，沒有壓力、沒有負擔，用愉快輕鬆的心情一起快樂學習長板', CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 3, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 1, 1, 0, N'高雄苓雅區極限運動場', 800, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 600, N'初級', N'請穿著適合運動服飾及滑板鞋', 5, 0, N'/Content/imgClass/6fb71c6e-6352-4133-b4c5-4455b92dcf97.jpg', N'/Content/imgClass/bfdb22ca-3648-4c37-b6d2-bc52c1e72a82.jpg', N'/Content/imgClass/8a3f7e43-a2ed-4b51-be79-d5fdddf6dbe2.jpg', N'其它類別', N'其它', N'一對一課程', N'苓雅區', N'滑板', 19, CAST(N'2020-12-09T22:35:51.990' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (59, N'花式直排輪', N'熟練基礎溜冰招式：前溜葫蘆、平行轉彎、ｓ形、左右單腳練好（尤其是單腳）然後練後溜葫蘆後溜推刃ｔ煞', N'10小時的體驗時間包含了動態熱身、認識花式溜冰鞋、溜冰動作練習及緩和收操，經驗豐富的教練們按照學員不同年齡、不同體能基礎做最適合的調整，由簡單踏步到滑行的變化中，鍛鍊身體協調、平衡及肌肉控制能力，體驗課程也銜接至花式初級班的教學內容中，從初學、休閒，到參加檢定、比賽，皆有完整的訓練系統及規劃，使小朋友在循序漸進的教學過程中，體驗挫折、學習跌倒再站起來的勇氣，引發對花式溜冰的興趣及成就感，展現力與美的自信！', CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 10, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 6, 2, 0, N'高雄市前鎮區草衙道直排輪場', 2000, CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 1800, N'中級', N'1. 請著適合運動的服裝、鞋子，建議穿高過腳踝的襪子
2. 費用包含租借溜冰鞋和護膝，帶著水壺、毛巾來報到就可以了', 4, 0, N'/Content/imgClass/88ba3edc-18de-44f6-9299-808e3a4a7521.jpg', N'/Content/imgClass/a69e7537-68bb-4fdb-8b1b-291ae294c008.jpg', N'/Content/imgClass/4a63d090-6f58-41ab-aeee-d80340c64490.jpg', N'其它類別', N'其它', N'團體課程', N'其它區', N'直排輪', 20, CAST(N'2020-12-09T23:02:56.153' AS DateTime))
INSERT [dbo].[tClass] ([classID], [c_name], [c_nameText], [c_intro], [c_startTime], [c_endTime], [c_hourRate], [c_registerStart], [c_registerEnd], [c_maxStudent], [c_minStudent], [c_student], [c_location], [c_price], [c_onsaleStart], [c_onsaleEnd], [c_discount], [c_level], [c_requirement], [c_rate], [c_rateTotal], [c_imgPath1], [c_imgPath2], [c_imgPath3], [c_lable1], [c_lable2], [c_lable3], [c_lable4], [c_lable5], [teacherID], [c_getNow]) VALUES (60, N'瑜珈入門', N'123', N'1234567', CAST(N'2020-12-19' AS Date), CAST(N'2020-12-31' AS Date), 5, CAST(N'2020-12-21' AS Date), CAST(N'2020-12-31' AS Date), 6, 2, 0, N'高雄前金區圈圈路999號', 10, CAST(N'2020-12-10' AS Date), CAST(N'2020-12-30' AS Date), 5, N'初級', N'123', 0, 0, N'/Content/imgClass/7be17a67-3ee3-4588-9457-4fc2f712c74d.jpg', N'/Content/imgClass/defaultClass.png', N'/Content/imgClass/defaultClass.png', N'其它類別', N'其它', N'一對一課程', N'前金區', N'其它', 3, CAST(N'2020-12-10T11:44:30.557' AS DateTime))
SET IDENTITY_INSERT [dbo].[tClass] OFF
GO
SET IDENTITY_INSERT [dbo].[tDeposit] ON 

INSERT [dbo].[tDeposit] ([depositID], [d_point], [d_method], [d_memo], [d_getNow], [memberID]) VALUES (1, 10000, N'信用卡', NULL, CAST(N'2020-12-09T23:51:08.217' AS DateTime), 2)
INSERT [dbo].[tDeposit] ([depositID], [d_point], [d_method], [d_memo], [d_getNow], [memberID]) VALUES (2, -10000, N'點數扣款', N'親子瑜珈/', CAST(N'2020-12-09T23:51:12.987' AS DateTime), 2)
INSERT [dbo].[tDeposit] ([depositID], [d_point], [d_method], [d_memo], [d_getNow], [memberID]) VALUES (3, 50000, N'信用卡', NULL, CAST(N'2020-12-10T00:19:20.633' AS DateTime), 2)
INSERT [dbo].[tDeposit] ([depositID], [d_point], [d_method], [d_memo], [d_getNow], [memberID]) VALUES (4, -3000, N'點數扣款', N'羽球基本動作/', CAST(N'2020-12-10T00:19:23.300' AS DateTime), 2)
INSERT [dbo].[tDeposit] ([depositID], [d_point], [d_method], [d_memo], [d_getNow], [memberID]) VALUES (5, -10600, N'點數扣款', N'柔道防身班/桌球指導/硬舉初級班/', CAST(N'2020-12-10T00:24:38.747' AS DateTime), 2)
INSERT [dbo].[tDeposit] ([depositID], [d_point], [d_method], [d_memo], [d_getNow], [memberID]) VALUES (6, 40000, N'信用卡', NULL, CAST(N'2020-12-10T00:44:28.457' AS DateTime), 47)
INSERT [dbo].[tDeposit] ([depositID], [d_point], [d_method], [d_memo], [d_getNow], [memberID]) VALUES (7, -34455, N'點數扣款', N'熱瑜珈/長跑訓練/長板衝浪/新型態高效能-重訓團班/花式飛盤/', CAST(N'2020-12-10T00:44:32.320' AS DateTime), 47)
INSERT [dbo].[tDeposit] ([depositID], [d_point], [d_method], [d_memo], [d_getNow], [memberID]) VALUES (8, 20000, N'信用卡', NULL, CAST(N'2020-12-10T00:52:09.930' AS DateTime), 4)
INSERT [dbo].[tDeposit] ([depositID], [d_point], [d_method], [d_memo], [d_getNow], [memberID]) VALUES (9, -19700, N'點數扣款', N'修復瑜珈/幼兒足球/競賽飛盤/長板基礎教學課程/', CAST(N'2020-12-10T00:52:12.517' AS DateTime), 4)
INSERT [dbo].[tDeposit] ([depositID], [d_point], [d_method], [d_memo], [d_getNow], [memberID]) VALUES (10, 60000, N'信用卡', NULL, CAST(N'2020-12-10T09:56:11.403' AS DateTime), 7)
INSERT [dbo].[tDeposit] ([depositID], [d_point], [d_method], [d_memo], [d_getNow], [memberID]) VALUES (11, -52000, N'點數扣款', N'棒球進階/有氧拳擊/修復瑜珈/空中瑜伽/', CAST(N'2020-12-10T09:56:15.587' AS DateTime), 7)
INSERT [dbo].[tDeposit] ([depositID], [d_point], [d_method], [d_memo], [d_getNow], [memberID]) VALUES (12, -2400, N'點數扣款', N'競賽飛盤/', CAST(N'2020-12-10T10:04:37.260' AS DateTime), 7)
INSERT [dbo].[tDeposit] ([depositID], [d_point], [d_method], [d_memo], [d_getNow], [memberID]) VALUES (13, 20000, N'信用卡', NULL, CAST(N'2020-12-10T11:13:45.083' AS DateTime), 48)
INSERT [dbo].[tDeposit] ([depositID], [d_point], [d_method], [d_memo], [d_getNow], [memberID]) VALUES (14, -16500, N'點數扣款', N'熱瑜珈/裸弓教學課程/幼兒足球/', CAST(N'2020-12-10T11:13:46.783' AS DateTime), 48)
SET IDENTITY_INSERT [dbo].[tDeposit] OFF
GO
SET IDENTITY_INSERT [dbo].[tMember] ON 

INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (2, N'明子', N'籐', CAST(N'1990-09-10' AS Date), N'男性', N'm1@gmail.com', N'1234', N'0911567890', N'高雄市旗津區', N'中華一路5號', 1, N'游泳', N'/Content/imgMember/b5e9eed9-a1b2-474f-8e3f-92baab6d4e34.jpeg', 36400, CAST(N'2020-11-01T00:00:00.000' AS DateTime), N'No             ')
INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (3, N'小霞', N'馮', CAST(N'1990-02-13' AS Date), N'女性', N't1@gmail.com', N'1234', N'0921456897', N'高雄市三民區', N'三民大道22號', 2, N'瑜珈', N'/Content/imgMember/0a5b9507-e113-4a1d-8831-7bc01fa63b3e.jpeg', 700, CAST(N'2020-11-01T00:00:00.000' AS DateTime), N'No             ')
INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (4, N'黑子', N'籐', CAST(N'1980-12-02' AS Date), N'男性', N't2@gmail.com', N'1234', N'0911567890', N'高雄市旗津區', N'中華一路5號', 2, N'柔道', N'/Content/imgMember/7e1d6997-28ef-42f7-8ede-b99da2eb6bf4.jpeg', 300, CAST(N'2020-11-01T00:00:00.000' AS DateTime), N'No             ')
INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (5, N'小華', N'陳', CAST(N'1998-06-11' AS Date), N'男性', N'a2@gmail.com', N'1234', N'0932008088', N'高雄市前金區', N'中正路', 3, N'聽音樂', N'/Content/imgMember/defaultMember.png', 0, CAST(N'2020-11-24T00:00:00.000' AS DateTime), N'No             ')
INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (7, N'小白', N'郭', CAST(N'1998-06-12' AS Date), N'女性', N't3@gmail.com', N'1234', N'0932007077', N'高雄市三民區', N'鼎力路', 2, N'羽球', N'/Content/imgMember/f863c708-3a3d-4449-86c5-1fd999f903a3.jpeg', 5600, CAST(N'2020-11-24T00:00:00.000' AS DateTime), N'No             ')
INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (8, N'小恩', N'邱', CAST(N'1998-06-13' AS Date), N'男性', N't4@gmail.com', N'1234', N'0932006066', N'高雄市左營區', N'左營大路', 2, N'高爾夫', N'/Content/imgMember/66cb0aef-82be-45db-a886-4f2f86964b64.jpeg', 0, CAST(N'2020-11-24T00:00:00.000' AS DateTime), N'No             ')
INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (9, N'小智', N'林', CAST(N'1998-06-14' AS Date), N'男性', N't5@gmail.com', N'1234', N'0932005055', N'高雄市苓雅區', N'大順一路', 2, N'長跑、拳擊', N'/Content/imgMember/d5372ba0-d617-4f32-a649-0ac1944b35ce.jpeg', 0, CAST(N'2020-11-24T00:00:00.000' AS DateTime), N'No             ')
INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (10, N'小民', N'洪', CAST(N'1998-06-15' AS Date), N'男性', N't6@gmail.com', N'1234', N'0932004044', N'高雄市鹽埕區', N'五福路', 2, N'重訓、TRX', N'/Content/imgMember/aededc6e-5270-4a35-8931-c60838ba2848.jpeg', 0, CAST(N'2020-11-24T00:00:00.000' AS DateTime), N'No             ')
INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (28, N'小傑', N'江', CAST(N'1998-06-16' AS Date), N'男性', N't7@gmail.com', N'1234', N'0987563248', N'高雄市鼓山區', N'美術東路', 2, N'桌球', N'/Content/imgMember/7eee97c1-d269-4662-99b7-51f67fd1a507.jpeg', 0, CAST(N'2020-11-25T00:00:00.000' AS DateTime), N'No             ')
INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (29, N'小玟', N'陳', CAST(N'1998-06-17' AS Date), N'女性', N't8@gmail.com', N'1234', N'0932003033', N'高雄市左營區', N'華夏路', 2, N'跳高、拳擊', N'/Content/imgMember/f8fe1996-554c-42cd-a619-89545900e214.jpeg', 0, CAST(N'2020-11-25T00:00:00.000' AS DateTime), N'No             ')
INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (30, N'小硯', N'梁', CAST(N'1998-06-18' AS Date), N'男性', N't9@gmail.com', N'1234', N'0932002022', N'高雄市楠梓區', N'海專路', 2, N'籃球', N'/Content/imgMember/f6bd130e-4d1b-49bf-a7b4-307e6291254b.jpeg', 0, CAST(N'2020-11-25T00:00:00.000' AS DateTime), N'No             ')
INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (31, N'小雅', N'黃', CAST(N'1998-05-01' AS Date), N'女性', N't10@gmail.com', N'1234', N'0932001011', N'高雄市楠梓區', N'學專路', 2, N'足球', N'/Content/imgMember/03e8b733-b826-4601-b825-ae51bd219707.jpeg', 0, CAST(N'2020-11-25T00:00:00.000' AS DateTime), N'No             ')
INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (32, N'小泓', N'王', CAST(N'1998-05-02' AS Date), N'男性', N't11@gmail.com', N'1234', N'0932000000', N'高雄市楠梓區', N'加昌路', 2, N'游泳', N'/Content/imgMember/dc3a6ab0-df3a-4e7f-928b-3eff1973e900.jpeg', 0, CAST(N'2020-11-25T00:00:00.000' AS DateTime), N'No             ')
INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (33, N'小明', N'王', CAST(N'1998-05-03' AS Date), N'男性', N't12@gmail.com', N'1234', N'0922222222', N'高雄市楠梓區', N'後專路', 2, N'飛盤、踢毽子', N'/Content/imgMember/b9392ecb-1da5-48c5-bf80-32b4bba03385.jpeg', 0, CAST(N'2020-11-25T00:00:00.000' AS DateTime), N'No             ')
INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (34, N'小甫', N'蔣', CAST(N'1998-05-04' AS Date), N'男性', N't13@gmail.com', N'1234', N'0911111111', N'高雄市楠梓區', N'高楠大路', 2, N'射箭', N'/Content/imgMember/4f1b5fcd-e594-49fa-866f-e3b0e1a04ac2.jpeg', 0, CAST(N'2020-11-25T00:00:00.000' AS DateTime), N'No             ')
INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (35, N'小豪', N'黃', CAST(N'1998-05-05' AS Date), N'男性', N't14@gmail.com', N'1234', N'0900000000', N'高雄市楠梓區', N'高楠大路', 2, N'網球', N'/Content/imgMember/ec6efe31-b60b-4c02-999a-cd5ec64259c1.jpeg', 0, CAST(N'2020-11-25T00:00:00.000' AS DateTime), N'No             ')
INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (42, N'清樹', N'賴', CAST(N'1990-11-29' AS Date), N'男性', N't15@gmail.com', N'1234', N'0956287654', N'高雄市楠梓區', N'高楠大路', 2, N'滑板、長板、交通板', N'/Content/imgMember/37e3263e-2292-4143-977a-8868cb4bfb3d.jpeg', 0, CAST(N'2020-11-25T00:00:00.000' AS DateTime), N'No             ')
INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (43, N'台喜', N'可', CAST(N'2001-03-05' AS Date), N'女性', N't16@gmail.com', N'1234', N'0964587256', N'高雄市楠梓區', N'高楠大路', 2, N'直排輪', N'/Content/imgMember/c2d88bc9-f67e-4dfa-aedb-c27a59dc5ab6.jpeg', 0, CAST(N'2020-11-25T00:00:00.000' AS DateTime), N'No             ')
INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (44, N'地徒', N'呂', CAST(N'1990-07-12' AS Date), N'男性', N'm2@gmail.com', N'1234', N'0953562785', N'高雄市楠梓區', N'高楠大路', 1, N'運動、散步、爬山', N'/Content/imgMember/defaultMember.png', 100, CAST(N'2020-11-25T00:00:00.000' AS DateTime), N'No             ')
INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (45, N'Admin', N'JJM', CAST(N'2020-08-17' AS Date), N'男性', N'a1@gmail.com', N'1234', N'0912345678', N'高雄市前金區', N'中正一路221號', 3, N'溜溜球', N'/Content/imgMember/defaultMember.png', 100000, CAST(N'2020-11-29T00:00:00.000' AS DateTime), N'Yes            ')
INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (46, N'小雅', N'張', CAST(N'1998-06-20' AS Date), N'女性', N't17@gmail.com', N'1234', N'0935246986', N'高雄市鼓山區', N'中華路1010號', 2, N'現代舞', N'/Content/imgMember/2ec90b16-65f9-46d0-a485-2e4be9142964.jpeg', 0, CAST(N'2020-11-29T00:00:00.000' AS DateTime), N'No             ')
INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (47, N'小萱', N'魏', CAST(N'1990-07-21' AS Date), N'女性', N'm3@gmail.com', N'1234', N'0965324897', NULL, NULL, 1, NULL, N'/Content/imgMember/defaultMember.png', 5545, CAST(N'2020-12-10T00:41:00.050' AS DateTime), NULL)
INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (48, N'小盡', N'畢', CAST(N'1993-11-01' AS Date), N'男性', N'm4@gmail.com', N'1234', N'0945678321', NULL, NULL, 1, NULL, N'/Content/imgMember/e7963051-062b-4156-a72f-93289712a355.jpeg', 3500, CAST(N'2020-12-10T11:12:51.477' AS DateTime), NULL)
INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (49, N'小仲', N'盧', CAST(N'1990-07-19' AS Date), N'男性', N'm5@gmail.com', N'1234', N'0954687985', NULL, NULL, 1, NULL, N'/Content/imgMember/77f994a3-58c8-4dd2-96c6-e31ad41ce3a4.jpeg', NULL, CAST(N'2020-12-10T11:37:06.893' AS DateTime), NULL)
INSERT [dbo].[tMember] ([memberID], [m_firstName], [m_lastName], [m_birth], [m_gender], [m_email], [m_password], [m_phone], [m_district], [m_address], [m_role], [m_hobby], [m_imgPath], [m_Jpoint], [m_getNow], [m_emailConfirm]) VALUES (50, N'est', N't', CAST(N'1990-07-01' AS Date), N'男性', N'm6@gmail.com', N'1234', N'099999999', NULL, NULL, 1, NULL, N'/Content/imgMember/defaultMember.png', NULL, CAST(N'2020-12-10T12:03:56.307' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[tMember] OFF
GO
SET IDENTITY_INSERT [dbo].[tOrder] ON 

INSERT [dbo].[tOrder] ([orderID], [memberID], [o_orderdate]) VALUES (1, 2, CAST(N'2020-10-09T23:51:12.907' AS DateTime))
INSERT [dbo].[tOrder] ([orderID], [memberID], [o_orderdate]) VALUES (2, 2, CAST(N'2020-10-10T00:19:23.283' AS DateTime))
INSERT [dbo].[tOrder] ([orderID], [memberID], [o_orderdate]) VALUES (3, 2, CAST(N'2020-11-30T00:24:38.703' AS DateTime))
INSERT [dbo].[tOrder] ([orderID], [memberID], [o_orderdate]) VALUES (4, 47, CAST(N'2020-11-10T00:44:32.200' AS DateTime))
INSERT [dbo].[tOrder] ([orderID], [memberID], [o_orderdate]) VALUES (5, 4, CAST(N'2020-12-10T00:52:12.447' AS DateTime))
INSERT [dbo].[tOrder] ([orderID], [memberID], [o_orderdate]) VALUES (6, 7, CAST(N'2020-11-30T09:56:15.543' AS DateTime))
INSERT [dbo].[tOrder] ([orderID], [memberID], [o_orderdate]) VALUES (7, 7, CAST(N'2020-12-10T10:04:37.250' AS DateTime))
INSERT [dbo].[tOrder] ([orderID], [memberID], [o_orderdate]) VALUES (8, 48, CAST(N'2020-12-10T11:13:46.757' AS DateTime))
SET IDENTITY_INSERT [dbo].[tOrder] OFF
GO
SET IDENTITY_INSERT [dbo].[tOrder_Detail] ON 

INSERT [dbo].[tOrder_Detail] ([od_itemID], [orderID], [classID], [c_name], [c_price], [c_discount], [od_profit]) VALUES (1, 1, 33, N'親子瑜珈', 10000, 9000, 100)
INSERT [dbo].[tOrder_Detail] ([od_itemID], [orderID], [classID], [c_name], [c_price], [c_discount], [od_profit]) VALUES (2, 2, 35, N'羽球基本動作', 3000, 2800, 30)
INSERT [dbo].[tOrder_Detail] ([od_itemID], [orderID], [classID], [c_name], [c_price], [c_discount], [od_profit]) VALUES (3, 3, 37, N'柔道防身班', 3000, 1200, 30)
INSERT [dbo].[tOrder_Detail] ([od_itemID], [orderID], [classID], [c_name], [c_price], [c_discount], [od_profit]) VALUES (4, 3, 40, N'桌球指導', 2600, 3000, 26)
INSERT [dbo].[tOrder_Detail] ([od_itemID], [orderID], [classID], [c_name], [c_price], [c_discount], [od_profit]) VALUES (5, 3, 52, N'硬舉初級班', 5000, 4600, 50)
INSERT [dbo].[tOrder_Detail] ([od_itemID], [orderID], [classID], [c_name], [c_price], [c_discount], [od_profit]) VALUES (6, 4, 29, N'熱瑜珈', 8000, 7500, 80)
INSERT [dbo].[tOrder_Detail] ([od_itemID], [orderID], [classID], [c_name], [c_price], [c_discount], [od_profit]) VALUES (7, 4, 48, N'長跑訓練', 2555, 2500, 26)
INSERT [dbo].[tOrder_Detail] ([od_itemID], [orderID], [classID], [c_name], [c_price], [c_discount], [od_profit]) VALUES (8, 4, 44, N'長板衝浪', 3400, 3000, 34)
INSERT [dbo].[tOrder_Detail] ([od_itemID], [orderID], [classID], [c_name], [c_price], [c_discount], [od_profit]) VALUES (9, 4, 49, N'新型態高效能-重訓團班', 17500, 14800, 175)
INSERT [dbo].[tOrder_Detail] ([od_itemID], [orderID], [classID], [c_name], [c_price], [c_discount], [od_profit]) VALUES (10, 4, 55, N'花式飛盤', 3000, 2700, 30)
INSERT [dbo].[tOrder_Detail] ([od_itemID], [orderID], [classID], [c_name], [c_price], [c_discount], [od_profit]) VALUES (11, 5, 30, N'修復瑜珈', 12000, 11000, 120)
INSERT [dbo].[tOrder_Detail] ([od_itemID], [orderID], [classID], [c_name], [c_price], [c_discount], [od_profit]) VALUES (12, 5, 42, N'幼兒足球', 4500, 4000, 45)
INSERT [dbo].[tOrder_Detail] ([od_itemID], [orderID], [classID], [c_name], [c_price], [c_discount], [od_profit]) VALUES (13, 5, 54, N'競賽飛盤', 2400, 2200, 24)
INSERT [dbo].[tOrder_Detail] ([od_itemID], [orderID], [classID], [c_name], [c_price], [c_discount], [od_profit]) VALUES (14, 5, 58, N'長板基礎教學課程', 800, 600, 8)
INSERT [dbo].[tOrder_Detail] ([od_itemID], [orderID], [classID], [c_name], [c_price], [c_discount], [od_profit]) VALUES (15, 6, 39, N'棒球進階', 17500, 14800, 175)
INSERT [dbo].[tOrder_Detail] ([od_itemID], [orderID], [classID], [c_name], [c_price], [c_discount], [od_profit]) VALUES (16, 6, 53, N'有氧拳擊', 17500, 14800, 175)
INSERT [dbo].[tOrder_Detail] ([od_itemID], [orderID], [classID], [c_name], [c_price], [c_discount], [od_profit]) VALUES (17, 6, 30, N'修復瑜珈', 12000, 11000, 120)
INSERT [dbo].[tOrder_Detail] ([od_itemID], [orderID], [classID], [c_name], [c_price], [c_discount], [od_profit]) VALUES (18, 6, 31, N'空中瑜伽', 5000, 4500, 50)
INSERT [dbo].[tOrder_Detail] ([od_itemID], [orderID], [classID], [c_name], [c_price], [c_discount], [od_profit]) VALUES (19, 7, 54, N'競賽飛盤', 2400, 2200, 24)
INSERT [dbo].[tOrder_Detail] ([od_itemID], [orderID], [classID], [c_name], [c_price], [c_discount], [od_profit]) VALUES (20, 8, 29, N'熱瑜珈', 8000, 7500, 80)
INSERT [dbo].[tOrder_Detail] ([od_itemID], [orderID], [classID], [c_name], [c_price], [c_discount], [od_profit]) VALUES (21, 8, 56, N'裸弓教學課程', 4000, 2700, 40)
INSERT [dbo].[tOrder_Detail] ([od_itemID], [orderID], [classID], [c_name], [c_price], [c_discount], [od_profit]) VALUES (22, 8, 42, N'幼兒足球', 4500, 4000, 45)
SET IDENTITY_INSERT [dbo].[tOrder_Detail] OFF
GO
SET IDENTITY_INSERT [dbo].[tPay] ON 

INSERT [dbo].[tPay] ([payID], [p_money], [p_getNow], [p_status], [p_method], [p_getMoneyTime], [p_memo], [teacherID]) VALUES (1, 300, CAST(N'2020-12-10T13:28:34.440' AS DateTime), N'未匯款', N'教練請款', CAST(N'1990-01-01T00:00:00.000' AS DateTime), N'', 3)
SET IDENTITY_INSERT [dbo].[tPay] OFF
GO
SET IDENTITY_INSERT [dbo].[tRating] ON 

INSERT [dbo].[tRating] ([ratingID], [memberID], [classID], [r_send], [r_sendText], [r_star], [r_sendTime]) VALUES (2, 2, 33, N'籐明子', NULL, NULL, NULL)
INSERT [dbo].[tRating] ([ratingID], [memberID], [classID], [r_send], [r_sendText], [r_star], [r_sendTime]) VALUES (3, 2, 35, N'籐明子', NULL, NULL, NULL)
INSERT [dbo].[tRating] ([ratingID], [memberID], [classID], [r_send], [r_sendText], [r_star], [r_sendTime]) VALUES (4, 2, 37, N'籐明子', NULL, NULL, NULL)
INSERT [dbo].[tRating] ([ratingID], [memberID], [classID], [r_send], [r_sendText], [r_star], [r_sendTime]) VALUES (5, 2, 40, N'籐明子', NULL, NULL, NULL)
INSERT [dbo].[tRating] ([ratingID], [memberID], [classID], [r_send], [r_sendText], [r_star], [r_sendTime]) VALUES (6, 2, 52, N'籐明子', NULL, NULL, NULL)
INSERT [dbo].[tRating] ([ratingID], [memberID], [classID], [r_send], [r_sendText], [r_star], [r_sendTime]) VALUES (7, 47, 29, N'魏小萱', NULL, NULL, NULL)
INSERT [dbo].[tRating] ([ratingID], [memberID], [classID], [r_send], [r_sendText], [r_star], [r_sendTime]) VALUES (8, 47, 48, N'魏小萱', NULL, NULL, NULL)
INSERT [dbo].[tRating] ([ratingID], [memberID], [classID], [r_send], [r_sendText], [r_star], [r_sendTime]) VALUES (9, 47, 44, N'魏小萱', NULL, NULL, NULL)
INSERT [dbo].[tRating] ([ratingID], [memberID], [classID], [r_send], [r_sendText], [r_star], [r_sendTime]) VALUES (10, 47, 49, N'魏小萱', NULL, NULL, NULL)
INSERT [dbo].[tRating] ([ratingID], [memberID], [classID], [r_send], [r_sendText], [r_star], [r_sendTime]) VALUES (11, 47, 55, N'魏小萱', NULL, NULL, NULL)
INSERT [dbo].[tRating] ([ratingID], [memberID], [classID], [r_send], [r_sendText], [r_star], [r_sendTime]) VALUES (12, 4, 30, N'籐黑子', NULL, NULL, NULL)
INSERT [dbo].[tRating] ([ratingID], [memberID], [classID], [r_send], [r_sendText], [r_star], [r_sendTime]) VALUES (13, 4, 42, N'籐黑子', NULL, NULL, NULL)
INSERT [dbo].[tRating] ([ratingID], [memberID], [classID], [r_send], [r_sendText], [r_star], [r_sendTime]) VALUES (14, 4, 54, N'籐黑子', NULL, NULL, NULL)
INSERT [dbo].[tRating] ([ratingID], [memberID], [classID], [r_send], [r_sendText], [r_star], [r_sendTime]) VALUES (15, 4, 58, N'籐黑子', NULL, NULL, NULL)
INSERT [dbo].[tRating] ([ratingID], [memberID], [classID], [r_send], [r_sendText], [r_star], [r_sendTime]) VALUES (16, 7, 39, N'郭小白', NULL, NULL, NULL)
INSERT [dbo].[tRating] ([ratingID], [memberID], [classID], [r_send], [r_sendText], [r_star], [r_sendTime]) VALUES (17, 7, 53, N'郭小白', NULL, NULL, NULL)
INSERT [dbo].[tRating] ([ratingID], [memberID], [classID], [r_send], [r_sendText], [r_star], [r_sendTime]) VALUES (18, 7, 30, N'郭小白', NULL, NULL, NULL)
INSERT [dbo].[tRating] ([ratingID], [memberID], [classID], [r_send], [r_sendText], [r_star], [r_sendTime]) VALUES (19, 7, 31, N'郭小白', NULL, NULL, NULL)
INSERT [dbo].[tRating] ([ratingID], [memberID], [classID], [r_send], [r_sendText], [r_star], [r_sendTime]) VALUES (20, 7, 54, N'郭小白', NULL, NULL, NULL)
INSERT [dbo].[tRating] ([ratingID], [memberID], [classID], [r_send], [r_sendText], [r_star], [r_sendTime]) VALUES (21, 48, 29, N'畢小盡', NULL, NULL, NULL)
INSERT [dbo].[tRating] ([ratingID], [memberID], [classID], [r_send], [r_sendText], [r_star], [r_sendTime]) VALUES (22, 48, 56, N'畢小盡', NULL, NULL, NULL)
INSERT [dbo].[tRating] ([ratingID], [memberID], [classID], [r_send], [r_sendText], [r_star], [r_sendTime]) VALUES (23, 48, 42, N'畢小盡', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[tRating] OFF
GO
SET IDENTITY_INSERT [dbo].[tTeacher] ON 

INSERT [dbo].[tTeacher] ([teacherID], [t_certificateImg1], [t_certificateImg2], [t_certificateImg3], [t_certificateTxt], [t_title], [t_intro], [t_experience], [t_expertise], [t_messageTotal], [t_moneyTotal], [t_money], [t_studentTotal], [t_classTotal], [t_rateAvg], [t_socialMedia1], [t_socialMedia2], [t_socialMedia3], [t_socialMedia4], [memberID], [t_getNow]) VALUES (3, N'/Content/imgTeacher/32aa195b-30be-4561-a45c-fcc133ff910b.jpg', N'/Content/imgTeacher/3c1846a7-9205-4fcc-afb1-655ff7410db5.jpg', N'/Content/imgTeacher/57945c65-0718-478c-9ff2-134729b2a94d.jpg', N'RYT-200（Registered Yoga Teacher 200-hour）教練資格2019 Allpamama Healing Circle, Himalayan Singing Bowl Workshop Level 1', N'瑜珈老師', N'擁有瑜珈RYT-200教師資格', N'2018年 50小時陰瑜伽師資培訓
2017年 200小時瑜伽師資培訓', N'擅長伸展瑜珈、基礎瑜珈、熱瑜珈', 150, 3000, 700, 45, 7, 4, N'Facebook', N'Twitter', N'Instagram', N'Line', 3, CAST(N'2020-11-28T12:51:32.057' AS DateTime))
INSERT [dbo].[tTeacher] ([teacherID], [t_certificateImg1], [t_certificateImg2], [t_certificateImg3], [t_certificateTxt], [t_title], [t_intro], [t_experience], [t_expertise], [t_messageTotal], [t_moneyTotal], [t_money], [t_studentTotal], [t_classTotal], [t_rateAvg], [t_socialMedia1], [t_socialMedia2], [t_socialMedia3], [t_socialMedia4], [memberID], [t_getNow]) VALUES (5, N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'中華民國柔道授段證書四段
', N'金牌柔道教練
', N'多年柔道訓練經驗並規律實施訓練的柔道教練
', N'全國柔術比賽金牌
', N'擅長關節技、地上擒拿術
', 60, 500, 500, 60, 6, 4, N'Facebook
', N'Twitter
', N'Instagram
', N'Line
', 4, CAST(N'2020-11-28T12:51:32.057' AS DateTime))
INSERT [dbo].[tTeacher] ([teacherID], [t_certificateImg1], [t_certificateImg2], [t_certificateImg3], [t_certificateTxt], [t_title], [t_intro], [t_experience], [t_expertise], [t_messageTotal], [t_moneyTotal], [t_money], [t_studentTotal], [t_classTotal], [t_rateAvg], [t_socialMedia1], [t_socialMedia2], [t_socialMedia3], [t_socialMedia4], [memberID], [t_getNow]) VALUES (6, N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'中華民國羽球協會C級羽球教練、羽球裁判
', N'羽球教練', N'全國大專杯甲組排名賽女單第一名
', N'主修體育，專長羽球，有8年訓練和選手資歷，教學經驗有5年之久
', N'可提供中、英文羽球教學
', 50, 550, 500, 50, 5, 4, N'Facebook
', N'Twitter
', N'Instagram
', N'Line
', 7, CAST(N'2020-11-28T12:51:32.057' AS DateTime))
INSERT [dbo].[tTeacher] ([teacherID], [t_certificateImg1], [t_certificateImg2], [t_certificateImg3], [t_certificateTxt], [t_title], [t_intro], [t_experience], [t_expertise], [t_messageTotal], [t_moneyTotal], [t_money], [t_studentTotal], [t_classTotal], [t_rateAvg], [t_socialMedia1], [t_socialMedia2], [t_socialMedia3], [t_socialMedia4], [memberID], [t_getNow]) VALUES (7, N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'中華民國高爾夫協會C級教練合格名單', N'高爾夫教練', N'入選2009台灣高爾夫50大名師', N'十年教學經歷', N'揮桿基礎課程
初級高爾夫一對一課程', 40, 400, 400, 50, 7, 4, N'Facebook', N'Twitter', N'Instagram', N'Line', 8, CAST(N'2020-11-28T12:51:32.057' AS DateTime))
INSERT [dbo].[tTeacher] ([teacherID], [t_certificateImg1], [t_certificateImg2], [t_certificateImg3], [t_certificateTxt], [t_title], [t_intro], [t_experience], [t_expertise], [t_messageTotal], [t_moneyTotal], [t_money], [t_studentTotal], [t_classTotal], [t_rateAvg], [t_socialMedia1], [t_socialMedia2], [t_socialMedia3], [t_socialMedia4], [memberID], [t_getNow]) VALUES (8, N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'中華民國田徑協會核發C級教練證照', N'自由教練', N'教導學員學會正確姿勢與目標肌肉發力，不管是要增肌、減脂、雕塑體態、增加肌耐力、爆發力、運動表現等，才會收到好的效果。 ', N'服務項目： 1.重量訓練、抗阻訓練 2.減脂、有氧訓練 3.訓練動作 ', N'我的專長是指導目標肌肉發力、修正姿勢體態，目標是教會你能夠自己訓練，歡迎體驗。 ', 70, 600, 600, 70, 8, 3, N'Facebook', N'Twitter', N'Instagram', N'Line', 9, CAST(N'2020-11-28T12:51:32.057' AS DateTime))
INSERT [dbo].[tTeacher] ([teacherID], [t_certificateImg1], [t_certificateImg2], [t_certificateImg3], [t_certificateTxt], [t_title], [t_intro], [t_experience], [t_expertise], [t_messageTotal], [t_moneyTotal], [t_money], [t_studentTotal], [t_classTotal], [t_rateAvg], [t_socialMedia1], [t_socialMedia2], [t_socialMedia3], [t_socialMedia4], [memberID], [t_getNow]) VALUES (9, N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'TRX-STC(Suspension Trainer Course)懸吊訓練證照', N'重訓教練', N'體重的控制在於熱量，減脂、體態雕塑靠健身。', N'服務項目： 1.重量訓練、抗阻訓練 2.減脂、有氧訓練 3.訓練動作 ', N'我的專長是指導目標肌肉發力、修正姿勢體態，目標是教會你能夠自己訓練，歡迎體驗。', 80, 500, 500, 30, 6, 5, N'Facebook', N'Twitter', N'Instagram', N'Line', 10, CAST(N'2020-11-28T12:51:32.057' AS DateTime))
INSERT [dbo].[tTeacher] ([teacherID], [t_certificateImg1], [t_certificateImg2], [t_certificateImg3], [t_certificateTxt], [t_title], [t_intro], [t_experience], [t_expertise], [t_messageTotal], [t_moneyTotal], [t_money], [t_studentTotal], [t_classTotal], [t_rateAvg], [t_socialMedia1], [t_socialMedia2], [t_socialMedia3], [t_socialMedia4], [memberID], [t_getNow]) VALUES (10, N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'國際C級桌球教練講習', N'國際C級桌球教練講習', N'從學習桌球過程中，能讓學員增進團隊精神、激發學習能力，並且不斷自我成長，克服困難，勇於挑戰自我。', N'教學經驗長達20年以上，善於傾聽、溝通、了解家長及孩子的需求，多以鼓勵及因材施教的教學模式。', N'男子單人桌球', 80, 700, 700, 40, 6, 5, N'Facebook', N'Twitter', N'Instagram', N'Line', 28, CAST(N'2020-11-28T12:51:32.057' AS DateTime))
INSERT [dbo].[tTeacher] ([teacherID], [t_certificateImg1], [t_certificateImg2], [t_certificateImg3], [t_certificateTxt], [t_title], [t_intro], [t_experience], [t_expertise], [t_messageTotal], [t_moneyTotal], [t_money], [t_studentTotal], [t_classTotal], [t_rateAvg], [t_socialMedia1], [t_socialMedia2], [t_socialMedia3], [t_socialMedia4], [memberID], [t_getNow]) VALUES (11, N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'WBC國際拳擊教練認證', N'國際拳擊教練', N'中華民國拳擊協會109 年度C 級拳擊教練', N'十年拳擊教學經歷', N'重訓、硬舉、拳擊
', 50, 300, 300, 50, 5, 4, N'Facebook', N'Twitter', N'Instagram', N'Line', 29, CAST(N'2020-11-28T12:51:32.057' AS DateTime))
INSERT [dbo].[tTeacher] ([teacherID], [t_certificateImg1], [t_certificateImg2], [t_certificateImg3], [t_certificateTxt], [t_title], [t_intro], [t_experience], [t_expertise], [t_messageTotal], [t_moneyTotal], [t_money], [t_studentTotal], [t_classTotal], [t_rateAvg], [t_socialMedia1], [t_socialMedia2], [t_socialMedia3], [t_socialMedia4], [memberID], [t_getNow]) VALUES (12, N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'教練證照', N'籃球教練', N'籃球專業年資十年,HBL&UBA球員，BH Fitness Rider 飛輪教練認證', N'十年籃球教學經歷', N'旅美籃球選手', 100, 500, 500, 20, 7, 5, N'Facebook', N'Twitter', N'Instagram', N'Line', 30, CAST(N'2020-11-28T12:51:32.057' AS DateTime))
INSERT [dbo].[tTeacher] ([teacherID], [t_certificateImg1], [t_certificateImg2], [t_certificateImg3], [t_certificateTxt], [t_title], [t_intro], [t_experience], [t_expertise], [t_messageTotal], [t_moneyTotal], [t_money], [t_studentTotal], [t_classTotal], [t_rateAvg], [t_socialMedia1], [t_socialMedia2], [t_socialMedia3], [t_socialMedia4], [memberID], [t_getNow]) VALUES (13, N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'亞洲足球聯盟(AFC)資深教練證照', N'台灣木蘭足球聯賽冠軍教練', N'2007 亞洲足球聯盟B級教練證', N'五年教學經驗', N'足球、桌球', 400, 400, 400, 50, 3, 3, N'Facebook', N'Twitter', N'Instagram', N'Line', 31, CAST(N'2020-11-28T12:51:32.057' AS DateTime))
INSERT [dbo].[tTeacher] ([teacherID], [t_certificateImg1], [t_certificateImg2], [t_certificateImg3], [t_certificateTxt], [t_title], [t_intro], [t_experience], [t_expertise], [t_messageTotal], [t_moneyTotal], [t_money], [t_studentTotal], [t_classTotal], [t_rateAvg], [t_socialMedia1], [t_socialMedia2], [t_socialMedia3], [t_socialMedia4], [memberID], [t_getNow]) VALUES (14, N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'開放水域救生員/游泳裁判教練', N'游泳教練', N'高雄大學附設游泳池之游泳教練
', N'十年教學經歷', N'自由式、蛙式、仰式、蝶式', 10, 600, 600, 40, 5, 5, N'Facebook', N'Twitter', N'Instagram', N'Line', 32, CAST(N'2020-11-28T12:51:32.057' AS DateTime))
INSERT [dbo].[tTeacher] ([teacherID], [t_certificateImg1], [t_certificateImg2], [t_certificateImg3], [t_certificateTxt], [t_title], [t_intro], [t_experience], [t_expertise], [t_messageTotal], [t_moneyTotal], [t_money], [t_studentTotal], [t_classTotal], [t_rateAvg], [t_socialMedia1], [t_socialMedia2], [t_socialMedia3], [t_socialMedia4], [memberID], [t_getNow]) VALUES (15, N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'中華民國C級飛盤教練證照', N'飛盤教練', N'中華民國C級飛盤教練', N'六年資深飛盤C級教練、競賽飛盤教練', N'擅長引領飛盤初學者入門、飛盤選手訓練', 60, 400, 400, 60, 9, 3, N'Facebook', N'Twitter', N'Instagram', N'Line', 33, CAST(N'2020-11-28T12:51:32.057' AS DateTime))
INSERT [dbo].[tTeacher] ([teacherID], [t_certificateImg1], [t_certificateImg2], [t_certificateImg3], [t_certificateTxt], [t_title], [t_intro], [t_experience], [t_expertise], [t_messageTotal], [t_moneyTotal], [t_money], [t_studentTotal], [t_classTotal], [t_rateAvg], [t_socialMedia1], [t_socialMedia2], [t_socialMedia3], [t_socialMedia4], [memberID], [t_getNow]) VALUES (16, N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'中華民國射擊協會C級教練證照/世界射箭總會第二級教練證照', N'世界射箭總會第二級教練', N'中華民國射擊協會C級教練
世界射箭總會第二級教練', N'射箭運動推廣及教學
中正射箭場之招生及招募系統之建立', N'擅長標靶射箭、原野射箭、地靶射箭', 123, 700, 700, 40, 7, 4, N'Facebook', N'Twitter', N'Instagram', N'Line', 34, CAST(N'2020-11-28T12:51:32.057' AS DateTime))
INSERT [dbo].[tTeacher] ([teacherID], [t_certificateImg1], [t_certificateImg2], [t_certificateImg3], [t_certificateTxt], [t_title], [t_intro], [t_experience], [t_expertise], [t_messageTotal], [t_moneyTotal], [t_money], [t_studentTotal], [t_classTotal], [t_rateAvg], [t_socialMedia1], [t_socialMedia2], [t_socialMedia3], [t_socialMedia4], [memberID], [t_getNow]) VALUES (18, N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'C級棒球教練講習合格人員教練證，中華網球協會C級教練證', N'網球教練、新北市棒球聯賽亞軍', N'身為網球選手也同樣熱愛棒球，歡迎新手加入我的課程', N'網球教學十年', N'網球、棒球', 123, 700, 700, 40, 7, 4, N'Facebook', N'Twitter', N'Instagram', N'Line', 35, CAST(N'2020-11-28T12:51:32.057' AS DateTime))
INSERT [dbo].[tTeacher] ([teacherID], [t_certificateImg1], [t_certificateImg2], [t_certificateImg3], [t_certificateTxt], [t_title], [t_intro], [t_experience], [t_expertise], [t_messageTotal], [t_moneyTotal], [t_money], [t_studentTotal], [t_classTotal], [t_rateAvg], [t_socialMedia1], [t_socialMedia2], [t_socialMedia3], [t_socialMedia4], [memberID], [t_getNow]) VALUES (19, N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'國家認證109年C級滑輪板教練證照', N'滑板教練', N'不管你是想要帥氣的溜著滑板，並用滑板版來代步，甚至是想要學習高超的滑板技巧，如果你正在尋滑板教練，歡迎報名清樹教練的課程！', N'擁有長達五年滑板經驗', N'擅長長板、短板、技術板', 1, 2, 1, 1, 1, 4.5, N'Facebook', N'Twitter', N'Instagram', N'Line', 42, CAST(N'2020-11-28T12:51:32.057' AS DateTime))
INSERT [dbo].[tTeacher] ([teacherID], [t_certificateImg1], [t_certificateImg2], [t_certificateImg3], [t_certificateTxt], [t_title], [t_intro], [t_experience], [t_expertise], [t_messageTotal], [t_moneyTotal], [t_money], [t_studentTotal], [t_classTotal], [t_rateAvg], [t_socialMedia1], [t_socialMedia2], [t_socialMedia3], [t_socialMedia4], [memberID], [t_getNow]) VALUES (20, N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'中華民國滑輪溜冰協會C級教練證照', N'直排輪教練', N'中華民國滑輪溜冰協會C級教練', N'4年直排輪教學經歷', N'擅長競速直排輪及花式直排輪', 1, 1, 1, 1, 1, 4.5, N'Facebook', N'Twitter', N'Instagram', N'Line', 43, CAST(N'2020-11-28T12:53:39.497' AS DateTime))
INSERT [dbo].[tTeacher] ([teacherID], [t_certificateImg1], [t_certificateImg2], [t_certificateImg3], [t_certificateTxt], [t_title], [t_intro], [t_experience], [t_expertise], [t_messageTotal], [t_moneyTotal], [t_money], [t_studentTotal], [t_classTotal], [t_rateAvg], [t_socialMedia1], [t_socialMedia2], [t_socialMedia3], [t_socialMedia4], [memberID], [t_getNow]) VALUES (21, N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'/Content/imgTeacher/defaultTeacher.png', N'DVTA中華民國舞蹈職業教練', N'前香港城市當代舞團（CCDC）舞者', N'曾為香港城市當代舞團之舞者。在團期間曾至德國(柏林、斯圖加特、慕尼黑及十多個城市)、美國(華盛頓、休士頓、洛杉磯、紐約)、英國倫敦、韓國、澳洲、法國里昂、布拉格、瑞士、奧地利、荷蘭、義大利、威尼斯、新加坡、台灣、北京、上海、廣東等多個國家及城市演出。多次演出黎海寧、彭錦耀等著名編舞家的作品中之獨舞及雙人舞。以及曹誠淵、舒巧合編的舞劇三毛(飾演三毛) 。', N'於香港期間，多次為香港業餘舞團編舞及授課。', N'現代舞獨舞、雙人舞', 20, 6000, 200, 15, 1, 3.6, N'Facebook
', N'Twitter
', N'Instagram
', N'Line
', 46, CAST(N'2020-12-09T22:09:36.763' AS DateTime))
SET IDENTITY_INSERT [dbo].[tTeacher] OFF
GO
ALTER TABLE [dbo].[tBill]  WITH CHECK ADD  CONSTRAINT [FK_tBill_tMember] FOREIGN KEY([memberID])
REFERENCES [dbo].[tMember] ([memberID])
GO
ALTER TABLE [dbo].[tBill] CHECK CONSTRAINT [FK_tBill_tMember]
GO
ALTER TABLE [dbo].[tClass]  WITH CHECK ADD  CONSTRAINT [FK_tClass_tTeacher] FOREIGN KEY([teacherID])
REFERENCES [dbo].[tTeacher] ([teacherID])
GO
ALTER TABLE [dbo].[tClass] CHECK CONSTRAINT [FK_tClass_tTeacher]
GO
ALTER TABLE [dbo].[tDeposit]  WITH CHECK ADD  CONSTRAINT [FK_tDeposit_tMember] FOREIGN KEY([memberID])
REFERENCES [dbo].[tMember] ([memberID])
GO
ALTER TABLE [dbo].[tDeposit] CHECK CONSTRAINT [FK_tDeposit_tMember]
GO
ALTER TABLE [dbo].[tMessage]  WITH CHECK ADD  CONSTRAINT [FK_tMessage_tMember] FOREIGN KEY([memberID])
REFERENCES [dbo].[tMember] ([memberID])
GO
ALTER TABLE [dbo].[tMessage] CHECK CONSTRAINT [FK_tMessage_tMember]
GO
ALTER TABLE [dbo].[tOrder]  WITH CHECK ADD  CONSTRAINT [FK_tOrder_tMember] FOREIGN KEY([memberID])
REFERENCES [dbo].[tMember] ([memberID])
GO
ALTER TABLE [dbo].[tOrder] CHECK CONSTRAINT [FK_tOrder_tMember]
GO
ALTER TABLE [dbo].[tOrder_Detail]  WITH CHECK ADD  CONSTRAINT [FK_tOrder_Detail_tClass] FOREIGN KEY([classID])
REFERENCES [dbo].[tClass] ([classID])
GO
ALTER TABLE [dbo].[tOrder_Detail] CHECK CONSTRAINT [FK_tOrder_Detail_tClass]
GO
ALTER TABLE [dbo].[tOrder_Detail]  WITH CHECK ADD  CONSTRAINT [FK_tOrder_Detail_tOrder] FOREIGN KEY([orderID])
REFERENCES [dbo].[tOrder] ([orderID])
GO
ALTER TABLE [dbo].[tOrder_Detail] CHECK CONSTRAINT [FK_tOrder_Detail_tOrder]
GO
ALTER TABLE [dbo].[tPay]  WITH CHECK ADD  CONSTRAINT [FK_tPay_tTeacher] FOREIGN KEY([teacherID])
REFERENCES [dbo].[tTeacher] ([teacherID])
GO
ALTER TABLE [dbo].[tPay] CHECK CONSTRAINT [FK_tPay_tTeacher]
GO
ALTER TABLE [dbo].[tRating]  WITH CHECK ADD  CONSTRAINT [FK_tRating_tClass] FOREIGN KEY([classID])
REFERENCES [dbo].[tClass] ([classID])
GO
ALTER TABLE [dbo].[tRating] CHECK CONSTRAINT [FK_tRating_tClass]
GO
ALTER TABLE [dbo].[tRating]  WITH CHECK ADD  CONSTRAINT [FK_tRating_tMember] FOREIGN KEY([memberID])
REFERENCES [dbo].[tMember] ([memberID])
GO
ALTER TABLE [dbo].[tRating] CHECK CONSTRAINT [FK_tRating_tMember]
GO
ALTER TABLE [dbo].[tShop]  WITH CHECK ADD  CONSTRAINT [FK_tShop_tClass] FOREIGN KEY([classID])
REFERENCES [dbo].[tClass] ([classID])
GO
ALTER TABLE [dbo].[tShop] CHECK CONSTRAINT [FK_tShop_tClass]
GO
ALTER TABLE [dbo].[tShop]  WITH CHECK ADD  CONSTRAINT [FK_tShop_tMember] FOREIGN KEY([memberID])
REFERENCES [dbo].[tMember] ([memberID])
GO
ALTER TABLE [dbo].[tShop] CHECK CONSTRAINT [FK_tShop_tMember]
GO
ALTER TABLE [dbo].[tTeacher]  WITH CHECK ADD  CONSTRAINT [FK_tTeacher_tMember] FOREIGN KEY([memberID])
REFERENCES [dbo].[tMember] ([memberID])
GO
ALTER TABLE [dbo].[tTeacher] CHECK CONSTRAINT [FK_tTeacher_tMember]
GO
ALTER TABLE [dbo].[tWish]  WITH CHECK ADD  CONSTRAINT [FK_tWish_tClass] FOREIGN KEY([classID])
REFERENCES [dbo].[tClass] ([classID])
GO
ALTER TABLE [dbo].[tWish] CHECK CONSTRAINT [FK_tWish_tClass]
GO
ALTER TABLE [dbo].[tWish]  WITH CHECK ADD  CONSTRAINT [FK_tWish_tMember] FOREIGN KEY([memberID])
REFERENCES [dbo].[tMember] ([memberID])
GO
ALTER TABLE [dbo].[tWish] CHECK CONSTRAINT [FK_tWish_tMember]
GO
USE [master]
GO
ALTER DATABASE [JJMdb] SET  READ_WRITE 
GO
