using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
  public class Order
  {
    public List<Pizza> Pizzas { get; set; }
    public DateTime DateOrdered { get; set; }
    public string Name { get; set; }
    public bool IsNew { get; set; }
    public decimal Price { get; set; }

    //Called when creating an order in the application
    public Order(string name)
    {
      Name = name;
      Pizzas = new List<Pizza>();
      IsNew = true;
      Price = 0.00m;
    }

    //Called when loading an order from the database
    public Order(string name, List<Pizza> pizzas)
    {
      Name = name;
      Pizzas = pizzas;
      IsNew = false;
      Price = CalcOrderPrice();
    }

    public void CreatePizza(string size, string crust, List<string> toppings)
    {
      if (Pizzas.Count < 50) 
      {
        Pizzas.Add(new Pizza(size, crust, toppings));

        //Re-calculate order price
        Price = CalcOrderPrice();
      }

      else System.Console.WriteLine("Too many pizzas in order; could not add pizza. Please remove some pizzas or make a separate order.");
    }

    public void RemoveLastPizza()
    {
      if (Pizzas.Count > 0) 
      {
        Pizzas.RemoveAt(Pizzas.Count - 1);

        //Re-calculate order price
        Price = CalcOrderPrice();
      }

      else System.Console.WriteLine("Cannot remove pizza; no pizzas added yet.");
    }

    public void ListPizzas()
    {
      System.Console.WriteLine($"Order for {Name}:");
      foreach(var pizza in Pizzas)
      {
        System.Console.WriteLine($"{pizza}Cost: ${pizza.Price}");
      }

      System.Console.WriteLine($"Total Price: ${Price}");
    }

    //Calculates price of the order based on the prices of each pizza
    private decimal CalcOrderPrice()
    {
      decimal orderPrice = 0.00m;

      foreach(var pizza in Pizzas)
      {
        orderPrice += pizza.Price;
      }

      return orderPrice;
    }
  }
}