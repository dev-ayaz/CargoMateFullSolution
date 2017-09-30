using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.BasicData.Localizations
{
    [Table("[BasicData.Localization].CustomerStatuses")]
    public class LocalizedCustomerStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public long? CustomerStatusId { get; set; }

        [StringLength(20)]
        public string CultureCode { get; set; }

        public virtual CustomerStatus CustomerStatus { get; set; }
    }
}
