using System.ComponentModel.DataAnnotations.Schema;

namespace Курсовая.Domain.Entity
{
    public class Stamp
    {
        public int Id { get; set; }                     // Уникальный идентификатор марки
        public string Name { get; set; }               // Название марки
        public string Code { get; set; }               // Код марки (каталожный номер)
        public string Theme { get; set; }              // Тематика марки
        public string Country { get; set; }            // Страна-эмитент
        public string Features { get; set; }          // Особенности марки
        public int ReleaseYear { get; set; }           // Год выпуска
        public decimal NominalValue { get; set; }      // Номинальная стоимость
        public int Circulation { get; set; }          // Тираж
        public string Perforation { get; set; }        // Тип зубцовки
        public string Series { get; set; }            // Название серии
        public decimal CatalogPrice { get; set; }     // Текущая цена по каталогу
        public DateTime CreationDate { get; set; }
        public DateTime? LastBy { get; set; }
        // Вычисляемое свойство - является ли марка редкой
        public bool IsRare => CatalogPrice > 1000;

        // Навигационное свойство для коллекций, содержащих эту марку
        public ICollection<Collection> Collections { get; set; } = new List<Collection>();
    }
}
