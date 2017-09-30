using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.Customers
{

    [Table("Customers.Companies")]
    public class Company
    {
        public Company()
        {
            Customers = new HashSet<Customer>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        public DbGeography Location { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public long? CrNumber { get; set; }

        public string Logo { get; set; }

        [StringLength(250)]
        public string WebSiteUrl { get; set; }

        public long? CountryId { get; set; }

        public string Address { get; set; }

        public virtual BasicData.Country Country { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
