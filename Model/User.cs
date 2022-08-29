using System.ComponentModel.DataAnnotations;

namespace TruYumWebAPI.Model
{

    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";
        public const string Anon = "Anon";
    }

    public class SignUp
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? ConfirmPassword { get; set; }
}
        public class Login    
       {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

       [Required(ErrorMessage = "Password is required")]
       public string? Password { get; set; }
      }
        public class User
       {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public Single Userid { get; set; }
        
    }
    public class Response
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
    }
}
