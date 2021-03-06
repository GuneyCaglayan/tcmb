USE [TCMB]
GO
/****** Object:  Table [dbo].[Kurlar]    Script Date: 16.11.2019 15:22:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kurlar](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Tarih] [date] NOT NULL,
	[Kod] [nvarchar](3) NOT NULL,
	[Adi] [nvarchar](50) NOT NULL,
	[Alis] [decimal](7, 4) NOT NULL,
	[Satis] [decimal](7, 4) NOT NULL,
 CONSTRAINT [PK_Kurlar] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
