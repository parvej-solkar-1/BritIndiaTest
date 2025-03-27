CREATE TABLE [dbo].[RefreshToken] (
    [Id]       INT            NOT NULL,
    [UserId]   NVARCHAR (50)  NOT NULL,
    [Token]    NVARCHAR (MAX) NOT NULL,
    [IsActive] BIT            NOT NULL,
    CONSTRAINT [PK_RefreshToken] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RefreshToken_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId])
);

