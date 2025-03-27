CREATE TABLE [dbo].[Product] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [ProductName] NVARCHAR (255) NOT NULL,
    [CreatedBy]   NVARCHAR (100) NOT NULL,
    [CreatedOn]   DATETIME       NOT NULL,
    [ModifiedBy]  NVARCHAR (100) NULL,
    [ModifiedOn]  DATETIME       NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC)
);

