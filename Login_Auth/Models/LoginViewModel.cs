using System.ComponentModel.DataAnnotations;

namespace Login_Auth.Models
{
    public class LoginViewModel
    {
        //[Required]
        //[EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }


        [Display(Name = "User Name")]
        [Required]
        public string UserName { get; set; }

        //public string HdUserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        //public string Password { get; set; }
        public string Key { get; set; }


        //public string HdPassword { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }


        public int UserSubTypeId { get; set; }
    }
}
