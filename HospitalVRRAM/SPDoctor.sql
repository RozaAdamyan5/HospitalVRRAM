create proc dbo.insertDoctor(@Name varchar(20), @Surname varchar(20), @PassportId char(9),
							@Login varchar(20), @Password varchar(20), @DateOfBirth datetime, 
							@Speciality varchar(20), @ConsultationCost smallmoney, @GetEmployed datetime, @PhoneNumber char(9))
as
begin
	insert into Doctor( PassportID, [Name], Surname, Balance, Picture, PhoneNumber,
					 Speciality, DateOfApproval, [Login], [Password], ConsultationCost)
		values(@PassportID, @Name, @Surname, 0, null, @PhoneNumber, @Speciality, @GetEmployed, @DateOfBirth, @Login, @Password, @ConsultationCost)
end

go


create proc dbo.GetDiagnoseMedicine(@patId char(9),@diagnoseID int)
as 
begin 
	Select Medicine.[Name], Medicine.Country, Medicine.Price, Medicine.ExpirationDate,AssignedTo.[Count]
		From AssignedTo
			join Medicine on AssignedTo.Medicine = Medicine.Name 
            join Diagnoses on Diagnoses.DiagnosesID = AssignedTo.DiagnoseID
         Where patientID=@patID and AssignedTo.DiagnoseId=@diagnoseID
end 
go



create proc dbo.sp_WriteDiagnosInDiagnoses(@description nvarchar(20), @dateOfDiagnoses datetime,
										   @patientID char(9), @DoctorID char(9))
as
begin
	insert into Diagnoses values(@description, @dateOfDiagnoses, @patientID, @DoctorID)
end
go

create proc dbo.sp_AddMedicineInAssignedTo(@diagnoseID int, @medicine nvarchar(20), @cnt int)
as
begin
	insert into AssignedTo values(@diagnoseID, @medicine, @cnt)
end
go


create proc dbo.GetDiagnose(@PassportID char(9))
as 
begin 
 Select DiagnosesID,[Description],DateOfDiagnosis
        From Diagnoses
        Where patientID=@PassportID
end

go

create proc dbo.SignInDoctor(@login varchar(20), @password varchar(20))
as 
begin 
	Select *
	From Doctor
	Where [Login]=@login and [Password]=@password 
end 
go




create proc dbo.ShowDoctorQueue(@PassportID int)
as
begin
	select [Time],(SELECT PassportID,name,Surname,Address,DateOfBirth,InsuranceCard
					FROM  Patient
					WHERE Patient.PassportID = Queues.PatID )
	from Queues
	where DocID = @PassportID
end
go


 Create procedure AddPatientToQueue  
 (@DocID	CHAR(9)		 ,
  @PatID	CHAR(9)		, 
  @Date		DATETIME	)
 AS
 BEGIN
   insert into Queues(DocID ,PatID	, Time	,CostOfConsult)
   values (@DocID, @PatID, @Date, (SELECT Doctor.ConsultationCost
									FROM Doctor
									where Doctor.PassportID = @DocID))
 END					
GO

create proc dbo.ChangeDoctorPassword(@PassportID char(9), @Password varchar(20))
as
begin
update Doctor
Set [Password] = @Password
where PassportID = @PassportID
end
go

create proc dbo.sp_GetDiagnoseID
AS begin
	Select Max(DiagnosesID)
	From Diagnoses
end 
	go
go

create proc dbo.LoadMedicineByName(@Name varchar(20))
as
begin
select * from Medicine
where [Name] = @Name
end
go

create proc dbo.LoadMedicineNames
as
begin
	select [Name]
	from Medicine
end