namespace CargoMate.DataAccess.Models.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User.RolePermissions")]
    public partial class RolePermission
    {
        public int Id { get; set; }

        public int? MenuActionId { get; set; }

        [StringLength(128)]
        public string RoleId { get; set; }

        public virtual Role Role { get; set; }

        public virtual MenuAction MenuAction { get; set; }
    }
}
