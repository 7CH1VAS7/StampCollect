using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Курсовая.Domain.Entity
{
    public class Collector
    {
        public int Id { get; set; }                   // Уникальный идентификатор
        public string FullName { get; set; }           // ФИО коллекционера
        public string Country { get; set; }            // Страна проживания
        public string ContactInfo { get; set; }        // Контактная информация
        public string Email { get; set; }              // Электронная почта
        public string Phone { get; set; }              // Телефон

        // Навигационное свойство для коллекций
        public ICollection<Collection> Collections { get; set; } = new List<Collection>();

        // Вычисляемое свойство - количество редких марок во всех коллекциях
        public int RareStampsCount => Collections?
            .SelectMany(c => c.Stamps)
            .Count(s => s.IsRare) ?? 0;

        
    }
}
