update dict.pharmacySetting set [name] = 'Hospital Pharmacy' where id = 1
update dict.pharmacySetting set [name] = 'Community Pharmacy' where id = 2
update dict.pharmacySetting set [name] = 'Other' where id = 3

--EXEC sp_rename 'oauth.account.ocupation', 'specialty', 'COLUMN';  
--alter procedure test.vExamResultReport
--alter procedure test.vFeedbackReport

--create table [dict].[specialty]
--insert into dict.specialty ([name]) values ('Pharmacist')
--insert into dict.specialty ([name]) values ('Other')

--alter table oauth.account add pharmacySetting nvarchar(300) null
--alter table oauth.account add specialtyId int default(1) not null
--alter table oauth.account alter column specialty nvarchar(300) null
--alter table oauth.account add constraint [fk_oauthAccountSpecialtyId] foreign key ([specialtyId]) references [dict].[specialty] ([id])
