using System.ComponentModel.DataAnnotations;

namespace curso.api.Model.Inputs
{
    public class RegisterInput
    {
        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Pass is required")]
        public string Pass { get; set; }
    }
}
