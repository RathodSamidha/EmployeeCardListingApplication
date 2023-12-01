using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeCardListingApplication.ViewModels
{
    public class EmployeeViewModel:EditImageViewModel
    {
        [Key]
        public int EmployeeID { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        
        public string Designation { get; set; }
        [Required]
        
        public string Department { get; set; }
        [Required]
        
        public string Phone { get; set; }

    }
}
