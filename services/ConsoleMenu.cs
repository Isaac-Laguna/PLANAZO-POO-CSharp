using System.Globalization;
using PLANAZO_POO_CSharp.Models;

namespace PLANAZO_POO_CSharp.Services;

public sealed class ConsoleMenu
{
    private readonly EventCatalog _catalog;
    private readonly ReservationService _reservationService;
    private readonly UserService _userService;

    public ConsoleMenu()
    {
        _catalog = InitialData.CreateCatalog();
        _reservationService = new ReservationService(_catalog);
        _userService = new UserService(InitialData.CreateUsers());
    }

    public void Run()
    {
        var continueRunning = true;

        while (continueRunning)
        {
            ShowMenu();
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ShowEvents();
                    break;
                case "2":
                    FindEvent();
                    break;
                case "3":
                    CreateReservation();
                    break;
                case "4":
                    ShowReservations();
                    break;
                case "5":
                    CancelReservation();
                    break;
                case "6":
                    ShowSummary();
                    break;
                case "7":
                    ShowUserReservations();
                    break;
                case "0":
                    continueRunning = false;
                    break;
                case null:
                    continueRunning = false;
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

            if (continueRunning)
            {
                Console.WriteLine();
            }
        }

        Console.WriteLine("Gracias por usar PLANAZO.");
    }

    private static void ShowMenu()
    {
        Console.WriteLine("================================");
        Console.WriteLine("        PLANAZO - EVENTOS");
        Console.WriteLine("================================");
        Console.WriteLine("1. Ver eventos disponibles");
        Console.WriteLine("2. Consultar evento");
        Console.WriteLine("3. Reservar entradas");
        Console.WriteLine("4. Ver reservas realizadas");
        Console.WriteLine("5. Cancelar una reserva");
        Console.WriteLine("6. Ver resumen de reservas");
        Console.WriteLine("0. Salir");
        Console.Write("Seleccione una opción: ");
    }

    private void ShowEvents()
    {
        Console.WriteLine();
        Console.WriteLine("EVENTOS DISPONIBLES");

        foreach (var eventItem in _catalog.GetAll())
        {
            ShowEventDetails(eventItem);
        }
    }

    private void FindEvent()
    {
        if (!ReadInteger("Ingrese el identificador del evento: ", out var eventId))
        {
            return;
        }

        var eventItem = _catalog.FindById(eventId);
        if (eventItem is null)
        {
            Console.WriteLine("No se encontró un evento con ese identificador.");
            return;
        }

        ShowEventDetails(eventItem);
    }

    private void CreateReservation()
    {
        Console.Write("Nombre de la persona: ");
        var userName = Console.ReadLine();

        foreach (var eventItem in _catalog.GetAll())
        {
            ShowEventDetails(eventItem);
        }

        if (!ReadInteger("Identificador del evento: ", out var eventId)
            || !ReadInteger("Cantidad de entradas: ", out var ticketCount))
        {
            return;
        }

        try
        {
            var user = _userService.GetOrCreate(userName ?? string.Empty);
            var reservation = _reservationService.CreateReservation(user, eventId, ticketCount);
            Console.WriteLine($"Reserva #{reservation.Id} creada para {user.Name} (usuario #{user.Id}).");
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
                $"Reserva #{reservation.Id} | {reservation.User.Name} (#{reservation.User.Id}) | {reservation.Event.Name} | " +
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

    private void ShowUserReservations()
    {
        var users = _userService.GetAll();
        if (users.Count == 0)
        {
            Console.WriteLine("Todavía no hay usuarios registrados.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("USUARIOS REGISTRADOS");
        foreach (var user in users)
        {
            Console.WriteLine($"#{user.Id} | {user.Name}");
        }

        if (!ReadInteger("Identificador del usuario: ", out var userId))
        {
            return;
        }

        var selectedUser = _userService.FindById(userId);
        if (selectedUser is null)
        {
            Console.WriteLine("No se encontró un usuario con ese identificador.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"RESERVAS DE {selectedUser.Name}");

        var userReservations = _reservationService.GetByUser(userId);
        if (userReservations.Count == 0)
        {
            Console.WriteLine("Este usuario no tiene reservas.");
            return;
        }

        foreach (var reservation in userReservations)
        {
            Console.WriteLine(
                $"Reserva #{reservation.Id} | {reservation.Event.Name} | " +
                $"{reservation.TicketCount} entrada(s) | {FormatCurrency(reservation.TotalPrice)} | " +
                $"{GetReservationStatus(reservation.Status)}");
        }
    }

    private static void ShowEventDetails(Event eventItem)
    {
        Console.WriteLine(
            $"#{eventItem.Id} | {GetEventType(eventItem)} | {eventItem.Name} | " +
            $"{eventItem.Date:dd/MM/yyyy} | Precio: {FormatCurrency(eventItem.CalculateFinalPrice())} | " +
            $"Cupos: {eventItem.AvailableSeats}/{eventItem.MaximumCapacity}");
    }

    private static string GetEventType(Event eventItem) => eventItem switch
    {
        Movie => "Cine",
        Concert => "Concierto",
        Workshop => "Taller",
        Festival => "Festival",
        _ => "Evento"
    };

    private static string GetReservationStatus(ReservationStatus status) => status switch
    {
        ReservationStatus.Active => "Activa",
        ReservationStatus.Canceled => "Cancelada",
        _ => "Desconocido"
    };

    private static bool ReadInteger(string prompt, out int value)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out value))
        {
            return true;
        }

        Console.WriteLine("Debe ingresar un número entero válido.");
        return false;
    }

    private static string FormatCurrency(decimal amount) =>
        amount.ToString("C2", CultureInfo.GetCultureInfo("es-CO"));
}
