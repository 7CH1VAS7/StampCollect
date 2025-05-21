using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Курсовая.CoreLogin.LogModel
{
    public class UserModel
    { 
        int Id { get; set; }
        [DisplayName("Имя")]
        [Required]
        [MaxLength(20)]
        string Name { get; set; }
        [DisplayName("Роль")]
        [Required]
        string Role { get; set; }
        [DisplayName("Password")]
        [Required]
        string hashPass { get; set; }
    }
}
