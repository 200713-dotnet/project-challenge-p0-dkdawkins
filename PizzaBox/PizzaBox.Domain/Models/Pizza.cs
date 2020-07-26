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

    public Pizza(string size, string crust, List<string> toppings)
    {
      Size = size;
      Crust = crust;
      Toppings.AddRange(toppings);
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
  }
}