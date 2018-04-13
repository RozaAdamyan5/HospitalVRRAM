GO
 Create procedure sp_AddMedecine  
 (@Name  			NVARCHAR(20),
 @Country			NVARCHAR(20), 
 @ExpirationDate	DATETIME,
 @Price				SMALLMONEY
 )
 AS
 BEGIN
   insert into Medicine([Name], Country, ExpirationDate, Price)
   values (@Name, @Country, @ExpirationDate, @Price)
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
 CREATE PROCEDURE sp_DeleteMedicine
 @Name		NVARCHAR(20)
 AS
 BEGIN
	DELETE FROM Medicine
	WHERE [Name] = @Name

 END

 GO

 Create procedure sp_AllDoctors
 AS
 BEGIN
	SELECT *
	FROM Doctor
 END		
GO

 Create procedure sp_AllMedicine
 AS
 BEGIN
	SELECT *
	FROM Medicine
 END		
GO