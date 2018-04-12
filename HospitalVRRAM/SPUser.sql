CREATE PROC dbo.FindPatientPasspordID
@passpordID char(9)
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
@passpordID char(9)
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





