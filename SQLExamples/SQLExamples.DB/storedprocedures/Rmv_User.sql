CREATE PROCEDURE [dbo].[Rmv_User]
	@UserName VARCHAR(50)
AS
BEGIN
  DELETE FROM [Users] WHERE [UserName] = @UserName;

  SELECT TOP(10) * FROM [Users];
END