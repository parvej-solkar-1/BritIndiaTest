CREATE TABLE [dbo].[User] (
    [UserId]   NVARCHAR (50)  NOT NULL,
    [Password] NVARCHAR (MAX) NOT NULL,
    [Role]     NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_User_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId])
);

