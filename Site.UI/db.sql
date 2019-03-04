if (not exists (select *
from sys.schemas
where name = 'oauth')) 
begin
    exec ('create schema [oauth]')
end

go
-----------------------------------------------------------------
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
create procedure [oauth].[pSessionTokenTake]
	@id uniqueidentifier
as
begin
	select *
	from oauth.sessionToken
	where id = @id
	delete from oauth.sessionToken where id = @id
end

go

create procedure [oauth].[pSessionTokenInsert]
	@id uniqueidentifier,
	@accountId uniqueidentifier,
	@issuedUtc datetime,
	@expiresUtc datetime,
	@protectedTicket nvarchar(900)
as
begin
	insert into [oauth].[sessionToken]
		([id], [accountId], [issuedUtc], [expiresUtc], [protectedTicket])
	values
		(@id, @accountId, @issuedUtc, @expiresUtc, @protectedTicket)
end

go

create procedure [oauth].[pAccountGetById]
	@id uniqueidentifier
as
begin
	select *
	from [oauth].[account]
	where [id] = @id
end

go

create procedure [oauth].[pAccountGetByUserName]
	@userName nvarchar(300)
as
begin
	select *
	from [oauth].[account]
	where [userName] = @userName
end

go

create procedure [oauth].[pAccountGetByEmail]
	@email nvarchar(330)
as
begin
	select *
	from [oauth].[account]
	where [email] = @email
end

go

create procedure [oauth].[pAccountActivate]
	@id uniqueidentifier
as
begin
	update [oauth].[account] set [isActivated] = 1 where [id]=@id
end

go
-----------------------------------------------------------------