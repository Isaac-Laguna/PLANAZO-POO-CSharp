using PLANAZO_POO_CSharp.Models;

namespace PLANAZO_POO_CSharp.Services;

public sealed class ReservationService
{
    private readonly EventCatalog _catalog;
    private readonly List<Reserva> _reservations = [];
    private int _nextId = 1;

    public ReservationService(EventCatalog catalog)
    {
        _catalog = catalog;
    }

    public IReadOnlyList<Reserva> GetAll() => _reservations.AsReadOnly();

    public Reserva CreateReservation(string personName, int eventId, int ticketCount)
    {
        if (string.IsNullOrWhiteSpace(personName))
        {
            throw new ArgumentException("A person's name is required.", nameof(personName));
        }

        var eventItem = _catalog.FindById(eventId)
            ?? throw new KeyNotFoundException("No event was found with that identifier.");

        var reservation = new Reserva(_nextId, personName.Trim(), eventItem, ticketCount);
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
