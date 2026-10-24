namespace PLANAZO_POO_CSharp.Models;

public sealed class Concierto : Evento
{
    public Concierto(int id, string name, DateTime date, decimal basePrice, int maximumCapacity)
        : base(id, name, date, basePrice, maximumCapacity)
    {
    }

    public override decimal CalcularPrecioFinal() => BasePrice * 1.15m;
}
