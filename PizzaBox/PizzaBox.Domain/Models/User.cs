using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
  public class User
  {
    public List<Order> Orders { get; set; }
    public Name Name { get; set; }
  }
}