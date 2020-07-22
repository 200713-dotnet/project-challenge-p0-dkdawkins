using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
  public class User
  {

    public List<Order> Orders { get; set; }
    //public Name Name { get; set; }
    public string Name { get; set; }

    public User()
    {
      //Intentionally empty
    }

    public User (string userName)
    {
      Name = userName;
    }
  }
}