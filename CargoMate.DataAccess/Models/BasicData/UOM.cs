using CargoMate.DataAccess.Models.BasicData.Localizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.BasicData
{
    [Table("BasicData.UOM")]
    public class UOM
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public double Factor { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<LocalizedUOM> LocalizedUOMs { get; set; }
    }
}
