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
--	[Password]		    varchar(8)       NOT NULL,
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
--	[Password]		varchar(8)      NOT NULL,
--);

---------------------------------------------------------------------------------------------

--CREATE TABLE Diagnoses
--(
--	DiagnosesID		INT			PRIMARY KEY IDENTITY(10000, 1),
--	[Description]	NVARCHAR(200) 	NOT NULL,
--	DateOfDiagnosis	DATETIME		NOT NULL,
--	PatientID 		char(9)			FOREIGN KEY REFERENCES Patient(PassportID),
--	DoctorID 		char(9)			FOREIGN KEY REFERENCES Doctor(PassportID),
--);	


---------------------------------------------------------------------------------------------

--CREATE TABLE Medicine 
--(	
--	ID				INT			PRIMARY KEY ,
--    [Name] 			NVARCHAR(20) 	NOT NULL,//it must be unique
--	Country			NVARCHAR(20) 	NOT NULL,
--	ExpirationDate	DATETIME		NOT NULL,
--	Price			SMALLMONEY		NOT NULL,
--	Picture			VARBINARY,
--);

---------------------------------------------------------------------------------------------

--CREATE TABLE Queues
--(
--	DocID			char(9)		NOT NULL	FOREIGN KEY REFERENCES Doctor(PassportID),
--	PatID			char(9)			NOT NULL	FOREIGN KEY REFERENCES Patient(PassportID),
--	[Time]			DATETIME	NOT NULL,
--	CostOfConsult 	SMALLMONEY  NOT NULL,

--	CONSTRAINT 	PK_Queue	PRIMARY KEY (DocID,PatID,[Time]),
--);

---------------------------------------------------------------------------------------------

--CREATE TABLE AssignedTo
--(
--	DiagnoseID		INT		NOT NULL	FOREIGN KEY REFERENCES Diagnoses(DiagnosesID),
--	MedicineID		INT		FOREIGN KEY REFERENCES Medicine(ID),
     
--	CONSTRAINT 	PK_Assign	PRIMARY KEY (DiagnoseID,MedicineID),
--);
----------------------------------------------------------------------------------------------
 --Create procedure sp_AddMedecine  
 --@[ID]				int		,
 --@[Name1] 			NVARCHAR    ,
 --@Country			NVARCHAR, 
 --@ExpirationDate	DATETIME	,
 --@Price			    SMALLMONEY,
 --@Picture			VARBINARY 
 --AS
 --BEGIN
 --  insert into Medicine(ID,[Name] ,Country	,ExpirationDate	,Price,Picture)
 --  values (@[ID] , @[Name] ,@Country	,@ExpirationDate,@Price,@Picture)
 --END					
--chem karoxanum
