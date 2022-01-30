using System;

namespace WebAPI.Data.ViewModel
{
    public class OrderViewModel
    {
        public long Id { get; set; }
        public DateTime PlacedOn { get; set; }
        public string Status { get; set; }
        public long TotalAmount { get; set; }
    }
}
