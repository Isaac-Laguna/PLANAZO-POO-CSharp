namespace PLANAZO_POO_CSharp.Models;

public sealed class Taller : Evento
{
    public Taller(int id, string name, DateTime date, decimal basePrice, int maximumCapacity)
        : base(id, name, date, basePrice, maximumCapacity)
    {
    }

    public override decimal CalcularPrecioFinal() => BasePrice * 0.90m;
}
