using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CargoMate.DataAccess.Models;
using CargoMate.DataAccess.Models.Users;

namespace CargoMate.Web.Admin.Areas.Users.Models
{
    public class UserViewModel
    {
        [Key]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Required")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }


        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string CofirmPassword { get; set; }

        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Role")]
        public string RoleId { get; set; }

        public string RoleName { get; set; }

        public string UserImage { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public UserRole UserRoles { get; set; }

        public List<Role> Roles { get; set; }


    }
}