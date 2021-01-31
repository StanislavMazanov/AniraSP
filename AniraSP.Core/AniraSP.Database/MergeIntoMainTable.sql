CREATE TABLE #OT
(Id      INT,
 SiteId  INT,
 OfferId NVARCHAR(50),
 OfferInfo NVARCHAR(max),
 CreationDate Datetime2(7));
--readpast
INSERT INTO #OT
SELECT TOP (500000)
           Id,
       SiteId,
       OfferId,
       OfferInfo,
       CreationDate
from OffersTemp

         MERGE dbo.Offers AS T --Целевая таблица
	   USING (SELECT Id, SiteId, OfferId, OfferInfo, CreationDate from #OT WHERE Id in (SELECT max(Id) FROM #OT group by SiteId, OfferId))  as Source
ON (T.SiteId = Source.SiteId and  T.OfferId=Source.OfferId) --Условие объединени
    WHEN MATCHED THEN --Если истина (UPDATE)
UPDATE SET OfferInfo = T.OfferInfo
    WHEN NOT MATCHED THEN --Если НЕ истина (INSERT)
INSERT (SiteId, OfferId, OfferInfo)
VALUES (Source.SiteId, Source.OfferId, Source.OfferInfo);

DELETE OffersTemp
WHERE Id in (SELECT Id FROM #OT);
DROP TABLE #OT