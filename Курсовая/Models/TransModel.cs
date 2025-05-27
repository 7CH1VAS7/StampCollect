using Курсовая.Domain.Entity;

namespace Курсовая.Models
{
    public class TransModel
    {
        public Collector Collector { get; set; }
        public Stamp Stamp { get; set; }

        public TransModel(Collector Collector, Stamp Stamp)
        {
            this.Collector = Collector;
            this.Stamp = Stamp;
        }
    }
}
