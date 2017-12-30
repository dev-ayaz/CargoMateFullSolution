using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Models.CustomerViewModels
{
    public class CustomerViewModel
    {
    }

    public class CustomerDisplayModel
    {
        public long Id { get; set; }

        public string CustomerId { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string ImageSrc { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public bool? Gender { get; set; }

        public string Address { get; set; }

        public CompanyShortViewModel Company { get; set; }
    }

    public class CompanyShortViewModel
    {
        public long? Id { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }

        public string Location { get; set; }
    }

    public class CustomerFormModel
    {
        [Key]
        public long? Id { get; set; }

        public string Name { get; set; }

        [StringLength(50)]
        public string CustomerId { get; set; }

        [Required(ErrorMessage = "Please Enter Email Address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Please Enter Phone Number")]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

        public string ImageSrc { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public bool? Gender { get; set; }

        [Display(Name = "Company Name")]
        public long? CompanyId { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        public string Location { get; set; }
    }
}