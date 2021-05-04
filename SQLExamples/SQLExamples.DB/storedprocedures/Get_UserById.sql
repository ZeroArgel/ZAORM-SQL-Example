CREATE PROCEDURE [dbo].[Get_UserById]
  @UserId UNIQUEIDENTIFIER,
  @WithoutPassword BIT = 0
AS
BEGIN
  IF @WithoutPassword = '1'
  BEGIN
    SELECT TOP(10) [UserId],[UserName],[NameFull],[Email], [Available] 
     FROM [Users] 
     WHERE [UserId] = @UserId;
  END
  ELSE
  BEGIN
    SELECT TOP(10) * FROM [Users] WHERE [UserId] = @UserId;;
  END
END