CREATE TABLE [dbo].[OffersTemp](
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [SiteId] [int] NOT NULL,
    [OfferId] [nvarchar](100) NOT NULL,
    [OfferInfo] [xml] NOT NULL,
    [CreationDate] [datetime2](7) NOT NULL,
    CONSTRAINT [PK_OffersTemp] PRIMARY KEY CLUSTERED
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO

ALTER TABLE [dbo].[OffersTemp] ADD  CONSTRAINT [DF_OffersTemp_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
    GO