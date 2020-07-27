using System;
using System.Collections.Generic;

namespace PizzaBox.Storing
{
    public partial class Pborder
    {
        public Pborder()
        {
            PizzaOrder = new HashSet<PizzaOrder>();
        }

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int StoreId { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }

        public virtual Pbstore Store { get; set; }
        public virtual Pbuser User { get; set; }
        public virtual ICollection<PizzaOrder> PizzaOrder { get; set; }
    }
}
