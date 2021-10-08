using System;

namespace API.Models
{
    public class ProductInventory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CurrentStock { get; set; }
    }
}