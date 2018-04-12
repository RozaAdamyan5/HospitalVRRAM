GO
 Create procedure sp_AddMedecine  
 (@Name  			NVARCHAR(20) ,
 @Country			NVARCHAR(20), 
 @ExpirationDate	DATETIME	,
 @Price				SMALLMONEY,
 @Picture			VARBINARY(MAX) )
 AS
 BEGIN
   insert into Medicine([Name], Country, ExpirationDate, Price, Picture)
   values (@Name ,@Country	,@ExpirationDate, @Price, @Picture)
 END					

GO
Create procedure sp_DeleteDoctor 
(   @IDDoctor VARCHAR(20) )
 AS
BEGIN
  DELETE FROM  Doctor
  WHERE PassportID = @IDDoctor
END


GO
 Create procedure sp_UpdateMedicine  
 @Name			NVARCHAR(20),
 @NewPrice      SMALLMONEY
 AS
 BEGIN
	UPDATE Medicine
	SET Price = @NewPrice
	WHERE Name = @Name
 END	

 GO
 Create procedure sp_AddDoctor 
	@PassportID			CHAR(9)			,
	@Name   			NVARCHAR(20)	,
	@Surname			NVARCHAR(20) 	,
	@Balance			SMALLMONEY 		,
	@Picture			VARBINARY(MAX)	,
	@PhoneNumber		CHAR(9)		    ,
	@Speciality 		TINYINT 		,
	@DateOfApproval		DATETIME		,
	@Login		        VARCHAR(20)      ,
	@Password		    VARCHAR(20)     ,
  @ConsultationCost     SMALLMONEY      
 AS
 BEGIN
   insert into Doctor( PassportID, [Name], Surname, Balance, Picture, PhoneNumber,
					 Speciality, DateOfApproval, [Login], [Password], ConsultationCost)
   values (@PassportID, @Name, @Surname, @Balance, @Picture, @PhoneNumber, @Speciality, 
 						@DateOfApproval, @Login, @Password, @ConsultationCost)
 END		

GO
 Create procedure sp_AllDoctors
 AS
 BEGIN
	SELECT *
	FROM Doctor
 END		
GO