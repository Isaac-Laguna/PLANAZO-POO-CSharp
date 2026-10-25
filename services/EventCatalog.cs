using PLANAZO_POO_CSharp.Models;

namespace PLANAZO_POO_CSharp.Services;

public sealed class EventCatalog(IEnumerable<Event> events)
{
    public IReadOnlyList<Event> GetAll() => events.ToList().AsReadOnly();
    
    public Event? FindById(int id) => events.FirstOrDefault(item => item.Id == id);
}
