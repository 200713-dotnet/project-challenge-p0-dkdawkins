using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
  public class Store
  {
    public List<Order> Orders { get; set; }
    
    //Should this be a Name type?
    public string Name { get; set; }

    public Store()
    {
      //Intentionally empty
    }
    
    public Store(string storeName)
    {
      Name = storeName;
      Orders = new List<Order>();
    }

    public Order CreateOrder()
    {
      return new Order();
    }

    public void ViewOrders()
    {

    }

    public void ViewSales()
    {
      
    }
  }
}