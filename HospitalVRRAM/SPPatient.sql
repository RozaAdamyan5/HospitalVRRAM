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

create proc dbo.FindDoctorBySpeciality(@Speciality varchar(28) )
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

CREATE PROC dbo.FindPatientPasspordID
@passpordID varchar(20)
AS
BEGIN
	IF(exists (select *
				from Patient
				where PassportID = @passpordID))
	BEGIN 
	RETURN 1
	END

	ELSE
	BEGIN
	RETURN 0
	END
END
go

CREATE PROC dbo.FindDoctorPasspordID
@passpordID varchar(20)
AS
BEGIN
	IF(exists (select *
				from Doctor
				where PassportID = @passpordID))
	BEGIN 
	RETURN 1
	END

	ELSE
	BEGIN
	RETURN 0
	END
END
go

CREATE PROC dbo.FindPatientLogin
@login varchar(20)
AS
BEGIN
	IF(exists (select *
				from Patient
				where Login = @login))
	BEGIN 
	RETURN 1
	END

	ELSE
	BEGIN
	RETURN 0
	END
END
go

CREATE PROC dbo.FindDoctortLogin
@login varchar(20)
AS
BEGIN
	IF(exists (select *
				from Doctor
				where Login = @login))
	BEGIN 
	RETURN 1
	END

	ELSE
	BEGIN
	RETURN 0
	END
END
go

create proc dbo.sp_WriteDiagnosInDiagnoses(@description nvarchar(20), @dateOfDiagnoses datetime,
										   @patientID char(9), @DoctorID char(9))
as
begin
	insert into Diagnoses values(@description, @dateOfDiagnoses, @patientID, @DoctorID)
end
go

create proc dbo.sp_AddMedicineInAssignedTo(@diagnoseID int, @medicineID int)
as
begin
	insert into AssignedTo values(@diagnoseID, @medicineID)
end
go

create proc dbo.ReadMyHistory(@PassportID int)
as
begin
	select [Description], DateOfDiagnosis, DiagnosesID
	from Diagnoses
	where PatientID = @PassportID
end
go





