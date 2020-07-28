using System;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Repositories;

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
      var repo = new ProjectRepository();
      var clientType = GetClientType();
      var clientName = Login(clientType);

      if (clientName != null)
      {
        //Instantiate application user and run the appropriate menu
        switch (clientType)
        {
          case 1:
            User user = new User(clientName);
            UserMenu(user, repo);
            break;
          case 2:
            Store store = new Store(clientName);
            StoreMenu(store, repo);
            break;
          default:
            break;
        }
      }
    }

    //Determine whether the client is a user or store
    static int GetClientType()
    {
      int type = 0;
      
      Console.WriteLine("Enter 1 if User, 2 if Store...");
      int.TryParse(Console.ReadLine(), out type);

      return type;
    }

    //Determine the name of the user/store running the application
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

    //Display options for user and recieve input
    static void UserMenu(User user, ProjectRepository repo)
    {
      var exit = false;
      int selection = 0;
      Store userStore = null;

      //User selects a store to view/order
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

      //Load user's orders from database
      if (userStore != null) repo.Read(user, userStore, false);

      while (!exit)
      {
        Console.WriteLine($"Welcome to {userStore.Name}, {user.Name}! What do you want to do?");
        Console.WriteLine("Enter 1 to place an order.");
        Console.WriteLine("Enter 2 to view your active orders.");
        Console.WriteLine("Enter any other key to exit the program.");
        int.TryParse(Console.ReadLine(), out selection);

        switch(selection)
        {
          case 1:
            user.PlaceOrder(userStore);
            break;
          case 2:
            user.ViewOrders();
            break;
          default:
            exit = true;
            break;
        }
      }

      //Save new orders in the database
      repo.Create(user, userStore);
    }

    //Display store options and recieve input
    static void StoreMenu(Store store, ProjectRepository repo)
    {
      var exit = false;
      int selection = 0;

      //Load store's orders from database
      repo.Read(null, store, true);

      while (!exit)
      {
        Console.WriteLine($"Welcome {store.Name}! What do you want to do?");
        Console.WriteLine("Enter 1 to view active orders.");
        Console.WriteLine("Enter 2 to clear the first active order.");
        Console.WriteLine("Enter any other key to exit the program.");
        int.TryParse(Console.ReadLine(), out selection);

        switch(selection)
        {
          case 1:
            store.ViewOrders();
            break;
          case 2:
            if (store.Orders.Count > 0) 
            {
              store.DeleteFirstOrder();
              repo.Delete(store);
            }
            else Console.WriteLine("No orders exist to delete");
            break;
          default:
            exit = true;
            break;
        }
      }
    }
  }
}
