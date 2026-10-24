namespace PLANAZO_POO_CSharp.Models;

public sealed class Reserva
{
    internal Reserva(int id, string personName, Evento eventItem, int ticketCount)
    {
        if (ticketCount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(ticketCount), "Ticket count must be greater than zero.");
        }

        Id = id;
        PersonName = personName;
        Event = eventItem;
        TicketCount = ticketCount;
        TotalPrice = eventItem.CalcularPrecioFinal() * ticketCount;
        Status = ReservationStatus.Active;
    }

    public int Id { get; }
    public string PersonName { get; }
    public Evento Event { get; }
    public int TicketCount { get; }
    public decimal TotalPrice { get; }
    public ReservationStatus Status { get; private set; }

    internal void Cancel()
    {
        if (Status == ReservationStatus.Canceled)
        {
            throw new InvalidOperationException("The reservation has already been canceled.");
        }

        Event.ReleaseSeats(TicketCount);
        Status = ReservationStatus.Canceled;
    }
}
