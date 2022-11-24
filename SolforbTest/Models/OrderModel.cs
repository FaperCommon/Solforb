using System;

namespace SolforbTest.Models
{
    public class OrderModel : BaseModel
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }
    }
}
