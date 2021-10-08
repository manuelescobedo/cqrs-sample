using System;

namespace API.Models
{
    public class ProductDisplay
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsOutOfStock { get; set; }

    }
}