CREATE PROCEDURE [dbo].[Upd_User]
	@UserName VARCHAR(50),
	@Email VARCHAR(150)
AS
BEGIN
  UPDATE [Users]
   SET [UserName] = @UserName
   WHERE [Email] LIKE '%' + @Email + '%';
  -- Same comment, select top(10) only for more faster the tests.
  SELECT TOP(10) * FROM [Users];
END