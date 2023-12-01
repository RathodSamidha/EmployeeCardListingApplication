using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EmployeeCardListingApplication.ViewModels
{
    public class UploadImageViewModel
    {        
        [Display(Name = "Image")]
        public IFormFile ProfilePicture { get; set; }
    }
}
