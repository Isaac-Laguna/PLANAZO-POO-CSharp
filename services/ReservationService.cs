using PLANAZO_POO_CSharp.Models;

namespace PLANAZO_POO_CSharp.Services;

public sealed class ReservationService
{
    private readonly EventCatalog _catalog;
    private readonly List<Reservation> _reservations = [];
    private int _nextId = 1;

    public ReservationService(EventCatalog catalog)
    {
        _catalog = catalog;
    }

    public IReadOnlyList<Reservation> GetAll() => _reservations.AsReadOnly();

    public Reservation CreateReservation(User user, int eventId, int ticketCount)
    {
        if (user is null)
        {
            throw new ArgumentException("A user is required.", nameof(user));
        }

        var eventItem = _catalog.FindById(eventId)
            ?? throw new KeyNotFoundException("No event was found with that identifier.");

        var reservation = new Reservation(_nextId, user, eventItem, ticketCount);
        eventItem.ReserveSeats(ticketCount);
        _reservations.Add(reservation);
        _nextId++;

        return reservation;
    }

    public void CancelReservation(int reservationId)
    {
        var reservation = _reservations.FirstOrDefault(item => item.Id == reservationId)
            ?? throw new KeyNotFoundException("No reservation was found with that identifier.");

        reservation.Cancel();
    }

    public (int ActiveReservations, int ReservedTickets, decimal TotalValue) GetSummary()
    {
        var activeReservations = _reservations
            .Where(reservation => reservation.Status == ReservationStatus.Active)
            .ToList();

        return (
            activeReservations.Count,
            activeReservations.Sum(reservation => reservation.TicketCount),
            activeReservations.Sum(reservation => reservation.TotalPrice));
    }
}
