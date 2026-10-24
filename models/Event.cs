namespace PLANAZO_POO_CSharp.Models;

public abstract class Evento
{
    protected Evento(int id, string name, DateTime date, decimal basePrice, int maximumCapacity)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "The identifier must be greater than zero.");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("The event name is required.", nameof(name));
        }

        if (basePrice < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(basePrice), "The base price cannot be negative.");
        }

        if (maximumCapacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maximumCapacity), "Capacity must be greater than zero.");
        }

        Id = id;
        Name = name;
        Date = date;
        BasePrice = basePrice;
        MaximumCapacity = maximumCapacity;
        AvailableSeats = maximumCapacity;
    }

    public int Id { get; }
    public string Name { get; }
    public DateTime Date { get; }
    public decimal BasePrice { get; }
    public int MaximumCapacity { get; }
    public int AvailableSeats { get; private set; }

    public abstract decimal CalcularPrecioFinal();

    public void ReserveSeats(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
        }

        if (quantity > AvailableSeats)
        {
            throw new InvalidOperationException("There are not enough seats available.");
        }

        AvailableSeats -= quantity;
    }

    internal void ReleaseSeats(int quantity)
    {
        if (quantity <= 0 || quantity > MaximumCapacity - AvailableSeats)
        {
            throw new InvalidOperationException("Cannot release that number of seats.");
        }

        AvailableSeats += quantity;
    }
}
