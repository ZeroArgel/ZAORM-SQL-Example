namespace SQLExamples
{
    #region Usings
    using Models;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Configuration;
    using ZAExtensions; // Other project with functions that repeat much when programming.
    using ZAORM.SQL; // Functions of SQL from ZAORM.
    using static ZAORM.ZAEnum; // Only for short declaration the enums.
    #endregion

    public class Program
    {
        /// <summary>
        /// Hello everyone, this project is for show all function into and its use of ZAORM.SQL.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Only for Console Project.
            var connectionString = ConfigurationManager.ConnectionStrings["ZAExampleDB"].ConnectionString;

            // create connection
            var db = new ZADB(connectionString);

            // All examples for TSQL.
            OnlyTSQL(db);

            // All examples for SP or Stored Procedure.
            OnlySP(db);
        }
        private static void OnlyTSQL(ZADB _db, AllCmdType _cmdType = AllCmdType.TSql)
        {
            #region Insert and show field.
            // You able to put all into ZADBBase field but is more easier to read for separate
            var password = "Test".ToHash();
            var cmd = "INSERT INTO [Users]([UserName],[Password],[NameFull],[Email]) VALUES('Test', '" + password + "', 'Test ZAORM', 'test@hotmail.com');";
            var zABase = new ZABase(cmd, _cmdType);
            //var userInsert = _db.Post(zABase);
            //ShowData("user Insert - TSQL\nResult:", userInsert); // Show all data.
            #endregion
            #region Select * or all fields.
            // I put top(10) for make the test more faster.
            zABase = new ZABase("SELECT TOP(10) [UserId],[UserName],[Password],[NameFull],[Email],[Available] FROM [Users]", _cmdType);
            var userSelectAll = _db.Post<IEnumerable<Users>>(zABase);
            ShowData("userSelectAll - TSQL", userSelectAll); // Show all data.
            #endregion
            #region Select with some fields.
            // I put top(10) for make the test more faster.
            zABase = new ZABase("SELECT TOP(10) [UserId],[UserName],[NameFull],[Email] FROM [Users] WHERE [Available] = 1", _cmdType);
            var userSelectExact = _db.Post<IEnumerable<Users>>(zABase);
            ShowData("userSelectExact - TSQL", userSelectExact); // Show all data.
            #endregion
            #region update and show field.
            // You able to put all into ZADBBase field but is more easier to read for separate
            cmd = "UPDATE [Users] SET [UserName] = 'test1' WHERE [UserName] = 'Test'";
            zABase = new ZABase(cmd, _cmdType);
            var userUpdate = _db.Post(zABase);
            ShowData("user Update - TSQL\nResult:", userUpdate); // Show all data.
            #endregion
            #region Delete and show field.
            // You able to put all into ZADBBase field but is more easier to read for separate
            cmd = "DELETE FROM [Users] WHERE [UserName] = 'test1'";
            zABase = new ZABase(cmd, _cmdType);
            var userDelete = _db.Post(zABase);
            ShowData("userDelete - TSQL", userDelete); // Show all data.
            #endregion
        }
        private static void OnlySP(ZADB _db, AllCmdType _cmdType = AllCmdType.SP)
        {
            #region Insert and show field.
            // You able to put all into ZADBBase field but is more easier to read for separate
            object zAParam = new List<ZAParam>()
            {
                new ZAParam("UserName", "Test", AllSQLType.VarChar, 50),
                new ZAParam("Password", "Test".ToHash(), AllSQLType.VarChar, 64),
                new ZAParam("NameFull", "Test", AllSQLType.VarChar, 200),
                new ZAParam("Email", "Test@hotmail.com", AllSQLType.VarChar, 150)
            };
            var zABase = new ZABase("[Add_User]", _cmdType, zAParam);
            var userInsert = _db.Post<IEnumerable<Users>>(zABase);
            ShowData("userInsert - SP", userInsert); // Show all data.
            #endregion
            #region Select * or all fields.
            zABase = new ZABase("[Get_AllUser]", _cmdType);
            var userSelectAll = _db.Post<IEnumerable<Users>>(zABase);
            ShowData("userSelectAll - SP", userSelectAll); // Show all data.
            #endregion
            #region Select with some fields.
            var UserId = userSelectAll.FirstOrDefault(x=>x.UserName == "Test").UserId;
            zAParam = new ZAParam("UserId", UserId, AllSQLType.Guid);
            zABase = new ZABase("[Get_UserById]", _cmdType, zAParam);
            var userSelectExact = _db.Post<IEnumerable<Users>>(zABase);
            ShowData("userSelectExact - SP", userSelectExact); // Show all data.
            #endregion            
            #region update and show field.
            zAParam = new List<ZAParam>()
            {
                new ZAParam("UserName", "Test1", AllSQLType.VarChar, 50),
                new ZAParam("Email", "Test@hotmail.com", AllSQLType.VarChar, 150)
            };
            zABase = new ZABase("[Upd_User]", _cmdType, zAParam);
            var userUpdate = _db.Post<IEnumerable<Users>>(zABase);
            ShowData("userUpdate - SP", userUpdate); // Show all data.
            #endregion
            #region Delete and show field.
            zAParam = new ZAParam("UserName", "Test1", AllSQLType.VarChar, 50);
            zABase = new ZABase("[Rmv_User]", _cmdType, zAParam);
            var userDelete = _db.Post<IEnumerable<Users>>(zABase);
            ShowData("userDelete - SP", userDelete); // Show all data.
            #endregion
        }
        #region ShowData
        private static void ShowData<T>(string _titleTest, IEnumerable<T> _model)
        {
            Console.WriteLine("Test: " + _titleTest);
            foreach(var _m in _model)
            {
                Console.WriteLine(_m.ToSerialize());
            }
            Console.WriteLine();
        }
        private static void ShowData<T>(string _titleTest, T _model)
        {
            Console.WriteLine("Test: " + _titleTest);
            Console.Write(_model);
            Console.WriteLine();
        }
        #endregion
    }
}