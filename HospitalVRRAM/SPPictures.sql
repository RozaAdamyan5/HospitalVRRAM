create proc dbo.PatientAddPicture
@PassportID char(9),
@Pic varbinary(max)
as
begin
update Patient
set Picture = @pic
where PassportID = @PassportID
end
go

create proc dbo.DoctorAddPicture
@PassportID char(9),
@Pic varbinary(max)
as
begin
update Doctor
set Picture = @pic
where PassportID = @PassportID
end
go

create proc dbo.MedicineAddPicture
@Name nvarchar(20),
@Pic varbinary(max)
as
begin
update Medicine
set Picture = @pic
where Name = @Name
end
go
