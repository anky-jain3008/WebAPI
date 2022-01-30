using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Data.Model
{
    public class Cart
    {
        public long Id { get; set; }

        [ForeignKey("Customer")]
        public long CustomerId { get; set; }

        [ForeignKey("Product")]
        public long ProductId { get; set; }

        public short Count { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }
    }
}