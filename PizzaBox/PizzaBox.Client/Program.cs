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
      User user = null;
      Store store = null;
      var exit = false;

      //Instantiate application user
      switch (clientType)
      {
        case 1:
          user = new User(clientName);
          break;
        case 2:
          store = new Store(clientName);
          break;
        default:
          break;
      }

      if (clientName == null) exit = true;

      //Main program loop
      while(!exit)
      {
        switch(clientType)
        {
          case 1:
            DisplayUserMenu(user);
            break;
          case 2:
            DisplayStoreMenu(store);
            break;
          default:
            break;
        }
        exit = true;
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

    static void DisplayUserMenu(User user)
    {
      Console.WriteLine($"Welcome {user.Name}!");
    }

    static void DisplayStoreMenu(Store store)
    {
      Console.WriteLine($"Welcome {store.Name}!");
    }
  }
}
