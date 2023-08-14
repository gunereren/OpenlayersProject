CREATE TABLE [dbo].[Parcels] (
    [ParcelId]       INT           IDENTITY (1, 1) NOT NULL,
    [ParcelCity]     NVARCHAR (100),
    [ParcelDistrict] NVARCHAR (100),
    [ParcelNeighbourhood] NVARCHAR (100)
);

GO

CREATE NONCLUSTERED INDEX [ParcelCity]
    ON [dbo].[Parcels]([ParcelCity] ASC);


GO
CREATE NONCLUSTERED INDEX [ParcelDistrict]
    ON [dbo].[Parcels]([ParcelDistrict] ASC);


GO
CREATE NONCLUSTERED INDEX [ParcelNeighbourhood]
    ON [dbo].[Parcels]([ParcelNeighbourhood] ASC);

