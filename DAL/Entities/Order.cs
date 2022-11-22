using System;

namespace DAL.Entities
{
    public class Order : BaseEntity
    {
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }
    }
}
