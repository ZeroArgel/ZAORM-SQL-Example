namespace SQLExamples.MVC.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Roles
    {
        [Display(Name="Role Id")]
        public Guid RoleId { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        [Display(Name = "Available")]
        public bool Available { get; set; }
    }
}