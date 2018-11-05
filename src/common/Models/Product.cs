using System;
using System.Collections.Generic;
using System.Text;

namespace common.Models
{ 
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Count { get; set; }
        public bool Purchased { get; set; }

    }
}
