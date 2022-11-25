using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
    }
}
