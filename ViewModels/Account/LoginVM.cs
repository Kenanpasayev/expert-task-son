using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels.Account
{
    public class LoginVM
    {
        [Required]
        public string Name { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
