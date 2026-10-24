namespace PLANAZO_POO_CSharp.Models;

public sealed class Cine : Evento
{
    public Cine(int id, string name, DateTime date, decimal basePrice, int maximumCapacity)
        : base(id, name, date, basePrice, maximumCapacity)
    {
    }

    public override decimal CalcularPrecioFinal() => BasePrice;
}
