using System;

namespace BLL.DTOs
{
    public class OrderDTO : BaseDTO
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }
    }
}
