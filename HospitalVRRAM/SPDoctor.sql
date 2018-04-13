create proc dbo.GetDiagnose(@patId char(9))
as 
begin 
 Select DiagnoseID,[Description],DateOfDiagnosis
        From Diagnoses
        Where patientID=@patID
end

go

create proc dbo.GetDiagnoseMedicine(@patId char(9))
as 
begin 
	Select Medicine.[Name], Medicine.Country, Medicine.Price, Medicine.ExpiryDate 
		From AssignedTo
			join Medicine on AssignedTo.MedicineID = Medicine.Name 
            join Diagnosis on Diagnosis.DiagnoseID = AssignedTo.DiagnoseID
         Where patientID=@patID
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
	insert into AssignedTo values(@diagnoseID, @medicineID, @cnt)
end
go

create proc dbo.ReadMyHistory(@PassportID char(9))
as
begin
	select [Description], DateOfDiagnosis, DiagnosesID
	from Diagnoses
	where PatientID = @PassportID
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
--create proc dbo.ShowBalance(@patId char(9))
--as 
--begin 
-- Select balance
--        From Doctor
--        Where passportID=@patID
--end



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