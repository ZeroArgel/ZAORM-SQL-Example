namespace SQLExamples.MVC.Controllers
{
    #region Usings.
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ZAORM.SQL;
    using static ZAORM.ZAEnum;
    #endregion
    public class RolesController : Controller
    {
        private readonly ZADB ZADB_;
        private readonly VMRoles vMRoles_;
        public RolesController(IConfiguration _configuration)
        {
            ZADB_ = new ZADB(_configuration.GetConnectionString("ZAExampleDB"));
            vMRoles_ = new VMRoles();
        }
        #region Index
        [HttpGet]
        public IActionResult Index()
        {   
            // Get all with T-SQL.
            vMRoles_.GetAll = ZADB_.Post<IEnumerable<Roles>>(new ZABase("SELECT [RoleId], [RoleName], [Available] FROM [Roles] ORDER BY [RoleName] DESC;", AllCmdType.TSql));

            // Get all roles by stored procedure.
            var zABase = new ZABase("[dbo].[Get_AllRoles]", AllCmdType.SP);
            vMRoles_.GetBySP = ZADB_.Post<IEnumerable<Roles>>(zABase);

            // Get only row with T-SQL.
            var role = vMRoles_.GetAll.FirstOrDefault();
            if (role != null)
            {
                zABase = new ZABase($"SELECT [RoleId], [RoleName], [Available] FROM [Roles] WHERE [RoleId] = '{role.RoleId}' ORDER BY [RoleName] DESC;", AllCmdType.TSql);
                vMRoles_.GetWithWhere = ZADB_.Post<IEnumerable<Roles>>(zABase).FirstOrDefault();
            } else vMRoles_.GetWithWhere = new Roles();
            return View(vMRoles_);
        }
        #endregion
        #region Add
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Add(Roles roles)
        {
            // Add new role with T-SQL
            var cmd = $"INSERT INTO [Roles]([RoleName]) VALUES('{roles.RoleName}');" +
                      $"SELECT [RoleId] FROM [Roles] WHERE [RoleName] = '{roles.RoleName}' ORDER BY [RoleName] DESC;";
            var zABase = new ZABase(cmd, AllCmdType.TSql);
            TempData["LastRoleIdAdded"] = ZADB_.Post<IEnumerable<Roles>>(zABase).FirstOrDefault()?.RoleId;
            TempData["IsSuccess"] = TempData["LastRoleIdAdded"] != null;
            return RedirectToAction("Index");
        }
        #endregion
        #region  AddSP
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddSP(Roles roles)
        {
            var zAParam = new ZAParam("RoleName", $"{roles.RoleName}", AllSQLType.VarChar, 150);
            var zABase = new ZABase("[dbo].[Add_Role]", AllCmdType.SP, zAParam);
            TempData["LastRoleIdAdded"] = ZADB_.Post<IEnumerable<Roles>>(zABase).FirstOrDefault()?.RoleId;
            TempData["IsSuccess"] = TempData["LastRoleIdAdded"] != null;
            return RedirectToAction("Index");
        }
        #endregion
        #region Upd
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Upd(Roles roles)
        {
            var cmd = $"UPDATE [Roles] SET [RoleName] = '{roles.RoleName}', [Available] = '{roles.Available}' WHERE [RoleId] = '{roles.RoleId}';"+
                      $"SELECT [RoleId] FROM [Roles] WHERE [RoleId] = '{roles.RoleId}' ORDER BY [RoleName] DESC;";
            var zABase = new ZABase(cmd, AllCmdType.TSql);
            TempData["LastRoleIdAdded"] = ZADB_.Post<IEnumerable<Roles>>(zABase).FirstOrDefault()?.RoleId;
            TempData["IsSuccess"] = TempData["LastRoleIdAdded"] != null;
            return RedirectToAction("Index");
        }
        #endregion
        #region UpdSP
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult UpdSP(Roles roles)
        {
            var zAParam = new List<ZAParam>() {
                new ZAParam("RoleId", roles.RoleId, AllSQLType.Guid),
                new ZAParam("RoleName", roles.RoleName, AllSQLType.VarChar, 150),
                new ZAParam("Available", roles.Available, AllSQLType.Bit)
            };
            var zABase = new ZABase("[dbo].[Upd_Role]", AllCmdType.SP, zAParam);
            TempData["LastRoleIdAdded"] = ZADB_.Post<IEnumerable<Roles>>(zABase).FirstOrDefault()?.RoleId;
            TempData["IsSuccess"] = TempData["LastRoleIdAdded"] != null;
            return RedirectToAction("Index");
        }
        #endregion
        #region Rmv
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Rmv(Roles roles)
        {
            var cmd = $"DELETE FROM [Roles] WHERE [RoleId] = '{roles.RoleId}';" +
                      $"SELECT [RoleId] FROM [Roles] WHERE [RoleId] = '{roles.RoleId}' ORDER BY [RoleName] DESC;";
            var zABase = new ZABase(cmd, AllCmdType.TSql);
            TempData["LastRoleIdAdded"] = ZADB_.Post<IEnumerable<Roles>>(zABase).FirstOrDefault()?.RoleId;
            TempData["IsSuccess"] = TempData["LastRoleIdAdded"] == null;
            return RedirectToAction("Index");
        }
        #endregion
        #region RmvSP
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult RmvSP(Roles roles)
        {
            var zAParam = new ZAParam("RoleId", roles.RoleId, AllSQLType.Guid);
            var zABase = new ZABase("[dbo].[Rmv_Role]", AllCmdType.SP, zAParam);
            TempData["LastRoleIdAdded"] = ZADB_.Post<IEnumerable<Roles>>(zABase).FirstOrDefault()?.RoleId;
            TempData["IsSuccess"] = TempData["LastRoleIdAdded"] == null;
            return RedirectToAction("Index");
        }
        #endregion
    }
}