--CREATE DATABASE Vrram

--use Vrram 

---------------------------------------------------------------------------------------------

--CREATE TABLE Doctor(
--	PassportID			CHAR(9)			PRIMARY KEY CHECK(PassportID LIKE '[0-9]*'),
--	[Name] 				NVARCHAR(20)	NOT NULL,
--	Surname			    NVARCHAR(20) 	NOT NULL,
--	Balance			    SMALLMONEY,
--	Picture			    VARBINARY,
--	PhoneNumber		    CHAR(9),
--	Speciality 		    TINYINT 		NOT NULL,
--	DateOfApproval		DATETIME,
--	[Login]		        varchar(8)   UNIQUE NOT NULL,
--	[Password]		    varchar(20)       NOT NULL,
--  ConsultationCost    SMALLMONEY       NOT NULL,
--);


---------------------------------------------------------------------------------------------

--CREATE TABLE Patient(	
--	PassportID		CHAR(9)	PRIMARY KEY CHECK(PassportID LIKE '[0-9]*'),
--	[Name]			NVARCHAR(20) 	NOT NULL,
--	Surname 		NVARCHAR(20) 	NOT NULL,
--	Balance		    SMALLMONEY,
--	Picture		    VARBINARY,
--	PhoneNumber	    CHAR(9),
--	[Address]	    NVARCHAR(20),
--	DateOfBirth	    DATETIME,
--	InsuranceCard	CHAR(9)  		UNIQUE,
--	[Login]		    varchar(8)      UNIQUE NOT NULL,
--	[Password]		varchar(20)      NOT NULL,
--);

---------------------------------------------------------------------------------------------

--CREATE TABLE Diagnoses
--(
--	DiagnosesID		INT			    PRIMARY KEY IDENTITY(10000, 1),
--	[Description]	NVARCHAR(20) 	NOT NULL,
--	DateOfDiagnosis	DATETIME		NOT NULL,
--	PatientID 		char(9)			FOREIGN KEY REFERENCES Patient(PassportID),
--	DoctorID 		char(9)			FOREIGN KEY REFERENCES Doctor(PassportID),
--);	


---------------------------------------------------------------------------------------------

--CREATE TABLE Medicine 
--(	
--  [Name] 			NVARCHAR(20) 	PRIMARY KEY,
--	Country			NVARCHAR(20) 	NOT NULL,
--	ExpirationDate	DATETIME		NOT NULL,
--	Price			SMALLMONEY		NOT NULL,
--	Picture			VARBINARY,
--);

---------------------------------------------------------------------------------------------

--CREATE TABLE Queues
--(
--	DocID			char(9)		NOT NULL	FOREIGN KEY REFERENCES Doctor(PassportID),
--	PatID			char(9)		NOT NULL	FOREIGN KEY REFERENCES Patient(PassportID),
--	[Time]			DATETIME	NOT NULL,
--	CostOfConsult 	SMALLMONEY  NOT NULL,

--	CONSTRAINT 	PK_Queue	PRIMARY KEY (DocID,PatID,[Time]),
--);

---------------------------------------------------------------------------------------------

--CREATE TABLE AssignedTo
--(
--	DiagnoseID		INT		NOT NULL	FOREIGN KEY REFERENCES Diagnoses(DiagnosesID),
--	MedicineID		INT		FOREIGN KEY REFERENCES Medicine(ID),
--  Count			INT		NOT NULL   CHECK >1 , ---please 
     
--	CONSTRAINT 	PK_Assign	PRIMARY KEY (DiagnoseID,MedicineID),
--);
-------------------------------------ARMINE---------------------------------------------
 --Create procedure sp_AddMedecine  
 --@[Name]  		NVARCHAR    ,
 --@Country			NVARCHAR, 
 --@ExpirationDate	DATETIME	,
 --@Price		    SMALLMONEY,
 --@Picture			VARBINARY 
 --AS
 --BEGIN
 --  insert into Medicine([Name] ,Country	,ExpirationDate	,Price,Picture)
 --  values (@[Name] ,@Country	,@ExpirationDate,@Price,@Picture)
 --END					
------------------------------------ARMINE----------------------------------------------------
--Create procedure sp_DeleteDoctor 
--(   @IDDoctor int )
-- AS
--BEGIN
--  DELETE FROM  Doctor
--  WHERE PassportID = @IDDoctor
--END
------------------------------------ARMINE----------------------------------------------------
 --Create procedure sp_AddMedecine  
 --@Name			NVARCHAR  ,
 --@NewPrice      SMALLMONEY
 --AS
 --BEGIN
	--UPDATE Medicine
	--SET Price = @NewPrice
	--WHERE Name = @Name
 --END	
------------------------------------ARMINE----------------------------------------------------
 --Create procedure sp_AddDoctor 
--	@PassportID			CHAR(9)			,
--	@[Name] 			NVARCHAR(20)	,
--	@Surname			NVARCHAR(20) 	,
--	@Balance			SMALLMONEY,		,
--	@Picture			VARBINARY,		,
--	@PhoneNumber		CHAR(9),		,
--	@Speciality 		TINYINT 		,
--	@DateOfApproval		DATETIME,		,
--	@[Login]		    varchar(8)      ,
--	@[Password]		    varchar(20)     ,
--  @ConsultationCost   SMALLMONEY      ,
 --AS
 --BEGIN
 --  insert into Doctor( PassportID, [Name], Surname, Balance, Picture, PhoneNumber,
--					 Speciality, DateOfApproval, [Login], [Password], ConsultationCost)
 --  values (@PassportID, @[Name], @Surname, @Balance, @Picture, @PhoneNumber, @Speciality, 
 --						@DateOfApproval, @[Login], @[Password], @ConsultationCost)
 --END		
------------------------------------ARMINE----------------------------------------------------
 --Create procedure sp_AllDoctors
 --AS
 --BEGIN
	--SELECT *
	--FROM Doctor
 --END		
------------------------------------ARMINE----------------------------------------------------
------------------------------------MANE----------------------------------------------------
--CREATE PROC FindPatientPasspordID
--@passpordID varchar(20)
--AS
--BEGIN
--	IF(exists (select *
--				from Patient
--				where PassportID = @passpordID))
--	BEGIN 
--	RETURN 1
--	END

--	ELSE
--	BEGIN
--	RETURN 0
--	END
-- END
------------------------------------MANE----------------------------------------------------
--CREATE PROC FindDoctorPasspordID
--@passpordID varchar(20)
--AS
--BEGIN
--	IF(exists (select *
--				from Doctor
--				where PassportID = @passpordID))
--	BEGIN 
--	RETURN 1
--	END

--	ELSE
--	BEGIN
--	RETURN 0
--	END
--END
------------------------------------MANE----------------------------------------------------
--CREATE PROC FindPatientLogin
--@login varchar(20)
--AS
--BEGIN
--	IF(exists (select *
--				from Patient
--				where Login = @login))
--	BEGIN 
--	RETURN 1
--	END

--	ELSE
--	BEGIN
--	RETURN 0
--	END
--END
------------------------------------MANE----------------------------------------------------

--CREATE PROC FindDoctortLogin
--@login varchar(20)
--AS
--BEGIN
--	IF(exists (select *
--				from Doctor
--				where Login = @login))
--	BEGIN 
--	RETURN 1
--	END

--	ELSE
--	BEGIN
--	RETURN 0
--	END
--END
------------------------------------MANE----------------------------------------------------
