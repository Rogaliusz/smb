using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace projekt_1.Models
{
    [Table("Products")]
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(Limitations.String.MAX_CHARS_64)]
        public string Name { get; set; }
        public float Price { get; set; }
        public int Count { get; set; }
        public bool Purchased { get; set; }
            
    }
}