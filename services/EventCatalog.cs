using PLANAZO_POO_CSharp.Models;

namespace PLANAZO_POO_CSharp.Services;

public sealed class EventCatalog(IEnumerable<Event> events)
{
    private readonly IEnumerable<Event> _events = events;

    public IReadOnlyList<Event> GetAll() => _events.ToList().AsReadOnly();
    
    public Event? FindById(int id) => _events.FirstOrDefault(item => item.Id == id);
}
