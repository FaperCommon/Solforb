namespace DAL.Entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
    }
}
