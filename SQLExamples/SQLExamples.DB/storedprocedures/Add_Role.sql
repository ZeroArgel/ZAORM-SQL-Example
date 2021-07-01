CREATE PROCEDURE [dbo].[Add_Role]
	@RoleName VARCHAR(150)
AS
BEGIN
  INSERT INTO [Roles]([RoleName])
   VALUES(@RoleName);
END