using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace digitalphotoprinting.Models
{

    [Table("Purchase_Order")]
    public class PurchaseOrder
    {
        [Key]
		[Display(Name = "Order Number")]
		public int Order_Number { get; set; }
        [Required]
        [Display(Name = "Image Title")]
        public string Image_Title { get; set; }
        [Required]
		[Display(Name = "Folder Name")]
		public string Folder_Name { get; set; }
        [Required]
		[Display(Name = "Print Size")]
		public string Print_size { get; set; }
        [Required]
        [EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }
		[Display(Name = "Email Subject")]
		[StringLength(100, ErrorMessage = "The Subject must not be more than {1} characters.")]
		public string Email_Subject { get; set; }
		[Display(Name = "Email Message")]
		[StringLength(300, ErrorMessage = "The Message must not be more than {1} characters.")]
		public string Email_Text { get; set; }
        [Required]
		[Display(Name = "Credit Card Number")]
		[StringLength(20, ErrorMessage = "The Credit Card Number must not be more than {1} characters.")]
		public string Credit_Card_Number { get; set; }
        [Required]
        [Display(Name = "Image Name")]
        public string Image_Name { get; set; }
		[Required]
		public string Status { get; set; }
    }
}

