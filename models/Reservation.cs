namespace PLANAZO_POO_CSharp.Models;

public sealed class Reservation
{
    internal Reservation(int id, User user, Event eventItem, int ticketCount)
    {
        if (ticketCount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(ticketCount), "Ticket count must be greater than zero.");
        }

        Id = id;
        User = user;
        Event = eventItem;
        TicketCount = ticketCount;
        TotalPrice = eventItem.CalculateFinalPrice() * ticketCount;
        Status = ReservationStatus.Active;
    }

    public int Id { get; }
    public User User { get; }
    public Event Event { get; }
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
