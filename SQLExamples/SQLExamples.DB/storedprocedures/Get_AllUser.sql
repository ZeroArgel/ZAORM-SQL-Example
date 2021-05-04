CREATE PROCEDURE [dbo].[Get_AllUser]
  @WithoutPassword BIT = 0
AS
BEGIN
  -- Same comment, select top(10) only for more faster the tests.
  IF @WithoutPassword = '1'
  BEGIN
    SELECT TOP(10) [UserId],[UserName],[NameFull],[Email], [Available] FROM [Users];
  END
  ELSE
  BEGIN
    SELECT TOP(10) * FROM [Users];
  END
END