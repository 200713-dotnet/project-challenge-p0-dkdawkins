using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain.Models
{
  public class Pizza
  {
    
    private List<string> _toppings = new List<string>();

    public List<string> Toppings 
    {
      get
      {
        return _toppings;
      }
    }
    public string Crust { get; set; }
    public string Size { get; set; }
    public decimal Price { get; set; }

    public Pizza(string size, string crust, List<string> toppings)
    {
      Size = size;
      Crust = crust;
      Toppings.AddRange(toppings);
      Price = CalcPrice();
    }

    public Pizza()
    {
      //Intentionally empty
    }

    public override string ToString()
    {
      var sb = new StringBuilder();

      foreach(var t in Toppings)
      {
        sb.Append(t + ", ");
      }
      return $"{Size}, {Crust}, {sb}";
    }

    //Calculates price based on crust and size
    private decimal CalcPrice()
    {
      decimal price = 0.00m;

      switch(Crust)
      {
        case "Normal":
          price += 5.00m;
          break;
        case "Stuffed":
          price += 8.00m;
          break;
        default:
          break;
      }

      switch(Size)
      {
        case "S":
          price += 10.00m;
          break;
        case "L":
          price += 15.00m;
          break;
        default:
          break;
      }

      return price;
    }
  }
}