using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Курсовая.Domain.Entity;

namespace Курсовая.Models
{
    public class LoginViewModel
    {
        [DisplayName("ЛОГИН")]
        [Required]
        [MaxLength(30)]
        public string Login { get; set; }

        [DisplayName("Пароль")]
        [MaxLength(100)]
        [Required]
        public string? Password { get; set; }
    }
}
