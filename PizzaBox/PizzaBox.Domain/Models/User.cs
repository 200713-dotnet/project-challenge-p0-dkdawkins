using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
  public class User
  {

    public List<Order> Orders { get; set; }
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
      var order = new Order(Name);

      while(!finished)
      {
        int selection;
        
        PrintOptions(order);
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
            order.RemoveLastPizza();
            break;
          default:
            finished = true;
            break;
        }
      }

      //Add completed order to the user's active orders
      if (order.Pizzas.Count > 0)
      {
        Orders.Add(order);
        System.Console.WriteLine("Order Placed.");
      }
    }

    public void ViewOrders()
    {
      foreach(var order in Orders)
      {
        order.ListPizzas();
        System.Console.WriteLine("");
      }
    }

    private void PrintOptions(Order order)
    {
      System.Console.WriteLine("Enter 1 to add a preset pizza ($23.00)");
      System.Console.WriteLine("Enter 2 to add a custom pizza");
      System.Console.WriteLine("Enter 3 to remove the last pizza");
      System.Console.WriteLine("Enter any other key to checkout");
      System.Console.WriteLine($"Current Order Price: ${order.Price}");
      System.Console.WriteLine($"Current # of pizzas: {order.Pizzas.Count}");
    }
    
    private void AddPresetPizza(Order order)
    {
      int selection = 0;

      //Select preset pizza and its size
      System.Console.WriteLine("Enter 1 to add a Pepperoni pizza");
      System.Console.WriteLine("Enter 2 to add a Ham pizza");
      System.Console.WriteLine("Enter 3 to add a Sausage pizza");
      System.Console.WriteLine("Enter 4 to add a Pineapple pizza");
      int.TryParse(System.Console.ReadLine(), out selection);

      //Add Pizza to Order
      switch(selection)
      {
        case 1:
          order.CreatePizza("L", "Stuffed", new List<string>{"cheese", "pepperoni"});
          break;
        case 2:
          order.CreatePizza("L", "Stuffed", new List<string>{"cheese", "ham"});
          break;
        case 3:
          order.CreatePizza("L", "Stuffed", new List<string>{"cheese", "sausage"});
          break;
        case 4:
          order.CreatePizza("L", "Stuffed", new List<string>{"cheese", "pineapple"});
          break;
        default:
          System.Console.WriteLine("Invalid order. Please try again.");
          break;
      }
    }

    private void AddCustomPizza(Order order)
    {
      //Select crust, size, and toppings
      string crust = SelectCrust();
      string size = SelectSize();
      List<string> toppings = SelectToppings();

      //Add Pizza to Order
      order.CreatePizza(size, crust, toppings);
    }

    private string SelectCrust()
    {
      string crust = "";
      int selection = 0;
      var isValid = false;

      while(!isValid)
      {
        System.Console.WriteLine("Enter 1 for Normal Crust ($5.00), 2 for Stuffed Crust ($8.00)");
        int.TryParse(System.Console.ReadLine(), out selection);

        switch(selection)
        {
          case 1:
            crust = "Normal";
            isValid = true;
            break;
          case 2:
            crust = "Stuffed";
            isValid = true;
            break;
          default:
            System.Console.WriteLine("Invalid selection. Please try again.");
            break;
        }
      }
      
      return crust;
    }

    private string SelectSize()
    {
      string size = "";
      int selection = 0;
      var isValid = false;

      while(!isValid)
      {
        System.Console.WriteLine("Enter 1 for Small Pizza ($10.00), 2 for Large Pizza ($15.00)");
        int.TryParse(System.Console.ReadLine(), out selection);

        switch(selection)
        {
          case 1:
            size = "S";
            isValid = true;
            break;
          case 2:
            size = "L";
            isValid = true;
            break;
          default:
            System.Console.WriteLine("Invalid selection. Please try again.");
            break;
        }
      }

      return size;
    }

    private List<string> SelectToppings()
    {
      List<string> toppings = new List<string>{"cheese"};
      int selection = 0;
      var isValid = false;

      while(!isValid)
      {
        System.Console.WriteLine("Please select the topping you want (cheese added by default)");
        System.Console.WriteLine("1=Pepperoni, 2=Ham, 3=Sausage, 4=Pineapple");
        int.TryParse(System.Console.ReadLine(), out selection);

        switch(selection)
        {
          case 1:
            toppings.Add("pepperoni");
            isValid = true;
            break;
          case 2:
            toppings.Add("ham");
            isValid = true;
            break;
          case 3:
            toppings.Add("sausage");
            isValid = true;
            break;
          case 4:
            toppings.Add("pineapple");
            isValid = true;
            break;
          default:
            System.Console.WriteLine("Invalid selection. Please try again.");
            break;
        }
      }

      return toppings;
    }
  }
}