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
    [Table("Customers.Customers")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerId { get; set; }

        [StringLength(50)]
        public string EmailAddress { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public string ImageUrl { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        public bool? Gender { get; set; }

        public string Address { get; set; }
        /// <summary>
        /// Location = "latitude longitude" eg : "20.932439 50.37246235" As string
        /// </summary>
        public DbGeography Location { get; set; }

        public decimal? Rating { get; set; }

        public long? CompanyId { get; set; }

        public bool? IsPhoneVerified { get; set; }

        public long? CustomerStatusId { get; set; }

        public virtual Company Company { get; set; }

        public virtual BasicData.CustomerStatus CustomerStatus { get; set; }
    }
}
