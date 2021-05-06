CREATE PROCEDURE [dbo].[Upd_Role]
	@RoleId UNIQUEIDENTIFIER,
	@RoleName VARCHAR(50),
	@Available VARCHAR(150)
AS
BEGIN
  UPDATE [Roles]
   SET [RoleName] = @RoleName,
       [Available] = @Available
   WHERE [RoleId] = @RoleId;

  SELECT [RoleId] 
   FROM [Roles] 
   WHERE [RoleName] = @RoleName 
   ORDER BY [RoleName] DESC;
END