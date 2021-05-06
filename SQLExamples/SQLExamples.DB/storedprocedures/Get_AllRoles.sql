CREATE PROCEDURE [dbo].[Get_AllRoles]
AS
BEGIN
  SELECT
     [RoleId],
     [RoleName],
     [Available]
    FROM [Roles]
    WHERE [Available] = 1
    ORDER BY [RoleName] DESC;
END