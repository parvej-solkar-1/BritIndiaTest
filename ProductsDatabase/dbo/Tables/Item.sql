CREATE TABLE [dbo].[Item] (
    [Id]        INT IDENTITY (1, 1) NOT NULL,
    [ProductId] INT NOT NULL,
    [Quantity]  INT NOT NULL,
    CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Item_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])
);

