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
      Orders = new List<Order>();
    }

    public void PlaceOrder(Store userStore)
    {
      var finished = false;
      var order = new Order();

      while(!finished)
      {
        int selection;
        
        PrintOptions();
        int.TryParse(System.Console.ReadLine(), out selection);
        switch(selection)
        {
          case 1:
            AddPresetPizza(order);
            break;
          case 2:
            AddCustomPizza(order);
            break;
          case 3:
            RemovePizza(order);
            break;
          default:
            finished = true;
            break;
        }
      }

      //Add completed order to the user and store's order lists
      if (order.Pizzas.Count > 0)
      {
        Orders.Add(order);
        userStore.Orders.Add(order);  //This must be added to db at some point
        System.Console.WriteLine("Order Placed.");
      }
    }
    public void ViewOrders(Order order)
    {

    }

    private void PrintOptions()
    {
      System.Console.WriteLine("Enter 1 to add a preset pizza");
      System.Console.WriteLine("Enter 2 to add a custom pizza");
      System.Console.WriteLine("Enter 3 to remove the last pizza");
      System.Console.WriteLine("Enter any other key to checkout");
      System.Console.WriteLine($"Current # of pizzas: {Orders.Count}");
    }
    private void AddPresetPizza(Order order)
    {
      //Select preset pizza and its size
      //Add Pizza to Order
    }

    private void AddCustomPizza(Order order)
    {
      //Select crust, size, and toppings
      //Add Pizza to Order
    }

    private void RemovePizza(Order order)
    {
      if (order.Pizzas.Count > 0) order.Pizzas.RemoveAt(order.Pizzas.Count - 1);
      else System.Console.WriteLine("Cannot remove pizza; no pizzas added yet.");
    }
  }
}