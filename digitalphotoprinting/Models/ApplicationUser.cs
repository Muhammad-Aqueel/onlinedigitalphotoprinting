using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;

namespace digitalphotoprinting.Models
{
    public class ApplicationUser : IdentityUser
    {
		[Display(Name = "First Name")]
		public string? FName { get; set; }
		[Display(Name = "Last Name")]
		public string? LName { get; set; }
		[Display(Name = "Date of Birth")]
		public string? DOB { get; set; }
		public string? Gender { get; set; }
		public string? Address { get; set; }
		public string? ProfilePicture { get; set; }
    }
}
