CREATE PROCEDURE [dbo].[Add_Role]
	@RoleName VARCHAR(150)
AS
BEGIN
  INSERT INTO [Roles]([RoleName])
   VALUES(@RoleName);

  SELECT [RoleId] 
   FROM [Roles] 
   WHERE [RoleName] = @RoleName 
   ORDER BY [RoleName] DESC;
END