using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Models
{
    public class Trolly
    {
        public List<TrollyProduct> Products { get; set; }
        public List<Special> Specials { get; set; }
        public List<TrollyQuantity> Quantities { get; set; }
    }

    public class Special
    {
        public List<TrollyQuantity> Quantities { get; set; }
        public float Total { get; set; }
    }

    public class TrollyProduct
    {
        public string Name { get; set; }
        public float Price { get; set; }
    }

    public class TrollyQuantity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
