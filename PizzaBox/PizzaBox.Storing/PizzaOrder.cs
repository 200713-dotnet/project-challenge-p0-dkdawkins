using System;
using System.Collections.Generic;

namespace PizzaBox.Storing
{
    public partial class PizzaOrder
    {
        public int PizzaOrderId { get; set; }
        public int OrderId { get; set; }
        public int PizzaId { get; set; }
        public bool Active { get; set; }

        public virtual Pborder Order { get; set; }
        public virtual Pizza Pizza { get; set; }
    }
}
