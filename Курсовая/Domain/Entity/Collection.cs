using System.ComponentModel.DataAnnotations.Schema;

namespace Курсовая.Domain.Entity
{
    public class Collection
    {
        public int Id { get; set; }// Уникальный идентификатор
        public string Name { get; set; }// Название коллекции



        // Внешний ключ и навигационное свойство для владельца
        public int CollectorId { get; set; }
        public Collector Collector { get; set; }
        // Список марок в коллекции (связь многие-ко-многим)
        public ICollection<Stamp> Stamps { get; set; } = new List<Stamp>();
        // Список цен покупки соответствующих марок
        public List<decimal>? PurchasePrices { get; set; } = new List<decimal>();
        // Вычисляемое свойство - общая стоимость коллекции
        public decimal TotalValue => Stamps?.Sum(s => s.CatalogPrice) ?? 0;

        [NotMapped]
        public List<int> SelectedStampIds { get; set; } = new List<int>();
    }
}
