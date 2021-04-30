using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MYCVMAKERPROJECT.Models
{
    [MetadataType(typeof(UsersMetaData))]
    public partial class User
    {
        public bool RememberMe { get; set; }
    }
    public class UsersMetaData
    {
        
        public int Id { get; set; }
        [Display(Name ="User Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Name is required")]
        public string UserName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage = "Minmum 6 characters required")]
        public string UserPassword { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("UserPassword",ErrorMessage ="Confirm password and password do not match")]
        public string ConfirmPassword { get; set; }
        [Display(Name ="Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        public int UserState { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [DataType(DataType.Url)]
        public string InstagramLink { get; set; }
        [DataType(DataType.Url)]
        public string GetHupLink { get; set; }
        [DataType(DataType.Url)]
        public string LinkedInLink { get; set; }
        [DataType(DataType.Url)]
        public string FacebookLink { get; set; }
        [Display(Name = "Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Display(Name = "Phone Numper")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone Numper is required")]
        public int PhoneNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Admin> Admins { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Company> Companies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HitCounter> HitCounters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Personal> Personals { get; set; }
    }

}