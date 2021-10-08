using System;

namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsOutOfStock
        {
            get
            {
                return CurrentStock <= 0;
            }
        }
        public int CurrentStock { get; set; }
    }
}