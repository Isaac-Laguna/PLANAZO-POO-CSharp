using PLANAZO_POO_CSharp.Models;

namespace PLANAZO_POO_CSharp.Services;

public sealed class EventCatalog
{
    private readonly List<Evento> _events;

    public EventCatalog(IEnumerable<Evento> events)
    {
        _events = events.ToList();
    }

    public IReadOnlyList<Evento> GetAll() => _events.AsReadOnly();

    public Evento? FindById(int id) => _events.FirstOrDefault(item => item.Id == id);
}
