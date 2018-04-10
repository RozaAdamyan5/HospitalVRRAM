create proc dbo.insertPatient(@Name nvarchar(20),@Surname nvarchar(20),@PassportID char(9),@Login varchar(8),
							  @Password varchar(8),@InsuranceCard char(9),@Address nvarchar(20),@DateOfBirth datetime)
as
begin
	insert into Patient values(@PassportID,@Name,@Surname,0,0,0,@Address,
							   @DateOfBirth,@InsuranceCard,@Login,@Password)
end
go

create proc dbo.SignUpPatient(@Login varchar(8),@Password varchar(8))
as
begin
	select * from Patient
	where [Password]=@Password and [Login]=@Login
end
go		

create proc dbo.ReadMyHistory(@PassportID char(9)) 
as
begin
	select distinct DiagnosesID,[Description], DateOfDiagnosis
	from Diagnoses
	where PatientID=@PassportID
end
go

create proc dbo.Drugs(@DiagnosisID int)	
as 
begin
	select Name,Country,Price,ExpirationDate
	from Medicine
	join  AssignedTo on MedicineID = ID
	where DiagnoseID = @DiagnosisID
end
go			   

create proc dbo.changeBalance(@PassportID char(9),@Balance smallmoney)
as
begin
	update Patient
	Set Balance = @Balance
	where PassportID = @PassportID
end
go 

create proc dbo.FindDoctorBySpeciality(@Speciality tinyint /*should be varchar*/)
as
begin
	select *
	from Doctor
	where Speciality = @Speciality
end
go

create proc dbo.ChangePatientAddress(@PassportID char(9),@Address nvarchar(20))
as
begin
	update Patient
	Set [Address] = @Address
	where PassportID = @PassportID
end
go 