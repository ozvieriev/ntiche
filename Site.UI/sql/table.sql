create table [oauth].[account]
(
    [id] uniqueidentifier constraint [df_oauthAccountId] default (newid()) not null,
    [firstName] nvarchar (300) not null,
    [lastName] nvarchar (300) not null,
    [userName] nvarchar (300) not null,
    [password] nvarchar (50) not null,
    [email] nvarchar (330) not null,
    [ocupation] nvarchar (300) not null,
    [countryIso2] nvarchar (2) not null,
    [province] nvarchar (300) not null,
    [city] nvarchar (300) not null,
    [isActivated] bit default (0) not null,
    [isOptin] bit default (0) not null,
    [createDateUtc] datetime default (getutcdate()) not null,
    constraint [pk_oauthAccount] primary key clustered ([id] asc),
    constraint [uniq_oauthAccountUserName] unique nonclustered ([userName] ASC),
    constraint [uniq_oauthAccountEmail] unique nonclustered ([email] ASC)
);

create table [oauth].[sessionToken]
(
    [id] uniqueidentifier constraint [df_oauthSessionTokenId] default (newid()) not null,
    [accountId] uniqueidentifier not null,
    [issuedUtc] datetime not null,
    [expiresUtc] datetime not null,
    [protectedTicket] varchar (900) not null,
    [createDateUtc] datetime default (getutcdate()) not null,
    constraint [pk_oauthSessionToken] primary key clustered ([id] asc),
    constraint [fk_oauthAccountAccountId] foreign key ([accountId]) references [oauth].[account] ([id])
);

go
-----------------------------------------------------------------