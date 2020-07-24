using System;
using PizzaBox.Domain.Models;

namespace PizzaBox.Client
{
  class Program
  {
    static void Main(string[] args)
    {
      Run();
    }

    static void Run()
    {
      var clientType = GetClientType();
      var clientName = Login(clientType);

      if (clientName != null)
      {
        //Instantiate application user and run the appropriate menu
        switch (clientType)
        {
          case 1:
            User user = new User(clientName);
            UserMenu(user);
            break;
          case 2:
            Store store = new Store(clientName);
            StoreMenu(store);
            break;
          default:
            break;
        }
      }
    }

    static int GetClientType()
    {
      int type = 0;
      
      Console.WriteLine("Enter 1 if User, 2 if Store...");
      int.TryParse(Console.ReadLine(), out type);

      return type;
    }

    static string Login(int userType)
    {
      string name;
      switch(userType)
      {
        case 1:
          Console.WriteLine("Please enter your name...");
          name = Console.ReadLine();
          break;
        case 2:
          Console.WriteLine("Please enter your store name...");
          name = Console.ReadLine();
          break;
        default:
          name = null;
          break;
      }

      return name;
    }

    static void UserMenu(User user)
    {
      var exit = false;
      int selection = 0;
      Store userStore = null;

      //User selects a store to view/order from (may want to make this a class function)
      Console.WriteLine($"Welcome {user.Name}! Please select a store to view (1 for PizzaHut, 2 for Dominos).");
      int.TryParse(Console.ReadLine(), out selection);

      switch(selection)
      {
        case 1:
          userStore = new Store("PizzaHut");
          break;
        case 2:
          userStore = new Store("Dominos");
          break;
        default:
          Console.WriteLine("No valid selection; exiting program.");
          exit = true;
          break;
      }

      while (!exit)
      {
        Console.WriteLine($"Welcome to {userStore.Name}, {user.Name}! What do you want to do?");
        Console.WriteLine("Enter 1 to place an order.");
        Console.WriteLine("Enter 2 to view your order history.");
        Console.WriteLine("Enter any other key to exit the program.");
        int.TryParse(Console.ReadLine(), out selection);  //Test this line to ensure no erroneous carry-over

        switch(selection)
        {
          case 1:
            user.PlaceOrder(userStore);
            break;
          case 2:
            //Display order history (may be a class function) until exit
            break;
          default:
            exit = true;
            break;
        }
      }
    }

    static void StoreMenu(Store store)
    {
      var exit = false;
      int selection = 0;

      while (!exit)
      {
        Console.WriteLine($"Welcome {store.Name}! What do you want to do?");
        Console.WriteLine("Enter 1 to view order history.");
        Console.WriteLine("Enter 2 to view sales information.");
        Console.WriteLine("Enter any other key to exit the program.");
        int.TryParse(Console.ReadLine(), out selection);

        switch(selection)
        {
          case 1:
            //Display order history (this may be a class function) until exit
            break;
          case 2:
            //Display sales info. (may be a class function) until exit
            break;
          default:
            exit = true;
            break;
        }
      }
    }
  }
}
