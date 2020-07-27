using System;
using System.Collections.Generic;

namespace PizzaBox.Storing
{
    public partial class Pbuser
    {
        public Pbuser()
        {
            Pborder = new HashSet<Pborder>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Pborder> Pborder { get; set; }
    }
}
