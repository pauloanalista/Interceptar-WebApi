CREATE TABLE [dbo].[LogApi](
	[Id] [varchar](50) NOT NULL,
	[Application] [varchar](100) NULL,
	[Machine] [varchar](100) NULL,
	[RequestContentBody] [varchar](max) NULL,
	[RequestContentType] [varchar](max) NULL,
	[RequestHeaders] [varchar](max) NULL,
	[RequestIpAddress] [varchar](max) NULL,
	[RequestMethod] [varchar](300) NULL,
	[RequestRouteData] [varchar](max) NULL,
	[RequestRouteTemplate] [varchar](max) NULL,
	[RequestTimestamp] [datetime] NULL,
	[RequestUri] [varchar](1024) NULL,
	[ResponseContentBody] [varchar](max) NULL,
	[ResponseContentType] [varchar](max) NULL,
	[ResponseHeaders] [varchar](max) NULL,
	[ResponseStatusCode] [int] NULL,
	[ResponseTimestamp] [datetime] NULL,
	[User] [varchar](max) NULL,
 CONSTRAINT [PK__LogApi__3214EC07970CD49C] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
