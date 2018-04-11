create proc dbo.GetDiagnose(@patId char(9))
as 
begin 
 Select [Description],DateOfDiagnosis
        From Diagnoses
        Where patientID=@patID
end

go

create proc dbo.GetDiagnoseMedicine(@patId char(9))
as 
begin 
	Select Medicine.name,Medicine.country,Medicine.price,Medicine.ExpiryDate 
		From AssignedTo
			join Medicine on AssignedTo.MedicineID=Medicine.MedicineID 
            join Diagnosis on Diagnosis.DiagnoseID=AssignedTo.DiagnoseID
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

--create proc dbo.ShowBalance(@patId char(9))
--as 
--begin 
-- Select [Description],DateOfDiagnosis
--        From Diagnoses
--        Where patientID=@patID
--end

