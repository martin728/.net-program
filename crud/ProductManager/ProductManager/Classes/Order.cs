using System;

namespace ConsoleApplication1.Classes
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int ProductId { get; set; }
        public OrderStatusEnum Status { get; set; }
    }
}