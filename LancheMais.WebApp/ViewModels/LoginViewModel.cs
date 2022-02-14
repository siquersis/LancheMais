using System.ComponentModel.DataAnnotations;

namespace LancheMais.WebApp.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Digite o Nome")]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Digite a senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
