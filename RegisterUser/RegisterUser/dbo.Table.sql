CREATE TABLE [dbo].[UserData] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [Name]     VARCHAR (50) NULL,
    [Surname]  VARCHAR (50) NULL,
    [Email]    VARCHAR (50) NOT NULL,
    [Password] NCHAR (10)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
