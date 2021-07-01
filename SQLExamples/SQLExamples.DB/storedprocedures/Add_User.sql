CREATE PROCEDURE [dbo].[Add_User]
	@UserName VARCHAR(50),
	@Password VARCHAR(64),
	@NameFull VARCHAR(200),
	@Email VARCHAR(150)
AS
BEGIN
  INSERT INTO [Users]([UserName], [Password], [NameFull], [Email])
   VALUES(@UserName, @Password, @NameFull, @Email);
END