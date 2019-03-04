create procedure [oauth].[pSessionTokenTake]
	@id uniqueidentifier
as
begin
	select *
	from oauth.sessionToken
	where id = @id
	delete from oauth.sessionToken where id = @id
end

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

create procedure [oauth].[pAccountGetById]
	@id uniqueidentifier
as
begin
	select *
	from [oauth].[account]
	where [id] = @id
end

create procedure [oauth].[pAccountGetByUserName]
	@userName nvarchar(300)
as
begin
	select *
	from [oauth].[account]
	where [userName] = @userName
end

create procedure [oauth].[pAccountGetByEmail]
	@email nvarchar(330)
as
begin
	select *
	from [oauth].[account]
	where [email] = @email
end

create procedure [oauth].[pAccountActivate]
	@id uniqueidentifier
as
begin
	update [oauth].[account] set [isActivated] = 1 where [id]=@id
end

go
-----------------------------------------------------------------