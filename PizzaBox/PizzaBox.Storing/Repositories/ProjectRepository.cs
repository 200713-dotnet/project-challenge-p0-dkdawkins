using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PizzaBox.Domain.Models;

namespace PizzaBox.Storing.Repositories
{
  public class ProjectRepository
  {
    private PizzaBoxDbContext _db = new PizzaBoxDbContext();

    //Creates Pborders and PizzaOrders in the database for each newly-created order at the end of program execution
    public void Create(User user, Store userStore)
    {
      int userId = -1;
      int storeId = -1;
      
      //Retrieve IDs of user and store
      foreach (var pbuser in _db.Pbuser.ToList()) if (pbuser.Name == user.Name)
      { 
        userId = pbuser.UserId;
      }
      foreach (var pbstore in _db.Pbstore.ToList()) if (pbstore.Name == userStore.Name)
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

        _db.Pborder.Add(pborder);
        _db.SaveChanges();

        //Add pizza relationships to order
        foreach(var pizza in order.Pizzas)
        {
          int pizzaId = -1;
          //Find pizzaId by name (assumes pizzas already exist in db)
          foreach(var dbpizza in _db.Pizza.ToList()) if (dbpizza.Name == pizza.ToString())
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

          _db.PizzaOrder.Add(pizzaOrder);
          _db.SaveChanges();
        }
      }
    }

    public void Read(User user, Store store, bool isStore)
    {
      if (isStore) StoreRead(store);
      else UserRead(user, store);
    }

    public void Update()
    {

    }

    public void Delete()
    {

    }

    private void UserRead(User user, Store userStore)
    {
      //Check if the user exists in the db
      var userExists = false;

      foreach (var pbuser in _db.Pbuser.ToList()) if (pbuser.Name == user.Name) 
      {
        userExists = true;
      }
      
      if (userExists)
      {
        //Load tables from db for reference
        var orders = _db.Pborder.Include(t => t.User).Include(t => t.Store);
        var pizzaOrders = _db.PizzaOrder.Include(t => t.Pizza).Include(t => t.Pizza.Size).Include(t => t.Pizza.Crust);
        var pizzaToppings = _db.PizzaTopping.Include(t => t.Topping);

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
        _db.Pbuser.Add(pbuser);
        _db.SaveChanges();
      }
    }

    private void StoreRead(Store store)
    {
      //Load tables from db for reference
      var orders = _db.Pborder.Include(t => t.Store);
      var pizzaOrders = _db.PizzaOrder.Include(t => t.Pizza).Include(t => t.Pizza.Size).Include(t => t.Pizza.Crust);
      var pizzaToppings = _db.PizzaTopping.Include(t => t.Topping);

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
  }
}