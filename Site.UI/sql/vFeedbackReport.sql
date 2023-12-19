alter procedure test.vFeedbackReport
	@accountFirstName nvarchar (300) null,
	@accountLastName nvarchar (300) null,
	@accountPharmacistLicense nvarchar(100) null,
	@accountPharmacySettingId int null,
	@accountProvinceId char(2) null,
	@accountEmail nvarchar (330) null,
	@accountCity nvarchar (300) null,
	--@accountIsActivated bit null,
	@accountIsOptin bit null,
	@accountFrom datetime null,
	@accountTo datetime null
as
begin

	if @accountFrom is not null and @accountTo is not null and @accountFrom > @accountTo
	begin
		declare @tmp datetime = @accountFrom
		set @accountFrom = @accountTo
		set @accountTo = @tmp
	end

	select 
		account.firstName as accountFirstName,
		account.lastName as accountLastName,
		account.pharmacistLicense as accountPharmacistLicense,
		--account.pharmacySetting as accountPharmacySetting,
		isnull(account.pharmacySetting, pharmacySetting.[name])  as accountPharmacySetting,
		province.[name] as accoutnProvinceName,
		account.email as accountEmail,
		--account.specialty as accountSpecialty,
		isnull(account.specialty, specialty.[name]) as accountSpecialty,
		'Canada' as accountCountryName,
		account.city as accountCity,
		--account.isActivated as accountIsActivated,
		account.isOptin as accountIsOptin,
		account.createDate as accountCreateDate,

		feedback.enhancedRating as feedbackEnhancedRating,
		feedback.overallLearningObjectives1Before as feedbackOverallLearningObjectives1Before,
        feedback.overallLearningObjectives1After as feedbackOverallLearningObjectives1After,
        feedback.overallLearningObjectives1Relevance as feedbackOverallLearningObjectives1Relevance,
        feedback.overallLearningObjectives2Before as feedbackOverallLearningObjectives2Before,
        feedback.overallLearningObjectives2After as feedbackOverallLearningObjectives2After,
        feedback.overallLearningObjectives2Relevance as feedbackOverallLearningObjectives2Relevance, 
        feedback.overallLearningObjectives3Before as feedbackOverallLearningObjectives3Before,
        feedback.overallLearningObjectives3After as feedbackOverallLearningObjectives3After,
        feedback.overallLearningObjectives3Relevance as feedbackOverallLearningObjectives3Relevance,

		feedback.programRating as feedbackProgramRating,
		feedback.programRatingEducational as feedbackProgramRatingEducational,
		feedback.isAppreciateDelivery as feedbackIsAppreciateDelivery,
		feedback.isPerceiveDegree as feedbackIsPerceiveDegree,
		feedback.meetStatedLearningObjectives as feedbackMeetStatedLearningObjectives,
		feedback.perceiveDegreeComments as feedbackPerceiveDegreeComments,
		feedback.changesComments as feedbackChangesComments,
		feedback.topicsComments as feedbackTopicsComments,
		feedback.additionalComments as feedbackAdditionalComments,
		feedback.createDate as feedbackCreateDate

	from feedback
		inner join oauth.account on feedback.accountId = account.id
		inner join dict.pharmacySetting on account.pharmacySettingId = pharmacySetting.id
		inner join dict.specialty on account.specialtyId = specialty.id
		inner join dict.province on account.provinceId = province.id
		
	where 
			(( @accountFirstName is null) or( @accountFirstName is not null and @accountFirstName = account.firstName )) and
			(( @accountLastName is null) or ( @accountLastName is not null and @accountLastName = account.lastName )) and
			(( @accountPharmacistLicense is null) or ( @accountPharmacistLicense is not null and @accountPharmacistLicense = account.pharmacistLicense )) and
			(( @accountPharmacySettingId is null) or ( @accountPharmacySettingId is not null and @accountPharmacySettingId = account.pharmacySettingId )) and
			(( @accountProvinceId is null) or ( @accountProvinceId is not null and @accountProvinceId = account.provinceId )) and
			(( @accountEmail is null) or ( @accountEmail is not null and @accountEmail = account.email )) and
			(( @accountCity is null) or ( @accountCity is not null and @accountCity = account.city )) and
			--(( @accountIsActivated is null) or ( @accountIsActivated is not null and @accountIsActivated = account.isActivated )) and
			(( @accountIsOptin is null) or ( @accountIsOptin is not null and @accountIsOptin = account.isOptin )) and
			(( @accountFrom is null) or ( @accountFrom is not null and @accountFrom <= account.createDate )) and
			(( @accountTo is null) or ( @accountTo is not null and @accountTo >= account.createDate ))

	order by account.createDate

end

--test zone
/* 

	exec test.vFeedbackReport
		@accountFirstName = null,
		@accountLastName = null,
		@accountPharmacistLicense = null,
		@accountPharmacySettingId = null,
		@accountProvinceId = null,
		@accountEmail = null,
		@accountCity = null,
		--@accountIsActivated = null,
		@accountIsOptin = null,
		@accountFrom = null,
		@accountTo = null

*/