namespace PLANAZO_POO_CSharp.Services;
public sealed class ConsoleMenu
{
    private readonly ReservationService _reservationService;
    public void Run()
    {
    }
    private void CreateReservation()
    {
        Console.Write("Nombre de la persona: ");
        var personName = Console.ReadLine();

        if (!ReadInteger("Identificador del evento: ", out var eventId)
            || !ReadInteger("Cantidad de entradas: ", out var ticketCount))
        {
            return;
        }

        try
        {
            var reservation = _reservationService.CreateReservation(personName ?? string.Empty, eventId, ticketCount);
            Console.WriteLine($"Reserva #{reservation.Id} creada para {reservation.PersonName}.");
            Console.WriteLine($"Valor total: {FormatCurrency(reservation.TotalPrice)}");
        }
        catch (ArgumentException)
        {
            Console.WriteLine("No se pudo crear la reserva: el nombre es obligatorio y la cantidad debe ser mayor que cero.");
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("No se pudo crear la reserva: no hay suficientes cupos disponibles.");
        }
        catch (KeyNotFoundException)
        {
            Console.WriteLine("No se encontró un evento con ese identificador.");
        }
    }

    private void ShowReservations()
    {
        var reservations = _reservationService.GetAll();
        if (reservations.Count == 0)
        {
            Console.WriteLine("Todavía no hay reservas.");
            return;
        }

        foreach (var reservation in reservations)
        {
            Console.WriteLine(
                $"Reserva #{reservation.Id} | {reservation.PersonName} | {reservation.Event.Name} | " +
                $"{reservation.TicketCount} entrada(s) | {FormatCurrency(reservation.TotalPrice)} | " +
                $"{GetReservationStatus(reservation.Status)}");
        }
    }

    private void CancelReservation()
    {
        if (!ReadInteger("Identificador de la reserva: ", out var reservationId))
        {
            return;
        }

        try
        {
            _reservationService.CancelReservation(reservationId);
            Console.WriteLine($"La reserva #{reservationId} fue cancelada y sus cupos fueron devueltos.");
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("No se pudo cancelar la reserva: ya fue cancelada.");
        }
        catch (KeyNotFoundException)
        {
            Console.WriteLine("No se encontró una reserva con ese identificador.");
        }
    }

    private void ShowSummary()
    {
        var summary = _reservationService.GetSummary();
        Console.WriteLine($"Reservas activas: {summary.ActiveReservations}");
        Console.WriteLine($"Entradas reservadas: {summary.ReservedTickets}");
        Console.WriteLine($"Valor total: {FormatCurrency(summary.TotalValue)}");
    }
}
