namespace PLANAZO_POO_CSharp.Models;

public sealed class Movie(int id, string name, DateTime date, decimal basePrice, int maximumCapacity) 
    : Event(id, name, date, basePrice, maximumCapacity)
{
    public override decimal CalculateFinalPrice() => BasePrice;
}
