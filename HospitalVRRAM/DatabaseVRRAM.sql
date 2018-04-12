drop database Vrram

CREATE DATABASE Vrram

use Vrram 

-------------------------------------------------------------------------------------------

CREATE TABLE Doctor(
	PassportID			CHAR(9)				PRIMARY KEY CHECK(PassportID LIKE '[0-9]*'),
	[Name] 				NVARCHAR(20)		NOT NULL,
	Surname			    NVARCHAR(20) 		NOT NULL,
	Balance			    SMALLMONEY,
	Picture			    VARBINARY(max) filestream null,
	[Id] [uniqueidentifier] ROWGUIDCOL NOT NULL UNIQUE, 
	PhoneNumber		    CHAR(9),
	Speciality 		    VARCHAR(20)			NOT NULL,
	DateOfApproval		DATETIME,
	DateOfBirth	        DATETIME,
	[Login]		        varchar(20)			UNIQUE NOT NULL,
	[Password]		    varchar(20)			NOT NULL,
	ConsultationCost    SMALLMONEY			NOT NULL,
);


-------------------------------------------------------------------------------------------

CREATE TABLE Patient(	
	PassportID		CHAR(9)				PRIMARY KEY CHECK(PassportID LIKE '[0-9]*'),
	[Name]			NVARCHAR(20) 		NOT NULL,
	Surname 		NVARCHAR(20) 		NOT NULL,
	Balance		    SMALLMONEY,
	Picture		    VARBINARY(max) filestream null,
	[Id] [uniqueidentifier] ROWGUIDCOL NOT NULL UNIQUE, 
	PhoneNumber	    CHAR(9),
	[Address]	    NVARCHAR(20),
	DateOfBirth	    DATETIME,
	InsuranceCard	CHAR(9)  			UNIQUE,
	[Login]		    varchar(20)			UNIQUE NOT NULL,
	[Password]		varchar(20)			NOT NULL,
);

-------------------------------------------------------------------------------------------

CREATE TABLE Diagnoses
(
	DiagnosesID		INT			    PRIMARY KEY IDENTITY(10000, 1),
	[Description]	NVARCHAR(128) 	NOT NULL,
	DateOfDiagnosis	DATETIME		NOT NULL,
	PatientID 		char(9)			FOREIGN KEY REFERENCES Patient(PassportID),
	DoctorID 		char(9)			FOREIGN KEY REFERENCES Doctor(PassportID),
);	


-------------------------------------------------------------------------------------------

CREATE TABLE Medicine 
(	
  [Name] 			NVARCHAR(20) 	PRIMARY KEY,
	Country			NVARCHAR(20) 	NOT NULL,
	ExpirationDate	DATETIME		NOT NULL,
	Price			SMALLMONEY		NOT NULL,
	Picture			VARBINARY(max) filestream null,
	[Id] [uniqueidentifier] ROWGUIDCOL NOT NULL UNIQUE, 
);

-------------------------------------------------------------------------------------------

CREATE TABLE Queues
(
	DocID			char(9)		NOT NULL	FOREIGN KEY REFERENCES Doctor(PassportID),
	PatID			char(9)		NOT NULL	FOREIGN KEY REFERENCES Patient(PassportID),
	[Time]			DATETIME	NOT NULL,
	CostOfConsult 	SMALLMONEY  NOT NULL,

	CONSTRAINT 	PK_Queue	PRIMARY KEY (DocID,PatID,[Time]),
);

-------------------------------------------------------------------------------------------

CREATE TABLE AssignedTo
(
	DiagnoseID		INT		NOT NULL	FOREIGN KEY REFERENCES Diagnoses(DiagnosesID),
	MedicineID		Nvarchar(20)		FOREIGN KEY REFERENCES Medicine([Name]),
    [Count]			INT		NOT NULL   CHECK  ([Count]>1) ,
     
	CONSTRAINT 	PK_Assign	PRIMARY KEY (DiagnoseID,MedicineID),
);

