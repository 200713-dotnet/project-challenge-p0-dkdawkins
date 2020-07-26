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

    /*public Order CreateOrder()
    {
      return new Order();
    }*/

    public void ViewOrders()
    {
      System.Console.WriteLine("Press enter again to view all orders, or enter a name to filter by.");
      string name = System.Console.ReadLine();

      if (name == "")
      {
        //List all orders
        foreach(var order in Orders)
        {
          order.ListPizzas();
          System.Console.WriteLine("");
        }
      }

      else
      {
        //List orders by the given user
        foreach(var order in Orders)
        {
          if (order.Name == name) //Needs further testing
          {
            order.ListPizzas();
            System.Console.WriteLine("");
          }
        }
      }
    }

    /*public void ViewSales()
    {
      
    }*/
  }
}