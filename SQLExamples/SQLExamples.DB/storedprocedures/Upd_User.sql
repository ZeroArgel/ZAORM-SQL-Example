CREATE PROCEDURE [dbo].[Upd_User]
	@UserName VARCHAR(50),
	@Email VARCHAR(150)
AS
BEGIN
  UPDATE [Users]
   SET [UserName] = @UserName
   WHERE [Email] LIKE '%' + @Email + '%';
END