namespace CargoMate.DataAccess.Models.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User.MenuActions")]
    public partial class MenuAction
    {
        public int Id { get; set; }

        public int? MenuId { get; set; }

        public int? ActionId { get; set; }

        public virtual Action Action { get; set; }

        public virtual Menu Menu { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
