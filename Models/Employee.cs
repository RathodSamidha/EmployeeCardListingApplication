using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeCardListingApplication.Models
{
    public class Employee 
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Designation")]
        public string Designation { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Department")]
        public string Department { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Required]        
        [Display(Name = "Image")]
        public string ProfilePicture { get; set; }
    }
}
