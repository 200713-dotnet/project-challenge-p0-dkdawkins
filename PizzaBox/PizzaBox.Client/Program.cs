using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PizzaBox.Domain.Models;
using PizzaBox.Storing;

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
      var db = new PizzaBoxDbContext();
      var clientType = GetClientType();
      var clientName = Login(clientType);

      if (clientName != null)
      {
        //Instantiate application user and run the appropriate menu
        switch (clientType)
        {
          case 1:
            User user = new User(clientName);
            UserMenu(user, db);
            break;
          case 2:
            Store store = new Store(clientName);
            StoreMenu(store, db);
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
          Console.WriteLine("Please enter your store name..."); //only supports PizzaHut and Dominos right now!!
          name = Console.ReadLine();
          break;
        default:
          name = null;
          break;
      }

      return name;
    }

    static void UserMenu(User user, PizzaBoxDbContext db)
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

      //Load from database (check that store has been selected first)
      if (userStore != null) GetUserData(user, userStore, db);

      while (!exit)
      {
        Console.WriteLine($"Welcome to {userStore.Name}, {user.Name}! What do you want to do?");
        Console.WriteLine("Enter 1 to place an order.");
        Console.WriteLine("Enter 2 to view your order history.");
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

      //Save changes to db
      SaveUserData(user, userStore, db);
    }

    static void StoreMenu(Store store, PizzaBoxDbContext db)
    {
      var exit = false;
      int selection = 0;

      //Load from database
      GetStoreData(store, db);

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
            store.ViewOrders();
            break;
          case 2:
            //Display sales info. (may be a class function) until exit
            break;
          default:
            exit = true;
            break;
        }
      }

      //Save changes to db
      //SaveStoreData(store, db);
    }

    static void GetUserData(User user, Store userStore, PizzaBoxDbContext db)
    {
      //Check if the user exists in the db
      var userExists = false;

      foreach (var pbuser in db.Pbuser.ToList()) if (pbuser.Name == user.Name) 
      {
        userExists = true;
      }
      
      if (userExists)
      {
        //Load tables from db for reference
        var orders = db.Pborder.Include(t => t.User).Include(t => t.Store);
        var pizzaOrders = db.PizzaOrder.Include(t => t.Pizza).Include(t => t.Pizza.Size).Include(t => t.Pizza.Crust);
        var pizzaToppings = db.PizzaTopping.Include(t => t.Topping);

        //Load User's orders from database (only those associated with store; does not differentiate users with same name)
        foreach(var order in orders.ToList()) if (order.User.Name == user.Name && order.Store.Name == userStore.Name)
        {
          List<Domain.Models.Pizza> orderPizzas = new List<Domain.Models.Pizza>();

          //Populate list of pizzas associated with a given order
          foreach(var pizzaOrder in pizzaOrders.ToList()) if (pizzaOrder.OrderId == order.OrderId)
          {
            List<string> toppings = new List<string>();
            foreach(var pizzaTopping in pizzaToppings) if (pizzaTopping.PizzaId == pizzaOrder.PizzaId)
            {
              toppings.Add(pizzaTopping.Topping.Name);
            }
            orderPizzas.Add(new Domain.Models.Pizza(pizzaOrder.Pizza.Size.Name, pizzaOrder.Pizza.Crust.Name, toppings));
          }
          user.Orders.Add(new Order(order.Name, orderPizzas));
        }
      }
      
      else
      {
        //Create user in the db for later reference if they don't exist yet
        Pbuser pbuser = new Pbuser();
        pbuser.Name = user.Name;
        db.Pbuser.Add(pbuser);
        db.SaveChanges();
      }
    }

    static void SaveUserData(User user, Store userStore, PizzaBoxDbContext db)
    {
      int userId = -1;
      int storeId = -1;
      
      //Retrieve IDs of user and store
      foreach (var pbuser in db.Pbuser.ToList()) if (pbuser.Name == user.Name)
      { 
        userId = pbuser.UserId;
      }
      foreach (var pbstore in db.Pbstore.ToList()) if (pbstore.Name == userStore.Name)
      {
        storeId = pbstore.StoreId;
      }
      
      //Save new user's orders
      foreach(var order in user.Orders) if (order.IsNew)
      {
        Pborder pborder = new Pborder();
        pborder.Name = order.Name;
        pborder.UserId = userId;
        pborder.StoreId = storeId;

        db.Pborder.Add(pborder);
        db.SaveChanges();

        //Add pizza relationships to order
        foreach(var pizza in order.Pizzas)
        {
          int pizzaId = -1;
          //Find pizzaId by name (assumes pizzas already exist in db)
          foreach(var dbpizza in db.Pizza.ToList()) if (dbpizza.Name == pizza.ToString())
          {
            pizzaId = dbpizza.PizzaId;
          }
          ///If one cant be found, create it under that name
          /*if (pizzaId == -1)
          {
            Storing.Pizza pizza1 = new Storing.Pizza();
            pizza1.Name = pizza.ToString();
          }*/
          
          //Create a row in the PizzaOrder junction table
          PizzaOrder pizzaOrder = new PizzaOrder();
          pizzaOrder.OrderId = pborder.OrderId;
          pizzaOrder.PizzaId = pizzaId;

          db.PizzaOrder.Add(pizzaOrder);
          db.SaveChanges();
        }
      }
    }

    static void GetStoreData(Store store, PizzaBoxDbContext db)
    {

      //Load tables from db for reference
      var orders = db.Pborder.Include(t => t.Store);
      var pizzaOrders = db.PizzaOrder.Include(t => t.Pizza).Include(t => t.Pizza.Size).Include(t => t.Pizza.Crust);
      var pizzaToppings = db.PizzaTopping.Include(t => t.Topping);

      //Load Store's orders from database
      foreach(var order in orders.ToList()) if (order.Store.Name == store.Name)
      {
        List<Domain.Models.Pizza> orderPizzas = new List<Domain.Models.Pizza>();

        //Populate list of pizzas associated with a given order
        foreach(var pizzaOrder in pizzaOrders.ToList()) if (pizzaOrder.OrderId == order.OrderId)
        {
          List<string> toppings = new List<string>();
          foreach(var pizzaTopping in pizzaToppings) if (pizzaTopping.PizzaId == pizzaOrder.PizzaId)
          {
            toppings.Add(pizzaTopping.Topping.Name);
          }
          orderPizzas.Add(new Domain.Models.Pizza(pizzaOrder.Pizza.Size.Name, pizzaOrder.Pizza.Crust.Name, toppings));
        }
        store.Orders.Add(new Order(order.Name, orderPizzas));
      }
    }

    static void SaveStoreData(Store store, PizzaBoxDbContext db)
    {
      //Save changes to store orders in database
      //int userId = -1;
      int storeId = -1;
      
      //Retrieve IDs of user and store
      /*foreach (var pbuser in db.Pbuser.ToList()) if (pbuser.Name == user.Name)
      { 
        userId = pbuser.UserId;
      }*/
      foreach (var pbstore in db.Pbstore.ToList()) if (pbstore.Name == store.Name)
      {
        storeId = pbstore.StoreId;
      }
      
      //Save new user's orders
      /*foreach(var order in user.Orders) if (order.IsNew)
      {
        Pborder pborder = new Pborder();
        pborder.Name = order.Name;
        pborder.UserId = userId;
        pborder.StoreId = storeId;

        db.Pborder.Add(pborder);
        db.SaveChanges();

        //Add pizza relationships to order
        foreach(var pizza in order.Pizzas)
        {
          int pizzaId = -1;
          //Find pizzaId by name (assumes pizzas already exist in db)
          foreach(var dbpizza in db.Pizza.ToList()) if (dbpizza.Name == pizza.ToString())
          {
            pizzaId = dbpizza.PizzaId;
          }
          ///If one cant be found, create it under that name
          if (pizzaId == -1)
          {
            Storing.Pizza pizza1 = new Storing.Pizza();
            pizza1.Name = pizza.ToString();
          }
          
          //Create a row in the PizzaOrder junction table
          PizzaOrder pizzaOrder = new PizzaOrder();
          pizzaOrder.OrderId = pborder.OrderId;
          pizzaOrder.PizzaId = pizzaId;

          db.PizzaOrder.Add(pizzaOrder);
          db.SaveChanges();
        }
      }*/
    }
  }
}
