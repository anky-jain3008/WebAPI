using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Data.Model
{
    public class Order
    {
        public long Id { get; set; }

        [ForeignKey("Customer")]
        public long CustomerId { get; set; }

        public DateTime PlacedOn { get; set; }

        public string Status { get; set; }

        public long TotalAmount { get; set; }

        public virtual Customer Customer { get; set; }
    }

    public class OrderDetail
    {
        public long Id { get; set; }

        [ForeignKey("Order")]
        public long OrderId { get; set; }

        [ForeignKey("Product")]
        public long ProductId { get; set; }

        public short Count { get; set; }
        public long AmountPerItem { get; set; }
        public long TotalAmount { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}