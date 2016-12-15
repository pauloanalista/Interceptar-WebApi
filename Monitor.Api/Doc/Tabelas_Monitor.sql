-- Sql Server
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

-- MySql

CREATE TABLE `logapi` (
  `Id` varchar(50) NOT NULL,
  `Application` varchar(100) DEFAULT NULL,
  `Machine` varchar(100) DEFAULT NULL,
  `RequestContentBody` varchar(1000) DEFAULT NULL,
  `RequestContentType` varchar(1000) DEFAULT NULL,
  `RequestHeaders` varchar(1000) DEFAULT NULL,
  `RequestIpAddress` varchar(1000) DEFAULT NULL,
  `RequestMethod` varchar(1000) DEFAULT NULL,
  `RequestRouteData` varchar(1000) DEFAULT NULL,
  `RequestRouteTemplate` varchar(1000) DEFAULT NULL,
  `RequestTimestamp` datetime DEFAULT NULL,
  `RequestUri` varchar(1024) DEFAULT NULL,
  `ResponseContentBody` varchar(1000) DEFAULT NULL,
  `ResponseContentType` varchar(1000) DEFAULT NULL,
  `ResponseHeaders` varchar(1000) DEFAULT NULL,
  `ResponseStatusCode` int(11) DEFAULT NULL,
  `ResponseTimestamp` datetime DEFAULT NULL,
  `User` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
