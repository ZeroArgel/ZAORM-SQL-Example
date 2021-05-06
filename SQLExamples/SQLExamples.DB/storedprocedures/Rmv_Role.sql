CREATE PROCEDURE [dbo].[Rmv_Role]
	@RoleId UNIQUEIDENTIFIER
AS
BEGIN
  DELETE FROM [Roles] WHERE [RoleId] = @RoleId;

  SELECT [RoleId] 
   FROM [Roles] 
   WHERE [RoleId] = @RoleId 
   ORDER BY [RoleName] DESC;
END