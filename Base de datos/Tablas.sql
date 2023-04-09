USE [Newshore.Technical.DataBase]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--***TABLA Flight****
CREATE TABLE [dbo].[Flight](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Origin] [varchar](4) NULL,
	[Destination] [varchar](4) NULL,
	[Price] [float] NULL,
	[TransportId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [FK_Flight_Transport] FOREIGN KEY([TransportId])
REFERENCES [dbo].[Transport] ([Id])
GO

ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [FK_Flight_Transport]
GO

----------------------------------------------------------------------------------------------------------

--***TABLA Journey****
CREATE TABLE [dbo].[Journey](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Origin] [varchar](4) NULL,
	[Destination] [varchar](4) NULL,
	[Price] [float] NULL,
	[IsDirectFlight] [bit] NULL,
	[IsRoundTripFlight] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-------------------------------------------------------------------------------------------------------------

--***TABLA JourneyFlight****
CREATE TABLE [dbo].[JourneyFlight](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JourneyId] [int] NULL,
	[FlightId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[JourneyFlight]  WITH CHECK ADD  CONSTRAINT [FK_JourneyFlight_Flight] FOREIGN KEY([FlightId])
REFERENCES [dbo].[Flight] ([Id])
GO

ALTER TABLE [dbo].[JourneyFlight] CHECK CONSTRAINT [FK_JourneyFlight_Flight]
GO

ALTER TABLE [dbo].[JourneyFlight]  WITH CHECK ADD  CONSTRAINT [FK_JourneyFlight_Journey] FOREIGN KEY([JourneyId])
REFERENCES [dbo].[Journey] ([Id])
GO

ALTER TABLE [dbo].[JourneyFlight] CHECK CONSTRAINT [FK_JourneyFlight_Journey]
GO

-------------------------------------------------------------------------------------------------------------

--***TABLA Transport****
CREATE TABLE [dbo].[Transport](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FlightCarrier] [varchar](100) NULL,
	[FlightNumber] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO