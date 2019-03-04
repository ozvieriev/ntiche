if (not exists (select * from sys.schemas where name = 'oauth')) 
begin
    exec ('create schema oauth')
end

go
-----------------------------------------------------------------