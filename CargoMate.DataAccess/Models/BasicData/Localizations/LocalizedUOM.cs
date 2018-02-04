using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.BasicData.Localizations
{

    [Table("[BasicData.Localization].LocalizedUOM")]
    public class LocalizedUOM
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }

        public long? UOMId { get; set; }

        [StringLength(10)]
        public string CultureCode { get; set; }

        public virtual UOM UOM { get; set; }

    }
}
