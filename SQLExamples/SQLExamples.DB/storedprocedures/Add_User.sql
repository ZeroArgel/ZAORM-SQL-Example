CREATE PROCEDURE [dbo].[Add_User]
	@UserName VARCHAR(50),
	@Password VARCHAR(64),
	@NameFull VARCHAR(200),
	@Email VARCHAR(150)
AS
BEGIN
  INSERT INTO [Users]([UserName], [Password], [NameFull], [Email])
   VALUES(@UserName, @Password, @NameFull, @Email);
  -- Same comment, select top(10) only for more faster the tests.
  SELECT TOP(10) * FROM [Users];
END