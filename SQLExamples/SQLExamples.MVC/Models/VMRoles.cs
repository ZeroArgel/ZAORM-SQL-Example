namespace SQLExamples.MVC.Models
{
    using System;
    using System.Collections.Generic;
    public class VMRoles
    {
        public Roles Roles { get; set; }
        public IEnumerable<Roles> GetAll { get; set; }
        public Roles GetWithWhere { get; set; }
        public IEnumerable<Roles> GetBySP { get; set; }
    }
}