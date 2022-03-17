using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace curso.api.Model.Inputs
{
    public class LoginInput
    {
        [Required(ErrorMessage = "Login is required.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Pass { get; set; }
    }
}
