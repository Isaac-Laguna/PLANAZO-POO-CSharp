using PLANAZO_POO_CSharp.Models;

namespace PLANAZO_POO_CSharp.Services;

public static class InitialData
{
    public static EventCatalog CreateCatalog()
    {
        var today = DateTime.Today;
        Evento[] events =
        [
            new Cine(1, "La aventura comienza", today.AddDays(3), 12000m, 80),
            new Cine(2, "Misterio en la ciudad", today.AddDays(5), 14000m, 70),
            new Cine(3, "Viaje a las estrellas", today.AddDays(8), 16000m, 90),
            new Cine(4, "Risas de domingo", today.AddDays(10), 11000m, 60),
            new Concierto(5, "Noche de rock", today.AddDays(12), 50000m, 150),
            new Concierto(6, "Acústico bajo las luces", today.AddDays(15), 42000m, 100),
            new Concierto(7, "Festival de verano", today.AddDays(20), 68000m, 200),
            new Concierto(8, "Clásicos en vivo", today.AddDays(25), 55000m, 120),
            new Taller(9, "Introducción a la fotografía", today.AddDays(4), 30000m, 25),
            new Taller(10, "Cocina creativa", today.AddDays(9), 45000m, 20),
            new Taller(11, "Pintura para principiantes", today.AddDays(14), 28000m, 18),
            new Taller(12, "Escritura de cuentos", today.AddDays(18), 35000m, 22)
        ];

        return new EventCatalog(events);
    }
}
