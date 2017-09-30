namespace CargoMate.DataAccess.Models.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User.Menus")]
    public partial class Menu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Menu()
        {
            MenuActions = new HashSet<MenuAction>();
            SubMenus = new HashSet<Menu>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public string JsFunction { get; set; }

        public string Icon { get; set; }

        public bool IsSidebarMenu { get; set; }

        public string MenuKey { get; set; }

        public int? ParentId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuAction> MenuActions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [ForeignKey("ParentId")]
        public virtual ICollection<Menu> SubMenus { get; set; }

        public virtual Menu Menu1 { get; set; }
    }
}
