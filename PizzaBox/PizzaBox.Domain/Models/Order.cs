using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
  public class Order
  {
    public List<Pizza> Pizzas { get; set; }
    public DateTime DateOrdered { get; set; }
    public string Name { get; set; }

    public Order(string name)
    {
      Name = name;
      Pizzas = new List<Pizza>();
    }

    public void CreatePizza(string size, string crust, List<string> toppings)
    {
      if (Pizzas.Count < 50) Pizzas.Add(new Pizza(size, crust, toppings));
      else System.Console.WriteLine("Too many pizzas in order; could not add pizza. Please remove some pizzas or make a separate order.");  //Needs testing
    }

    public void RemoveLastPizza()
    {
      if (Pizzas.Count > 0) Pizzas.RemoveAt(Pizzas.Count - 1);
      else System.Console.WriteLine("Cannot remove pizza; no pizzas added yet.");
    }

    public void ListPizzas()
    {
      System.Console.WriteLine($"Order for {Name}:");
      foreach(var pizza in Pizzas)
      {
        System.Console.WriteLine(pizza);
      }
    }
  }
}