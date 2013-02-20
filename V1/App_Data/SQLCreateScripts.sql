CREATE TABLE [dbo].[entity_IssuesCountPerDays] (
    [Date]  DATETIME NOT NULL,
    [Count] BIGINT   NOT NULL,
    CONSTRAINT [PK_IssuesCountPerDays] PRIMARY KEY CLUSTERED ([Date] ASC)
);
CREATE TABLE [dbo].[entity_Place_Pin] (
    [PIN]        NVARCHAR (8)  NOT NULL,
    [Place_Name] NVARCHAR (36) NOT NULL,
    [District]   NVARCHAR (36) NULL,
    [State]      NVARCHAR (36) NULL,
    CONSTRAINT [PK_Place_Pin] PRIMARY KEY CLUSTERED ([PIN] ASC)
);
CREATE TABLE [dbo].[entity_Issues] (
    [IssueId]           NVARCHAR (15)  NOT NULL,
    [Subject]           NVARCHAR (50)  NOT NULL,
    [Description]       NVARCHAR (400) NOT NULL,
    [IssueVisibility]   BIT            NULL,
    [CreatorVisibility] BIT            NULL,
    [Category_ID]       TINYINT        NULL,
    [CreatorID]         NVARCHAR (30)  NULL,
    [Comment_ID]        NVARCHAR (12)  NULL,
    [Creation_date]     DATETIME       DEFAULT (getdate()) NOT NULL,
    [PIN]               NVARCHAR (8)   NULL,
    CONSTRAINT [PK_Issues] PRIMARY KEY CLUSTERED ([IssueId] ASC),
    CONSTRAINT [FK_Pin_Issue] FOREIGN KEY ([PIN]) REFERENCES [dbo].[entity_Place_Pin] ([PIN])
);
--ALTER TABLE Orders ADD CONSTRAINT fk_PerOrders FOREIGN KEY (P_Id)  REFERENCES Persons(P_Id)

CREATE TABLE [dbo].[entity_Attachments] (
    [Attachment_Id] NVARCHAR (20) NOT NULL,
    [Filename]      NVARCHAR (50) NOT NULL,
    [IssueId]       NVARCHAR (15) NOT NULL,
    CONSTRAINT [PK_Attachments] PRIMARY KEY CLUSTERED ([Attachment_Id] ASC),
    CONSTRAINT [FK_IssueAttachments] FOREIGN KEY ([IssueId]) REFERENCES [dbo].[entity_Issues] ([IssueId])
);
GO
CREATE NONCLUSTERED INDEX [IX_FK_IssueAttachments]
    ON [dbo].[entity_Attachments]([IssueId] ASC);

CREATE TABLE [dbo].[entity_Comments] (
    [Comment_Id]  NVARCHAR (12)  NOT NULL,
    [Description] NVARCHAR (200) NOT NULL,
    [IssueId]     NVARCHAR (15)  NOT NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED ([Comment_Id] ASC),
    CONSTRAINT [FK_IssueComment] FOREIGN KEY ([IssueId]) REFERENCES [dbo].[entity_Issues] ([IssueId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_IssueComment]
    ON [dbo].[entity_Comments]([IssueId] ASC);

--CREATE TRIGGER TRG_issue_countPday_update   ON  entity_issues AFTER INSERT
--AS 
--BEGIN
-- insert into entity_IssuesCountPerDays values(GETDATE(),1)
--END

----AUTHENTICATION-----------------------------------------------
CREATE TABLE [dbo].[webpages_Roles] (
    [RoleId]   INT            IDENTITY (1, 1) NOT NULL,
    [RoleName] NVARCHAR (256) NOT NULL,
    PRIMARY KEY CLUSTERED ([RoleId] ASC),
    UNIQUE NONCLUSTERED ([RoleName] ASC)
);

CREATE TABLE [dbo].[webpages_Membership] (
    [UserId]                                  INT            NOT NULL,
    [CreateDate]                              DATETIME       NULL,
    [ConfirmationToken]                       NVARCHAR (128) NULL,
    [IsConfirmed]                             BIT            DEFAULT ((0)) NULL,
    [LastPasswordFailureDate]                 DATETIME       NULL,
    [PasswordFailuresSinceLastSuccess]        INT            DEFAULT ((0)) NOT NULL,
    [Password]                                NVARCHAR (128) NOT NULL,
    [PasswordChangedDate]                     DATETIME       NULL,
    [PasswordSalt]                            NVARCHAR (128) NOT NULL,
    [PasswordVerificationToken]               NVARCHAR (128) NULL,
    [PasswordVerificationTokenExpirationDate] DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);


CREATE TABLE [dbo].[UserProfile] (
    [UserId]   INT            IDENTITY (1, 1) NOT NULL,
    [UserName] NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);

CREATE TABLE [dbo].[webpages_OAuthMembership] (
    [Provider]       NVARCHAR (30)  NOT NULL,
    [ProviderUserId] NVARCHAR (100) NOT NULL,
    [UserId]         INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Provider] ASC, [ProviderUserId] ASC)
);

CREATE TABLE [dbo].[webpages_UsersInRoles] (
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [fk_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserProfile] ([UserId]),
    CONSTRAINT [fk_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[webpages_Roles] ([RoleId])
);


