CREATE TABLE [dbo].[Offers](
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [SiteId] [int] NOT NULL,
    [OfferId] [nvarchar](100) NOT NULL,
    [OfferInfo] [xml] NOT NULL,
    [CreationDate] [datetime2](7) NOT NULL,
    [UpdateDate] [datetime2](7) NOT NULL,
    CONSTRAINT [PK_Offer] PRIMARY KEY CLUSTERED
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO

ALTER TABLE [dbo].[Offers] ADD  CONSTRAINT [DF_Offer_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
    GO

ALTER TABLE [dbo].[Offers] ADD  CONSTRAINT [DF_Offer_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
    GO