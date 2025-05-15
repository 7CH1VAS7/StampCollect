using System.ComponentModel.DataAnnotations;

namespace Курсовая.Domain.Entity
{
    //   На разных пк то писал возникали проблемы с отображением, в этом случае может быть выведено значение ключа а не содержимого

    /*public enum EnumerableZub
    {
        Рамочная,Линейная,Гребенчатая, Простая, Комбинированная, Сложнокомбинированная
    }*/


    public enum EnumerableZub
    {
        [Display(Name = "Рамочная")]
        Рамочная,

        [Display(Name = "Линейная")]
        Линейная,

        [Display(Name = "Гребенчатая")]
        Гребенчатая,

        [Display(Name = "Простая")]
        Простая,

        [Display(Name = "Комбинированная")]
        Комбинированная,

        [Display(Name = "Сложнокомбинированная")]
        Сложнокомбинированная
    }
}
