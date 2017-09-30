using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.BasicData
{
    [Table("BasicData.CustomerStatuses")]
    public class CustomerStatus
    {
        public CustomerStatus()
        {
            LocalizedCustomerStatuses = new HashSet<Localizations.LocalizedCustomerStatus>();
            Customers = new HashSet<Customers.Customer>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<Localizations.LocalizedCustomerStatus> LocalizedCustomerStatuses { get; set; }

        public virtual ICollection<Customers.Customer> Customers { get; set; }
    }
}
