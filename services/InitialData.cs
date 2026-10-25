using PLANAZO_POO_CSharp.Models;

namespace PLANAZO_POO_CSharp.Services;

public static class InitialData
{
    public static EventCatalog CreateCatalog()
    {
        var today = DateTime.Today;
        Event[] events =
        [
            new Movie(1, "La aventura comienza", today.AddDays(3), 12000m, 80),
            new Movie(2, "Misterio en la ciudad", today.AddDays(5), 14000m, 70),
            new Movie(3, "Viaje a las estrellas", today.AddDays(8), 16000m, 90),
            new Movie(4, "Risas de domingo", today.AddDays(10), 11000m, 60),
            new Concert(5, "Noche de rock", today.AddDays(12), 50000m, 150),
            new Concert(6, "Acústico bajo las luces", today.AddDays(15), 42000m, 100),
            new Concert(7, "Festival de verano", today.AddDays(20), 68000m, 200),
            new Concert(8, "Clásicos en vivo", today.AddDays(25), 55000m, 120),
            new Workshop(9, "Introducción a la fotografía", today.AddDays(4), 30000m, 25),
            new Workshop(10, "Cocina creativa", today.AddDays(9), 45000m, 20),
            new Workshop(11, "Pintura para principiantes", today.AddDays(14), 28000m, 18),
            new Workshop(12, "Escritura de cuentos", today.AddDays(18), 35000m, 22),
            new Festival(13, "Feria del emprendimiento", today.AddDays(30), 40000m, 180),
            new Festival(14, "Carnaval cultural", today.AddDays(35), 55000m, 250),
            new Festival(15, "Expo gastronomía", today.AddDays(40), 35000m, 160),
            new Festival(16, "Maratón de la música", today.AddDays(45), 60000m, 300)
        ];

        return new EventCatalog(events);
    }

    public static User[] CreateUsers()
    {
        return
        [
            new User(1, "Ana Gómez"),
            new User(2, "Luis Rodríguez"),
            new User(3, "Marta Fernández")
        ];
    }
}

