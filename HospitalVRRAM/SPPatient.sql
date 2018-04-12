create proc dbo.insertPatient(@Name nvarchar(20), @Surname nvarchar(20), @PassportID char(9), @Login varchar(20),
							  @Password varchar(20), @InsuranceCard char(9), @Address nvarchar(20), @DateOfBirth datetime, @PhoneNumber char(9))
as
begin
	insert into Patient values(@PassportID, @Name, @Surname, 0, 0, @PhoneNumber, @Address,
							   @DateOfBirth, @InsuranceCard, @Login, @Password)
end
go

create proc dbo.SignInPatient(@Login varchar(20), @Password varchar(20))
as
begin
	select * from Patient
	where [Password]=@Password and [Login]=@Login
end
go		

create proc dbo.ReadMyHistory(@PassportID char(9)) 
as
begin
	select distinct DiagnosesID, [Description], DateOfDiagnosis
	from Diagnoses
	where PatientID=@PassportID
end
go

create proc dbo.Drugs(@DiagnosisID int)	
as 
begin
	select [Name], Country, Price, ExpirationDate
	from Medicine
	join  AssignedTo on [Name] = AssignedTo.Medicine
	where DiagnoseID = @DiagnosisID
end
go			   

create proc dbo.changeBalance(@PassportID char(9), @Balance smallmoney)
as
begin
	update Patient
	Set Balance = @Balance
	where PassportID = @PassportID
end
go 

create proc dbo.FindDoctorBySpeciality(@Speciality varchar(20) )
as
begin
	select *
	from Doctor
	where Speciality = @Speciality
end
go

create proc dbo.ChangePatientAddress(@PassportID char(9), @Address nvarchar(20))
as
begin
	update Patient
	Set [Address] = @Address
	where PassportID = @PassportID
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