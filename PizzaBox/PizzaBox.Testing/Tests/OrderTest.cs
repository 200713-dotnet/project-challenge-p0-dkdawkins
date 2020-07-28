using System.Collections.Generic;
using PizzaBox.Domain.Models;
using Xunit;

namespace PizzaBox.Testing.Tests
{
  public class OrderTest
  {
    //Test that CreatePizza works and doesn't allow more than 50 pizzas
    [Fact]
    public void Test_CreatePizza()
    {
      var name = "Test";
      var size = "S";
      var crust = "Normal";
      List<string> toppings = new List<string>{"cheese", "ham"};
      var sut = new Order(name);

      for (int i=0; i<=60; i++)
      {
        sut.CreatePizza(size, crust, toppings);
      }

      Assert.True(sut.Pizzas.Count == 50);
    }
  }
}